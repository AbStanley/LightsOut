﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightsOut
{
    public partial class MainForm : Form
    {
        private const int GridOffset = 25; // Distance from upper-left side of window         
        private const int GridLength = 200; // Size in pixels of grid         
                                            // private const int NumCells = 3; // Number of cells in grid         
        private LightsOutGame LightsOutGame;
        private int CellLength { get { return GridLength / LightsOutGame.GridSize; } }
        private int NumCells { get {return LightsOutGame.GridSize; } }
        
        public MainForm()
        {
            InitializeComponent();

            LightsOutGame = new LightsOutGame();

            //rand = new Random(); // Initializes random number generator        
            //grid = new bool[NumCells, NumCells];

            //// Turn entire grid on             
            //for (int r = 0; r < NumCells; r++)
            //    for (int c = 0; c < NumCells; c++)
            //        grid[r, c] = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int r = 0; r < LightsOutGame.GridSize; r++)
            {
                for (int c = 0; c < LightsOutGame.GridSize; c++)
                {
                    // Get proper pen and brush for on/off       
                    // grid section                     
                    Brush brush;
                    Pen pen;

                    if (LightsOutGame.GetGridValue(r, c))
                    {
                        pen = Pens.Black;
                        brush = Brushes.White; // On                     
                    }
                    else
                    {
                        pen = Pens.White;
                        brush = Brushes.Black; // Off                     
                    }
                    
                    // Determine (x,y) coord of row and col to draw rectangle                                         
                    int x = c * CellLength + GridOffset;
                    int y = r * CellLength + GridOffset;
                    // Draw outline and inner rectangle                     
                    g.DrawRectangle(pen, x, y, CellLength, CellLength);
                    g.FillRectangle(brush, x + 1, y + 1, CellLength - 1, CellLength - 1);
                }
            }

        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X < GridOffset || e.X > CellLength * NumCells + GridOffset || e.Y < GridOffset || e.Y > CellLength * NumCells + GridOffset) return;

            // Find row, col of mouse press   
            int r = (e.Y - GridOffset) / CellLength;
            int c = (e.X - GridOffset) / CellLength;
            // Invert selected box and all surrounding boxes   
            LightsOutGame.Move(r, c);
            // Redraw grid   
            this.Invalidate();
            // Check to see if puzzle has been solved   
            if (LightsOutGame.IsGameOver())
            {
                // Display winner dialog box      
                MessageBox.Show(this, "Congratulations!  You've won!", "Lights Out!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            // Fill grid with either white or black
            LightsOutGame.NewGame();
            // Redraw grid
            this.Invalidate();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newGameButton_Click(sender, e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutBox = new AboutForm();
            aboutBox.ShowDialog(this);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}