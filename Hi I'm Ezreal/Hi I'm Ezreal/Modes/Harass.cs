using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
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
                var target = TargetSelector.GetTarget(Q.Range - 50, DamageType.Physical);
                var predQ = Q.GetPrediction(target);
                if (target != null && predQ.HitChance >= SpellManager.PredQ())
                {
                    Q.Cast(predQ.CastPosition);
                }
            }
            if (Settings.UseW && Player.Instance.ManaPercent > Settings.ManaW && W.IsReady())
            {
                var target = TargetSelector.GetTarget(W.Range - 50, DamageType.Physical);
                var predW = W.GetPrediction(target);
                if (target != null && predW.HitChance >= SpellManager.PredW())
                {
                    W.Cast(predW.CastPosition);
                }
            }
        }
    }
}
