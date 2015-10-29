using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using Settings = AddonTemplate.Config.Modes.KillSteal;

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
            if (!Player.Instance.IsRecalling())
            {
                KsChamp();
                AutoCCed();
              //  KsBuff();
                QIfUnkillable();
            }
        }

        private static void QIfUnkillable()
        {
            foreach (var minions in EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.ServerPosition, SpellManager.Q.Range))
            {
                if (Prediction.Health.GetPrediction(minions, (int)(Player.Instance.AttackDelay * 1000)) <= 0 && !Orbwalker.CanAutoAttack)
                {
                    if (Config.Modes.Misc.UseQOnUnkillable &&
                        Player.Instance.GetSpellDamage(minions, SpellSlot.Q) >= minions.Health && (Orbwalker.LastTarget == null || Orbwalker.LastTarget.NetworkId != minions.NetworkId))
                        Q.Cast(minions);
                }
            }
        }

       // private static AIHeroClient MyHero => ObjectManager.Player;

        public static void KsChamp()
        {

            foreach (var enemy in EntityManager.Heroes.Enemies.Where(enemy => enemy.IsValidTarget(Q.Range)))
            {
                if (Settings.KsQ && Q.IsReady() && enemy.IsKsable(SpellSlot.Q))
                {
                    Q.Cast(enemy);
                }
            }
            foreach (var enemy in EntityManager.Heroes.Enemies.Where(enemy => enemy.IsValidTarget(W.Range)))
            {
                if (Settings.KsW && W.IsReady() && enemy.IsKsable(SpellSlot.W))
                {
                    W.Cast(enemy);
                }
            }
        }

        public static void AutoCCed()
        {
            string[] hardCc =
            {
                "Charm", "Fear", "Flee", "Knockup", "Polymorph", "Sleep", "Slow", "Snare", "Stun", "Suppression", "Taunt"
            };
            foreach (var enemy in EntityManager.Heroes.Enemies)
            {
                foreach (var debuff in hardCc.Where(debuff => enemy.HasBuffOfType((BuffType)Enum.Parse(typeof(BuffType), debuff))))
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
        /*
                public static void KsBuff()
                {
                    var monstersBuff = new List<String>();
                    if (Config.Modes.KillSteal.DragonSteal)
                        monstersBuff.Add("SRU_Dragon");
                    if (Config.Modes.KillSteal.BaronSteal)
                        monstersBuff.Add("SRU_Baron");

                    List<Obj_AI_Minion> jungleBuffs = EntityManager.MinionsAndMonsters.GetJungleMonsters(null, Int32.MaxValue, true).ToList();
                    if (MyHero.Team.ToString().Equals("Order"))
                    {
                        if (Config.Modes.KillSteal.RedSteal)
                            monstersBuff.Add("SRU_Red10.1.1");
                        if (Config.Modes.KillSteal.BlueSteal)
                            monstersBuff.Add("SRU_Blue7.1.1");
                    }
                    else
                    {
                        if (Config.Modes.KillSteal.BlueSteal)
                            monstersBuff.Add("SRU_Blue1.1.1");
                        if (Config.Modes.KillSteal.RedSteal)
                            monstersBuff.Add("SRU_Red4.1.1");
                    }

                    foreach (Obj_AI_Minion mob in jungleBuffs)
                    {
                        foreach (string name in monstersBuff)
                        {
                            if (Regex.IsMatch(mob.Name, name + "[0-9.]*$"))
                            {
                                var timeQ = (distance / SpellManager.Q.Speed + SpellManager.Q.CastDelay) * 1000;
                                var timeR = 1000 * (int)(Extensions.Distance(Player.Instance, mob) / SpellManager.R.Speed) + SpellManager.R.CastDelay;
                                var predHealth = Prediction.Health.GetPrediction(mob, timeR);

                                Chat.Print("TimeR : " + timeR + " Pred : " + predHealth);

                               if (Prediction.Health.GetPrediction(mob, (int)timeQ) <= MyHero.GetSpellDamage(mob, SpellSlot.Q)
                                    && Prediction.Health.GetPrediction(mob, (int)timeQ) >= 0)
                                {
                                    Q.Cast(mob);
                                }
                                if (predHealth <= MyHero.GetSpellDamage(mob, SpellSlot.R) - ((myHero.GetSpellDamage(mob, SpellSlot.R) / 100) * 30)) && predHealth >= 0)
                                {
                                    R.Cast(mob);
                                }
                            }
                        }
                    }
                }
                */
    }

}

