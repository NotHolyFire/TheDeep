using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Content.Tiles;
using TheDeep.Content.Tiles.Machinery;

namespace TheDeep.Content.Items.Misc
{

    public class ManaFuelCell : ModItem
    {
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 6));

            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }

        public override void SetDefaults()
    {
        Item.width = 12;
        Item.height = 30;
        Item.maxStack = Item.CommonMaxStack;
        Item.value = 500;
        Item.rare = ItemRarityID.LightRed;
    }

    public override void PostUpdate()
    {
        Lighting.AddLight(Item.Center, Color.PowderBlue.ToVector3() * 1f * Main.essScale);
    }
        public override void AddRecipes()
        {
            Recipe coalEngine = CreateRecipe();
            coalEngine.AddIngredient(ItemID.Coal);
            coalEngine.AddTile(ModContent.TileType<ManaInfuser>());
            coalEngine.Register();

            Recipe starInfuser = CreateRecipe(5);
            starInfuser.AddIngredient(ItemID.ManaCrystal);
            starInfuser.AddTile(ModContent.TileType<ManaInfuser>());
            starInfuser.Register();
        }


}
}