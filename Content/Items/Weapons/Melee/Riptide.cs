using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Content.Projectiles.player.Weapons.Melee;

namespace TheDeep.Content.Items.Weapons.Melee
{
    public class Riptide : ModItem
    {
        public static float Speed = 14f;
        public static int SuperAttackCharge = 0;
        public override void SetStaticDefaults()
        {
            ItemID.Sets.SkipsInitialUseSound[Item.type] = true;
            ItemID.Sets.Spears[Item.type] = true;
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

        public override void SetDefaults() 
        {
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(gold: 3);

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 20;
            Item.useTime = 20; 
            Item.UseSound = SoundID.Item71; 
            Item.autoReuse = true;  

            Item.damage = 25;
            Item.crit = 7;
            Item.knockBack = 5.5f;
            Item.noUseGraphic = true; 
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true; 

            Item.shootSpeed = 3.7f; 
            Item.shoot = ModContent.ProjectileType<RiptideProjectile>();
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
        public override void HoldItem(Player player)
        {
            if (player.wet == true)
            {
                Item.damage = 33;
                Item.crit = 8;
            }

            if (player.wet == false)
            {
                Item.damage = 25;
                Item.crit = 7;
            }
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<RiptideProjectile>();
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);

            if (SuperAttackCharge <= 5) {

                type = ModContent.ProjectileType<RiptideBoomerang>();
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            }

            if (SuperAttackCharge == 6)
            {
                SuperAttackCharge = 0;
                 type = ModContent.ProjectileType<RiptideSuperAttack>();
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI); 
            }
            if (player.altFunctionUse == 2 && player.wet == true || player.altFunctionUse == 2 && Main.raining == true/*&& !player.HasBuff(ModContent.BuffType<Resting>())*/)
            {
                float speed = 20f;
                player.velocity = player.Center.DirectionTo(Main.MouseWorld) * speed;
                //player.AddBuff(ModContent.BuffType<Resting>(), 180);
            }


            // We do not want vanilla to spawn a duplicate projectile.
            return false;
        }

        public override bool? UseItem(Player player)
        {
            SuperAttackCharge +=1;
            if (!Main.dedServ && Item.UseSound.HasValue)
            {
                SoundEngine.PlaySound(Item.UseSound.Value, player.Center);
            }

            return null;

        }
    }
}
