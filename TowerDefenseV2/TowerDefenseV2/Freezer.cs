using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace Tower_Defense
{
    public class Freezer : Tower
    {
        protected int frzDamage;
        protected float frzRange;
        protected int frzCost;
        protected float frzRateOfFire;
        
        public Freezer()
        {
        }

        public override void Shoot(GameTime gameTime)
        {

        }


        public override void Draw(SpriteBatch spriteBatch)
        {

        }


        public override void LoadContent(ContentManager content)
        {
            towSprite = content.Load<Texture2D>("FreezerTower");
        }


        public override void Update(GameTime gameTime)
        {

        }

    }
}