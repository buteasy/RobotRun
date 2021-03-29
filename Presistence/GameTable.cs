using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace RobotRun.Presistence
{
    public class GameTable
    {
        #region Fields
        private Int32 _tableSize;
        private Int32 _gameTime;
        private Field[,] _fieldBoard;
        #endregion

        #region Properties
        public Int32 TableSize
        {
            get { return _tableSize; }
            set { _tableSize = value; }
        }

        public Field[,] FieldBoard { get { return _fieldBoard; } }
        #endregion

        #region Constructor
        public GameTable(Int32 size)
        {
            _gameTime = 0;
            Random rnd = new Random();
            _tableSize = size;
            _fieldBoard = new Field[size, size];

            for(Int32 i = 0; i < _tableSize; ++i)
            {
                for(Int32 j = 0; j < _tableSize; ++j)
                {
                    _fieldBoard[i, j] = Field.Empty;
                }
            }
            _fieldBoard[_tableSize / 2, _tableSize / 2] = Field.Magnet;
            _fieldBoard[rnd.Next(0, _tableSize - 1), rnd.Next(0, _tableSize - 1)] = Field.Robot;
        }
        #endregion

        public void SetGameTime(Int32 size) { _gameTime = size; }
        public void IncreaseGameTime() { _gameTime++; }
        public Int32 GetGameTime() { return _gameTime; }
        public Int32 GetValue(Int32 x, Int32 y)
        {
            if (x < 0 || x >= _tableSize)
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _tableSize)
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");
            
            switch(_fieldBoard[x, y])
            {
                case Field.Empty:
                    return 0;
                case Field.Robot:
                    return 1;
                case Field.Magnet:
                    return 2;
                case Field.Wall:
                    return 3;
                case Field.DestroyedWall:
                    return 4;
            }
            return -1;
        }

        public void SetValue(Int32 x, Int32 y, Int32 value)
        {
            if (x < 0 || x >= _tableSize)
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _tableSize)
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");
            if (value < 0 || value>5)
                throw new ArgumentOutOfRangeException("value", "The value is out of range.");

            Field fieldToSet = Field.Empty;

            switch (value)
            {
                case 0:
                    fieldToSet = Field.Empty;
                    break;
                case 1:
                    fieldToSet = Field.Robot;
                    break;
                case 2:
                    fieldToSet = Field.Magnet;
                    break;
                case 3:
                    fieldToSet = Field.Wall;
                    break;
                case 4:
                    fieldToSet = Field.DestroyedWall;
                    break;
            }
            _fieldBoard[x, y] = fieldToSet;
        }
    }
}
