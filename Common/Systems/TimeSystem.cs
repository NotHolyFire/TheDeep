using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDeep.Common.Systems
{

    public sealed class TimeSystem : ModSystem
    {

        public delegate void TimeDelegate(bool day);

        public static event TimeDelegate TimeChanged;

        private bool _wasDayTime;

        public override void OnWorldLoad() => _wasDayTime = Main.dayTime;

        public override void PostUpdateEverything()
        {
            if (Main.dayTime != _wasDayTime)
                TimeChanged?.Invoke(Main.dayTime);

            _wasDayTime = Main.dayTime;
        }

        public override void Unload() => TimeChanged = null;
    }
}
