using EloBuddy.SDK;

namespace AddonTemplate.Modes
{
    public abstract class ModeBase
    {
        protected static Spell.Skillshot Q => SpellManager.Q;

        protected static Spell.Skillshot W => SpellManager.W;

        protected static Spell.Skillshot E => SpellManager.E;

        protected static Spell.Skillshot R => SpellManager.R;

        public abstract bool ShouldBeExecuted();

        public abstract void Execute();
    }
}
