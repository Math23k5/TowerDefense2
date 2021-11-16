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


        public static int[,] enemyMovePattern = new int[,] { { 2, 0 }, { 2, 3 }, { 4, 3 } };
        private int[,] enemyWaves = new int[,] { { 20, 0, 0 }, { 10, 5, 2 } };
        private int currentWave = 0;
        private int enemiesOfTypeSpawned = 0;
        private int currentTypeSpawn = 0;

        public static int gold = 40;
        public static int playerHealth = 20;

        private float timeToWave = 30.0f;
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

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            _graphics.IsFullScreen = true;
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
            notOccupied = placeAble;
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
            HandleInput();

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
                if (notOccupied[selPos[0], selPos[1]] == true)
                {
                    myTowers.Add(new Standard(new Vector2(selPos[0] * 50 + (_graphics.GraphicsDevice.Viewport.Width - (mapWidth * 50)) / 2, selPos[1] * 50 + (_graphics.GraphicsDevice.Viewport.Height - (mapHeight * 50)) / 2)));
                    notOccupied[selPos[0], selPos[1]] = false;
                }
                keyDownTower = true;
            }
            else if (keyState.IsKeyDown(Keys.D2) && keyDownTower != true)
            {
                if (notOccupied[selPos[0], selPos[1]] == true)
                {
                    myTowers.Add(new Sniper(new Vector2(selPos[0] * 50 + (_graphics.GraphicsDevice.Viewport.Width - (mapWidth * 50)) / 2, selPos[1] * 50 + (_graphics.GraphicsDevice.Viewport.Height - (mapHeight * 50)) / 2)));
                    notOccupied[selPos[0], selPos[1]] = false;
                }
                keyDownTower = true;
            }
            else if (keyState.IsKeyDown(Keys.D3) && keyDownTower != true)
            {
                if (notOccupied[selPos[0], selPos[1]] == true)
                {
                    myTowers.Add(new Bomber(new Vector2(selPos[0] * 50 + (_graphics.GraphicsDevice.Viewport.Width - (mapWidth * 50)) / 2, selPos[1] * 50 + (_graphics.GraphicsDevice.Viewport.Height - (mapHeight * 50)) / 2)));
                    notOccupied[selPos[0], selPos[1]] = false;
                }
                keyDownTower = true;
            }
            else if (keyState.IsKeyDown(Keys.D4) && keyDownTower != true)
            {
                if (notOccupied[selPos[0], selPos[1]] == true)
                {
                    myTowers.Add(new Freezer(new Vector2(selPos[0] * 50 + (_graphics.GraphicsDevice.Viewport.Width - (mapWidth * 50)) / 2, selPos[1] * 50 + (_graphics.GraphicsDevice.Viewport.Height - (mapHeight * 50)) / 2)));
                    notOccupied[selPos[0], selPos[1]] = false;
                }
                keyDownTower = true;
            }
            else if (keyState.IsKeyUp(Keys.D1) && keyState.IsKeyUp(Keys.D2) && keyState.IsKeyUp(Keys.D3) && keyState.IsKeyUp(Keys.D4) && keyDownTower == true)
            {
                keyDownTower = false;
            }
        }
    }
}
