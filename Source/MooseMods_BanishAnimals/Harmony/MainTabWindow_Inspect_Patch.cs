using HarmonyLib;
using RimWorld;
using UnityEngine;

namespace BanishAnimals.Harmony
{
    [HarmonyPatch(typeof(MainTabWindow_Inspect), "DoInspectPaneButtons")]
    internal static class MainTabWindow_Inspect_Patch_DoInspectPaneButtons
    {
        [HarmonyPostfix]
        public static void Postfix( Rect rect, ref float lineEndWidth)
        {
            BanishButton.DrawBanishButton(rect, ref lineEndWidth);
        }
    }
}
