using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Tower_Defense
{
    class EnemyFast : Enemy
    {
        // Variabler hentes fra Enemy superklassen
        private int fastHealth = 75;


        /* Enemy skal spawnes ved wave start og skal bevæge sig til givne positioner.
         * Enemy skal tage skade når den rammes af de forskellige skud fra tårnene.
         * Enemy skal tage have nedsænket fart når det rammes af "slow" effekt fra freeze tower.
         * Når enemy dør skal den sende dens værdi tilbage og tilføjes til spillerens guld
         * Fast enemy skal være hurtigere, men svagere end normal enemy
        */



        public void Death()
        {
            int fastValue = 8;
            GameWorld.Gold();

        }

        public override void LoadContent(ContentManager content)
        {
            content.Load<Texture2D>("enemyFast");
            Health = 75;
        }

        public override void Update(GameTime gameTime)
        {
            // Make the unit move on the specified path (enum?)

        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Death();
            }
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
