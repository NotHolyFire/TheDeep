using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TheDeep.Content.Items.Fish;

namespace TheDeep.Content.Items.Armor.Dev
{

    [AutoloadEquip(EquipType.Body)] // ?? não funciona?
    public class  BloodfiendCoat : ModItem
    {

        public override void Load()
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            EquipLoader.AddEquipTexture(Mod, "TheDeep/Content/Items/Armor/Dev/BloodfiendCoat_Back", EquipType.Back, this);
            EquipLoader.AddEquipTexture(Mod, "TheDeep/Content/Items/Armor/Dev/BloodfiendCoat_Front", EquipType.Front, this);
            EquipLoader.AddEquipTexture(Mod, "TheDeep/Content/Items/Armor/Dev/BloodfiendCoat_Waist", EquipType.Waist, this);
        }

        public override void EquipFrameEffects(Player player, EquipType type)
        {
            player.back = (sbyte)EquipLoader.GetEquipSlot(Mod, Name, EquipType.Back);
            player.front = (sbyte)EquipLoader.GetEquipSlot(Mod, Name, EquipType.Front);
            player.waist = (sbyte)EquipLoader.GetEquipSlot(Mod, Name, EquipType.Waist);
        }

        public override void SetDefaults()
        {
            Item.width = 18; 
            Item.height = 18; 
            Item.value = Item.sellPrice(silver: 75);
            Item.rare = ItemRarityID.Cyan;
            Item.vanity = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Silk, 8);
            recipe.AddTile(TileID.Loom);
            recipe.Register();
        }

    }
}