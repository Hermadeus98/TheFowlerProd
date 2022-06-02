using QRCode.Extensions;
using Sirenix.OdinInspector;

using UnityEngine;

namespace TheFowler
{
    [ExecuteInEditMode]
    public class BillBoard : MonoBehaviour
    {
        [SerializeField, BoxGroup("References")]
        private Camera m_Camera = default;

        [SerializeField, BoxGroup("Settings")]
        private bool useMainCamera = true;

        public Camera Camera
        {
            get => m_Camera;
            set => m_Camera = value;
        }
        
        private void Start()
        {
            if (useMainCamera)
            {
                var cam = Camera.main;
                m_Camera = cam;
            }
        }

        private void LateUpdate()
        {
            if (m_Camera == null)
            {
                m_Camera = Camera.main;
                return;
            }
            
            var rotation = Camera.transform.rotation;
            transform.LookAt(transform.position + rotation * Vector3.forward,
                rotation * Vector3.up
                );
        }
    }
}
