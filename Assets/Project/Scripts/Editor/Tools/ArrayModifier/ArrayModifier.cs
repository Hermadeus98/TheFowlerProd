#if UNITY_EDITOR
using System;
using Sirenix.OdinInspector;

using UnityEditor;

using UnityEngine;

namespace TheFowler.Editor
{

    public class ArrayModifier : SerializedMonoBehaviour
    {
        [BoxGroup("Main Settings")]
        [OnValueChanged("UpdateArray")]
        [Required, SerializeField] private GameObject instancePrefab;

        [BoxGroup("Main Settings")]
        [OnValueChanged("UpdateArray")]
        [SerializeField] private MeshRenderer meshReference;
        
        [BoxGroup("Main Settings")]
        [Range(0,100)]
        [OnValueChanged("UpdateArray")]
        [SerializeField] private int 
            CountX = 1, 
            CountY = 1,
            CountZ = 1;
        
        [BoxGroup("Main Settings")]
        [OnValueChanged("UpdateArray")]
        [SerializeField] private Vector3 offset = Vector3.zero;
       
        [FoldoutGroup("OtherSettings")]
        [Title("Curvature")]
        [SerializeField] private AnimationCurve 
            offsetxCurvature = new AnimationCurve(new Keyframe(0,0f), new Keyframe(1f, 0f)),
            offsetyCurvature = new AnimationCurve(new Keyframe(0,0f), new Keyframe(1f, 0f)),
            offsetzCurvature = new AnimationCurve(new Keyframe(0,0f), new Keyframe(1f, 0f));
        
        [FoldoutGroup("OtherSettings")] 
        [OnValueChanged("UpdateArray")]
        [SerializeField] private float
            multiplierXCurvature = 1f,
            multiplierYCurvature = 1f,
            multiplierZCurvature = 1f;

        [OnValueChanged("UpdateScale")] 
        [Title("Rotation")] 
        [FoldoutGroup("OtherSettings")] 
        [SerializeField] private bool scale = false;
        
        [FoldoutGroup("OtherSettings")]
        [OnValueChanged("UpdateScale")]
        [HideIf("@this.scale == false")]
        [SerializeField] private Vector3 scaleMultiplier = Vector3.one;
        
        [OnValueChanged("UpdateArray")]
        [Title("Rotation")]
        [FoldoutGroup("OtherSettings")] 
        [SerializeField] private bool rotation = false;
        
        [FoldoutGroup("OtherSettings")]
        [OnValueChanged("UpdateArray")]
        [HideIf("@this.rotation == false")]
        [SerializeField] private Vector3 rotationOffset;

        [FoldoutGroup("OtherSettings")]
        [OnValueChanged("UpdateArray")]
        [HideIf("@this.rotation == false")]
        [SerializeField]
        private bool
            isIncrementalX = false,
            isIncrementalY = false,
            isIncrementalZ = false;
        
        [Button]
        private void UpdateArray()
        {
            Reset();
            Apply();
        }
        
        private void Apply()
        {
            if(instancePrefab == null || meshReference == null)
                return;

            if(EditorApplication.isPlayingOrWillChangePlaymode)
                return;

            if (instancePrefab == null)
                throw new Exception($"Instance is missing.");

            for (int x = 0; x < CountX; x++)
            {
                for (int y = 0; y < CountY; y++)
                {
                    for (int z = 0; z < CountZ; z++)
                    {


                        var newObj = PrefabUtility.InstantiatePrefab(instancePrefab, transform) as GameObject;


                        float c_x = multiplierXCurvature * offsetxCurvature.Evaluate((float)x / (float)CountX);
                        float c_y = multiplierYCurvature * offsetyCurvature.Evaluate((float)y / (float)CountY);
                        float c_z = multiplierZCurvature * offsetzCurvature.Evaluate((float)z / (float)CountZ);
                        
                        newObj.transform.localPosition = new Vector3(
                            (meshReference.bounds.size.x + offset.x + c_x) * x,
                            (meshReference.bounds.size.y + offset.y + c_y) * y,
                            (meshReference.bounds.size.z + offset.z + c_z) * z
                        );

                        if (rotation)
                        {
                            if(!isIncrementalX && !isIncrementalY && !isIncrementalZ)
                                newObj.transform.eulerAngles = rotationOffset;

                            newObj.transform.eulerAngles = new Vector3(
                                 isIncrementalX ? rotationOffset.x * x : rotationOffset.x,
                                 isIncrementalY ? rotationOffset.y * y : rotationOffset.y,
                                 isIncrementalZ ? rotationOffset.z * z : rotationOffset.z                                   
                            );
                        }
                    }
                }
            }
        }

        private void UpdateScale()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var t = transform.GetChild(i).transform;
                t.localScale = scaleMultiplier;
            }
        }

        private void Reset()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

        [Button]
        private void ResetCurvature()
        {
            offsetxCurvature = new AnimationCurve(new Keyframe(0, 0f), new Keyframe(1f, 0f));
            offsetyCurvature = new AnimationCurve(new Keyframe(0, 0f), new Keyframe(1f, 0f));
            offsetzCurvature = new AnimationCurve(new Keyframe(0,0f), new Keyframe(1f, 0f));
        }

    }

}
#endif