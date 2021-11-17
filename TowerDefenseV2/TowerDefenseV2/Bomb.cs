using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tower_Defense
{
    class Bomb : Projectile
    {

        // 1. Bomben skal kunne spawne fra Bombtower positionen (nok origin)
        // 2. Bomben skal kunne target fjender fra start position
        // 3. Når bomben kolliderer med fjender, så skal bomben ekploderer og lave Aoe damage (Damage forelægger på tower?) (sprite animation)
        // 4. Bomben skal kunne fjernes fra spillet efter animation, for at sparer ressourcer
        // 5. Bomben skal genbruges?
        
        int damage;
        int areaofeffect;
        Vector2 direction;
        public Vector2 velocity;
        private Vector2 Position;

        public Bomb(Vector2 position, Texture2D sprite, Vector2 dest, int damage, int areaofeffect) : base(null, position, sprite)
        {
            
            this.damage = damage;
            this.areaofeffect = areaofeffect;





            direction = (position - dest);
            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }
        }

        public override void Move(GameTime gameTime)
        {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            position += ((speed * direction) * deltaTime);


        }



        public void Damage()
        {
            foreach (Enemy e in GameWorld.myEnemies)
            {
                if ((int)Math.Sqrt(Math.Pow(this.Position.X - e.Position.X, 2) + Math.Pow(this.Position.Y - e.Position.Y, 2)) <= areaofeffect)
                {
                    
                    e.TakeDamage(damage);
                }
            }
        }
        public override void OnCollision(Enemy other)
        {
            if (other is Enemy)
            {
                Damage();
            }
        }

        public override void LoadContent(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {
            //Spawne spriten
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Spawne spriten

            //Lav Animate metode til at animere bombe
        }

        
        
    }
}