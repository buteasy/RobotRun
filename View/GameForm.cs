using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RobotRun.Presistence;
using RobotRun.Model;

namespace RobotRun.View
{
    public partial class GameForm : Form
    {
        #region Fields
        private Button[,] _buttonGrid;
        private Timer _timer;
        private RobotRunModel _model;
        private Int32 _gameSize;
        private IRobotRunDataAccess _dataAccess;
        private Boolean _isPaused;
        #endregion

        #region Constructor
        public GameForm()
        {
            InitializeComponent();
        }


        #endregion

        #region Form event handlers
        private void GameForm_Load(object sender, EventArgs e)
        {
            this.Cursor = new Cursor("C:/Qt/eva/.Net/beadandok/bead3/RobotRun/images/cursor.cur");
            _dataAccess = new RobotRunFileDataAccess();
            _model = new RobotRunModel(11, _dataAccess);
            _model.TimeRefreshed += new EventHandler<RobotRunEventArgs>(Game_TimeRefreshed);
            _model.TableRefreshed += new EventHandler<RobotRunEventArgs>(Game_TableRefreshed);
            _model.GameOver += new EventHandler<RobotRunEventArgs>(Game_GameOver);
            _model.GameStuck += new EventHandler<RobotRunEventArgs>(Game_GameStuck);
            _dataAccess.GameTableSizeChanged += new EventHandler<int>(Game_SizeChanged);
            _gameSize = 11;
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += new EventHandler(On_Tick);
            GenerateTable();
            SetupTable();
            _model.NewGame(_gameSize);
            _isPaused = false;
            _timer.Start();
        }

        private void Game_GameStuck(object sender, RobotRunEventArgs e)
        {
            _timer.Stop();
            MessageBox.Show("The Robot got stuck! \n Time: " + e.GameTime);
            NewGame(_gameSize);
        }
        #endregion

        #region Game event handlers

        private void Game_GameOver(Object sender, RobotRunEventArgs e)
        {
            _timer.Stop();
            MessageBox.Show("Congartulations, you won! \n Time: " + e.GameTime);
            NewGame(_gameSize);
        }
        private void Game_TimeRefreshed(Object sender, RobotRunEventArgs e)
        {
            timeLabel1.Text = TimeSpan.FromSeconds(_model.gameTime).ToString("g");
        }

        private void Game_TableRefreshed(Object sender, RobotRunEventArgs e)
        {
            SetupTable();
        }

        private void ButtonGrid_MouseClick(Object sender, MouseEventArgs e)
        {
            Int32 x = ((sender as Button).TabIndex - 100) / _model.gameSize;
            Int32 y = ((sender as Button).TabIndex - 100) % _model.gameSize;

            _model.BuildWall(x, y);
            SetupTable();
        }

        private void Game_SizeChanged(Object sender, Int32 e)
        {
            NewGame(e);
        }
        #endregion

        #region Menu event handlers

        private void NewGameClicked(Object sender, EventArgs e)
        {
            NewGame(_gameSize);
        }
        private void SmallGameClicked(Object sender, EventArgs e)
        {
            NewGame(7);
        }

        private void MediumGameClicked(Object sender, EventArgs e)
        {
            NewGame(11);
        }

        private void LargeGameClicked(Object sender, EventArgs e)
        {
            NewGame(15);
        }

        private void PauseGameClicked(Object sender, EventArgs e)
        {
            if (_isPaused)
            {
                pauseItem.Text = "Pause";
                _isPaused = false;
                _timer.Start();
            }
            else
            {
                pauseItem.Text = "Resume";
                _isPaused = true;
                _timer.Stop();
            }
        }
        #endregion

        #region Time event handlers
        private void On_Tick(Object sender, EventArgs e)
        {
            _model.RefreshTime();
            _model.RefreshTable();
        }
        #endregion

        #region Private methods

        private void NewGame(Int32 n)
        {
            DeleteTable();
            _gameSize = n;
            GenerateTable();
            _model.NewGame(_gameSize);
            _isPaused = false;
            _timer.Start();
            SetupTable();
        }
        private void GenerateTable()
        {
            _buttonGrid = new Button[_gameSize, _gameSize];
            this.MinimumSize = new Size(_gameSize * 50 + 22, _gameSize * 50 + 117);
            this.MaximumSize = new Size(_gameSize * 50 + 22, _gameSize * 50 + 117);
            for (Int32 i = 0; i < _gameSize; ++i)
            {
                for (Int32 j = 0; j < _gameSize; ++j)
                {
                    _buttonGrid[i, j] = new Button();
                    _buttonGrid[i, j].Location = new Point(50 * j, 30 + 50 * i);
                    _buttonGrid[i, j].Size = new Size(50, 50);
                    _buttonGrid[i, j].TabIndex = 100 + i * _gameSize + j;
                    _buttonGrid[i, j].FlatStyle = FlatStyle.Flat;
                    _buttonGrid[i, j].FlatAppearance.BorderColor = Color.LightGray;
                    _buttonGrid[i, j].MouseClick += new MouseEventHandler(ButtonGrid_MouseClick);

                    Controls.Add(_buttonGrid[i, j]);
                }
            }
        }

        private void SetupTable()
        {
            for (Int32 i = 0; i < _gameSize; i++)
            {
                for (Int32 j = 0; j < _gameSize; ++j)
                {
                    switch (_model.GameTable.FieldBoard[i, j])
                    {
                        case Field.Empty:
                            _buttonGrid[i, j].BackgroundImage = null;
                            _buttonGrid[i, j].BackColor = ColorTranslator.FromHtml("#d2eaee");
                            break;
                        case Field.Robot:
                            _buttonGrid[i, j].BackColor = ColorTranslator.FromHtml("#d2eaee");
                            _buttonGrid[i, j].BackgroundImage = Properties.Resources.robot;
                            break;
                        case Field.Magnet:
                            _buttonGrid[i, j].BackColor = ColorTranslator.FromHtml("#d2eaee");
                            _buttonGrid[i, j].BackgroundImage = Properties.Resources.magnet;
                            break;
                        case Field.Wall:
                            _buttonGrid[i, j].BackgroundImage = Properties.Resources.wall;
                            break;
                        case Field.DestroyedWall:
                            _buttonGrid[i, j].BackgroundImage = Properties.Resources.destroyedwall;
                            break;
                    }
                }
            }
            timeLabel1.Text = TimeSpan.FromSeconds(_model.gameTime).ToString("g");
        }

        private void DeleteTable()
        {
            for (Int32 i = 0; i < _gameSize; i++)
            {
                for (Int32 j = 0; j < _gameSize; j++)
                {
                    if (_buttonGrid[i, j] != null)
                        _buttonGrid[i, j].Dispose();
                }
            }
        }

        private async void SaveGame_Clicked(Object sender, EventArgs e)
        {
            _timer.Stop();
            if (_saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    await _model.SaveGameAsync(_saveFileDialog.FileName);
                }
                catch (RobotRunDataException)
                {
                    MessageBox.Show("Játék mentése sikertelen!" + Environment.NewLine + "Hibás az elérési út, vagy a könyvtár nem írható.", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            _timer.Start();
        }

        private async void LoadGame_Clicked(Object sender, EventArgs e)
        {
            _timer.Stop();
            if (_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    await _model.LoadGameAsync(_openFileDialog.FileName);
                }
                catch (RobotRunDataException)
                {
                    MessageBox.Show("Játék betöltése sikertelen!" + Environment.NewLine + "Hibás az elérési út, vagy a fájlformátum.", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    _model.NewGame(_model.gameSize);
                }

                SetupTable();
            }
            _timer.Start();
        }
        #endregion
    }
}
