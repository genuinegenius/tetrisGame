using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TetrisGame
{
    public partial class Form1 : Form
    {
        private Color[] blockEmptyColor;
        private Random rand;
        private Timer timer;
        private int tetrisPiecePos;
        private int tetrisPiecePos2;
        private int tetrisPiecePos3;
        private int tetrisPiecePos4;
        private int score;
        private int nivel;
        private int nivelcount;
        private int pieceType;
        private bool[] blocksFilled;

        public Form1()
        {
            InitializeComponent();

            //
            // square labels
            //
            int blockNum = 0;
            int blockPosX = 0;
            int blockRow = 0;
            foreach (System.Windows.Forms.Label block in gameLabel)
            {
                gameLabel[blockNum] = new System.Windows.Forms.Label();
                this.gameLabel[blockNum].BackColor = Color.Gray;
                this.gameLabel[blockNum].Location = new System.Drawing.Point(26 * blockPosX, 26 * blockRow);
                this.gameLabel[blockNum].Name = "blockLabel" + blockNum.ToString();
                this.gameLabel[blockNum].Size = new System.Drawing.Size(25, 25);
                this.gameLabel[blockNum].TabIndex = blockNum;
                blockNum++;
                blockPosX++;
                if (blockNum % 10 == 0)
                {
                    blockRow++;
                    blockPosX = 0;
                }
            }
            blockNum = 0;
            foreach (System.Windows.Forms.Label block in gameLabel)
            {
                this.gamePanel.Controls.Add(this.gameLabel[blockNum]);
                blockNum++;
            }
            //
            // random piece
            //
            rand = new Random();
            pieceType = rand.Next(5);
            
            //
            // set timer
            //

            timer = new Timer();
            timer.Interval = 200;
            timer.Enabled = true;
            timer.Tick += new System.EventHandler(TimerTickEvent);

            score = 0;
            nivel = 1;
            nivelcount = 100;
            label4.Text = nivel.ToString();

            blockEmptyColor = new Color[2];
            blocksFilled = new bool[200];

            if (pieceType == 0)
            {
                tetrisPiecePos = -7;
                tetrisPiecePos2 = -6;
                tetrisPiecePos3 = -5;
                tetrisPiecePos4 = -4;
            }
            if (pieceType == 1)
            {
                tetrisPiecePos = -6;
                tetrisPiecePos2 = -5;
                tetrisPiecePos3 = tetrisPiecePos + 10;
                tetrisPiecePos4 = tetrisPiecePos2 + 10;
            }
            if(pieceType == 2)
            {
                tetrisPiecePos = -6;
                tetrisPiecePos2 = -5;
                tetrisPiecePos3 = 5;
                tetrisPiecePos4 = 6;
            }
            if(pieceType == 3)
            {
                tetrisPiecePos = -4;
                tetrisPiecePos2 = -5;
                tetrisPiecePos3 = 5;
                tetrisPiecePos4 = 4;
            }
            if(pieceType == 4)
            {
                tetrisPiecePos = -6;
                tetrisPiecePos2 = 4;
                tetrisPiecePos3 = 5;
                tetrisPiecePos4 = 6;
            }
            if (pieceType == 5)
            {
                tetrisPiecePos = -4;
                tetrisPiecePos2 = 6;
                tetrisPiecePos3 = 5;
                tetrisPiecePos4 = 4;
            }

            //setting up the colors for labels
            blockEmptyColor[0] = Color.Gray;
            blockEmptyColor[1] = Color.Green;

            foreach(Label label in GameLabel)
            {
                label.BackColor = blockEmptyColor[0];
            }
            
            timer.Start();

        }

        private void TimerTickEvent(object sender, EventArgs e)
        {
            if (pieceType == 0)
            {
                // move of pieces
                tetrisPiecePos += 10;
                tetrisPiecePos2 += 10;
                tetrisPiecePos3 += 10;
                tetrisPiecePos4 += 10;

                //if pieces don't go beyond map and hit a block it stop and start again from top
                if (tetrisPiecePos < 200 && tetrisPiecePos2 < 200 && tetrisPiecePos3 < 200 && tetrisPiecePos4 < 200)
                {
                    if (GameLabel[tetrisPiecePos].BackColor != blockEmptyColor[0])
                    {
                        //erasing lines from the screen if full and movind down stack
                        int count, index;
                        for (int i = 19; i >= 0; i--)
                        {
                            count = 0;
                            for (int j = 9; j >= 0; j--)
                            {
                                index = i * 10 + j;
                                if (GameLabel[index].BackColor != blockEmptyColor[0])
                                {
                                    count++;
                                }
                            }
                            index = i * 10;
                            if (count == 10)
                            {
                                for (int p = index; p < index + 10; p++)
                                {
                                    GameLabel[p].BackColor = blockEmptyColor[0];
                                    blocksFilled[p] = false;
                                }
                                for (int p = index - 1; p >= 10; p--)
                                {
                                    if (GameLabel[p].BackColor != blockEmptyColor[0])
                                    {
                                        blocksFilled[p] = false;
                                        GameLabel[p].BackColor = blockEmptyColor[0];
                                        int countdown = 10;
                                        GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                    }
                                }
                                score += 10;
                                i++;
                            }   
                        }
                        rand = new Random();
                        pieceType = rand.Next(5);
                        if (pieceType == 1)
                        {
                            tetrisPiecePos = -6;
                            tetrisPiecePos2 = -5;
                            tetrisPiecePos3 = tetrisPiecePos + 10;
                            tetrisPiecePos4 = tetrisPiecePos2 + 10;
                        }
                        else
                        {
                            if (pieceType == 0)
                            {
                                tetrisPiecePos = -7;
                                tetrisPiecePos2 = -6;
                                tetrisPiecePos3 = -5;
                                tetrisPiecePos4 = -4;
                            }
                            else
                            {
                                if (pieceType == 2)
                                {
                                    tetrisPiecePos = -6;
                                    tetrisPiecePos2 = -5;
                                    tetrisPiecePos3 = tetrisPiecePos2 + 10;
                                    tetrisPiecePos4 = tetrisPiecePos3 + 1;
                                }
                                else
                                {
                                    if(pieceType == 3)
                                    {
                                        tetrisPiecePos = -4;
                                        tetrisPiecePos2 = -5;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 4;
                                    }
                                    else
                                    {
                                        if(pieceType == 4)
                                        {
                                            tetrisPiecePos = -6;
                                            tetrisPiecePos2 = 4;
                                            tetrisPiecePos3 = 5;
                                            tetrisPiecePos4 = 6;
                                        }
                                        else
                                        {
                                            if(pieceType == 5)
                                            {
                                                tetrisPiecePos = -4;
                                                tetrisPiecePos = 6;
                                                tetrisPiecePos = 5;
                                                tetrisPiecePos = 4;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if(GameLabel[tetrisPiecePos2].BackColor != blockEmptyColor[0])
                        {
                            //erasing lines from the screen if full and movind down stack
                            int count, index;
                            for (int i = 19; i >= 0; i--)
                            {
                                count = 0;
                                for (int j = 9; j >= 0; j--)
                                {
                                    index = i * 10 + j;
                                    if (GameLabel[index].BackColor == blockEmptyColor[1])
                                    {
                                        count++;
                                    }
                                }
                                index = i * 10;
                                if (count == 10)
                                {
                                    for (int p = index; p < index + 10; p++)
                                    {
                                        GameLabel[p].BackColor = blockEmptyColor[0];
                                        blocksFilled[p] = false;
                                    }
                                    for (int p = index - 1; p >= 10; p--)
                                    {
                                        if (GameLabel[p].BackColor == blockEmptyColor[1])
                                        {
                                            blocksFilled[p] = false;
                                            GameLabel[p].BackColor = blockEmptyColor[0];
                                            int countdown = 10;
                                            GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                        }
                                    }
                                    score += 10;
                                    i++;
                                }
                            }
                            rand = new Random();
                            pieceType = rand.Next(5);
                            if (pieceType == 1)
                            {
                                tetrisPiecePos = -6;
                                tetrisPiecePos2 = -5;
                                tetrisPiecePos3 = tetrisPiecePos + 10;
                                tetrisPiecePos4 = tetrisPiecePos2 + 10;
                            }
                            else
                            {
                                if (pieceType == 0)
                                {
                                    tetrisPiecePos = -7;
                                    tetrisPiecePos2 = -6;
                                    tetrisPiecePos3 = -5;
                                    tetrisPiecePos4 = -4;
                                }
                                else
                                {
                                    if (pieceType == 2)
                                    {
                                        tetrisPiecePos = -6;
                                        tetrisPiecePos2 = -5;
                                        tetrisPiecePos3 = tetrisPiecePos2 + 10;
                                        tetrisPiecePos4 = tetrisPiecePos3 + 1;
                                    }
                                    else
                                    {
                                        if (pieceType == 3)
                                        {
                                            tetrisPiecePos = -4;
                                            tetrisPiecePos2 = -5;
                                            tetrisPiecePos3 = 5;
                                            tetrisPiecePos4 = 4;
                                        }
                                        else
                                        {
                                            if (pieceType == 4)
                                            {
                                                tetrisPiecePos = -6;
                                                tetrisPiecePos2 = 4;
                                                tetrisPiecePos3 = 5;
                                                tetrisPiecePos4 = 6;
                                            }
                                            else
                                            {
                                                if (pieceType == 5)
                                                {
                                                    tetrisPiecePos = -4;
                                                    tetrisPiecePos = 6;
                                                    tetrisPiecePos = 5;
                                                    tetrisPiecePos = 4;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if(GameLabel[tetrisPiecePos3].BackColor != blockEmptyColor[0])
                            {
                                //erasing lines from the screen if full and movind down stack
                                int count, index;
                                for (int i = 19; i >= 0; i--)
                                {
                                    count = 0;
                                    for (int j = 9; j >= 0; j--)
                                    {
                                        index = i * 10 + j;
                                        if (GameLabel[index].BackColor == blockEmptyColor[1])
                                        {
                                            count++;
                                        }
                                    }
                                    index = i * 10;
                                    if (count == 10)
                                    {
                                        for (int p = index; p < index + 10; p++)
                                        {
                                            GameLabel[p].BackColor = blockEmptyColor[0];
                                            blocksFilled[p] = false;
                                        }
                                        for (int p = index - 1; p >= 10; p--)
                                        {
                                            if (GameLabel[p].BackColor == blockEmptyColor[1])
                                            {
                                                blocksFilled[p] = false;
                                                GameLabel[p].BackColor = blockEmptyColor[0];
                                                int countdown = 10;
                                                GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                            }
                                        }
                                        score += 10;
                                        i++;
                                    }
                                }
                                rand = new Random();
                                pieceType = rand.Next(5);
                                if (pieceType == 1)
                                {
                                    tetrisPiecePos = -6;
                                    tetrisPiecePos2 = -5;
                                    tetrisPiecePos3 = tetrisPiecePos + 10;
                                    tetrisPiecePos4 = tetrisPiecePos2 + 10;
                                }
                                else
                                {
                                    if (pieceType == 0)
                                    {
                                        tetrisPiecePos = -7;
                                        tetrisPiecePos2 = -6;
                                        tetrisPiecePos3 = -5;
                                        tetrisPiecePos4 = -4;
                                    }
                                    else
                                    {
                                        if (pieceType == 2)
                                        {
                                            tetrisPiecePos = -6;
                                            tetrisPiecePos2 = -5;
                                            tetrisPiecePos3 = tetrisPiecePos2 + 10;
                                            tetrisPiecePos4 = tetrisPiecePos3 + 1;
                                        }
                                        else
                                        {
                                            if (pieceType == 3)
                                            {
                                                tetrisPiecePos = -4;
                                                tetrisPiecePos2 = -5;
                                                tetrisPiecePos3 = 5;
                                                tetrisPiecePos4 = 4;
                                            }
                                            else
                                            {
                                                if (pieceType == 4)
                                                {
                                                    tetrisPiecePos = -6;
                                                    tetrisPiecePos2 = 4;
                                                    tetrisPiecePos3 = 5;
                                                    tetrisPiecePos4 = 6;
                                                }
                                                else
                                                {
                                                    if (pieceType == 5)
                                                    {
                                                        tetrisPiecePos = -4;
                                                        tetrisPiecePos = 6;
                                                        tetrisPiecePos = 5;
                                                        tetrisPiecePos = 4;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if(GameLabel[tetrisPiecePos4].BackColor != blockEmptyColor[0])
                                {
                                    //erasing lines from the screen if full and movind down stack
                                    int count, index;
                                    for (int i = 19; i >= 0; i--)
                                    {
                                        count = 0;
                                        for (int j = 9; j >= 0; j--)
                                        {
                                            index = i * 10 + j;
                                            if (GameLabel[index].BackColor == blockEmptyColor[1])
                                            {
                                                count++;
                                            }
                                        }
                                        index = i * 10;
                                        if (count == 10)
                                        {
                                            for (int p = index; p < index + 10; p++)
                                            {
                                                GameLabel[p].BackColor = blockEmptyColor[0];
                                                blocksFilled[p] = false;
                                            }
                                            for (int p = index - 1; p >= 10; p--)
                                            {
                                                if (GameLabel[p].BackColor == blockEmptyColor[1])
                                                {
                                                    blocksFilled[p] = false;
                                                    GameLabel[p].BackColor = blockEmptyColor[0];
                                                    int countdown = 10;
                                                    GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                                }
                                            }
                                            score += 10;
                                            i++;
                                        }
                                    }
                                    rand = new Random();
                                    pieceType = rand.Next(5);
                                    if (pieceType == 1)
                                    {
                                        tetrisPiecePos = -6;
                                        tetrisPiecePos2 = -5;
                                        tetrisPiecePos3 = tetrisPiecePos + 10;
                                        tetrisPiecePos4 = tetrisPiecePos2 + 10;
                                    }
                                    else
                                    {
                                        if (pieceType == 0)
                                        {
                                            tetrisPiecePos = -7;
                                            tetrisPiecePos2 = -6;
                                            tetrisPiecePos3 = -5;
                                            tetrisPiecePos4 = -4;
                                        }
                                        else
                                        {
                                            if (pieceType == 2)
                                            {
                                                tetrisPiecePos = -6;
                                                tetrisPiecePos2 = -5;
                                                tetrisPiecePos3 = tetrisPiecePos2 + 10;
                                                tetrisPiecePos4 = tetrisPiecePos3 + 1;
                                            }
                                            else
                                            {
                                                if (pieceType == 3)
                                                {
                                                    tetrisPiecePos = -4;
                                                    tetrisPiecePos2 = -5;
                                                    tetrisPiecePos3 = 5;
                                                    tetrisPiecePos4 = 4;
                                                }
                                                else
                                                {
                                                    if (pieceType == 4)
                                                    {
                                                        tetrisPiecePos = -6;
                                                        tetrisPiecePos2 = 4;
                                                        tetrisPiecePos3 = 5;
                                                        tetrisPiecePos4 = 6;
                                                    }
                                                    else
                                                    {
                                                        if (pieceType == 5)
                                                        {
                                                            tetrisPiecePos = -4;
                                                            tetrisPiecePos = 6;
                                                            tetrisPiecePos = 5;
                                                            tetrisPiecePos = 4;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //if piece cross the map a new piece start from top
                if (tetrisPiecePos > 200 && tetrisPiecePos2 > 200 && tetrisPiecePos4 > 200 && tetrisPiecePos4 > 200)
                {
                    //erasing lines from the screen if full and movind down stack
                    int count, index;
                    for (int i = 19; i >= 0; i--)
                    {
                        count = 0;
                        for (int j = 9; j >= 0; j--)
                        {
                            index = i * 10 + j;
                            if (GameLabel[index].BackColor == blockEmptyColor[1])
                            {
                                count++;
                            }
                        }
                        index = i * 10;
                        if (count == 10)
                        {
                            for (int p = index; p < index + 10; p++)
                            {
                                GameLabel[p].BackColor = blockEmptyColor[0];
                                blocksFilled[p] = false;
                            }
                            for (int p = index - 1; p >= 10; p--)
                            {
                                if (GameLabel[p].BackColor == blockEmptyColor[1])
                                {
                                    blocksFilled[p] = false;
                                    GameLabel[p].BackColor = blockEmptyColor[0];
                                    int countdown = 10;
                                    GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                }
                            }
                            score += 10;
                            i++;
                        }
                    }
                    rand = new Random();
                    pieceType = rand.Next(5);
                    if (pieceType == 1)
                    {
                        tetrisPiecePos = -6;
                        tetrisPiecePos2 = -5;
                        tetrisPiecePos3 = tetrisPiecePos + 10;
                        tetrisPiecePos4 = tetrisPiecePos2 + 10;
                    }
                    else
                    {
                        if (pieceType == 0)
                        {
                            tetrisPiecePos = -7;
                            tetrisPiecePos2 = -6;
                            tetrisPiecePos3 = -5;
                            tetrisPiecePos4 = -4;
                        }
                        else
                        {
                            if (pieceType == 2)
                            {
                                tetrisPiecePos = -6;
                                tetrisPiecePos2 = -5;
                                tetrisPiecePos3 = tetrisPiecePos2 + 10;
                                tetrisPiecePos4 = tetrisPiecePos3 + 1;
                            }
                            else
                            {
                                if (pieceType == 3)
                                {
                                    tetrisPiecePos = -4;
                                    tetrisPiecePos2 = -5;
                                    tetrisPiecePos3 = 5;
                                    tetrisPiecePos4 = 4;
                                }
                                else
                                {
                                    if (pieceType == 4)
                                    {
                                        tetrisPiecePos = -6;
                                        tetrisPiecePos2 = 4;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 6;
                                    }
                                    else
                                    {
                                        if (pieceType == 5)
                                        {
                                            tetrisPiecePos = -4;
                                            tetrisPiecePos = 6;
                                            tetrisPiecePos = 5;
                                            tetrisPiecePos = 4;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //erasing the last positions of pieces from screen
                if (tetrisPiecePos < 200 && tetrisPiecePos2 < 200 && tetrisPiecePos3 < 200 && tetrisPiecePos4 < 200 && pieceType == 0 && tetrisPiecePos > 0)
                {
                    if (GameLabel[tetrisPiecePos].BackColor == blockEmptyColor[0] && GameLabel[tetrisPiecePos2].BackColor == blockEmptyColor[0] &&
                        GameLabel[tetrisPiecePos3].BackColor == blockEmptyColor[0] && GameLabel[tetrisPiecePos4].BackColor == blockEmptyColor[0])
                    {
                        GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                        if (tetrisPiecePos - 10 > 0)
                        {
                            GameLabel[tetrisPiecePos - 10].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos2 - 10].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos3 - 10].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos4 - 10].BackColor = blockEmptyColor[0];
                        }
                    }
                }
                
                //if the piece at the top blockfilled vector is filled with the new info about labels ( true-if green / false-if gray-default)
                if (tetrisPiecePos == -7 && tetrisPiecePos2 == -6 && tetrisPiecePos3 == -5 && tetrisPiecePos4 == -4)
                {
                    int k = 0;
                    foreach (Label block in GameLabel)
                    {
                        if (block.BackColor == blockEmptyColor[1])
                        {
                            blocksFilled[k] = true;
                        }
                        k++;
                    }
                }
            }
            if (pieceType == 1)
            {
                
                // move of pieces
                tetrisPiecePos += 10;
                tetrisPiecePos2 += 10;
                tetrisPiecePos3 = tetrisPiecePos + 10;
                tetrisPiecePos4 = tetrisPiecePos2 + 10;

                //if hit a block it stop and start again from top
                if (tetrisPiecePos3 < 200 && tetrisPiecePos4 < 200)
                {
                    if (GameLabel[tetrisPiecePos3].BackColor != blockEmptyColor[0])
                    {
                        //erasing lines from the screen if full and movind down stack
                        int count, index;
                        for (int i = 19; i >= 0; i--)
                        {
                            count = 0;
                            for (int j = 9; j >= 0; j--)
                            {
                                index = i * 10 + j;
                                if (GameLabel[index].BackColor == blockEmptyColor[1])
                                {
                                    count++;
                                }
                            }
                            index = i * 10;
                            if (count == 10)
                            {
                                for (int p = index; p < index + 10; p++)
                                {
                                    GameLabel[p].BackColor = blockEmptyColor[0];
                                    blocksFilled[p] = false;
                                }
                                for (int p = index - 1; p >= 10; p--)
                                {
                                    if(GameLabel[p].BackColor == blockEmptyColor[1])
                                    {
                                        blocksFilled[p] = false;
                                        GameLabel[p].BackColor = blockEmptyColor[0];
                                        int countdown = 10;
                                        GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                    }
                                }
                                score += 10;
                                i++;
                            }
                        }
                        rand = new Random();
                        pieceType = rand.Next(5);
                        if (pieceType == 1)
                        {
                            tetrisPiecePos = -6;
                            tetrisPiecePos2 = -5;
                            tetrisPiecePos3 = tetrisPiecePos + 10;
                            tetrisPiecePos4 = tetrisPiecePos2 + 10;
                        }
                        else
                        {
                            if(pieceType == 0)
                            {
                                tetrisPiecePos = -7;
                                tetrisPiecePos2 = -6;
                                tetrisPiecePos3 = -5;
                                tetrisPiecePos4 = -4;
                            }
                            else
                            {
                                if (pieceType == 2)
                                {
                                    tetrisPiecePos = -6;
                                    tetrisPiecePos2 = -5;
                                    tetrisPiecePos3 = tetrisPiecePos2 + 10;
                                    tetrisPiecePos4 = tetrisPiecePos3 + 1;
                                }
                                else
                                {
                                    if (pieceType == 3)
                                    {
                                        tetrisPiecePos = -4;
                                        tetrisPiecePos2 = -5;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 4;
                                    }
                                    else
                                    {
                                        if (pieceType == 4)
                                        {
                                            tetrisPiecePos = -6;
                                            tetrisPiecePos2 = 4;
                                            tetrisPiecePos3 = 5;
                                            tetrisPiecePos4 = 6;
                                        }
                                        else
                                        {
                                            if (pieceType == 5)
                                            {
                                                tetrisPiecePos = -4;
                                                tetrisPiecePos = 6;
                                                tetrisPiecePos = 5;
                                                tetrisPiecePos = 4;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if(GameLabel[tetrisPiecePos4].BackColor != blockEmptyColor[0])
                        {
                            //erasing lines from the screen if full and movind down stack
                            int count, index;
                            for (int i = 19; i >= 0; i--)
                            {
                                count = 0;
                                for (int j = 9; j >= 0; j--)
                                {
                                    index = i * 10 + j;
                                    if (GameLabel[index].BackColor == blockEmptyColor[1])
                                    {
                                        count++;
                                    }
                                }
                                index = i * 10;
                                if (count == 10)
                                {
                                    for (int p = index; p < index + 10; p++)
                                    {
                                        GameLabel[p].BackColor = blockEmptyColor[0];
                                        blocksFilled[p] = false;
                                    }
                                    for (int p = index - 1; p >= 10; p--)
                                    {
                                        if (GameLabel[p].BackColor == blockEmptyColor[1])
                                        {
                                            blocksFilled[p] = false;
                                            GameLabel[p].BackColor = blockEmptyColor[0];
                                            int countdown = 10;
                                            GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                        }
                                    }
                                    score += 10;
                                    i++;
                                }
                            }
                            rand = new Random();
                            pieceType = rand.Next(5);
                            if (pieceType == 1)
                            {
                                tetrisPiecePos = -6;
                                tetrisPiecePos2 = -5;
                                tetrisPiecePos3 = tetrisPiecePos + 10;
                                tetrisPiecePos4 = tetrisPiecePos2 + 10;
                            }
                            else
                            {
                                if (pieceType == 0)
                                {
                                    tetrisPiecePos = -7;
                                    tetrisPiecePos2 = -6;
                                    tetrisPiecePos3 = -5;
                                    tetrisPiecePos4 = -4;
                                }
                                else
                                {
                                    if (pieceType == 2)
                                    {
                                        tetrisPiecePos = -6;
                                        tetrisPiecePos2 = -5;
                                        tetrisPiecePos3 = tetrisPiecePos2 + 10;
                                        tetrisPiecePos4 = tetrisPiecePos3 + 1;
                                    }
                                    else
                                    {
                                        if (pieceType == 3)
                                        {
                                            tetrisPiecePos = -4;
                                            tetrisPiecePos2 = -5;
                                            tetrisPiecePos3 = 5;
                                            tetrisPiecePos4 = 4;
                                        }
                                        else
                                        {
                                            if (pieceType == 4)
                                            {
                                                tetrisPiecePos = -6;
                                                tetrisPiecePos2 = 4;
                                                tetrisPiecePos3 = 5;
                                                tetrisPiecePos4 = 6;
                                            }
                                            else
                                            {
                                                if (pieceType == 5)
                                                {
                                                    tetrisPiecePos = -4;
                                                    tetrisPiecePos = 6;
                                                    tetrisPiecePos = 5;
                                                    tetrisPiecePos = 4;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //if piece cross the map a new piece start from top
                if (tetrisPiecePos4 > 200 && tetrisPiecePos3 > 200)
                {
                    //erasing lines from the screen if full and movind down stack
                    int count, index;
                    for (int i = 19; i >= 0; i--)
                    {
                        count = 0;
                        for (int j = 9; j >= 0; j--)
                        {
                            index = i * 10 + j;
                            if (GameLabel[index].BackColor == blockEmptyColor[1])
                            {
                                count++;
                            }
                        }
                        index = i * 10;
                        if (count == 10)
                        {
                            for (int p = index; p < index + 10; p++)
                            {
                                GameLabel[p].BackColor = blockEmptyColor[0];
                                blocksFilled[p] = false;
                            }
                            for (int p = index - 1; p >= 10; p--)
                            {
                                if (GameLabel[p].BackColor == blockEmptyColor[1])
                                {
                                    blocksFilled[p] = false;
                                    GameLabel[p].BackColor = blockEmptyColor[0];
                                    int countdown = 10;
                                    GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                }
                            }
                            score += 10;
                            i++;
                        }
                    }
                    rand = new Random();
                    pieceType = rand.Next(5);
                    if (pieceType == 1)
                    {
                        tetrisPiecePos = -6;
                        tetrisPiecePos2 = -5;
                        tetrisPiecePos3 = tetrisPiecePos + 10;
                        tetrisPiecePos4 = tetrisPiecePos2 + 10;
                    }
                    else
                    {
                        if (pieceType == 0)
                        {
                            tetrisPiecePos = -7;
                            tetrisPiecePos2 = -6;
                            tetrisPiecePos3 = -5;
                            tetrisPiecePos4 = -4;
                        }
                        else
                        {
                            if (pieceType == 2)
                            {
                                tetrisPiecePos = -6;
                                tetrisPiecePos2 = -5;
                                tetrisPiecePos3 = tetrisPiecePos2 + 10;
                                tetrisPiecePos4 = tetrisPiecePos3 + 1;
                            }
                            else
                            {
                                if (pieceType == 3)
                                {
                                    tetrisPiecePos = -4;
                                    tetrisPiecePos2 = -5;
                                    tetrisPiecePos3 = 5;
                                    tetrisPiecePos4 = 4;
                                }
                                else
                                {
                                    if (pieceType == 4)
                                    {
                                        tetrisPiecePos = -6;
                                        tetrisPiecePos2 = 4;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 6;
                                    }
                                    else
                                    {
                                        if (pieceType == 5)
                                        {
                                            tetrisPiecePos = -4;
                                            tetrisPiecePos = 6;
                                            tetrisPiecePos = 5;
                                            tetrisPiecePos = 4;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //erasing the last positions of pieces from screen
                if (tetrisPiecePos < 190 && tetrisPiecePos2 < 190 && tetrisPiecePos3 < 200 && tetrisPiecePos4 < 200 && pieceType == 1)
                {
                    if (GameLabel[tetrisPiecePos3].BackColor == blockEmptyColor[0] && GameLabel[tetrisPiecePos4].BackColor == blockEmptyColor[0])
                    {
                        GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                    }
                    if (tetrisPiecePos == 4 && tetrisPiecePos2 == 5)
                    {
                        GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                    }
                    else
                    {
                        if (tetrisPiecePos - 10 > 0)
                        {
                            GameLabel[tetrisPiecePos - 10].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos2 - 10].BackColor = blockEmptyColor[0];
                        }
                    }
                }

                //if the piece is at the top blockfilled vector is filled with the new info about labels ( true-if green / false-if gray-default)
                if (tetrisPiecePos == -6 && tetrisPiecePos2 == -5 && tetrisPiecePos3 == 4 && tetrisPiecePos4 == 5)
                {
                    int k = 0;
                    foreach (Label block in GameLabel)
                    {
                        if (block.BackColor == blockEmptyColor[1])
                        {
                            blocksFilled[k] = true;
                        }
                        k++;
                    }
                }
            }
            if (pieceType == 2)
            {
                tetrisPiecePos += 10;
                tetrisPiecePos2 += 10;
                tetrisPiecePos3 += 10;
                tetrisPiecePos4 += 10;
                //if hit a block it stop and start again from top
                if (tetrisPiecePos < 190 && tetrisPiecePos2 < 190 && tetrisPiecePos3 < 200 && tetrisPiecePos4 < 200)
                {
                    if (GameLabel[tetrisPiecePos].BackColor != blockEmptyColor[0])
                    {
                        //erasing lines from the screen if full and movind down stack
                        int count, index;
                        for (int i = 19; i >= 0; i--)
                        {
                            count = 0;
                            for (int j = 9; j >= 0; j--)
                            {
                                index = i * 10 + j;
                                if (GameLabel[index].BackColor == blockEmptyColor[1])
                                {
                                    count++;
                                }
                            }
                            index = i * 10;
                            if (count == 10)
                            {
                                for (int p = index; p < index + 10; p++)
                                {
                                    GameLabel[p].BackColor = blockEmptyColor[0];
                                    blocksFilled[p] = false;
                                }
                                for (int p = index - 1; p >= 10; p--)
                                {
                                    if (GameLabel[p].BackColor == blockEmptyColor[1])
                                    {
                                        blocksFilled[p] = false;
                                        GameLabel[p].BackColor = blockEmptyColor[0];
                                        int countdown = 10;
                                        GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                    }
                                }
                                score += 10;
                                i++;
                            }
                        }
                        rand = new Random();
                        pieceType = rand.Next(5);
                        if (pieceType == 1)
                        {
                            tetrisPiecePos = -6;
                            tetrisPiecePos2 = -5;
                            tetrisPiecePos3 = tetrisPiecePos + 10;
                            tetrisPiecePos4 = tetrisPiecePos2 + 10;
                        }
                        else
                        {
                            if (pieceType == 0)
                            {
                                tetrisPiecePos = -7;
                                tetrisPiecePos2 = -6;
                                tetrisPiecePos3 = -5;
                                tetrisPiecePos4 = -4;
                            }
                            else
                            {
                                if (pieceType == 2)
                                {
                                    tetrisPiecePos = -6;
                                    tetrisPiecePos2 = -5;
                                    tetrisPiecePos3 = 5;
                                    tetrisPiecePos4 = 6;
                                }
                                else
                                {
                                    if (pieceType == 3)
                                    {
                                        tetrisPiecePos = -4;
                                        tetrisPiecePos2 = -5;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 4;
                                    }
                                    else
                                    {
                                        if (pieceType == 4)
                                        {
                                            tetrisPiecePos = -6;
                                            tetrisPiecePos2 = 4;
                                            tetrisPiecePos3 = 5;
                                            tetrisPiecePos4 = 6;
                                        }
                                        else
                                        {
                                            if (pieceType == 5)
                                            {
                                                tetrisPiecePos = -4;
                                                tetrisPiecePos = 6;
                                                tetrisPiecePos = 5;
                                                tetrisPiecePos = 4;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (GameLabel[tetrisPiecePos3].BackColor != blockEmptyColor[0])
                        {
                            //erasing lines from the screen if full and movind down stack
                            int count, index;
                            for (int i = 19; i >= 0; i--)
                            {
                                count = 0;
                                for (int j = 9; j >= 0; j--)
                                {
                                    index = i * 10 + j;
                                    if (GameLabel[index].BackColor == blockEmptyColor[1])
                                    {
                                        count++;
                                    }
                                }
                                index = i * 10;
                                if (count == 10)
                                {
                                    for (int p = index; p < index + 10; p++)
                                    {
                                        GameLabel[p].BackColor = blockEmptyColor[0];
                                        blocksFilled[p] = false;
                                    }
                                    for (int p = index - 1; p >= 10; p--)
                                    {
                                        if (GameLabel[p].BackColor == blockEmptyColor[1])
                                        {
                                            blocksFilled[p] = false;
                                            GameLabel[p].BackColor = blockEmptyColor[0];
                                            int countdown = 10;
                                            GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                        }
                                    }
                                    score += 10;
                                    i++;
                                }
                            }
                            rand = new Random();
                            pieceType = rand.Next(5);
                            if (pieceType == 1)
                            {
                                tetrisPiecePos = -6;
                                tetrisPiecePos2 = -5;
                                tetrisPiecePos3 = tetrisPiecePos + 10;
                                tetrisPiecePos4 = tetrisPiecePos2 + 10;
                            }
                            else
                            {
                                if (pieceType == 0)
                                {
                                    tetrisPiecePos = -7;
                                    tetrisPiecePos2 = -6;
                                    tetrisPiecePos3 = -5;
                                    tetrisPiecePos4 = -4;
                                }
                                else
                                {
                                    if (pieceType == 2)
                                    {
                                        tetrisPiecePos = -6;
                                        tetrisPiecePos2 = -5;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 6;
                                    }
                                    else
                                    {
                                        if (pieceType == 3)
                                        {
                                            tetrisPiecePos = -4;
                                            tetrisPiecePos2 = -5;
                                            tetrisPiecePos3 = 5;
                                            tetrisPiecePos4 = 4;
                                        }
                                        else
                                        {
                                            if (pieceType == 4)
                                            {
                                                tetrisPiecePos = -6;
                                                tetrisPiecePos2 = 4;
                                                tetrisPiecePos3 = 5;
                                                tetrisPiecePos4 = 6;
                                            }
                                            else
                                            {
                                                if (pieceType == 5)
                                                {
                                                    tetrisPiecePos = -4;
                                                    tetrisPiecePos = 6;
                                                    tetrisPiecePos = 5;
                                                    tetrisPiecePos = 4;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (GameLabel[tetrisPiecePos4].BackColor != blockEmptyColor[0])
                            {
                                //erasing lines from the screen if full and movind down stack
                                int count, index;
                                for (int i = 19; i >= 0; i--)
                                {
                                    count = 0;
                                    for (int j = 9; j >= 0; j--)
                                    {
                                        index = i * 10 + j;
                                        if (GameLabel[index].BackColor == blockEmptyColor[1])
                                        {
                                            count++;
                                        }
                                    }
                                    index = i * 10;
                                    if (count == 10)
                                    {
                                        for (int p = index; p < index + 10; p++)
                                        {
                                            GameLabel[p].BackColor = blockEmptyColor[0];
                                            blocksFilled[p] = false;
                                        }
                                        for (int p = index - 1; p >= 10; p--)
                                        {
                                            if (GameLabel[p].BackColor == blockEmptyColor[1])
                                            {
                                                blocksFilled[p] = false;
                                                GameLabel[p].BackColor = blockEmptyColor[0];
                                                int countdown = 10;
                                                GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                            }
                                        }
                                        score += 10;
                                        i++;
                                    }
                                }
                                rand = new Random();
                                pieceType = rand.Next(5);
                                if (pieceType == 1)
                                {
                                    tetrisPiecePos = -6;
                                    tetrisPiecePos2 = -5;
                                    tetrisPiecePos3 = tetrisPiecePos + 10;
                                    tetrisPiecePos4 = tetrisPiecePos2 + 10;
                                }
                                else
                                {
                                    if (pieceType == 0)
                                    {
                                        tetrisPiecePos = -7;
                                        tetrisPiecePos2 = -6;
                                        tetrisPiecePos3 = -5;
                                        tetrisPiecePos4 = -4;
                                    }
                                    else
                                    {
                                        if (pieceType == 2)
                                        {
                                            tetrisPiecePos = -6;
                                            tetrisPiecePos2 = -5;
                                            tetrisPiecePos3 = 5;
                                            tetrisPiecePos4 = 6;
                                        }
                                        else
                                        {
                                            if (pieceType == 3)
                                            {
                                                tetrisPiecePos = -4;
                                                tetrisPiecePos2 = -5;
                                                tetrisPiecePos3 = 5;
                                                tetrisPiecePos4 = 4;
                                            }
                                            else
                                            {
                                                if (pieceType == 4)
                                                {
                                                    tetrisPiecePos = -6;
                                                    tetrisPiecePos2 = 4;
                                                    tetrisPiecePos3 = 5;
                                                    tetrisPiecePos4 = 6;
                                                }
                                                else
                                                {
                                                    if (pieceType == 5)
                                                    {
                                                        tetrisPiecePos = -4;
                                                        tetrisPiecePos = 6;
                                                        tetrisPiecePos = 5;
                                                        tetrisPiecePos = 4;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        }
                    }
                

                //if piece cross the map a new piece start from top
                if (tetrisPiecePos > 190 && tetrisPiecePos2 > 190 && tetrisPiecePos4 > 200 && tetrisPiecePos3 > 200)
                {
                    //erasing lines from the screen if full and movind down stack
                    int count, index;
                    for (int i = 19; i >= 0; i--)
                    {
                        count = 0;
                        for (int j = 9; j >= 0; j--)
                        {
                            index = i * 10 + j;
                            if (GameLabel[index].BackColor == blockEmptyColor[1])
                            {
                                count++;
                            }
                        }
                        index = i * 10;
                        if (count == 10)
                        {
                            for (int p = index; p < index + 10; p++)
                            {
                                GameLabel[p].BackColor = blockEmptyColor[0];
                                blocksFilled[p] = false;
                            }
                            for (int p = index - 1; p >= 10; p--)
                            {
                                if (GameLabel[p].BackColor == blockEmptyColor[1])
                                {
                                    blocksFilled[p] = false;
                                    GameLabel[p].BackColor = blockEmptyColor[0];
                                    int countdown = 10;
                                    GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                }
                            }
                            score += 10;
                            i++;
                        }
                    }
                    rand = new Random();
                    pieceType = rand.Next(5);
                    if (pieceType == 1)
                    {
                        tetrisPiecePos = -6;
                        tetrisPiecePos2 = -5;
                        tetrisPiecePos3 = tetrisPiecePos + 10;
                        tetrisPiecePos4 = tetrisPiecePos2 + 10;
                    }
                    else
                    {
                        if (pieceType == 0)
                        {
                            tetrisPiecePos = -7;
                            tetrisPiecePos2 = -6;
                            tetrisPiecePos3 = -5;
                            tetrisPiecePos4 = -4;
                        }
                        else
                        {
                            if (pieceType == 2)
                            {
                                tetrisPiecePos = -6;
                                tetrisPiecePos2 = -5;
                                tetrisPiecePos3 = 5;
                                tetrisPiecePos4 = 6;
                            }
                            else
                            {
                                if (pieceType == 4)
                                {
                                    tetrisPiecePos = -6;
                                    tetrisPiecePos2 = 4;
                                    tetrisPiecePos3 = 5;
                                    tetrisPiecePos4 = 6;
                                }
                                else
                                {
                                    if (pieceType == 5)
                                    {
                                        tetrisPiecePos = -4;
                                        tetrisPiecePos = 6;
                                        tetrisPiecePos = 5;
                                        tetrisPiecePos = 4;
                                    }
                                }
                            }
                        }
                    }
                }

                //erasing the last positions of pieces from screen
                if (tetrisPiecePos < 190 && tetrisPiecePos2 < 190 && tetrisPiecePos3 < 200 && tetrisPiecePos4 < 200 && pieceType == 2)
                {
                    if (GameLabel[tetrisPiecePos3].BackColor != blockEmptyColor[1])
                    {
                        if (tetrisPiecePos > 0)
                        {
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                        }
                        if (tetrisPiecePos > 9)
                        {
                            GameLabel[tetrisPiecePos - 10].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos2 - 10].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos4 - 10].BackColor = blockEmptyColor[0];
                        }
                    }

                }

                //if the piece is at the top blockfilled vector is filled with the new info about labels ( true-if green / false-if gray-default)
                if (tetrisPiecePos == -6 && tetrisPiecePos2 == -5 && tetrisPiecePos3 == 5 && tetrisPiecePos4 == 6)
                {
                    int k = 0;
                    foreach (Label block in GameLabel)
                    {
                        if (block.BackColor == blockEmptyColor[1])
                        {
                            blocksFilled[k] = true;
                        }
                        k++;
                    }
                }

            }
            if (pieceType == 3)
            {
                tetrisPiecePos += 10;
                tetrisPiecePos2 += 10;
                tetrisPiecePos3 += 10;
                tetrisPiecePos4 += 10;
                //if hit a block it stop and start again from top
                if (tetrisPiecePos < 190 && tetrisPiecePos2 < 190 && tetrisPiecePos3 < 200 && tetrisPiecePos4 < 200)
                {
                    if (GameLabel[tetrisPiecePos].BackColor != blockEmptyColor[0])
                    {
                        //erasing lines from the screen if full and movind down stack
                        int count, index;
                        for (int i = 19; i >= 0; i--)
                        {
                            count = 0;
                            for (int j = 9; j >= 0; j--)
                            {
                                index = i * 10 + j;
                                if (GameLabel[index].BackColor == blockEmptyColor[1])
                                {
                                    count++;
                                }
                            }
                            index = i * 10;
                            if (count == 10)
                            {
                                for (int p = index; p < index + 10; p++)
                                {
                                    GameLabel[p].BackColor = blockEmptyColor[0];
                                    blocksFilled[p] = false;
                                }
                                for (int p = index - 1; p >= 10; p--)
                                {
                                    if (GameLabel[p].BackColor == blockEmptyColor[1])
                                    {
                                        blocksFilled[p] = false;
                                        GameLabel[p].BackColor = blockEmptyColor[0];
                                        int countdown = 10;
                                        GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                    }
                                }
                                score += 10;
                                i++;
                            }
                        }
                        rand = new Random();
                        pieceType = rand.Next(5);
                        if (pieceType == 1)
                        {
                            tetrisPiecePos = -6;
                            tetrisPiecePos2 = -5;
                            tetrisPiecePos3 = tetrisPiecePos + 10;
                            tetrisPiecePos4 = tetrisPiecePos2 + 10;
                        }
                        else
                        {
                            if (pieceType == 0)
                            {
                                tetrisPiecePos = -7;
                                tetrisPiecePos2 = -6;
                                tetrisPiecePos3 = -5;
                                tetrisPiecePos4 = -4;
                            }
                            else
                            {
                                if (pieceType == 2)
                                {
                                    tetrisPiecePos = -6;
                                    tetrisPiecePos2 = -5;
                                    tetrisPiecePos3 = 5;
                                    tetrisPiecePos4 = 6;
                                }
                                else
                                {
                                    if (pieceType == 3)
                                    {
                                        tetrisPiecePos = -4;
                                        tetrisPiecePos2 = -5;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 4;
                                    }
                                    else
                                    {
                                        if (pieceType == 4)
                                        {
                                            tetrisPiecePos = -6;
                                            tetrisPiecePos2 = 4;
                                            tetrisPiecePos3 = 5;
                                            tetrisPiecePos4 = 6;
                                        }
                                        else
                                        {
                                            if (pieceType == 5)
                                            {
                                                tetrisPiecePos = -4;
                                                tetrisPiecePos = 6;
                                                tetrisPiecePos = 5;
                                                tetrisPiecePos = 4;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (GameLabel[tetrisPiecePos3].BackColor != blockEmptyColor[0])
                        {
                            //erasing lines from the screen if full and movind down stack
                            int count, index;
                            for (int i = 19; i >= 0; i--)
                            {
                                count = 0;
                                for (int j = 9; j >= 0; j--)
                                {
                                    index = i * 10 + j;
                                    if (GameLabel[index].BackColor == blockEmptyColor[1])
                                    {
                                        count++;
                                    }
                                }
                                index = i * 10;
                                if (count == 10)
                                {
                                    for (int p = index; p < index + 10; p++)
                                    {
                                        GameLabel[p].BackColor = blockEmptyColor[0];
                                        blocksFilled[p] = false;
                                    }
                                    for (int p = index - 1; p >= 10; p--)
                                    {
                                        if (GameLabel[p].BackColor == blockEmptyColor[1])
                                        {
                                            blocksFilled[p] = false;
                                            GameLabel[p].BackColor = blockEmptyColor[0];
                                            int countdown = 10;
                                            GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                        }
                                    }
                                    score += 10;
                                    i++;
                                }
                            }
                            rand = new Random();
                            pieceType = rand.Next(5);
                            if (pieceType == 1)
                            {
                                tetrisPiecePos = -6;
                                tetrisPiecePos2 = -5;
                                tetrisPiecePos3 = tetrisPiecePos + 10;
                                tetrisPiecePos4 = tetrisPiecePos2 + 10;
                            }
                            else
                            {
                                if (pieceType == 0)
                                {
                                    tetrisPiecePos = -7;
                                    tetrisPiecePos2 = -6;
                                    tetrisPiecePos3 = -5;
                                    tetrisPiecePos4 = -4;
                                }
                                else
                                {
                                    if (pieceType == 2)
                                    {
                                        tetrisPiecePos = -6;
                                        tetrisPiecePos2 = -5;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 6;
                                    }
                                    else
                                    {
                                        if (pieceType == 3)
                                        {
                                            tetrisPiecePos = -4;
                                            tetrisPiecePos2 = -5;
                                            tetrisPiecePos3 = 5;
                                            tetrisPiecePos4 = 4;
                                        }
                                        else
                                        {
                                            if (pieceType == 4)
                                            {
                                                tetrisPiecePos = -6;
                                                tetrisPiecePos2 = 4;
                                                tetrisPiecePos3 = 5;
                                                tetrisPiecePos4 = 6;
                                            }
                                            else
                                            {
                                                if (pieceType == 5)
                                                {
                                                    tetrisPiecePos = -4;
                                                    tetrisPiecePos = 6;
                                                    tetrisPiecePos = 5;
                                                    tetrisPiecePos = 4;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (GameLabel[tetrisPiecePos4].BackColor != blockEmptyColor[0])
                            {
                                //erasing lines from the screen if full and movind down stack
                                int count, index;
                                for (int i = 19; i >= 0; i--)
                                {
                                    count = 0;
                                    for (int j = 9; j >= 0; j--)
                                    {
                                        index = i * 10 + j;
                                        if (GameLabel[index].BackColor == blockEmptyColor[1])
                                        {
                                            count++;
                                        }
                                    }
                                    index = i * 10;
                                    if (count == 10)
                                    {
                                        for (int p = index; p < index + 10; p++)
                                        {
                                            GameLabel[p].BackColor = blockEmptyColor[0];
                                            blocksFilled[p] = false;
                                        }
                                        for (int p = index - 1; p >= 10; p--)
                                        {
                                            if (GameLabel[p].BackColor == blockEmptyColor[1])
                                            {
                                                blocksFilled[p] = false;
                                                GameLabel[p].BackColor = blockEmptyColor[0];
                                                int countdown = 10;
                                                GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                            }
                                        }
                                        score += 10;
                                        i++;
                                    }
                                }
                                rand = new Random();
                                pieceType = rand.Next(5);
                                if (pieceType == 1)
                                {
                                    tetrisPiecePos = -6;
                                    tetrisPiecePos2 = -5;
                                    tetrisPiecePos3 = tetrisPiecePos + 10;
                                    tetrisPiecePos4 = tetrisPiecePos2 + 10;
                                }
                                else
                                {
                                    if (pieceType == 0)
                                    {
                                        tetrisPiecePos = -7;
                                        tetrisPiecePos2 = -6;
                                        tetrisPiecePos3 = -5;
                                        tetrisPiecePos4 = -4;
                                    }
                                    else
                                    {
                                        if (pieceType == 2)
                                        {
                                            tetrisPiecePos = -6;
                                            tetrisPiecePos2 = -5;
                                            tetrisPiecePos3 = 5;
                                            tetrisPiecePos4 = 6;
                                        }
                                        else
                                        {
                                            if (pieceType == 3)
                                            {
                                                tetrisPiecePos = -4;
                                                tetrisPiecePos2 = -5;
                                                tetrisPiecePos3 = 5;
                                                tetrisPiecePos4 = 4;
                                            }
                                            else
                                            {
                                                if (pieceType == 4)
                                                {
                                                    tetrisPiecePos = -6;
                                                    tetrisPiecePos2 = 4;
                                                    tetrisPiecePos3 = 5;
                                                    tetrisPiecePos4 = 6;
                                                }
                                                else
                                                {
                                                    if (pieceType == 5)
                                                    {
                                                        tetrisPiecePos = -4;
                                                        tetrisPiecePos = 6;
                                                        tetrisPiecePos = 5;
                                                        tetrisPiecePos = 4;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //if piece cross the map a new piece start from top
                if (tetrisPiecePos > 190 && tetrisPiecePos2 > 190 && tetrisPiecePos4 > 200 && tetrisPiecePos3 > 200)
                {
                    //erasing lines from the screen if full and movind down stack
                    int count, index;
                    for (int i = 19; i >= 0; i--)
                    {
                        count = 0;
                        for (int j = 9; j >= 0; j--)
                        {
                            index = i * 10 + j;
                            if (GameLabel[index].BackColor == blockEmptyColor[1])
                            {
                                count++;
                            }
                        }
                        index = i * 10;
                        if (count == 10)
                        {
                            for (int p = index; p < index + 10; p++)
                            {
                                GameLabel[p].BackColor = blockEmptyColor[0];
                                blocksFilled[p] = false;
                            }
                            for (int p = index - 1; p >= 10; p--)
                            {
                                if (GameLabel[p].BackColor == blockEmptyColor[1])
                                {
                                    blocksFilled[p] = false;
                                    GameLabel[p].BackColor = blockEmptyColor[0];
                                    int countdown = 10;
                                    GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                }
                            }
                            score += 10;
                            i++;
                        }
                    }
                    rand = new Random();
                    pieceType = rand.Next(5);
                    if (pieceType == 1)
                    {
                        tetrisPiecePos = -6;
                        tetrisPiecePos2 = -5;
                        tetrisPiecePos3 = tetrisPiecePos + 10;
                        tetrisPiecePos4 = tetrisPiecePos2 + 10;
                    }
                    else
                    {
                        if (pieceType == 0)
                        {
                            tetrisPiecePos = -7;
                            tetrisPiecePos2 = -6;
                            tetrisPiecePos3 = -5;
                            tetrisPiecePos4 = -4;
                        }
                        else
                        {
                            if (pieceType == 2)
                            {
                                tetrisPiecePos = -6;
                                tetrisPiecePos2 = -5;
                                tetrisPiecePos3 = 5;
                                tetrisPiecePos4 = 6;
                            }
                            else
                            {
                                if (pieceType == 3)
                                {
                                    tetrisPiecePos = -4;
                                    tetrisPiecePos2 = -5;
                                    tetrisPiecePos3 = 5;
                                    tetrisPiecePos4 = 4;
                                }
                                else
                                {
                                    if (pieceType == 4)
                                    {
                                        tetrisPiecePos = -6;
                                        tetrisPiecePos2 = 4;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 6;
                                    }
                                    else
                                    {
                                        if (pieceType == 5)
                                        {
                                            tetrisPiecePos = -4;
                                            tetrisPiecePos = 6;
                                            tetrisPiecePos = 5;
                                            tetrisPiecePos = 4;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //erasing the last positions of pieces from screen
                if (tetrisPiecePos < 190 && tetrisPiecePos2 < 190 && tetrisPiecePos3 < 200 && tetrisPiecePos4 < 200 && pieceType == 3)
                {
                    if (GameLabel[tetrisPiecePos3].BackColor != blockEmptyColor[1])
                    {
                        if (tetrisPiecePos > 0)
                        {
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                        }
                        if (tetrisPiecePos > 9)
                        {
                            GameLabel[tetrisPiecePos - 10].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos2 - 10].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos4 - 10].BackColor = blockEmptyColor[0];
                        }
                    }

                }

                //if the piece is at the top blockfilled vector is filled with the new info about labels ( true-if green / false-if gray-default)
                if (tetrisPiecePos == 6 && tetrisPiecePos2 == 5 && tetrisPiecePos3 == 15 && tetrisPiecePos4 == 14)
                {
                    int k = 0;
                    foreach (Label block in GameLabel)
                    {
                        if (block.BackColor == blockEmptyColor[1])
                        {
                            blocksFilled[k] = true;
                        }
                        k++;
                    }
                }
            }
            if (pieceType == 4)
            {
                tetrisPiecePos += 10;
                tetrisPiecePos2 += 10;
                tetrisPiecePos3 += 10;
                tetrisPiecePos4 += 10;
                //if hit a block it stop and start again from top
                if (tetrisPiecePos < 190 && tetrisPiecePos2 < 200 && tetrisPiecePos3 < 200 && tetrisPiecePos4 < 200)
                {
                    if (GameLabel[tetrisPiecePos2].BackColor != blockEmptyColor[0])
                    {
                        //erasing lines from the screen if full and movind down stack
                        int count, index;
                        for (int i = 19; i >= 0; i--)
                        {
                            count = 0;
                            for (int j = 9; j >= 0; j--)
                            {
                                index = i * 10 + j;
                                if (GameLabel[index].BackColor == blockEmptyColor[1])
                                {
                                    count++;
                                }
                            }
                            index = i * 10;
                            if (count == 10)
                            {
                                for (int p = index; p < index + 10; p++)
                                {
                                    GameLabel[p].BackColor = blockEmptyColor[0];
                                    blocksFilled[p] = false;
                                }
                                for (int p = index - 1; p >= 10; p--)
                                {
                                    if (GameLabel[p].BackColor == blockEmptyColor[1])
                                    {
                                        blocksFilled[p] = false;
                                        GameLabel[p].BackColor = blockEmptyColor[0];
                                        int countdown = 10;
                                        GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                    }
                                }
                                score += 10;
                                i++;
                            }
                        }
                        rand = new Random();
                        pieceType = rand.Next(5);
                        if (pieceType == 1)
                        {
                            tetrisPiecePos = -6;
                            tetrisPiecePos2 = -5;
                            tetrisPiecePos3 = tetrisPiecePos + 10;
                            tetrisPiecePos4 = tetrisPiecePos2 + 10;
                        }
                        else
                        {
                            if (pieceType == 0)
                            {
                                tetrisPiecePos = -7;
                                tetrisPiecePos2 = -6;
                                tetrisPiecePos3 = -5;
                                tetrisPiecePos4 = -4;
                            }
                            else
                            {
                                if (pieceType == 2)
                                {
                                    tetrisPiecePos = -6;
                                    tetrisPiecePos2 = -5;
                                    tetrisPiecePos3 = 5;
                                    tetrisPiecePos4 = 6;
                                }
                                else
                                {
                                    if (pieceType == 3)
                                    {
                                        tetrisPiecePos = -4;
                                        tetrisPiecePos2 = -5;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 4;
                                    }
                                    else
                                    {
                                        if (pieceType == 4)
                                        {
                                            tetrisPiecePos = -6;
                                            tetrisPiecePos2 = 4;
                                            tetrisPiecePos3 = 5;
                                            tetrisPiecePos4 = 6;
                                        }
                                        else
                                        {
                                            if (pieceType == 5)
                                            {
                                                tetrisPiecePos = -4;
                                                tetrisPiecePos = 6;
                                                tetrisPiecePos = 5;
                                                tetrisPiecePos = 4;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (GameLabel[tetrisPiecePos3].BackColor != blockEmptyColor[0])
                        {
                            //erasing lines from the screen if full and movind down stack
                            int count, index;
                            for (int i = 19; i >= 0; i--)
                            {
                                count = 0;
                                for (int j = 9; j >= 0; j--)
                                {
                                    index = i * 10 + j;
                                    if (GameLabel[index].BackColor == blockEmptyColor[1])
                                    {
                                        count++;
                                    }
                                }
                                index = i * 10;
                                if (count == 10)
                                {
                                    for (int p = index; p < index + 10; p++)
                                    {
                                        GameLabel[p].BackColor = blockEmptyColor[0];
                                        blocksFilled[p] = false;
                                    }
                                    for (int p = index - 1; p >= 10; p--)
                                    {
                                        if (GameLabel[p].BackColor == blockEmptyColor[1])
                                        {
                                            blocksFilled[p] = false;
                                            GameLabel[p].BackColor = blockEmptyColor[0];
                                            int countdown = 10;
                                            GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                        }
                                    }
                                    score += 10;
                                    i++;
                                }
                            }
                            rand = new Random();
                            pieceType = rand.Next(5);
                            if (pieceType == 1)
                            {
                                tetrisPiecePos = -6;
                                tetrisPiecePos2 = -5;
                                tetrisPiecePos3 = tetrisPiecePos + 10;
                                tetrisPiecePos4 = tetrisPiecePos2 + 10;
                            }
                            else
                            {
                                if (pieceType == 0)
                                {
                                    tetrisPiecePos = -7;
                                    tetrisPiecePos2 = -6;
                                    tetrisPiecePos3 = -5;
                                    tetrisPiecePos4 = -4;
                                }
                                else
                                {
                                    if (pieceType == 2)
                                    {
                                        tetrisPiecePos = -6;
                                        tetrisPiecePos2 = -5;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 6;
                                    }
                                    else
                                    {
                                        if (pieceType == 3)
                                        {
                                            tetrisPiecePos = -4;
                                            tetrisPiecePos2 = -5;
                                            tetrisPiecePos3 = 5;
                                            tetrisPiecePos4 = 4;
                                        }
                                        else
                                        {
                                            if (pieceType == 4)
                                            {
                                                tetrisPiecePos = -6;
                                                tetrisPiecePos2 = 4;
                                                tetrisPiecePos3 = 5;
                                                tetrisPiecePos4 = 6;
                                            }
                                            else
                                            {
                                                if (pieceType == 5)
                                                {
                                                    tetrisPiecePos = -4;
                                                    tetrisPiecePos = 6;
                                                    tetrisPiecePos = 5;
                                                    tetrisPiecePos = 4;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (GameLabel[tetrisPiecePos4].BackColor != blockEmptyColor[0])
                            {
                                //erasing lines from the screen if full and movind down stack
                                int count, index;
                                for (int i = 19; i >= 0; i--)
                                {
                                    count = 0;
                                    for (int j = 9; j >= 0; j--)
                                    {
                                        index = i * 10 + j;
                                        if (GameLabel[index].BackColor == blockEmptyColor[1])
                                        {
                                            count++;
                                        }
                                    }
                                    index = i * 10;
                                    if (count == 10)
                                    {
                                        for (int p = index; p < index + 10; p++)
                                        {
                                            GameLabel[p].BackColor = blockEmptyColor[0];
                                            blocksFilled[p] = false;
                                        }
                                        for (int p = index - 1; p >= 10; p--)
                                        {
                                            if (GameLabel[p].BackColor == blockEmptyColor[1])
                                            {
                                                blocksFilled[p] = false;
                                                GameLabel[p].BackColor = blockEmptyColor[0];
                                                int countdown = 10;
                                                GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                            }
                                        }
                                        score += 10;
                                        i++;
                                    }
                                }
                                rand = new Random();
                                pieceType = rand.Next(5);
                                if (pieceType == 1)
                                {
                                    tetrisPiecePos = -6;
                                    tetrisPiecePos2 = -5;
                                    tetrisPiecePos3 = tetrisPiecePos + 10;
                                    tetrisPiecePos4 = tetrisPiecePos2 + 10;
                                }
                                else
                                {
                                    if (pieceType == 0)
                                    {
                                        tetrisPiecePos = -7;
                                        tetrisPiecePos2 = -6;
                                        tetrisPiecePos3 = -5;
                                        tetrisPiecePos4 = -4;
                                    }
                                    else
                                    {
                                        if (pieceType == 2)
                                        {
                                            tetrisPiecePos = -6;
                                            tetrisPiecePos2 = -5;
                                            tetrisPiecePos3 = 5;
                                            tetrisPiecePos4 = 6;
                                        }
                                        else
                                        {
                                            if (pieceType == 3)
                                            {
                                                tetrisPiecePos = -4;
                                                tetrisPiecePos2 = -5;
                                                tetrisPiecePos3 = 5;
                                                tetrisPiecePos4 = 4;
                                            }
                                            else
                                            {
                                                if (pieceType == 4)
                                                {
                                                    tetrisPiecePos = -6;
                                                    tetrisPiecePos2 = 4;
                                                    tetrisPiecePos3 = 5;
                                                    tetrisPiecePos4 = 6;
                                                }
                                                else
                                                {
                                                    if (pieceType == 5)
                                                    {
                                                        tetrisPiecePos = -4;
                                                        tetrisPiecePos = 6;
                                                        tetrisPiecePos = 5;
                                                        tetrisPiecePos = 4;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //if piece cross the map a new piece start from top
                if (tetrisPiecePos > 190 && tetrisPiecePos2 > 200 && tetrisPiecePos3 > 200 && tetrisPiecePos4 > 200)
                {
                    //erasing lines from the screen if full and movind down stack
                    int count, index;
                    for (int i = 19; i >= 0; i--)
                    {
                        count = 0;
                        for (int j = 9; j >= 0; j--)
                        {
                            index = i * 10 + j;
                            if (GameLabel[index].BackColor == blockEmptyColor[1])
                            {
                                count++;
                            }
                        }
                        index = i * 10;
                        if (count == 10)
                        {
                            for (int p = index; p < index + 10; p++)
                            {
                                GameLabel[p].BackColor = blockEmptyColor[0];
                                blocksFilled[p] = false;
                            }
                            for (int p = index - 1; p >= 10; p--)
                            {
                                if (GameLabel[p].BackColor == blockEmptyColor[1])
                                {
                                    blocksFilled[p] = false;
                                    GameLabel[p].BackColor = blockEmptyColor[0];
                                    int countdown = 10;
                                    GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                }
                            }
                            score += 10;
                            i++;
                        }
                    }
                    rand = new Random();
                    pieceType = rand.Next(5);
                    if (pieceType == 1)
                    {
                        tetrisPiecePos = -6;
                        tetrisPiecePos2 = -5;
                        tetrisPiecePos3 = tetrisPiecePos + 10;
                        tetrisPiecePos4 = tetrisPiecePos2 + 10;
                    }
                    else
                    {
                        if (pieceType == 0)
                        {
                            tetrisPiecePos = -7;
                            tetrisPiecePos2 = -6;
                            tetrisPiecePos3 = -5;
                            tetrisPiecePos4 = -4;
                        }
                        else
                        {
                            if (pieceType == 2)
                            {
                                tetrisPiecePos = -6;
                                tetrisPiecePos2 = -5;
                                tetrisPiecePos3 = 5;
                                tetrisPiecePos4 = 6;
                            }
                            else
                            {
                                if (pieceType == 3)
                                {
                                    tetrisPiecePos = -4;
                                    tetrisPiecePos2 = -5;
                                    tetrisPiecePos3 = 5;
                                    tetrisPiecePos4 = 4;
                                }
                                else
                                {
                                    if (pieceType == 4)
                                    {
                                        tetrisPiecePos = -6;
                                        tetrisPiecePos2 = 4;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 6;
                                    }
                                    else
                                    {
                                        if (pieceType == 5)
                                        {
                                            tetrisPiecePos = -4;
                                            tetrisPiecePos = 6;
                                            tetrisPiecePos = 5;
                                            tetrisPiecePos = 4;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //erasing the last positions of pieces from screen
                if (tetrisPiecePos < 190 && tetrisPiecePos2 < 200 && tetrisPiecePos3 < 200 && tetrisPiecePos4 < 200 && pieceType == 4)
                { 
                    if (tetrisPiecePos > 0)
                    {
                        GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                    }
                    if (tetrisPiecePos > 9)
                    {
                        GameLabel[tetrisPiecePos - 10].BackColor = blockEmptyColor[0];
                        GameLabel[tetrisPiecePos3 - 10].BackColor = blockEmptyColor[0];
                        GameLabel[tetrisPiecePos4 - 10].BackColor = blockEmptyColor[0];
                    }
                }
            }
            if (pieceType == 5)
            {
                tetrisPiecePos += 10;
                tetrisPiecePos2 += 10;
                tetrisPiecePos3 += 10;
                tetrisPiecePos4 += 10;
                //if hit a block it stop and start again from top
                if (tetrisPiecePos < 190 && tetrisPiecePos2 < 200 && tetrisPiecePos3 < 200 && tetrisPiecePos4 < 200)
                {
                    if (GameLabel[tetrisPiecePos2].BackColor != blockEmptyColor[0])
                    {
                        //erasing lines from the screen if full and movind down stack
                        int count, index;
                        for (int i = 19; i >= 0; i--)
                        {
                            count = 0;
                            for (int j = 9; j >= 0; j--)
                            {
                                index = i * 10 + j;
                                if (GameLabel[index].BackColor == blockEmptyColor[1])
                                {
                                    count++;
                                }
                            }
                            index = i * 10;
                            if (count == 10)
                            {
                                for (int p = index; p < index + 10; p++)
                                {
                                    GameLabel[p].BackColor = blockEmptyColor[0];
                                    blocksFilled[p] = false;
                                }
                                for (int p = index - 1; p >= 10; p--)
                                {
                                    if (GameLabel[p].BackColor == blockEmptyColor[1])
                                    {
                                        blocksFilled[p] = false;
                                        GameLabel[p].BackColor = blockEmptyColor[0];
                                        int countdown = 10;
                                        GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                    }
                                }
                                score += 10;
                                i++;
                            }
                        }
                        rand = new Random();
                        pieceType = rand.Next(5);
                        if (pieceType == 1)
                        {
                            tetrisPiecePos = -6;
                            tetrisPiecePos2 = -5;
                            tetrisPiecePos3 = tetrisPiecePos + 10;
                            tetrisPiecePos4 = tetrisPiecePos2 + 10;
                        }
                        else
                        {
                            if (pieceType == 0)
                            {
                                tetrisPiecePos = -7;
                                tetrisPiecePos2 = -6;
                                tetrisPiecePos3 = -5;
                                tetrisPiecePos4 = -4;
                            }
                            else
                            {
                                if (pieceType == 2)
                                {
                                    tetrisPiecePos = -6;
                                    tetrisPiecePos2 = -5;
                                    tetrisPiecePos3 = 5;
                                    tetrisPiecePos4 = 6;
                                }
                                else
                                {
                                    if (pieceType == 3)
                                    {
                                        tetrisPiecePos = -4;
                                        tetrisPiecePos2 = -5;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 4;
                                    }
                                    else
                                    {
                                        if (pieceType == 4)
                                        {
                                            tetrisPiecePos = -6;
                                            tetrisPiecePos2 = 4;
                                            tetrisPiecePos3 = 5;
                                            tetrisPiecePos4 = 6;
                                        }
                                        else
                                        {
                                            if (pieceType == 5)
                                            {
                                                tetrisPiecePos = -4;
                                                tetrisPiecePos = 6;
                                                tetrisPiecePos = 5;
                                                tetrisPiecePos = 4;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (GameLabel[tetrisPiecePos3].BackColor != blockEmptyColor[0])
                        {
                            //erasing lines from the screen if full and movind down stack
                            int count, index;
                            for (int i = 19; i >= 0; i--)
                            {
                                count = 0;
                                for (int j = 9; j >= 0; j--)
                                {
                                    index = i * 10 + j;
                                    if (GameLabel[index].BackColor == blockEmptyColor[1])
                                    {
                                        count++;
                                    }
                                }
                                index = i * 10;
                                if (count == 10)
                                {
                                    for (int p = index; p < index + 10; p++)
                                    {
                                        GameLabel[p].BackColor = blockEmptyColor[0];
                                        blocksFilled[p] = false;
                                    }
                                    for (int p = index - 1; p >= 10; p--)
                                    {
                                        if (GameLabel[p].BackColor == blockEmptyColor[1])
                                        {
                                            blocksFilled[p] = false;
                                            GameLabel[p].BackColor = blockEmptyColor[0];
                                            int countdown = 10;
                                            GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                        }
                                    }
                                    score += 10;
                                    i++;
                                }
                            }
                            rand = new Random();
                            pieceType = rand.Next(5);
                            if (pieceType == 1)
                            {
                                tetrisPiecePos = -6;
                                tetrisPiecePos2 = -5;
                                tetrisPiecePos3 = tetrisPiecePos + 10;
                                tetrisPiecePos4 = tetrisPiecePos2 + 10;
                            }
                            else
                            {
                                if (pieceType == 0)
                                {
                                    tetrisPiecePos = -7;
                                    tetrisPiecePos2 = -6;
                                    tetrisPiecePos3 = -5;
                                    tetrisPiecePos4 = -4;
                                }
                                else
                                {
                                    if (pieceType == 2)
                                    {
                                        tetrisPiecePos = -6;
                                        tetrisPiecePos2 = -5;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 6;
                                    }
                                    else
                                    {
                                        if (pieceType == 3)
                                        {
                                            tetrisPiecePos = -4;
                                            tetrisPiecePos2 = -5;
                                            tetrisPiecePos3 = 5;
                                            tetrisPiecePos4 = 4;
                                        }
                                        else
                                        {
                                            if (pieceType == 4)
                                            {
                                                tetrisPiecePos = -6;
                                                tetrisPiecePos2 = 4;
                                                tetrisPiecePos3 = 5;
                                                tetrisPiecePos4 = 6;
                                            }
                                            else
                                            {
                                                if (pieceType == 5)
                                                {
                                                    tetrisPiecePos = -4;
                                                    tetrisPiecePos = 6;
                                                    tetrisPiecePos = 5;
                                                    tetrisPiecePos = 4;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (GameLabel[tetrisPiecePos4].BackColor != blockEmptyColor[0])
                            {
                                //erasing lines from the screen if full and movind down stack
                                int count, index;
                                for (int i = 19; i >= 0; i--)
                                {
                                    count = 0;
                                    for (int j = 9; j >= 0; j--)
                                    {
                                        index = i * 10 + j;
                                        if (GameLabel[index].BackColor == blockEmptyColor[1])
                                        {
                                            count++;
                                        }
                                    }
                                    index = i * 10;
                                    if (count == 10)
                                    {
                                        for (int p = index; p < index + 10; p++)
                                        {
                                            GameLabel[p].BackColor = blockEmptyColor[0];
                                            blocksFilled[p] = false;
                                        }
                                        for (int p = index - 1; p >= 10; p--)
                                        {
                                            if (GameLabel[p].BackColor == blockEmptyColor[1])
                                            {
                                                blocksFilled[p] = false;
                                                GameLabel[p].BackColor = blockEmptyColor[0];
                                                int countdown = 10;
                                                GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                            }
                                        }
                                        score += 10;
                                        i++;
                                    }
                                }
                                rand = new Random();
                                pieceType = rand.Next(5);
                                if (pieceType == 1)
                                {
                                    tetrisPiecePos = -6;
                                    tetrisPiecePos2 = -5;
                                    tetrisPiecePos3 = tetrisPiecePos + 10;
                                    tetrisPiecePos4 = tetrisPiecePos2 + 10;
                                }
                                else
                                {
                                    if (pieceType == 0)
                                    {
                                        tetrisPiecePos = -7;
                                        tetrisPiecePos2 = -6;
                                        tetrisPiecePos3 = -5;
                                        tetrisPiecePos4 = -4;
                                    }
                                    else
                                    {
                                        if (pieceType == 2)
                                        {
                                            tetrisPiecePos = -6;
                                            tetrisPiecePos2 = -5;
                                            tetrisPiecePos3 = 5;
                                            tetrisPiecePos4 = 6;
                                        }
                                        else
                                        {
                                            if (pieceType == 3)
                                            {
                                                tetrisPiecePos = -4;
                                                tetrisPiecePos2 = -5;
                                                tetrisPiecePos3 = 5;
                                                tetrisPiecePos4 = 4;
                                            }
                                            else
                                            {
                                                if (pieceType == 4)
                                                {
                                                    tetrisPiecePos = -6;
                                                    tetrisPiecePos2 = 4;
                                                    tetrisPiecePos3 = 5;
                                                    tetrisPiecePos4 = 6;
                                                }
                                                else
                                                {
                                                    if (pieceType == 5)
                                                    {
                                                        tetrisPiecePos = -4;
                                                        tetrisPiecePos = 6;
                                                        tetrisPiecePos = 5;
                                                        tetrisPiecePos = 4;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //if piece cross the map a new piece start from top
                if (tetrisPiecePos > 190 && tetrisPiecePos2 > 200 && tetrisPiecePos3 > 200 && tetrisPiecePos4 > 200)
                {
                    //erasing lines from the screen if full and movind down stack
                    int count, index;
                    for (int i = 19; i >= 0; i--)
                    {
                        count = 0;
                        for (int j = 9; j >= 0; j--)
                        {
                            index = i * 10 + j;
                            if (GameLabel[index].BackColor == blockEmptyColor[1])
                            {
                                count++;
                            }
                        }
                        index = i * 10;
                        if (count == 10)
                        {
                            for (int p = index; p < index + 10; p++)
                            {
                                GameLabel[p].BackColor = blockEmptyColor[0];
                                blocksFilled[p] = false;
                            }
                            for (int p = index - 1; p >= 10; p--)
                            {
                                if (GameLabel[p].BackColor == blockEmptyColor[1])
                                {
                                    blocksFilled[p] = false;
                                    GameLabel[p].BackColor = blockEmptyColor[0];
                                    int countdown = 10;
                                    GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                }
                            }
                            score += 10;
                            i++;
                        }
                    }
                    rand = new Random();
                    pieceType = rand.Next(5);
                    if (pieceType == 1)
                    {
                        tetrisPiecePos = -6;
                        tetrisPiecePos2 = -5;
                        tetrisPiecePos3 = tetrisPiecePos + 10;
                        tetrisPiecePos4 = tetrisPiecePos2 + 10;
                    }
                    else
                    {
                        if (pieceType == 0)
                        {
                            tetrisPiecePos = -7;
                            tetrisPiecePos2 = -6;
                            tetrisPiecePos3 = -5;
                            tetrisPiecePos4 = -4;
                        }
                        else
                        {
                            if (pieceType == 2)
                            {
                                tetrisPiecePos = -6;
                                tetrisPiecePos2 = -5;
                                tetrisPiecePos3 = 5;
                                tetrisPiecePos4 = 6;
                            }
                            else
                            {
                                if (pieceType == 3)
                                {
                                    tetrisPiecePos = -4;
                                    tetrisPiecePos2 = -5;
                                    tetrisPiecePos3 = 5;
                                    tetrisPiecePos4 = 4;
                                }
                                else
                                {
                                    if (pieceType == 4)
                                    {
                                        tetrisPiecePos = -6;
                                        tetrisPiecePos2 = 4;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 6;
                                    }
                                    else
                                    {
                                        if (pieceType == 5)
                                        {
                                            tetrisPiecePos = -4;
                                            tetrisPiecePos = 6;
                                            tetrisPiecePos = 5;
                                            tetrisPiecePos = 4;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //erasing the last positions of pieces from screen
                if (tetrisPiecePos < 190 && tetrisPiecePos2 < 200 && tetrisPiecePos3 < 200 && tetrisPiecePos4 < 200 && pieceType == 5)
                {
                    if (tetrisPiecePos > 0)
                    {
                        GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                    }

                    if (tetrisPiecePos > 9)
                    {
                        GameLabel[tetrisPiecePos - 10].BackColor = blockEmptyColor[0];
                        GameLabel[tetrisPiecePos3 - 10].BackColor = blockEmptyColor[0];
                        GameLabel[tetrisPiecePos4 - 10].BackColor = blockEmptyColor[0];
                    }
                }
            }

            if (pieceType == 11)
            {
                tetrisPiecePos += 10;
                tetrisPiecePos2 += 10;
                tetrisPiecePos3 += 10;
                tetrisPiecePos4 += 10;

                //if hit a block it stop and start again from top
                if (tetrisPiecePos4 < 200)
                {
                    if (GameLabel[tetrisPiecePos4].BackColor == blockEmptyColor[1])
                    {
                        //erasing lines from the screen if full and movind down stack
                        int count, index;
                        for (int i = 19; i >= 0; i--)
                        {
                            count = 0;
                            for (int j = 9; j >= 0; j--)
                            {
                                index = i * 10 + j;
                                if (GameLabel[index].BackColor == blockEmptyColor[1])
                                {
                                    count++;
                                }
                            }
                            index = i * 10;
                            if (count == 10)
                            {
                                for (int p = index; p < index + 10; p++)
                                {
                                    GameLabel[p].BackColor = blockEmptyColor[0];
                                    blocksFilled[p] = false;
                                }
                                for (int p = index - 1; p >= 10; p--)
                                {
                                    if (GameLabel[p].BackColor == blockEmptyColor[1])
                                    {
                                        blocksFilled[p] = false;
                                        GameLabel[p].BackColor = blockEmptyColor[0];
                                        int countdown = 10;
                                        GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                    }
                                }
                                score += 10;
                                i++;
                            }
                        }
                        rand = new Random();
                        pieceType = rand.Next(5);
                        if (pieceType == 1)
                        {
                            tetrisPiecePos = -6;
                            tetrisPiecePos2 = -5;
                            tetrisPiecePos3 = tetrisPiecePos + 10;
                            tetrisPiecePos4 = tetrisPiecePos2 + 10;
                        }
                        else
                        {
                            if (pieceType == 0)
                            {
                                tetrisPiecePos = -7;
                                tetrisPiecePos2 = -6;
                                tetrisPiecePos3 = -5;
                                tetrisPiecePos4 = -4;
                            }
                            else
                            {
                                if (pieceType == 2)
                                {
                                    tetrisPiecePos = -6;
                                    tetrisPiecePos2 = -5;
                                    tetrisPiecePos3 = tetrisPiecePos2 + 10;
                                    tetrisPiecePos4 = tetrisPiecePos3 + 1;
                                }
                                else
                                {
                                    if (pieceType == 3)
                                    {
                                        tetrisPiecePos = -4;
                                        tetrisPiecePos2 = -5;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 4;
                                    }
                                    else
                                    {
                                        if (pieceType == 4)
                                        {
                                            tetrisPiecePos = -6;
                                            tetrisPiecePos2 = 4;
                                            tetrisPiecePos3 = 5;
                                            tetrisPiecePos4 = 6;
                                        }
                                        else
                                        {
                                            if (pieceType == 5)
                                            {
                                                tetrisPiecePos = -4;
                                                tetrisPiecePos = 6;
                                                tetrisPiecePos = 5;
                                                tetrisPiecePos = 4;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }    
                }

                //if piece cross the map a new piece start from top
                if (tetrisPiecePos4 > 200)
                {
                    //erasing lines from the screen if full and movind down stack
                    int count, index;
                    for (int i = 19; i >= 0; i--)
                    {
                        count = 0;
                        for (int j = 9; j >= 0; j--)
                        {
                            index = i * 10 + j;
                            if (GameLabel[index].BackColor == blockEmptyColor[1])
                            {
                                count++;
                            }
                        }
                        index = i * 10;
                        if (count == 10)
                        {
                            for (int p = index; p < index + 10; p++)
                            {
                                GameLabel[p].BackColor = blockEmptyColor[0];
                                blocksFilled[p] = false;
                            }
                            for (int p = index - 1; p >= 10; p--)
                            {
                                if (GameLabel[p].BackColor == blockEmptyColor[1])
                                {
                                    blocksFilled[p] = false;
                                    GameLabel[p].BackColor = blockEmptyColor[0];
                                    int countdown = 10;
                                    GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                }
                            }
                            score += 10;
                            i++;
                        }
                    }
                    rand = new Random();
                    pieceType = rand.Next(5);
                    if (pieceType == 1)
                    {
                        tetrisPiecePos = -6;
                        tetrisPiecePos2 = -5;
                        tetrisPiecePos3 = tetrisPiecePos + 10;
                        tetrisPiecePos4 = tetrisPiecePos2 + 10;
                    }
                    else
                    {
                        if (pieceType == 0)
                        {
                            tetrisPiecePos = -7;
                            tetrisPiecePos2 = -6;
                            tetrisPiecePos3 = -5;
                            tetrisPiecePos4 = -4;
                        }
                        else
                        {
                            if (pieceType == 2)
                            {
                                tetrisPiecePos = -6;
                                tetrisPiecePos2 = -5;
                                tetrisPiecePos3 = tetrisPiecePos2 + 10;
                                tetrisPiecePos4 = tetrisPiecePos3 + 1;
                            }
                            else
                            {
                                if (pieceType == 3)
                                {
                                    tetrisPiecePos = -4;
                                    tetrisPiecePos2 = -5;
                                    tetrisPiecePos3 = 5;
                                    tetrisPiecePos4 = 4;
                                }
                                else
                                {
                                    if (pieceType == 4)
                                    {
                                        tetrisPiecePos = -6;
                                        tetrisPiecePos2 = 4;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 6;
                                    }
                                    else
                                    {
                                        if (pieceType == 5)
                                        {
                                            tetrisPiecePos = -4;
                                            tetrisPiecePos = 6;
                                            tetrisPiecePos = 5;
                                            tetrisPiecePos = 4;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //erasing the last positions of pieces from screen
                if (tetrisPiecePos4 < 200 && pieceType == 11)
                {
                    if (GameLabel[tetrisPiecePos4].BackColor == blockEmptyColor[0])
                    {
                        GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                        if (tetrisPiecePos > 10)
                        {
                            GameLabel[tetrisPiecePos - 10].BackColor = blockEmptyColor[0];
                        }
                    }
                }

            }
            if (pieceType == 21)
            {
                tetrisPiecePos += 10;
                tetrisPiecePos2 += 10;
                tetrisPiecePos3 += 10;
                tetrisPiecePos4 += 10;
                
                //if hit a block it stop and start again from top
                if (tetrisPiecePos < 190 && tetrisPiecePos2 < 190 && tetrisPiecePos3 < 200 && tetrisPiecePos4 < 200)
                {
                    if (GameLabel[tetrisPiecePos2].BackColor == blockEmptyColor[1])
                    {
                        //erasing lines from the screen if full and movind down stack
                        int count, index;
                        for (int i = 19; i >= 0; i--)
                        {
                            count = 0;
                            for (int j = 9; j >= 0; j--)
                            {
                                index = i * 10 + j;
                                if (GameLabel[index].BackColor == blockEmptyColor[1])
                                {
                                    count++;
                                }
                            }
                            index = i * 10;
                            if (count == 10)
                            {
                                for (int p = index; p < index + 10; p++)
                                {
                                    GameLabel[p].BackColor = blockEmptyColor[0];
                                    blocksFilled[p] = false;
                                }
                                for (int p = index - 1; p >= 10; p--)
                                {
                                    if (GameLabel[p].BackColor == blockEmptyColor[1])
                                    {
                                        blocksFilled[p] = false;
                                        GameLabel[p].BackColor = blockEmptyColor[0];
                                        int countdown = 10;
                                        GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                    }
                                }
                                score += 10;
                                i++;
                            }
                        }
                        rand = new Random();
                        pieceType = rand.Next(5);
                        if (pieceType == 1)
                        {
                            tetrisPiecePos = -6;
                            tetrisPiecePos2 = -5;
                            tetrisPiecePos3 = tetrisPiecePos + 10;
                            tetrisPiecePos4 = tetrisPiecePos2 + 10;
                        }
                        else
                        {
                            if (pieceType == 0)
                            {
                                tetrisPiecePos = -7;
                                tetrisPiecePos2 = -6;
                                tetrisPiecePos3 = -5;
                                tetrisPiecePos4 = -4;
                            }
                            else
                            {
                                if (pieceType == 2)
                                {
                                    tetrisPiecePos = -6;
                                    tetrisPiecePos2 = -5;
                                    tetrisPiecePos3 = 5;
                                    tetrisPiecePos4 = 6;
                                }
                                else
                                {
                                    if (pieceType == 3)
                                    {
                                        tetrisPiecePos = -4;
                                        tetrisPiecePos2 = -5;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 4;
                                    }
                                    else
                                    {
                                        if (pieceType == 4)
                                        {
                                            tetrisPiecePos = -6;
                                            tetrisPiecePos2 = 4;
                                            tetrisPiecePos3 = 5;
                                            tetrisPiecePos4 = 6;
                                        }
                                        else
                                        {
                                            if (pieceType == 5)
                                            {
                                                tetrisPiecePos = -4;
                                                tetrisPiecePos = 6;
                                                tetrisPiecePos = 5;
                                                tetrisPiecePos = 4;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if(GameLabel[tetrisPiecePos4].BackColor == blockEmptyColor[1])
                        {
                            //erasing lines from the screen if full and movind down stack
                            int count, index;
                            for (int i = 19; i >= 0; i--)
                            {
                                count = 0;
                                for (int j = 9; j >= 0; j--)
                                {
                                    index = i * 10 + j;
                                    if (GameLabel[index].BackColor == blockEmptyColor[1])
                                    {
                                        count++;
                                    }
                                }
                                index = i * 10;
                                if (count == 10)
                                {
                                    for (int p = index; p < index + 10; p++)
                                    {
                                        GameLabel[p].BackColor = blockEmptyColor[0];
                                        blocksFilled[p] = false;
                                    }
                                    for (int p = index - 1; p >= 10; p--)
                                    {
                                        if (GameLabel[p].BackColor == blockEmptyColor[1])
                                        {
                                            blocksFilled[p] = false;
                                            GameLabel[p].BackColor = blockEmptyColor[0];
                                            int countdown = 10;
                                            GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                        }
                                    }
                                    score += 10;
                                    i++;
                                }
                            }
                            rand = new Random();
                            pieceType = rand.Next(5);
                            if (pieceType == 1)
                            {
                                tetrisPiecePos = -6;
                                tetrisPiecePos2 = -5;
                                tetrisPiecePos3 = tetrisPiecePos + 10;
                                tetrisPiecePos4 = tetrisPiecePos2 + 10;
                            }
                            else
                            {
                                if (pieceType == 0)
                                {
                                    tetrisPiecePos = -7;
                                    tetrisPiecePos2 = -6;
                                    tetrisPiecePos3 = -5;
                                    tetrisPiecePos4 = -4;
                                }
                                else
                                {
                                    if (pieceType == 2)
                                    {
                                        tetrisPiecePos = -6;
                                        tetrisPiecePos2 = -5;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 6;
                                    }
                                    else
                                    {
                                        if (pieceType == 3)
                                        {
                                            tetrisPiecePos = -4;
                                            tetrisPiecePos2 = -5;
                                            tetrisPiecePos3 = 5;
                                            tetrisPiecePos4 = 4;
                                        }
                                        else
                                        {
                                            if (pieceType == 4)
                                            {
                                                tetrisPiecePos = -6;
                                                tetrisPiecePos2 = 4;
                                                tetrisPiecePos3 = 5;
                                                tetrisPiecePos4 = 6;
                                            }
                                            else
                                            {
                                                if (pieceType == 5)
                                                {
                                                    tetrisPiecePos = -4;
                                                    tetrisPiecePos = 6;
                                                    tetrisPiecePos = 5;
                                                    tetrisPiecePos = 4;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //if piece cross the map a new piece start from top
                if (tetrisPiecePos > 190 && tetrisPiecePos2 > 190 && tetrisPiecePos4 > 200 && tetrisPiecePos3 > 200)
                {
                    //erasing lines from the screen if full and movind down stack
                    int count, index;
                    for (int i = 19; i >= 0; i--)
                    {
                        count = 0;
                        for (int j = 9; j >= 0; j--)
                        {
                            index = i * 10 + j;
                            if (GameLabel[index].BackColor == blockEmptyColor[1])
                            {
                                count++;
                            }
                        }
                        index = i * 10;
                        if (count == 10)
                        {
                            for (int p = index; p < index + 10; p++)
                            {
                                GameLabel[p].BackColor = blockEmptyColor[0];
                                blocksFilled[p] = false;
                            }
                            for (int p = index - 1; p >= 10; p--)
                            {
                                if (GameLabel[p].BackColor == blockEmptyColor[1])
                                {
                                    blocksFilled[p] = false;
                                    GameLabel[p].BackColor = blockEmptyColor[0];
                                    int countdown = 10;
                                    GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                }
                            }
                            score += 10;
                            i++;
                        }
                    }
                    rand = new Random();
                    pieceType = rand.Next(5);
                    if (pieceType == 1)
                    {
                        tetrisPiecePos = -6;
                        tetrisPiecePos2 = -5;
                        tetrisPiecePos3 = tetrisPiecePos + 10;
                        tetrisPiecePos4 = tetrisPiecePos2 + 10;
                    }
                    else
                    {
                        if (pieceType == 0)
                        {
                            tetrisPiecePos = -7;
                            tetrisPiecePos2 = -6;
                            tetrisPiecePos3 = -5;
                            tetrisPiecePos4 = -4;
                        }
                        else
                        {
                            if (pieceType == 2)
                            {
                                tetrisPiecePos = -6;
                                tetrisPiecePos2 = -5;
                                tetrisPiecePos3 = 5;
                                tetrisPiecePos4 = 6;
                            }
                            else
                            {
                                if (pieceType == 3)
                                {
                                    tetrisPiecePos = -4;
                                    tetrisPiecePos2 = -5;
                                    tetrisPiecePos3 = 5;
                                    tetrisPiecePos4 = 4;
                                }
                                else
                                {
                                    if (pieceType == 4)
                                    {
                                        tetrisPiecePos = -6;
                                        tetrisPiecePos2 = 4;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 6;
                                    }
                                    else
                                    {
                                        if (pieceType == 5)
                                        {
                                            tetrisPiecePos = -4;
                                            tetrisPiecePos = 6;
                                            tetrisPiecePos = 5;
                                            tetrisPiecePos = 4;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //erasing the last positions of pieces from screen
                if (tetrisPiecePos < 190 && tetrisPiecePos2 < 190 && tetrisPiecePos3 < 200 && tetrisPiecePos4 < 200 && pieceType == 21)
                {
                    if (GameLabel[tetrisPiecePos4].BackColor != blockEmptyColor[1])
                    {
                        if (tetrisPiecePos > 0)
                        {
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                        }
                        if (tetrisPiecePos > 9)
                        {
                            GameLabel[tetrisPiecePos - 10].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos3 - 10].BackColor = blockEmptyColor[0];
                        }
                    }
                }
            }
            if (pieceType == 31)
            {
                tetrisPiecePos += 10;
                tetrisPiecePos2 += 10;
                tetrisPiecePos3 += 10;
                tetrisPiecePos4 += 10;

                //if hit a block it stop and start again from top
                if (tetrisPiecePos < 190 && tetrisPiecePos2 < 190 && tetrisPiecePos3 < 200 && tetrisPiecePos4 < 200)
                {
                    if (GameLabel[tetrisPiecePos2].BackColor == blockEmptyColor[1])
                    {
                        //erasing lines from the screen if full and movind down stack
                        int count, index;
                        for (int i = 19; i >= 0; i--)
                        {
                            count = 0;
                            for (int j = 9; j >= 0; j--)
                            {
                                index = i * 10 + j;
                                if (GameLabel[index].BackColor == blockEmptyColor[1])
                                {
                                    count++;
                                }
                            }
                            index = i * 10;
                            if (count == 10)
                            {
                                for (int p = index; p < index + 10; p++)
                                {
                                    GameLabel[p].BackColor = blockEmptyColor[0];
                                    blocksFilled[p] = false;
                                }
                                for (int p = index - 1; p >= 10; p--)
                                {
                                    if (GameLabel[p].BackColor == blockEmptyColor[1])
                                    {
                                        blocksFilled[p] = false;
                                        GameLabel[p].BackColor = blockEmptyColor[0];
                                        int countdown = 10;
                                        GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                    }
                                }
                                score += 10;
                                i++;
                            }
                        }
                        rand = new Random();
                        pieceType = rand.Next(5);
                        if (pieceType == 1)
                        {
                            tetrisPiecePos = -6;
                            tetrisPiecePos2 = -5;
                            tetrisPiecePos3 = tetrisPiecePos + 10;
                            tetrisPiecePos4 = tetrisPiecePos2 + 10;
                        }
                        else
                        {
                            if (pieceType == 0)
                            {
                                tetrisPiecePos = -7;
                                tetrisPiecePos2 = -6;
                                tetrisPiecePos3 = -5;
                                tetrisPiecePos4 = -4;
                            }
                            else
                            {
                                if (pieceType == 2)
                                {
                                    tetrisPiecePos = -6;
                                    tetrisPiecePos2 = -5;
                                    tetrisPiecePos3 = 5;
                                    tetrisPiecePos4 = 6;
                                }
                                else
                                {
                                    if (pieceType == 3)
                                    {
                                        tetrisPiecePos = -4;
                                        tetrisPiecePos2 = -5;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 4;
                                    }
                                    else
                                    {
                                        if (pieceType == 4)
                                        {
                                            tetrisPiecePos = -6;
                                            tetrisPiecePos2 = 4;
                                            tetrisPiecePos3 = 5;
                                            tetrisPiecePos4 = 6;
                                        }
                                        else
                                        {
                                            if (pieceType == 5)
                                            {
                                                tetrisPiecePos = -4;
                                                tetrisPiecePos = 6;
                                                tetrisPiecePos = 5;
                                                tetrisPiecePos = 4;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if(GameLabel[tetrisPiecePos4].BackColor == blockEmptyColor[1])
                        {
                            //erasing lines from the screen if full and movind down stack
                            int count, index;
                            for (int i = 19; i >= 0; i--)
                            {
                                count = 0;
                                for (int j = 9; j >= 0; j--)
                                {
                                    index = i * 10 + j;
                                    if (GameLabel[index].BackColor == blockEmptyColor[1])
                                    {
                                        count++;
                                    }
                                }
                                index = i * 10;
                                if (count == 10)
                                {
                                    for (int p = index; p < index + 10; p++)
                                    {
                                        GameLabel[p].BackColor = blockEmptyColor[0];
                                        blocksFilled[p] = false;
                                    }
                                    for (int p = index - 1; p >= 10; p--)
                                    {
                                        if (GameLabel[p].BackColor == blockEmptyColor[1])
                                        {
                                            blocksFilled[p] = false;
                                            GameLabel[p].BackColor = blockEmptyColor[0];
                                            int countdown = 10;
                                            GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                        }
                                    }
                                    score += 10;
                                    i++;
                                }
                            }
                            rand = new Random();
                            pieceType = rand.Next(5);
                            if (pieceType == 1)
                            {
                                tetrisPiecePos = -6;
                                tetrisPiecePos2 = -5;
                                tetrisPiecePos3 = tetrisPiecePos + 10;
                                tetrisPiecePos4 = tetrisPiecePos2 + 10;
                            }
                            else
                            {
                                if (pieceType == 0)
                                {
                                    tetrisPiecePos = -7;
                                    tetrisPiecePos2 = -6;
                                    tetrisPiecePos3 = -5;
                                    tetrisPiecePos4 = -4;
                                }
                                else
                                {
                                    if (pieceType == 2)
                                    {
                                        tetrisPiecePos = -6;
                                        tetrisPiecePos2 = -5;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 6;
                                    }
                                    else
                                    {
                                        if (pieceType == 3)
                                        {
                                            tetrisPiecePos = -4;
                                            tetrisPiecePos2 = -5;
                                            tetrisPiecePos3 = 5;
                                            tetrisPiecePos4 = 4;
                                        }
                                        else
                                        {
                                            if (pieceType == 4)
                                            {
                                                tetrisPiecePos = -6;
                                                tetrisPiecePos2 = 4;
                                                tetrisPiecePos3 = 5;
                                                tetrisPiecePos4 = 6;
                                            }
                                            else
                                            {
                                                if (pieceType == 5)
                                                {
                                                    tetrisPiecePos = -4;
                                                    tetrisPiecePos = 6;
                                                    tetrisPiecePos = 5;
                                                    tetrisPiecePos = 4;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //if piece cross the map a new piece start from top
                if (tetrisPiecePos > 190 && tetrisPiecePos2 > 190 && tetrisPiecePos4 > 200 && tetrisPiecePos3 > 200)
                {
                    //erasing lines from the screen if full and movind down stack
                    int count, index;
                    for (int i = 19; i >= 0; i--)
                    {
                        count = 0;
                        for (int j = 9; j >= 0; j--)
                        {
                            index = i * 10 + j;
                            if (GameLabel[index].BackColor == blockEmptyColor[1])
                            {
                                count++;
                            }
                        }
                        index = i * 10;
                        if (count == 10)
                        {
                            for (int p = index; p < index + 10; p++)
                            {
                                GameLabel[p].BackColor = blockEmptyColor[0];
                                blocksFilled[p] = false;
                            }
                            for (int p = index - 1; p >= 10; p--)
                            {
                                if (GameLabel[p].BackColor == blockEmptyColor[1])
                                {
                                    blocksFilled[p] = false;
                                    GameLabel[p].BackColor = blockEmptyColor[0];
                                    int countdown = 10;
                                    GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                }
                            }
                            score += 10;
                            i++;
                        }
                    }
                    rand = new Random();
                    pieceType = rand.Next(5);
                    if (pieceType == 1)
                    {
                        tetrisPiecePos = -6;
                        tetrisPiecePos2 = -5;
                        tetrisPiecePos3 = tetrisPiecePos + 10;
                        tetrisPiecePos4 = tetrisPiecePos2 + 10;
                    }
                    else
                    {
                        if (pieceType == 0)
                        {
                            tetrisPiecePos = -7;
                            tetrisPiecePos2 = -6;
                            tetrisPiecePos3 = -5;
                            tetrisPiecePos4 = -4;
                        }
                        else
                        {
                            if (pieceType == 2)
                            {
                                tetrisPiecePos = -6;
                                tetrisPiecePos2 = -5;
                                tetrisPiecePos3 = 5;
                                tetrisPiecePos4 = 6;
                            }
                            else
                            {
                                if (pieceType == 3)
                                {
                                    tetrisPiecePos = -4;
                                    tetrisPiecePos2 = -5;
                                    tetrisPiecePos3 = 5;
                                    tetrisPiecePos4 = 4;
                                }
                                else
                                {
                                    if (pieceType == 4)
                                    {
                                        tetrisPiecePos = -6;
                                        tetrisPiecePos2 = 4;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 6;
                                    }
                                    else
                                    {
                                        if (pieceType == 5)
                                        {
                                            tetrisPiecePos = -4;
                                            tetrisPiecePos = 6;
                                            tetrisPiecePos = 5;
                                            tetrisPiecePos = 4;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //erasing the last positions of pieces from screen
                if (tetrisPiecePos < 190 && tetrisPiecePos2 < 190 && tetrisPiecePos3 < 200 && tetrisPiecePos4 < 200 && pieceType == 31)
                {
                    if (GameLabel[tetrisPiecePos4].BackColor != blockEmptyColor[1])
                    {
                        if (tetrisPiecePos > 0)
                        {
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                        }
                        if (tetrisPiecePos > 9)
                        {
                            GameLabel[tetrisPiecePos - 10].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos3 - 10].BackColor = blockEmptyColor[0];
                        }
                    }
                }
            }
            if (pieceType == 41)
            {
                tetrisPiecePos += 10;
                tetrisPiecePos2 += 10;
                tetrisPiecePos3 += 10;
                tetrisPiecePos4 += 10;

                //if hit a block it stop and start again from top
                if (tetrisPiecePos < 200 && tetrisPiecePos2 < 200 && tetrisPiecePos3 < 190 && tetrisPiecePos4 < 180)
                {
                    if (GameLabel[tetrisPiecePos2].BackColor == blockEmptyColor[1])
                    {
                        //erasing lines from the screen if full and movind down stack
                        int count, index;
                        for (int i = 19; i >= 0; i--)
                        {
                            count = 0;
                            for (int j = 9; j >= 0; j--)
                            {
                                index = i * 10 + j;
                                if (GameLabel[index].BackColor == blockEmptyColor[1])
                                {
                                    count++;
                                }
                            }
                            index = i * 10;
                            if (count == 10)
                            {
                                for (int p = index; p < index + 10; p++)
                                {
                                    GameLabel[p].BackColor = blockEmptyColor[0];
                                    blocksFilled[p] = false;
                                }
                                for (int p = index - 1; p >= 10; p--)
                                {
                                    if (GameLabel[p].BackColor == blockEmptyColor[1])
                                    {
                                        blocksFilled[p] = false;
                                        GameLabel[p].BackColor = blockEmptyColor[0];
                                        int countdown = 10;
                                        GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                    }
                                }
                                score += 10;
                                i++;
                            }
                        }
                        rand = new Random();
                        pieceType = rand.Next(5);
                        if (pieceType == 1)
                        {
                            tetrisPiecePos = -6;
                            tetrisPiecePos2 = -5;
                            tetrisPiecePos3 = tetrisPiecePos + 10;
                            tetrisPiecePos4 = tetrisPiecePos2 + 10;
                        }
                        else
                        {
                            if (pieceType == 0)
                            {
                                tetrisPiecePos = -7;
                                tetrisPiecePos2 = -6;
                                tetrisPiecePos3 = -5;
                                tetrisPiecePos4 = -4;
                            }
                            else
                            {
                                if (pieceType == 2)
                                {
                                    tetrisPiecePos = -6;
                                    tetrisPiecePos2 = -5;
                                    tetrisPiecePos3 = 5;
                                    tetrisPiecePos4 = 6;
                                }
                                else
                                {
                                    if (pieceType == 3)
                                    {
                                        tetrisPiecePos = -4;
                                        tetrisPiecePos2 = -5;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 4;
                                    }
                                    else
                                    {
                                        if (pieceType == 4)
                                        {
                                            tetrisPiecePos = -6;
                                            tetrisPiecePos2 = 4;
                                            tetrisPiecePos3 = 5;
                                            tetrisPiecePos4 = 6;
                                        }
                                        else
                                        {
                                            if (pieceType == 5)
                                            {
                                                tetrisPiecePos = -4;
                                                tetrisPiecePos = 6;
                                                tetrisPiecePos = 5;
                                                tetrisPiecePos = 4;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if(GameLabel[tetrisPiecePos].BackColor == blockEmptyColor[1])
                        {
                            //erasing lines from the screen if full and movind down stack
                            int count, index;
                            for (int i = 19; i >= 0; i--)
                            {
                                count = 0;
                                for (int j = 9; j >= 0; j--)
                                {
                                    index = i * 10 + j;
                                    if (GameLabel[index].BackColor == blockEmptyColor[1])
                                    {
                                        count++;
                                    }
                                }
                                index = i * 10;
                                if (count == 10)
                                {
                                    for (int p = index; p < index + 10; p++)
                                    {
                                        GameLabel[p].BackColor = blockEmptyColor[0];
                                        blocksFilled[p] = false;
                                    }
                                    for (int p = index - 1; p >= 10; p--)
                                    {
                                        if (GameLabel[p].BackColor == blockEmptyColor[1])
                                        {
                                            blocksFilled[p] = false;
                                            GameLabel[p].BackColor = blockEmptyColor[0];
                                            int countdown = 10;
                                            GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                        }
                                    }
                                    score += 10;
                                    i++;
                                }
                            }
                            rand = new Random();
                            pieceType = rand.Next(5);
                            if (pieceType == 1)
                            {
                                tetrisPiecePos = -6;
                                tetrisPiecePos2 = -5;
                                tetrisPiecePos3 = tetrisPiecePos + 10;
                                tetrisPiecePos4 = tetrisPiecePos2 + 10;
                            }
                            else
                            {
                                if (pieceType == 0)
                                {
                                    tetrisPiecePos = -7;
                                    tetrisPiecePos2 = -6;
                                    tetrisPiecePos3 = -5;
                                    tetrisPiecePos4 = -4;
                                }
                                else
                                {
                                    if (pieceType == 2)
                                    {
                                        tetrisPiecePos = -6;
                                        tetrisPiecePos2 = -5;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 6;
                                    }
                                    else
                                    {
                                        if (pieceType == 3)
                                        {
                                            tetrisPiecePos = -4;
                                            tetrisPiecePos2 = -5;
                                            tetrisPiecePos3 = 5;
                                            tetrisPiecePos4 = 4;
                                        }
                                        else
                                        {
                                            if (pieceType == 4)
                                            {
                                                tetrisPiecePos = -6;
                                                tetrisPiecePos2 = 4;
                                                tetrisPiecePos3 = 5;
                                                tetrisPiecePos4 = 6;
                                            }
                                            else
                                            {
                                                if (pieceType == 5)
                                                {
                                                    tetrisPiecePos = -4;
                                                    tetrisPiecePos = 6;
                                                    tetrisPiecePos = 5;
                                                    tetrisPiecePos = 4;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //if piece cross the map a new piece start from top
                if (tetrisPiecePos > 200 && tetrisPiecePos2 > 200 && tetrisPiecePos4 > 190 && tetrisPiecePos3 > 180)
                {
                    //erasing lines from the screen if full and movind down stack
                    int count, index;
                    for (int i = 19; i >= 0; i--)
                    {
                        count = 0;
                        for (int j = 9; j >= 0; j--)
                        {
                            index = i * 10 + j;
                            if (GameLabel[index].BackColor == blockEmptyColor[1])
                            {
                                count++;
                            }
                        }
                        index = i * 10;
                        if (count == 10)
                        {
                            for (int p = index; p < index + 10; p++)
                            {
                                GameLabel[p].BackColor = blockEmptyColor[0];
                                blocksFilled[p] = false;
                            }
                            for (int p = index - 1; p >= 10; p--)
                            {
                                if (GameLabel[p].BackColor == blockEmptyColor[1])
                                {
                                    blocksFilled[p] = false;
                                    GameLabel[p].BackColor = blockEmptyColor[0];
                                    int countdown = 10;
                                    GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                }
                            }
                            score += 10;
                            i++;
                        }
                    }
                    rand = new Random();
                    pieceType = rand.Next(5);
                    if (pieceType == 1)
                    {
                        tetrisPiecePos = -6;
                        tetrisPiecePos2 = -5;
                        tetrisPiecePos3 = tetrisPiecePos + 10;
                        tetrisPiecePos4 = tetrisPiecePos2 + 10;
                    }
                    else
                    {
                        if (pieceType == 0)
                        {
                            tetrisPiecePos = -7;
                            tetrisPiecePos2 = -6;
                            tetrisPiecePos3 = -5;
                            tetrisPiecePos4 = -4;
                        }
                        else
                        {
                            if (pieceType == 2)
                            {
                                tetrisPiecePos = -6;
                                tetrisPiecePos2 = -5;
                                tetrisPiecePos3 = 5;
                                tetrisPiecePos4 = 6;
                            }
                            else
                            {
                                if (pieceType == 3)
                                {
                                    tetrisPiecePos = -4;
                                    tetrisPiecePos2 = -5;
                                    tetrisPiecePos3 = 5;
                                    tetrisPiecePos4 = 4;
                                }
                                else
                                {
                                    if (pieceType == 4)
                                    {
                                        tetrisPiecePos = -6;
                                        tetrisPiecePos2 = 4;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 6;
                                    }
                                    else
                                    {
                                        if (pieceType == 5)
                                        {
                                            tetrisPiecePos = -4;
                                            tetrisPiecePos = 6;
                                            tetrisPiecePos = 5;
                                            tetrisPiecePos = 4;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //erasing the last positions of pieces from screen
                if (tetrisPiecePos < 200 && tetrisPiecePos2 < 200 && tetrisPiecePos3 < 190 && tetrisPiecePos4 < 180 && pieceType == 41)
                {
                    if (tetrisPiecePos > 0)
                    {
                        GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                    }
                    if (tetrisPiecePos > 9)
                    {
                        GameLabel[tetrisPiecePos - 10].BackColor = blockEmptyColor[0];
                        GameLabel[tetrisPiecePos4 - 10].BackColor = blockEmptyColor[0];
                    }
                }
            }
            if (pieceType == 42)
            {
                tetrisPiecePos += 10;
                tetrisPiecePos2 += 10;
                tetrisPiecePos3 += 10;
                tetrisPiecePos4 += 10;

                //if hit a block it stop and start again from top
                if (tetrisPiecePos < 200 && tetrisPiecePos2 < 190 && tetrisPiecePos3 < 190 && tetrisPiecePos4 < 190)
                {
                    if (GameLabel[tetrisPiecePos].BackColor == blockEmptyColor[1])
                    {
                        //erasing lines from the screen if full and movind down stack
                        int count, index;
                        for (int i = 19; i >= 0; i--)
                        {
                            count = 0;
                            for (int j = 9; j >= 0; j--)
                            {
                                index = i * 10 + j;
                                if (GameLabel[index].BackColor == blockEmptyColor[1])
                                {
                                    count++;
                                }
                            }
                            index = i * 10;
                            if (count == 10)
                            {
                                for (int p = index; p < index + 10; p++)
                                {
                                    GameLabel[p].BackColor = blockEmptyColor[0];
                                    blocksFilled[p] = false;
                                }
                                for (int p = index - 1; p >= 10; p--)
                                {
                                    if (GameLabel[p].BackColor == blockEmptyColor[1])
                                    {
                                        blocksFilled[p] = false;
                                        GameLabel[p].BackColor = blockEmptyColor[0];
                                        int countdown = 10;
                                        GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                    }
                                }
                                score += 10;
                                i++;
                            }
                        }
                        rand = new Random();
                        pieceType = rand.Next(5);
                        if (pieceType == 1)
                        {
                            tetrisPiecePos = -6;
                            tetrisPiecePos2 = -5;
                            tetrisPiecePos3 = tetrisPiecePos + 10;
                            tetrisPiecePos4 = tetrisPiecePos2 + 10;
                        }
                        else
                        {
                            if (pieceType == 0)
                            {
                                tetrisPiecePos = -7;
                                tetrisPiecePos2 = -6;
                                tetrisPiecePos3 = -5;
                                tetrisPiecePos4 = -4;
                            }
                            else
                            {
                                if (pieceType == 2)
                                {
                                    tetrisPiecePos = -6;
                                    tetrisPiecePos2 = -5;
                                    tetrisPiecePos3 = 5;
                                    tetrisPiecePos4 = 6;
                                }
                                else
                                {
                                    if (pieceType == 3)
                                    {
                                        tetrisPiecePos = -4;
                                        tetrisPiecePos2 = -5;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 4;
                                    }
                                    else
                                    {
                                        if (pieceType == 4)
                                        {
                                            tetrisPiecePos = -6;
                                            tetrisPiecePos2 = 4;
                                            tetrisPiecePos3 = 5;
                                            tetrisPiecePos4 = 6;
                                        }
                                        else
                                        {
                                            if (pieceType == 5)
                                            {
                                                tetrisPiecePos = -4;
                                                tetrisPiecePos = 6;
                                                tetrisPiecePos = 5;
                                                tetrisPiecePos = 4;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if(GameLabel[tetrisPiecePos4].BackColor == blockEmptyColor[1])
                        {
                            //erasing lines from the screen if full and movind down stack
                            int count, index;
                            for (int i = 19; i >= 0; i--)
                            {
                                count = 0;
                                for (int j = 9; j >= 0; j--)
                                {
                                    index = i * 10 + j;
                                    if (GameLabel[index].BackColor == blockEmptyColor[1])
                                    {
                                        count++;
                                    }
                                }
                                index = i * 10;
                                if (count == 10)
                                {
                                    for (int p = index; p < index + 10; p++)
                                    {
                                        GameLabel[p].BackColor = blockEmptyColor[0];
                                        blocksFilled[p] = false;
                                    }
                                    for (int p = index - 1; p >= 10; p--)
                                    {
                                        if (GameLabel[p].BackColor == blockEmptyColor[1])
                                        {
                                            blocksFilled[p] = false;
                                            GameLabel[p].BackColor = blockEmptyColor[0];
                                            int countdown = 10;
                                            GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                        }
                                    }
                                    score += 10;
                                    i++;
                                }
                            }
                            rand = new Random();
                            pieceType = rand.Next(5);
                            if (pieceType == 1)
                            {
                                tetrisPiecePos = -6;
                                tetrisPiecePos2 = -5;
                                tetrisPiecePos3 = tetrisPiecePos + 10;
                                tetrisPiecePos4 = tetrisPiecePos2 + 10;
                            }
                            else
                            {
                                if (pieceType == 0)
                                {
                                    tetrisPiecePos = -7;
                                    tetrisPiecePos2 = -6;
                                    tetrisPiecePos3 = -5;
                                    tetrisPiecePos4 = -4;
                                }
                                else
                                {
                                    if (pieceType == 2)
                                    {
                                        tetrisPiecePos = -6;
                                        tetrisPiecePos2 = -5;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 6;
                                    }
                                    else
                                    {
                                        if (pieceType == 3)
                                        {
                                            tetrisPiecePos = -4;
                                            tetrisPiecePos2 = -5;
                                            tetrisPiecePos3 = 5;
                                            tetrisPiecePos4 = 4;
                                        }
                                        else
                                        {
                                            if (pieceType == 4)
                                            {
                                                tetrisPiecePos = -6;
                                                tetrisPiecePos2 = 4;
                                                tetrisPiecePos3 = 5;
                                                tetrisPiecePos4 = 6;
                                            }
                                            else
                                            {
                                                if (pieceType == 5)
                                                {
                                                    tetrisPiecePos = -4;
                                                    tetrisPiecePos = 6;
                                                    tetrisPiecePos = 5;
                                                    tetrisPiecePos = 4;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if(GameLabel[tetrisPiecePos3].BackColor == blockEmptyColor[1])
                            {
                                //erasing lines from the screen if full and movind down stack
                                int count, index;
                                for (int i = 19; i >= 0; i--)
                                {
                                    count = 0;
                                    for (int j = 9; j >= 0; j--)
                                    {
                                        index = i * 10 + j;
                                        if (GameLabel[index].BackColor == blockEmptyColor[1])
                                        {
                                            count++;
                                        }
                                    }
                                    index = i * 10;
                                    if (count == 10)
                                    {
                                        for (int p = index; p < index + 10; p++)
                                        {
                                            GameLabel[p].BackColor = blockEmptyColor[0];
                                            blocksFilled[p] = false;
                                        }
                                        for (int p = index - 1; p >= 10; p--)
                                        {
                                            if (GameLabel[p].BackColor == blockEmptyColor[1])
                                            {
                                                blocksFilled[p] = false;
                                                GameLabel[p].BackColor = blockEmptyColor[0];
                                                int countdown = 10;
                                                GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                            }
                                        }
                                        score += 10;
                                        i++;
                                    }
                                }
                                rand = new Random();
                                pieceType = rand.Next(5);
                                if (pieceType == 1)
                                {
                                    tetrisPiecePos = -6;
                                    tetrisPiecePos2 = -5;
                                    tetrisPiecePos3 = tetrisPiecePos + 10;
                                    tetrisPiecePos4 = tetrisPiecePos2 + 10;
                                }
                                else
                                {
                                    if (pieceType == 0)
                                    {
                                        tetrisPiecePos = -7;
                                        tetrisPiecePos2 = -6;
                                        tetrisPiecePos3 = -5;
                                        tetrisPiecePos4 = -4;
                                    }
                                    else
                                    {
                                        if (pieceType == 2)
                                        {
                                            tetrisPiecePos = -6;
                                            tetrisPiecePos2 = -5;
                                            tetrisPiecePos3 = 5;
                                            tetrisPiecePos4 = 6;
                                        }
                                        else
                                        {
                                            if (pieceType == 3)
                                            {
                                                tetrisPiecePos = -4;
                                                tetrisPiecePos2 = -5;
                                                tetrisPiecePos3 = 5;
                                                tetrisPiecePos4 = 4;
                                            }
                                            else
                                            {
                                                if (pieceType == 4)
                                                {
                                                    tetrisPiecePos = -6;
                                                    tetrisPiecePos2 = 4;
                                                    tetrisPiecePos3 = 5;
                                                    tetrisPiecePos4 = 6;
                                                }
                                                else
                                                {
                                                    if (pieceType == 5)
                                                    {
                                                        tetrisPiecePos = -4;
                                                        tetrisPiecePos = 6;
                                                        tetrisPiecePos = 5;
                                                        tetrisPiecePos = 4;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //if piece cross the map a new piece start from top
                if (tetrisPiecePos > 200 && tetrisPiecePos2 > 190 && tetrisPiecePos4 > 190 && tetrisPiecePos3 > 190)
                {
                    //erasing lines from the screen if full and movind down stack
                    int count, index;
                    for (int i = 19; i >= 0; i--)
                    {
                        count = 0;
                        for (int j = 9; j >= 0; j--)
                        {
                            index = i * 10 + j;
                            if (GameLabel[index].BackColor == blockEmptyColor[1])
                            {
                                count++;
                            }
                        }
                        index = i * 10;
                        if (count == 10)
                        {
                            for (int p = index; p < index + 10; p++)
                            {
                                GameLabel[p].BackColor = blockEmptyColor[0];
                                blocksFilled[p] = false;
                            }
                            for (int p = index - 1; p >= 10; p--)
                            {
                                if (GameLabel[p].BackColor == blockEmptyColor[1])
                                {
                                    blocksFilled[p] = false;
                                    GameLabel[p].BackColor = blockEmptyColor[0];
                                    int countdown = 10;
                                    GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                }
                            }
                            score += 10;
                            i++;
                        }
                    }
                    rand = new Random();
                    pieceType = rand.Next(5);
                    if (pieceType == 1)
                    {
                        tetrisPiecePos = -6;
                        tetrisPiecePos2 = -5;
                        tetrisPiecePos3 = tetrisPiecePos + 10;
                        tetrisPiecePos4 = tetrisPiecePos2 + 10;
                    }
                    else
                    {
                        if (pieceType == 0)
                        {
                            tetrisPiecePos = -7;
                            tetrisPiecePos2 = -6;
                            tetrisPiecePos3 = -5;
                            tetrisPiecePos4 = -4;
                        }
                        else
                        {
                            if (pieceType == 2)
                            {
                                tetrisPiecePos = -6;
                                tetrisPiecePos2 = -5;
                                tetrisPiecePos3 = 5;
                                tetrisPiecePos4 = 6;
                            }
                            else
                            {
                                if (pieceType == 3)
                                {
                                    tetrisPiecePos = -4;
                                    tetrisPiecePos2 = -5;
                                    tetrisPiecePos3 = 5;
                                    tetrisPiecePos4 = 4;
                                }
                                else
                                {
                                    if (pieceType == 4)
                                    {
                                        tetrisPiecePos = -6;
                                        tetrisPiecePos2 = 4;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 6;
                                    }
                                    else
                                    {
                                        if (pieceType == 5)
                                        {
                                            tetrisPiecePos = -4;
                                            tetrisPiecePos = 6;
                                            tetrisPiecePos = 5;
                                            tetrisPiecePos = 4;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //erasing the last positions of pieces from screen
                if (tetrisPiecePos < 200 && tetrisPiecePos2 < 190 && tetrisPiecePos3 < 190 && tetrisPiecePos4 < 190 && pieceType == 42)
                {
                    if (tetrisPiecePos > 0)
                    {
                        GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                    }
                    if (tetrisPiecePos > 9)
                    {
                        GameLabel[tetrisPiecePos2 - 10].BackColor = blockEmptyColor[0];
                        GameLabel[tetrisPiecePos3 - 10].BackColor = blockEmptyColor[0];
                        GameLabel[tetrisPiecePos4 - 10].BackColor = blockEmptyColor[0];
                    }
                }
            }
            if (pieceType == 43)
            {
                tetrisPiecePos += 10;
                tetrisPiecePos2 += 10;
                tetrisPiecePos3 += 10;
                tetrisPiecePos4 += 10;

                //if hit a block it stop and start again from top
                if (tetrisPiecePos < 180 && tetrisPiecePos2 < 180 && tetrisPiecePos3 < 190 && tetrisPiecePos4 < 200)
                {
                    if (GameLabel[tetrisPiecePos].BackColor == blockEmptyColor[1])
                    {
                        //erasing lines from the screen if full and movind down stack
                        int count, index;
                        for (int i = 19; i >= 0; i--)
                        {
                            count = 0;
                            for (int j = 9; j >= 0; j--)
                            {
                                index = i * 10 + j;
                                if (GameLabel[index].BackColor == blockEmptyColor[1])
                                {
                                    count++;
                                }
                            }
                            index = i * 10;
                            if (count == 10)
                            {
                                for (int p = index; p < index + 10; p++)
                                {
                                    GameLabel[p].BackColor = blockEmptyColor[0];
                                    blocksFilled[p] = false;
                                }
                                for (int p = index - 1; p >= 10; p--)
                                {
                                    if (GameLabel[p].BackColor == blockEmptyColor[1])
                                    {
                                        blocksFilled[p] = false;
                                        GameLabel[p].BackColor = blockEmptyColor[0];
                                        int countdown = 10;
                                        GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                    }
                                }
                                score += 10;
                                i++;
                            }
                        }
                        rand = new Random();
                        pieceType = rand.Next(5);
                        if (pieceType == 1)
                        {
                            tetrisPiecePos = -6;
                            tetrisPiecePos2 = -5;
                            tetrisPiecePos3 = tetrisPiecePos + 10;
                            tetrisPiecePos4 = tetrisPiecePos2 + 10;
                        }
                        else
                        {
                            if (pieceType == 0)
                            {
                                tetrisPiecePos = -7;
                                tetrisPiecePos2 = -6;
                                tetrisPiecePos3 = -5;
                                tetrisPiecePos4 = -4;
                            }
                            else
                            {
                                if (pieceType == 2)
                                {
                                    tetrisPiecePos = -6;
                                    tetrisPiecePos2 = -5;
                                    tetrisPiecePos3 = 5;
                                    tetrisPiecePos4 = 6;
                                }
                                else
                                {
                                    if (pieceType == 3)
                                    {
                                        tetrisPiecePos = -4;
                                        tetrisPiecePos2 = -5;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 4;
                                    }
                                    else
                                    {
                                        if (pieceType == 4)
                                        {
                                            tetrisPiecePos = -6;
                                            tetrisPiecePos2 = 4;
                                            tetrisPiecePos3 = 5;
                                            tetrisPiecePos4 = 6;
                                        }
                                        else
                                        {
                                            if (pieceType == 5)
                                            {
                                                tetrisPiecePos = -4;
                                                tetrisPiecePos = 6;
                                                tetrisPiecePos = 5;
                                                tetrisPiecePos = 4;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if(GameLabel[tetrisPiecePos4].BackColor == blockEmptyColor[1])
                        {
                            //erasing lines from the screen if full and movind down stack
                            int count, index;
                            for (int i = 19; i >= 0; i--)
                            {
                                count = 0;
                                for (int j = 9; j >= 0; j--)
                                {
                                    index = i * 10 + j;
                                    if (GameLabel[index].BackColor == blockEmptyColor[1])
                                    {
                                        count++;
                                    }
                                }
                                index = i * 10;
                                if (count == 10)
                                {
                                    for (int p = index; p < index + 10; p++)
                                    {
                                        GameLabel[p].BackColor = blockEmptyColor[0];
                                        blocksFilled[p] = false;
                                    }
                                    for (int p = index - 1; p >= 10; p--)
                                    {
                                        if (GameLabel[p].BackColor == blockEmptyColor[1])
                                        {
                                            blocksFilled[p] = false;
                                            GameLabel[p].BackColor = blockEmptyColor[0];
                                            int countdown = 10;
                                            GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                        }
                                    }
                                    score += 10;
                                    i++;
                                }
                            }
                            rand = new Random();
                            pieceType = rand.Next(5);
                            if (pieceType == 1)
                            {
                                tetrisPiecePos = -6;
                                tetrisPiecePos2 = -5;
                                tetrisPiecePos3 = tetrisPiecePos + 10;
                                tetrisPiecePos4 = tetrisPiecePos2 + 10;
                            }
                            else
                            {
                                if (pieceType == 0)
                                {
                                    tetrisPiecePos = -7;
                                    tetrisPiecePos2 = -6;
                                    tetrisPiecePos3 = -5;
                                    tetrisPiecePos4 = -4;
                                }
                                else
                                {
                                    if (pieceType == 2)
                                    {
                                        tetrisPiecePos = -6;
                                        tetrisPiecePos2 = -5;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 6;
                                    }
                                    else
                                    {
                                        if (pieceType == 3)
                                        {
                                            tetrisPiecePos = -4;
                                            tetrisPiecePos2 = -5;
                                            tetrisPiecePos3 = 5;
                                            tetrisPiecePos4 = 4;
                                        }
                                        else
                                        {
                                            if (pieceType == 4)
                                            {
                                                tetrisPiecePos = -6;
                                                tetrisPiecePos2 = 4;
                                                tetrisPiecePos3 = 5;
                                                tetrisPiecePos4 = 6;
                                            }
                                            else
                                            {
                                                if (pieceType == 5)
                                                {
                                                    tetrisPiecePos = -4;
                                                    tetrisPiecePos = 6;
                                                    tetrisPiecePos = 5;
                                                    tetrisPiecePos = 4;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //if piece cross the map a new piece start from top
                if (tetrisPiecePos > 180 && tetrisPiecePos2 > 180 && tetrisPiecePos4 > 190 && tetrisPiecePos3 > 200)
                {
                    //erasing lines from the screen if full and movind down stack
                    int count, index;
                    for (int i = 19; i >= 0; i--)
                    {
                        count = 0;
                        for (int j = 9; j >= 0; j--)
                        {
                            index = i * 10 + j;
                            if (GameLabel[index].BackColor == blockEmptyColor[1])
                            {
                                count++;
                            }
                        }
                        index = i * 10;
                        if (count == 10)
                        {
                            for (int p = index; p < index + 10; p++)
                            {
                                GameLabel[p].BackColor = blockEmptyColor[0];
                                blocksFilled[p] = false;
                            }
                            for (int p = index - 1; p >= 10; p--)
                            {
                                if (GameLabel[p].BackColor == blockEmptyColor[1])
                                {
                                    blocksFilled[p] = false;
                                    GameLabel[p].BackColor = blockEmptyColor[0];
                                    int countdown = 10;
                                    GameLabel[p + countdown].BackColor = blockEmptyColor[1];
                                }
                            }
                            score += 10;
                            i++;
                        }
                    }
                    rand = new Random();
                    pieceType = rand.Next(5);
                    if (pieceType == 1)
                    {
                        tetrisPiecePos = -6;
                        tetrisPiecePos2 = -5;
                        tetrisPiecePos3 = tetrisPiecePos + 10;
                        tetrisPiecePos4 = tetrisPiecePos2 + 10;
                    }
                    else
                    {
                        if (pieceType == 0)
                        {
                            tetrisPiecePos = -7;
                            tetrisPiecePos2 = -6;
                            tetrisPiecePos3 = -5;
                            tetrisPiecePos4 = -4;
                        }
                        else
                        {
                            if (pieceType == 2)
                            {
                                tetrisPiecePos = -6;
                                tetrisPiecePos2 = -5;
                                tetrisPiecePos3 = 5;
                                tetrisPiecePos4 = 6;
                            }
                            else
                            {
                                if (pieceType == 3)
                                {
                                    tetrisPiecePos = -4;
                                    tetrisPiecePos2 = -5;
                                    tetrisPiecePos3 = 5;
                                    tetrisPiecePos4 = 4;
                                }
                                else
                                {
                                    if (pieceType == 4)
                                    {
                                        tetrisPiecePos = -6;
                                        tetrisPiecePos2 = 4;
                                        tetrisPiecePos3 = 5;
                                        tetrisPiecePos4 = 6;
                                    }
                                    else
                                    {
                                        if (pieceType == 5)
                                        {
                                            tetrisPiecePos = -4;
                                            tetrisPiecePos = 6;
                                            tetrisPiecePos = 5;
                                            tetrisPiecePos = 4;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //erasing the last positions of pieces from screen
                if (tetrisPiecePos < 180 && tetrisPiecePos2 < 180 && tetrisPiecePos3 < 190 && tetrisPiecePos4 < 200 && pieceType == 43)
                {
                    if (tetrisPiecePos > 0)
                    {
                        GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                    }
                    if (tetrisPiecePos > 9)
                    {
                        GameLabel[tetrisPiecePos - 10].BackColor = blockEmptyColor[0];
                        GameLabel[tetrisPiecePos2 - 10].BackColor = blockEmptyColor[0];
                    }
                }
            }

            //score label print and nivel label print
            label2.Text = score.ToString();
            if(score >= nivelcount && timer.Interval >= 50)
            {
                nivel++;
                nivelcount += 100;
                label4.Text = nivel.ToString();
                timer.Interval -= 10;
            }

            // restart or exit message if game end
            int pr = 10, kr = 0, okay = 0;
            for(int i = 0; i < 9; i++)
            {
                pr = 0;
                kr = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (GameLabel[i + pr].BackColor == blockEmptyColor[1])
                    {
                        kr++;
                    }
                    pr += 10;
                }
                if(kr==3)
                {
                    okay = 1;
                    break;
                }
            }
            if (okay == 1)
            {
                timer.Stop();
                DialogResult quitM = MessageBox.Show("You lost! Restart?", "Fail", MessageBoxButtons.YesNo);

                if (quitM == DialogResult.Yes)
                {
                    foreach (Label block in GameLabel)
                    {
                        block.BackColor = blockEmptyColor[0];
                    }
                    blocksFilled = new bool[200];

                    if (pieceType == 0)
                    {
                        tetrisPiecePos = -7;
                        tetrisPiecePos2 = -6;
                        tetrisPiecePos3 = -5;
                        tetrisPiecePos4 = -4;
                    }
                    if (pieceType == 1)
                    {
                        tetrisPiecePos = -6;
                        tetrisPiecePos2 = -5;
                        tetrisPiecePos3 = tetrisPiecePos + 10;
                        tetrisPiecePos4 = tetrisPiecePos2 + 10;
                    }
                    if (pieceType == 2)
                    {
                        tetrisPiecePos = -6;
                        tetrisPiecePos2 = -5;
                        tetrisPiecePos3 = 5;
                        tetrisPiecePos4 = 6;
                    }
                    if (pieceType == 3)
                    {
                        tetrisPiecePos = -6;
                        tetrisPiecePos2 = -5;
                        tetrisPiecePos3 = 5;
                        tetrisPiecePos4 = 6;
                    }
                    if (pieceType == 4)
                    {
                        tetrisPiecePos = -6;
                        tetrisPiecePos2 = 4;
                        tetrisPiecePos3 = 5;
                        tetrisPiecePos4 = 6;
                    }
                    if (pieceType == 5)
                    {
                        tetrisPiecePos = -4;
                        tetrisPiecePos = 6;
                        tetrisPiecePos = 5;
                        tetrisPiecePos = 4;
                    }

                    timer.Start();
                }
                else
                {
                    Environment.Exit(0);
                }
            }

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Left && tetrisPiecePos > 9)
            {
                if(tetrisPiecePos < 190)
                {
                    if (tetrisPiecePos != 0   && tetrisPiecePos != 10  && tetrisPiecePos != 30  && tetrisPiecePos != 50 &&
                        tetrisPiecePos != 60  && tetrisPiecePos != 70  && tetrisPiecePos != 80  && tetrisPiecePos != 90 &&
                        tetrisPiecePos != 100 && tetrisPiecePos != 110 && tetrisPiecePos != 120 && tetrisPiecePos != 130 &&
                        tetrisPiecePos != 140 && tetrisPiecePos != 150 && tetrisPiecePos != 160 && tetrisPiecePos != 170 &&
                        tetrisPiecePos != 180 && tetrisPiecePos != 190 && tetrisPiecePos != 20  && tetrisPiecePos != 40)
                    {
                        if (GameLabel[tetrisPiecePos - 1].BackColor != blockEmptyColor[1] && pieceType == 0)
                        {
                            tetrisPiecePos--;
                            tetrisPiecePos2--;
                            tetrisPiecePos3--;
                            tetrisPiecePos4--;
                            GameLabel[tetrisPiecePos4 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                            return true;
                        }
                        if (GameLabel[tetrisPiecePos3 - 1].BackColor != blockEmptyColor[1] && GameLabel[tetrisPiecePos - 1].BackColor != blockEmptyColor[1] && pieceType == 1)
                        {
                            tetrisPiecePos--;
                            tetrisPiecePos2--;
                            tetrisPiecePos3--;
                            tetrisPiecePos4--;
                            GameLabel[tetrisPiecePos2 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos4 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                            return true;
                        }
                        if (GameLabel[tetrisPiecePos - 1].BackColor != blockEmptyColor[1] && GameLabel[tetrisPiecePos2 - 1].BackColor != blockEmptyColor[1] &&
                            GameLabel[tetrisPiecePos3 - 1].BackColor != blockEmptyColor[1] && GameLabel[tetrisPiecePos4 - 1].BackColor != blockEmptyColor[1] && pieceType == 11)
                        {
                            tetrisPiecePos--;
                            tetrisPiecePos2--;
                            tetrisPiecePos3--;
                            tetrisPiecePos4--;
                            GameLabel[tetrisPiecePos + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos2 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos3 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos4 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                            return true;
                        }
                        if(GameLabel[tetrisPiecePos - 1].BackColor != blockEmptyColor[1] && GameLabel[tetrisPiecePos3 - 1].BackColor != blockEmptyColor[1] && pieceType == 2)
                        {
                            tetrisPiecePos--;
                            tetrisPiecePos2--;
                            tetrisPiecePos3--;
                            tetrisPiecePos4--;
                            GameLabel[tetrisPiecePos + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos2 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos3 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos4 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                            return true;
                        }
                        if(GameLabel[tetrisPiecePos - 1].BackColor != blockEmptyColor[1] && pieceType == 31)
                        {
                            tetrisPiecePos--;
                            tetrisPiecePos2--;
                            tetrisPiecePos3--;
                            tetrisPiecePos4--;
                            GameLabel[tetrisPiecePos + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos2 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos3 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos4 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                            return true;
                        }
                        if(GameLabel[tetrisPiecePos - 1].BackColor != blockEmptyColor[1] && GameLabel[tetrisPiecePos2 - 1].BackColor != blockEmptyColor[1] && pieceType == 4)
                        {
                            tetrisPiecePos--;
                            tetrisPiecePos2--;
                            tetrisPiecePos3--;
                            tetrisPiecePos4--;
                            GameLabel[tetrisPiecePos + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos2 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos3 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos4 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                            return true;
                        }
                        if(GameLabel[tetrisPiecePos - 1].BackColor != blockEmptyColor[1] && pieceType == 41)
                        {
                            tetrisPiecePos--;
                            tetrisPiecePos2--;
                            tetrisPiecePos3--;
                            tetrisPiecePos4--;
                            GameLabel[tetrisPiecePos + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos2 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos3 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos4 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                            return true;
                        }
                    }
                    if (tetrisPiecePos3 != 0 && tetrisPiecePos3 != 10 && tetrisPiecePos3 != 30 && tetrisPiecePos3 != 50 &&
                        tetrisPiecePos3 != 60 && tetrisPiecePos3 != 70 && tetrisPiecePos3 != 80 && tetrisPiecePos3 != 90 &&
                        tetrisPiecePos3 != 100 && tetrisPiecePos3 != 110 && tetrisPiecePos3 != 120 && tetrisPiecePos3 != 130 &&
                        tetrisPiecePos3 != 140 && tetrisPiecePos3 != 150 && tetrisPiecePos3 != 160 && tetrisPiecePos3 != 170 &&
                        tetrisPiecePos3 != 180 && tetrisPiecePos3 != 190 && tetrisPiecePos3 != 20 && tetrisPiecePos3 != 40)
                    {
                        if (GameLabel[tetrisPiecePos3 - 1].BackColor != blockEmptyColor[1] && GameLabel[tetrisPiecePos4 - 1].BackColor != blockEmptyColor[1] && pieceType == 21)
                        {
                            tetrisPiecePos--;
                            tetrisPiecePos2--;
                            tetrisPiecePos3--;
                            tetrisPiecePos4--;
                            GameLabel[tetrisPiecePos + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos2 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos3 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos4 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                            return true;
                        }
                    }
                    if (tetrisPiecePos4 != 0 && tetrisPiecePos4 != 10 && tetrisPiecePos4 != 30 && tetrisPiecePos4 != 50 &&
                        tetrisPiecePos4 != 60 && tetrisPiecePos4 != 70 && tetrisPiecePos4 != 80 && tetrisPiecePos4 != 90 &&
                        tetrisPiecePos4 != 100 && tetrisPiecePos4 != 110 && tetrisPiecePos4 != 120 && tetrisPiecePos4 != 130 &&
                        tetrisPiecePos4 != 140 && tetrisPiecePos4 != 150 && tetrisPiecePos4 != 160 && tetrisPiecePos4 != 170 &&
                        tetrisPiecePos4 != 180 && tetrisPiecePos4 != 190 && tetrisPiecePos4 != 20 && tetrisPiecePos4 != 40)
                    {
                        if (GameLabel[tetrisPiecePos4 - 1].BackColor != blockEmptyColor[1] && pieceType == 3)
                        {
                            tetrisPiecePos--;
                            tetrisPiecePos2--;
                            tetrisPiecePos3--;
                            tetrisPiecePos4--;
                            GameLabel[tetrisPiecePos + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos2 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos3 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos4 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                            return true;
                        }
                        if (GameLabel[tetrisPiecePos4 - 1].BackColor != blockEmptyColor[1] && GameLabel[tetrisPiecePos - 1].BackColor != blockEmptyColor[1] && pieceType == 42)
                        {
                            tetrisPiecePos--;
                            tetrisPiecePos2--;
                            tetrisPiecePos3--;
                            tetrisPiecePos4--;
                            GameLabel[tetrisPiecePos + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos2 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos3 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos4 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                            return true;
                        }
                        if (GameLabel[tetrisPiecePos4 - 1].BackColor != blockEmptyColor[1] && GameLabel[tetrisPiecePos3 - 1].BackColor != blockEmptyColor[1] &&
                            GameLabel[tetrisPiecePos2 - 1].BackColor != blockEmptyColor[1] && pieceType == 43)
                        {
                            tetrisPiecePos--;
                            tetrisPiecePos2--;
                            tetrisPiecePos3--;
                            tetrisPiecePos4--;
                            GameLabel[tetrisPiecePos + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos2 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos3 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos4 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                            return true;
                        }
                        if (GameLabel[tetrisPiecePos4 - 1].BackColor != blockEmptyColor[1] && GameLabel[tetrisPiecePos - 1].BackColor != blockEmptyColor[1] &&
                           pieceType == 5)
                        {
                            tetrisPiecePos--;
                            tetrisPiecePos2--;
                            tetrisPiecePos3--;
                            tetrisPiecePos4--;
                            GameLabel[tetrisPiecePos + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos2 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos3 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos4 + 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                            return true;
                        }
                    }
                }
                return false;
            }
            if(keyData == Keys.Right && tetrisPiecePos > 9)
            {
                if (tetrisPiecePos4 < 199 && blocksFilled[tetrisPiecePos4] == false)
                {
                    if (tetrisPiecePos4 != 9   && tetrisPiecePos4 != 19  && tetrisPiecePos4 != 29  && tetrisPiecePos4 != 39  &&
                        tetrisPiecePos4 != 49  && tetrisPiecePos4 != 59  && tetrisPiecePos4 != 69  && tetrisPiecePos4 != 79  &&
                        tetrisPiecePos4 != 89  && tetrisPiecePos4 != 99  && tetrisPiecePos4 != 109 && tetrisPiecePos4 != 119 &&
                        tetrisPiecePos4 != 129 && tetrisPiecePos4 != 139 && tetrisPiecePos4 != 149 && tetrisPiecePos4 != 159 &&
                        tetrisPiecePos4 != 169 && tetrisPiecePos4 != 179 && tetrisPiecePos4 != 189 && tetrisPiecePos4 != 199)
                    {
                        if(GameLabel[tetrisPiecePos4 + 1].BackColor != blockEmptyColor[1] && pieceType == 0)
                        {
                            tetrisPiecePos++;
                            tetrisPiecePos2++;
                            tetrisPiecePos3++;
                            tetrisPiecePos4++;
                            GameLabel[tetrisPiecePos - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                            return true;
                        }
                        if (GameLabel[tetrisPiecePos2 + 1].BackColor != blockEmptyColor[1] && GameLabel[tetrisPiecePos4 + 1].BackColor != blockEmptyColor[1] && pieceType == 1)
                        {
                            tetrisPiecePos++;
                            tetrisPiecePos2++;
                            tetrisPiecePos3++;
                            tetrisPiecePos4++;
                            GameLabel[tetrisPiecePos - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos3 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                            return true;
                        }
                        if (GameLabel[tetrisPiecePos + 1].BackColor != blockEmptyColor[1] && GameLabel[tetrisPiecePos2 + 1].BackColor != blockEmptyColor[1] &&
                            GameLabel[tetrisPiecePos3 + 1].BackColor != blockEmptyColor[1] && GameLabel[tetrisPiecePos4 + 1].BackColor != blockEmptyColor[1] && pieceType == 11)
                        {
                            tetrisPiecePos++;
                            tetrisPiecePos2++;
                            tetrisPiecePos3++;
                            tetrisPiecePos4++;
                            GameLabel[tetrisPiecePos - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos2 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos3 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos4 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                            return true;
                        }
                        if (GameLabel[tetrisPiecePos4 + 1].BackColor != blockEmptyColor[1] && GameLabel[tetrisPiecePos2 + 1].BackColor != blockEmptyColor[1] && pieceType == 2)
                        {
                            tetrisPiecePos++;
                            tetrisPiecePos2++;
                            tetrisPiecePos3++;
                            tetrisPiecePos4++;
                            GameLabel[tetrisPiecePos - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos2 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos3 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos4 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                            return true;
                        }
                        if (GameLabel[tetrisPiecePos4 + 1].BackColor != blockEmptyColor[1] && pieceType == 31)
                        {
                            tetrisPiecePos++;
                            tetrisPiecePos2++;
                            tetrisPiecePos3++;
                            tetrisPiecePos4++;
                            GameLabel[tetrisPiecePos - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos2 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos3 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos4 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                            return true;
                        }
                        if (GameLabel[tetrisPiecePos4 + 1].BackColor != blockEmptyColor[1] && pieceType == 4)
                        {
                            tetrisPiecePos++;
                            tetrisPiecePos2++;
                            tetrisPiecePos3++;
                            tetrisPiecePos4++;
                            GameLabel[tetrisPiecePos - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos2 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos3 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos4 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                            return true;
                        }
                        if (GameLabel[tetrisPiecePos4 + 1].BackColor != blockEmptyColor[1] && pieceType == 41)
                        {
                            tetrisPiecePos++;
                            tetrisPiecePos2++;
                            tetrisPiecePos3++;
                            tetrisPiecePos4++;
                            GameLabel[tetrisPiecePos - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos2 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos3 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos4 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                            return true;
                        }
                    }
                    if (tetrisPiecePos != 9   && tetrisPiecePos != 19  && tetrisPiecePos != 29  && tetrisPiecePos != 39  &&
                        tetrisPiecePos != 49  && tetrisPiecePos != 59  && tetrisPiecePos != 69  && tetrisPiecePos != 79  &&
                        tetrisPiecePos != 89  && tetrisPiecePos != 99  && tetrisPiecePos != 109 && tetrisPiecePos != 119 &&
                        tetrisPiecePos != 129 && tetrisPiecePos != 139 && tetrisPiecePos != 149 && tetrisPiecePos != 159 &&
                        tetrisPiecePos != 169 && tetrisPiecePos != 179 && tetrisPiecePos != 189 && tetrisPiecePos != 199)
                    {
                        if (GameLabel[tetrisPiecePos + 1].BackColor != blockEmptyColor[1] && GameLabel[tetrisPiecePos2 + 1].BackColor != blockEmptyColor[1] && pieceType == 21)
                        {
                            tetrisPiecePos++;
                            tetrisPiecePos2++;
                            tetrisPiecePos3++;
                            tetrisPiecePos4++;
                            GameLabel[tetrisPiecePos - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos2 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos3 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos4 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                            return true;
                        }
                        if (GameLabel[tetrisPiecePos + 1].BackColor != blockEmptyColor[1] && pieceType == 3)
                        {
                            tetrisPiecePos++;
                            tetrisPiecePos2++;
                            tetrisPiecePos3++;
                            tetrisPiecePos4++;
                            GameLabel[tetrisPiecePos - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos2 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos3 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos4 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                            return true;
                        }
                        if (GameLabel[tetrisPiecePos + 1].BackColor != blockEmptyColor[1] && GameLabel[tetrisPiecePos2 + 1].BackColor != blockEmptyColor[1] && pieceType == 42)
                        {
                            tetrisPiecePos++;
                            tetrisPiecePos2++;
                            tetrisPiecePos3++;
                            tetrisPiecePos4++;
                            GameLabel[tetrisPiecePos - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos2 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos3 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos4 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                            return true;
                        }
                        if (GameLabel[tetrisPiecePos + 1].BackColor != blockEmptyColor[1] && GameLabel[tetrisPiecePos3 + 1].BackColor != blockEmptyColor[1] &&
                            GameLabel[tetrisPiecePos4 - 1].BackColor != blockEmptyColor[1] && pieceType == 43)
                        {
                            tetrisPiecePos++;
                            tetrisPiecePos2++;
                            tetrisPiecePos3++;
                            tetrisPiecePos4++;
                            GameLabel[tetrisPiecePos - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos2 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos3 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos4 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                            return true;
                        }
                        if(GameLabel[tetrisPiecePos + 1].BackColor != blockEmptyColor[1] && GameLabel[tetrisPiecePos2 + 1].BackColor != blockEmptyColor[1] &&
                           pieceType == 5)
                        {
                            tetrisPiecePos++;
                            tetrisPiecePos2++;
                            tetrisPiecePos3++;
                            tetrisPiecePos4++;
                            GameLabel[tetrisPiecePos - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos2 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos3 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos4 - 1].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                            return true;
                        }
                    }
                }
            }
            if (keyData == Keys.Space && tetrisPiecePos > 9)
            {
                if (tetrisPiecePos < 180 && blocksFilled[tetrisPiecePos] == false)
                {
                    if (pieceType == 0 && tetrisPiecePos4 > 30)
                    {
                        GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[0];
                        GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[0];
                        GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[0];
                        GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[0];
                        tetrisPiecePos = tetrisPiecePos4 - 30;
                        tetrisPiecePos2 = tetrisPiecePos4 - 20;
                        tetrisPiecePos3 = tetrisPiecePos4 - 10;
                        blocksFilled[tetrisPiecePos4 - 3] = false;
                        blocksFilled[tetrisPiecePos4 - 2] = false;
                        blocksFilled[tetrisPiecePos4 - 1] = false;
                        GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                        pieceType = 11;
                        return true;
                    }
                    if (pieceType == 11)
                    {
                        if (tetrisPiecePos4 % 10 > 2 && GameLabel[tetrisPiecePos4 - 1].BackColor != blockEmptyColor[1] && GameLabel[tetrisPiecePos4 - 2].BackColor != blockEmptyColor[1] && GameLabel[tetrisPiecePos4 - 3].BackColor != blockEmptyColor[1])
                        {
                            tetrisPiecePos = tetrisPiecePos4 - 3;
                            tetrisPiecePos2 = tetrisPiecePos4 - 2;
                            tetrisPiecePos3 = tetrisPiecePos4 - 1;
                            GameLabel[tetrisPiecePos4 - 30].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos4 - 20].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos4 - 10].BackColor = blockEmptyColor[0];
                            blocksFilled[tetrisPiecePos4 - 30] = false;
                            blocksFilled[tetrisPiecePos4 - 20] = false;
                            blocksFilled[tetrisPiecePos4 - 10] = false;
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                            pieceType = 0;
                            return true;
                        }
                    } 
                    if (pieceType == 2 && GameLabel[tetrisPiecePos + 20].BackColor == blockEmptyColor[0])
                    {
                        GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[0];
                        GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[0];
                        GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[0];
                        GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[0];
                        blocksFilled[tetrisPiecePos] = false;
                        blocksFilled[tetrisPiecePos2] = false;
                        blocksFilled[tetrisPiecePos3] = false;
                        blocksFilled[tetrisPiecePos4] = false;
                        tetrisPiecePos = tetrisPiecePos2;
                        tetrisPiecePos2 = tetrisPiecePos3;
                        tetrisPiecePos3 = tetrisPiecePos2 - 1;
                        tetrisPiecePos4 = tetrisPiecePos3 + 10;
                        GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                        pieceType = 21;
                        return true;
                    }
                    if (pieceType == 21 && GameLabel[tetrisPiecePos2 + 1].BackColor != blockEmptyColor[1])
                    {
                        if (tetrisPiecePos != 9   && tetrisPiecePos != 19  && tetrisPiecePos != 29  && tetrisPiecePos != 39 &&
                            tetrisPiecePos != 49  && tetrisPiecePos != 59  && tetrisPiecePos != 69  && tetrisPiecePos != 79 &&
                            tetrisPiecePos != 89  && tetrisPiecePos != 99  && tetrisPiecePos != 109 && tetrisPiecePos != 119 &&
                            tetrisPiecePos != 129 && tetrisPiecePos != 139 && tetrisPiecePos != 149 && tetrisPiecePos != 159 &&
                            tetrisPiecePos != 169 && tetrisPiecePos != 179 && tetrisPiecePos != 189 && tetrisPiecePos != 199)
                        {
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[0];
                            blocksFilled[tetrisPiecePos] = false;
                            blocksFilled[tetrisPiecePos2] = false;
                            blocksFilled[tetrisPiecePos3] = false;
                            blocksFilled[tetrisPiecePos4] = false;
                            tetrisPiecePos--;
                            tetrisPiecePos3++;
                            tetrisPiecePos2 = tetrisPiecePos2 - 10;
                            tetrisPiecePos4 = tetrisPiecePos3 + 1;
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                            pieceType = 2;
                            return true;
                        }
                    }
                    if (pieceType == 3 && GameLabel[tetrisPiecePos + 20].BackColor == blockEmptyColor[0])
                    {
                        GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[0];
                        GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[0];
                        GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[0];
                        GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[0];
                        blocksFilled[tetrisPiecePos] = false;
                        blocksFilled[tetrisPiecePos2] = false;
                        blocksFilled[tetrisPiecePos3] = false;
                        blocksFilled[tetrisPiecePos4] = false;
                        tetrisPiecePos = tetrisPiecePos2;
                        tetrisPiecePos2 = tetrisPiecePos3;
                        tetrisPiecePos3 = tetrisPiecePos2 + 1;
                        tetrisPiecePos4 = tetrisPiecePos3 + 10;
                        GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                        pieceType = 31;
                        return true;
                    }
                    if (pieceType == 31 && GameLabel[tetrisPiecePos2 - 1].BackColor != blockEmptyColor[1])
                    {
                        if (tetrisPiecePos != 0 && tetrisPiecePos != 10 && tetrisPiecePos != 30 && tetrisPiecePos != 50 &&
                            tetrisPiecePos != 60 && tetrisPiecePos != 70 && tetrisPiecePos != 80 && tetrisPiecePos != 90 &&
                            tetrisPiecePos != 100 && tetrisPiecePos != 110 && tetrisPiecePos != 120 && tetrisPiecePos != 130 &&
                            tetrisPiecePos != 140 && tetrisPiecePos != 150 && tetrisPiecePos != 160 && tetrisPiecePos != 170 &&
                            tetrisPiecePos != 180 && tetrisPiecePos != 190 && tetrisPiecePos != 20 && tetrisPiecePos != 40)
                        {
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[0];
                            blocksFilled[tetrisPiecePos] = false;
                            blocksFilled[tetrisPiecePos2] = false;
                            blocksFilled[tetrisPiecePos3] = false;
                            blocksFilled[tetrisPiecePos4] = false;
                            tetrisPiecePos++;
                            tetrisPiecePos2 -= 10;
                            tetrisPiecePos3--;
                            tetrisPiecePos4 = tetrisPiecePos3 - 1;
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                            pieceType = 3;
                            return true;
                        }
                    }
                    if (pieceType == 4 && tetrisPiecePos > 20)
                    {
                        GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[0];
                        GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[0];
                        GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[0];
                        GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[0];
                        blocksFilled[tetrisPiecePos] = false;
                        blocksFilled[tetrisPiecePos2] = false;
                        blocksFilled[tetrisPiecePos3] = false;
                        blocksFilled[tetrisPiecePos4] = false;
                        tetrisPiecePos = tetrisPiecePos2;
                        tetrisPiecePos2 = tetrisPiecePos3;
                        tetrisPiecePos3 = tetrisPiecePos2 - 10;
                        tetrisPiecePos4 = tetrisPiecePos2 - 20;
                        GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                        pieceType = 41;
                        return true;
                    }
                    if (pieceType == 41)
                    {
                        if (tetrisPiecePos != 0 && tetrisPiecePos != 10 && tetrisPiecePos != 30 && tetrisPiecePos != 50 &&
                            tetrisPiecePos != 60 && tetrisPiecePos != 70 && tetrisPiecePos != 80 && tetrisPiecePos != 90 &&
                            tetrisPiecePos != 100 && tetrisPiecePos != 110 && tetrisPiecePos != 120 && tetrisPiecePos != 130 &&
                            tetrisPiecePos != 140 && tetrisPiecePos != 150 && tetrisPiecePos != 160 && tetrisPiecePos != 170 &&
                            tetrisPiecePos != 180 && tetrisPiecePos != 190 && tetrisPiecePos != 20 && tetrisPiecePos != 40)
                        {
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[0];
                            blocksFilled[tetrisPiecePos] = false;
                            blocksFilled[tetrisPiecePos2] = false;
                            blocksFilled[tetrisPiecePos3] = false;
                            blocksFilled[tetrisPiecePos4] = false;
                            tetrisPiecePos = tetrisPiecePos2;
                            tetrisPiecePos2 = tetrisPiecePos3;
                            tetrisPiecePos3 = tetrisPiecePos2 - 1;
                            tetrisPiecePos4 = tetrisPiecePos2 - 2;
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                            pieceType = 42;
                            return true;
                        }
                    }
                    if (pieceType == 42 && GameLabel[tetrisPiecePos3 + 10].BackColor != blockEmptyColor[1] && GameLabel[tetrisPiecePos3 + 20].BackColor != blockEmptyColor[1])
                    {
                        GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[0];
                        GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[0];
                        GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[0];
                        GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[0];
                        blocksFilled[tetrisPiecePos] = false;
                        blocksFilled[tetrisPiecePos2] = false;
                        blocksFilled[tetrisPiecePos3] = false;
                        blocksFilled[tetrisPiecePos4] = false;
                        tetrisPiecePos = tetrisPiecePos2;
                        tetrisPiecePos2 = tetrisPiecePos3;
                        tetrisPiecePos3 = tetrisPiecePos2 + 10;
                        tetrisPiecePos4 = tetrisPiecePos2 + 20;
                        GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                        GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                        pieceType = 43;
                        return true;
                    }
                    if(pieceType == 43 && GameLabel[tetrisPiecePos3 + 1].BackColor == blockEmptyColor[0] && GameLabel[tetrisPiecePos3 + 2].BackColor == blockEmptyColor[0])
                    {
                        if (tetrisPiecePos != 9 && tetrisPiecePos != 19 && tetrisPiecePos != 29 && tetrisPiecePos != 39 &&
                            tetrisPiecePos != 49 && tetrisPiecePos != 59 && tetrisPiecePos != 69 && tetrisPiecePos != 79 &&
                            tetrisPiecePos != 89 && tetrisPiecePos != 99 && tetrisPiecePos != 109 && tetrisPiecePos != 119 &&
                            tetrisPiecePos != 129 && tetrisPiecePos != 139 && tetrisPiecePos != 149 && tetrisPiecePos != 159 &&
                            tetrisPiecePos != 169 && tetrisPiecePos != 179 && tetrisPiecePos != 189 && tetrisPiecePos != 199)
                        {
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[0];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[0];
                            blocksFilled[tetrisPiecePos] = false;
                            blocksFilled[tetrisPiecePos2] = false;
                            blocksFilled[tetrisPiecePos3] = false;
                            blocksFilled[tetrisPiecePos4] = false;
                            tetrisPiecePos = tetrisPiecePos2;
                            tetrisPiecePos2 = tetrisPiecePos3;
                            tetrisPiecePos3 = tetrisPiecePos2 + 1;
                            tetrisPiecePos4 = tetrisPiecePos2 + 2;
                            GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                            GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                            pieceType = 4;
                            return true;
                        }
                    }
                }
            }
            if (keyData == Keys.Down)
            {
                if(tetrisPiecePos4 > 20 && tetrisPiecePos4 < 185 && GameLabel[tetrisPiecePos4 + 10].BackColor == blockEmptyColor[0] &&
                   GameLabel[tetrisPiecePos3 + 10].BackColor == blockEmptyColor[0] && GameLabel[tetrisPiecePos2 + 10].BackColor == blockEmptyColor[0] &&
                   GameLabel[tetrisPiecePos + 10].BackColor == blockEmptyColor[0] && pieceType == 0)
                {
                    tetrisPiecePos += 10;
                    tetrisPiecePos2 += 10;
                    tetrisPiecePos3 += 10;
                    tetrisPiecePos4 += 10;
                    GameLabel[tetrisPiecePos - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos2 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos3 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos4 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos].BackColor =  blockEmptyColor[1];
                    GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                }
                if(tetrisPiecePos4 > 20 && tetrisPiecePos4 < 190 && GameLabel[tetrisPiecePos4 + 10].BackColor == blockEmptyColor[0] &&
                   GameLabel[tetrisPiecePos3 + 10].BackColor == blockEmptyColor[0] && pieceType == 1)
                {
                    tetrisPiecePos += 10;
                    tetrisPiecePos2 += 10;
                    tetrisPiecePos3 += 10;
                    tetrisPiecePos4 += 10;
                    GameLabel[tetrisPiecePos - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos2 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos3 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos4 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos].BackColor =  blockEmptyColor[1];
                    GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                }
                if(tetrisPiecePos4 > 20 && tetrisPiecePos4 < 190 && GameLabel[tetrisPiecePos4 + 10].BackColor == blockEmptyColor[0] &&
                   pieceType == 11)
                {
                    tetrisPiecePos += 10;
                    tetrisPiecePos2 += 10;
                    tetrisPiecePos3 += 10;
                    tetrisPiecePos4 += 10;
                    GameLabel[tetrisPiecePos - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos2 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos3 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos4 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos].BackColor =  blockEmptyColor[1];
                    GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                }
                if(tetrisPiecePos4 > 20 && tetrisPiecePos4 < 190 && GameLabel[tetrisPiecePos4 + 10].BackColor == blockEmptyColor[0] &&
                   GameLabel[tetrisPiecePos3 + 10].BackColor == blockEmptyColor[0] && GameLabel[tetrisPiecePos + 10].BackColor == blockEmptyColor[0] && pieceType == 2)
                {
                    tetrisPiecePos += 10;
                    tetrisPiecePos2 += 10;
                    tetrisPiecePos3 += 10;
                    tetrisPiecePos4 += 10;
                    GameLabel[tetrisPiecePos - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos2 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos3 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos4 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                }
                if(tetrisPiecePos4 > 20 && tetrisPiecePos4 < 190 && GameLabel[tetrisPiecePos4 + 10].BackColor == blockEmptyColor[0] &&
                   GameLabel[tetrisPiecePos2 + 10].BackColor == blockEmptyColor[0] && pieceType == 21)
                {
                    tetrisPiecePos += 10;
                    tetrisPiecePos2 += 10;
                    tetrisPiecePos3 += 10;
                    tetrisPiecePos4 += 10;
                    GameLabel[tetrisPiecePos - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos2 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos3 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos4 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                }
                if (tetrisPiecePos4 > 20 && tetrisPiecePos4 < 190 && GameLabel[tetrisPiecePos4 + 10].BackColor == blockEmptyColor[0] &&
                   GameLabel[tetrisPiecePos3 + 10].BackColor == blockEmptyColor[0] && GameLabel[tetrisPiecePos + 10].BackColor == blockEmptyColor[0] && pieceType == 3)
                {
                    tetrisPiecePos += 10;
                    tetrisPiecePos2 += 10;
                    tetrisPiecePos3 += 10;
                    tetrisPiecePos4 += 10;
                    GameLabel[tetrisPiecePos - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos2 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos3 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos4 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                }
                if (tetrisPiecePos4 > 20 && tetrisPiecePos4 < 190 && GameLabel[tetrisPiecePos4 + 10].BackColor == blockEmptyColor[0] &&
                   GameLabel[tetrisPiecePos2 + 10].BackColor == blockEmptyColor[0] && pieceType == 31)
                {
                    tetrisPiecePos += 10;
                    tetrisPiecePos2 += 10;
                    tetrisPiecePos3 += 10;
                    tetrisPiecePos4 += 10;
                    GameLabel[tetrisPiecePos - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos2 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos3 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos4 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                }
                if (tetrisPiecePos4 > 20 && tetrisPiecePos4 < 190 && GameLabel[tetrisPiecePos4 + 10].BackColor == blockEmptyColor[0] &&
                   GameLabel[tetrisPiecePos3 + 10].BackColor == blockEmptyColor[0] && GameLabel[tetrisPiecePos2 + 10].BackColor == blockEmptyColor[0] && pieceType == 4)
                {
                    tetrisPiecePos += 10;
                    tetrisPiecePos2 += 10;
                    tetrisPiecePos3 += 10;
                    tetrisPiecePos4 += 10;
                    GameLabel[tetrisPiecePos - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos2 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos3 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos4 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                }
                if (tetrisPiecePos4 > 20 && tetrisPiecePos4 < 170 && GameLabel[tetrisPiecePos + 10].BackColor == blockEmptyColor[0] &&
                    GameLabel[tetrisPiecePos2 + 10].BackColor == blockEmptyColor[0] && pieceType == 41)
                {
                    tetrisPiecePos += 10;
                    tetrisPiecePos2 += 10;
                    tetrisPiecePos3 += 10;
                    tetrisPiecePos4 += 10;
                    GameLabel[tetrisPiecePos - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos2 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos3 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos4 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                }
                if (tetrisPiecePos4 > 20 && tetrisPiecePos4 < 170 && GameLabel[tetrisPiecePos + 10].BackColor == blockEmptyColor[0] &&
                    GameLabel[tetrisPiecePos3 + 10].BackColor == blockEmptyColor[0] && GameLabel[tetrisPiecePos4 + 10].BackColor == blockEmptyColor[0] && pieceType == 42)
                {
                    tetrisPiecePos += 10;
                    tetrisPiecePos2 += 10;
                    tetrisPiecePos3 += 10;
                    tetrisPiecePos4 += 10;
                    GameLabel[tetrisPiecePos - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos2 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos3 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos4 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                }
                if (tetrisPiecePos4 > 20 && tetrisPiecePos4 < 170 && GameLabel[tetrisPiecePos + 10].BackColor == blockEmptyColor[0] &&
                    GameLabel[tetrisPiecePos4 + 10].BackColor == blockEmptyColor[0] && pieceType == 43)
                {
                    tetrisPiecePos += 10;
                    tetrisPiecePos2 += 10;
                    tetrisPiecePos3 += 10;
                    tetrisPiecePos4 += 10;
                    GameLabel[tetrisPiecePos - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos2 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos3 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos4 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                }
                if (tetrisPiecePos4 > 20 && tetrisPiecePos4 < 170 && GameLabel[tetrisPiecePos2 + 10].BackColor == blockEmptyColor[0] &&
                    GameLabel[tetrisPiecePos3 + 10].BackColor == blockEmptyColor[0] && GameLabel[tetrisPiecePos4 + 10].BackColor == blockEmptyColor[0] && pieceType == 5)
                {
                    tetrisPiecePos += 10;
                    tetrisPiecePos2 += 10;
                    tetrisPiecePos3 += 10;
                    tetrisPiecePos4 += 10;
                    GameLabel[tetrisPiecePos - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos2 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos3 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos4 - 10].BackColor = blockEmptyColor[0];
                    GameLabel[tetrisPiecePos].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos2].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos3].BackColor = blockEmptyColor[1];
                    GameLabel[tetrisPiecePos4].BackColor = blockEmptyColor[1];
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
