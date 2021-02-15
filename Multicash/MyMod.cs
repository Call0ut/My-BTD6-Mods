using MelonLoader;
using Harmony;
using NKHook6;
using Assets.Scripts.Simulation;
using NKHook6.Api.Events;
using Assets.Scripts.Unity.UI_New.Popups;
using TMPro;

namespace Multicash
{
    public class MyMod : MelonMod
    {
        static float multi = 2;

        static string holdKey = "";

        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            EventRegistry.instance.listen(typeof(MyMod));
            Logger.Log("Multi-Cash loaded");
        }
        [HarmonyPatch(typeof(Simulation), "AddCash")]
        public class Mcash
        {
            [HarmonyPrefix]
            public static bool Prefix(ref double c, ref Simulation.CashSource source)
            {
                if (source != Simulation.CashSource.CoopTransferedCash && source != Simulation.CashSource.TowerSold) c *= multi;
                return true;

            }
        }
        public override void OnUpdate()
        {
            base.OnUpdate();

            if (testForActivity)
            {
                if (PopupScreen.instance.GetFirstActivePopup() != null)
                {
                    PopupScreen.instance.GetFirstActivePopup().GetComponentInChildren<TMP_InputField>().characterValidation = TMP_InputField.CharacterValidation.None;
                    testForActivity = false;
                }

            }
        }
        static bool testForActivity;

        [EventAttribute("KeyHeldEvent")]
        public static void onHoldEvent(KeyEvent e)
        {

            string key = e.key + "";
            if (key == "LeftShift")
            {
                holdKey = "LeftShift";
            }

        }
        
        [EventAttribute("KeyReleaseEvent")]
        public static void onReleaseEvent(KeyEvent e)
        {

            string key = e.key + "";
            if (key == "LeftShift")
            {
                holdKey = "";
            }

        }

        [EventAttribute("KeyPressEvent")]
        public static void onKeyEvent(KeyEvent e)
        {

            string key = e.key + "";

            if (key == "F10" && holdKey == "LeftShift")
            {
                Il2CppSystem.Action<string> mod = (Il2CppSystem.Action<string>)delegate (string s)
                {
                    multi = float.Parse(s);

                };
                PopupScreen.instance.ShowSetNamePopup("Multicash", "Multiply Cash by", mod, "2");
                testForActivity = true;
            }




        }

    }
}
