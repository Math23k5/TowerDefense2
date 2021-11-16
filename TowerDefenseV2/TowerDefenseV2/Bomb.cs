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
        Vector2 destination;
        int damage;
        int areaofeffect;
        Vector2 direction;
        Action<int, Vector2> damageFunc;
        public Vector2 velocity;
        private Vector2 Position;

        public Bomb(Vector2 position, Texture2D tex, Vector2 dest, List<Enemy> myEnemies, int damage, int areaofeffect, Action<int, Point> damageFunc)
        {
            this.destination = dest;
            this.Tex = tex;
            this.damage = damage;
            this.areaofeffect = areaofeffect;
            this.myEnemies = myEnemies;
            this.damageFunc = damageFunc;

            

            direction = (position - dest).ToVector2();
            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }
        }

        public override void Move(GameTime gameTime)
        {
            velocity = position - target.Position;
            velocity.Normalize();

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            position += ((speed * velocity) * deltaTime);


        }

        public override void Damage()
        {
            foreach (Enemy e in GameWorld.myEnemies)
            {
                if ((int)Math.Sqrt(Math.Pow(this.Position.X - e.Position.X, 2) + Math.Pow(this.Position.Y - e.Position.Y, 2)) <= areaofeffect)
                {
                    damageFunc(damage, e.Position);
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

        public override 
        
    }
}