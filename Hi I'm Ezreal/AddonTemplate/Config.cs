using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace AddonTemplate
{
    public static class Config
    {
        private const string MenuName = "GinjiEzreal";

        private static readonly Menu Menu;

        static Config()
        {
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Welcome to my first addon!");
            Menu.AddLabel("tg ducon");

            // Initialize the modes
            Modes.Initialize();
        }

        public static void Initialize()
        {
        }

        public static class Modes
        {
            private static readonly Menu Menu_combo;
            private static readonly Menu Menu_harass;
            private static readonly Menu Menu_killsteal;
            private static readonly Menu Menu_misc;
            private static readonly Menu Menu_draw;
            private static readonly Menu Menu_clear;

            static Modes()
            {
                Menu_combo = Menu.AddSubMenu("Combo");
                Menu_harass = Menu.AddSubMenu("Harass");
                Menu_killsteal = Menu.AddSubMenu("KillSteal");
                Menu_misc = Menu.AddSubMenu("Misc");
                Menu_draw = Menu.AddSubMenu("Draw");
                Menu_clear = Menu.AddSubMenu("Clear");

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
                    Menu_combo.AddGroupLabel("Combo");
                    _useQ = Menu_combo.Add("comboUseQ", new CheckBox("Use Q"));
                    _useW = Menu_combo.Add("comboUseW", new CheckBox("Use W"));
                    _useE = Menu_combo.Add("comboUseE", new CheckBox("Use E"));
                    _useR = Menu_combo.Add("comboUseR", new CheckBox("Use R", false)); // Default false
                    _numberR = Menu_combo.Add("combonumberR", new Slider("Min enemy to use R", 2, 1, 6));
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
                    Menu_harass.AddGroupLabel("Harass");
                    _useQ = Menu_harass.Add("harassUseQ", new CheckBox("Use Q"));
                    _useW = Menu_harass.Add("harassUseW", new CheckBox("Use W"));
                    _manaQ = Menu_harass.Add("harassManaQ", new Slider("Maximum mana to use Q ({0}%)", 40));
                    _manaW = Menu_harass.Add("harassManaW", new Slider("Maximum mana to use W ({0}%)", 40));

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
                static Draw()
                {
                    Menu_draw.AddGroupLabel("Draw");
                    _drawQ = Menu_draw.Add("drawQ", new CheckBox("Draw Q"));
                    _drawW = Menu_draw.Add("drawW", new CheckBox("Draw W"));
                    _drawE = Menu_draw.Add("drawE", new CheckBox("Draw E"));
                    _onlyRdy = Menu_draw.Add("onlyRdy", new CheckBox("Draw only when spell is not on cooldown"));

                }

                public static void Initialize()
                {
                }
            }
            public static class KillSteal
            {
                private static readonly CheckBox _KsQ;
                private static readonly CheckBox _KsW;
                private static readonly CheckBox _KsR;

                public static bool KsQ
                {
                    get { return _KsQ.CurrentValue; }
                }
                public static bool KsW
                {
                    get { return _KsW.CurrentValue; }
                }
                public static bool KsR
                {
                    get { return _KsR.CurrentValue; }
                }
                static KillSteal()
                {
                    Menu_killsteal.AddGroupLabel("KillSteal");
                    _KsQ = Menu_killsteal.Add("KsQ", new CheckBox("Use Q"));
                    _KsW = Menu_killsteal.Add("KsW", new CheckBox("Use W"));
                    _KsR = Menu_killsteal.Add("KsR", new CheckBox("Use R"));
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

                static Clear()
                {
                    Menu_clear.AddGroupLabel("Clear");
                    _QLastHit = Menu_clear.Add("QLastHit", new CheckBox("Use Q on wave clear"));
                    _WOnAlly = Menu_clear.Add("WAlly", new CheckBox("Use W on allies"));
                    _NumberW = Menu_clear.Add("NumberW", new Slider("Number of allies to use W", 1, 1, 5));
                }

                public static void Initialize()
                {
                }
            }
            public static class Misc
            {
                private static readonly CheckBox _CcQ;
                private static readonly CheckBox _CcW;
                private static readonly CheckBox _RedSteal;
                private static readonly CheckBox _BlueSteal;
                private static readonly CheckBox _DragonSteal;
                private static readonly CheckBox _BaronSteal;
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
                    Menu_misc.AddGroupLabel("Misc");
                    _CcQ = Menu_misc.Add("CCQ", new CheckBox("Use Q on CCed enemy"));
                    _CcW = Menu_misc.Add("CCW", new CheckBox("Use W on CCed enemy"));
                    _RedSteal = Menu_misc.Add("RedS", new CheckBox("Use ult to KS enemy Red Buff"));
                    _BlueSteal = Menu_misc.Add("BlueS", new CheckBox("Use ult to KS enemy Blue Buff"));
                    _DragonSteal = Menu_misc.Add("DragonS", new CheckBox("Use ult to KS Dragon"));
                    _BaronSteal = Menu_misc.Add("BaronS", new CheckBox("Use ult to KS Baron"));
                    _UseQOnUnkillable = Menu_misc.Add("QUnkillable", new CheckBox("Use Q if can't kill minion with AA"));
                    _UseQUnderTurret = Menu_misc.Add("QUnderTurret", new CheckBox("Use Q if can't kill minion with AA under turret"));
                    _SelfW = Menu_misc.Add("SelfW", new KeyBind("SelfW", false, KeyBind.BindTypes.HoldActive, 'J'));
                    Menu_misc.AddLabel("HitChance : 1 = Low, 2 = Medium, 3 = High");
                    _PredQ = Menu_misc.Add("PredQ", new Slider("Q HitChance", 2, 1, 4));
                    _PredW = Menu_misc.Add("PredW", new Slider("W HitChance", 2, 1, 3));
                    _PredR = Menu_misc.Add("PredR", new Slider("R HitChance", 2, 1, 3));
            
                }

                public static void Initialize()
                {
                }
            }
        }
    }
}
