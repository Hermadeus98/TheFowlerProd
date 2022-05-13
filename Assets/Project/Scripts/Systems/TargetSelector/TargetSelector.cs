using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QRCode;
using QRCode.Extensions;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace TheFowler
{
    public static class TargetSelector
    {
        //private static List<BattleActor> availableTargets;
        public static List<BattleActor> AvailableTargets;
        
        //private static List<BattleActor> selectedTargets;
        public static List<BattleActor> SelectedTargets;

        private static TargetTypeEnum targetType;
        private static int currentIndex;

        public static Action<List<BattleActor>> OnTargetChanged;

        private static bool blockNavigation = false;

        public static void DebugSelectedTargets()
        {
            if (!SelectedTargets.IsNullOrEmpty())
            {
                SelectedTargets.ForEach(w => QRDebug.Log("TARGET SELECTOR", FrenchPallet.ALIZARIN, w.gameObject.name));
            }
        }

        public static void Initialize(TargetTypeEnum targetType)
        {
            TargetSelector.targetType = targetType;
            AvailableTargets = new List<BattleActor>();
            SelectedTargets = new List<BattleActor>();
            currentIndex = 0;
            blockNavigation = false;

            if (Player.SelectedSpell.Effects[0] is BatonPassEffect)
            {
                Debug.Log("CORRECTION ICI");
                if (Player.SelectedSpell.Effects[0] is BatonPassEffect)
                    AvailableTargets.AddRange(GetAllAlliesAndDeadAllies());
                else
                    AvailableTargets.AddRange(GetAllAlliesOf(BattleManager.CurrentBattleActor));
                Select(AvailableTargets[0]);
            }
            else
            {
                if (BattleManager.CurrentBattleActor.BattleActorInfo.isTaunt &&
                    !BattleManager.CurrentBattleActor.GetBattleComponent<Taunt>().taunter.BattleActorInfo.isDeath &&
                    targetType != TargetTypeEnum.ALL_ENEMIES &&
                    targetType != TargetTypeEnum.ALL_ALLIES)
                {
                    var taunt = BattleManager.CurrentBattleActor.GetBattleComponent<Taunt>();
                    AvailableTargets.Add(taunt.taunter);
                    SelectedTargets.Add(taunt.taunter);
                    Select(AvailableTargets[0]);
                    blockNavigation = true;
                }
                else
                {
                    switch (targetType)
                    {
                        case TargetTypeEnum.SELF:
                            AvailableTargets.Add(GetSelf());
                            SelectedTargets.Add(GetSelf());
                            Select(AvailableTargets[0]);
                            break;
                        case TargetTypeEnum.SOLO_ENEMY:
                            AvailableTargets.AddRange(GetAllEnemiesOf(BattleManager.CurrentBattleActor));
                            Select(AvailableTargets[0]);
                            break;
                        case TargetTypeEnum.ALL_ENEMIES:
                            AvailableTargets.AddRange(GetAllEnemiesOf(BattleManager.CurrentBattleActor));
                            SelectedTargets.AddRange(GetAllEnemiesOf(BattleManager.CurrentBattleActor));
                            SelectAll(SelectedTargets);
                            break;
                        case TargetTypeEnum.SOLO_ALLY:
                            if (Player.SelectedSpell.Effects[0] is BatonPassEffect)
                                AvailableTargets.AddRange(GetAllAlliesAndDeadAllies());
                            else
                                AvailableTargets.AddRange(GetAllAlliesOf(BattleManager.CurrentBattleActor));
                            Select(AvailableTargets[0]);
                            break;
                        case TargetTypeEnum.ALL_ALLIES:
                            AvailableTargets.AddRange(GetAllAlliesOf(BattleManager.CurrentBattleActor));
                            SelectedTargets.AddRange(GetAllAlliesOf(BattleManager.CurrentBattleActor));
                            SelectAll(SelectedTargets);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(targetType), targetType, null);
                    }
                }
            }

            if (BattleManager.IsAllyTurn && (targetType == TargetTypeEnum.ALL_ENEMIES || targetType == TargetTypeEnum.SOLO_ENEMY))
            {
                var weak = AvailableTargets.Cast<EnemyActor>().Where(w => w.IsWeakOf(Player.SelectedSpell.SpellType));
                weak.ForEach(w => w.weak.Show());

                var resist = AvailableTargets.Cast<EnemyActor>()
                    .Where(w => w.IsResistantOf(Player.SelectedSpell.SpellType));
                resist.ForEach(w => w.resist.Show());
                
                if (weak.Count() > 0)
                {
                    SoundManager.PlaySound(AudioGenericEnum.TF_SFX_Combat_UI_WeakDisplay, null);
                }
                
                if (weak.Count() == 1 && targetType == TargetTypeEnum.SOLO_ENEMY)
                {
                    Select(weak.ElementAt(0));
                }
                else if (targetType == TargetTypeEnum.ALL_ALLIES)
                {
                    SelectAll(SelectedTargets);
                }
            }

            OnTargetChanged?.Invoke(SelectedTargets);
        }

        public static void Quit()
        {
            if (!AvailableTargets.IsNullOrEmpty())
            {
                for (int i = 0; i < AvailableTargets.Count; i++)
                {
                    if (AvailableTargets[i] is EnemyActor enemyActor)
                    {
                        enemyActor.weak.Hide();
                        enemyActor.resist.Hide();
                    }
                    EndPreview(AvailableTargets[i]);
                }
                AvailableTargets.Clear();
            }

            OnTargetChanged = null;
        }

        public static void ResetSelectedTargets()
        {
            if (!SelectedTargets.IsNullOrEmpty())
            {
                SelectedTargets.Clear();
            }
        }
        
        //---<CORE>----------------------------------------------------------------------------------------------------<
        public static bool Select(bool validate, out IEnumerable<BattleActor> battleActors)
        {
            if (validate)
            {
                battleActors = SelectedTargets;
                return true;
            }

            battleActors = null;
            return false;
        }

        public static void Navigate(bool previous, bool next)
        {
            if(blockNavigation)
                return;
            
            if (targetType == TargetTypeEnum.SOLO_ALLY || targetType == TargetTypeEnum.SOLO_ENEMY)
            {
                if (next)
                {
                    SelectNext();
                }

                if (previous)
                {
                    SelectPrevious();
                }
            }
        }
        
        private static void SelectNext()
        {
            SoundManager.PlaySound(AudioGenericEnum.TF_SFX_Combat_UI_SwitchTarget, null);
            currentIndex++;
            if (currentIndex >= AvailableTargets.Count)
            {
                currentIndex = 0;
            }
            
            Select(AvailableTargets[currentIndex]);
        }

        private static void SelectPrevious()
        {
            SoundManager.PlaySound(AudioGenericEnum.TF_SFX_Combat_UI_SwitchTarget, null);
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = AvailableTargets.Count - 1;
            }

            Select(AvailableTargets[currentIndex]);
        }

        private static void Select(BattleActor actor)
        {
            if (SelectedTargets.Count > 0)
            {
                SelectedTargets.ForEach(w => Deselect(w));
            }

            SelectedTargets.Clear();
            SelectedTargets.Add(actor);
            Preview(actor);
            
            OnTargetChanged?.Invoke(SelectedTargets);
        }

        private static void SelectAll(IEnumerable<BattleActor> actors)
        {
            actors.ForEach(w => w.OnTarget());
        }

        public static void Deselect(BattleActor actor)
        {
            actor.OnEndTarget();
            EndPreview(actor);
        }

        public static void DeselectAll()
        {
            
            for (int i = 0; i < BattleManager.GetAllEnemies().Length; i++)
            {
                BattleManager.GetAllEnemies()[i].OnEndTarget();
                EndPreview(BattleManager.GetAllEnemies()[i]);
            }

            for (int i = 0; i < BattleManager.GetAllAllies().Length; i++)
            {
                BattleManager.GetAllAllies()[i].OnEndTarget();
                EndPreview(BattleManager.GetAllAllies()[i]);
            }
        }


        private static void Preview(BattleActor actor)
        {
            actor.OnTarget();
        }

        private static void EndPreview(BattleActor actor)
        {
            actor.OnEndTarget();
        }
        
        //---<HELPERS>-------------------------------------------------------------------------------------------------<
        public static BattleActor GetSelf()
        {
            return BattleManager.CurrentBattleActor;
        }

        public static BattleActor[] GetAllAlliesOf(BattleActor battleActor)
        {
            if (battleActor is AllyActor)
            {
                return GetAllAllies();
            }
            
            if (battleActor is EnemyActor)
            {
                return GetAllEnemies();
            }

            return null;
        }
        
        public static BattleActor[] GetAllEnemiesOf(BattleActor battleActor)
        {
            if (battleActor is AllyActor)
            {
                return GetAllEnemies();
            }
            
            if (battleActor is EnemyActor)
            {
                return GetAllAllies();
            }

            return null;
        }

        public static BattleActor[] GetAllAlliesAndDeadAllies()
        {
            var list = new List<BattleActor>();
            if (BattleManager.CurrentBattle.abi != null)
            {
                if(BattleManager.CurrentBattle.abi.isParticipant)
                    list.Add(BattleManager.CurrentBattle.abi);
            }

            if (BattleManager.CurrentBattle.phoebe != null)
            {
                if(BattleManager.CurrentBattle.phoebe.isParticipant)
                    list.Add(BattleManager.CurrentBattle.phoebe);
            }

            if (BattleManager.CurrentBattle.robyn != null)
            {
                if(BattleManager.CurrentBattle.robyn.isParticipant)
                    list.Add(BattleManager.CurrentBattle.robyn);
            }

            return list.ToArray();
        }
        
        public static BattleActor[] GetAllEnemies()
        {
            return BattleManager.GetAllEnemies();
        }

        public static BattleActor[] GetAllAllies()
        {
            return BattleManager.GetAllAllies();
        }

        //---<AI>------------------------------------------------------------------------------------------------------<

        public static void SelectAsTarget(this BattleActor actor)
        {
            SelectedTargets = new List<BattleActor>();
            SelectedTargets.Add(actor);
        }

        public static void SelectAsTargets(this IEnumerable<BattleActor> actors)
        {
            SelectedTargets = new List<BattleActor>(actors);
        }
        
        public static BattleActor GetWeakerAlly()
        {
            return GetAllAllies().OrderBy(w => w.Health.CurrentHealth).First();
        }
        
        public static BattleActor GetWeakerEnemy()
        {
            return GetAllEnemies().OrderBy(w => w.Health.CurrentHealth).First();
        }
        
        public static BattleActor GetStrongerAlly()
        {
            return GetAllAllies().OrderByDescending(w => w.Health.CurrentHealth).First();
        }
        
        public static BattleActor GetStrongerEnemy()
        {
            return GetAllEnemies().OrderByDescending(w => w.Health.CurrentHealth).First();
        }

        public static BattleActor GetRandomAlly()
        {
            return GetAllAllies()[Random.Range(0, GetAllAllies().Length)];
        }
        
        public static BattleActor GetRandomEnemy()
        {
            return GetAllEnemies()[Random.Range(0, GetAllEnemies().Length)];
        }
    }

    public interface ITarget
    {
        public void OnTarget();
        public void OnEndTarget();
    }

    public enum TargetTypeEnum
    {
        SELF = 0,
        SOLO_ENEMY = 1,
        ALL_ENEMIES = 2,
        SOLO_ALLY = 3,
        ALL_ALLIES = 4,
    }
}
