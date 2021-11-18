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
            isActive = true;
            speed = 300;
            scale = 0.3f;
            rect = new Rectangle(0, 0, sprite.Width, sprite.Height);
        }

        
        public override void LoadContent(ContentManager content)
        {
            //sprite = content.Load<Texture2D>("Reddot");
        }

        public override void Update(GameTime gameTime)
        {
            if (isActive == true )
            {
                Move(gameTime);
            }
            if(target.IsActive != true)
            {
                isActive = false;
            }
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            //Hvis kuglen er aktiv, så skal den tegnes
            if (isActive == true)
            {
                spriteBatch.Draw(sprite, new Vector2(position.X + sprite.Width / 2, position.Y + sprite.Height / 2), rect, Color.White, 0.0f, origin, scale, effects, 1.0f);
            }
        }

        public override void Move(GameTime gameTime)
        {
            velocity = new Vector2(target.WorldPos.X + (target.Sprite.Width * target.Scale) / 2, target.WorldPos.Y + (target.Sprite.Height * target.Scale) / 2) - new Vector2(position.X + sprite.Width / 2, position.Y + sprite.Height / 2);
            if (velocity.X < 5.0f && velocity.X > -5.0f && velocity.Y < 5.0f && velocity.Y > -5.0f)
            {
                target.TakeDamage(damage);
                isActive = false;
            }
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