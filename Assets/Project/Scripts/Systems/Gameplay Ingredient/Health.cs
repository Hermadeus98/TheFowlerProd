using QRCode.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace TheFowler
{
    public class Health : BattleActorComponent
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;

        public FloatUnityEvent onDamaged, onHealed;
        public UnityEvent onDeath, onResurect;

        public void Initialize(float health)
        {
            maxHealth = currentHealth = health;
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
        }

        [Button]
        public void Heal(float heal)
        {
            if(heal == 0)
                return;
            
            currentHealth += heal;
            if (currentHealth > maxHealth) currentHealth = maxHealth;
            onHealed?.Invoke(currentHealth);
        }

        [Button]
        public void Kill()
        {
            currentHealth = 0;
            onDamaged?.Invoke(currentHealth);
            Death();
        }

        [Button]
        private void Death()
        {
            onDeath?.Invoke();
            ReferedActor.BattleActorInfo.isDeath = true;
        }

        [Button]
        public void Resurect(int health)
        {
            currentHealth = health;
            onHealed?.Invoke(currentHealth);
            onResurect?.Invoke();
            ReferedActor.BattleActorInfo.isDeath = false;
        }
    }
}
