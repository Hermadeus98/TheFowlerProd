using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace TheFowler
{
    public class HarmonisationInitializer : MonoBehaviour
    {
        [SerializeField] private FriendName[] friendName;
        [SerializeField] private GameplayPhase phaseToPlay;
        [SerializeField] private DialogueNode node;
        private bool isTrigger;
        private float elapsedTime = 0;
        [Button]
        public void TriggerHarmonisation()
        {

            isTrigger = true;
            
            for (int i = 0; i < friendName.Length; i++)
            {
                switch (friendName[i])
                {
                    case FriendName.ABI:
                        Player.Abigael?.InitializeHarmonisation(phaseToPlay);
                        LaunchDialogue(Player.Abigael.gameObject);
                        break;
                    case FriendName.PHOEBE:
                        Player.Pheobe?.InitializeHarmonisation(phaseToPlay);
                        break;
                }
                     
            }

        }

        private void LaunchDialogue(GameObject actor)
        {
            UI.OpenView(UI.Views.StaticDialogs);
            UI.RefreshView(UI.Views.StaticDialogs, new DialogueArg()
            {
                Dialogue = node.dialogue,
                DialogueNode = node,
            });
            SoundManager.PlaySound(node.dialogue.voice, actor);
        }

        private void Update()
        {
            if (isTrigger)
            {
                
                if(elapsedTime >= node.dialogue.displayDuration)
                {
                    UI.CloseView(UI.Views.StaticDialogs);
                    isTrigger = false;
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

