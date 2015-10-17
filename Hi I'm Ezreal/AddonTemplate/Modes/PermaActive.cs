using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using EloBuddy;
using EloBuddy.SDK;
using AddonTemplate;
using EloBuddy.SDK.Events;
using Settings = AddonTemplate.Config.Modes.KillSteal;
using SharpDX;

namespace AddonTemplate.Modes
{
    public sealed class PermaActive : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return true;
        }

        public override void Execute()
        {
            KSChamp();
            AutoCCed();
            KSBuff();
            QIfUnkillable();
        }

        private void QIfUnkillable()
        {
            foreach (var minions in EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.ServerPosition, SpellManager.Q.Range))
            {
                if (Prediction.Health.GetPrediction(minions, (int)Player.Instance.AttackDelay * 1000 + Game.Ping / 2) <= 0)
                {
                    if (Config.Modes.Misc.UseQOnUnkillable &&
                        Player.Instance.GetSpellDamage(minions, SpellSlot.Q) >= minions.Health)
                        Q.Cast(minions);
                }
            }
        }

        private static AIHeroClient myHero { get { return ObjectManager.Player; } }
        public static void KSChamp()
        {
            foreach (AIHeroClient enemy in EntityManager.Heroes.Enemies.Where(enemy => enemy.IsValidTarget(Q.Range)))
            {
                if (Settings.KsQ && Q.IsReady() && enemy.IsKsable(SpellSlot.Q))
                {
                    Q.Cast(enemy);
                }
            }
            foreach (AIHeroClient enemy in EntityManager.Heroes.Enemies.Where(enemy => enemy.IsValidTarget(W.Range)))
            {
                if (Settings.KsW && W.IsReady() && enemy.IsKsable(SpellSlot.W) && (!Q.IsReady() || !enemy.IsKsable(SpellSlot.Q)))
                {
                    W.Cast(enemy);
                }
            }
            foreach (AIHeroClient enemy in EntityManager.Heroes.Enemies.Where(enemy => enemy.IsValidTarget(Int32.MaxValue)))
            {
                if (Settings.KsR && R.IsReady() && enemy.IsKsable(SpellSlot.R)
                    && ((!Q.IsReady() || !enemy.IsKsable(SpellSlot.Q)) && (!W.IsReady() || !enemy.IsKsable(SpellSlot.W))))
                {
                    R.Cast(enemy);
                }
            }
        }

        public static void AutoCCed()
        {
            string[] HardCC =
            {
                "Charm", "Fear", "Flee", "Knockup", "Polymorph", "Sleep", "Slow", "Snare", "Stun", "Suppression", "Taunt"
            };
            foreach (AIHeroClient enemy in EntityManager.Heroes.Enemies)
            {
                foreach (string debuff in HardCC.Where(debuff => enemy.HasBuffOfType((BuffType)Enum.Parse(typeof(BuffType), debuff))))
                {
                    if (Config.Modes.Misc.CcQ && Q.IsReady())
                    {
                        Q.Cast(enemy);
                    }
                    if (Config.Modes.Misc.CcW && W.IsReady())
                    {
                        W.Cast(enemy);
                    }
                }
            }
        }

        public static void KSBuff()
        {
            var MonstersBuff = new List<String>();
            if (Config.Modes.Misc.DragonSteal)
                MonstersBuff.Add("SRU_Dragon");
            if (Config.Modes.Misc.BaronSteal)
                MonstersBuff.Add("SRU_Baron");




            //List<Obj_AI_Minion> JungleBuffs = EntityManager.MinionsAndMonsters.GetJungleMonsters(null, Int32.MaxValue, true).ToList();
            var 
            jungleBuffs = EntityManager.MinionsAndMonsters.GetJungleMonsters().Where(
                a => a.Distance(Player.Instance) < 2000000 && !a.IsDead && !a.IsInvulnerable).ToList();
        
            if (myHero.Team.ToString().Equals("Order"))
            {
                if (Config.Modes.Misc.RedSteal)
                    MonstersBuff.Add("SRU_Red10.1.1");
                if (Config.Modes.Misc.BlueSteal)
                    MonstersBuff.Add("SRU_Blue7.1.1");
            }
            else
            {
                if (Config.Modes.Misc.BlueSteal)
                    MonstersBuff.Add("SRU_Blue1.1.1");
                if (Config.Modes.Misc.RedSteal)
                    MonstersBuff.Add("SRU_Red4.1.1");
            }

            foreach (Obj_AI_Minion mob in jungleBuffs)
            {
                foreach (string name in MonstersBuff)
                {
                    if (Regex.IsMatch(mob.Name, name + "[0-9.]*$"))
                    {
                        var level = Player.Instance.Spellbook.GetSpell(SpellSlot.R).Level - 1;

                        float damage = new float[] { 350, 500, 650 }[level] + 0.9f * Player.Instance.FlatMagicDamageMod + 1 * Player.Instance.FlatPhysicalDamageMod;
                        double total = Player.Instance.CalculateDamageOnUnit(mob, DamageType.Magical, damage) * 0.7;
                        Chat.Print("totla : " + total + " Get : " + myHero.GetSpellDamage(mob, SpellSlot.R));
                    }
                    var distance = Vector3.Distance(Player.Instance.ServerPosition, mob.Position);
                    var timeQ = (distance / SpellManager.Q.Speed + SpellManager.Q.CastDelay) * 1000;
                    var timeR = (distance / SpellManager.R.Speed + SpellManager.R.CastDelay) * 1000;

                    Chat.Print("Time " + timeR + " Rounded : " + (int)timeR);

                    if (Prediction.Health.GetPrediction(mob, (int)timeQ) <= myHero.GetSpellDamage(mob, SpellSlot.Q)
                        && Prediction.Health.GetPrediction(mob, (int)timeQ) >= 0)
                    {
                        Q.Cast(mob);
                    }
                    else if (Prediction.Health.GetPrediction(mob, (int)timeR) <= myHero.GetSpellDamage(mob, SpellSlot.R)
                      && Prediction.Health.GetPrediction(mob, (int)timeR) >= 0)
                    {
                        if (!Player.Instance.Spellbook.IsChanneling)
                            TacticalMap.SendPing(PingCategory.OnMyWay, mob);
                        R.Cast(mob);
                    }
                }
            }
        }
    }
}

