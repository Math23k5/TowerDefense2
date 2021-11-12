﻿using System;
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
        private int normalHealth = 100;
        private float normalSpeed = 10;
        private int normalValue = 10;
        private Texture2D normalSprite;
        private Rectangle normalRectangle;

        /* Enemy skal spawnes ved wave start og skal bevæge sig til givne positioner.
         * Enemy skal tage skade når den rammes af de forskellige skud fra tårnene.
         * Enemy skal tage have nedsænket fart når det rammes af "slow" effekt fra freeze tower.
         * Når enemy dør skal den sende dens værdi tilbage og tilføjes til spillerens guld
         * Normal enemy er baseline for alle andre typer af enemies.
        */

        public EnemyNormal(float speed, int value, Texture2D sprite, int health) : base(speed, value, sprite, health)
        {
            value = normalValue;
            speed = normalSpeed;
            health = normalHealth;
        }


        public void Death()
        {
            int goldValue = normalValue = 10;
            GameWorld.Gold();
        }

        public override void LoadContent(ContentManager content)
        {
            content.Load<Texture2D>("enemyNormal");
            Health = normalHealth;
        }

        public override void Update(GameTime gameTime)
        {
            // Make the unit move on the specified path (enum?)

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(normalSprite, normalRectangle, Color.White);
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