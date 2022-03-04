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
                    if (Player.SelectedSpell.ContainEffect<DamageEffect>(out var damageEffect))
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
                    if (Player.SelectedSpell.ContainEffect<DamageEffect>(out var damageEffect))
                    {
                        for (int i = 0; i < actors.Count; i++)
                        {
                            var damage = DamageCalculator.CalculateDamage(damageEffect.damage,
                                BattleManager.CurrentBattleActor, actors[i], damageEffect.ReferedSpell.SpellType);
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
    }
}
