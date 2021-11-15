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
        private int health = 75;
        private float speed = 15;
        private int value = 7;
        private Texture2D sprite;
        

        /* Enemy skal spawnes ved wave start og skal bevæge sig til givne positioner.
         * Enemy skal tage skade når den rammes af de forskellige skud fra tårnene.
         * Enemy skal tage have nedsænket fart når det rammes af "slow" effekt fra freeze tower.
         * Når enemy dør skal den sende dens værdi tilbage og tilføjes til spillerens guld.
         * Fast enemy skal være hurtigere, men svagere end normal enemy.
         * Når enemy når til sidste waypoint skal den fjerne 1 liv fra spilleren.
         * 
         * screenSize = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
         * Spørg på grid width og grid height for placering af enemy
        */
        public EnemyFast(int[,] enemyMovePattern)
        {
            
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("enemyFast");
            CollisionBox = new Rectangle(0, 0, sprite.Width, sprite.Height);
            base.Health = this.health;
            base.Value = this.value;
            base.NormalSpeed = this.speed;
        }

        //public void OnCollision(Projectile x)
        //{
        //    if (other is /* Insert projectile */)
        //    {
        //        // Add damage or destruction/de-activate
        //    }
        //}

    }
}
