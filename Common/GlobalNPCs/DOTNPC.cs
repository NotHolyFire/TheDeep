using Terraria;
using Terraria.ModLoader;

namespace TheDeep.Common.GlobalNPCs
{
    public class DOTNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool PressureDOTNpc;
        public bool LacerationDOTNpc;

        public override void ResetEffects(NPC npc)
        {
			PressureDOTNpc = false;
            LacerationDOTNpc = false;
		}

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (PressureDOTNpc)
            {
			if (npc.lifeRegen > 0)
            {
				npc.lifeRegen = 0;
			}
            npc.lifeRegen -=20; // 10 DoT
            }
            if (LacerationDOTNpc)
            {
			if (npc.lifeRegen > 0)
            {
				npc.lifeRegen = 0;
			}
            npc.lifeRegen -= 10; // 10 DoT
            }
        }
    }
}
