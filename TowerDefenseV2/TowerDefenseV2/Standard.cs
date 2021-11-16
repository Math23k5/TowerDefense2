﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace Tower_Defense
{
    public class Standard : Tower
    {
        
        protected Texture2D sprite;


        public Standard(Vector2 position) : base(position)
        {
        }

        public override void Shoot(GameTime gameTime)
        {
            foreach (Enemy myEnemy in GameWorld.myEnemies)
            {
                distance = (float)Math.Sqrt((Math.Pow(position.X - myEnemy.Position.X, 2) + Math.Pow(position.Y - myEnemy.Position.Y, 2)));
                if(distance <= range)
                {
                    GameWorld.myProjectiles.Add(new Bullet(new Vector2(position.X + sprite.Width / 2, position.Y + sprite.Height / 2), damage));
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