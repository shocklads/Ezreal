using EloBuddy;
using EloBuddy.SDK;

// Using the config like this makes your life easier, trust me
using Settings = AddonTemplate.Config.Modes.Harass;

namespace AddonTemplate.Modes
{
    public sealed class Harass : ModeBase
    {
        public override bool ShouldBeExecuted()
        { 
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
        }

        public override void Execute()
        {
            if (Settings.UseQ && Player.Instance.ManaPercent > Settings.ManaQ && Q.IsReady())
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
                if (target != null && Q.GetPrediction(target).HitChance >= SpellManager.PredQ())
                {
                    Q.Cast(target);
                }
            }
            if (Settings.UseW && Player.Instance.ManaPercent > Settings.ManaW && W.IsReady())
            {
                var target = TargetSelector.GetTarget(W.Range, DamageType.Physical);
                if (target != null && W.GetPrediction(target).HitChance >= SpellManager.PredW())
                {
                    W.Cast(target);
                }
            }
        }
    }
}
