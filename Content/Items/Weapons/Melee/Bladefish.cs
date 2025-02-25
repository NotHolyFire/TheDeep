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

namespace TheDeep.Content.Items.Weapons.Melee
{
    // This is a basic item template.
    // Please see tModLoader's ExampleMod for every other example:
    // https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
    public class Bladefish : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 40; // The item texture's width.
            Item.height = 40; // The item texture's height.

            Item.useStyle = ItemUseStyleID.Swing; // The useStyle of the Item.
            Item.useTime = 25; // The time span of using the weapon. Remember in terraria, 60 frames is a second.
            Item.useAnimation = 25; // The time span of the using animation of the weapon, suggest setting it the same as useTime.
            Item.autoReuse = true; // Whether the weapon can be used more than once automatically by holding the use button.

            Item.DamageType = DamageClass.Melee; // Whether your item is part of the melee class.
            Item.damage = 13; // The damage your item deals.
            Item.knockBack = 20; // The force of knockback of the weapon. Maximum is 20
            Item.crit = 6; // The critical strike chance the weapon has. The player, by default, has a 4% critical strike chance.

            Item.value = Item.buyPrice(gold: 1); // The value of the weapon in copper coins.
            Item.rare = ItemRarityID.Blue; // Give this item our custom rarity.
            Item.UseSound = SoundID.Item1; // The sound when the weapon is being used.
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            // 60 frames = 1 second
            target.AddBuff(BuffID.Wet, 180);
        }
    }
}