using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using UnityEngine;

namespace Power
{
    [BepInPlugin("DJ.UnlimitedPower", "DJs Unlimited Power", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        internal static GameObject Hook;
        internal static UnlimitedPower Script;
        internal static ManualLogSource logger;
        internal static ConfigEntry<bool> EnablePowerChanges;

        void Awake()
        {
            logger = Logger;
            Logger.LogInfo("Loading Unlimited...Powaaaaaa");
            Hook = new GameObject("Power Object");
            Script = Hook.AddComponent<UnlimitedPower>();
            DontDestroyOnLoad(Hook);

            EnablePowerChanges = Config.Bind(
                "Power",
                "Enable Dynamic Power Changes",
                true,
                new ConfigDescription("If enabled, allows power switches to be dynamically turned on at random points throughout your raids.",
                null,
                new ConfigurationManagerAttributes { IsAdvanced = false, ShowRangeAsPercent = false, Order = 1 }));
        }
    }
}