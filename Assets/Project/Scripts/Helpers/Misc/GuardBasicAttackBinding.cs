using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using TheFowler;
using UnityEngine;

public class GuardBasicAttackBinding : MonoBehaviour
{
    public MMFeedbacks Feedbacks;

    public VFXDistanceScaler[] DistanceScalers;

    public BattleActor emitter, receiver;

    public Transform hitPS;
    
    [Button]
    public void BindData(Action action)
    {
        if (emitter is EnemyActor enemyActor)
        {
            transform.position = enemyActor.spawnBasicAttack.position;
        }
        
        DistanceScalers.ForEach(w => w.SetTarget(receiver.transform));

        var hit = Feedbacks.Feedbacks.Where(w => w.Label == "Hit").Cast<MMFeedbackAnimation>().ToArray();
        hit.ForEach(w => w.BoundAnimator = receiver.BattleActorAnimator.Animator);
        
        var impact = Feedbacks.Feedbacks.Where(w => w.Label == "Impact").Cast<MMFeedbackParticlesInstantiation>().ToArray();
        impact.ForEach(w => w.InstantiateParticlesPosition = receiver.transform);

        hitPS.position = receiver.transform.position + new Vector3(0f, 1.25f, 0f);

        var damage = Feedbacks.Feedbacks.Find(w => w.Label == "Damage") as MMUnityEvent;
        damage.InstantEvent.AddListener(action.Invoke);
    }

    [Button]
    public void Play()
    {
        Feedbacks.PlayFeedbacks();
    }
}
