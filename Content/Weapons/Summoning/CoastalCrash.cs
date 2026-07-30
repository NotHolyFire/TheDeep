using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TheDeep.Content.Buffs;
using TheDeep.Content.Buffs.Weapon;
using TheDeep.Content.Items.Fish;
using TheDeep.Content.Items.Material;
using TheDeep.Content.Items.Misc;
using TheDeep.Content.Items.Placeables;
using TheDeep.Content.Projectiles;
using TheDeep.Content.Projectiles.player.Weapons.Summoner;

namespace TheDeep.Content.Items.Weapons.Summoning
{
    public class CoastalCrash : ModItem
    {

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(CoastalWhipDebuff.TagDamage);

        public override void SetDefaults()
        {
            // Call this method to quickly set some of the properties below.
            //Item.DefaultToWhip(ModContent.ProjectileType<ExampleWhipProjectileAdvanced>(), 20, 2, 4);

            Item.DamageType = DamageClass.SummonMeleeSpeed;
            Item.damage = 30;
            Item.knockBack = 2;
            Item.rare = ItemRarityID.Green;

            Item.shoot = ModContent.ProjectileType<CoastalCrashProjectile>();
            Item.shootSpeed = 4;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.UseSound = SoundID.Item152;
            Item.channel = true; // This is used for the charging functionality. Remove it if your whip shouldn't be chargeable.
            Item.noMelee = true;
            Item.noUseGraphic = true;
        }

        // Makes the whip receive melee prefixes
        public override bool MeleePrefix()
        {
            return true;
        }
    }
}