using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

namespace TheFowler
{
    public static class PreviewManager
    {
        private static List<BattleActor> oldList;
        
        public static void SetPreviews(List<BattleActor> actors)
        {
            if (BattleManager.IsAllyTurn)
            {
                //Manage Previous Targets
                {
                    //---<GESTION DAMAGE EFFECT>-----------------------------------------------------------------------<
                    DamageEffect damageEffect = null;
                    
                    if (Player.SelectedSpell.ContainEffect<DamageEffect>(out damageEffect))
                    {
                        if (!oldList.IsNullOrEmpty())
                        {
                            for (int i = 0; i < oldList.Count; i++)
                            {
                                oldList[i].Health.FillBar?.HidePreview();
                            }
                        }
                    }
                    
                    if (Player.SelectedSpell.ContainEffect<VampirismeEffect>(out var vampirisme))
                    {
                        if (!oldList.IsNullOrEmpty())
                        {
                            for (int i = 0; i < oldList.Count; i++)
                            {
                                oldList[i].Health.FillBar?.HidePreview();
                            }
                        }
                    }
                }
                
                oldList = new List<BattleActor>(actors);

                //Manage Current Targets
                {
                    //---<GESTION DAMAGE EFFECT>-----------------------------------------------------------------------<
                    DamageEffect damageEffect = null;
                    
                    if (Player.SelectedSpell.ContainEffect<DamageEffect>(out damageEffect))
                    {
                        for (int i = 0; i < actors.Count; i++)
                        {
                            var damage = DamageCalculator.CalculateDamage(damageEffect.damage,
                                BattleManager.CurrentBattleActor, actors[i], damageEffect.ReferedSpell.SpellType,
                                out var result);
                            var previewFill = actors[i].Health.CurrentHealth - damage;
                            actors[i].Health.FillBar?.SetPreview(previewFill);
                        }
                    }
                    
                    
                    if (Player.SelectedSpell.ContainEffect<VampirismeEffect>(out var vampirisme))
                    {
                        for (int i = 0; i < actors.Count; i++)
                        {
                            var damage = DamageCalculator.CalculateDamage(vampirisme.damage,
                                BattleManager.CurrentBattleActor, actors[i], vampirisme.ReferedSpell.SpellType,
                                out var result);
                            var previewFill = actors[i].Health.CurrentHealth - damage;
                            actors[i].Health.FillBar?.SetPreview(previewFill);
                        }
                    }
                }
            }
        }

        public static void EndPreviews()
        {
            //---<GESTION DAMAGE EFFECT>-------------------------------------------------------------------------------<
            if (!oldList.IsNullOrEmpty())
            {
                oldList.ForEach(w => w.Health.FillBar?.HidePreview());
            }

            oldList.Clear();
        }

        public static void HideActorPreview(BattleActor actor)
        {
            actor.Health.FillBar?.HidePreview();
        }

        public static void ShowActorPreview(BattleActor actor)
        {
            actor.Health.FillBar?.ShowPreview();
        }
    }
}
