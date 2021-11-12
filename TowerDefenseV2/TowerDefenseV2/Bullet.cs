using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Linq;

namespace Tower_Defense
{
    class Bullet : Projectile
    {


        public Texture2D texture;
        public Rectangle rec;
        public Vector2 movement;
        public Vector2 velocity;
        

        public void Init(Texture2D text, Vector2 pos, Vector2 move)
        {
            texture = text;
            position = pos;
            movement = move;
        }
        public override void OnHit(Enemy other)
        {

        }
        public Bullet(Texture2D texture, Point position)
        {
            
        }

        public override void LoadContent(ContentManager content)
        {
            content.Load<Texture2D>("Reddot");

            rec = new Rectangle(0, 0, texture.Width, texture.Height);
        }

        public override void Update(GameTime gameTime)
        {




        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();


            if (isActive == true)
            {
                spriteBatch.Draw(texture, rec, Color.White);
            }

            spriteBatch.End();



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

        }

    }


}