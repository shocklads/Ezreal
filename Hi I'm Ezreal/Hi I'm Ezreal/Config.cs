using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass

namespace AddonTemplate
{
    public static class Config
    {
        private const string MenuName = "Hi I'm Ezreal";

        private static readonly Menu Menu;

        static Config()
        {
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Welcome to Hi I'm Ezreal addon!");
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
            private static readonly Menu MenuKillsteal;
            private static readonly Menu MenuMisc;
            private static readonly Menu MenuDraw;
            private static readonly Menu MenuClear;

            static Modes()
            {
                MenuCombo = Menu.AddSubMenu("Combo");
                MenuHarass = Menu.AddSubMenu("Harass");
                MenuKillsteal = Menu.AddSubMenu("KillSteal");
                MenuMisc = Menu.AddSubMenu("Global settings");
                MenuDraw = Menu.AddSubMenu("Visual");
                MenuClear = Menu.AddSubMenu("Wave Clear");

                Combo.Initialize();
                Harass.Initialize();
                KillSteal.Initialize();
                Draw.Initialize();
                Misc.Initialize();
                Clear.Initialize();
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
                private static readonly Slider _numberR;

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

            public static int NumberR
            {
                get { return _numberR.CurrentValue; }
            }

            static Combo()
                {
                    MenuCombo.AddGroupLabel("Combo");
                    _useQ = MenuCombo.Add("comboUseQ", new CheckBox("Use Q"));
                    _useW = MenuCombo.Add("comboUseW", new CheckBox("Use W"));
                    _useE = MenuCombo.Add("comboUseE", new CheckBox("Use E", false));
                    _useR = MenuCombo.Add("comboUseR", new CheckBox("Use R"));
                    MenuCombo.AddSeparator();
                    _numberR = MenuCombo.Add("combonumberR", new Slider("Min enemy to use R", 2, 1, 5));
                }

                public static void Initialize()
                {
                }
            }

            public static class Harass
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly Slider _manaQ;
                private static readonly Slider _manaW;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }

                public static int ManaQ
                {
                    get { return _manaQ.CurrentValue; }
                }

                public static int ManaW
                {
                    get { return _manaW.CurrentValue; }
                }

                static Harass()
                {
                    MenuHarass.AddGroupLabel("Harass");
                    _useQ = MenuHarass.Add("harassUseQ", new CheckBox("Use Q"));
                    _useW = MenuHarass.Add("harassUseW", new CheckBox("Use W"));
                    MenuHarass.AddSeparator();
                    MenuHarass.AddGroupLabel("Mana Management");
                    _manaQ = MenuHarass.Add("harassManaQ", new Slider("Minimum mana to use Q ({0}%)", 40));
                    _manaW = MenuHarass.Add("harassManaW", new Slider("Minimum mana to use W ({0}%)", 40));

                }

                public static void Initialize()
                {
                }
            }

            public static class Draw
            {
                private static readonly CheckBox _drawQ;
                private static readonly CheckBox _drawW;
                private static readonly CheckBox _drawE;
                private static readonly CheckBox _onlyRdy;
                public static readonly Slider _skinhax;
                public static string[] skinName = { "Classic Ezreal", "Nottingham Ezreal", "Striker Ezreal", "Frosted Ezreal", "Explorer Ezreal", "Pulsefire Ezreal", "TPA Ezreal", "Debonair Ezreal", "Ace of Spades Ezreal" };

                public static bool DrawQ
                {
                    get { return _drawQ.CurrentValue; }
                }

                public static bool DrawW
                {
                    get { return _drawW.CurrentValue; }
                }

                public static bool DrawE
                {
                    get { return _drawE.CurrentValue; }
                }

                public static bool OnlyRdy
                {
                    get { return _onlyRdy.CurrentValue; }
                }

                public static int SkinHax
                {
                    get { return _skinhax.CurrentValue; }
                }

                static Draw()
                {
                    MenuDraw.AddGroupLabel("Spells range");
                    _drawQ = MenuDraw.Add("drawQ", new CheckBox("Draw Q"));
                    _drawW = MenuDraw.Add("drawW", new CheckBox("Draw W"));
                    _drawE = MenuDraw.Add("drawE", new CheckBox("Draw E"));
                    _onlyRdy = MenuDraw.Add("onlyRdy", new CheckBox("Draw only when spell is not on cooldown"));
                    MenuDraw.AddSeparator();
                    MenuDraw.AddGroupLabel("Skin hack");
                    _skinhax = MenuDraw.Add("skinhax", new Slider("Skin hack", 0, skinName.Length - 1, 0));
                }

                public static void Initialize()
                {
                }
            }
            public static class KillSteal
            {
                private static readonly CheckBox _KsQ;
                private static readonly CheckBox _KsW;
                private static readonly CheckBox _RedSteal;
                private static readonly CheckBox _BlueSteal;
                private static readonly CheckBox _DragonSteal;
                private static readonly CheckBox _BaronSteal;

                public static bool KsQ
                {
                    get { return _KsQ.CurrentValue; }
                }

                public static bool KsW
                {
                    get { return _KsW.CurrentValue; }
                }

                public static bool RedSteal
                {
                    get { return _RedSteal.CurrentValue; }
                }

                public static bool BlueSteal
                {
                    get { return _BlueSteal.CurrentValue; }
                }

                public static bool DragonSteal
                {
                    get { return _DragonSteal.CurrentValue; }
                }

                public static bool BaronSteal
                {
                    get { return _BaronSteal.CurrentValue; }
                }

                static KillSteal()
                {
                    MenuKillsteal.AddGroupLabel("KillSteal");
                    _KsQ = MenuKillsteal.Add("KsQ", new CheckBox("Use Q"));
                    _KsW = MenuKillsteal.Add("KsW", new CheckBox("Use W", false));
                    MenuKillsteal.AddSeparator();
                    MenuKillsteal.AddGroupLabel("Jungle steal");
                    MenuKillsteal.AddLabel("Disable for now !");
                    _RedSteal = MenuKillsteal.Add("RedS", new CheckBox("Use ult to KS enemy Red Buff", false));
                    _BlueSteal = MenuKillsteal.Add("BlueS", new CheckBox("Use ult to KS enemy Blue Buff", false));
                    _DragonSteal = MenuKillsteal.Add("DragonS", new CheckBox("Use ult to KS Dragon", false));
                    _BaronSteal = MenuKillsteal.Add("BaronS", new CheckBox("Use ult to KS Baron", false));
                }

                public static void Initialize()
                {
                }
            }

            public static class Clear
            {
                private static readonly CheckBox _QLastHit;
                private static readonly CheckBox _WOnAlly;
                private static readonly Slider _NumberW;
                private static readonly Slider _manaQ;
                private static readonly Slider _manaW;

                public static bool UseQLastHit
                {
                    get { return _QLastHit.CurrentValue; }
                }

                public static bool UseWOnAlly
                {
                    get { return _WOnAlly.CurrentValue; }
                }

                public static int NumberW
                {
                    get { return _NumberW.CurrentValue; }
                }

                public static int ManaQ
                {
                    get { return _manaQ.CurrentValue; }
                }

                public static int ManaW
                {
                    get { return _manaW.CurrentValue; }
                }

                static Clear()
                {
                    MenuClear.AddGroupLabel("Wave / Jungle clear");
                    _QLastHit = MenuClear.Add("QLastHit", new CheckBox("Use Smart Q on wave clear"));
                    _WOnAlly = MenuClear.Add("WAlly", new CheckBox("Use W on allies"));
                    MenuClear.AddSeparator();
                    _NumberW = MenuClear.Add("NumberW", new Slider("Number of allies to use W", 2, 1, 5));
                    MenuClear.AddSeparator();
                    MenuClear.AddGroupLabel("Mana Management");
                    _manaQ = MenuClear.Add("clearManaQ", new Slider("Minimum mana to use Q ({0}%)", 40));
                    _manaW = MenuClear.Add("clearManaW", new Slider("Minimum mana to use W ({0}%)", 40));

                }

                public static void Initialize()
                {
                }
            }
            public static class Misc
            {
                private static readonly CheckBox _CcQ;
                private static readonly CheckBox _CcW;
                private static readonly CheckBox _UseQOnUnkillable;
                private static readonly CheckBox _UseQUnderTurret;
                private static readonly Slider _PredQ;
                private static readonly Slider _PredW;
                private static readonly Slider _PredR;
                public static KeyBind _SelfW;



                public static bool CcQ
                {
                    get { return _CcQ.CurrentValue; }
                }

                public static bool CcW
                {
                    get { return _CcW.CurrentValue; }
                }

                public static bool UseQOnUnkillable
                {
                    get { return _UseQOnUnkillable.CurrentValue; }
                }

                public static bool SelfW
                {
                    get { return _SelfW.CurrentValue; }
                }

                public static bool UseQUnderTurret
                {
                    get { return _UseQUnderTurret.CurrentValue; }
                }

                public static int PredQ
                {
                    get { return _PredQ.CurrentValue; }
                }

                public static int PredW
                {
                    get { return _PredW.CurrentValue; }
                }

                public static int PredR
                {
                    get { return _PredR.CurrentValue; }
                }


                static Misc()
                {
                    MenuMisc.AddGroupLabel("CCed target");
                    _CcQ = MenuMisc.Add("CCQ", new CheckBox("Use Q on CCed enemy"));
                    _CcW = MenuMisc.Add("CCW", new CheckBox("Use W on CCed enemy"));
                    MenuMisc.AddSeparator();
                    MenuMisc.AddGroupLabel("Use Q to secure minion");
                    _UseQOnUnkillable = MenuMisc.Add("QUnkillable", new CheckBox("Use Q if can't kill minion with AA"));
                    _UseQUnderTurret = MenuMisc.Add("QUnderTurret", new CheckBox("Use Q if can't kill minion with AA under turret"));
                    MenuMisc.AddSeparator();
                    MenuMisc.AddGroupLabel("Use W and E to get attack speed");
                    _SelfW = MenuMisc.Add("SelfW", new KeyBind("Self W", false, KeyBind.BindTypes.HoldActive, 'J'));
                    MenuMisc.AddSeparator();
                    MenuMisc.AddGroupLabel("Hit Chance");
                    MenuMisc.AddLabel("HitChance : 1 = Low, 2 = Medium, 3 = High");
                    _PredQ = MenuMisc.Add("PredQ", new Slider("Q HitChance", 2, 1, 3));
                    _PredW = MenuMisc.Add("PredW", new Slider("W HitChance", 2, 1, 3));
                    _PredR = MenuMisc.Add("PredR", new Slider("R HitChance", 2, 1, 3));
            
                }

                public static void Initialize()
                {
                }
            }
        }
    }
}
