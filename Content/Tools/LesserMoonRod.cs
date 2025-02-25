using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using TheDeep.Content.Items.Placeables;
using TheDeep.Content.Items.Material;
using TheDeep.Content.Buffs.Cooldown;
using TheDeep.Content.Buffs.Moon;
using TheDeep.Content.Weapons.Reeling;
using TheDeep.Content.Projectiles.Bobber;

namespace TheDeep.Content.Items.Tools
{
    // ExampleFishingRod is a fishing rod item.
    // The code in SetDefaults and the code setting lineOriginOffset in ModifyFishingLine is all the would be needed for a typical working fishing rod item.
    // All of the rest of the code showcases other additional capabilities, such as multiple bobbers, custom line colors, and fishing in lava.
    public class LesserMoonRod : ModItem
    {

        public override void SetStaticDefaults()
        {
            // This set is one that every boss bag should have.
            // It will create a glowing effect around the item when dropped in the world.
            // It will also let our boss bag drop dev armor..
            ItemID.Sets.BossBag[Type] = true;
            ItemID.Sets.SortingPriorityMaterials[Item.type] = 712;
            Item.ResearchUnlockCount = 1;

            if (Main.moonPhase == 2 && Main.bloodMoon == false)
            {
                ItemID.Sets.CanFishInLava[Item.type] = true;
            }

            if (Main.moonPhase == 6 && Main.bloodMoon == false)
            {
                ItemID.Sets.CanFishInLava[Item.type] = true;
            }

        }

        public override void SetDefaults()
        {
            // These are copied through the CloneDefaults method:
            Item.width = 24;
            Item.height = 28;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 8;
            Item.useTime = 8;
            Item.UseSound = SoundID.Item1;
            Item.damage = 35;
            Item.DamageType = ModContent.GetInstance<Reeling>();

            Item.rare = ItemRarityID.Red;
            Item.fishingPole = 15; // Sets the poles fishing power
            Item.shootSpeed = 18f; // Sets the speed in which the bobbers are launched. Wooden Fishing Pole is 9f and Golden Fishing Rod is 17f.
            Item.shoot = ModContent.ProjectileType<MoonBobber>(); // The bobber projectile. Note that this will be overridden by Fishing Bobber accessories if present, so don't assume the bobber spawned is the specified projectile. https://terraria.wiki.gg/wiki/Fishing_Bobbers
        }

        // Grants the High Test Fishing Line bool if holding the item.
        // NOTE: Only triggers through the hotbar, not if you hold the item by hand outside of the inventory.

        // Overrides the default shooting method to fire multiple bobbers.
        // NOTE: This will allow the fishing rod to summon multiple Duke Fishrons with multiple Truffle Worms in the inventory.
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {


            int bobberAmount = 1;
            if (Main.moonPhase == 0 && Main.bloodMoon == false)
            {
                bobberAmount = 2;
            }
            float spreadAmount = 150f; // how much the different bobbers are spread out.


            for (int index = 0; index < bobberAmount; ++index)
            {
                Vector2 bobberSpeed = velocity + new Vector2(Main.rand.NextFloat(-spreadAmount, spreadAmount) * 0.05f, Main.rand.NextFloat(-spreadAmount, spreadAmount) * 0.05f);

                // Generate new bobbers
                Projectile.NewProjectile(source, position, bobberSpeed, type, damage, 0f, player.whoAmI);
            }
            return false;
        }

        public override void HoldItem(Player player)
        {

            if (Main.moonPhase == 0 && Main.bloodMoon == false)
            {
                player.AddBuff(ModContent.BuffType<MoonRodFullMoon>(), 1);
                Item.fishingPole = 40;
            }

            if (Main.moonPhase == 1 && Main.bloodMoon == false)
            {
                player.AddBuff(ModContent.BuffType<MoonRodWaningGib>(), 1);
                Item.fishingPole = 35;
                
            }

            if (Main.moonPhase == 2 && Main.bloodMoon == false)
            {
                player.AddBuff(ModContent.BuffType<MoonRod34>(), 1);
                Item.fishingPole = 30;
            }

            if (Main.moonPhase == 3 && Main.bloodMoon == false)
            {
                player.AddBuff(ModContent.BuffType<MoonRodWaningCres>(), 1);
                Item.fishingPole = 25;
            }


            if (Main.moonPhase == 4 && Main.bloodMoon == false)
            {
                player.AddBuff(ModContent.BuffType<MoonRodNew>(), 1);
                Item.fishingPole = 20;
            }

            if (Main.moonPhase == 5 && Main.bloodMoon == false)
            {
                player.AddBuff(ModContent.BuffType<MoonRodWaxingCres>(), 1);
                Item.fishingPole = 25;
            }

            if (Main.moonPhase == 6 && Main.bloodMoon == false)
            {
                player.AddBuff(ModContent.BuffType<MoonRod14>(), 1);
                Item.fishingPole = 30;
            }

            if (Main.moonPhase == 7 && Main.bloodMoon == false)
            {
                player.AddBuff(ModContent.BuffType<MoonRodWaxingGib>(), 1);
                Item.fishingPole = 35;
            }

            if (Main.bloodMoon == true)
            {
                Item.fishingPole = 40;
                player.AddBuff(BuffID.Battle, 1);
            }
        }

        public override void ModifyFishingLine(Projectile bobber, ref Vector2 lineOriginOffset, ref Color lineColor)
        {
            // Change these two values in order to change the origin of where the line is being drawn.
            // This will make it draw 43 pixels right and 30 pixels up from the player's center, while they are looking right and in normal gravity.
            lineOriginOffset = new Vector2(43, -30);
        }
    }
}