using MetroFramework;
using MetroFramework.Components;
using MetroFramework.Forms;
using System;

namespace ArduPlayeris
{
    partial class MainForm
    {
        private FixedStyleManager m_themeManager;

        private void InitThemeSettings()
        {
           
                m_themeManager = new FixedStyleManager(this);
                themeCombobox.Items.AddRange(Enum.GetNames(typeof(MetroThemeStyle)));
                styleCombobox.Items.AddRange(Enum.GetNames(typeof(MetroColorStyle)));

                themeCombobox.SelectedValueChanged += themeCombobox_SelectedValueChanged;
                styleCombobox.SelectedValueChanged += styleCombobox_SelectedValueChanged;
                

                themeCombobox.SelectedItem = Properties.Settings.Default.Theme.ToString();
                styleCombobox.SelectedItem = Properties.Settings.Default.Style.ToString();
                metroToggle2.Checked = Properties.Settings.Default.Try;

              //  tglStartByDefault.Checked = Config.Default.StartProxyByDefault;
              // lstServers.SelectedItem = Config.Default.DefaultServerName;

        }

        private void SaveSettingsButton(object sender, EventArgs e)
        {            
            Properties.Settings.Default.Theme = (MetroThemeStyle)Enum.Parse(typeof(MetroThemeStyle), (string)themeCombobox.SelectedItem, true);            
            Properties.Settings.Default.Style = (MetroColorStyle)Enum.Parse(typeof(MetroColorStyle), (string)styleCombobox.SelectedItem, true);
            Properties.Settings.Default.Try = metroToggle2.Checked;
            Properties.Settings.Default.Save();
            MetroMessageBox.Show(this, "\nSettings have been saved successfully","Settings");
            
        }

        private void styleCombobox_SelectedValueChanged(object sender, EventArgs e)
        {
            m_themeManager.Style = (MetroColorStyle)Enum.Parse(typeof(MetroColorStyle), (string)styleCombobox.SelectedItem, true);
        }

        private void themeCombobox_SelectedValueChanged(object sender, EventArgs e)
        {
            m_themeManager.Theme = (MetroThemeStyle)Enum.Parse(typeof(MetroThemeStyle), (string)themeCombobox.SelectedItem, true);
        }

        private class FixedStyleManager
        {
            public event EventHandler OnThemeChanged;
            public event EventHandler OnStyleChanged;

            private MetroStyleManager m_manager;

            private MetroColorStyle m_colorStyle;
            private MetroThemeStyle m_themeStyle;


            public FixedStyleManager(MetroForm form)
            {
                m_manager = new MetroStyleManager(form.Container);
                m_manager.Owner = form;
            }

            public MetroColorStyle Style
            {
                get { return m_colorStyle; }
                set
                {
                    m_colorStyle = value;
                    Update();
                    if (OnStyleChanged != null) OnStyleChanged(this, new EventArgs());
                }
            }

            public MetroThemeStyle Theme
            {
                get { return m_themeStyle; }
                set
                {
                    m_themeStyle = value;
                    Update();
                    if (OnThemeChanged != null) OnThemeChanged(this, new EventArgs());
                }
            }

            public void Update()
            {
                (m_manager.Owner as MetroForm).Theme = m_themeStyle;
                (m_manager.Owner as MetroForm).Style = m_colorStyle;

                m_manager.Theme = m_themeStyle;
                m_manager.Style = m_colorStyle;
            }
        }
    }
}
