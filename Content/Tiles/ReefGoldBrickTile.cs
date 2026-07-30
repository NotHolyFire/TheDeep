

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDeep.Content.Tiles
{
    public class ReefGoldBrickTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileShine[Type] = 1500;
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;

            AddMapEntry(new Color(161, 139, 46));

            DustType = DustID.Gold;
            HitSound = SoundID.Tink;
        }
    }
}