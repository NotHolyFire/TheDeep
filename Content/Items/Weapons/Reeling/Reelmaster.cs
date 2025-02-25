using Terraria;
using Terraria.ModLoader;

namespace TheDeep.Content.Weapons.Reeling
{

	public class Reeling : DamageClass
	{
        internal static Reeling Instance;

        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
		{

			if (damageClass == DamageClass.Generic)
				return StatInheritanceData.Full;

			return StatInheritanceData.None;
		}

		public override bool GetEffectInheritance(DamageClass damageClass)
		{
			if (damageClass == DamageClass.Melee)
				return true;

			return false;
		}

		public override void SetDefaultStats(Player player)
		{
			player.GetArmorPenetration<Reeling>() = 4;
		}

		public override bool UseStandardCritCalcs => true;
	}
}