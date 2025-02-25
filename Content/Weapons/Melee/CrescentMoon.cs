using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Content.Buffs;
using TheDeep.Content.Items.Fish;
using TheDeep.Content.Items.Material;
using TheDeep.Content.Items.Misc;
using TheDeep.Content.Projectiles;
using TheDeep.Content.Projectiles.player.Weapons.Melee;

namespace TheDeep.Content.Items.Weapons.Melee
{
    // This is a basic item template.
    // Please see tModLoader's ExampleMod for every other example:
    // https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
    public class CrescentMoon : ModItem
    {
            public override void SetDefaults()
            {
                Item.damage = 8;
                Item.knockBack = 4f;
                Item.useStyle = ItemUseStyleID.Rapier; // Makes the player do the proper arm motion
                Item.useAnimation = 12;
                Item.useTime = 12;
                Item.width = 32;
                Item.height = 32;
                Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Melee;
                Item.autoReuse = false;
                Item.noUseGraphic = true; // The sword is actually a "projectile", so the item should not be visible when used
                Item.noMelee = true; // The projectile will do the damage and not the item

                Item.rare = ItemRarityID.Lime;

            Item.shoot = ModContent.ProjectileType<CrescentMoonProjectile>(); // The projectile is what makes a shortsword work
                Item.shootSpeed = 2.1f; // This value bleeds into the behavior of the projectile as velocity, keep that in mind when tweaking values
            }
        }
    }