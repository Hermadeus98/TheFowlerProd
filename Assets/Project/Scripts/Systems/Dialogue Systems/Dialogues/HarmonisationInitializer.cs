using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace TheFowler
{
    public class HarmonisationInitializer : MonoBehaviour
    {
        [SerializeField] private FriendName[] friendName;
        [SerializeField] private GameplayPhase[] phaseToPlay;
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
            for (int i = 0; i < friendName.Length; i++)
            {
                switch (friendName[i])
                {

                    case FriendName.ABI:
                        Player.Abigael?.InitializeHarmonisation(phaseToPlay[0]);
                        break;
                    case FriendName.PHOEBE:
                        Player.Pheobe?.InitializeHarmonisation(phaseToPlay[1]);
                        break;
                }
                     
            }

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

    
}

