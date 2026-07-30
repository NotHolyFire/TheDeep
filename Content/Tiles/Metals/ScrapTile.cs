using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Biomes.CaveHouse;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace TheDeep.Content.Tiles.Metals
{
    public class ScrapTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileShine2[Type] = true; // Modifies the draw color slightly.
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(84, 77, 91), name);

            DustType = 84;
            HitSound = SoundID.Tink;
            // MineResist = 4f;
            // MinPick = 200;
        }
    }
}