using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDeep.Content.Items.Fish.Quest
{
    public class Oilfish : ModItem
    {

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 2;
            ItemID.Sets.CanBePlacedOnWeaponRacks[Type] = true; // All vanilla fish can be placed in a weapon rack.
        }

        public override void SetDefaults()
        {
            Item.DefaultToQuestFish();
        }

        public override bool IsQuestFish() => true; // Makes the item a quest fish

        public override void AnglerQuestChat(ref string description, ref string catchLocation)
        {
            // How the angler describes the fish to the player.
            description = "Have you seen a fish made of liquid before? No? Well I have, and when I tried to catch it, all that was left in my hands was this black ooze! To get revenge, I want you to fetch it and bring it to me!";
            // What it says on the bottom of the angler's text box of how to catch the fish.
            catchLocation = "Caught in Caverns";
        }
    }
}
