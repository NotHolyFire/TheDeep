using Terraria.DataStructures;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace TheDeep.Common.SubPlayer
{
    public class DOTPlayer : ModPlayer
    {
        public bool PressureDOT;
        public bool LacerationDOT;

        public override void ResetEffects() {
			PressureDOT = false;
            LacerationDOT = false;
		}

        public override void UpdateBadLifeRegen()
        {
            if(PressureDOT)
            {

                if (Player.lifeRegen > 0)
				Player.lifeRegen = 0;

                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 10;
                if (Player.statLife <= 1)
                {
                    Player.KillMe(PlayerDeathReason.ByCustomReason("" + Player.name + Language.GetTextValue("Mods.SubmergedMod.DeathReason.Pressure")), 9999, 0);
                }
                
            }
            if(LacerationDOT)
            {

                if (Player.lifeRegen > 0)
				Player.lifeRegen = 0;

                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 20;
                if (Player.statLife <= 1)
                {
                    Player.KillMe(PlayerDeathReason.ByCustomReason("" + Player.name + Language.GetTextValue("Mods.SubmergedMod.DeathReason.Laceration")), 9999, 0);
                }
            }
        }
    } //Mais uma vez, vlw Klon pela ajuda com a Death Message
}