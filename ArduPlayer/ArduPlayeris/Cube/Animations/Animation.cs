using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Serialization;
using MetroFramework;
using MetroFramework.Controls;

namespace ArduPlayeris.Cube.Animations
{
    [Serializable]
    public class Animation
    {
        private List<Frame> frames = new List<Frame>();

        public string AnimationName = "Unnamed Animation";

        public int CycleAmmount = 1;
   
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
                    control.uzdegti = frame.uzdegti;
                    controls.Add(control);
                }
                return controls;
            }  
        }

        public void NewFrame()
        {
            int frameCount = frames.Count+1;
            Frame frame = new Frame(frameCount);
            frames.Add(frame);
        }

        internal void SaveFrame(Frame currentFrame)
        {
            foreach (Frame frame in frames)
            {
                if (frame.Number == currentFrame.Number)
                    frame.uzdegti = currentFrame.uzdegti;
            }
        }

        public void Save(List<Frame> frames)
        {
            this.frames = frames;
        }

        public override string ToString()
        {
            return AnimationName;
        }
    }
}