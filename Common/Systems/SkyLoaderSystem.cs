using TheDeep.Common.Skies.SupermoonSky;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace TheDeep.Common.Systems
{
    [Autoload(Side = ModSide.Client)]
    internal sealed class SkyLoaderSystem : ModSystem
    {
        public override void Load()
        {
            SkyManager.Instance["SubmergedMod:SuperMoon"] = new SupermoonSky();
        }
    }
}
