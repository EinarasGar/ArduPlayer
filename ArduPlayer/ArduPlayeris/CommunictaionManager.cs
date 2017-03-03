﻿using System;
using System.Threading.Tasks;

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

            mainform.ColorOranToggle.CheckedChanged += new System.EventHandler(this.ColorOranToggleChanged);
            mainform.metroComboBox1.SelectedIndexChanged += MetroComboBox1_SelectedIndexChanged;
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

        private async void SpotifyHelper_SpotifySongChanged(string artist, string title)
        {
            serial.Send("artist" + artist);
            await Task.Delay(100);
            serial.Send("title" + title);
            mainform.NowPlayingLbl.Text = "Now playing: " + title + " by " + artist; //+ ", " + collection[2];
        }

        private void Serial_CommandRecieved(string text)
        {
            text = text.Substring(1);

            switch (text)
            {
                case "+":
                    SpotifyHelper.VolumeHelper.IncrementVolume("Spotify");
                    // serial.Send("volume" + Math.Round((double)SpotifyHelper.VolumeHelper.GetApplicationVolume("Spotify")).ToString());
                    break;
                case "-":
                    SpotifyHelper.VolumeHelper.DecrementVolume("Spotify");
                    //serial.Send("volume" + Math.Round((double)SpotifyHelper.VolumeHelper.GetApplicationVolume("Spotify")).ToString());
                    break;
                case "cl1":
                    //CheckSpotiySong(null, null);
                    SpotifyHelper.Win32.SendMessage(SpotifyHelper.GetSpotify(), SpotifyHelper.Win32.Constants.WM_APPCOMMAND, IntPtr.Zero, new IntPtr((long)SpotifyHelper.SpotifyAction.PlayPause));
                    break;
                case "cl2":
                    //CheckSpotiySong(null, null);
                    SpotifyHelper.Win32.SendMessage(SpotifyHelper.GetSpotify(), SpotifyHelper.Win32.Constants.WM_APPCOMMAND, IntPtr.Zero, new IntPtr((long)SpotifyHelper.SpotifyAction.NextTrack));
                    break;
                case "cl3":
                    //CheckSpotiySong(null, null);
                    // Sending two because cl3 happens only after cl2 and cl2 plays next track so we have to go back twice for prev track.
                    SpotifyHelper.Win32.SendMessage(SpotifyHelper.GetSpotify(), SpotifyHelper.Win32.Constants.WM_APPCOMMAND, IntPtr.Zero, new IntPtr((long)SpotifyHelper.SpotifyAction.PreviousTrack));
                    SpotifyHelper.Win32.SendMessage(SpotifyHelper.GetSpotify(), SpotifyHelper.Win32.Constants.WM_APPCOMMAND, IntPtr.Zero, new IntPtr((long)SpotifyHelper.SpotifyAction.PreviousTrack));
                    break;
                case "!":
                    // getInfo();
                    break;

            }
        }
    }
}