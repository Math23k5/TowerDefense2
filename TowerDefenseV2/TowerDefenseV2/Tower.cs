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

        protected Texture2D towSprite;
        protected Texture2D bulSprite;
        protected Texture2D bombSprite;

        protected float distance;
        protected Vector2 position;
        public bool isShooting = false;


        public Tower(Vector2 position, int damage = 0)
        {
            this.position = position;
            this.damage = damage;
        }

        public abstract void Shoot(GameTime gameTime);


        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(towSprite, position, Color.White);
            
        }



        public abstract void LoadContent(ContentManager content);



        public abstract void Update(GameTime gameTime);
    }
}