using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Content.Items.Fish;
using TheDeep.Content.Items.Material;
using TheDeep.Content.Items.Misc;
using TheDeep.Content.Projectiles;
using TheDeep.Content.Projectiles.player.Weapons.Ranged;

namespace TheDeep.Content.Items.Weapons.Ranged
{
    // This is a basic item template.
    // Please see tModLoader's ExampleMod for every other example:
    // https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
    public class Shellshock : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Registers a vertical animation with 4 frames and each one will last 5 ticks (1/12 second)
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 6));
        }

        public override void SetDefaults()
        {
            Item.width = 40; // The item texture's width.
            Item.height = 40; // The item texture's height.

            Item.useStyle = ItemUseStyleID.Shoot; // The useStyle of the Item.
            Item.useTime = 9; // The time span of using the weapon. Remember in terraria, 60 frames is a second.
            Item.useAnimation = 9; // The time span of the using animation of the weapon, suggest setting it the same as useTime.
            Item.autoReuse = true; // Whether the weapon can be used more than once automatically by holding the use button.

            Item.DamageType = DamageClass.Ranged; // Whether your item is part of the melee class.
            Item.damage = 11; // The damage your item deals.
            Item.knockBack = 4; // The force of knockback of the weapon. Maximum is 20
            Item.crit = 2; // The critical strike chance the weapon has. The player, by default, has a 4% critical strike chance.

            Item.value = Item.buyPrice(silver: 150); // The value of the weapon in copper coins.
            Item.rare = ItemRarityID.Green; // Give this item our custom rarity.
            Item.UseSound = SoundID.Item11; // The sound when the weapon is being used.

            Item.shoot = ModContent.ProjectileType<RedCoralShot>();
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Bullet; 
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(4));

            type = ModContent.ProjectileType<RedCoralShot>();
            // Here we randomly set type to either the original (as defined by the ammo), a vanilla projectile, or a mod projectile.
            type = Main.rand.Next(new int[] { type, ModContent.ProjectileType<RedCoralShot>(), ModContent.ProjectileType<BlueCoralShot>(), ModContent.ProjectileType<OrangeCoralShot>(), ModContent.ProjectileType<PinkCoralShot>(), ModContent.ProjectileType<GreenCoralShot>(), ModContent.ProjectileType<RedCoralShot>() });
            if (type == ModContent.ProjectileType<OrangeCoralShot>())
            {
                damage *= 2;
            }
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(6f, -2f);
        }
    }
}