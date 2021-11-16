using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace Tower_Defense
{
    public class Freezer : Tower
    {
        protected Texture2D sprite;
        protected int frzDamage;
        protected float frzRange;
        protected int frzCost;
        protected float frzRateOfFire;

        
        public Freezer(Vector2 position) : base(position)
        {
        }

        public override void Shoot(GameTime gameTime)
        {
            foreach (Enemy myEnemy in GameWorld.myEnemies)
            {
                distance = (float)Math.Sqrt((Math.Pow(position.X - myEnemy.Position.X, 2) + Math.Pow(position.Y - myEnemy.Position.Y, 2)));
                if (distance <= range && myEnemy.Health < 0)
                {
                    myEnemy.isSlowed = true;
                }

            }
        }

        public override void LoadContent(ContentManager content)
        {
            towSprite = content.Load<Texture2D>("FreezerSprite");
        }


        public override void Update(GameTime gameTime)
        {

        }

    }
}