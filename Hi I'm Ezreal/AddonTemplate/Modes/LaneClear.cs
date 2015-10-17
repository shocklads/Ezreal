using System;
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
            // Only execute this mode when the orbwalker is on laneclear mode
            return (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear)
                || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear));
        }

        public override void Execute()
        {
            if (Config.Modes.Clear.UseQLastHit && Q.IsReady())
            {
                foreach (
                    var minions in
                        EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy,
                            Player.Instance.ServerPosition, SpellManager.Q.Range))
                {
                    if (Player.Instance.GetSpellDamage(minions, SpellSlot.Q) >= minions.Health)
                        Q.Cast(minions);
                }
            }


            if (Config.Modes.Clear.UseWOnAlly && W.IsReady())
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
