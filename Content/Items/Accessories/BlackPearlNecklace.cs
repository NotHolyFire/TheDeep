using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using TheDeep.Common.SubPlayer;

namespace TheDeep.Content.Items.Accessories {
    [AutoloadEquip(EquipType.Neck)]
    public class BlackPearlNecklace : ModItem
    {


        public override void SetStaticDefaults()
        {
            int equipslot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Neck);
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 26;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(gold: 3);
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetCritChance(DamageClass.Generic) += 6f;
            player.equipmentBasedLuckBonus += 0.06f; //wish I knew it was this easy THREE MONTHS AGO
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BlackPearl, 3);
            recipe.AddIngredient(ItemID.Chain, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}