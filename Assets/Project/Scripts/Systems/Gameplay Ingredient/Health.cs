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

        public FillBar FillBar => fillBar;
        public float CurrentHealth => currentHealth;

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
            
            onDamaged?.Invoke(currentHealth);
            fillBar?.SetFill(currentHealth);
            ReferedActor.AllyData?.Refresh();
        }

        [Button]
        public void Heal(float heal)
        {
            if(heal == 0)
                return;
            
            currentHealth += heal;
            if (currentHealth > maxHealth) currentHealth = maxHealth;
            onHealed?.Invoke(currentHealth);
            fillBar?.SetFill(currentHealth);
            ReferedActor.AllyData?.Refresh();
        }

        [Button]
        public void Kill()
        {
            currentHealth = 0;
            onDamaged?.Invoke(currentHealth);
            fillBar?.SetFill(currentHealth);
            ReferedActor.AllyData?.Refresh();

            Death();
        }

        private void Death()
        {
            onDeath?.Invoke();
            ReferedActor.BattleActorInfo.isDeath = true;
            ReferedActor.BattleActorAnimator.Death();
        }

        [Button]
        public void Resurect(int health)
        {
            currentHealth = health;
            onHealed?.Invoke(currentHealth);
            fillBar?.SetFill(currentHealth);
            ReferedActor.AllyData?.Refresh();

            onResurect?.Invoke();
            ReferedActor.BattleActorInfo.isDeath = false;
        }
    }
}
