using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.GameContent.NetModules;
using Terraria.GameInput;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Net;
using Terraria.WorldBuilding;
using static Terraria.Main;
using static Terraria.ModLoader.ModContent;


namespace TheDeep.Common.SubPlayer
{
    public partial class SubmergedPlayer : ModPlayer
    {
        public int SupermoonSkyShader = 0;
    }
}
