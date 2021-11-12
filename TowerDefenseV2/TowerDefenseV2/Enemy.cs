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
        protected float speed;
        protected int value;
        protected Texture2D sprite;
        private int health;
        public bool isSlowed = false;



        public Enemy()
        {

        }


        public int Health { get => health; set => health = value; }
        public Vector2 Position { get => position; set => position = value; }


        public void Move(float speed, Vector2 position)
        {
            
        }

        // Metode til at instantiere content
        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);


    }
}