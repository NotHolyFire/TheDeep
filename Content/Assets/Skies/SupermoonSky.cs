using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria;
using TheDeep.Common.Systems;

namespace TheDeep.Content.Assets.Skies;

public class SupermoonSky : CustomSky
{

    public Texture2D textureBG;

    public bool active;

    public float opacity;

    public override void OnLoad()
    {
        textureBG = ModContent.Request<Texture2D>("Terraria/Images/Misc/StarDustSky/Background", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
    }

    public override void Update(GameTime gameTime)
    {
        if (active)
        {
            opacity = Math.Min(1f, 0.01f + opacity);
        }
        else
        {
            opacity = Math.Max(0f, opacity - 0.01f);
        }
    }

    public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
    {
        if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
        {
            spriteBatch.Draw(textureBG, new Rectangle(0, Math.Max(0, (int)((Main.worldSurface * 16.0 - (double)Main.screenPosition.Y - 700.0) * 0.10000000149011612)), Main.screenWidth, Main.screenHeight), new Color(94, 255, 250, 240) * Math.Min(1f, (Main.screenPosition.Y - 800f) / 1000f * opacity));
        }
    }

    public override float GetCloudAlpha()
    {
        return (1f - opacity) * 0.3f + 0.7f;
    }
    public override void Activate(Vector2 position, params object[] args)
    {
       active = Supermoon.SuperMoon = true;
    }
    public override void Deactivate(params object[] args)
    {
        active = Supermoon.SuperMoon = false;
    }

    public override void Reset()
    {
        active = false;
    }

    public override bool IsActive()
    {
        if (!active)
        {
            return opacity > 0.001f;
        }
        return true;
    }
}
