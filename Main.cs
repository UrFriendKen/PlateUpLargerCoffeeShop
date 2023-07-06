using CustomSettingsAndLayouts;
using HarmonyLib;
using KitchenData;
using KitchenMods;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace KitchenLargerCoffeeShop
{
    public class Main : IModInitializer
    {
        public const string MOD_GUID = $"IcedMilo.PlateUp.{MOD_NAME}";
        public const string MOD_NAME = "Larger Coffee Shop";
        public const string MOD_VERSION = "0.1.0";

        Harmony _harmony;

        public Main()
        {
            _harmony = new Harmony(MOD_GUID);
            _harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public void PostActivate(KitchenMods.Mod mod)
        {
            LogWarning($"{MOD_GUID} v{MOD_VERSION} in use!");
        }

        internal static Dictionary<int, int> CustomSettingLayouts => new Dictionary<int, int>()
        {
            { 1970109064, -1017771555 }
        };

        public void PreInject()
        {
            foreach (KeyValuePair<int, int> settingLayout in CustomSettingLayouts)
            {
                if (GameData.Main.TryGet(settingLayout.Value, out LayoutProfile layout, warn_if_fail: true))
                {
                    if (GameData.Main.TryGet(settingLayout.Key, out RestaurantSetting setting, warn_if_fail: true))
                    {
                        Registry.GrantCustomSetting(setting);
                        Registry.AddSettingLayout(setting, layout, noDuplicates: true);
                    }
                }
            }
        }

        public void PostInject()
        {
        }

        #region Logging
        public static void LogInfo(string _log) { Debug.Log($"[{MOD_NAME}] " + _log); }
        public static void LogWarning(string _log) { Debug.LogWarning($"[{MOD_NAME}] " + _log); }
        public static void LogError(string _log) { Debug.LogError($"[{MOD_NAME}] " + _log); }
        public static void LogInfo(object _log) { LogInfo(_log.ToString()); }
        public static void LogWarning(object _log) { LogWarning(_log.ToString()); }
        public static void LogError(object _log) { LogError(_log.ToString()); }
        #endregion
    }
}
