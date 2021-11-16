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
        protected Projectile Bullet;
        protected List<Projectile> Bullets;

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

        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Is executed everytime a collision occurs
        /// </summary>
        /// <param name="other">The Object we collided with</param>
        public abstract void OnCollision(Enemy other);

        public virtual Rectangle GetCollisionBox()
        {

        }
        

        

        public void CheckCollision(Enemy other)
        {
            if (CollisionBox.Intersects(other.CollisionBox))
            {
                OnCollision(other);
            }
        }

        

    }

}