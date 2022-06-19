using System;
using System.Collections;
using System.Linq;
using MoreMountains.Feedbacks;
using QRCode.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Random = UnityEngine.Random;

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

        public void SetFillBar(FillBar fb) => fillBar = fb;

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
        public void TakeDamage(float damage, DamageCalculator.ResistanceFaiblesseResult result = DamageCalculator.ResistanceFaiblesseResult.NEUTRE, bool leaveOneHP = false)
        {
            if(damage == 0 ||currentHealth <= 0)
                return;

            damage = Mathf.RoundToInt(damage);

            PunchlineData hitData = null;
            
            ReferedActor.BattleActorAnimator.Hit();

            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;

                if (leaveOneHP)
                    currentHealth = 1;
                else
                    Death();
            }
            else
            {
                if (currentHealth < maxHealth / 4f)
                {
                    ReferedActor.punchline.PlayPunchline(PunchlineCallback.LOW_HP, out hitData);
                }
                else
                {
                    if (damage < 100)
                    {
                        ReferedActor.punchline.PlayPunchline(PunchlineCallback.DAMAGE_TAKEN_LOW, out hitData);
                    }
                    else
                    {
                        ReferedActor.punchline.PlayPunchline(PunchlineCallback.DAMAGE_TAKEN_HIGH, out hitData);
                    }
                }
            }

            switch (result)
            {
                case DamageCalculator.ResistanceFaiblesseResult.RESISTANCE:
                    popupDamageComponent.extraImage = Spawnables.Instance.resist;
                    break;
                case DamageCalculator.ResistanceFaiblesseResult.FAIBLESSE:
                    popupDamageComponent.extraImage = Spawnables.Instance.weak;
                    break;
                case DamageCalculator.ResistanceFaiblesseResult.NEUTRE:
                    popupDamageComponent.extraImage = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(result), result, null);
            }

            popupDamageComponent.message = "-" + damage.ToString();

            popupDamageComponent.setSize = true;
            popupDamageComponent.sizePercent = Mathf.Clamp01(damage / 400f);

            ReferedActor.BattleActorStats.health = currentHealth;
            onDamaged?.Invoke(currentHealth);
            fillBar?.SetFill(currentHealth);

            ReferedActor.AllyData?.Refresh();
            ReferedActor.AllyData?.ShakeHearth();

            if (lifeTxt != null) lifeTxt.text = currentHealth.ToString();
            
            StartCoroutine(AllyReaction(hitData.soundDuration));
        }
        
        IEnumerator AllyReaction(float wait)
        {
            yield return new WaitForSeconds(wait);
            
            if(ReferedActor is EnemyActor)
                yield break;
            
            //Robyn
            if (ReferedActor == BattleManager.CurrentBattle.robyn)
            {
                var rand = Random.Range(0, 3);
                if (rand == 0 && !ReferedActor.BattleActorInfo.isDeath)
                {
                    ReferedActor.punchline.PlayPunchline(PunchlineCallback.REACT_ROBYN_ROBYN);
                    //React to herself
                }
                else if (rand == 1 && BattleManager.CurrentBattle.abi != null)
                {
                    if (!BattleManager.CurrentBattle.abi.BattleActorInfo.isDeath)
                    {
                        BattleManager.CurrentBattle.abi.punchline.PlayPunchline(PunchlineCallback.REACT_ABI_ROBYN);
                        //Abi react
                    }
                }
                else if (rand == 2  && BattleManager.CurrentBattle.phoebe != null)
                {
                    if (!BattleManager.CurrentBattle.phoebe.BattleActorInfo.isDeath)
                    {
                        BattleManager.CurrentBattle.phoebe.punchline.PlayPunchline(PunchlineCallback.REACT_PHOEBE_ROBYN);
                        //Phoebe react
                    }
                }
            }
            else if (ReferedActor == BattleManager.CurrentBattle.abi)
            {
                var rand = Random.Range(0, 3);
                if (rand == 0 && !ReferedActor.BattleActorInfo.isDeath)
                {
                    ReferedActor.punchline.PlayPunchline(PunchlineCallback.REACT_ABI_ABI);
                    //React to herself
                }
                else if (rand == 1 && BattleManager.CurrentBattle.robyn != null)
                {
                    if (!BattleManager.CurrentBattle.robyn.BattleActorInfo.isDeath)
                    {
                        BattleManager.CurrentBattle.robyn.punchline.PlayPunchline(PunchlineCallback.REACT_ROBYN_ABI);
                        //Robyn react
                    }
                }
                else if (rand == 2 && BattleManager.CurrentBattle.phoebe != null)
                {
                    if (!BattleManager.CurrentBattle.phoebe.BattleActorInfo.isDeath)
                    {
                        BattleManager.CurrentBattle.phoebe.punchline.PlayPunchline(PunchlineCallback.REACT_PHOEBE_ABI);
                        //Phoebe react
                    }
                }
            }
            else if (ReferedActor == BattleManager.CurrentBattle.phoebe)
            {
                var rand = Random.Range(0, 3);
                if (rand == 0 && !ReferedActor.BattleActorInfo.isDeath)
                {
                    ReferedActor.punchline.PlayPunchline(PunchlineCallback.REACT_PHOEBE_PHOEBE);
                    //React to herself
                }
                else if (rand == 1 && BattleManager.CurrentBattle.robyn != null)
                {
                    if (!BattleManager.CurrentBattle.robyn.BattleActorInfo.isDeath)
                    {
                        BattleManager.CurrentBattle.robyn.punchline.PlayPunchline(PunchlineCallback.REACT_ROBYN_PHOEBE);
                        //Robyn react
                    }
                }
                else if (rand == 2 && BattleManager.CurrentBattle.abi != null)
                {
                    if (!BattleManager.CurrentBattle.abi.BattleActorInfo.isDeath)
                    {
                        BattleManager.CurrentBattle.abi.punchline.PlayPunchline(PunchlineCallback.REACT_ABI_PHOEBE);
                        //Phoebe react
                    }
                }
            }
            
            yield break;
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

            heal = Mathf.RoundToInt(heal);
            
            currentHealth += heal;

            popupHealComponent.message = "+" + heal.ToString();
            
            ReferedActor.BattleActorStats.health = currentHealth;
            onHealed?.Invoke(currentHealth);
            fillBar?.SetFill(currentHealth);

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

            //ReferedActor.punchline.PlayPunchline(PunchlineEnum.DEATH);
            PunchlineAllyDeath();
            //BattleManager.CurrentBattleActor.punchline.PlayPunchline(PunchlineEnum.KILL);
            if (!Tutoriel.hasTriggerDeath)
            {
                if (ReferedActor.BattleActorInfo.isAlly)
                {
                    Tutoriel.hasTriggerDeath = true;
                }
            }



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
                            //act.punchline.PlayPunchline(PunchlineEnum.ALLYDEATH);
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
                            //act.punchline.PlayPunchline(PunchlineEnum.ALLYDEATH);
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
                
            ReferedActor.BattleActorInfo.isDeath = false;
            
            currentHealth = x;
            onHealed?.Invoke(currentHealth);
            fillBar?.SetFill(currentHealth);
            ReferedActor.AllyData?.SetGraphicToNormal();
            ReferedActor.AllyData?.Refresh();
            ReferedActor.BattleActorStats.health = currentHealth;

            onResurect?.Invoke();
            
            ReferedActor.OnResurect();

            if (lifeTxt != null) lifeTxt.text = currentHealth.ToString();
        }

        public void ResetHealth()
        {
            ReferedActor.BattleActorInfo.isDeath = false;
            currentHealth = maxHealth;
            fillBar?.SetFill(currentHealth);
            ReferedActor.AllyData?.Refresh();
            ReferedActor.BattleActorStats.health = currentHealth;
        }

        public bool IsMidLife()
        {
            if (currentHealth == 0)
                return false;
            
            return currentHealth < maxHealth / 2f;
        }
        
        public bool IsQuartLife()
        {
            if (currentHealth == 0)
                return false;
            
            return currentHealth < maxHealth / 4f;
        }
    }
}
