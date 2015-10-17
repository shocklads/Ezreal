using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Xml.Schema;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

// Using the config like this makes your life easier, trust me
using Settings = AddonTemplate.Config.Modes.Combo;

namespace AddonTemplate.Modes
{
    public sealed class Combo : ModeBase
    {
        public int TEST = 0;

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
                var target = TargetSelector.GetTarget(R.Range, DamageType.Physical);
                if (target != null && R.GetPrediction(target).HitChance >= SpellManager.PredR())
                {
                    /* foreach (var enemy in EntityManager.Heroes.Enemies)
                     {
                         var countEnemies = 1;

                         var result = Prediction.Position.PredictLinearMissile(enemy,
                             Int32.MaxValue, 250, SpellManager.R.CastDelay, SpellManager.R.Speed, Int32.MaxValue,
                             Player.Instance.ServerPosition);

                         var colli = result.CollisionObjects;

                         for (int j = 0; j < colli.Length; j++)
                         {
                             if (!colli[j].IsMinion)
                             {
                                 countEnemies++;
                             }
                         }
                         if (countEnemies >= Settings.NumberR)
                         {
                             R.Cast(enemy);
                         }
                     }
                 }*/
                    var heroes = EntityManager.Heroes.Enemies;
                    var collision = new List<AIHeroClient>();

                    var startPos = Player.Instance.Position.To2D();

                    foreach (var hero in heroes.Where(hero => !hero.IsDead))
                    {
                        collision.Clear();
                        var endPos = startPos.Extend(hero.Position.To2D(), Int32.MaxValue);
                        if (Prediction.Position.Collision.LinearMissileCollision(hero, startPos, endPos,
                            SpellManager.R.Speed, SpellManager.R.Width, SpellManager.R.CastDelay))
                        {
                            collision.Add(hero);
                        }
                        if (collision.Count >= Settings.NumberR)
                        {
                            Chat.Print(collision.Count + "ppl hit");
                            foreach (var herro in collision)
                            {
                               
                                Chat.Print(herro.Name + " hit");
                            }
                            R.Cast(hero.Position);
                            Chat.Print("Ro");
                        }
                    }
                }
            }
        }
    }
}
