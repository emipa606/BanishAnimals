using RimWorld;
using UnityEngine;
using Verse;

namespace BanishAnimals.Harmony
{
    internal static class BanishButton
    {
        public static void DrawBanishButton(Rect rect, ref float lineEndWidth)
        {
            try
            {
                if (Find.Selector.NumSelected != 1)
                {
                    return;
                }

                Thing singleSelectedThing = Find.Selector.SingleSelectedThing;

                if (singleSelectedThing == null)
                {
                    return;
                }
                if (!(singleSelectedThing is Pawn pawn) || pawn.Faction != Faction.OfPlayer || !pawn.RaceProps.Animal)
                {
                    return;
                }
                lineEndWidth += 24f;

                var rect2 = new Rect(rect.width - lineEndWidth, 0f, 24f, 24f);
                Texture2D ico = ContentFinder<Texture2D>.Get("UI/Buttons/Banish");
                TooltipHandler.TipRegion(rect2, PawnBanishUtility.GetBanishButtonTip(pawn));

                if (!Widgets.ButtonImage(rect2, ico))
                {
                    return;
                }
                if (pawn.Downed)
                {
                    Messages.Message("MessageCantBanishDownedPawn".Translate(pawn.LabelShort, pawn).AdjustedFor(pawn), pawn, MessageTypeDefOf.RejectInput, historical: false);
                }
                else
                {
                    PawnBanishUtility.ShowBanishPawnConfirmationDialog(pawn);
                }
            }
            catch (System.Exception e)
            {
                Log.Message($"BanishAnimals : BanishButton.DrawBanishButton error - {e.Message}", true);
            }
        }
    }
}
