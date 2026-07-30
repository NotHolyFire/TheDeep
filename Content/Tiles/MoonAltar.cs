
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using TheDeep.Common.Systems;
using TheDeep.Content.Items.Fish;
using TheDeep.Content.Items.Misc;


namespace TheDeep.Content.Tiles
{
    public class MoonAltar : ModTile
    {
        public override void SetStaticDefaults()
        {
            // Properties
            Main.tileLighted[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;
            TileID.Sets.PreventsTileRemovalIfOnTopOfIt[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;

            // Placement
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.StyleLineSkip = 5; // This needs to be added to work for modded tiles.
            TileObjectData.addTile(Type);
            MineResist = 999f;
            MinPick = 10000;


            AddMapEntry(new Microsoft.Xna.Framework.Color(60, 60, 60), CreateMapEntryName());
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            if (Main.dayTime == false)
            {
                r = 0.73f;
                g = 1.16f;
                b = 1.30f;
            }


        }


        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            fail = true;
            noItem = true;
            effectOnly = true;
        }


        public override void NumDust(int x, int y, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void KillMultiTile(int x, int y, int frameX, int frameY)
        {
        }
    }
}