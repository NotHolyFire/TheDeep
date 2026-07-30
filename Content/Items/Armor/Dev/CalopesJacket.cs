using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TheDeep.Content.Items.Fish;
using TheDeep.Content.Items.Material;

namespace TheDeep.Content.Items.Armor.Dev
{
    // This is a basic item template.
    // Please see tModLoader's ExampleMod for every other example:
    // https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
    [AutoloadEquip(EquipType.Body)]
    public class CalopesJacket : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 18; // Width of the item
            Item.height = 18; // Height of the item
            Item.value = Item.sellPrice(silver: 75); // How many coins the item is worth
            Item.rare = ItemRarityID.Cyan; // The rarity of the item
            Item.vanity = true; // The amount of defense the item will give when equipped
        }

        public override void Load()
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            EquipLoader.AddEquipTexture(Mod, "TheDeep/Content/Items/Armor/Dev/CalopesJacket_Back", EquipType.Back, this);
            EquipLoader.AddEquipTexture(Mod, "TheDeep/Content/Items/Armor/Dev/CalopesJacket_Front", EquipType.Front, this);
        }

        public override void EquipFrameEffects(Player player, EquipType type)
        {
            player.back = (sbyte)EquipLoader.GetEquipSlot(Mod, Name, EquipType.Back);
            player.front = (sbyte)EquipLoader.GetEquipSlot(Mod, Name, EquipType.Front);

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