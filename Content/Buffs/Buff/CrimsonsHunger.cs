
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDeep.Content.Buffs.Buff
{
    public class CrimsonsHunger : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = false; 
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<CrimsonRegenBuff>().CrimsonRegen = true;
        }
        }
        public class CrimsonRegenBuff : ModPlayer
    {
        public bool CrimsonRegen;
        public override void ResetEffects()
        {
            CrimsonRegen = false;
        }
        public override void UpdateLifeRegen()
        {
            if (CrimsonRegen)
            {
                Player player = Main.LocalPlayer;
                
                player.lifeRegen += 10;
                for (int i = 0; i < 20; i++)
                {
                	Vector2 velocity = Vector2.One.RotatedBy(MathHelper.TwoPi * (i / 10f));
                    if (Main.rand.NextBool(20))
                	    Dust.NewDustPerfect(player.MountedCenter, DustID.Blood, velocity * 2f, 0, default, 0.75f).noGravity = true;
                }
            }
        }
    }
}