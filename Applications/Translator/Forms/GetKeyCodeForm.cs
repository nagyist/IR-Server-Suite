using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using IrssComms;
using IrssUtils;

namespace Translator
{

  partial class GetKeyCodeForm : Form
  {

    #region Delegates

    delegate void DelegateKeyCodeSet();

    #endregion Delegates

    #region Variables

    string _keyCode = String.Empty;

    DelegateKeyCodeSet _keyCodeSet;

    #endregion Variables

    #region Properties

    public string KeyCode
    {
      get { return _keyCode; }
    }

    #endregion Properties

    #region Constructor

    public GetKeyCodeForm()
    {
      InitializeComponent();
    }

    #endregion Constructor

    private void GetKeyCodeForm_Load(object sender, EventArgs e)
    {
      labelStatus.Text = "Press the remote button to map";

      _keyCodeSet = new DelegateKeyCodeSet(KeyCodeSet);

      Program.HandleMessage += new ClientMessageSink(MessageReceiver);

      timer.Start();
    }

    void MessageReceiver(IrssMessage received)
    {
      if (received.Type == MessageType.RemoteEvent)
      {
        byte[] data = received.GetDataAsBytes();
        int deviceNameSize = BitConverter.ToInt32(data, 0);
        string deviceName = Encoding.ASCII.GetString(data, 4, deviceNameSize);
        int keyCodeSize = BitConverter.ToInt32(data, 4 + deviceNameSize);
        string keyCode = Encoding.ASCII.GetString(data, 8 + deviceNameSize, keyCodeSize);

        _keyCode = keyCode;
        /*
        if (!deviceName.Equals("Abstract", StringComparison.OrdinalIgnoreCase))
        {
          _keyCode = String.Format("{0} ({1})", deviceName, keyCode);
          // TODO: REMOVE!
          return;
        }*/

        this.Invoke(_keyCodeSet);
      }
    }

    void KeyCodeSet()
    {
      timer.Stop();

      Program.HandleMessage -= new ClientMessageSink(MessageReceiver);

      this.Close();
    }

    private void timer_Tick(object sender, EventArgs e)
    {
      KeyCodeSet();
    }

  }

}
