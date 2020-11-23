using HarmonyLib;
using System.Reflection;
using Verse;
using System.Linq;

namespace BanishAnimals.Harmony
{
    internal static class RimHUD_Compatibility
    {
        public static bool LoadRimHUDCompatibility(HarmonyLib.Harmony instance, ModContentPack mod)
        {
            Assembly rimhudAssembly = mod.assemblies.loadedAssemblies.FirstOrDefault((Assembly assembly) => assembly.GetName().Name == "RimHUD");

            if (rimhudAssembly == null)
            {
                Log.Message("banish.animals : RimHUD installed, but not loaded or unexpected AssemblyName", false);
            }
            else
            {
                System.Type type = rimhudAssembly.GetType("RimHUD.Interface.InspectPanePlus");

                if (type != null)
                {
                    MethodInfo method = type.GetMethod("DrawButtons", BindingFlags.Static | BindingFlags.NonPublic);

                    if (method != null)
                    {
                        instance.Patch(method, null, new HarmonyMethod(typeof(BanishButton).GetMethod("DrawBanishButton", BindingFlags.Static | BindingFlags.Public)));
                        Log.Message("banish.animals : RimHUD installed, PostFix_InspectPanePlus_DrawButtons patched", false);
                        return true;
                    }
                    else
                    {
                        Log.Message("banish.animals : RimHUD installed, could not find patch method name", false);
                    }
                }
                else
                {
                    Log.Message("banish.animals : RimHUD installed, could not find patch type", false);
                }
            }

            return false;
        }
    }
}
