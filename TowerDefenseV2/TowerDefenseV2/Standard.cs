using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace Tower_Defense
{
    /// <summary>
    /// Standard er et Tower der koster mindst i forhold til de andre Towers, men også skader mindst
    /// </summary>
    public class Standard : Tower
    { 
        //bullets sprite som bliver brugt når tårnet skyder
        private Texture2D bullet;

        public Standard(Vector2 position) : base(position)
        {
            //Skaden tårnet overfører til den prjectile der bliver skudt afsted
            damage = 20;

            //sekunder mellem hver gang Shoot() bliver kaldt i Update()
            rateOfFire = 0.8f;

            //rækkeviden på tårnet
            range = 100.0f;
        }

        /// <summary>
        /// Skyd hver gang en enemy er indenfor range
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Shoot(GameTime gameTime)
        {
            foreach (Enemy myEnemy in GameWorld.myEnemies)
            {
                if (myEnemy.IsActive == true)
                {
                    //Er hver enemy i myEnemies inden for tårnets range
                    distance = (float)Math.Sqrt((Math.Pow(position.X - myEnemy.WorldPos.X, 2) + Math.Pow(position.Y - myEnemy.WorldPos.Y, 2)));
                    if (distance <= range)
                    {
                        //tilføj en Bullet til myProjectile listen
                        //Bullet for tildelt damage fra tårnet,
                        //et target som er myEnemy
                        //og til sidst en Texture2D sprite som er bullet
                        GameWorld.myProjectiles.Add(new Bullet(new Vector2(position.X, position.Y), damage, myEnemy, bullet));
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Loader towerets sprite og bullets sprite
        /// scale: Sætter scale så tårnet ikke fylder halvdelen af skærmen
        /// rect: bruges til at tegne tårnet ind på banen
        /// </summary>
        /// <param name="content"></param>
        /// content leder hen til mappen Content
        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("StandardSprite");
            scale = 0.2f;
            rect = new Rectangle(0, 0, sprite.Width, sprite.Height - 30);
            bullet = content.Load<Texture2D>("Bullet");
        }

        /// <summary>
        /// Alt det der sker med tårnet i løbet af en frame
        /// </summary>
        /// <param name="gameTime"></param>
        /// standard tid mellem hver update
        public override void Update(GameTime gameTime)
        {
            //skyd hvis der er gået mere tiden end værdien af rateOfFire
            if (timeSinceShot >= rateOfFire)
            {
                Shoot(gameTime);
                timeSinceShot = 0.0f;
            } else
            {
                timeSinceShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}