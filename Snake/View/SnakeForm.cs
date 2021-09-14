using System;
using System.Drawing;
using System.Windows.Forms;
using Snake.Model;
using System.IO;
using Snake.Persistence;

namespace Snake
{
    public partial class SnakeForm : Form
    {
        #region Variables
        private Button[,] _buttonGrid;
        private SnakeGameModel _model;
        private Timer _timer;
        private ISnakeDataAccess _dataAccess;
        #endregion

        #region Constructor
        
        /// <summary>
        /// Constructor of window initializes values, assings handlers
        /// </summary>
        public SnakeForm()
        {
            InitializeComponent();

            this.Size = new Size(350, 350);
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            _newGame10MenuItem.Click += NewGameHandler;
            _newGame15MenuItem.Click += NewGameHandler;
            _newGame20MenuItem.Click += NewGameHandler;
            _fileQuitMenuItem.Click += QuitGameHandler;
            _pauseMenuItem.Click += PauseButtonHandler;
            _pauseMenuItem.Enabled = false;
            this.KeyDown += KeyPressHandler;
        }
        #endregion

        #region Form Handlers
        
        /// <summary>
        /// Handles new game selection and loads maps from files
        /// </summary>
        private async void NewGameHandler(Object sender, EventArgs e)
        {
            _dataAccess = new SnakeFileDataAccess();
            _model = new SnakeGameModel(_dataAccess);
            String filePath = "";
            
            if ((sender as ToolStripMenuItem).Text == "10 x 10")
            {
                filePath = @"..\..\..\Maps\n10Map.txt";
            }
            else if((sender as ToolStripMenuItem).Text == "15 x 15")
            {
                filePath = @"..\..\..\Maps\n15Map.txt";
            }
            else if((sender as ToolStripMenuItem).Text == "20 x 20")
            {
                filePath = @"..\..\..\Maps\n20Map.txt";
            }
            try
            {
                await _model.LoadGameAsync(filePath);
            }
            catch (SnakeDataException)
            {
                MessageBox.Show("Error while loading" + Environment.NewLine + "Wrong path or format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            _model.GameAdvanced += new EventHandler<SnakeEventArgs>(GameAdvancedHandler);
            _model.GameOver += new EventHandler<SnakeEventArgs>(GameOverHandler);
            GenerateTables();
            _timer = new Timer();
            _timer.Interval = 300;
            _timer.Tick += new EventHandler(Tick);
            _pauseMenuItem.Enabled = true;
            _pauseMenuItem.BackColor = Color.LightGreen;
            UpdateView();
        }

        /// <summary>
        /// Handles quitting the game via MenuItem
        /// </summary>
        private void QuitGameHandler(Object sender, EventArgs e)
        {
            bool bTimerState = _timer.Enabled;
            if(bTimerState)
            {
                _timer.Stop();
            }
            if (MessageBox.Show("Are you sure you wish to quit", "Snake? Snaaaaaake!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
            }
            else if(bTimerState)
            {

                _timer.Start();
            }
        }

        /// <summary>
        /// Handles the pause button 
        /// </summary>
        private void PauseButtonHandler(Object sender, EventArgs e)
        {
            if (_timer.Enabled)
            {
                _timer.Stop();
                _pauseMenuItem.BackColor = Color.LightGreen;
                _pauseMenuItem.Text = "Start";
            }
            else
            {
                _timer.Start();
                _pauseMenuItem.BackColor = Color.IndianRed;
                _pauseMenuItem.Text = "Pause";
            }
        }

        /// <summary>
        /// Handles A and D keys being pressed in order to change the direction of the snake
        /// </summary>
        private void KeyPressHandler(Object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (_timer.Enabled)
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                        _model.ChangeDirection(0);
                        break;
                    case Keys.D:
                        _model.ChangeDirection(1);
                        break;
                    default:
                        break;
                }
            }
        }
        
        /// <summary>
        /// Updates the eggcounter label
        /// </summary>
        private void GameAdvancedHandler(Object sender, SnakeEventArgs e)
        {
            _eggCountDataLabel.Text = _model.GetEggCount().ToString();
        }
        
        /// <summary>
        /// Handles game over, shows score and exits the program
        /// </summary>
        private void GameOverHandler(Object sender, SnakeEventArgs e)
        {
            _timer.Stop();
            MessageBox.Show("Game Over" + Environment.NewLine + "Eggs Eaten: " + _model.GetEggCount().ToString());
            Close();
        }
        #endregion

        #region Timer Handler
        
        /// <summary>
        /// Advances the game
        /// </summary>
        private void Tick(Object sender, EventArgs e)
        {
            _model.AdvanceGame();
            UpdateView();
        }
        #endregion

        #region Private Methods
        
        /// <summary>
        /// Handles the visual representation of the initial table
        /// </summary>
        private void GenerateTables()
        {
            Int32 tempMapSize = _model.GetSnakeTable().GetMapSize();
            _buttonGrid = new Button[tempMapSize, tempMapSize];
            _gridPanel.Controls.Clear();
            for (Int32 i = 0; i < tempMapSize; i++)
                for (Int32 j = 0; j < tempMapSize; j++)
                {
                    _buttonGrid[i, j] = new Button();
                    _buttonGrid[i, j].Location = new Point(35 * j,35 * i); // elhelyezkedés
                    _buttonGrid[i, j].Size = new Size(35, 35); // méret
                    _buttonGrid[i, j].Enabled = false; // kikapcsolt állapot
                    _buttonGrid[i, j].TabIndex = 100 + i * tempMapSize + j; // a gomb számát a TabIndex-ben tároljuk
                    _buttonGrid[i, j].FlatStyle = FlatStyle.Flat; // lapított stípus
                    _buttonGrid[i, j].FlatAppearance.BorderSize = 0;

                    _gridPanel.Controls.Add(_buttonGrid[i, j]);
                    this.Size = new Size((35 * tempMapSize) +17, (35 * tempMapSize) +88);
                }
        }

        /// <summary>
        /// Responds to changes in the table and updates the colours
        /// </summary>
        private void UpdateView()
        {
            for (int i = 0; i < _model.GetSnakeTable().GetMapSize(); i++)
            {
                for (int j = 0; j < _model.GetSnakeTable().GetMapSize(); j++)
                {
                    switch (_model.GetSnakeTable().GetMapValue(i,j))
                    {
                        case 0:
                            _buttonGrid[i, j].BackColor = Color.LightGray;
                            break;
                        case 1:
                            _buttonGrid[i, j].BackColor = Color.Coral;
                            break;
                        case 2:
                            _buttonGrid[i, j].BackColor = Color.LightPink;
                            break;
                        case 3:
                            _buttonGrid[i, j].BackColor = Color.LightYellow;
                            break;
                        case 4:
                            _buttonGrid[i, j].BackColor = Color.Black;
                            break;
                        default:
                            _buttonGrid[i, j].Text = "Not handled";
                            break;
                    }
                }
            }
        }
        #endregion
    }
}
