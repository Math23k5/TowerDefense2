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


        /* Enemy skal spawnes ved wave start og skal bevæge sig til givne positioner.
         * Enemy skal tage skade når den rammes af de forskellige skud fra tårnene.
         * Enemy skal tage have nedsænket fart når det rammes af "slow" effekt fra freeze tower.
         * Når enemy dør skal den sende dens værdi tilbage og tilføjes til spillerens guld
         * Strong enemy er langsommere end normal enemies, men har mere helbred.
        */

        public EnemyStrong(int[,] enemyMovePattern)
        {
           
        }

        public override void LoadContent(ContentManager content)
        {
            this.sprite = content.Load<Texture2D>("enemyStrong");
            CollisionBox = new Rectangle(0, 0, sprite.Width, sprite.Height);
            base.Health = this.health;
            base.Value = this.value;
            base.NormalSpeed = this.speed;
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
    