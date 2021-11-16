using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace Tower_Defense
{
    public class Sniper : Tower
    {
        protected Texture2D sprite;

        public Sniper()
        {
        }

        public override void Shoot(GameTime gameTime)
        {

        }

        public override void LoadContent(ContentManager content)
        {
            towSprite = content.Load<Texture2D>("SniperSprite");
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}