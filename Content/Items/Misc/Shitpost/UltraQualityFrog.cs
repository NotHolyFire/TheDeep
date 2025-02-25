using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Content.Items.Material;
using TheDeep.Content.Items.Material.Components;

namespace TheDeep.Content.Items.Misc.Shitpost
{

    public class UltraQualityFrog : ModItem
    {

        public override void SetDefaults()
    {
        Item.width = 12;
        Item.height = 30;
        Item.rare = ItemRarityID.Master;
    }

        public override void SetStaticDefaults()
        {
            ItemID.Sets.BossBag[Type] = true;
            Item.ResearchUnlockCount = 1;
        }


    }
}