using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PongExample
{
    public partial class Form1 : Form
    {
        int paddle1X = 10;
        int paddle1Y = 170;
        int player1Score = 0;

        int paddle2X = 580;
        int paddle2Y = 170;
        int player2Score = 0;

        int paddleWidth = 10;
        int paddleHeight = 60;
        int paddleSpeed = 4;

        int ballX = 295;
        int ballY = 195;
        int ballXSpeed = 6;
        int ballYSpeed = -6;
        int ballWidth = 10;
        int ballHeight = 10;

        bool wDown = false;
        bool sDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        Font screenFont = new Font("Consolas", 12);


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
            }

        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move ball
            ballX += ballXSpeed;
            ballY += ballYSpeed;

            //move player 1
            if (wDown == true && paddle1Y > 0)
            {
                paddle1Y -= paddleSpeed;
            }

            if (sDown == true && paddle1Y < this.Height - paddleHeight)
            {
                paddle1Y += paddleSpeed;
            }

            //move player 2
            if (upArrowDown == true && paddle2Y > 0)
            {
                paddle2Y -= paddleSpeed;
            }

            if (downArrowDown == true && paddle2Y < this.Height - paddleHeight)
            {
                paddle2Y += paddleSpeed;
            }

            //top and bottom wall collision
            if (ballY < 0 || ballY > this.Height - ballHeight)
            {
                ballYSpeed *= -1;  // or: ballYSpeed = -ballYSpeed;
            }

            //create Rectangles of objects on screen to be used for collision detection
            Rectangle player1Rec = new Rectangle(paddle1X, paddle1Y, paddleWidth, paddleHeight);
            Rectangle player2Rec = new Rectangle(paddle2X, paddle2Y, paddleWidth, paddleHeight);
            Rectangle ballRec = new Rectangle(ballX, ballY, ballWidth, ballHeight);

            //check if ball hits either paddle. If it does change the direction
            //and place the ball in front of the paddle hit
            if (player1Rec.IntersectsWith(ballRec))
            {
                ballXSpeed *= -1;
                ballX = paddle1X + paddleWidth + 1;
            }
            else if (player2Rec.IntersectsWith(ballRec))
            {
                ballXSpeed *= -1;
                ballX = paddle2X - ballWidth - 1;
            }

            if (ballX < 0)
            {
                player2Score++;
                ballX = 295;
                ballY = 195;

                paddle1Y = 170;
                paddle2Y = 170;
            }
            else if (ballX > 600)
            {
                player1Score++;

                ballX = 295;
                ballY = 195;

                paddle1Y = 170;
                paddle2Y = 170;
            }

            if (player1Score == 3 || player2Score == 3)
            {
                gameTimer.Enabled = false;
            }


            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(whiteBrush, ballX, ballY, ballWidth, ballHeight);

            e.Graphics.FillRectangle(blueBrush, paddle1X, paddle1Y, paddleWidth, paddleHeight);
            e.Graphics.FillRectangle(blueBrush, paddle2X, paddle2Y, paddleWidth, paddleHeight);
            

            e.Graphics.DrawString($"{player1Score}", screenFont, whiteBrush, 280, 10);
            e.Graphics.DrawString($"{player2Score}", screenFont, whiteBrush, 310, 10);

        }
    }
}
