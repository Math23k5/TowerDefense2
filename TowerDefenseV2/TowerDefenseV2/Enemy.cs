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
        private float speed;
        private float normalSpeed;
        private int value;
        protected Texture2D sprite;
        private int health;
        public bool isSlowed = false;
        private Vector2 velocity;
        private Rectangle collisionBox;

        public int Health { get => health; set => health = value; }
        public Vector2 Position { get => position; set => position = value; }
        public Rectangle CollisionBox { get => collisionBox; set => collisionBox = value; }
        protected int Value { get => value; set => this.value = value; }
        public float NormalSpeed { get => normalSpeed; set => normalSpeed = value; }

        public Enemy()
        {
           // Variabler mangler
           // screenSize = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
           // Spørg på grid width og grid height for placering af enemy
           // gridwidth = screensize.width / 30(grids)
           // gridheight = screensize.height / 22(grids)

        //private int[,] enemyMovePattern = new int[,] { { 4, -1 }, { 4, 4 }, { 11, 4 }, { 11, 2 }, {20, 2 }, {20 , 5 }, {26 , 5}, {;
    }
        // Metode til at instantiere content
        public abstract void LoadContent(ContentManager content);

        public void Update(GameTime gameTime)
        {
            // Lav emp static?
            // Positioner på banen til enemies, er position grid eller reel?
            for (int path = 0; path < GameWorld.enemyMovePattern.GetLength(0); path++)
            {
                if ((position.X == GameWorld.enemyMovePattern[path, 0] * 50) && (position.Y == GameWorld.enemyMovePattern[path, 1] * 50))
                {
                    velocity += new Vector2(GameWorld.enemyMovePattern[path + 1, 0] - position.X, GameWorld.enemyMovePattern[path + 1, 1] - position.Y);
                }
            }

            if (isSlowed == true)
            {
                speed = NormalSpeed * 0.8f;
            }
            else
            {
                speed = NormalSpeed;
            }

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            position += ((speed * velocity) * deltaTime);
            CollisionBox = new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, Position, Color.White);
        }
        public void Death()
        {
            GameWorld.gold += Value;
        }

        //public void TakeDamage(int damage)
        //{
        //    Health -= damage;
        //    if (Health <= 0)
        //    {
        //        Death();
        //    }
        //}

        public void CheckCollision(/*Freezer, Projectile, Bomb*/ other)
        {
            if (CollisionBox.Intersects(other.CollisionBox))
            {
                OnCollision(other);
            }
        }

        public virtual void OnCollision(/*Freezer, Projectile, Bomb*/ other)
        {
            // if(type freezer)
            // isSlowed = true;

            // if(projectile || bomb)
            // TakeDamage();
        }
    

    }
}