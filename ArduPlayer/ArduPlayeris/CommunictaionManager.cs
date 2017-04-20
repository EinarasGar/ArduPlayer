using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArduPlayeris.Cube.Animations;
using MetroFramework.Controls;

namespace ArduPlayeris
{
    public class CommunictaionManager
    {
        private SCom serial;
        private SpotifyHelper spotifyHelper;
        private MainForm mainform;
        public CommunictaionManager(MainForm main)
        {
            this.serial = main.serial;
            this.mainform = main;
            this.spotifyHelper = main.spotfyH;
            serial.CommandRecieved += Serial_CommandRecieved;
            spotifyHelper.SpotifySongChanged += SpotifyHelper_SpotifySongChanged;

            serial.USBconnected += Serial_USBconnected;

            mainform.ColorOranToggle.CheckedChanged += new System.EventHandler(this.ColorOranToggleChanged);
            mainform.metroComboBox1.SelectedIndexChanged += MetroComboBox1_SelectedIndexChanged;
            mainform.metroToggle3.CheckedChanged += MetroToggle3_CheckedChanged;
        }

        private async void Serial_USBconnected()
        {
            if (currentTitle != null && currentArtist != null)
            {
                await Task.Delay(5000);
                serial.Send("artist" + currentArtist);
                await Task.Delay(100);
                serial.Send("title" + currentTitle);
            }
        }

        private void MetroToggle3_CheckedChanged(object sender, EventArgs e)
        {
            if(mainform.metroToggle3.Checked)
                serial.Send("debugon");
            else
                serial.Send("debugoff");
        }

        private void MetroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            serial.Send("colormode"+mainform.metroComboBox1.SelectedIndex);
        }
        
        private void ColorOranToggleChanged(object sender, EventArgs e)
        {
            if (mainform.ColorOranToggle.Checked)
                serial.Send("colorson");
            else
                serial.Send("colorsoff");
        }

        private string currentArtist = null;
        private string currentTitle = null;
        private async void SpotifyHelper_SpotifySongChanged(string artist, string title)
        {
            serial.Send("artist" + artist);
            await Task.Delay(100);
            serial.Send("title" + title);
            currentArtist = artist;
            currentTitle = title;
            mainform.NowPlayingLbl.Text = "Now playing: " + artist + " by " + title; //+ ", " + collection[2];
        }

        private void Serial_CommandRecieved(string text)
        {
            text = text.Substring(1);

            switch (text)
            {
                case "+":
                    SpotifyHelper.VolumeHelper.IncrementVolume("Spotify");
                    serial.Send("v" + Math.Round((double)SpotifyHelper.VolumeHelper.GetApplicationVolume("Spotify")).ToString());
                    return;
                case "-":
                    SpotifyHelper.VolumeHelper.DecrementVolume("Spotify");
                    serial.Send("v" + Math.Round((double)SpotifyHelper.VolumeHelper.GetApplicationVolume("Spotify")).ToString());
                    return;
                case "cl1":
                    //CheckSpotiySong(null, null);
                    SpotifyHelper.Win32.SendMessage(SpotifyHelper.GetSpotify(), SpotifyHelper.Win32.Constants.WM_APPCOMMAND, IntPtr.Zero, new IntPtr((long)SpotifyHelper.SpotifyAction.PlayPause));
                    return;
                case "cl2":
                    //CheckSpotiySong(null, null);
                    SpotifyHelper.Win32.SendMessage(SpotifyHelper.GetSpotify(), SpotifyHelper.Win32.Constants.WM_APPCOMMAND, IntPtr.Zero, new IntPtr((long)SpotifyHelper.SpotifyAction.NextTrack));
                    return;
                case "cl3":
                    //CheckSpotiySong(null, null);
                    // Sending two because cl3 happens only after cl2 and cl2 plays next track so we have to go back twice for prev track.
                    SpotifyHelper.Win32.SendMessage(SpotifyHelper.GetSpotify(), SpotifyHelper.Win32.Constants.WM_APPCOMMAND, IntPtr.Zero, new IntPtr((long)SpotifyHelper.SpotifyAction.PreviousTrack));
                    SpotifyHelper.Win32.SendMessage(SpotifyHelper.GetSpotify(), SpotifyHelper.Win32.Constants.WM_APPCOMMAND, IntPtr.Zero, new IntPtr((long)SpotifyHelper.SpotifyAction.PreviousTrack));
                    return;
                case "!":
                    // getInfo();
                    return;

            }

            if (text.Substring(0, 2) == "th")
            {
                text = text.Substring(2);
                string[] textSplitStrings = text.Split('/');


                MetroLabel TempLbl = mainform.TempLbl;
                MetroLabel HumidityLbl = mainform.HumidityLbl;

                if (TempLbl.InvokeRequired)
                    TempLbl.Invoke(new MethodInvoker(delegate { TempLbl.Text = "Temperature: " + textSplitStrings[0] + " °C"; }));
                else
                    TempLbl.Text = "Temperature: " + textSplitStrings[0] + " °C";

                if (HumidityLbl.InvokeRequired)
                    HumidityLbl.Invoke(new MethodInvoker(delegate { HumidityLbl.Text = "Humidity: " + textSplitStrings[1] + " %"; }));
                else
                    HumidityLbl.Text = "Humidity: " + textSplitStrings[1] + " %";

                return;
            }
            
        }
    }
}