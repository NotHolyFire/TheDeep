using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDeep.Content.Items.Fish.Quest
{
    // This is a basic item template.
    // Please see tModLoader's ExampleMod for every other example:
    // https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
    public class Fisharaoh : ModItem
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
            description = "Once upon a time, there was a fish as old as Terraria itself.. No, I won't waste my time describing it, go get it for my aquarium!";
            catchLocation = "Caught in Lava Lakes in Caverns";
        }
    }
}
