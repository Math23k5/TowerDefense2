using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace Tower_Defense
{
    public class Standard : Tower
    {
        
        protected Texture2D sprite;

        public Standard()
        {
        }

        public override void Shoot(GameTime gameTime)
        {
            foreach (Enemy myEnemy in GameWorld.myEnemies)
            {
                distance = Math.Sqrt((Math.Pow(position.X - myEnemy.position.X, 2) + Math.Pow(position.Y - myEnemy.position.Y, 2)));
                if(distance <= range)
                {
                    myEnemy.Health -= damage;
                }

            }
        }

        public override void LoadContent(ContentManager content)
        {
            towSprite = content.Load<Texture2D>("StandardSprite");
        }


        public override void Update(GameTime gameTime)
        {

        }
    }
}