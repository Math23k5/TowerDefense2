using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace Tower_Defense
{
    public abstract class Enemy
    {
        // Variabler/fields
        private Vector2 position;
        //protected float speed;
        //protected int value;
        protected Texture2D sprite;
        private int health;
        public bool isSlowed = false;


        public Enemy()
        {
           // Variabler mangler
           // screenSize = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
           // Spørg på grid width og grid height for placering af enemy
        }

        public int Health { get => health; set => health = value; }
        public Vector2 Position { get => position; set => position = value; }

        public void Move(float speed, Vector2 position)
        {
            // Lav emp static?
            // Positioner på banen til enemies, er position grid eller reel?
            for (int path = 0; path < enemyMovePattern.GetLength(0); path++)
            {
                if ((position.X == enemyMovePattern[path, 0])*grid.width && (position.Y == enemyMovePattern[path, 1])*grid.height)
                {
                    velocity += new Vector2(enemyMovePattern[path + 1, 0] - position.X, enemyMovePattern[path + 1, 1] - position.Y);
                }
            }
          /* spaceshooter
           * protected void Move(GameTime gameTime)
            {
                float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

                position += ((speed * velocity) * deltaTime);
                CollisionBox = new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height);
            }

            public void CheckCollision(GameObject other)
            {
                if (CollisionBox.Intersects(other.CollisionBox))
                {
                    OnCollision(other);
                }
            }

        public virtual void OnCollision(GameObject other)
        {
            color = Color.Red;
        }*/

    }
    


        // Metode til at instantiere content
        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);


    }
}