using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MetroFramework;
using MetroFramework.Controls;

namespace ArduPlayeris.Cube.Animations
{
    public class Animation
    {
        private List<Frame> frames = new List<Frame>();
        

        public List<Frame> Frames {
            get
            {
                List<Frame> controls = new List<Frame>();
                foreach (Frame frame in frames)
                {
                    Frame control = new Frame(frame.Number);
                    int frameCount = frame.Number;
                    if (frameCount == 1)
                        control.Location = new Point(6, 8);
                    else
                        control.Location = new Point(controls.ElementAt(frameCount - 2).Location.X + controls.ElementAt(frameCount - 2).Size.Width + 6, 8); ;

                    control.Style = MetroColorStyle.Lime;
                    control.Theme = MetroThemeStyle.Dark;
                    controls.Add(control);

                }
                return controls;
            }  
        }

        public void NewFrame(MetroColorStyle Style,MetroThemeStyle Theme)
        {
            int frameCount = frames.Count+1;
            Frame frame = new Frame(frameCount);
            frames.Add(frame);
        }
    }
}