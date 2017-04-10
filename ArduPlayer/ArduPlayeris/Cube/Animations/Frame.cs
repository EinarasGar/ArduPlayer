using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Controls;

namespace ArduPlayeris.Cube.Animations
{

    public class Frame : MetroLabel
    {
        private int frameCount;
        private int spacing = 6;
        private int textSize = 7;
        public List<int> uzdegti = new List<int>();
        

        public int Number {
            get { return frameCount;}
        }

        public Frame(int frameCount)
        {
            this.frameCount = frameCount;
            this.Cursor = Cursors.Hand;

            int digitsInFrame = (int)Math.Floor(Math.Log10(frameCount) + 1);
            int xSize = 9 + digitsInFrame * textSize;
            this.Size=new Size(xSize,19);
            this.Text = frameCount.ToString();
        }
    }
}