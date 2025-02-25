using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Content.Buffs;
using TheDeep.Content.Buffs.Cooldown;
using TheDeep.Content.Items.Fish;
using TheDeep.Content.Projectiles;
using TheDeep.Content.Projectiles.player.Weapons.Melee;

namespace TheDeep.Content.Items.Weapons.Melee
{
    // This is a basic item template.
    // Please see tModLoader's ExampleMod for every other example:
    // https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
    public class Riptide : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.SkipsInitialUseSound[Item.type] = true; // This skips use animation-tied sound playback, so that we're able to make it be tied to use time instead in the UseItem() hook.
            ItemID.Sets.Spears[Item.type] = true; // This allows the game to recognize our new item as a spear.
        }

        public override void SetDefaults()
        {
            // Common Properties
            Item.rare = ItemRarityID.Expert; // Assign this item a rarity level of Pink
            Item.value = Item.sellPrice(gold: 3); // The number and type of coins item can be sold for to an NPC

            // Use Properties
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.useAnimation = 12; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useTime = 18; // The length of the item's use time in ticks (60 ticks == 1 second.)
            Item.UseSound = SoundID.Item71; // The sound that this item plays when used.
            Item.autoReuse = true; // Allows the player to hold click to automatically use the item again. Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()
            Item.expert = true;

            // Weapon Properties
            Item.damage = 25;
            Item.crit = 4;
            Item.knockBack = 5.5f;
            Item.noUseGraphic = true; // When true, the item's sprite will not be visible while the item is in use. This is true because the spear projectile is what's shown so we do not want to show the spear sprite as well.
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true; // Allows the item's animation to do damage. This is important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.

            // Projectile Properties
            Item.shootSpeed = 3.7f; // The speed of the projectile measured in pixels per frame.
            Item.shoot = ModContent.ProjectileType<RoyalTridentProjectile>(); // The projectile that is fired from this weapon
        }

        public override bool CanUseItem(Player player)
        {
            // Ensures no more than one spear can be thrown out, use this when using autoReuse
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
        public override void HoldItem(Player player)
        {
            if (player.wet == true)
            {
                Item.damage = 33;
                Item.crit = 8;
                player.AddBuff(BuffID.Flipper, 1);
            }

            if (player.wet == false)
            {
                Item.damage = 25;
                Item.crit = 4;
            }
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // NewProjectile returns the index of the projectile it creates in the NewProjectile array.
            // Here we are using it to gain access to the projectile object.
            type = ModContent.ProjectileType<RoyalTridentProjectile>();
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);

            if (player.wet == true) {

                type = ModContent.ProjectileType<TridentShotFriendly>();
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            }

            if (player.altFunctionUse == 2 && player.wet == true && !player.HasBuff(ModContent.BuffType<Resting>()))
            {
                float speed = 20f;
                player.velocity = player.Center.DirectionTo(Main.MouseWorld) * speed;
                player.AddBuff(ModContent.BuffType<Resting>(), 180);
            }

            // We do not want vanilla to spawn a duplicate projectile.
            return false;
        }

        public override bool? UseItem(Player player)
        {
            // Because we're skipping sound playback on use animation start, we have to play it ourselves whenever the item is actually used.
            if (!Main.dedServ && Item.UseSound.HasValue)
            {
                SoundEngine.PlaySound(Item.UseSound.Value, player.Center);
            }

            return null;

        }
    }
}