using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using MetroFramework.Controls;

/*
 
    * Adding new frame does not carry over lit leds
     
*/

namespace ArduPlayeris.Cube.Animations
{
    public class AnimationManager
    {
        private MainForm _mainForm;
        private SCom _serial;
        private Animation _currentAnimation;
        private MetroPanel _framePanel = new MetroPanel();
        private MetroScrollBar _scrollBar;
        private List<Frame> _currentFrames = new List<Frame>();
        private Frame _currentFrame = new Frame(0);

        private List<int> _latestLit = new List<int>();
        private bool _cycleAnimation;

        private List<Animation> _allAnimations = new List<Animation>();

        public AnimationManager(MainForm mainForm)
        {
            _mainForm = mainForm;
            _serial = mainForm.serial;

            mainForm.LedsChanged += mainForm_LedsChanged;

            _mainForm.CycleButton.Click += metroButton6_Click;
            _mainForm.AddFrameButton.Click += metroButton2_Click;
            _mainForm.NewAnimationButton.Click += metroToggle3_Click;
            _mainForm.SaveAnimationbutton.Click += SaveAnimationbutton_Click;
            _mainForm.LoadAnimationButton.Click += loadAnimationButton_Click;
            _mainForm.RenameButton.Click += RenameButton_Click;
            _mainForm.AnimationSelector.SelectedIndexChanged += animationSelector_SelectedIndexChanged;
            _mainForm.AnimationNameTextBox.TextChanged += AnimationNameTextBox_TextChanged;
            _mainForm.AnimationNameTextBox.KeyDown += AnimationNameTextBox_KeyDown;
            _currentAnimation = new Animation();
            _scrollBar = CreateScrollBar();
            mainForm.metroTabPage1.Controls.Add(_scrollBar);
            AddFrame();
            LoadAnimation();
        }

      

        private void loadAnimationButton_Click(object sender, EventArgs e)
        {
            LoadAnimationsFromFiles();
        }

        private void mainForm_LedsChanged(List<int> litLeds)
        {
            _currentFrame.uzdegti = litLeds;
            _latestLit = litLeds;
            SaveFrame();
        }

        private void SaveFrame()
        {
            foreach (Frame frame in _currentFrames)
            {
                if (frame.Number == _currentFrame.Number)
                    frame.uzdegti = _currentFrame.uzdegti;
            }
        }

        private MetroPanel CreatePanel()
        {
            MetroPanel panel = new MetroPanel();
            panel.AutoScroll = true;
            panel.HorizontalScrollbar = true;
            panel.HorizontalScrollbarBarColor = true;
            panel.HorizontalScrollbarHighlightOnWheel = true;
            panel.HorizontalScrollbarSize = 0;
            panel.Location = new Point(3, 235);
            panel.Size = new Size(340, 36);
            panel.Style = MetroFramework.MetroColorStyle.Lime;
            panel.TabIndex = 5;
            panel.Theme = MetroFramework.MetroThemeStyle.Dark;
            panel.VerticalScrollbar = false;
            panel.VerticalScrollbarBarColor = true;
            panel.VerticalScrollbarHighlightOnWheel = false;
            panel.VerticalScrollbarSize = 10;
            return panel;
        }

        private MetroScrollBar CreateScrollBar()
        {
            MetroScrollBar metroScrollBar1 = new MetroScrollBar();
            metroScrollBar1.LargeChange = 10;
            metroScrollBar1.Location = new Point(3, 219);
            metroScrollBar1.Maximum = 100;
            metroScrollBar1.Minimum = 1;
            metroScrollBar1.MouseWheelBarPartitions = 10;
            metroScrollBar1.Orientation = MetroScrollOrientation.Horizontal;
            metroScrollBar1.ScrollbarSize = 10;
            metroScrollBar1.Size = new Size(340, 10);
            metroScrollBar1.Style = MetroFramework.MetroColorStyle.Lime;
            metroScrollBar1.TabIndex = 12;
            metroScrollBar1.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroScrollBar1.UseSelectable = true;
            metroScrollBar1.Scroll += metroScrollBar1_Scroll;
            return metroScrollBar1;
        }

        private void metroScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            _framePanel.HorizontalScroll.Value = _framePanel.HorizontalScroll.Maximum;
            _framePanel.ScrollControlIntoView(_currentFrames.ElementAt(_scrollBar.Value - 1));
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            SaveFrames();
            AddFrame();
            ShowFrames();
        }

        private void SaveFrames()
        {
            _currentAnimation.Save(_currentFrames);
        }

        private void AddFrame()
        {
            _currentAnimation.NewFrame();
        }

        private void UpdateScrolbar()
        {
            int frameNumber = _currentFrames.Count;
            if (frameNumber > 15)
            {
                int c = 0;
                foreach (Frame frame in _currentFrames)
                {
                    if (frame.Location.X > -5 && frame.Location.X < 330)
                    {
                        c++;
                    }
                }
                _scrollBar.Maximum = frameNumber - c + 3;
                _scrollBar.Value = _scrollBar.Maximum;
            }
        }

        private void metroToggle3_Click(object sender, EventArgs e)
        {
            LoadAnimation();
        }

        private void LoadAnimation()
        {
            _framePanel.Dispose();
            _framePanel = CreatePanel();
            _mainForm.metroTabPage1.Controls.Add(_framePanel);
            
            _currentAnimation = new Animation();
            
            AddFrame();
            ShowFrames();
            _framePanel.HorizontalScroll.Value = _framePanel.HorizontalScroll.Maximum;
        }

        private void LoadAnimation(Animation animation)
        {
            _framePanel.Dispose();
            _framePanel = CreatePanel();
            _mainForm.metroTabPage1.Controls.Add(_framePanel);

            _currentAnimation = animation;
            ShowFrames();
            _framePanel.HorizontalScroll.Value = _framePanel.HorizontalScroll.Maximum;
        }

        private void ShowFrames()
        {
            _currentFrames = _currentAnimation.Frames;
            foreach (Frame frame in _currentFrames)
            {
                frame.Style = _mainForm.Style;
                frame.Theme = _mainForm.Theme;
                frame.Click += frame_Click;
            }

            Frame frameToLoad = _currentFrames.Last();
            //frameToLoad.uzdegti = latestLit.ToArray().ToList();
            LoadFrame(frameToLoad);
            _framePanel.Controls.Clear();
            _framePanel.Controls.AddRange(_currentFrames.ToArray());
            _framePanel.Refresh();
            UpdateScrolbar();
        }

        private void frame_Click(object sender, EventArgs e)
        {
            Frame frame = sender as Frame;
            LoadFrame(frame);
        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            if (!_cycleAnimation)
            {
                _cycleAnimation = true;
                _mainForm.CycleButton.Text = "Stop Cycling";
                LoopCycle();
            }
            else
            {
                _mainForm.CycleButton.Text = "Cycle";
                _cycleAnimation = false;
            }
        }

        private async void LoopCycle()
        {
            while (_cycleAnimation)
            {
                List<Frame> frames = _currentFrames;
                _framePanel.ScrollControlIntoView(frames.ElementAt(0));
                _scrollBar.Value = 1;
                _framePanel.PerformLayout();
                _framePanel.Refresh();
                for (var i = 0; i < frames.Count; i++)
                {
                    Frame frame = frames[i];
                    if (frame.Location.X < -5 || frame.Location.X > 330)
                    {
                        _framePanel.HorizontalScroll.Value = _framePanel.HorizontalScroll.Maximum;
                        _framePanel.ScrollControlIntoView(frames.ElementAt(i - 1));
                        _scrollBar.Value = i;
                        _framePanel.PerformLayout();
                        _framePanel.Refresh();
                    }

                    frame.UseCustomForeColor = true;
                    frame.ForeColor = Color.Green;
                    LoadFrame(frame);
                    await Task.Delay(250);
                    frame.UseCustomForeColor = false;
                    frame.ResetForeColor();
                    _framePanel.PerformLayout();
                    _framePanel.Refresh();
                    if (!_cycleAnimation) return;
                }
            }
        }

        private async void Cycle()
        {
            List<Frame> frames = _currentFrames;
            _framePanel.ScrollControlIntoView(frames.ElementAt(0));
            _scrollBar.Value = 1;
            _framePanel.PerformLayout();
            _framePanel.Refresh();
            for (var i = 0; i < frames.Count; i++)
            {
                Frame frame = frames[i];
                if (frame.Location.X < -5 || frame.Location.X > 330)
                {
                    _framePanel.HorizontalScroll.Value = _framePanel.HorizontalScroll.Maximum;
                    _framePanel.ScrollControlIntoView(frames.ElementAt(i - 1));
                    _scrollBar.Value = i;
                    _framePanel.PerformLayout();
                    _framePanel.Refresh();
                }

                frame.UseCustomForeColor = true;
                frame.ForeColor = Color.Green;
                LoadFrame(frame);
                await Task.Delay(250);
                frame.UseCustomForeColor = false;
                frame.ResetForeColor();
                _framePanel.PerformLayout();
                _framePanel.Refresh();
            }
        }

        private void LoadFrame(Frame frame)
        {
            _currentFrame = frame;
            _mainForm.LitLeds = _currentFrame.uzdegti;

            LightFrame(_currentFrame.uzdegti);

            _mainForm.RenderCube();
        }

        private void LightFrame(List<int> uzdegti)
        {
            byte header = 200;
            byte end = 201;
            List<byte> litLeds = new List<byte>();
            foreach (int i in uzdegti)
            {
                litLeds.Add((byte) i);
            }

            litLeds.Insert(0, header);
            litLeds.Add(end);
            byte[] bytesToSend = litLeds.ToArray();
            _serial.Send(bytesToSend);
        }

        private void SaveAnimationbutton_Click(object sender, EventArgs e)
        {
            SaveAnimationToFile();
        }

        private void SaveAnimationToFile()
        {
            if (!Directory.Exists("Animations"))
                Directory.CreateDirectory("Animations");
            SaveFrames();

            List<XElement> frameElements = new List<XElement>();
            foreach (Frame frame in _currentAnimation.Frames)
            {
                List<XElement> litLeds = new List<XElement>();
                foreach (int i in frame.uzdegti)
                {
                    XElement led = new XElement("led", i);
                    litLeds.Add(led);
                }

                XElement element = new XElement("Frame",
                    new XAttribute("Number", frame.Number),
                    new XElement("LitLeds", litLeds));
                frameElements.Add(element);
            }


            string result =
                new XElement("Animation",
                    new XElement("Name", _currentAnimation),
                    new XElement("Frames", frameElements)
                ).ToString();

            File.WriteAllText(@"Animations\" + _currentAnimation + ".animation", result);
        }

        private void LoadAnimationsFromFiles()
        {
            string[] files = Directory.GetFiles("Animations", "*.animation");
            _allAnimations.Clear();
            foreach (string file in files)
            {
                LoadAnimationFromFiles(file);
            }
            _mainForm.AnimationSelector.Items.Clear();
            _mainForm.AnimationSelector.Items.AddRange(_allAnimations.ToArray());
            //  mainForm.AnimationSelector.SelectedIndex =
            //      mainForm.AnimationSelector.FindStringExact(currentAnimation.AnimationName); */
        }

        private void RenameButton_Click(object sender, EventArgs e)
        {
            _mainForm.AnimationNameTextBox.Visible = true;
            _mainForm.RenameButton.Visible = false;
            _mainForm.NewAnimationButton.Visible = false;

            _mainForm.CycleButton.Enabled = false;
            _mainForm.NewAnimationButton.Enabled = false;
            _mainForm.SaveAnimationbutton.Enabled = false;
            _mainForm.LoadAnimationButton.Enabled = false;
            _mainForm.AddFrameButton.Enabled = false;

        }


        private void AnimationNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _mainForm.AnimationNameTextBox.Visible = false;
                _mainForm.RenameButton.Visible = true;
                _mainForm.NewAnimationButton.Visible = true;

                _mainForm.CycleButton.Enabled = true;
                _mainForm.NewAnimationButton.Enabled = true;
                _mainForm.SaveAnimationbutton.Enabled = true;
                _mainForm.LoadAnimationButton.Enabled = true;
                _mainForm.AddFrameButton.Enabled = true;
            }
        }

        private void AnimationNameTextBox_TextChanged(object sender, EventArgs e)
        {
            _currentAnimation.AnimationName = _mainForm.AnimationNameTextBox.Text;
        }

        private void animationSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAnimation(_allAnimations[_mainForm.AnimationSelector.SelectedIndex]);
        }

        private void LoadAnimationFromFiles(string path)
        {
            if (!Directory.Exists("Animations"))
                Directory.CreateDirectory("Animations");

            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            if (doc.DocumentElement != null)
            {
                XmlNode framesNode = doc.DocumentElement.SelectSingleNode("/Animation/Frames");
                XmlNode nameNode = doc.DocumentElement.SelectSingleNode("/Animation/Name");
                Animation loadedAnimation = new Animation();
                List<Frame> loadedFrames = new List<Frame>();
                if (framesNode != null)
                    foreach (XmlNode childNode in framesNode.ChildNodes)
                    {
                        if (childNode.Attributes != null)
                        {
                            int number = int.Parse(childNode.Attributes["Number"].InnerText);
                            Frame loadedFrame = new Frame(number);
                            List<int> uzdegti = new List<int>();
                            XmlNode litLeds = childNode.FirstChild;
                            foreach (XmlNode ledsChildNode in litLeds.ChildNodes)
                            {
                                uzdegti.Add(int.Parse(ledsChildNode.InnerText));
                            }
                            loadedFrame.uzdegti = uzdegti;
                            loadedFrames.Add(loadedFrame);
                        }
                        else
                            MessageBox.Show("Error loading " + path);
                    }
                else
                    MessageBox.Show("Error loading " + path);


                if (nameNode != null) loadedAnimation.AnimationName = nameNode.InnerText;
                else MessageBox.Show("Error loading " + path);

                loadedAnimation.Save(loadedFrames);

                _allAnimations.Add(loadedAnimation);
            }
            else
                MessageBox.Show("Error loading " + path);
            
            //loadAnimation(loadedAnimation);
        }
    }
}