﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Tic_Tac_Toe
{
    /// <summary>Interaction logic for MainWindow.xaml</summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private string _currentPlayer = "X";
        private string _status;
        private string[,] _tttGrid = new string[3, 3];
        private int _player1Wins, _player2Wins, _tiegames;
        private bool _gameOver;

        #region Data Binding

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data Binding

        #region Properties

        public int Player1Wins
        {
            get { return _player1Wins; }
            set { _player1Wins = value; OnPropertyChanged("Player1Wins"); OnPropertyChanged("Player1WinsToString"); }
        }

        public string Player1WinsToString => "Player 1 Wins:\n" + Player1Wins.ToString("N0");

        public int Player2Wins
        {
            get { return _player2Wins; }
            set { _player2Wins = value; OnPropertyChanged("Player2Wins"); OnPropertyChanged("Player2WinsToString"); }
        }

        public string Player2WinsToString => "Player 2 Wins:\n" + Player2Wins.ToString("N0");

        public int TieGames
        {
            get { return _tiegames; }
            set { _tiegames = value; OnPropertyChanged("TieGames"); OnPropertyChanged("TieGamesToString"); }
        }

        public string TieGamesToString => "Tie Games:\n" + TieGames.ToString("N0");

        public string CurrentPlayer
        {
            get { return _currentPlayer; }
            set { _currentPlayer = value; OnPropertyChanged("CurrentPlayer"); OnPropertyChanged("CurrentPlayerTurn"); }
        }

        public string CurrentPlayerTurn => !GameOver ? CurrentPlayer + "'s Turn" : "";

        public string Status
        {
            get { return GameOver ? _status : ""; }
            set { _status = value; OnPropertyChanged("Status"); }
        }

        // ReSharper disable once InconsistentNaming
        public string[,] TTTGrid
        {
            get { return _tttGrid; }
            set { _tttGrid = value; OnPropertyChanged("TTTGrid"); }
        }

        public string TopLeft => TTTGrid[0, 0];
        public string TopCenter => TTTGrid[0, 1];
        public string TopRight => TTTGrid[0, 2];
        public string CenterLeft => TTTGrid[1, 0];
        public string Center => TTTGrid[1, 1];
        public string CenterRight => TTTGrid[1, 2];
        public string BottomLeft => TTTGrid[2, 0];
        public string BottomCenter => TTTGrid[2, 1];
        public string BottomRight => TTTGrid[2, 2];

        private bool TopRow => !string.IsNullOrEmpty(TopLeft) && TopLeft == TopCenter && TopLeft == TopRight;

        private bool CenterRow => !string.IsNullOrEmpty(CenterLeft) && CenterLeft == Center && CenterLeft == CenterRight
        ;

        private bool BottomRow => !string.IsNullOrEmpty(BottomLeft) && BottomLeft == BottomCenter && BottomLeft == BottomRight;
        private bool LeftColumn => !string.IsNullOrEmpty(TopLeft) && TopLeft == CenterLeft && TopLeft == BottomLeft;

        private bool CenterColumn
            => !string.IsNullOrEmpty(TopCenter) && TopCenter == Center && TopCenter == BottomCenter;

        private bool RightColumn
            => !string.IsNullOrEmpty(TopRight) && TopRight == CenterRight && TopRight == BottomRight;

        private bool DiagonalLeft => !string.IsNullOrEmpty(TopLeft) && TopLeft == Center && TopLeft == BottomRight;

        private bool DiagonalRight
            => !string.IsNullOrEmpty(BottomLeft) && BottomLeft == Center && BottomLeft == TopRight;

        public bool GameOver
        {
            get { return _gameOver; }
            set { _gameOver = value; OnPropertyChanged("GameOver"); OnPropertyChanged("Status"); OnPropertyChanged("CurrentPlayerTurn"); }
        }

        #endregion Properties

        /// <summary>Checks whether the board is full or completed with a win.</summary>
        /// <returns>Returns true if the board is full or completed with a win</returns>
        private bool CheckBoardState()
        {
            if (TopRow)
            {
                EndGame(true);
                BtnTopLeft.Foreground = Brushes.Blue;
                BtnTopCenter.Foreground = Brushes.Blue;
                BtnTopRight.Foreground = Brushes.Blue;
                return true;
            }
            else if (CenterRow)
            {
                EndGame(true);
                BtnCenterLeft.Foreground = Brushes.Blue;
                BtnCenter.Foreground = Brushes.Blue;
                BtnCenterRight.Foreground = Brushes.Blue;
                return true;
            }
            else if (BottomRow)
            {
                EndGame(true);
                BtnBottomLeft.Foreground = Brushes.Blue;
                BtnBottomCenter.Foreground = Brushes.Blue;
                BtnBottomRight.Foreground = Brushes.Blue;
                return true;
            }
            else if (LeftColumn)
            {
                EndGame(true);
                BtnTopLeft.Foreground = Brushes.Blue;
                BtnCenterLeft.Foreground = Brushes.Blue;
                BtnBottomLeft.Foreground = Brushes.Blue;
                return true;
            }
            else if (CenterColumn)
            {
                EndGame(true);
                BtnTopCenter.Foreground = Brushes.Blue;
                BtnCenter.Foreground = Brushes.Blue;
                BtnBottomCenter.Foreground = Brushes.Blue;
                return true;
            }
            else if (RightColumn)
            {
                EndGame(true);
                BtnTopRight.Foreground = Brushes.Blue;
                BtnCenterRight.Foreground = Brushes.Blue;
                BtnBottomRight.Foreground = Brushes.Blue;
                return true;
            }
            else if (DiagonalLeft)
            {
                EndGame(true);
                BtnTopLeft.Foreground = Brushes.Blue;
                BtnCenter.Foreground = Brushes.Blue;
                BtnBottomRight.Foreground = Brushes.Blue;
                return true;
            }
            else if (DiagonalRight)
            {
                EndGame(true);
                BtnTopRight.Foreground = Brushes.Blue;
                BtnCenter.Foreground = Brushes.Blue;
                BtnBottomLeft.Foreground = Brushes.Blue;
                return true;
            }

            int count = 0;
            foreach (string str in TTTGrid)
                if (!string.IsNullOrEmpty(str))
                    count += 1;
            if (count == 9)
            {
                EndGame(false);
                return true;
            }
            return false;
        }

        /// <summary>Ensures that the grid's state is current.</summary>
        private void UpdateGrid()
        {
            OnPropertyChanged("TopLeft");
            OnPropertyChanged("TopCenter");
            OnPropertyChanged("TopRight");
            OnPropertyChanged("CenterLeft");
            OnPropertyChanged("Center");
            OnPropertyChanged("CenterRight");
            OnPropertyChanged("BottomLeft");
            OnPropertyChanged("BottomCenter");
            OnPropertyChanged("BottomRight");
        }

        /// <summary>Resets all the Button colors back to black.</summary>
        private void ResetButtonColors()
        {
            BtnTopLeft.Foreground = Brushes.Black;
            BtnTopCenter.Foreground = Brushes.Black;
            BtnTopRight.Foreground = Brushes.Black;
            BtnCenterLeft.Foreground = Brushes.Black;
            BtnCenter.Foreground = Brushes.Black;
            BtnCenterRight.Foreground = Brushes.Black;
            BtnBottomLeft.Foreground = Brushes.Black;
            BtnBottomCenter.Foreground = Brushes.Black;
            BtnBottomRight.Foreground = Brushes.Black;
        }

        /// <summary>Assigns a specific grid location to the current player, assuming the space is available.</summary>
        /// <param name="vertical">Vertical position of the intended placement</param>
        /// <param name="horizontal">Horizontal position of the intended placement</param>
        private void ChooseGridLocation(int vertical, int horizontal)
        {
            if (!GameOver && string.IsNullOrWhiteSpace(TTTGrid[vertical, horizontal]))
            {
                TTTGrid[vertical, horizontal] = CurrentPlayer;
                UpdateGrid();
                CheckBoardState();
                CurrentPlayer = CurrentPlayer == "X" ? "O" : "X";
            }
        }

        /// <summary>Ends the current game.</summary>
        /// <param name="win">Was the game completed with a win?</param>
        private void EndGame(bool win)
        {
            if (win)
            {
                if (CurrentPlayer == "X")
                    Player1Wins += 1;
                else
                    Player2Wins += 1;
                Status = "Game Over! " + CurrentPlayer + " Wins!";
            }
            else
            {
                TieGames += 1;
                Status = "Tie Game!";
            }

            //ToggleGridButtons(false);
            GameOver = true;
            BtnNewGame.IsEnabled = true;
        }

        #region Window-Manipulation Methods

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        #endregion Window-Manipulation Methods

        #region Button-Click Methods

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnNewGame_Click(object sender, RoutedEventArgs e)
        {
            GameOver = false;
            BtnNewGame.IsEnabled = false;
            TTTGrid = new string[3, 3];
            DataContext = this;
            ResetButtonColors();
            UpdateGrid();
        }

        #endregion Button-Click Methods

        #region Grid Button-Click Methods

        private void BtnTopLeft_Click(object sender, RoutedEventArgs e)
        {
            ChooseGridLocation(0, 0);
        }

        private void BtnTopCenter_Click(object sender, RoutedEventArgs e)
        {
            ChooseGridLocation(0, 1);
        }

        private void BtnTopRight_Click(object sender, RoutedEventArgs e)
        {
            ChooseGridLocation(0, 2);
        }

        private void BtnCenterLeft_Click(object sender, RoutedEventArgs e)
        {
            ChooseGridLocation(1, 0);
        }

        private void BtnCenter_Click(object sender, RoutedEventArgs e)
        {
            ChooseGridLocation(1, 1);
        }

        private void BtnCenterRight_Click(object sender, RoutedEventArgs e)
        {
            ChooseGridLocation(1, 2);
        }

        private void BtnBottomLeft_Click(object sender, RoutedEventArgs e)
        {
            ChooseGridLocation(2, 0);
        }

        private void BtnBottomCenter_Click(object sender, RoutedEventArgs e)
        {
            ChooseGridLocation(2, 1);
        }

        private void BtnBottomRight_Click(object sender, RoutedEventArgs e)
        {
            ChooseGridLocation(2, 2);
        }

        #endregion Grid Button-Click Methods
    }
}