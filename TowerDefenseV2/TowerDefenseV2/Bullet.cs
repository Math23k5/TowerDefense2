using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Linq;

namespace Tower_Defense
{
    class Bullet : Projectile
    {


        // 1. Bullet skal kunne spawne på towers position
        // 2. Bullet skal kunne bevæge sig mod fjender, fra towers positition
        // 3. Bullet skal leverer damage (præcis damage ligger på tower) på impact. Bullet har sin egen damage parameter, som indhentes fra tower
        // 4. Bullet skal kunne fjernes fra spillet efter collision, for at sparer ressourcer
        // 5. Eventuelt genbruge bullet, så man sparer ressourcer.

        public Texture2D texture;
        
        public Vector2 movement;
        public Vector2 velocity;
        public int damage;
        

        


        public Bullet(Vector2 position, int damage, Enemy target, Texture2D sprite) : base(target, position, sprite)
        {
            
            this.position = position;
            this.damage = damage;

            
            
        }

        
        public override void LoadContent(ContentManager content)
        {
            content.Load<Texture2D>("Reddot");

            
        }

        public override void Update(GameTime gameTime)
        {

           


        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            //Hvis kuglen er aktiv, så skal den tegnes
            if (isActive == true)
            {
                spriteBatch.Draw(texture, rec, Color.White);
            }

            spriteBatch.End();



        }

        public override void Move(GameTime gameTime)
        {
            
            velocity = target.Position - position;
            velocity.Normalize();

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            position += ((speed * velocity) * deltaTime);

        }

        public override void OnCollision(Enemy other)
        {
            if (other is Enemy)
            {
                other.Health -= damage;
                GameWorld.myProjectiles.Remove(this);
            }
        }

       

    }


}