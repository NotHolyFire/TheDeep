
using TheDeep.Content.Projectiles.Bobber;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Content.Projectiles.Bobbers;
using TheDeep.Common.SubPlayer;

namespace TheDeep.Content.Items.Accessories
{
    public class FishingMagnet : ModItem
    {
        
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 50);
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<SubmergedStats>().TreasureChance += 0.2f;
            if (hideVisual == false) 
                player.overrideFishingBobber = ModContent.ProjectileType<MagnetBobber>();
        }
    }
}