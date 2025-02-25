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
using TheDeep.Content.Items.Placeables;
using TheDeep.Content.Projectiles;
using TheDeep.Content.Projectiles.player.Weapons.Magic;
using TheDeep.Content.Projectiles.player.Weapons.Melee;

namespace TheDeep.Content.Items.Weapons.Magic
{
    public class SandClawStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Type] = true; // This makes the useStyle animate as a staff instead of as a gun.
        }

        public override void SetDefaults()
        {
            Item.width = 40; // The item texture's width.
            Item.height = 40; // The item texture's height.

            Item.useStyle = ItemUseStyleID.Shoot; // The useStyle of the Item.
            Item.useTime = 50; // The time span of using the weapon. Remember in terraria, 60 frames is a second.
            Item.useAnimation = 50; // The time span of the using animation of the weapon, suggest setting it the same as useTime.
            Item.mana = 16;
            Item.shootSpeed = 8;
            Item.noMelee = true;
            Item.autoReuse = true; // Whether the weapon can be used more than once automatically by holding the use button.
            Item.DamageType = DamageClass.Magic; // Whether your item is part of the melee class.
            Item.damage = 50; // The damage your item deals.
            Item.knockBack = 5; // The force of knockback of the weapon. Maximum is 20
            Item.shoot = ModContent.ProjectileType<SandBoulder>();
            Item.value = Item.buyPrice(gold: 2); // The value of the weapon in copper coins.
            Item.rare = ItemRarityID.Green; // Give this iem our custom rarity.
            Item.UseSound = SoundID.Item109; // The sound when the weapon is being used.

        }
    }
}