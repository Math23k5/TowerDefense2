﻿using System;
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
        private int strongHealth = 150;
        private float strongSpeed = 5;
        private int strongValue = 15;
        private Texture2D strongSprite;
        private Rectangle strongRectangle;


        /* Enemy skal spawnes ved wave start og skal bevæge sig til givne positioner.
         * Enemy skal tage skade når den rammes af de forskellige skud fra tårnene.
         * Enemy skal tage have nedsænket fart når det rammes af "slow" effekt fra freeze tower.
         * Når enemy dør skal den sende dens værdi tilbage og tilføjes til spillerens guld
         * Strong enemy er langsommere end normal enemies, men har mere helbred.
        */

        public EnemyStrong(float speed, int value, Texture2D sprite, int health) : base(speed, value, sprite, health)
        {
            speed = strongSpeed;
            health = strongHealth;
            value = strongValue;

        }

        public void Death()
        {
            GameWorld.gold += strongValue;
        }

        public override void LoadContent(ContentManager content)
        {
            content.Load<Texture2D>("enemyStrong");
            Health = strongHealth;
        }

        public override void Update(GameTime gameTime)
        {
            // Make the unit move on the specified path (enum?)

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(strongSprite, strongRectangle, Color.White);
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
    