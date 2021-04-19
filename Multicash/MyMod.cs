using MelonLoader;
using Harmony;
using Assets.Scripts.Simulation;
using Assets.Scripts.Unity.UI_New.Popups;
using Assets.Scripts.Unity.UI_New.Settings;
using TMPro;
using System;
using Assets.Scripts.Unity.UI_New.InGame;
using UnityEngine;
using UnityEngine.UI;
using System.Drawing;
using System.IO;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.UI_New.InGame.TowerSelectionMenu;






namespace Multicash
{
    public class MyMod : MelonMod
    {
        static float multi = 2;
        static bool Hotkeyd;
        public static bool definitelyTryPlaceNext;

        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            MelonLogger.Log("multicash loaded");
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

            if (Hotkeyd)
            {
                if (PopupScreen.instance.GetFirstActivePopup() != null)
                {
                    PopupScreen.instance.GetFirstActivePopup().GetComponentInChildren<TMP_InputField>().characterValidation = TMP_InputField.CharacterValidation.None;
                    Hotkeyd = false;
                }
                
            }
            

            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKeyDown(KeyCode.F10))
                {
                    Il2CppSystem.Action<string> mod = (Il2CppSystem.Action<string>)delegate (string s)
                    {
                        multi = float.Parse(s);

                    };
                    PopupScreen.instance.ShowSetNamePopup("Cash", "Multiply Cash by", mod, "2");
                    
                    Hotkeyd = true;
                }

            }






        
        
    }


    }
}
