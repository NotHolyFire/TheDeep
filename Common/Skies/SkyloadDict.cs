using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using static TheDeep.Common.Skies.SkyloadDict;

namespace TheDeep.Common.Skies;

public static class SkyloadDict
{
    public static IDictionary<string, Func<Player, bool>> LoadedSkies {get; set;} = new Dictionary<string, Func<Player, bool>>();
}

internal class UnloadDict : ILoadable
{
    public void Load(Mod mod) { }
    
    public void Unload() => LoadedSkies = new Dictionary<string, Func<Player, bool>>();
}