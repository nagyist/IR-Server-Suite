namespace InputService.Plugin
{

  #region Enumerations

  internal enum RemoteDetectionState
  {
    HeaderPulse,
    HeaderSpace,
    PreData,
    Data,
    KeyCode,
    Leading
  }

  #endregion Enumerations

  internal class RemoteDetectionData
  {
    #region Member Variables

    private byte _bit;
    private uint _code;
    private byte _halfBit;
    private uint _header;
    private bool _longPulse;
    private bool _longSpace;
    private RemoteDetectionState _state = RemoteDetectionState.HeaderPulse;
    private int _toggle;

    #endregion Member Variables

    #region Properties

    public RemoteDetectionState State
    {
      get { return _state; }
      set { _state = value; }
    }

    public byte Bit
    {
      get { return _bit; }
      set { _bit = value; }
    }

    public byte HalfBit
    {
      get { return _halfBit; }
      set { _halfBit = value; }
    }

    public uint Code
    {
      get { return _code; }
      set { _code = value; }
    }

    public uint Header
    {
      get { return _header; }
      set { _header = value; }
    }

    public bool LongPulse
    {
      get { return _longPulse; }
      set { _longPulse = value; }
    }

    public bool LongSpace
    {
      get { return _longSpace; }
      set { _longSpace = value; }
    }

    public int Toggle
    {
      get { return _toggle; }
      set { _toggle = value; }
    }

    #endregion Properties

    #region Constructors

    public RemoteDetectionData() : this(RemoteDetectionState.HeaderPulse)
    {
    }

    public RemoteDetectionData(RemoteDetectionState state)
    {
      _state = state;
    }

    #endregion Constructors
  }
}