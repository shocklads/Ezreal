using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace AddonTemplate
{
    public static class KillSteal
    {
        private static AIHeroClient myHero { get { return ObjectManager.Player; } }
        public static bool IsKsable(this AIHeroClient target, SpellSlot spell)
        {

            var totalHealth = target.TotalShieldHealth();

            var hero = target as AIHeroClient;
            if (hero != null)
            {
                if (hero.HasUndyingBuff() || hero.HasSpellShield() || hero.IsInvulnerable)
                {
                    return false;
                }
                if (hero.ChampionName == "Blitzcrank" && !target.HasBuff("BlitzcrankManaBarrierCD") && !target.HasBuff("ManaBarrier"))
                {
                    totalHealth += target.Mana / 2;
                }
            }
            return (myHero.GetSpellDamage(target, spell, DamageLibrary.SpellStages.Default) >= totalHealth);
        }

        public static float TotalShieldHealth(this Obj_AI_Base target)
        {
            return target.Health + target.AllShield + target.AttackShield + target.MagicShield;
        }
        public static bool HasUndyingBuff(this AIHeroClient target)
        {
            //TODO :  CHECK NAME + IsInvicible
            if (target.Buffs.Any(
                b => b.IsValid() &&
                     (b.Name == "ChronoShift" /* Zilean R */||
                      b.Name == "FioraW" || /* Fiora Riposte */
                      b.Name == "BardRStasis" || /* Bard ult */
                      b.Name == "JudicatorIntervention" /* Kayle R */||
                      b.Name == "Undying Rage" /* Tryndamere R */)))
            {
                return true;
            }

            // Poppy R
            if (target.ChampionName == "Poppy")
            {
                if (EntityManager.Heroes.Allies.Any(o => !o.IsMe && o.Buffs.Any(b => b.Caster.NetworkId == target.NetworkId && b.IsValid() && b.DisplayName == "PoppyDITarget")))
                {
                    return true;
                }
            }

            return target.IsInvulnerable;
        }

        public static bool HasSpellShield(this AIHeroClient target)
        {
            // Various spellshields
            return target.HasBuffOfType(BuffType.SpellShield) || target.HasBuffOfType(BuffType.SpellImmunity);
        }
    }
}
