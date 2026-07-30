using TheDeep.Common.Systems;
using Terraria.Graphics.Effects;
using TheDeep.Common.Skies;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Utilities;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.GameContent;

namespace TheDeep.Common.Skies.SupermoonSky
{

    public class SupermoonSky : CustomSky
    {
        public bool Active;
        public float opacity;
        public float skyopacity;

        public override Color OnTileColor(Color inColor)
        {
            return Color.Lerp(inColor, new Color(63, 51, 90, inColor.A), 1f);
        }
        private Color skycolor;
        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            Texture2D skyTex = ModContent.Request<Texture2D>("TheDeep/Common/Skies/SupermoonSky/SupermoonSky").Value;
            Texture2D MoonTex = ModContent.Request<Texture2D>("TheDeep/Common/Skies/SupermoonSky/Supermoon").Value;

            Vector2 skyPos = new(Main.screenWidth / 2, Main.screenHeight / 2);
            Rectangle rect = new(0, 0, Main.screenWidth, Main.screenHeight);

            float goofyAhhVar = 3.40282347E+38f; //wtf is this number????
            if (maxDepth >= goofyAhhVar && minDepth < goofyAhhVar)
            {

                Vector2 screenCenter = Main.screenPosition + new Vector2(Main.screenWidth, Main.screenHeight) * 0.5f;
                Vector2 drawWorldPos = new Vector2(Main.LocalPlayer.Center.X, 20f);
                Vector2 drawPos = (drawWorldPos - screenCenter) * 0.017f + screenCenter - Main.screenPosition - Vector2.UnitY * 2f;

                Vector2 MoonOrigin = MoonTex.Size() * 0.5f;

                spriteBatch.Draw(TextureAssets.BlackTile.Value, rect, new Color(0, 0, 15) * opacity);
                spriteBatch.Draw(skyTex, rect, Color.White * opacity);

                if (!Main.dedServ)
                {
                    int bgTop = (int)((-Main.screenPosition.Y) / (Main.worldSurface * 16.0 - 600.0) * 200.0);
                    float colorMult = 0.952f * opacity;
                    float width1 = Main.screenWidth / 500f;
                    float height1 = Main.screenHeight / 600f;
                    float width2 = Main.screenWidth / 600f;
                    float height2 = Main.screenHeight / 800f;
                    float width3 = Main.screenWidth / 200f;
                    float height3 = Main.screenHeight / 900f;
                    float width4 = Main.screenWidth / 1000f;
                    float height4 = Main.screenHeight / 200f;
                    for (int i = 0; i < Main.star.Length; i++)
                    {
                        Star star = Main.star[i];
                        if (star == null)
                            continue;

                        Texture2D t2D = TextureAssets.Star[star.type].Value;
                        Vector2 origin = new Vector2(t2D.Width * 0.5f, t2D.Height * 0.5f);
                        float posX = star.position.X * width1;
                        float posY = star.position.Y * height1;
                        Vector2 position = new Vector2(posX + origin.X, posY + origin.Y + bgTop);
                        spriteBatch.Draw(t2D, position, new Rectangle(0, 0, t2D.Width, t2D.Height), Color.White * star.twinkle * colorMult, star.rotation, origin, (star.scale * star.twinkle) - 0.2f, SpriteEffects.None, 0f);

                        origin = new Vector2(t2D.Width * 0.2f, t2D.Height * 0.2f);
                        posX = star.position.X * width2;
                        posY = star.position.Y * height2;
                        position = new Vector2(posX + origin.X, posY + origin.Y + bgTop);
                        spriteBatch.Draw(t2D, position, new Rectangle(0, 0, t2D.Width, t2D.Height), Color.LightBlue * star.twinkle * colorMult, star.rotation, origin, (star.scale * star.twinkle) + 0.2f, SpriteEffects.None, 0f);

                        origin = new Vector2(t2D.Width * 0.8f, t2D.Height * 0.8f);
                        posX = star.position.X * width3;
                        posY = star.position.Y * height3;
                        position = new Vector2(posX + origin.X, posY + origin.Y + bgTop);
                        spriteBatch.Draw(t2D, position, new Rectangle(0, 0, t2D.Width, t2D.Height), Color.White * star.twinkle * colorMult, star.rotation, origin, star.scale * star.twinkle, SpriteEffects.None, 0f);

                        origin = new Vector2(t2D.Width * 0.5f, t2D.Height * 0.5f);
                        posX = star.position.X * width4;
                        posY = star.position.Y * height4;
                        position = new Vector2(posX + origin.X, posY + origin.Y + bgTop);
                        spriteBatch.Draw(t2D, position, new Rectangle(0, 0, t2D.Width, t2D.Height), Color.White * star.twinkle * colorMult, star.rotation, origin, star.scale * star.twinkle, SpriteEffects.None, 0f);
                    }
                }
                spriteBatch.Draw(MoonTex, drawPos, null, Color.White * opacity, 0f, MoonOrigin, 1f, SpriteEffects.None, 0f);
            }

        }
        public override void Update(GameTime gameTime)
        {
            if (!Supermoon.SuperMoon || Main.gameMenu || Main.dayTime)
                Active = false;

            if (Active && opacity < 1f)
                opacity += 0.02f;
            else if (!Active && opacity > 0f)
                opacity -= 0.02f;

            Opacity = opacity;
        }
        public override float GetCloudAlpha()
        {
            return (1f - opacity) * 0.97f + 0.03f;
        }
        private readonly UnifiedRandom _random = new();

        public override void Activate(Vector2 position, params object[] args)
        {
            Active = true;
        }
        public override void Deactivate(params object[] args)
        {
            Active = (Supermoon.SuperMoon && !Main.dayTime);
        }
        public override void Reset()
        {
            Active = false;
        }
        public override bool IsActive()
        {
            return Active || opacity > 0f;
        }
    }
} // idk man, maybe later, I have no ideia of what I am doing
