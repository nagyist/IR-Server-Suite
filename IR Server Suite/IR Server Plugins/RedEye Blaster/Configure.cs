#region Copyright (C) 2005-2009 Team MediaPortal

// Copyright (C) 2005-2009 Team MediaPortal
// http://www.team-mediaportal.com
// 
// This Program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2, or (at your option)
// any later version.
// 
// This Program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with GNU Make; see the file COPYING.  If not, write to
// the Free Software Foundation, 675 Mass Ave, Cambridge, MA 02139, USA.
// http://www.gnu.org/copyleft/gpl.html

#endregion

using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace IRServer.Plugin
{
  internal partial class Configure : Form
  {
    #region Variables

    private readonly string[] _ports;

    #endregion Variables

    #region Properties

    /// <summary>
    /// Gets or sets the communications port.
    /// </summary>
    /// <value>The communications port.</value>
    public string CommPort
    {
      get { return comboBoxPort.SelectedItem as string; }
      set { comboBoxPort.SelectedItem = value; }
    }

    /// <summary>
    /// Gets or sets the blaster mode.
    /// </summary>
    /// <value>The blaster mode.</value>
    public BlastMode BlasterMode
    {
      get
      {
        if (radioButtonIRDA.Checked) return BlastMode.IRDA;
        else if (radioButtonRC5.Checked) return BlastMode.RC5;
        else return BlastMode.Sky;
      }
      set
      {
        switch (value)
        {
          case BlastMode.IRDA:
            radioButtonIRDA.Checked = true;
            break;
          case BlastMode.RC5:
            radioButtonRC5.Checked = true;
            break;
          case BlastMode.Sky:
            radioButtonSky.Checked = true;
            break;
        }
      }
    }

    #endregion Properties

    #region Constructor

    public Configure()
    {
      InitializeComponent();

      comboBoxPort.Items.Clear();

      _ports = SerialPort.GetPortNames();
      if (_ports == null || _ports.Length == 0)
      {
        MessageBox.Show(this, "No available serial ports found!", "IRMan Receiver", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
        return;
      }

      comboBoxPort.Items.AddRange(_ports);
    }

    #endregion Constructor

    #region Buttons

    private void buttonOK_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.OK;
      Close();
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
      Close();
    }

    #endregion Buttons
  }
}