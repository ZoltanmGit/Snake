using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Snake.Persistence;

namespace Snake.Model
{
    public class SnakeGameModel
    {
        #region Variables
        private SnakeTable _table;
        private Snake _snake;
        private List<Coordinate> _spawnLocations;
        private ISnakeDataAccess _dataAccess;
        public bool bIsGameOver;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes values
        /// </summary>
        /// <param name="dataAccess">Data access for model
        /// </param>
        public SnakeGameModel(ISnakeDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
            _spawnLocations = new List<Coordinate>();
            bIsGameOver = false;
            
        }
        #endregion
        
        #region Events
        
        /// <summary>
        /// Event of Game advancment
        /// </summary>
        public event EventHandler<SnakeEventArgs> GameAdvanced;

        /// <summary>
        /// Event of Game being over
        /// </summary>
        public event EventHandler<SnakeEventArgs> GameOver;
        #endregion

        #region Public Methods
        
        /// <summary>
        /// Returns the table of the map
        /// </summary>
        /// <returns>SnakeTable A matrix of mapvalues</returns>
        public SnakeTable GetSnakeTable()
        {
            return _table;
        }
        
        /// <summary>
        /// Gets the number of eggs
        /// </summary>
        /// <returns>The number of eggs in Int32 type</returns>
        public Int32 GetEggCount()
        {
            return _snake.GetEggCount();
        }

        /// <summary>
        /// Load Game
        /// </summary>
        /// <param name="path">Path to the file</param>
        public async Task LoadGameAsync(String path)
        {
            if (_dataAccess == null)
            {
                throw new InvalidOperationException("No data access is provided.");
            }

            _table = await _dataAccess.LoadAsync(path);
            _table.GetMapSize();
            switch (_table.GetMapSize())
            {
                case 10:
                    SpawnSnake(5, 4);
                    break;
                case 15:
                    SpawnSnake(10,1);
                    break;
                case 20:
                    SpawnSnake(14, 0);
                    break;
                default:
                    break;
            }
            UpdateSpawnLocations();
            SpawnEgg();
        }
        
        /// <summary>
        /// Gives new directions to the snake
        /// </summary>
        /// <param name="value">0 or 1</param>
        public void ChangeDirection(Int32 value)
        {
            //0 - left || 1 - right
            switch (value)
            {
                case 0:
                    _snake.SetDirection(-1);
                    break;
                case 1:
                    _snake.SetDirection(1);
                    break;
                default:
                    break;
            }
        }
        
        /// <summary>
        /// Moves the snake
        /// Changes appropriate table values
        /// Signals event
        /// </summary>
        public void AdvanceGame()
        {
            if (!bIsGameOver)
            {
                _snake.bCanTurn = true;
                ExecuteMovement();
                RepresentSnake();
                OnGameAdvanced();
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Spawn the snake.
        /// </summary>
        /// <param name="x">Rows</param>
        /// <param name="y">Column</param>
        public void SpawnSnake(Int32 x, Int32 y)
        {
            _snake = new Snake(x, y);
            RepresentSnake();
        }
        /// <summary>
        /// For testing purposes where a table is created outside of a LoadGame
        /// </summary>
        public void SetSnakeTable(SnakeTable argSnakeTable)
        {
            _table = argSnakeTable;
        }
        /// <summary>
        /// Returns the snake for testing purposes
        /// </summary>
        public Snake GetSnake()
        {
            return _snake;
        }
        #endregion

        #region Private Methods
        
        /// <summary>
        /// Changes table values to represt the snake in it
        /// </summary>
        private void RepresentSnake()
        {
            _table.SetMapValue(_snake.GetSnakeHead().x, _snake.GetSnakeHead().y, 1);
            for (int i = 0; i < _snake.GetSnakeBody().Count; i++)
            {
                _table.SetMapValue(_snake.GetSnakeBody()[i].x, _snake.GetSnakeBody()[i].y, 2);
            }
        }
        
        /// <summary>
        /// Spawns a new egg in a valid location
        /// </summary>
        private void SpawnEgg()
        {
            if (_spawnLocations.Count != 0)
            {
                Random rnd = new Random();
                bool bIsValid = false;
                do
                {
                    Int32 index = rnd.Next(0, _spawnLocations.Count);
                    if (_table.GetMapValue(_spawnLocations[index].x, _spawnLocations[index].y) == 0)
                    {
                        bIsValid = true;
                        _table.SetMapValue(_spawnLocations[index].x, _spawnLocations[index].y, 3);
                    }
                } while (!bIsValid);
            }
        }
        
        /// <summary>
        /// Handles the movement of the snake if valid and signals Game Over if invalid
        /// </summary>
        private void ExecuteMovement()
        {
            Coordinate newHeadLocation = new Coordinate();
            switch (_snake.GetDirection())
            {
                case Direction.Left:
                    newHeadLocation.x = _snake.GetSnakeHead().x;
                    newHeadLocation.y = _snake.GetSnakeHead().y-1;
                    break;
                case Direction.Up:
                    newHeadLocation.x = _snake.GetSnakeHead().x-1;
                    newHeadLocation.y = _snake.GetSnakeHead().y;
                    break;
                case Direction.Right:
                    newHeadLocation.x = _snake.GetSnakeHead().x;
                    newHeadLocation.y = _snake.GetSnakeHead().y+1;
                    break;
                case Direction.Down:
                    newHeadLocation.x = _snake.GetSnakeHead().x+1;
                    newHeadLocation.y = _snake.GetSnakeHead().y;
                    break;
                default:
                    break;
            }
            if((newHeadLocation.x < 0 || newHeadLocation.x == _table.GetMapSize()) || (newHeadLocation.y < 0 || newHeadLocation.y == _table.GetMapSize()) || (_table.GetMapValue(newHeadLocation.x,newHeadLocation.y) == 4) || (_table.GetMapValue(newHeadLocation.x, newHeadLocation.y) == 2))
            {
                bIsGameOver = true;
                OnGameOver(true);
            }
            else
            {
                if(_table.GetMapValue(newHeadLocation.x,newHeadLocation.y) == 3)
                {
                    _snake.EggConsumed();
                }
                _snake.MoveBody();
                _snake.MoveHead(newHeadLocation);
                if(!_snake.bHaveEaten)
                {
                    _table.SetMapValue(_snake.GetTailCollection().x, _snake.GetTailCollection().y, 0);
                }
                else
                {
                    SpawnEgg();
                }
                _snake.bHaveEaten = false;
            }
        }
        /// <summary>
        /// Fills the _spawnlocations list with non-wall cells
        /// </summary>
        private void UpdateSpawnLocations()
        {
            for (int i = 0; i < _table.GetMapSize(); i++)
            {
                for (int j = 0; j < _table.GetMapSize(); j++)
                {
                    if(_table.GetMapValue(i,j)!=4)
                    {
                        Coordinate tempCoordinate = new Coordinate();
                        tempCoordinate.x = i;
                        tempCoordinate.y = j;
                        _spawnLocations.Add(tempCoordinate);
                    }
                }
            }
        }
        
        /// <summary>
        /// Triggers the advancment game event
        /// </summary>
        private void OnGameAdvanced()
        {
            if (GameAdvanced != null)
            {
                GameAdvanced(this, new SnakeEventArgs(false, _snake.GetEggCount()));
            }
        }
        
        /// <summary>
        /// Triggers the game over event
        /// </summary>
        private void OnGameOver(Boolean isOver)
        {
            if (GameOver != null)
                GameOver(this, new SnakeEventArgs(isOver, _snake.GetEggCount()));
        }
        #endregion
    }
}
