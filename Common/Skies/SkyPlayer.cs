using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace TheDeep.Common.Skies;

public class Skyplayer : ModPlayer
{
    public void PostUdateMiscEffects()
    {
        if (Main.dedServ || Player.whoAmI != Main.myPlayer)
            return;

        foreach (string key in SkyloadDict.LoadedSkies.Keys)
        {
            bool skyIsActive = SkyloadDict.LoadedSkies[key](Player);

            if (skyIsActive)
                SkyManager.Instance.Activate(key);
            else if (SkyManager.Instance[key].IsActive())
                SkyManager.Instance.Deactivate(key);
        }
    }
}