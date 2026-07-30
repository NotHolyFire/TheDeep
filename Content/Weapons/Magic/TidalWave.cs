using TheDeep.Content.Projectiles.player.Weapons.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDeep.Content.Items.Weapons.Magic
{
    public class TidalWave : ModItem
    {
        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[Item.shoot] <= 0; 
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 32;
            Item.damage = 20;
            Item.knockBack = 10f;
            Item.rare = ItemRarityID.Blue;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 5;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.UseSound = SoundID.Item66;
            Item.value = Item.buyPrice(gold: 2);

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<TidalWaveHeldProj>();
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;
        }
    }
}