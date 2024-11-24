using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tic_Tac_Toe_Game.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Tic_Tac_Toe_Game
{
    public partial class formTicTacToeGame : Form
    {

        private Image defaultImage;
        public formTicTacToeGame()
        {
            InitializeComponent();
            defaultImage = Resources.play;
            ClearBoard();

        }
        public void ClearBoard()
        {

            foreach (Control c in Controls)
            {
                if (c is PictureBox)
                {
                    
                    PictureBox pictureBox = (PictureBox) c;
                    if (pictureBox != pictureBox1&& pictureBox != pbBlueHand 
                        && pictureBox != pbGreenHand)
                    {
                        pictureBox.Image = defaultImage;
                        pictureBox.Tag = "?";
                    } 
                }
            }
            PlayerTurn = enPlayer.PlayerX;
            lblTurn.Text = "Player X";
            lblWinner.Text = "In Progress";
            GameStatues.Winner = enWinner.GameInProgress;
            GameStatues.PlayCount = 0;
            GameStatues.GameOver = false;
            pbBlueHand.Visible = true ;
            pbGreenHand.Visible = false;
        }

        enum enWinner
        {
            PlayerX,PlayerO,Draw,GameInProgress
        }

        struct stGameStatues
        {
            public enWinner Winner;
            public bool GameOver;
            public short PlayCount;
        }

        stGameStatues GameStatues;
        enum enPlayer { PlayerX,PlayerO}

        private enPlayer PlayerTurn = enPlayer.PlayerX;

        public void ChangeImage(PictureBox pictureBox)
        {
            
            if (pictureBox.Tag.ToString() == "?")
            {
                switch(PlayerTurn)
                {
                    case enPlayer.PlayerX:
                        pictureBox.Image = Resources.x_button;
                        PlayerTurn = enPlayer.PlayerO;
                        pbBlueHand.Visible = false;
                        pbGreenHand.Visible = true;
                        GameStatues.PlayCount++;
                        lblTurn.Text = "Player X";
                        pictureBox.Tag = "X";
                        CheckWinner();
                        break;

                    case enPlayer.PlayerO:
                        pictureBox.Image = Resources.letter_o;
                        PlayerTurn = enPlayer.PlayerX;
                        pbBlueHand.Visible = true;
                        pbGreenHand.Visible = false;
                        GameStatues.PlayCount++;
                        lblTurn.Text = "Player O";
                        pictureBox.Tag = "O";
                        CheckWinner();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Please select another box", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (GameStatues.PlayCount == 9)
            {
                GameStatues.GameOver = true;
                GameStatues.Winner = enWinner.Draw;
                EndGame();
            }

            
        }

      
        public void EndGame()
        {
            lblTurn.Text = "Game Over";
            switch (GameStatues.Winner)
            {
                case enWinner.PlayerX:
                    lblWinner.Text = "Player X";
                    break;

                case enWinner.PlayerO:
                    lblWinner.Text = "Player O";
                    break;

                default:
                    lblWinner.Text = "Darw";
                    break;
            }   
            MessageBox.Show("Game Over","GameOver",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
      
        public bool CheckValue(PictureBox _pb1, PictureBox _pb2, PictureBox _pb3)
        {
            if (_pb1.Tag.ToString()!= "?" && 
                _pb1.Tag.ToString() == _pb2.Tag.ToString() &&
                _pb2.Tag.ToString() == _pb3.Tag.ToString())
            {
                if (_pb1.Tag.ToString() == "X")
                {
                    GameStatues.Winner = enWinner.PlayerX;
                    _pb1.Image = Resources.x_button__1_;
                    _pb2.Image = Resources.x_button__1_;
                    _pb3.Image = Resources.x_button__1_;
                }
                else
                {
                    GameStatues.Winner = enWinner.PlayerO;
                    _pb1.Image = Resources.o;
                    _pb2.Image = Resources.o;
                    _pb3.Image = Resources.o;
                }
                
                
                GameStatues.GameOver = true;
                EndGame();
                return true;
            }
            return false;
        }
        public void CheckWinner()
        {
            if (CheckValue(pb1, pb2, pb3)) return;
            if (CheckValue(pb4, pb5, pb6)) return;
            if (CheckValue(pb7, pb8, pb9)) return;

            if (CheckValue(pb1, pb4, pb7)) return;
            if (CheckValue(pb2, pb5, pb8)) return;
            if (CheckValue(pb3, pb6, pb9)) return;

            if (CheckValue(pb1, pb5, pb9)) return;
            if (CheckValue(pb3, pb5, pb7)) return;
            
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            ChangeImage((PictureBox) sender);
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            ClearBoard();
        }
    }
}
