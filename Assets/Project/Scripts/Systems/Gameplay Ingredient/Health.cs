using System.Linq;
using MoreMountains.Feedbacks;
using QRCode.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace TheFowler
{
    public class Health : BattleActorComponent
    {
        [SerializeField] private FillBar fillBar;
        
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;

        public FloatUnityEvent onDamaged, onHealed;
        public UnityEvent onDeath, onResurect;

        [SerializeField] private MMFeedbacks onDamageFeedbacks, onHealFeedbacks;
        private MMPopupText popupDamageComponent;
        private MMPopupText popupHealComponent;
        [SerializeField] private TextMeshProUGUI lifeTxt;
        
        public FillBar FillBar => fillBar;
        public float CurrentHealth => currentHealth;
        public float MaxHealth => maxHealth;



        public float NormalizedHealth
        {
            get
            {
                if (maxHealth == 0)
                    return 1f;
                else
                {
                   return currentHealth / maxHealth;
                }
            }
        } 
        
        public void Initialize(float health)
        {
            maxHealth = currentHealth = health;
            fillBar?.SetMaxValue(maxHealth);
            fillBar?.SetFill(currentHealth);
            
            ReferedActor.AllyData?.Refresh();
            
            popupDamageComponent = onDamageFeedbacks.Feedbacks.First(w => w.GetType() == typeof(MMPopupText)) as MMPopupText;
            popupHealComponent = onHealFeedbacks.Feedbacks.First(w => w.GetType() == typeof(MMPopupText)) as MMPopupText;
            if(lifeTxt != null)lifeTxt.text = health.ToString();
        }

        [Button]
        public void TakeDamage(float damage)
        {
            if(damage == 0 ||currentHealth <= 0)
                return;
            
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Death();
            }
            else
            {
                ReferedActor.punchline.PlayPunchline(PunchlineEnum.DAMAGETAKEN);
            }
            
            popupDamageComponent.message = damage.ToString();

            ReferedActor.BattleActorStats.health = currentHealth;
            onDamaged?.Invoke(currentHealth);
            fillBar?.SetFill(currentHealth);
            

            ReferedActor.AllyData?.Refresh();
            ReferedActor.AllyData?.ShakeHearth();

            if (lifeTxt != null) lifeTxt.text = currentHealth.ToString();
        }

        public void SetCurrentHealth(float value)
        {
            currentHealth = value;
            ReferedActor.BattleActorStats.health = currentHealth;
            ReferedActor.AllyData?.Refresh();
            ReferedActor.AllyData?.ShakeHearth();

            if (value == 0) Kill();
        }

        [Button]
        public void Heal(float heal)
        {
            if(currentHealth == 0)
                return;

            popupHealComponent.message = heal.ToString();

            ReferedActor.BattleActorStats.health = currentHealth;
            onHealed?.Invoke(currentHealth);
            fillBar?.SetFill(currentHealth);
            
            
            currentHealth += heal;
            if (currentHealth > maxHealth) currentHealth = maxHealth;
            
            ReferedActor.AllyData?.Refresh();

            if (lifeTxt != null) lifeTxt.text = currentHealth.ToString();
        }

        [Button]
        public void Kill()
        {
            currentHealth = 0;
            onDamaged?.Invoke(currentHealth);
            fillBar?.SetFill(currentHealth);
            ReferedActor.AllyData?.Refresh();
            ReferedActor.BattleActorStats.health = currentHealth;

            Death();
        }


        private void Death()
        {
            onDeath?.Invoke();
            ReferedActor.BattleActorInfo.isDeath = true;
            ReferedActor.OnDeath();
            ReferedActor.BattleActorStats.health = currentHealth;

            ReferedActor.punchline.PlayPunchline(PunchlineEnum.DEATH);
            PunchlineAllyDeath();
            BattleManager.CurrentBattleActor.punchline.PlayPunchline(PunchlineEnum.KILL);

        }

        private void PunchlineAllyDeath()
        {
            if (BattleManager.GetAllAllies().Contains(ReferedActor))
            {
                for (int i = 0; i < BattleManager.GetAllAllies().Length; i++)
                {
                    BattleActor act = BattleManager.GetAllAllies()[i];

                    if (act != ReferedActor)
                    {
                        if (act.BattleActorInfo.isDeath == false)
                        {
                            act.punchline.PlayPunchline(PunchlineEnum.ALLYDEATH);
                        }

                    }
                }
            }
            else
            {
                for (int i = 0; i < BattleManager.GetAllEnemies().Length; i++)
                {
                    BattleActor act = BattleManager.GetAllEnemies()[i];

                    if (act != ReferedActor)
                    {
                        if (act.BattleActorInfo.isDeath == false)
                        {
                            act.punchline.PlayPunchline(PunchlineEnum.ALLYDEATH);
                        }

                    }
                }

            }
        }

        [Button]
        public void Resurect(float healthPercent)
        {
            if (currentHealth > 0) return;

            healthPercent /= 100f;
            float x = maxHealth * healthPercent;
            x = Mathf.CeilToInt(x);
                
            currentHealth = x;
            onHealed?.Invoke(currentHealth);
            fillBar?.SetFill(currentHealth);
            ReferedActor.AllyData?.Refresh();
            ReferedActor.BattleActorStats.health = currentHealth;

            onResurect?.Invoke();
            ReferedActor.BattleActorInfo.isDeath = false;
            ReferedActor.Resurect();

            if (lifeTxt != null) lifeTxt.text = currentHealth.ToString();
        }
    }
}
