using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace Tower_Defense
{
    /// <summary>
    /// Bomber er et Tower med størst skade og desuden giver area damage til flere enemies i området
    /// Til gengæld har den også den længste rate of fire, så den skyder langsomere end alle de andre Towers
    /// Tårnet er ikke blevet implementeret korrekt i spillet
    /// </summary>
    public class Bomber : Tower
    {
        private Texture2D bomb;


        public Bomber(Vector2 position) : base(position)
        {
            damage = 20;
        }

        public override void Shoot(GameTime gameTime)
        {
            foreach (Enemy myEnemy in GameWorld.myEnemies)
            {
                distance = (float)Math.Sqrt((Math.Pow(position.X - myEnemy.Position.X, 2) + Math.Pow(position.Y - myEnemy.Position.Y, 2)));
                if (distance <= range)
                {
                    GameWorld.myProjectiles.Add(new Bomb(new Vector2(position.X + sprite.Width / 2, position.Y + sprite.Height / 2), myEnemy.Position, damage, 50, bomb));
                }

            }
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("BomberSprite");
            scale = 0.2f;
            rect = new Rectangle(0, 0, sprite.Width, sprite.Height);
            bomb = content.Load<Texture2D>("bullet");
        }


        public override void Update(GameTime gameTime)
        {

        }
    }
}