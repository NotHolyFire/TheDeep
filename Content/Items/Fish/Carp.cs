using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDeep.Content.Items.Fish
{
    // This is a basic item template.
    // Please see tModLoader's ExampleMod for every other example:
    // https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
    public class Carp : ModItem
    {

        public override void SetStaticDefaults()
        {
            ItemID.Sets.CanBePlacedOnWeaponRacks[Type] = true; // All vanilla fish can be placed in a weapon rack.
            Item.ResearchUnlockCount = 30;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.White;
            Item.value = Item.buyPrice(silver: 5);
        }
        public override void AddRecipes()
        {

            Recipe recipe = Recipe.Create(ItemID.CookedFish);
            recipe.AddIngredient(ModContent.ItemType<Content.Items.Fish.Carp>());
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
