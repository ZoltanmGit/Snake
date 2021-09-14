using System;
using System.Collections.Generic;

namespace Snake.Model
{
    /// <summary>
    /// The enum representation of the direction the snake is heading in
    /// </summary>
    public enum Direction
    {
        Left, Up, Right, Down
    }
    /// <summary>
    /// A class with 2 attributes an x and y meant to represent body parts of the snake
    /// </summary>
    public class Coordinate
    {
        public Int32 x;
        public Int32 y;
    }
    /// <summary>
    /// The snake class handles the direct data of a snake, it's movements, direction, and coordinates
    /// </summary>
    public class Snake
    {
        #region Private Variables
        private Direction _direction;
        private Coordinate _head;
        /// <summary>
        /// Tailcollection is the last bodypart's previous coordinates, so that it can be cleaned up after the snake has moved on, or extend its body if the snake has eaten
        /// </summary>
        private Coordinate _tailcollection;
        private List<Coordinate> _body;
        private Int32 _eggCount;
        #endregion

        #region Public Variables
        public bool bHaveEaten;
        public bool bCanTurn;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes the snake's head and it's body relative to it's head
        /// </summary>
        /// <param name="headX">x coordinate of it's head</param>
        /// <param name="headY">y coordinate of it's head</param>
        public Snake(Int32 headX, Int32 headY)
        {
            _eggCount = 0;
            bCanTurn = true;
            bHaveEaten = false;
            _tailcollection = new Coordinate();
            _head = new Coordinate();
            _head.x = headX;
            _head.y = headY;
            _body = new List<Coordinate>();
            for (int i = 0; i < 4; i++)
            {
                Coordinate temp = new Coordinate();
                if (i==0)
                {
                    temp.x = _head.x+1;
                    temp.y = _head.y;
                }
                else
                {
                    temp.x = _body[i - 1].x + 1;
                    temp.y = _body[i - 1].y;
                }
                _body.Add(temp);
                _direction = Direction.Up;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns the current direction of the snake
        /// </summary>
        /// <returns>Snake direction as enum</returns>
        public Direction GetDirection()
        {
            return _direction;
        }

        /// <summary>
        /// Returns the current position of the snake's head
        /// </summary>
        /// <returns>A coordinate class of the snake's head</returns>
        public Coordinate GetSnakeHead()
        {
            return _head;
        }

        /// <summary>
        /// Returns a list of coordinates representing the snake's body
        /// </summary>
        /// <returns>A list of Coordinate class</returns>
        public List<Coordinate> GetSnakeBody()
        {
            return _body;
        }
        
        /// <summary>
        /// Returns the tailcollection coordinates
        /// </summary>
        /// <returns>A Coordinate class of the snake's last bodypart's position</returns>
        public Coordinate GetTailCollection()
        {
            return _tailcollection;
        }
        
        /// <summary>
        /// Changes the snake's current direction relative to it's head
        /// </summary>
        /// <param name="value">-1 to turn left relatively and 0 to turn right relativelys</param>
        public void SetDirection(Int32 value)
        {
            if(bCanTurn)
            {
                if (_direction == Direction.Left && value == -1)
                {
                    _direction = Direction.Down;
                }
                else if (_direction == Direction.Down && value == 1)
                {
                    _direction = Direction.Left;
                }
                else
                {
                    _direction = _direction + value;
                }
            }
            bCanTurn = false;
        }
        
        /// <summary>
        /// Handles the body's movement so that it follows the head
        /// </summary>
        public void MoveBody()
        {
            _tailcollection.x = _body[_body.Count - 1].x;
            _tailcollection.y = _body[_body.Count - 1].y;
            for (int i =_body.Count-1; i > 0 ; i--)
            {
                _body[i].x = _body[i - 1].x;
                _body[i].y = _body[i - 1].y;
            }
            _body[0] = _head;
        }
        /// <summary>
        /// After the body has new coordinates the head receives it's new postion
        /// </summary>
        /// <param name="newHeadCoordinate">The new position of it's head depending on the direction</param>
        public void MoveHead(Coordinate newHeadCoordinate)
        {
            _head = newHeadCoordinate;
        }
        
        /// <summary>
        /// Creates a new body part and gives it coordinates in the event of an egg being consumed
        /// </summary>
        public void EggConsumed()
        {
            bHaveEaten = true;
            _eggCount += 1;
            Coordinate newBodyPart = new Coordinate();
            newBodyPart.x = _tailcollection.x;
            newBodyPart.y = _tailcollection.y;
            _body.Add(newBodyPart);
        }
        
        /// <summary>
        /// Returns the number of eggs the snake has consumed so far
        /// </summary>
        /// <returns>An Int32 of consumed eggs</returns>
        public Int32 GetEggCount()
        {
            return _eggCount;
        }
        #endregion
    }
}
