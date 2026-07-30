using TheDeep.Content.Projectiles.player.Weapons.Ranged;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TheDeep.Content.Items.Weapons.Ranged
{
    public class Shellshocker : ModItem
    {
        public const int HoldoutDistance = 20;

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.shootSpeed = 6f;
            Item.knockBack = 2f;
            Item.width = 50;
            Item.height = 28;
            Item.damage = 10;
            Item.shoot = ModContent.ProjectileType<ShellshockerMk2Projectile>();
            Item.useAmmo = AmmoID.Bullet;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(gold: 2);
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.DamageType = DamageClass.Ranged;
            Item.channel = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // Since this item will attempt to shoot an ammo item, we need to set it back to the actual held projectile here.
            type = ModContent.ProjectileType<ShellshockerMk2Projectile>();

            // The velocity value provided is not correct, so we need to calculate a new velocity since velocity for held projectiles is actually the holdout offset.
            velocity = Vector2.Normalize(velocity) * HoldoutDistance;

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer);
            return false;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            // This prevents the item from consuming ammo when initially used. The projectile will "spin up" and then it will consume ammo instead.
            if (player.ItemTimeIsZero)
            {
                return false;
            }
            return true;
        }
    }
}
