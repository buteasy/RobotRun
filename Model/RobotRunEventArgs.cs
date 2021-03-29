using System;
using System.Collections.Generic;
using System.Text;

namespace RobotRun.Model
{
    public class RobotRunEventArgs : EventArgs
    {
        private Int32 _gameTime;
        private bool _isWon;

        public Int32 GameTime { get { return _gameTime; } }
        public Boolean IsWon { get { return _isWon; } }

        public RobotRunEventArgs(Int32 gameTime, Boolean IsWon)
        {
            _gameTime = gameTime;
            _isWon = IsWon;
        }
    }
}
