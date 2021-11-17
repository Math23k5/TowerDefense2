using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace Tower_Defense
{
    public class Sniper : Tower
    {
        private Texture2D bullet;


        public Sniper(Vector2 position) : base(position)
        {
            damage = 50;
        }

        public override void Shoot(GameTime gameTime)
        {
            foreach (Enemy myEnemy in GameWorld.myEnemies)
            {
                distance = (float)Math.Sqrt((Math.Pow(position.X - myEnemy.Position.X, 2) + Math.Pow(position.Y - myEnemy.Position.Y, 2)));
                if (distance <= range)
                {
                    GameWorld.myProjectiles.Add(new Bullet(new Vector2(position.X + sprite.Width / 2, position.Y + sprite.Height / 2), damage, myEnemy, bullet));
                }

            }
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("SniperSprite");
            scale = 0.2f;
            rect = new Rectangle(0, 0, sprite.Width, sprite.Height - 30);
            bullet = content.Load<Texture2D>("Bullet");
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}