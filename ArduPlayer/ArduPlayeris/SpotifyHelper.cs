using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ArduPlayeris
{
    public delegate void SpotifySongListener(string artist,string title);
    public class SpotifyHelper
    {
        internal class Win32
        {
            [DllImport("user32.dll", SetLastError = true)]
            internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

            internal delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

            [DllImport("user32.dll")]
            internal static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

            [DllImport("user32.dll")]
            internal static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

            [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            internal static extern int GetWindowTextLength(IntPtr hWnd);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
            internal static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll")]
            internal static extern IntPtr SetFocus(IntPtr hWnd);

            [DllImport("user32.dll")]
            internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

            [DllImport("user32.dll")]
            internal static extern bool SetForegroundWindow(IntPtr hWnd);

            [DllImport("user32.dll")]
            internal static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

            [DllImport("user32.dll", SetLastError = true)]
            internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

            [Flags()]
            internal enum SetWindowPosFlags : uint
            {
                /// <summary>If the calling thread and the thread that owns the window are attached to different input queues, 
                /// the system posts the request to the thread that owns the window. This prevents the calling thread from 
                /// blocking its execution while other threads process the request.</summary>
                /// <remarks>SWP_ASYNCWINDOWPOS</remarks>
                AsynchronousWindowPosition = 0x4000,
                /// <summary>Prevents generation of the WM_SYNCPAINT message.</summary>
                /// <remarks>SWP_DEFERERASE</remarks>
                DeferErase = 0x2000,
                /// <summary>Draws a frame (defined in the window's class description) around the window.</summary>
                /// <remarks>SWP_DRAWFRAME</remarks>
                DrawFrame = 0x0020,
                /// <summary>Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to 
                /// the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE 
                /// is sent only when the window's size is being changed.</summary>
                /// <remarks>SWP_FRAMECHANGED</remarks>
                FrameChanged = 0x0020,
                /// <summary>Hides the window.</summary>
                /// <remarks>SWP_HIDEWINDOW</remarks>
                HideWindow = 0x0080,
                /// <summary>Does not activate the window. If this flag is not set, the window is activated and moved to the 
                /// top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter 
                /// parameter).</summary>
                /// <remarks>SWP_NOACTIVATE</remarks>
                DoNotActivate = 0x0010,
                /// <summary>Discards the entire contents of the client area. If this flag is not specified, the valid 
                /// contents of the client area are saved and copied back into the client area after the window is sized or 
                /// repositioned.</summary>
                /// <remarks>SWP_NOCOPYBITS</remarks>
                DoNotCopyBits = 0x0100,
                /// <summary>Retains the current position (ignores X and Y parameters).</summary>
                /// <remarks>SWP_NOMOVE</remarks>
                IgnoreMove = 0x0002,
                /// <summary>Does not change the owner window's position in the Z order.</summary>
                /// <remarks>SWP_NOOWNERZORDER</remarks>
                DoNotChangeOwnerZOrder = 0x0200,
                /// <summary>Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to 
                /// the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent 
                /// window uncovered as a result of the window being moved. When this flag is set, the application must 
                /// explicitly invalidate or redraw any parts of the window and parent window that need redrawing.</summary>
                /// <remarks>SWP_NOREDRAW</remarks>
                DoNotRedraw = 0x0008,
                /// <summary>Same as the SWP_NOOWNERZORDER flag.</summary>
                /// <remarks>SWP_NOREPOSITION</remarks>
                DoNotReposition = 0x0200,
                /// <summary>Prevents the window from receiving the WM_WINDOWPOSCHANGING message.</summary>
                /// <remarks>SWP_NOSENDCHANGING</remarks>
                DoNotSendChangingEvent = 0x0400,
                /// <summary>Retains the current size (ignores the cx and cy parameters).</summary>
                /// <remarks>SWP_NOSIZE</remarks>
                IgnoreResize = 0x0001,
                /// <summary>Retains the current Z order (ignores the hWndInsertAfter parameter).</summary>
                /// <remarks>SWP_NOZORDER</remarks>
                IgnoreZOrder = 0x0004,
                /// <summary>Displays the window.</summary>
                /// <remarks>SWP_SHOWWINDOW</remarks>
                ShowWindow = 0x0040,
            }

            internal struct WINDOWPLACEMENT
            {
                public int length;
                public int flags;
                public int showCmd;
                public System.Drawing.Point ptMinPosition;
                public System.Drawing.Point ptMaxPosition;
                public System.Drawing.Rectangle rcNormalPosition;
            }

            internal class Constants
            {
                internal const uint WM_APPCOMMAND = 0x0319;

                internal const int SW_SHOWMINIMIZED = 2;
                internal const int SW_SHOWNOACTIVATE = 4;
                internal const int SW_SHOWMINNOACTIVE = 7;
                internal const int SW_SHOW = 5;
                internal const int SW_RESTORE = 9;

                internal const int WM_CLOSE = 0x10;
                internal const int WM_QUIT = 0x12;
            }
        }

        public class VolumeHelper
        {
            // base code from: http://stackoverflow.com/a/14322736 

            public static void IncrementVolume(string name)
            {
                var curVolume = GetApplicationVolume(name);

                if (curVolume != null && curVolume < 100)
                    SetApplicationVolume(name, (float)curVolume + 3);
            }

            public static void DecrementVolume(string name)
            {
                var curVolume = GetApplicationVolume(name);

                if (curVolume != null && curVolume > 0)
                    SetApplicationVolume(name, (float)curVolume - 3);

            }

            public static float? GetApplicationVolume(string name)
            {
                ISimpleAudioVolume volume = GetVolumeObject(name);
                if (volume == null)
                    return null;

                float level;
                volume.GetMasterVolume(out level);
                return level * 100;
            }

            public static bool? GetApplicationMute(string name)
            {
                ISimpleAudioVolume volume = GetVolumeObject(name);
                if (volume == null)
                    return null;

                bool mute;
                volume.GetMute(out mute);
                return mute;
            }

            public static void SetApplicationVolume(string name, float level)
            {
                ISimpleAudioVolume volume = GetVolumeObject(name);
                if (volume == null)
                    return;

                Guid guid = Guid.Empty;
                volume.SetMasterVolume(level / 100, ref guid);
            }

            internal static void ToggleApplicationMute(string name)
            {

                var muted = GetApplicationMute(name);

                if (muted == null)
                    return;

                SetApplicationMute(name, !(bool)muted);
            }


            public static void SetApplicationMute(string name, bool mute)
            {
                ISimpleAudioVolume volume = GetVolumeObject(name);
                if (volume == null)
                    return;

                Guid guid = Guid.Empty;
                volume.SetMute(mute, ref guid);
            }

            public static IEnumerable<string> EnumerateApplications()
            {
                // get the speakers (1st render + multimedia) device
                IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)(new MMDeviceEnumerator());
                IMMDevice speakers;
                deviceEnumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia, out speakers);

                // activate the session manager. we need the enumerator
                Guid IID_IAudioSessionManager2 = typeof(IAudioSessionManager2).GUID;
                object o;
                speakers.Activate(ref IID_IAudioSessionManager2, 0, IntPtr.Zero, out o);
                IAudioSessionManager2 mgr = (IAudioSessionManager2)o;

                // enumerate sessions for on this device
                IAudioSessionEnumerator sessionEnumerator;
                mgr.GetSessionEnumerator(out sessionEnumerator);
                int count;
                sessionEnumerator.GetCount(out count);

                for (int i = 0; i < count; i++)
                {
                    IAudioSessionControl ctl;
                    IAudioSessionControl2 ctl2;

                    sessionEnumerator.GetSession(i, out ctl);

                    ctl2 = ctl as IAudioSessionControl2;

                    if (ctl2 != null)
                    {
                        uint pid = 0;
                        string sout1 = "";
                        string sout2 = "";

                        ctl2.GetSessionIdentifier(out sout1);
                        ctl2.GetProcessId(out pid);
                        ctl2.GetSessionInstanceIdentifier(out sout2);

                    }

                    string dn;
                    ctl.GetDisplayName(out dn);
                    yield return dn;
                    Marshal.ReleaseComObject(ctl);
                }
                Marshal.ReleaseComObject(sessionEnumerator);
                Marshal.ReleaseComObject(mgr);
                Marshal.ReleaseComObject(speakers);
                Marshal.ReleaseComObject(deviceEnumerator);
            }

            private static ISimpleAudioVolume GetVolumeObject(string name)
            {
                // get the speakers (1st render + multimedia) device
                IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)(new MMDeviceEnumerator());
                IMMDevice speakers;
                deviceEnumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia, out speakers);

                // activate the session manager. we need the enumerator
                Guid IID_IAudioSessionManager2 = typeof(IAudioSessionManager2).GUID;
                object o;
                speakers.Activate(ref IID_IAudioSessionManager2, 0, IntPtr.Zero, out o);
                IAudioSessionManager2 mgr = (IAudioSessionManager2)o;

                // enumerate sessions for on this device
                IAudioSessionEnumerator sessionEnumerator;
                mgr.GetSessionEnumerator(out sessionEnumerator);
                int count;
                sessionEnumerator.GetCount(out count);

                // lower case name for easier comparison with the Session ID later on
                name = name.ToLower();

                // search for an audio session with the required name
                // Note: Since GetDisplayName only returns a real name if the application bothered to call SetDisplayName
                //       (which apps like Spotify do not), we instead use the SessionID (which usually includes the exe name)
                ISimpleAudioVolume volumeControl = null;

                for (int i = 0; i < count; i++)
                {

                    IAudioSessionControl ctl;
                    IAudioSessionControl2 ctl2;

                    sessionEnumerator.GetSession(i, out ctl);

                    ctl2 = ctl as IAudioSessionControl2;

                    string dn;
                    ctl.GetDisplayName(out dn);

                    if (ctl2 != null)
                    {
                        string sessionID = "";

                        ctl2.GetSessionIdentifier(out sessionID);

                        if (sessionID.ToLower().Contains(name))
                        {
                            volumeControl = ctl as ISimpleAudioVolume;
                            break;
                        }
                    }

                    if (ctl != null)
                        Marshal.ReleaseComObject(ctl);

                    if (ctl2 != null)
                        Marshal.ReleaseComObject(ctl2);
                }

                Marshal.ReleaseComObject(sessionEnumerator);
                Marshal.ReleaseComObject(mgr);
                Marshal.ReleaseComObject(speakers);
                Marshal.ReleaseComObject(deviceEnumerator);

                return volumeControl;
            }

            //
            // Should probably be in the central WinHelper (or a NativeMethods) but it seemed cleaner to
            // keep it all together.
            //

            [ComImport]
            [Guid("BCDE0395-E52F-467C-8E3D-C4579291692E")]
            internal class MMDeviceEnumerator
            {
            }

            internal enum EDataFlow
            {
                eRender,
                eCapture,
                eAll,
                EDataFlow_enum_count
            }

            internal enum ERole
            {
                eConsole,
                eMultimedia,
                eCommunications,
                ERole_enum_count
            }

            [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
            internal interface IMMDeviceEnumerator
            {
                int NotImpl1();

                [PreserveSig]
                int GetDefaultAudioEndpoint(EDataFlow dataFlow, ERole role, out IMMDevice ppDevice);

                // the rest is not implemented
            }

            [Guid("D666063F-1587-4E43-81F1-B948E807363F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
            internal interface IMMDevice
            {
                [PreserveSig]
                int Activate(ref Guid iid, int dwClsCtx, IntPtr pActivationParams, [MarshalAs(UnmanagedType.IUnknown)] out object ppInterface);

                // the rest is not implemented
            }

            [Guid("77AA99A0-1BD6-484F-8BC7-2C654C9A9B6F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
            internal interface IAudioSessionManager2
            {
                int NotImpl1();
                int NotImpl2();

                [PreserveSig]
                int GetSessionEnumerator(out IAudioSessionEnumerator SessionEnum);

                // the rest is not implemented
            }

            [Guid("E2F5BB11-0570-40CA-ACDD-3AA01277DEE8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
            internal interface IAudioSessionEnumerator
            {
                [PreserveSig]
                int GetCount(out int SessionCount);

                [PreserveSig]
                int GetSession(int SessionCount, out IAudioSessionControl Session);
            }

            [Guid("F4B1A599-7266-4319-A8CA-E70ACB11E8CD"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
            internal interface IAudioSessionControl
            {
                int NotImpl1();

                [PreserveSig]
                int GetDisplayName([MarshalAs(UnmanagedType.LPWStr)] out string pRetVal);

                // the rest is not implemented
            }

            public enum AudioSessionState
            {
                AudioSessionStateInactive = 0,
                AudioSessionStateActive = 1,
                AudioSessionStateExpired = 2
            }

            public enum AudioSessionDisconnectReason
            {
                DisconnectReasonDeviceRemoval = 0,
                DisconnectReasonServerShutdown = (DisconnectReasonDeviceRemoval + 1),
                DisconnectReasonFormatChanged = (DisconnectReasonServerShutdown + 1),
                DisconnectReasonSessionLogoff = (DisconnectReasonFormatChanged + 1),
                DisconnectReasonSessionDisconnected = (DisconnectReasonSessionLogoff + 1),
                DisconnectReasonExclusiveModeOverride = (DisconnectReasonSessionDisconnected + 1)
            }

            [Guid("24918ACC-64B3-37C1-8CA9-74A66E9957A8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
            public interface IAudioSessionEvents
            {
                [PreserveSig]
                int OnDisplayNameChanged([MarshalAs(UnmanagedType.LPWStr)] string NewDisplayName, Guid EventContext);
                [PreserveSig]
                int OnIconPathChanged([MarshalAs(UnmanagedType.LPWStr)] string NewIconPath, Guid EventContext);
                [PreserveSig]
                int OnSimpleVolumeChanged(float NewVolume, bool newMute, Guid EventContext);
                [PreserveSig]
                int OnChannelVolumeChanged(UInt32 ChannelCount, IntPtr NewChannelVolumeArray, UInt32 ChangedChannel, Guid EventContext);
                [PreserveSig]
                int OnGroupingParamChanged(Guid NewGroupingParam, Guid EventContext);
                [PreserveSig]
                int OnStateChanged(AudioSessionState NewState);
                [PreserveSig]
                int OnSessionDisconnected(AudioSessionDisconnectReason DisconnectReason);
            }

            [Guid("BFB7FF88-7239-4FC9-8FA2-07C950BE9C6D"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
            public interface IAudioSessionControl2
            {
                [PreserveSig]
                int GetState(out AudioSessionState state);
                [PreserveSig]
                int GetDisplayName([Out(), MarshalAs(UnmanagedType.LPWStr)] out string name);
                [PreserveSig]
                int SetDisplayName([MarshalAs(UnmanagedType.LPWStr)] string value, Guid EventContext);
                [PreserveSig]
                int GetIconPath([Out(), MarshalAs(UnmanagedType.LPWStr)] out string Path);
                [PreserveSig]
                int SetIconPath([MarshalAs(UnmanagedType.LPWStr)] string Value, Guid EventContext);
                [PreserveSig]
                int GetGroupingParam(out Guid GroupingParam);
                [PreserveSig]
                int SetGroupingParam(Guid Override, Guid Eventcontext);
                [PreserveSig]
                int RegisterAudioSessionNotification(IAudioSessionEvents NewNotifications);
                [PreserveSig]
                int UnregisterAudioSessionNotification(IAudioSessionEvents NewNotifications);
                [PreserveSig]
                int GetSessionIdentifier([Out(), MarshalAs(UnmanagedType.LPWStr)] out string retVal);
                [PreserveSig]
                int GetSessionInstanceIdentifier([Out(), MarshalAs(UnmanagedType.LPWStr)] out string retVal);
                [PreserveSig]
                int GetProcessId(out UInt32 retvVal);
                [PreserveSig]
                int IsSystemSoundsSession();
                [PreserveSig]
                int SetDuckingPreference(bool optOut);
            }


            [Guid("87CE5498-68D6-44E5-9215-6DA47EF883D8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
            internal interface ISimpleAudioVolume
            {
                [PreserveSig]
                int SetMasterVolume(float fLevel, ref Guid EventContext);

                [PreserveSig]
                int GetMasterVolume(out float pfLevel);

                [PreserveSig]
                int SetMute(bool bMute, ref Guid EventContext);

                [PreserveSig]
                int GetMute(out bool pbMute);
            }
        }

        public event SpotifySongListener SpotifySongChanged;
        private System.Windows.Forms.Timer timer;

        public SpotifyHelper()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(timer1_tick);
            timer.Interval = 1000; // in miliseconds
            timer.Start();
        }

        private void timer1_tick(object sender, EventArgs e)
        {
            CheckSpotiySong();
        }

        string[] buvo = new string[3];
        private void CheckSpotiySong()
        {
            string[] collection = SpotifyHelper.GetSong();
            if (buvo[0] != collection[0])
            {
               buvo = collection;
               SpotifySongChanged?.Invoke(collection[0],collection[1]);
            }
        }

        public enum SpotifyAction : long
        {
            None = 0,
            ShowToast = 1,
            ShowSpotify = 2,
            CopyTrackInfo = 3,
            SettingsSaved = 4,
            PasteTrackInfo = 5,
            ThumbsUp = 6,   // not usable, left in for future (hopefully!)
            ThumbsDown = 7, // not usable, left in for future (hopefully!)
            PlayPause = 917504,
            Mute = 524288,
            VolumeDown = 589824,
            VolumeUp = 655360,
            Stop = 851968,
            PreviousTrack = 786432,
            NextTrack = 720896,
            FastForward = 49 << 16,
            Rewind = 50 << 16,
        }

        public static IntPtr GetSpotify()
        {
            var windowClassName = "SpotifyMainWindow";

            return Win32.FindWindow(windowClassName, null);
        }

        public static string[] GetSong()
        {
            string song = "";
            string artist = "";
            string whatsLeft = "";
            IntPtr hWnd = Win32.FindWindow("SpotifyMainWindow", null);
            int length = Win32.GetWindowTextLength(hWnd);
            StringBuilder sb = new StringBuilder(length + 1);
            Win32.GetWindowText(hWnd, sb, sb.Capacity);

            string title = sb.ToString();
            string[] collection = new string[3];

            if (!string.IsNullOrWhiteSpace(title) && title != "Spotify")
            {
                var portions = title.Split(new string[] { " - " }, StringSplitOptions.None);
                song = (portions.Length > 1 ? portions[1] : null);
                artist = portions[0];
                whatsLeft = (portions.Length > 2 ? string.Join(" ", portions.Skip(2).ToArray()) : null);                
                collection[0] = song;
                collection[1] = artist;
             //   collection[2] = whatsLeft;
            }
            return collection;
        }
    }
}
