using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using static TheDeep.Common.Skies.SkyloadDict;


namespace TheDeep.Common.Skies
{
    public abstract class Skyload : CustomSky, ILoadable // 14/03/2026, third day trying to make ts work - Azure
    {
        internal bool _isActive;
        public float _fadeOpacity {get; set;}
        internal virtual bool DisablesSunAndMoon{get; set;}  = false;
        internal virtual float FadeSpeed {get; set;} = 0.01f;
        public void Load(Mod mod)
        {
            string key = mod.Name + ":" + GetType().Name;
            SkyManager.Instance[mod.Name + ":" + GetType().Name] = (CustomSky)Activator.CreateInstance(GetType());
            LoadedSkies.Add(key, new Func<Player, bool>(ActivationCondition));
        }

        public void Unload() { }
        
public override void Update(GameTime gameTime)
        {
            if (_isActive)
            {
                _fadeOpacity = Math.Min(1f, 0.01f + _fadeOpacity);
            }
            else
            {
                _fadeOpacity = Math.Max(0f, _fadeOpacity - 0.01f);
            }
        }
                public override void Activate(Vector2 position, params object[] args)
        {
            _isActive = true;
            OnActivate(args);
        }

                public override void Deactivate(params object[] args)
        {
            _isActive = false;
            OnDeactivate(args);
        }
                public override void Reset()
        {
            _isActive = false;
            OnReset();
        }
                public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if(maxDepth < float.MaxValue)
            return;

            DoDraw(spriteBatch);

        }
        public override bool IsActive() => _isActive || _fadeOpacity > 0;

        internal virtual void OnActivate(params object[] args) { }
        internal virtual void OnDeactivate(params object[] args) { }
        internal virtual void OnReset(){ }
        internal virtual void DoDraw(SpriteBatch spriteBatch) { }
        internal virtual void OnUpdate(GameTime gameTime) { }
        internal abstract bool ActivationCondition(Player p);


        /*public override Color OnTileColor(Color inColor)
        {
            return Color.Lerp(inColor, new Color(63, 51, 90, inColor.A), _fadeOpacity);
        }

        public override void OnLoad()
        {
            _bgTexture = ModContent.Request<Texture2D>("SubmergedMod/Skies/Skyload").Value;
        }
        public override float GetCloudAlpha()
        {
            return (1f - _fadeOpacity) * 0.3f + 0.7f;
        }*/ //Later
    }
}
