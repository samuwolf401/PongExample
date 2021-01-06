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
        int paddle1X = 20;
        int paddle1Y = 100;
        int player1Score = 0;

        bool player1Control = true;

        int paddle2X = 20;
        int paddle2Y = 200;
        int player2Score = 0;

        int paddleWidth = 10;
        int paddleHeight = 60;
        int paddleSpeed = 5;

        int ballX = 250;
        int ballY = 195;
        int ballXSpeed = -6;
        int ballYSpeed = -6;
        int ballWidth = 10;
        int ballHeight = 10;

        bool wDown = false;
        bool sDown = false;
        bool dRight = false;
        bool aLeft = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool rightArrowDown = false;
        bool leftArrowDown = false;

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
                case Keys.D:
                    dRight = true;
                    break;
                case Keys.A:
                    aLeft = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
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
                case Keys.D:
                    dRight = false;
                    break;
                case Keys.A:
                    aLeft = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
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

            if (dRight == true && paddle1X < 480)
            {
                paddle1X += paddleSpeed;
            }

            if (aLeft == true && paddle1X > 0)
            {
                paddle1X -= paddleSpeed;
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
            if (rightArrowDown == true && paddle2X < 480)
            {
                paddle2X += paddleSpeed;
            }

            if (leftArrowDown == true && paddle2X > 0)
            {
                paddle2X -= paddleSpeed;
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
                if (player1Control)
                {
                    ballXSpeed *= -1;
                    ballX = paddle1X + paddleWidth + 1;
                }
            }
            else if (player2Rec.IntersectsWith(ballRec))
            {
                if (!player1Control)
                {
                    ballXSpeed *= -1;
                    ballX = paddle2X + ballWidth + 1;
                }
            }

            if (ballX < -5)
            {
                ballX = 440;
                ballY = 195;

                paddle1Y = 100;
                paddle1X = 20;
                paddle2Y = 200;
                paddle2X = 20;

                switch (player1Control)
                {
                    case true:
                        player2Score++;
                        break;
                    case false:
                        player1Score++;
                        break;
                }
            }
            else if (ballX > 480)
            {
                ballXSpeed = ballXSpeed * -1;
                player1Control = !player1Control;

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

            if (player1Control)
            {
                e.Graphics.FillRectangle(whiteBrush, paddle1X - 2, paddle1Y - 2, paddleWidth + 4, paddleHeight + 4);
            }
            else
            {
                e.Graphics.FillRectangle(whiteBrush, paddle2X - 2, paddle2Y - 2, paddleWidth+4, paddleHeight+4);
            }
            e.Graphics.FillRectangle(blueBrush, paddle1X, paddle1Y, paddleWidth, paddleHeight);
            e.Graphics.FillRectangle(blueBrush, paddle2X, paddle2Y, paddleWidth, paddleHeight);
            

            e.Graphics.DrawString($"{player1Score}", screenFont, whiteBrush, 225, 10);
            e.Graphics.DrawString($"{player2Score}", screenFont, whiteBrush, 255, 10);

        }
    }
}
