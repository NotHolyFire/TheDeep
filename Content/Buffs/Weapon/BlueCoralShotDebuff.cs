using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TheDeep.Common.GlobalNPCs;

namespace TheDeep.Content.Buffs.Weapon
{
    public class BlueCoralShotDebuff : ModBuff
    {
            public override void SetStaticDefaults()
            {
                // NPCs will automatically be immune to this buff if they are immune to BoneJavelin. SkeletronHead and SkeletronPrime are immune to BoneJavelin.
                BuffID.Sets.GrantImmunityWith[Type].Add(BuffID.BoneJavelin);
            }

            public override void Update(NPC npc, ref int buffIndex)
            {
            npc.GetGlobalNPC<BlueCoralShotDOT>().BlueCoralShotDebuff = true;
            }
        }
    }