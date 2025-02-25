using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Content.Items.Material;
using TheDeep.Content.Items.Material.Components;

namespace TheDeep.Content.Items.Misc
{

    public class Furukotofumi : ModItem
    {

        public override void SetDefaults()
    {
        Item.width = 12;
        Item.height = 30;
            Item.rare = ItemRarityID.Purple;
    }

        public override void SetStaticDefaults()
        {
            // This set is one that every boss bag should have.
            // It will create a glowing effect around the item when dropped in the world.
            // It will also let our boss bag drop dev armor..
            ItemID.Sets.BossBag[Type] = true;
            ItemID.Sets.SortingPriorityMaterials[Item.type] = 712;
            Item.ResearchUnlockCount = 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FallenStar, 10);
            recipe.AddIngredient<TatteredPages>(3);
            recipe.AddTile(TileID.Bookcases);
            recipe.Register();

        }

    }
}