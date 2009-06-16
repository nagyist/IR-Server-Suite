using System;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using IrssComms;

namespace TrayLauncher
{
  internal partial class GetKeyCodeForm : Form
  {
    private void GetKeyCodeForm_Load(object sender, EventArgs e)
    {
      labelStatus.Text = "Press the remote button you want to map";
      labelStatus.ForeColor = Color.Blue;

      _keyCodeSet = KeyCodeSet;

      Tray.HandleMessage += MessageReceiver;

      timer.Start();
    }

    private void MessageReceiver(IrssMessage received)
    {
      if (received.Type == MessageType.RemoteEvent)
      {
        byte[] data = received.GetDataAsBytes();
        int deviceNameSize = BitConverter.ToInt32(data, 0);
        string deviceName = Encoding.ASCII.GetString(data, 4, deviceNameSize);
        int keyCodeSize = BitConverter.ToInt32(data, 4 + deviceNameSize);
        string keyCode = Encoding.ASCII.GetString(data, 8 + deviceNameSize, keyCodeSize);

        _deviceName = deviceName;

        // TODO: When Abstract Remote Model becomes on by default
        //if (deviceName.Equals("Abstract", StringComparison.OrdinalIgnoreCase)
        _keyCode = keyCode;
        //else
        //  _keyCode = String.Format("{0} ({1})", deviceName, keyCode);

        Invoke(_keyCodeSet);
      }
    }

    private void KeyCodeSet()
    {
      timer.Stop();
      Tray.HandleMessage -= MessageReceiver;

      labelStatus.Text = String.Format("Received: {0}", _keyCode);
      labelStatus.ForeColor = Color.Green;
      labelStatus.Update();

      Thread.Sleep(1000);
      Close();
    }

    private void timer_Tick(object sender, EventArgs e)
    {
      timer.Stop();
      Tray.HandleMessage -= MessageReceiver;

      labelStatus.Text = "Timed out";
      labelStatus.ForeColor = Color.Red;
      labelStatus.Update();

      Thread.Sleep(2000);
      Close();
    }

    #region Nested type: DelegateKeyCodeSet

    private delegate void DelegateKeyCodeSet();

    #endregion

    #region Variables

    private string _deviceName = String.Empty;
    private string _keyCode = String.Empty;

    private DelegateKeyCodeSet _keyCodeSet;

    #endregion Variables

    #region Properties

    /// <summary>
    /// Gets the key code received.
    /// </summary>
    /// <value>The key code.</value>
    public string KeyCode
    {
      get { return _keyCode; }
    }

    /// <summary>
    /// Gets the name of the device that generated the key code.
    /// </summary>
    /// <value>The name of the device that generated the key code.</value>
    public string DeviceName
    {
      get { return _deviceName; }
    }

    #endregion Properties

    #region Constructor

    public GetKeyCodeForm()
    {
      InitializeComponent();
    }

    #endregion Constructor
  }
}