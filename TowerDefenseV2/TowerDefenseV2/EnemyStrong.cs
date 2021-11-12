using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Tower_Defense
{
    class EnemyStrong : Enemy
    {
        // Variabler hentes fra Enemy superklassen
        private int health = 150;
        private float speed = 5;
        private int value = 15;
        private Texture2D sprite;
        private Rectangle rectangle;


        /* Enemy skal spawnes ved wave start og skal bevæge sig til givne positioner.
         * Enemy skal tage skade når den rammes af de forskellige skud fra tårnene.
         * Enemy skal tage have nedsænket fart når det rammes af "slow" effekt fra freeze tower.
         * Når enemy dør skal den sende dens værdi tilbage og tilføjes til spillerens guld
         * Strong enemy er langsommere end normal enemies, men har mere helbred.
        */

        public EnemyStrong(int[,] enemyMovePattern)
        {
           
        }

        public void Death()
        {
            GameWorld.gold += value;
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("enemyStrong");
            Health = health;
        }

        public override void Update(GameTime gameTime)
        {
            // Make the unit move on the specified path (enum?)
            if (isSlowed == true)
            {
                Move(speed * 0.9f, Position);
            }
            //pos til mål
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(sprite, rectangle, Color.White);
            spriteBatch.End();
        }

        //public void OnCollision(Enemy other)
        //{
        //    if (other is /* Insert projectile */)
        //    {
        //        // Add damage or destruction/de-activate
        //    }
        //}
    }
}
    