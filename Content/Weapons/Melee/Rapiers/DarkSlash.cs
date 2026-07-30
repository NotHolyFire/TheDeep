
using TheDeep.Content.Items.Material;
using TheDeep.Content.Buffs;
using Terraria.DataStructures;
using TheDeep.Content.Projectiles.player.Weapons.Melee.Rapiers;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using TheDeep.Content.Buffs.Debuff;
using TheDeep.Content.Buffs.Buff;

namespace TheDeep.Content.Items.Weapons.Melee.Rapiers
{
    public class DarkSlash : ModItem
    {
        public int attackType = 0;
        public int comboExpireTimer = 0;

        public override void SetDefaults()
        {
            Item.damage = 25;
            Item.knockBack = 7;

			Item.useStyle = ItemUseStyleID.Rapier;
			Item.useAnimation = 5;
			Item.useTime = 5;

			Item.width = 52;
			Item.height = 52;

            Item.UseSound = SoundID.Item1;

			Item.DamageType = DamageClass.MeleeNoSpeed;

            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.noMelee = true;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 20);

            Item.shoot = ModContent.ProjectileType<DarkSlashProj>();
            Item.shootSpeed = 4.1f;
        }
        public override bool MeleePrefix() => true;

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.DemoniteBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override bool AltFunctionUse(Player player)
        {
            if (!player.HasBuff(ModContent.BuffType<ParryFail>()))
            {
                return true;
            }else
                return false;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) 
        {
            if (player.altFunctionUse == 0)
            {
                type = ModContent.ProjectileType<DarkSlashProj>();
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer, attackType);
            }
            if (player.altFunctionUse == 2 && !player.HasBuff(ModContent.BuffType<ParryFail>()) || player.altFunctionUse == 2 && !player.HasBuff(ModContent.BuffType<DarkSlashBuff>()))
            {
                type = ModContent.ProjectileType<DarkSlashParry>();
			// Using the shoot function, we override the swing projectile to set ai[0] (which attack it is)
			Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer, attackType);
			attackType = (attackType + 1) % 2; // Increment attackType to make sure next swing is different
			comboExpireTimer = 0;
            } // Every time the weapon is used, we reset this so the combo does not expire
			return false; // return false to prevent original projectile from being shot
		}
        public override void UpdateInventory(Player player)
        {
            if (comboExpireTimer++ >= 120)
                attackType = 0;
        }

    }
}