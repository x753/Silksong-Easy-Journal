using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace EasyJournal
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class EasyJournalMod : BaseUnityPlugin
    {
        private const string modGUID = "x753.Easy_Journal";
        private const string modName = "Easy_Journal";
        private const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);
        internal static ManualLogSource ModLogger;

        public static EasyJournalMod Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            harmony.PatchAll();
            ModLogger = BepInEx.Logging.Logger.CreateLogSource(modName);
            ModLogger.LogInfo($"Plugin {modName} is loaded!");

            InitializeConfigs();
        }

        public void InitializeConfigs()
        {
        }

        [HarmonyPatch(typeof(EnemyJournalRecord), "get_KillsRequired")]
        class EnemyJournalRecord_get_KillsRequired_Patch
        {
            [HarmonyPrefix]
            public static bool EnemyJournalRecord_get_KillsRequired_Prefix(ref int __result)
            {
                if (__result > 1) { __result = 1; }
                return false;
            }
        }
    }
}