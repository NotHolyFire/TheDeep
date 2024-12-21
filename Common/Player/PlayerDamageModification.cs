
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDeep.Common.Players
{
    internal class PlayerDamageModification : ModPlayer
    {
        public bool StunDebuff;


        public override void ResetEffects()
        {
            StunDebuff = false;
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {

            if (StunDebuff)
            {
                // These color adjustments match the withered armor debuff visuals.
                g *= 0.5f;
                r *= 0.75f;
            }
        }
    }
}