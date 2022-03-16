using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.VFX;

namespace TheFowler
{
    [AddComponentMenu("")]
    [FeedbackPath("TheFowler/VFX Graph Play")]
    public class MMPlayVFXGraph : MMFeedback
    {
        public enum Modes { Play, Stop, Pause }

        public Modes Mode = Modes.Play;
        
        public VisualEffect BoundParticleSystem;
        
        public bool MoveToPosition = false;
        
        public List<VisualEffect> RandomParticleSystems;

        /// <summary>
        /// On init we stop our particle system
        /// </summary>
        /// <param name="owner"></param>
        protected override void CustomInitialization(GameObject owner)
        {
            base.CustomInitialization(owner);
            StopParticles();
        }

        /// <summary>
        /// On play we play our particle system
        /// </summary>
        /// <param name="position"></param>
        /// <param name="feedbacksIntensity"></param>
        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1.0f)
        {
            if (!Active)
            {
                return;
            }
            PlayParticles(position);
        }
        
        /// <summary>
        /// On Stop, stops the particle system
        /// </summary>
        /// <param name="position"></param>
        /// <param name="feedbacksIntensity"></param>
        protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1.0f)
        {
            if (!Active)
            {
                return;
            }
            StopParticles();
        }

        /// <summary>
        /// On Reset, stops the particle system 
        /// </summary>
        protected override void CustomReset()
        {
            base.CustomReset();

            if (InCooldown)
            {
                return;
            }

            StopParticles();
        }

        /// <summary>
        /// Plays a particle system
        /// </summary>
        /// <param name="position"></param>
        protected virtual void PlayParticles(Vector3 position)
        {
            if (MoveToPosition)
            {
                BoundParticleSystem.transform.position = position;
                foreach (VisualEffect system in RandomParticleSystems)
                {
                    system.transform.position = position;
                }
            }

            if (RandomParticleSystems.Count > 0)
            {
                int random = Random.Range(0, RandomParticleSystems.Count);
                switch (Mode)
                {
                    case Modes.Play:
                        RandomParticleSystems[random].Play();
                        break;
                    case Modes.Stop:
                        RandomParticleSystems[random].Stop();
                        break;
                    case Modes.Pause:
                        RandomParticleSystems[random].pause = true;
                        break;
                }
                return;
            }
            else if (BoundParticleSystem != null)
            {
                switch (Mode)
                {
                    case Modes.Play:
                        BoundParticleSystem?.Play();
                        break;
                    case Modes.Stop:
                        BoundParticleSystem?.Stop();
                        break;
                    case Modes.Pause:
                        if (BoundParticleSystem.IsNotNull())
                        {
                            BoundParticleSystem.pause = true;
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// Stops all particle systems
        /// </summary>
        protected virtual void StopParticles()
        {
            foreach(VisualEffect system in RandomParticleSystems)
            {
                system?.Stop();
            }
            if (BoundParticleSystem != null)
            {
                BoundParticleSystem.Stop();
            }            
        }
    }
}
