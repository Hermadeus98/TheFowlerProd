using System.Collections;
using System.Collections.Generic;
using QRCode;
using UnityEngine;
using MoreMountains.Feedbacks;
namespace TheFowler
{
    public class BattleActorComponent : GameplayMonoBehaviour
    {
        [SerializeField] private BattleActor referedActor;

        public BattleActor ReferedActor => referedActor;

        private void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).GetComponent<MMFeedbacks>() != null)
                {
                    ReferedActor.feedbackReferences.FeedbackFromComponents.Add(transform.GetChild(i).GetComponent<MMFeedbacks>());
                }

            }
        }
        public virtual void Initialize()
        {
            
        }

        public virtual void OnTurnStart()
        {
            
        }

    }
}
