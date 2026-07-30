using Terraria.DataStructures;
using TheDeep.Content.Projectiles.player.Weapons.Melee.Rapiers;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using TheDeep.Content.Buffs;
using Microsoft.Xna.Framework;
using TheDeep.Content.Buffs.Debuff;

namespace TheDeep.Content.Items.Weapons.Melee.Rapiers
{
    public class VertebraeRipper : ModItem
    {
        
        public int attackType = 0;
        public int comboExpireTimer = 0;

        public override void SetDefaults()
        {
            Item.damage = 29;
            Item.knockBack = 7;

			Item.useStyle = ItemUseStyleID.Rapier;
			Item.useAnimation = 5;
			Item.useTime = 5;

			Item.width = 46;
			Item.height = 46;

            Item.UseSound = SoundID.Item1;

			Item.DamageType = DamageClass.MeleeNoSpeed;

            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.noMelee = true;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 25);

            Item.shoot = ModContent.ProjectileType<VertebraeRipperSwordProj>();
            Item.shootSpeed = 4.1f;
        }
        public override bool MeleePrefix() => true;

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.CrimtaneBar, 10);
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
                type = ModContent.ProjectileType<VertebraeRipperSwordProj>();
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer, attackType);
            }
            if (player.altFunctionUse == 2 && !player.HasBuff(ModContent.BuffType<ParryFail>()))
            {
                type = ModContent.ProjectileType<VertebraeRipperParry>();
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