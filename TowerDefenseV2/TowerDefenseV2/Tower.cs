using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Tower_Defense
{
    public abstract class Tower
    {

        protected int damage;
        protected float range;
        protected int cost;
        protected float rateOfFire;

        protected Texture2D sprite;

        protected float distance;
        protected Vector2 position;

        //bliver ikke brugt
        public bool isShooting = false;

        protected float timeSinceShot = 0.0f;

        protected Rectangle rect;
        protected Vector2 origin = Vector2.Zero;
        protected float scale = 1.0f;
        private SpriteEffects effect;


        public Tower(Vector2 position)
        {
            this.position = position;
        }

       
        public abstract void Shoot(GameTime gameTime);


        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(sprite, new Vector2(position.X - (sprite.Width * scale)/2 + 25, position.Y - (sprite.Height * scale)/2 + 25), rect, Color.White, 0.0f, origin, scale, effect, 1.0f);
            
        }

        public abstract void LoadContent(ContentManager content);



        public abstract void Update(GameTime gameTime);
    }
}