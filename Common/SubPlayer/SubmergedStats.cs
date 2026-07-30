using Terraria.ModLoader;
using Terraria;
using Terraria.DataStructures;
namespace TheDeep.Common.SubPlayer
{
    public class SubmergedStats : ModPlayer
    {
        Player player => Main.LocalPlayer;
        public int Hostility;
        public float TreasureChance;
        public bool CreatureCatch;
        public override void ResetEffects()
        {
            Hostility = 0; // Increases chance of hooking enemies
            TreasureChance = 1f; // Starts at 1f (0) Increases or decreases chance of hooking crates/treasures
            CreatureCatch = false;
        }

        public override void ModifyFishingAttempt(ref FishingAttempt attempt)
        {
            if (!attempt.crate && TreasureChance > 1f && Main.rand.NextFloat() < TreasureChance-1f)
            {
                attempt.crate = true;
            }

            if (attempt.crate && TreasureChance < 1f && Main.rand.NextFloat() < 1f - TreasureChance)
            {
                attempt.crate = false;
            }

            if (Main.rand.Next(100) < Hostility)
            {
                CreatureCatch = true;
            }

        }
    }
}
