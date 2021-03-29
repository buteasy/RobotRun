using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Diagnostics;
using RobotRun.Presistence;
using System.Threading.Tasks;

namespace RobotRun.Model
{
    public class RobotRunModel
    {
        #region Fields
        private Int32 _gameSize;
        private Int32 _gameTime;
        private GameTable _gameTable;
        private Int32 _currentDir;
        private Random rnd;
        private Boolean _isOnDestroyed;
        private IRobotRunDataAccess _dataAccess;

        #endregion

        #region Properties
        public Int32 gameSize { 
            get { return _gameSize; } 
            set { _gameSize = value; }
        }
        public Int32 gameTime { get { return _gameTime; } }

        public GameTable GameTable
        {
            get { return _gameTable; }
        }
        #endregion

        #region Constructor
        public RobotRunModel(Int32 size, IRobotRunDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
            rnd = new Random();
            _isOnDestroyed = false;
            _gameTime = 0;
            _gameSize = size;
            _gameTable = new GameTable(_gameSize);
            _currentDir = rnd.Next(-2, 3);
        }
        #endregion

        #region Events
        public event EventHandler<RobotRunEventArgs> TimeRefreshed;
        public event EventHandler<RobotRunEventArgs> TableRefreshed;
        public event EventHandler<RobotRunEventArgs> GameOver;
        public event EventHandler<RobotRunEventArgs> GameStuck;
        #endregion

        #region Public methods

        public void SetGameTime(Int32 size) { _gameTime = size; }

        public void NewGame(Int32 size)
        {
            _isOnDestroyed = false;
            _currentDir = rnd.Next(-2, 3);
            _gameSize = size;
            _gameTime = 0;
            _gameTable = new GameTable(size);

            RefreshTable();
        }
        public void RefreshTime()
        {
                _gameTime++;
                _gameTable.IncreaseGameTime();
                if (TimeRefreshed != null)
                {
                    TimeRefreshed(this, new RobotRunEventArgs(_gameTime, false));
                }
                if(_gameTime % 10 ==0) { FindNewDirection(); }
                if (!Move())
                {
                    FindNewDirection();
                    Move();
                }
                RefreshTable();
                CheckWon();
        }
  
        public void RefreshTable()
        {
            TableRefreshed(this, new RobotRunEventArgs(_gameSize, false));
        }

        public void BuildWall(Int32 x, Int32 y)
        {
            if (GameTable.FieldBoard[x, y] != Field.DestroyedWall && GameTable.FieldBoard[x, y] != Field.Robot && GameTable.FieldBoard[x, y] != Field.Magnet)
            {
                GameTable.FieldBoard[x, y] = Field.Wall;
                RefreshTable();
                CheckWon();
                if (CheckIfStuck(x, y)) 
                {
                    if(GameStuck != null)
                    {
                        GameStuck(this, new RobotRunEventArgs(_gameTime, false));
                    }
                }
            }
        }

        private bool CheckIfStuck(int x, int y)
        {
            if()GameTable.FieldBoard[x + 1, y] == Field.Wall &&
                   GameTable.FieldBoard[x - 1, y] == Field.Wall &&
                   GameTable.FieldBoard[x, y - 1] == Field.Wall &&
                   GameTable.FieldBoard[x, y + 1] == Field.Wall;
            return true;
        }

        public async Task SaveGameAsync(String path)
        {
            if (_dataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

            await _dataAccess.SaveAsync(path, _gameTable);
        }

        public async Task LoadGameAsync(String path)
        {
            if (_dataAccess == null)
                throw new InvalidOperationException("No data access is provided.");
            _gameTable = await _dataAccess.LoadAsync(path);
            _gameSize = _gameTable.TableSize;
            _gameTime = _gameTable.GetGameTime();
        }

            #endregion

        #region Private Methods

        private void FindNewDirection()
        {
            Int32 RobotX = 0;
            Int32 RobotY = 0;
            for (Int32 i = 0; i < _gameSize; ++i)
            {
                for (Int32 j = 0; j < _gameSize; ++j)
                {
                    if (GameTable.FieldBoard[i, j] == Field.Robot) { RobotX = i; RobotY = j; }
                }
            }
            Int32 newDir = -2;
            while (newDir == -2)
            {
                newDir=RandDirection(RobotX, RobotY, _currentDir);
            }
            if (newDir != -2)
            {
                _currentDir = newDir;
            }
        }
        private Int32 RandDirection(Int32 x, Int32 y, Int32 cur) //nem -1 ha talaltunk iranyt
        {
            var list = new List<int> {-1,0,1,2};
            list.Remove(cur);
            Int32 dir = list[rnd.Next(list.Count)];
            if (Math.Abs(dir) % 2 == 1) //-1 vagy 1
            {
                if (x + dir >= 0 && x + dir < _gameSize && GameTable.FieldBoard[x+dir, y] != Field.Wall)
                {
                    return dir;
                }
            }
            else //0 vagy 2
            {
                if (dir == 0)
                {
                    if (y - 1 >= 0 && GameTable.FieldBoard[x, y-1] != Field.Wall)
                    {
                        return 0;
                    }
                }
                if(dir == 2)
                {
                    if (y + 1 < _gameSize && GameTable.FieldBoard[x, y+1] != Field.Wall)
                    {
                        return 2;
                    }
                }
            }
            return -2;
        }

        private Boolean Move() //igaz ha sikeres a mozgas
        {
            Int32 RobotX = 0;
            Int32 RobotY = 0;

            for(Int32 i = 0; i < _gameSize; ++i)
            {
                for(Int32 j = 0; j < _gameSize; ++j)
                {
                    if (GameTable.FieldBoard[i, j] == Field.Robot) { RobotX = i; RobotY = j; }
                }
            }
            
            if(Math.Abs(_currentDir) % 2 == 1) //-1 vagy 1
            {
                if(RobotX+_currentDir >=0 && RobotX+_currentDir < _gameSize)
                {
                    Boolean tmp = GameTable.FieldBoard[RobotX + _currentDir, RobotY] == Field.DestroyedWall;
                    
                    if (GameTable.FieldBoard[RobotX + _currentDir, RobotY] == Field.Wall) //ütközés
                    {
                        GameTable.FieldBoard[RobotX + _currentDir, RobotY] = Field.DestroyedWall;
                        return false;
                    }
                    else
                    {
                        GameTable.FieldBoard[RobotX + _currentDir, RobotY] = Field.Robot;
                        if (_isOnDestroyed) { GameTable.FieldBoard[RobotX, RobotY] = Field.DestroyedWall; }
                        else { GameTable.FieldBoard[RobotX, RobotY] = Field.Empty; }

                        _isOnDestroyed = tmp;
                        return true;
                    }
                }
                return false;
            }
            else if(_currentDir == 0)
            {
                if (RobotY-1 >= 0)
                {
                    Boolean tmp = GameTable.FieldBoard[RobotX, RobotY - 1] == Field.DestroyedWall;
                    
                    if (GameTable.FieldBoard[RobotX, RobotY - 1] == Field.Wall)
                    {
                        GameTable.FieldBoard[RobotX, RobotY - 1] = Field.DestroyedWall;
                        return false;
                    }
                    else
                    {
                        GameTable.FieldBoard[RobotX, RobotY - 1] = Field.Robot;
                        if (_isOnDestroyed) { GameTable.FieldBoard[RobotX, RobotY] = Field.DestroyedWall; }
                        else { GameTable.FieldBoard[RobotX, RobotY] = Field.Empty; }

                        _isOnDestroyed = tmp;
                        return true;
                    }
                }
                return false;
            }
            else if (_currentDir == 2)
            {
                if (RobotY+1 < _gameSize)
                {
                   Boolean tmp = GameTable.FieldBoard[RobotX, RobotY + 1] == Field.DestroyedWall;
                    
                    if (GameTable.FieldBoard[RobotX, RobotY + 1] == Field.Wall)
                    {
                        GameTable.FieldBoard[RobotX, RobotY + 1] = Field.DestroyedWall;
                        return false;
                    }
                    else
                    {
                        GameTable.FieldBoard[RobotX, RobotY + 1] = Field.Robot;
                        if (_isOnDestroyed) { GameTable.FieldBoard[RobotX, RobotY] = Field.DestroyedWall; }
                        else { GameTable.FieldBoard[RobotX, RobotY] = Field.Empty; }

                        _isOnDestroyed = tmp;
                        return true;
                    }
                }
                return false;
            }
            return false;
        }

        private void CheckWon()
        {
            Int32 RobotX = 0;
            Int32 RobotY = 0;
            for(Int32 i = 0; i < _gameSize; ++i)
            {
                for(Int32 j = 0; j < _gameSize; ++j)
                {
                    if(GameTable.FieldBoard[i, j] == Field.Robot)
                    {
                        RobotX = i;
                        RobotY = j;
                    }
                }
            }
            if(RobotX == _gameSize/2 && RobotY == gameSize / 2)
            {
                GameOver(this, new RobotRunEventArgs(_gameTime, true));
            }
        }
            
        #endregion
    }
}
