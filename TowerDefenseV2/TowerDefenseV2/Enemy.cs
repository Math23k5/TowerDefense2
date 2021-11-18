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
        protected int value;
        protected Texture2D sprite;
        private int health;
        public bool isSlowed = false;
        private Vector2 velocity;
        private Rectangle collisionBox;

        protected Rectangle rect;
        private Vector2 origin = Vector2.Zero;
        protected float scale = 1.0f;
        private SpriteEffects effect;

        protected int mapWidth;
        protected int mapHeight;
        private int path = 0;
        protected bool isActive;
        private Vector2 worldPos;

        public int Health { get => health; set => health = value; } //Used to change the health value of enemies
        public Vector2 Position { get => position; set => position = value; } //Used to change the position of enemies
        public Vector2 WorldPos { get => worldPos; } //Used to check for the enemys position in the game
        public float Scale { get => scale; } //Used to get the scale of each enemy
        public bool IsActive { get => isActive; } //Used to get the activity status of each enemy
        public Texture2D Sprite { get => sprite; } //Used to get the sprite of each enemy
        public Rectangle CollisionBox { get => collisionBox; set => collisionBox = value; } //Used to set the collisionbox on each enemy
        protected int Value { get => value; set => this.value = value; } //Used to change the value on each enemy
        public float NormalSpeed { get => normalSpeed; set => normalSpeed = value; } //Used to set the speed of enemies back to normal

        /// <summary>
        /// Used to call enemy in GameWorld
        /// </summary>
        public Enemy()
        {
           // Variabler mangler
           // screenSize = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
           // Spørg på grid width og grid height for placering af enemy
           // gridwidth = screensize.width / 30(grids)
           // gridheight = screensize.height / 22(grids)

        //private int[,] enemyMovePattern = new int[,] { { 4, -1 }, { 4, 4 }, { 11, 4 }, { 11, 2 }, {20, 2 }, {20 , 5 }, {26 , 5}, {;
    }

        /// <summary>
        /// Loads initial content from sub-classes
        /// </summary>
        /// <param name="content">Takes ContentManager to look at content in content file</param>
        public abstract void LoadContent(ContentManager content);

        /// <summary>
        /// Updates the movement and speed of the enemies while also setting collionboxes
        /// </summary>
        /// <param name="gameTime">Takes a GameTime that provides the timespan since last call to update</param>
        /// <param name="graphics">Takes GraphicsDeviceManager that allow access to get screensize</param>
        public void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            if (isActive == true)
            {
                // Lav emp static?
                // Positioner på banen til enemies, er position grid eller reel?
                //for (int path = 0; path < GameWorld.enemyMovePattern.GetLength(0); path++)
                //{
                if (GameWorld.enemyMovePattern[path, 0] * 50 - position.X < 1.0f && GameWorld.enemyMovePattern[path, 0] * 50 - position.X > -1.0f && GameWorld.enemyMovePattern[path, 1] * 50 - position.Y < 1.0f && GameWorld.enemyMovePattern[path, 1] * 50 - position.Y > -1.0f)
                {
                    path++;
                    if (path >= GameWorld.enemyMovePattern.Length / 2)
                    {
                        GameWorld.playerHealth--;
                        isActive = false;
                    }
                    else
                    {
                        velocity = new Vector2(GameWorld.enemyMovePattern[path, 0] * 50 - position.X, GameWorld.enemyMovePattern[path, 1] * 50 - position.Y);
                        velocity.Normalize();
                    }
                }
                //}
                if (isActive == true)
                {
                    //velocity = new Vector2(GameWorld.enemyMovePattern[path, 0] * 50 - position.X, GameWorld.enemyMovePattern[path, 1] * 50 - position.Y);

                    if (isSlowed == true)
                    {
                        speed = NormalSpeed * 0.8f;
                    }
                    else
                    {
                        speed = NormalSpeed;
                    }

                    float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

                    position += (speed * velocity) * deltaTime;
                    CollisionBox = new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height);
                }
            }
        }

        /// <summary>
        /// Used to draw the visual picture of the enemy based on their position
        /// </summary>
        /// <param name="spriteBatch">Takes a SpriteBatch that contains the enemy sprites</param>
        /// <param name="graphics">Takes GraphicsDeviceManager that allow access to get screensize</param>
        public void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            if (isActive == true)
            {
                worldPos = new Vector2(position.X + (graphics.GraphicsDevice.Viewport.Width - (mapWidth * 50)) / 2 - (sprite.Width * scale) / 2 + 50, position.Y + (graphics.GraphicsDevice.Viewport.Height - (mapHeight * 50)) / 2 - (sprite.Height * scale) / 2 + 50);
                spriteBatch.Draw(sprite, worldPos, rect, Color.White, 0.0f, origin, scale, effect, 1.0f);
            }
        }

        /// <summary>
        /// Adds gold when an enemy die based on it's value
        /// </summary>
        public void Death()
        {
            GameWorld.gold += Value;
            isActive = false;
        }

        /// <summary>
        /// Calls for the enemy to take damage based on projectiles damage
        /// </summary>
        /// <param name="damage">Takes an int damage to subtract from enemy health</param>
        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Death();
            }
        }

        //public void CheckCollision(/*Freezer, Projectile, Bomb*/ other)
        //{
        //    if (CollisionBox.Intersects(other.CollisionBox))
        //    {
        //        OnCollision(other);
        //    }
        //}

        //public virtual void OnCollision(/*Freezer, Projectile, Bomb*/ other)
        //{
        //    // if(type freezer)
        //    // isSlowed = true;

        //    // if(projectile || bomb)
        //    // TakeDamage();
        //}
    

    }
}