using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDeep.Content.Items.Fish
{
    // This is a basic item template.
    // Please see tModLoader's ExampleMod for every other example:
    // https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
    public class Dinofin : ModItem
    {

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 2;
            ItemID.Sets.CanBePlacedOnWeaponRacks[Type] = true; // All vanilla fish can be placed in a weapon rack.
        }

        public override void SetDefaults()
        {
            // DefaultToQuestFish sets quest fish properties.
            // Of note, it sets rare to ItemRarityID.Quest, which is the special rarity for quest items.
            // It also sets uniqueStack to true, which prevents players from picking up a 2nd copy of the item into their inventory.
            Item.DefaultToQuestFish();
        }

        public override bool IsQuestFish() => true; // Makes the item a quest fish

        public override void AnglerQuestChat(ref string description, ref string catchLocation)
        {
            // How the angler describes the fish to the player.
            description = "Once upon a time, there was a fish as old as Terraria itself.. No, I won't waste my time describing it, go get it for my aquarium!";
            // What it says on the bottom of the angler's text box of how to catch the fish.
            catchLocation = "Caught in Lava Lakes in Caverns";
        }
    }
}
