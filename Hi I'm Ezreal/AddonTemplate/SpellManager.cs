using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace AddonTemplate
{
    public static class SpellManager
    {
        public static Spell.Skillshot Q { get; private set; }
        public static Spell.Skillshot W { get; private set; }
        public static Spell.Skillshot E { get; private set; }
        public static Spell.Skillshot R { get; private set; }

        static SpellManager()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1200, SkillShotType.Linear, 250, 2000, 60);
            W = new Spell.Skillshot(SpellSlot.W, 1050, SkillShotType.Linear, 250, 1600, 80);
            E = new Spell.Skillshot(SpellSlot.E, 475, SkillShotType.Linear, 250, 2000, 80);
            R = new Spell.Skillshot(SpellSlot.R, 2000000, SkillShotType.Linear, 250, 2000, 160);
        }

        public static void Initialize()
        {
            // Let the static initializer do the job, this way we avoid multiple init calls aswell
        }

        public static HitChance PredQ()
        {
            var mode = Config.Modes.Misc.PredQ;
            switch (mode)
            {
                case 1:
                    return HitChance.Low;
                case 2:
                    return HitChance.Medium;
                case 3:
                    return HitChance.High;
                case 4:
                    return HitChance.Collision;
            }
            return HitChance.Medium;
        }
        public static HitChance PredW()
        {
            var mode = Config.Modes.Misc.PredW;
            switch (mode)
            {
                case 1:
                    return HitChance.Low;
                case 2:
                    return HitChance.Medium;
                case 3:
                    return HitChance.High;
                case 4:
                    return HitChance.Collision;
            }
            return HitChance.Medium;
        }
        public static HitChance PredR()
        {
            var mode = Config.Modes.Misc.PredR;
            switch (mode)
            {
                case 1:
                    return HitChance.Low;
                case 2:
                    return HitChance.Medium;
                case 3:
                    return HitChance.High;
                case 4:
                    return HitChance.Collision;
            }
            return HitChance.Medium;
        }
    }
}
