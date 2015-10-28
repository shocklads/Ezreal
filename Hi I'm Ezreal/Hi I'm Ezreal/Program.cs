using System;
using EloBuddy;
using EloBuddy.SDK.Events;

namespace AddonTemplate
{
    public static class Program
    {
        public const string ChampName = "Ezreal";

        public static void Main(string[] args)
        {
            Loading.OnLoadingComplete += OnLoadingComplete;
        }

        private static void OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != ChampName)
            {
                Chat.Print(Player.Instance.ChampionName);
                return;
            }
            Config.Initialize();
            SpellManager.Initialize();
            ModeManager.Initialize();

            Drawing.OnDraw += GameEvent.OnDraw;
            Config.Modes.Draw._skinhax.DisplayName = Config.Modes.Draw.skinName[Config.Modes.Draw._skinhax.CurrentValue];
            Player.Instance.SetSkin(Player.Instance.ChampionName, Config.Modes.Draw._skinhax.CurrentValue);
            Config.Modes.Misc._SelfW.OnValueChange += GameEvent.SelfW_OnValueChanged;
            Config.Modes.Draw._skinhax.OnValueChange += GameEvent.SkinHax_OnValueChanged;
            Gapcloser.OnGapcloser += GameEvent.Gapcloser_OnGapCloser;
            Obj_AI_Base.OnBasicAttack += GameEvent.ObjTurret_OnTurretDamage;
        }
    }
}
