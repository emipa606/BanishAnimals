using System.Reflection;
using Verse;
using System.Linq;

namespace BanishAnimals.Harmony
{
    [StaticConstructorOnStartup]
    internal class PatchLoader
    {
        static PatchLoader()
        {
            var rimHudPatched = false; 
            var instance = new HarmonyLib.Harmony("banish.animals");        
            ModContentPack rimHUD = LoadedModManager.RunningMods.FirstOrDefault((ModContentPack mod) => mod.Name.Contains("RimHUD"));

            if(rimHUD != null)
            {
               rimHudPatched =  RimHUD_Compatibility.LoadRimHUDCompatibility(instance, rimHUD);
            }

            if (!rimHudPatched)
            {
                instance.PatchAll(Assembly.GetExecutingAssembly());
                Log.Message("banish.animals : patched DoInspectPaneButtons", false);
            }
        }
    }
}
