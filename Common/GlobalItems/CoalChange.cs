using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Content.Items.Material;

namespace TheDeep.Common.GlobalItems
{
    public class CoalChange : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.Coal;
        }

        public override void SetDefaults(Item item)
        {
            item.maxStack = Item.CommonMaxStack;
        }

        public class CoalRecipes : ModSystem
        {

            public override void AddRecipes()
            {

                Recipe recipe = Recipe.Create(ItemID.Coal);
                recipe.AddRecipeGroup("Wood", 5);
                recipe.AddTile(TileID.Fireplace);
                recipe.Register();

                Recipe trecipe = Recipe.Create(ItemID.Torch, 9);
                trecipe.AddIngredient(ItemID.Coal);
                trecipe.AddRecipeGroup("Wood");
                trecipe.Register();

                Recipe brecipe = Recipe.Create(ItemID.Bomb, 3);
                brecipe.AddRecipeGroup("IronBar", 2);
                brecipe.AddIngredient(ItemID.Coal, 3);
                brecipe.AddTile(TileID.Anvils);
                brecipe.Register();

            }
        }
    }
}