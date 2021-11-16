﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tower_Defense
{
    class Bomb : Projectile
    {
        Vector2 destination;
        int damage;
        int areaofeffect;
        List<Enemy> enemylist;
        Vector2 direction;
        Action<int, Vector2> damageFunc;
        public Vector2 velocity;
        private Vector2 Position;

        public Bomb(Vector2 position, Texture2D tex, Vector2 dest, List<Enemy> enemylist, int damage, int areaofeffect, Action<int, Point> damageFunc)
        {
            this.destination = dest;
            this.Tex = tex;
            this.damage = damage;
            this.areaofeffect = areaofeffect;
            this.enemylist = enemylist;
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
            foreach (Enemy e in enemylist)
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