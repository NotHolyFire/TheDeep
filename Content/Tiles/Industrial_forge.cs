
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;


namespace TheDeep.Content.Tiles
{
    public class Industrial_forge : ModTile
    {
        public override void SetStaticDefaults()
        {
            // Properties
            Main.tileLighted[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            AdjTiles = new int[] { TileID.Hellforge };
            AnimationFrameHeight = 36;

            // Placement
            TileObjectData.newTile.CopyFrom(TileObjectData.GetTileData(TileID.Campfire, 0));
            TileObjectData.newTile.StyleLineSkip = 5; // This needs to be added to work for modded tiles.
            TileObjectData.addTile(Type);


            AddMapEntry(new Microsoft.Xna.Framework.Color(60, 60, 60), CreateMapEntryName());
        }

        public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
        {
            // Tweak the frame drawn by x position so tiles next to each other are off-sync and look much more interesting
            int uniqueAnimationFrame = Main.tileFrame[Type] + i;
            if (i % 2 == 0)
                uniqueAnimationFrame += 3;
            if (i % 3 == 0)
                uniqueAnimationFrame += 3;
            if (i % 4 == 0)
                uniqueAnimationFrame += 3;
            uniqueAnimationFrame %= 6;

            frameYOffset = AnimationFrameHeight * Main.tileFrame [type];
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 2.22f;
                g = 1.01f;
            b = 0.34f;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            // We can change frames manually, but since we are just simulating a different tile, we can just use the same value
            frame = Main.tileFrame[TileID.Furnaces];}
        public override void NumDust(int x, int y, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void KillMultiTile(int x, int y, int frameX, int frameY)
        {
        }
    }
}