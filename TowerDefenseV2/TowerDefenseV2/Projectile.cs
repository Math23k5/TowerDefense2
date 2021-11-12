﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;


namespace Tower_Defense
{
    public abstract class Projectile
    {
        public int speed = 10;
        protected Enemy target;
        protected Vector2 position;
        protected bool isActive;

        public Projectile(Enemy target, Vector2 position) 
        {
            this.target = target;
            this.position = position;
        }

        public abstract void Move(GameTime gameTime);

            
        public abstract void Damage();

        public abstract void OnHit(Enemy other);

        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
    }

}