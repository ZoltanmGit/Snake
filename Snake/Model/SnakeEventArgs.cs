using System;

namespace Snake.Model
{
    public class SnakeEventArgs : EventArgs
    {
        private Int32 _eggsEaten;

        private Boolean _isOver;
        public Int32 EggCount { get { return _eggsEaten; } }
        public Boolean IsOver { get { return _isOver; } }
        public SnakeEventArgs(Boolean isOver, Int32 eggsEaten)
        {
            _isOver = isOver;
            _eggsEaten = eggsEaten;
        }
    }
}
