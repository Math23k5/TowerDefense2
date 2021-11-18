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
        public static List<Enemy> myEnemies = new List<Enemy>();
        public static List<Projectile> myProjectiles = new List<Projectile>();

        public static int[,] enemyMovePattern = new int[,] { { 4, -1 }, { 4, 4 }, { 10, 4 }, { 10, 2 }, { 20, 2 }, { 20, 4 }, { 26, 4 }, { 26, 14 }, { 22, 14 }, { 22, 8 }, { 16, 8 }, { 16, 12 }, { 14, 12 }, { 14, 14 }, { 10, 14 }, { 10, 8 }, { 4, 8 }, { 4, 10 }, { 2, 10 }, { 2, 14 }, { 6, 14 }, { 6, 18 } };
        private int[,] enemyWaves = new int[,] { { 10, 0, 0 }, { 10, 2, 0 }, { 15, 5, 1 }, { 5, 10, 2 }, { 10, 10, 5 }, { 10, 0, 10 }, { 0, 20, 0 }, { 0, 0, 20 } };
        private int currentWave = 0;
        private int enemiesOfTypeSpawned = 0;
        private int currentTypeSpawn = 0;

        public static int gold = 40;
        public static int playerHealth = 20;

        private float timeToWave = 5.0f;
        private float timeBetweenEnemies = 1.0f;

        private bool[,] placeAble;
        private bool[,] notOccupied;
        private Texture2D map;
        private Texture2D square;
        private Texture2D dirt;
        private Texture2D curPos;

        private SpriteFont timerFont;

        private int mapWidth;
        private int mapHeight;

        private int[] selPos = new int[2];
        private bool keyDownMove;
        private bool keyDownTower;
        private bool keyDownSpawn;

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width - 200;
            _graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height - 100;
            //_graphics.IsFullScreen = true;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //Bitmap img = new Bitmap("map1.png");

            square = Content.Load<Texture2D>("square");
            dirt = Content.Load<Texture2D>("dirt");
            curPos = Content.Load<Texture2D>("curPos");

            map = Content.Load<Texture2D>("map1");
            mapWidth = map.Width;
            mapHeight = map.Height;

            Color[] colors = new Color[mapWidth * mapHeight];
            map.GetData(colors);

            Color brickRGB = new Color(0, 0, 0);
            placeAble = new bool[mapWidth, mapHeight];

            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    if (colors[y * mapWidth + x] == brickRGB)
                    {
                        placeAble[x, y] = true;
                    }
                }
            }
            notOccupied = (bool[,])placeAble.Clone();
            timerFont = Content.Load<SpriteFont>("WaveTimer");

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
                myEnemy.Update(gameTime, _graphics);
            }
            foreach (Projectile myProjectile in myProjectiles)
            {
                myProjectile.Update(gameTime);
            }

            if (timeToWave <= 0 && currentWave < enemyWaves.Length/3)
            {
                SpawnEnemyWave(gameTime);
            }
            else
            {
                timeToWave -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            HandleInput();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();


            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    if (placeAble[x, y] == true)
                    {
                        _spriteBatch.Draw(square, new Vector2(x * 50 + (_graphics.GraphicsDevice.Viewport.Width - (mapWidth * 50)) / 2, y * 50 + (_graphics.GraphicsDevice.Viewport.Height - (mapHeight * 50)) / 2), Color.White);
                    }
                    else
                    {
                        _spriteBatch.Draw(dirt, new Vector2(x * 50 + (_graphics.GraphicsDevice.Viewport.Width - (mapWidth * 50)) / 2, y * 50 + (_graphics.GraphicsDevice.Viewport.Height - (mapHeight * 50)) / 2), Color.White);
                    }
                }
            }

            foreach (Tower myTower in myTowers)
            {
                myTower.Draw(_spriteBatch);
            }
            foreach (Enemy myEnemy in myEnemies)
            {
                myEnemy.Draw(_spriteBatch, _graphics);
            }
            foreach (Projectile myProjectile in myProjectiles)
            {
                myProjectile.Draw(_spriteBatch);
            }

            _spriteBatch.Draw(curPos, new Vector2(selPos[0] * 50 + (_graphics.GraphicsDevice.Viewport.Width - (mapWidth * 50)) / 2, selPos[1] * 50 + (_graphics.GraphicsDevice.Viewport.Height - (mapHeight * 50)) / 2), Color.White);

            _spriteBatch.DrawString(timerFont, "Time to Wave " + (int)timeToWave, new Vector2(20, 20), Color.White);
            _spriteBatch.DrawString(timerFont, "Gold: " + gold, new Vector2(20, 50), Color.White);
            _spriteBatch.DrawString(timerFont, "Health Left: " + playerHealth, new Vector2(20, 80), Color.White);


            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void SpawnEnemyWave(GameTime gameTime)
        {
            if (timeBetweenEnemies <= 0)
            {
                if (enemiesOfTypeSpawned >= enemyWaves[currentWave, currentTypeSpawn])
                {
                    enemiesOfTypeSpawned = 0;
                    currentTypeSpawn++;
                    if (currentTypeSpawn >= 3)
                    {
                        currentTypeSpawn = 0;
                        timeToWave = 30.0f;
                        currentWave++;
                    }
                } else
                {
                    switch (currentTypeSpawn)
                    {
                        case 0:
                            EnemyNormal myEnemy1 = new EnemyNormal(mapWidth, mapHeight);
                            myEnemy1.LoadContent(Content);
                            myEnemies.Add(myEnemy1);
                            break;
                        case 1:
                            EnemyFast myEnemy2 = new EnemyFast(mapWidth, mapHeight);
                            myEnemy2.LoadContent(Content);
                            myEnemies.Add(myEnemy2);
                            break;
                        case 2:
                            EnemyStrong myEnemy3 = new EnemyStrong(mapWidth, mapHeight);
                            myEnemy3.LoadContent(Content);
                            myEnemies.Add(myEnemy3);
                            break;
                    }
                    enemiesOfTypeSpawned++;
                    timeBetweenEnemies = 0.5f;
                }
            }
            else
            {
                timeBetweenEnemies -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        private void HandleInput()
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.W) && selPos[1] > 0 && keyDownMove != true)
            {
                selPos[1]--;
                keyDownMove = true;
            }
            else if (keyState.IsKeyDown(Keys.S) && selPos[1] < mapHeight - 1 && keyDownMove != true)
            {
                selPos[1]++;
                keyDownMove = true;
            }
            else if (keyState.IsKeyDown(Keys.A) && selPos[0] > 0 && keyDownMove != true)
            {
                selPos[0]--;
                keyDownMove = true;
            }
            else if (keyState.IsKeyDown(Keys.D) && selPos[0] < mapWidth - 1 && keyDownMove != true)
            {
                selPos[0]++;
                keyDownMove = true;
            }
            else if (keyState.IsKeyUp(Keys.W) && keyState.IsKeyUp(Keys.S) && keyState.IsKeyUp(Keys.A) && keyState.IsKeyUp(Keys.D) && keyState.IsKeyUp(Keys.Space) && keyDownMove == true)
            {
                keyDownMove = false;
            }


            if (keyState.IsKeyDown(Keys.D1) && keyDownTower != true)
            {
                if (notOccupied[selPos[0], selPos[1]] == true && gold >= 10)
                {
                    Standard newTower = new Standard(new Vector2(selPos[0] * 50 + (_graphics.GraphicsDevice.Viewport.Width - (mapWidth * 50)) / 2, selPos[1] * 50 + (_graphics.GraphicsDevice.Viewport.Height - (mapHeight * 50)) / 2));
                    newTower.LoadContent(Content);
                    myTowers.Add(newTower);
                    gold -= 10;
                    notOccupied[selPos[0], selPos[1]] = false;
                }
                keyDownTower = true;
            }
            else if (keyState.IsKeyDown(Keys.D2) && keyDownTower != true)
            {
                if (notOccupied[selPos[0], selPos[1]] == true && gold >= 30)
                {
                    Sniper newTower = new Sniper(new Vector2(selPos[0] * 50 + (_graphics.GraphicsDevice.Viewport.Width - (mapWidth * 50)) / 2, selPos[1] * 50 + (_graphics.GraphicsDevice.Viewport.Height - (mapHeight * 50)) / 2));
                    newTower.LoadContent(Content);
                    myTowers.Add(newTower);
                    gold -= 30;
                    notOccupied[selPos[0], selPos[1]] = false;
                }
                keyDownTower = true;
            }
            else if (keyState.IsKeyDown(Keys.D3) && keyDownTower != true)
            {
                if (notOccupied[selPos[0], selPos[1]] == true && gold >= 50)
                {
                    Bomber newTower = new Bomber(new Vector2(selPos[0] * 50 + (_graphics.GraphicsDevice.Viewport.Width - (mapWidth * 50)) / 2, selPos[1] * 50 + (_graphics.GraphicsDevice.Viewport.Height - (mapHeight * 50)) / 2));
                    newTower.LoadContent(Content);
                    myTowers.Add(newTower);
                    gold -= 50;
                    
                    notOccupied[selPos[0], selPos[1]] = false;
                }
                keyDownTower = true;
            }
            else if (keyState.IsKeyDown(Keys.D4) && keyDownTower != true)
            {
                if (notOccupied[selPos[0], selPos[1]] == true && gold >= 40)
                {
                    Freezer newTower = new Freezer(new Vector2(selPos[0] * 50 + (_graphics.GraphicsDevice.Viewport.Width - (mapWidth * 50)) / 2, selPos[1] * 50 + (_graphics.GraphicsDevice.Viewport.Height - (mapHeight * 50)) / 2));
                    newTower.LoadContent(Content);
                    myTowers.Add(newTower);
                    gold -= 40;
                    notOccupied[selPos[0], selPos[1]] = false;
                }
                keyDownTower = true;
            }
            else if (keyState.IsKeyUp(Keys.D1) && keyState.IsKeyUp(Keys.D2) && keyState.IsKeyUp(Keys.D3) && keyState.IsKeyUp(Keys.D4) && keyDownTower == true)
            {
                keyDownTower = false;
            }

            if (keyState.IsKeyDown(Keys.Space) && keyDownSpawn != true)
            {
                timeToWave = 0.0f;
                keyDownSpawn = true;

            } else if (keyState.IsKeyUp(Keys.Space) && keyDownSpawn == true)
            {
                keyDownSpawn = false;
            }
        }
    }
}
