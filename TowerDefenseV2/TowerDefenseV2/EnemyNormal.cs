using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Tower_Defense
{
    class EnemyNormal : Enemy
    {
        // Variabler hentes fra Enemy superklassen
        private int health = 10;
        private float speed = 100.0f;
        private Rectangle collisionBox;

        /* Enemy skal spawnes ved wave start og skal bevæge sig til givne positioner.
         * Enemy skal tage skade når den rammes af de forskellige skud fra tårnene.
         * Enemy skal tage have nedsænket fart når det rammes af "slow" effekt fra freeze tower.
         * Når enemy dør skal den sende dens værdi tilbage og tilføjes til spillerens guld
         * Normal enemy er baseline for alle andre typer af enemies.
        */

        public EnemyNormal(int mapWidth, int mapHeight) 
        {
            base.mapWidth = mapWidth;
            base.mapHeight = mapHeight;
            isActive = true;
            base.value = 10;
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("enemyNormal");
            scale = 0.05f;
            rect = new Rectangle(0, 0, sprite.Width, sprite.Height);
            Position = new Vector2(GameWorld.enemyMovePattern[0, 0] * 50, GameWorld.enemyMovePattern[0, 1] * 50);
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