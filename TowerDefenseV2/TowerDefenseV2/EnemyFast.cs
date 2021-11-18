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
        private float speed = 200;



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

        /// <summary>
        /// Calls an enemy that take map width and height as param
        /// </summary>
        /// <param name="mapWidth">Param used to set map width</param>
        /// <param name="mapHeight">Param used to set map height</param>
        public EnemyFast(int mapWidth, int mapHeight)
        {
            base.mapWidth = mapWidth;
            base.mapHeight = mapHeight;
            isActive = true;
            value = 5;
        }

        /// <summary>
        /// Overrides the LoadContent on Enemy to set the correct sprite and the stats of enemyFast
        /// </summary>
        /// <param name="content">Takes ContentManager to look at content in content file</param>
        public override void LoadContent(ContentManager content)
        {
            // SCALE SPRITE Png i korrekt størrelse
            sprite = content.Load<Texture2D>("enemyFast");
            scale = 0.05f;
            rect = new Rectangle(0, 0, sprite.Width, sprite.Height);
            CollisionBox = new Rectangle(0, 0, sprite.Width, sprite.Height);
            Position = new Vector2(GameWorld.enemyMovePattern[0, 0] * 50, GameWorld.enemyMovePattern[0, 1] * 50);
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
