using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace TheFowler
{
    public class HarmonisationInitializer : MonoBehaviour
    {
        [SerializeField] private HarmonisationData[] harmonisationDatas;
        [SerializeField] private BehaviourTree tree;
        private bool isTrigger;
        private float elapsedTime = 0;
        private int id = 0;
        private DialogueNode dialogueNode;
        [Button]
        public void TriggerHarmonisation()
        {

            isTrigger = true;
            LaunchDialogue();
            for (int i = 0; i < harmonisationDatas.Length; i++)
            {
                switch (harmonisationDatas[i].friendName)
                {

                    case FriendName.ABI:
                        SetActor(Player.Abigael, harmonisationDatas[i]);
                        break;
                    case FriendName.PHOEBE:
                        SetActor(Player.Pheobe, harmonisationDatas[i]);
                        break;

                        
                }
                     
            }

        }

        private void SetActor(Character chara, HarmonisationData data)
        {

            if(chara != null)
            {
                chara?.harmonisationComponent.InitializeHarmonisation(data.phaseToPlay, data.harmonisationpoint);
                chara.Controller.SetController(ControllerEnum.NAV_MESH_CONTROLLER);
                for (int j = 0; j < chara?.Controller.controllers.Length; j++)
                {
                    if (chara?.Controller.controllers[j] as NavMeshController)
                    {
                        NavMeshController var = chara?.Controller.controllers[j] as NavMeshController;
                        StartCoroutine(WaitToGoToDestination(var, data.harmonisationpoint));

                    }
                }


            }

        }

        private IEnumerator WaitToGoToDestination(NavMeshController controller, Transform dest)
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            controller.GoTo(dest);

            yield return null;
        }


        private void LaunchDialogue()
        {
            UI.OpenView(UI.Views.StaticDialogs);
            Next();
        }

        private void Next()
        {
            dialogueNode = tree.nodes[id] as DialogueNode;
            elapsedTime = 0;

            UI.RefreshView(UI.Views.StaticDialogs, new DialogueArg()
            {
                Dialogue = dialogueNode.dialogue,
                DialogueNode = dialogueNode,
            });
            SoundManager.PlaySound(dialogueNode.dialogue.voice, gameObject);
            
            
        }

        private void Update()
        {
            if (isTrigger)
            {
                
                if(elapsedTime >= dialogueNode.dialogue.displayDuration)
                {
                    
                    if(id == tree.nodes.Count - 1)
                    {
                        UI.CloseView(UI.Views.StaticDialogs);
                        isTrigger = false;
                        id = 0;
                    }
                    else
                    {
                        id++;
                        Next();
                        
                    }
                    
                }
                else
                {
                    elapsedTime += Time.deltaTime;
                }
            }
        }
    }

    public enum FriendName
    {
        ABI,
        PHOEBE
    }

    [System.Serializable]
    public struct HarmonisationData
    {
        public FriendName friendName;
        public GameplayPhase phaseToPlay;
        public Transform harmonisationpoint;
    }

    
}

