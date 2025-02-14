﻿using SharpPcap;
using SharpPcap.LibPcap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace MapleShark
{
    public partial class SetupForm : Form
    {
        private Dictionary<string, string> interfaceNames = new Dictionary<string, string>();

        public SetupForm()
        {
            InitializeComponent();

            Text = "MapleShark " + Program.AssemblyVersion + ", " + Program.AssemblyCopyright;
            bool selected = false;
            //int localAreaConnection = -1;

            foreach (var devInterface in PcapInterface.GetAllPcapInterfaces())
            {
                var deviceName = devInterface.FriendlyName;
                if (deviceName == null)
                    deviceName = devInterface.Name;
                
                int index = mInterfaceCombo.Items.Add(deviceName);
                interfaceNames.Add(deviceName, devInterface.Name);

                if (!selected && (selected = (deviceName == Config.Instance.Interface))) mInterfaceCombo.SelectedIndex = index;
            }

            if (!selected && mInterfaceCombo.Items.Count > 0) mInterfaceCombo.SelectedIndex = 0;
            mLowPortNumeric.Value = Config.Instance.LowPort;
            mHighPortNumeric.Value = Config.Instance.HighPort;
        }

        private void mInterfaceCombo_SelectedIndexChanged(object pSender, EventArgs pArgs)
        {
            mOKButton.Enabled = mInterfaceCombo.SelectedIndex >= 0;
        }

        private void mLowPortNumeric_ValueChanged(object pSender, EventArgs pArgs)
        {
            if (mLowPortNumeric.Value > mHighPortNumeric.Value) mLowPortNumeric.Value = mHighPortNumeric.Value;
        }

        private void mHighPortNumeric_ValueChanged(object pSender, EventArgs pArgs)
        {
            if (mHighPortNumeric.Value < mLowPortNumeric.Value) mHighPortNumeric.Value = mLowPortNumeric.Value;
        }

        private void mOKButton_Click(object pSender, EventArgs pArgs)
        {
            Config.Instance.Interface = interfaceNames[(string)mInterfaceCombo.SelectedItem];
            Config.Instance.LowPort = (ushort)mLowPortNumeric.Value;
            Config.Instance.HighPort = (ushort)mHighPortNumeric.Value;
            Config.Instance.Save();

            DialogResult = DialogResult.OK;
        }

        private void SetupForm_Load(object sender, EventArgs e)
        {

        }
    }
}
