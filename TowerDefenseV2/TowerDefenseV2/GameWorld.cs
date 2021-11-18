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

        private List<Tower> myTowers = new List<Tower>(); //list of all towers in game
        public static List<Enemy> myEnemies = new List<Enemy>(); //list of all enemies in game
        public static List<Projectile> myProjectiles = new List<Projectile>(); //list of projectiles in game

        //the map pattern that the enemies have to move thorugh
        public static int[,] enemyMovePattern = new int[,] { { 4, -1 }, { 4, 4 }, { 10, 4 }, { 10, 2 }, { 20, 2 }, { 20, 4 }, { 26, 4 }, { 26, 14 }, { 22, 14 }, { 22, 8 }, { 16, 8 }, { 16, 12 }, { 14, 12 }, { 14, 14 }, { 10, 14 }, { 10, 8 }, { 4, 8 }, { 4, 10 }, { 2, 10 }, { 2, 14 }, { 6, 14 }, { 6, 18 } };
        //the different waves of enemies, index 0 = normal, index 1 = fast, index 2 = strong
        private int[,] enemyWaves = new int[,] { { 10, 0, 0 }, { 10, 2, 0 }, { 15, 5, 1 }, { 5, 10, 2 }, { 10, 10, 5 }, { 10, 0, 10 }, { 0, 20, 0 }, { 0, 0, 20 }, { 0, 0, 999 } };
        private int currentWave = 0; 
        private int enemiesOfTypeSpawned = 0; 
        private int currentTypeSpawn = 0;

        public static int gold = 40; //player gold
        public static int playerHealth = 20; 

        private float timeToWave = 25.0f; 
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
            _graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width - 200; //sets the width of the window
            _graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height - 100; //sets the height of the window
            _graphics.IsFullScreen = true; //set the window to fullscreen
            _graphics.ApplyChanges(); //applies the changes

            base.Initialize();
        }

        /// <summary>
        ///     Loads the content
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //sets the art assets
            square = Content.Load<Texture2D>("square"); 
            dirt = Content.Load<Texture2D>("dirt");
            curPos = Content.Load<Texture2D>("curPos");

            map = Content.Load<Texture2D>("map1"); //sets the map asset
            mapWidth = map.Width;
            mapHeight = map.Height;

            Color[] colors = new Color[mapWidth * mapHeight]; //creates a color array for all pixels in the map
            map.GetData(colors); //sets those colors in the array

            Color brickRGB = new Color(0, 0, 0); //creates a black color
            placeAble = new bool[mapWidth, mapHeight]; //sets the array to the size of the map

            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    if (colors[y * mapWidth + x] == brickRGB) //if the checked pixel is black
                    {
                        placeAble[x, y] = true; //set checked location as a place you can place towers
                    }
                }
            }
            notOccupied = (bool[,])placeAble.Clone(); //creates a clone of the array
            timerFont = Content.Load<SpriteFont>("WaveTimer"); //sets the font for the screen text

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        ///     updates the state of the game for all objects
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            foreach (Tower myTower in myTowers) //run updates for all towers
            {
                myTower.Update(gameTime);
            }
            foreach (Enemy myEnemy in myEnemies) //run updates for all enemies
            {
                myEnemy.Update(gameTime, _graphics);
            }
            foreach (Projectile myProjectile in myProjectiles) //run updates for all projectiles
            {
                myProjectile.Update(gameTime);
            }

            if (timeToWave <= 0 && currentWave < enemyWaves.Length/3) //if it is time to spawn a new wave, and there are more waves to spawn
            {
                SpawnEnemyWave(gameTime); //spawn a wave
            }
            else
            {
                timeToWave -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            HandleInput(); //player input

            base.Update(gameTime);
        }

        /// <summary>
        ///     Draws all objects in the gameworld
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black); //background = black

            // TODO: Add your drawing code here
            _spriteBatch.Begin();


            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    if (placeAble[x, y] == true)
                    {
                        //draw a square sprite where they need to be
                        _spriteBatch.Draw(square, new Vector2(x * 50 + (_graphics.GraphicsDevice.Viewport.Width - (mapWidth * 50)) / 2, y * 50 + (_graphics.GraphicsDevice.Viewport.Height - (mapHeight * 50)) / 2), Color.White);
                    }
                    else
                    {
                        //draw a dirt sprite where they need to be
                        _spriteBatch.Draw(dirt, new Vector2(x * 50 + (_graphics.GraphicsDevice.Viewport.Width - (mapWidth * 50)) / 2, y * 50 + (_graphics.GraphicsDevice.Viewport.Height - (mapHeight * 50)) / 2), Color.White);
                    }
                }
            }

            foreach (Tower myTower in myTowers) //draw all towers
            {
                myTower.Draw(_spriteBatch); 
            }
            foreach (Enemy myEnemy in myEnemies) //draw all enemies
            {
                myEnemy.Draw(_spriteBatch, _graphics);
            }
            foreach (Projectile myProjectile in myProjectiles) //draw all projectiles
            {
                myProjectile.Draw(_spriteBatch);
            }

            //draw position of player selection
            _spriteBatch.Draw(curPos, new Vector2(selPos[0] * 50 + (_graphics.GraphicsDevice.Viewport.Width - (mapWidth * 50)) / 2, selPos[1] * 50 + (_graphics.GraphicsDevice.Viewport.Height - (mapHeight * 50)) / 2), Color.White);

            //draw UI text
            _spriteBatch.DrawString(timerFont, "Time to Wave " + (int)timeToWave, new Vector2(20, 20), Color.White); 
            _spriteBatch.DrawString(timerFont, "Gold: " + gold, new Vector2(20, 50), Color.White);
            _spriteBatch.DrawString(timerFont, "Health Left: " + playerHealth, new Vector2(20, 80), Color.White);


            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        ///     spawns waves of enemies
        /// </summary>
        /// <param name="gameTime"></param>
        private void SpawnEnemyWave(GameTime gameTime)
        {
            if (timeBetweenEnemies <= 0) //if it is time to spawn an enemy
            {
                if (enemiesOfTypeSpawned >= enemyWaves[currentWave, currentTypeSpawn]) //if the emount of a type of enemy has reached the maks in the wave
                {
                    enemiesOfTypeSpawned = 0; //set the amount of the next enemy type to 0
                    currentTypeSpawn++; //moves to the next type in the wave
                    if (currentTypeSpawn >= 3) //if all three types have been spawned
                    {
                        currentTypeSpawn = 0; //reset type
                        timeToWave = 30.0f; //reset the timer until next wave
                        currentWave++; //move to next wave
                    }
                } else
                {
                    switch (currentTypeSpawn) //what type is being spawned?
                    {
                        case 0: //spawn a normal enemy
                            EnemyNormal myEnemy1 = new EnemyNormal(mapWidth, mapHeight); //create enemy
                            myEnemy1.LoadContent(Content); //run the enemy's LoadContent
                            myEnemies.Add(myEnemy1); //add the enemy to the list of enemies
                            break;
                        case 1: //spawn a fast enemy
                            EnemyFast myEnemy2 = new EnemyFast(mapWidth, mapHeight);
                            myEnemy2.LoadContent(Content);
                            myEnemies.Add(myEnemy2);
                            break;
                        case 2: //spawn a strong enemy
                            EnemyStrong myEnemy3 = new EnemyStrong(mapWidth, mapHeight);
                            myEnemy3.LoadContent(Content);
                            myEnemies.Add(myEnemy3);
                            break;
                    }
                    enemiesOfTypeSpawned++; //count up the amount that has spawned of the current type 
                    timeBetweenEnemies = 0.5f; //reset timer until next spawn
                }
            }
            else
            {
                timeBetweenEnemies -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        /// <summary>
        ///     All player input
        /// </summary>
        private void HandleInput()
        {
            KeyboardState keyState = Keyboard.GetState(); //get state of keyboard

            //move selection on grid, W = up, S = down, A = left, D = right
            if (keyState.IsKeyDown(Keys.W) && selPos[1] > 0 && keyDownMove != true)
            {
                selPos[1]--;
                keyDownMove = true; //make sure the action can't be triggered again before this is false
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
            //makes sure that all the move keys can be used when all of the states are up (not currently pressed down)
            else if (keyState.IsKeyUp(Keys.W) && keyState.IsKeyUp(Keys.S) && keyState.IsKeyUp(Keys.A) && keyState.IsKeyUp(Keys.D) && keyState.IsKeyUp(Keys.Space) && keyDownMove == true)
            {
                keyDownMove = false;
            }

            //keys for spawning towers, 1 = standard, 2 = sniper, 3 = bomber, 4 = freezer
            if (keyState.IsKeyDown(Keys.D1) && keyDownTower != true) 
            {
                if (notOccupied[selPos[0], selPos[1]] == true && gold >= 10) //if a tower can be placed and the player has 10+ gold
                {
                    //spawn standard tower
                    Standard newTower = new Standard(new Vector2(selPos[0] * 50 + (_graphics.GraphicsDevice.Viewport.Width - (mapWidth * 50)) / 2, selPos[1] * 50 + (_graphics.GraphicsDevice.Viewport.Height - (mapHeight * 50)) / 2));
                    newTower.LoadContent(Content); //load its content
                    myTowers.Add(newTower); //add it to the tower list
                    gold -= 10; //take away 10 gold
                    notOccupied[selPos[0], selPos[1]] = false; //make it occupied
                }
                keyDownTower = true; //makes sure it only happens once
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
            //if all tower placement keys are up (not pressed down)
            else if (keyState.IsKeyUp(Keys.D1) && keyState.IsKeyUp(Keys.D2) && keyState.IsKeyUp(Keys.D3) && keyState.IsKeyUp(Keys.D4) && keyDownTower == true)
            {
                keyDownTower = false;
            }

            if (keyState.IsKeyDown(Keys.Space) && keyDownSpawn != true) //if space is pressed
            {
                timeToWave = 0.0f; //makes the next wave spawn
                keyDownSpawn = true;

            } else if (keyState.IsKeyUp(Keys.Space) && keyDownSpawn == true)
            {
                keyDownSpawn = false;
            }
        }
    }
}
