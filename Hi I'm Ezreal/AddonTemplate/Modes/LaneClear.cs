using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

namespace AddonTemplate.Modes
{
    public sealed class LaneClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear)
                || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear));
        }

        public override void Execute()
        {
            if (Config.Modes.Clear.UseQLastHit && Q.IsReady() && Player.Instance.ManaPercent > Config.Modes.Clear.ManaQ)
            {
                bool lastQ = false;
                var minions = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy,
                    Player.Instance.ServerPosition, SpellManager.Q.Range).OrderBy(h => h.Health);
                {
                    foreach (var minion in minions)
                    {
                        if (Player.Instance.GetSpellDamage(minion, SpellSlot.Q) >= minion.Health && Player.Instance.GetAutoAttackDamage(minion, true) < minion.Health)
                        {
                            Q.Cast(minion);
                            lastQ = true;
                        }
                    }
                    if (minions.Any() && !lastQ)
                    {
                        var test = minions.GetEnumerator();
                        while (test.MoveNext())
                        {
                            Q.Cast(minions.Last());
                        }
                    }
                }
            }


            if (Config.Modes.Clear.UseWOnAlly && W.IsReady() && Player.Instance.ManaPercent > Config.Modes.Clear.ManaW)
            {
                var heroes = EntityManager.Heroes.Allies;
                var collision = new List<AIHeroClient>();

                var startPos = Player.Instance.Position.To2D();

                foreach (var hero in heroes.Where(hero => !hero.IsDead))
                {
                    if (hero.Position.Distance(Player.Instance.Position.To2D()) <= SpellManager.W.Range)
                    {
                        var endPos = startPos.Extend(hero.Position.To2D(), SpellManager.W.Range);
                        if (Prediction.Position.Collision.LinearMissileCollision(hero, startPos, endPos,
                            SpellManager.W.Speed, SpellManager.W.Width, SpellManager.W.CastDelay))
                        {
                            collision.Add(hero);
                        }
                        if (collision.Count >= Config.Modes.Clear.NumberW)
                            W.Cast(hero);
                    }
                }
            }
        }
    }
}
