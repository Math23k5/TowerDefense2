using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;


namespace Tower_Defense
{
    public abstract class Projectile
    {

        //Variabaler/fields

        public int speed = 10;
        protected Enemy target;
        protected Vector2 position;
        protected bool isActive;

        public Projectile(Enemy target, Vector2 position) 
        {
            this.target = target;
            this.position = position;
        }
        /// <summary>
        /// Skal kontrollerer bevægelsen af projektilet
        /// </summary>
        /// <param name="gameTime"></param>

        public abstract void Move(GameTime gameTime);

         
        /// <summary>
        /// Skal kontrollere damage foretaget af projektilet på Enemy
        /// </summary>
        public abstract void Damage();

        public abstract void OnHit(Enemy other);

        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
    }

}