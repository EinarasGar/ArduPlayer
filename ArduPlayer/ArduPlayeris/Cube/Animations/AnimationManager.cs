using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Controls;


/*
 
    * Upon adding new frame - current frame doesnt change.
    *  
    
*/
namespace ArduPlayeris.Cube.Animations
{
    public class AnimationManager
    {
        private MainForm mainForm;
        private SCom serial;
        private Animation currentAnimation;
        private MetroPanel FramePanel = new MetroPanel();
        private MetroScrollBar scrollBar = new MetroScrollBar();
        private List<Frame> currentFrames = new List<Frame>();
        private Frame currentFrame = new Frame(0);

        private List<int> latestLit = new List<int>();

        public AnimationManager(MainForm mainForm)
        {
            this.mainForm = mainForm;
            this.serial = mainForm.serial;

            mainForm.LedsChanged += MainForm_LedsChanged;

            this.mainForm.metroButton6.Click += MetroButton6_Click;
            this.mainForm.metroButton2.Click += MetroButton2_Click;
            this.mainForm.metroButton3.Click += MetroToggle3_Click;
            currentAnimation = new Animation();
            scrollBar = createScrollBar();
            mainForm.metroTabPage1.Controls.Add(this.scrollBar);
            addFrame();
            loadAnimation();
          //  showFrames();
           // setCurrents();



        }

        private void setCurrents()
        {
            currentFrames = currentAnimation.Frames;
            currentFrame = currentFrames[0];
        }

        private void MainForm_LedsChanged(List<int> litLeds)
        {
            currentFrame.uzdegti = litLeds;
            latestLit = litLeds;
            saveFrame();
        }

        private void saveFrame()
        {
            foreach (Frame frame in currentFrames)
            {
                if (frame.Number == currentFrame.Number)
                    frame.uzdegti = currentFrame.uzdegti;
            }
        }

        private MetroPanel createPanel()
        {
            MetroPanel panel = new MetroPanel();
            panel.AutoScroll = true;
            panel.HorizontalScrollbar = true;
            panel.HorizontalScrollbarBarColor = true;
            panel.HorizontalScrollbarHighlightOnWheel = true;
            panel.HorizontalScrollbarSize = 0;
            panel.Location = new System.Drawing.Point(3, 235);
            panel.Size = new System.Drawing.Size(340, 36);
            panel.Style = MetroFramework.MetroColorStyle.Lime;
            panel.TabIndex = 5;
            panel.Theme = MetroFramework.MetroThemeStyle.Dark;
            panel.VerticalScrollbar = false;
            panel.VerticalScrollbarBarColor = true;
            panel.VerticalScrollbarHighlightOnWheel = false;
            panel.VerticalScrollbarSize = 10;
            return panel;

        }

        private MetroScrollBar createScrollBar()
        {
            MetroScrollBar metroScrollBar1 = new MetroScrollBar();
            metroScrollBar1.LargeChange = 10;
            metroScrollBar1.Location = new System.Drawing.Point(3, 219);
            metroScrollBar1.Maximum = 100;
            metroScrollBar1.Minimum = 1;
            metroScrollBar1.MouseWheelBarPartitions = 10;
            metroScrollBar1.Orientation = MetroFramework.Controls.MetroScrollOrientation.Horizontal;
            metroScrollBar1.ScrollbarSize = 10;
            metroScrollBar1.Size = new System.Drawing.Size(340, 10);
            metroScrollBar1.Style = MetroFramework.MetroColorStyle.Lime;
            metroScrollBar1.TabIndex = 12;
            metroScrollBar1.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroScrollBar1.UseSelectable = true;
            metroScrollBar1.Scroll += MetroScrollBar1_Scroll;
            return metroScrollBar1;
        }

        private void MetroScrollBar1_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
        {
            FramePanel.HorizontalScroll.Value = FramePanel.HorizontalScroll.Maximum;
            FramePanel.ScrollControlIntoView(currentFrames.ElementAt(scrollBar.Value - 1));
        }

        private void MetroButton2_Click(object sender, System.EventArgs e)
        {
            saveAnimation();
            addFrame();
            showFrames();
        }

        private void saveAnimation()
        {
            currentAnimation.Save(currentFrames);
        }

        private void addFrame()
        {
            currentAnimation.NewFrame();
        }

        private void updateScrolbar()
        {
            int frameNumber = currentFrames.Count;
            if (frameNumber > 15)
            {
                int c = 0;
                foreach (Frame frame in currentFrames)
                {
                    if (frame.Location.X > -5 && frame.Location.X < 330)
                    {
                        c++;
                    }
                }
                scrollBar.Maximum = frameNumber - c + 3;
                scrollBar.Value = scrollBar.Maximum;
            }
        }

        private void MetroToggle3_Click(object sender, System.EventArgs e)
        {
            loadAnimation();
        }

        private void loadAnimation()
        {
            FramePanel.Dispose();
            FramePanel = createPanel();
            mainForm.metroTabPage1.Controls.Add(this.FramePanel);

           // currentAnimation = new Animation();
          
            

            showFrames();
            FramePanel.HorizontalScroll.Value = FramePanel.HorizontalScroll.Maximum;
        }

        private void showFrames()
        {
            currentFrames = currentAnimation.Frames;
            foreach (Frame frame in currentFrames)
            {
                frame.Style = mainForm.Style;
                frame.Theme = mainForm.Theme;
                frame.Click += Frame_Click;
            }

            Frame frameToLoad = currentFrames.Last();
            frameToLoad.uzdegti = latestLit.ToArray().ToList();
            loadFrame(frameToLoad);
            FramePanel.Controls.Clear();
            FramePanel.Controls.AddRange(currentFrames.ToArray());
            FramePanel.Refresh();
            updateScrolbar();

        }

        private void Frame_Click(object sender, EventArgs e)
        {
            Frame frame = sender as Frame;
            loadFrame(frame);
        }

        private async void MetroButton6_Click(object sender, System.EventArgs e)
        {
            List<Frame> frames = currentFrames;
            FramePanel.ScrollControlIntoView(frames.ElementAt(0));
            scrollBar.Value = 1;
            FramePanel.PerformLayout();
            FramePanel.Refresh();
            for (var i = 0; i < frames.Count; i++)
            {
                Frame frame = frames[i];
                if (frame.Location.X < -5 || frame.Location.X > 330)
                {
                    FramePanel.HorizontalScroll.Value = FramePanel.HorizontalScroll.Maximum;
                    FramePanel.ScrollControlIntoView(frames.ElementAt(i - 1));
                    scrollBar.Value = i;
                    FramePanel.PerformLayout();
                    FramePanel.Refresh();
                }
         
                frame.UseCustomForeColor = true;
                frame.ForeColor = Color.Green;
                loadFrame(frame);
                await Task.Delay(250);
                frame.UseCustomForeColor = false;
                frame.ResetForeColor();
                FramePanel.PerformLayout();
                FramePanel.Refresh();
                
            }
        }

        private void loadFrame(Frame frame)
        {
            currentFrame = frame;
            mainForm.LitLeds = currentFrame.uzdegti;

            lightFrame(currentFrame.uzdegti);

            mainForm.RenderCube();
        }

        private void lightFrame(List<int> uzdegti)
        {
            byte header = 200;
            byte end = 201;
            List<byte> litLeds = new List<byte>();
            foreach (int i in uzdegti)
            {
                litLeds.Add((byte)i);
            }

            litLeds.Insert(0,header);
            litLeds.Add(end);
            byte[] bytesToSend = litLeds.ToArray();
            serial.Send(bytesToSend);
        }
    }
}