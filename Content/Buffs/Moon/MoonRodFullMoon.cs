using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDeep.Content.Buffs.Moon
{
    // This class serves as an example of a debuff that causes constant loss of life
    // See ExampleLifeRegenDebuffPlayer.UpdateBadLifeRegen at the end of the file for more information
    public class MoonRodFullMoon : ModBuff
    {

        public override void Update(Player player, ref int buffIndex)
        {
            player.calmed = true;
        }
    }
}