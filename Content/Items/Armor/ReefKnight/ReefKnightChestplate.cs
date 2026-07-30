using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDeep.Content.Items.Material;

namespace TheDeep.Content.Items.Armor.ReefKnight
{
    [AutoloadEquip(EquipType.Body)]

    public class ReefKnightChestplate : ModItem

    {

        public override void SetStaticDefaults()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                var equipSlot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Body);
                ArmorIDs.Body.Sets.HidesArms[equipSlot] = true;
                ArmorIDs.Body.Sets.HidesTopSkin[equipSlot] = true;
            }
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 20;
            Item.value = Item.sellPrice(silver: 5);
            Item.rare = ItemRarityID.Green;
            Item.defense = 12;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 5 / 100f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Coral, 10);
            recipe.AddIngredient<ReefGoldBar>(18);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
