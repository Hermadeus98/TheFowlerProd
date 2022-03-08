using System.Linq;
using MoreMountains.Feedbacks;
using QRCode.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

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
        }

        [Button]
        public void TakeDamage(float damage)
        {
            if(damage == 0)
                return;
            
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Death();
            }
            
            popupDamageComponent.message = damage.ToString();

            onDamaged?.Invoke(currentHealth);
            fillBar?.SetFill(currentHealth);
            ReferedActor.AllyData?.Refresh();
            ReferedActor.BattleActorStats.health = currentHealth;
        }

        [Button]
        public void Heal(float heal)
        {
            if(heal == 0)
                return;

            popupHealComponent.message = heal.ToString();
            
            currentHealth += heal;
            if (currentHealth > maxHealth) currentHealth = maxHealth;
            onHealed?.Invoke(currentHealth);
            fillBar?.SetFill(currentHealth);
            ReferedActor.AllyData?.Refresh();
            ReferedActor.BattleActorStats.health = currentHealth;
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
        }

        [Button]
        public void Resurect(int health)
        {
            currentHealth = health;
            onHealed?.Invoke(currentHealth);
            fillBar?.SetFill(currentHealth);
            ReferedActor.AllyData?.Refresh();
            ReferedActor.BattleActorStats.health = currentHealth;

            onResurect?.Invoke();
            ReferedActor.BattleActorInfo.isDeath = false;
        }
    }
}
