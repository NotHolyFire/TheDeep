using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using TheDeep.Content.Items.Placeables;
using TheDeep.Content.Items.Material;
using TheDeep.Content.Projectiles.Bobber;

namespace TheDeep.Content.Items.Tools
{
    // ExampleFishingRod is a fishing rod item.
    // The code in SetDefaults and the code setting lineOriginOffset in ModifyFishingLine is all the would be needed for a typical working fishing rod item.
    // All of the rest of the code showcases other additional capabilities, such as multiple bobbers, custom line colors, and fishing in lava.
    public class BrassFishingRod : ModItem
    {

        public override void SetDefaults()
        {
            // These are copied through the CloneDefaults method:
            // Item.width = 24;
            // Item.height = 28;
            // Item.useStyle = ItemUseStyleID.Swing;
            // Item.useAnimation = 8;
            // Item.useTime = 8;
            // Item.UseSound = SoundID.Item1;
            Item.CloneDefaults(ItemID.WoodFishingPole);

            Item.rare = ItemRarityID.Green;
            Item.fishingPole = 30; // Sets the poles fishing power
            Item.shootSpeed = 12f; // Sets the speed in which the bobbers are launched. Wooden Fishing Pole is 9f and Golden Fishing Rod is 17f.
            Item.shoot = ModContent.ProjectileType<BrassBobber>(); // The bobber projectile. Note that this will be overridden by Fishing Bobber accessories if present, so don't assume the bobber spawned is the specified projectile. https://terraria.wiki.gg/wiki/Fishing_Bobbers
        }

        // Grants the High Test Fishing Line bool if holding the item.
        // NOTE: Only triggers through the hotbar, not if you hold the item by hand outside of the inventory.
        public override void HoldItem(Player player)
        {
            player.accFishingLine = true;
        }

        // Overrides the default shooting method to fire multiple bobbers.
        // NOTE: This will allow the fishing rod to summon multiple Duke Fishrons with multiple Truffle Worms in the inventory.
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int bobberAmount = 2; // 2 bobbers!!!
            float spreadAmount = 75f; // how much the different bobbers are spread out.

            for (int index = 0; index < bobberAmount; ++index)
            {
                Vector2 bobberSpeed = velocity + new Vector2(Main.rand.NextFloat(-spreadAmount, spreadAmount) * 0.05f, Main.rand.NextFloat(-spreadAmount, spreadAmount) * 0.05f);

                // Generate new bobbers
                Projectile.NewProjectile(source, position, bobberSpeed, type, 0, 0f, player.whoAmI);
            }
            return false;
        }

        public override void ModifyFishingLine(Projectile bobber, ref Vector2 lineOriginOffset, ref Color lineColor)
        {
            // Change these two values in order to change the origin of where the line is being drawn.
            // This will make it draw 43 pixels right and 30 pixels up from the player's center, while they are looking right and in normal gravity.
            lineOriginOffset = new Vector2(43, -30);
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<BrassBar>(13);
            recipe.AddIngredient(ItemID.FiberglassFishingPole);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}