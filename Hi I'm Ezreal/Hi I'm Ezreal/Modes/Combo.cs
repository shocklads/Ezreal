using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

using Settings = AddonTemplate.Config.Modes.Combo;

namespace AddonTemplate.Modes
{
    public sealed class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            if (Settings.UseQ && Q.IsReady())
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
                if (target != null && Q.GetPrediction(target).HitChance >= SpellManager.PredQ())
                {
                    Q.Cast(target);
                }
            }
            if (Settings.UseW && W.IsReady())
            {
                var target = TargetSelector.GetTarget(W.Range, DamageType.Physical);
                if (target != null && W.GetPrediction(target).HitChance >= SpellManager.PredW())
                {
                    W.Cast(target);
                }
            }
            if (Settings.UseE && E.IsReady())
            {
                var target = TargetSelector.GetTarget(E.Range, DamageType.Physical);
                var test = Game.CursorPos;
                if (target != null)
                {
                    E.Cast(test);
                }
            }
            if (Settings.UseR && R.IsReady())
            {
                var heroes = EntityManager.Heroes.Enemies;
                foreach (var hero in heroes.Where(hero => !hero.IsDead && hero.IsVisible && hero.IsInRange(Player.Instance, 10000)))
                {
                    var collision = new List<AIHeroClient>();
                    var startPos = Player.Instance.Position.To2D();
                    var endPos = startPos.Extend(hero.Position.To2D(), 1500);
                    collision.Clear();
                    foreach (var colliHero in heroes.Where(colliHero => !colliHero.IsDead && colliHero.IsVisible && colliHero.IsInRange(hero, 3000)))
                    {
                        if (Prediction.Position.Collision.LinearMissileCollision(colliHero, startPos, endPos,
                            SpellManager.R.Speed, SpellManager.R.Width, SpellManager.R.CastDelay))
                        {
                            collision.Add(colliHero);
                        }
                        if (collision.Count >= Settings.NumberR)
                        {
                            R.Cast(hero);
                        }
                    }
                }
            }
        }
    }
}
