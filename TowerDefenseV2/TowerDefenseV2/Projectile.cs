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

        // 1. Projektile klassen er abstract, metodernes funktionalitet bliver derfor defineret af subklasserne: Bomb & bullet
        // 2. Ting som er generelle for begge typer af projektil, skal forelægge indenfor denne klasse
        // 3. Dette kan være ting som speed, target finding mm. 

        //Variabaler/fields

        public int speed = 10;
        protected Enemy target;
        protected Vector2 position;
        protected bool isActive;
        protected Projectile Bullet;
        protected Rectangle rec { get
            {
                return new Rectangle((int)position.X, (int) position.Y, sprite.Width, sprite.Height);
            } }
        protected Texture2D sprite;

        public Projectile(Enemy target, Vector2 position, Texture2D sprite)
        {
            this.target = target;
            this.position = position;
            this.sprite = sprite;
        }
        /// <summary>
        /// Skal kontrollerer bevægelsen af projektilet
        /// </summary>
        /// <param name="gameTime"></param>

        public abstract void Move(GameTime gameTime);

        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Is executed everytime a collision occurs
        /// </summary>
        /// <param name="other">The Object we collided with</param>
        public abstract void OnCollision(Enemy other);


        public void CheckCollision(Enemy other)
        {
            if (rec.Intersects(other.CollisionBox))
            {
                OnCollision(other);
            }
        }



    }

}