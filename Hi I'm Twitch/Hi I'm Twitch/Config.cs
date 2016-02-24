using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass
namespace AddonTemplate
{
    public static class Config
    {
        private const string MenuName = "AddonTemplate";

        private static readonly Menu Menu;

        static Config()
        {
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Welcome to Hi I'm Twitch addon!");
            Menu.AddLabel("Made by GinjiBan");

            Modes.Initialize();
        }

        public static void Initialize()
        {
        }

        public static class Modes
        {
            private static readonly Menu MenuCombo;
            private static readonly Menu MenuHarass;
            private static readonly Menu MenuDraw;

            static Modes()
            {
                MenuCombo = Config.Menu.AddSubMenu("Combo");
                MenuHarass = Config.Menu.AddSubMenu("Harass");
                MenuDraw = Config.Menu.AddSubMenu("Visual");

                Combo.Initialize();
                Harass.Initialize();
                Draw.Initialize();
            }

            public static void Initialize()
            {
            }

            public static class Combo
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly CheckBox _useR;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }
                public static bool UseR
                {
                    get { return _useR.CurrentValue; }
                }

                static Combo()
                {
                    MenuCombo.AddGroupLabel("Combo");
                    _useQ = MenuCombo.Add("comboUseQ", new CheckBox("Use Q"));
                    _useW = MenuCombo.Add("comboUseW", new CheckBox("Use W"));
                    _useE = MenuCombo.Add("comboUseE", new CheckBox("Use E"));
                    _useR = MenuCombo.Add("comboUseR", new CheckBox("Use R", false));
                }

                public static void Initialize()
                {
                }
            }

            public static class Harass
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly CheckBox _useR;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }
                public static bool UseR
                {
                    get { return _useR.CurrentValue; }
                }

                static Harass()
                {
                    MenuHarass.AddGroupLabel("Harass");
                    _useQ = MenuHarass.Add("harassUseQ", new CheckBox("Use Q"));
                    _useW = MenuHarass.Add("harassUseW", new CheckBox("Use W"));
                    _useE = MenuHarass.Add("harassUseE", new CheckBox("Use E"));
                    _useR = MenuHarass.Add("harassUseR", new CheckBox("Use R", false)); // Default false
                }

                public static void Initialize()
                {
                }
            }
            public static class Draw
            {
                private static readonly CheckBox _dmgIndicator;

                public static bool DamageIndicator
                {
                    get { return _dmgIndicator.CurrentValue; }
                }

                static Draw()
                {
                    MenuDraw.AddGroupLabel("Visual");
                    _dmgIndicator = MenuDraw.Add("damageIndicator", new CheckBox("Damage Indicator"));
                }

                public static void Initialize()
                {
                }
            }
        }
    }
}
