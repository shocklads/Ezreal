using System;
using System.Collections.Generic;
using System.Linq;
using AddonTemplate;
using AddonTemplate.Modes;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Constants;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

using Settings = AddonTemplate.Config.Modes.Combo;

namespace AddonTemplate.Modes
{


    public sealed class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        private void ItemUsage()
        {
            var target = TargetSelector.GetTarget(550, DamageType.Physical); // 550 = Botrk.Range
            if (Settings.UseMuramana && Config.Muramana.IsOwned() && Config.Muramana.IsReady() && !Player.HasBuff("Muramana"))
            {
                Config.Muramana.Cast();
            }
            if (Settings.UseYoumuu && Config.Youmuu.IsOwned() && Config.Youmuu.IsReady())
            {
                Config.Youmuu.Cast();
            }
            if (Settings.useBotrk && Item.HasItem(Config.Cutlass.Id) && Item.CanUseItem(Config.Cutlass.Id) && Player.Instance.HealthPercent < Settings.MinHPBotrk && target.HealthPercent < Settings.EnemyMinHPBotrk)
            {
             Item.UseItem(Config.Cutlass.Id, target);
            }
            if (Settings.useBotrk && Item.HasItem(Config.Botrk.Id) && Item.CanUseItem(Config.Botrk.Id) && Player.Instance.HealthPercent < Settings.MinHPBotrk && target.HealthPercent < Settings.EnemyMinHPBotrk)
            {
                Config.Botrk.Cast(target);
            }
        }

        public override void Execute()
        {
            Config.LastComboPressed = Game.Time;
            ItemUsage();
            if (Settings.UseQ && Q.IsReady())
            {
                var target = TargetSelector.GetTarget(Q.Range - 50, DamageType.Physical);
                if (target != null && Q.GetPrediction(target).HitChance >= SpellManager.PredQ())
                {
                    Q.Cast(target);
                }
            }
            if (Settings.UseW && W.IsReady())
            {
                var target = TargetSelector.GetTarget(W.Range - 50, DamageType.Physical);
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
                foreach (var hero in heroes.Where(hero => !hero.IsDead && hero.IsVisible && hero.IsInRange(Player.Instance, 3000)))
                {
                    if (hero.IsKsable(SpellSlot.R) && (!Q.IsReady() || !hero.IsKsable(SpellSlot.Q)) && (!W.IsReady() || !hero.IsKsable(SpellSlot.W)))
                    {
                        R.Cast(hero);
                    }
                 /*   var collision = new List<AIHeroClient>();
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
                */}
            }
        }
    }
}
