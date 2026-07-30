using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDeep.Content.Items.Fish.Quest
{
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
            description = "The Dryad was yapping about the species back in her time, like if I cared! Well, until she mentioned an old, scaly fish that caught my attention, so I want you to catch it while it's still alive!";
            // What it says on the bottom of the angler's text box of how to catch the fish.
            catchLocation = "Caught in Lava Lakes in Caverns";
        }
    }
}
