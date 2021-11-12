using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Tower_Defense
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private List<Tower> myTowers = new List<Tower>();
        private List<Enemy> myEnemies = new List<Enemy>();
        public static List<Enemy> MyEnemies
        {
            get { return myEnemies; }
        }

        private int[,] enemyMovePattern = new int[,] { { 2, 0 }, { 2, 3 }, { 4, 3 } };
        private int[,] enemyWaves = new int[,] { { 20, 0, 0 }, { 10, 5, 2 } };
        private int currentWave = 0;
        private int enemiesOfTypeSpawned = 0;
        private int currentTypeSpawn = 0;

        public static int gold = 0;

        private float timeToWave = 30.0f;
        private float timeBetweenEnemies = 1.0f;
        
        private Bitmap img = new Bitmap("map1.png");
        private bool[,] placeAble;
        private int mapWidth;
        private int mapHeight;

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Bitmap img = new Bitmap("map1.png");
            placeAble = new bool[img.Height, img.Width];
            mapHeight = img.Height;
            mapWidth = img.Width;

            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Color pixel = img.GetPixel(i,j);

                    if (pixel == (0,0,0))
                    {
                        placeAble[j,i] = true;
                    }
                }
            } 

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            foreach (Tower myTower in myTowers)
            {
                myTower.Update(gameTime);
            }
            foreach (Enemy myEnemy in myEnemies)
            {
                myEnemy.Update(gameTime);
            }

            if (timeToWave <= 0)
            {
                SpawnEnemyWave(gameTime);
            }
            else
            {
                timeToWave -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            foreach (Tower myTower in myTowers)
            {
                myTower.Draw(_spriteBatch);
            }

            foreach (Enemy myEnemy in myEnemies)
            {
                myEnemy.Draw(_spriteBatch);
            }

            for (int i = 0; i < placeAble[]; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Color pixel = img.GetPixel(i,j);

                    if (pixel == (0,0,0))
                    {
                        placeAble[j,i] = true;
                    }
                }
            } 

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void SpawnEnemyWave(GameTime gameTime)
        {
            if (timeBetweenEnemies <= 0)
            {
                if (enemiesOfTypeSpawned >= enemyWaves[currentWave, currentTypeSpawn])
                {
                    enemiesOfTypeSpawned = 0;
                    currentTypeSpawn++;
                    if (currentTypeSpawn > 3)
                    {
                        currentTypeSpawn = 0;
                        timeToWave = 30.0f;
                    }
                }

                if (currentTypeSpawn != 0)
                {

                    switch (currentTypeSpawn)
                    {
                        case 1:
                            myEnemies.Add(new EnemyNormal(enemyMovePattern));
                            break;
                        case 2:
                            myEnemies.Add(new EnemyFast(enemyMovePattern));
                            break;
                        case 3:
                            myEnemies.Add(new EnemyStrong(enemyMovePattern));
                            break;
                    }
                    enemiesOfTypeSpawned++;
                    timeBetweenEnemies = 1.0f;
                }
            }
            else
            {
                timeBetweenEnemies -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
