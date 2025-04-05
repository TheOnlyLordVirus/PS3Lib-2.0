// Token: 0x0200000B RID: 11
using System.Text;

namespace XDRPC;

public class XDRPCStringArgumentInfo : IXDRPCArgumentInfo
{
    // Token: 0x06000050 RID: 80 RVA: 0x00002C2F File Offset: 0x00001C2F
    public XDRPCStringArgumentInfo(string v) : this(v, Encoding.ASCII)
    {
    }

    // Token: 0x06000051 RID: 81 RVA: 0x00002C3D File Offset: 0x00001C3D
    public XDRPCStringArgumentInfo(string v, Encoding encoding) : this(v, encoding, ArgumentType.ByValue)
    {
    }

    // Token: 0x06000052 RID: 82 RVA: 0x00002C48 File Offset: 0x00001C48
    public XDRPCStringArgumentInfo(string v, Encoding encoding, ArgumentType t) : this(v, encoding, t, 0)
    {
    }

    // Token: 0x06000053 RID: 83 RVA: 0x00002C54 File Offset: 0x00001C54
    public XDRPCStringArgumentInfo(string v, Encoding encoding, ArgumentType t, int cchMax) : this(v, encoding, t, cchMax, XDRPCStringArgumentInfo.GetDefaultCountTypeForEncoding(encoding))
    {
    }

    // Token: 0x06000054 RID: 84 RVA: 0x00002C67 File Offset: 0x00001C67
    public XDRPCStringArgumentInfo(string v, Encoding encoding, ArgumentType t, int cMax, CountType ct)
    {
        this.Value = v;
        this.PassBy = t;
        this.Encoding = encoding;
        this.CchMax = cMax;
        this.MaxType = ct;
        this.CalculateBufferSize();
    }

    // Token: 0x17000008 RID: 8
    // (get) Token: 0x06000055 RID: 85 RVA: 0x00002C9A File Offset: 0x00001C9A
    // (set) Token: 0x06000056 RID: 86 RVA: 0x00002CA2 File Offset: 0x00001CA2
    public Encoding Encoding { get; private set; }

    // Token: 0x17000009 RID: 9
    // (get) Token: 0x06000057 RID: 87 RVA: 0x00002CAB File Offset: 0x00001CAB
    // (set) Token: 0x06000058 RID: 88 RVA: 0x00002CB3 File Offset: 0x00001CB3
    public string Value { get; private set; }

    // Token: 0x1700000A RID: 10
    // (get) Token: 0x06000059 RID: 89 RVA: 0x00002CBC File Offset: 0x00001CBC
    // (set) Token: 0x0600005A RID: 90 RVA: 0x00002CC4 File Offset: 0x00001CC4
    public ArgumentType PassBy { get; private set; }

    // Token: 0x1700000B RID: 11
    // (get) Token: 0x0600005B RID: 91 RVA: 0x00002CCD File Offset: 0x00001CCD
    // (set) Token: 0x0600005C RID: 92 RVA: 0x00002CD5 File Offset: 0x00001CD5
    public CountType MaxType { get; private set; }

    // Token: 0x1700000C RID: 12
    // (get) Token: 0x0600005D RID: 93 RVA: 0x00002CDE File Offset: 0x00001CDE
    // (set) Token: 0x0600005E RID: 94 RVA: 0x00002CE6 File Offset: 0x00001CE6
    public int CchMax { get; private set; }

    // Token: 0x0600005F RID: 95 RVA: 0x00002CF0 File Offset: 0x00001CF0
    public ulong GetArgumentValue(ulong bufferAddress)
    {
        switch (this.PassBy)
        {
            case ArgumentType.ByValue:
            case ArgumentType.ByRef:
            case ArgumentType.Out:
                if (this._maxBufferSize == 0)
                {
                    return 0UL;
                }
                return bufferAddress;
            default:
                return 0UL;
        }
    }

    // Token: 0x06000060 RID: 96 RVA: 0x00002D27 File Offset: 0x00001D27
    public int GetRequiredBufferSize()
    {
        return this._maxBufferSize;
    }

    // Token: 0x06000061 RID: 97 RVA: 0x00002D30 File Offset: 0x00001D30
    public byte[] PackBufferData()
    {
        byte[] array = null;
        if (this._maxBufferSize > 0)
        {
            if (this.Value != null)
            {
                array = this.Encoding.GetBytes(this.Value);
                if (BitConverter.IsLittleEndian && this.Encoding == Encoding.Unicode)
                {
                    MarshalingUtils.ReverseBytes(array, 2);
                }
            }
            else
            {
                array = new byte[this._maxBufferSize];
            }
        }
        return array;
    }

    // Token: 0x06000062 RID: 98 RVA: 0x00002D8C File Offset: 0x00001D8C
    public void UnpackBufferData(byte[] data)
    {
        if (this.PassBy == ArgumentType.ByRef || this.PassBy == ArgumentType.Out)
        {
            if (BitConverter.IsLittleEndian && this.Encoding == Encoding.Unicode)
            {
                MarshalingUtils.ReverseBytes(data, 2);
            }
            this.Value = this.Encoding.GetString(data, 0, this.GetReturnedStringLength(data));
        }
    }

    // Token: 0x06000063 RID: 99 RVA: 0x00002DE0 File Offset: 0x00001DE0
    private int GetReturnedStringLength(byte[] data)
    {
        Encoding encoding = this.Encoding;
        char[] chars = new char[1];
        byte[] bytes = encoding.GetBytes(chars);
        for (int i = 0; i < data.Length; i += bytes.Length)
        {
            bool flag = true;
            for (int j = 0; j < bytes.Length; j++)
            {
                if (data[i + j] != bytes[j])
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                return i;
            }
        }
        return data.Length;
    }

    // Token: 0x06000064 RID: 100 RVA: 0x00002E3C File Offset: 0x00001E3C
    private void CalculateBufferSize()
    {
        int num = 0;
        if (this.Value != null)
        {
            num = this.Encoding.GetByteCount(this.Value + "\0");
        }
        int num2 = (this.MaxType == CountType.Element) ? this.Encoding.GetMaxByteCount(this.CchMax) : this.CchMax;
        if (this.PassBy == ArgumentType.ByRef)
        {
            num = Math.Max(num, num2);
        }
        else if (this.PassBy == ArgumentType.Out)
        {
            num = num2;
        }
        this._maxBufferSize = num;
        if (this.MaxType == CountType.Byte)
        {
            this.CchMax = this._maxBufferSize;
            return;
        }
        int byteCount = this.Encoding.GetByteCount("\0");
        this.CchMax = this._maxBufferSize / byteCount - byteCount;
    }

    // Token: 0x06000065 RID: 101 RVA: 0x00002EED File Offset: 0x00001EED
    internal static CountType GetDefaultCountTypeForEncoding(Encoding encoding)
    {
        if (encoding != Encoding.UTF8)
        {
            return CountType.Element;
        }
        return CountType.Byte;
    }

    // Token: 0x06000066 RID: 102 RVA: 0x00002EFA File Offset: 0x00001EFA
    public override string ToString()
    {
        return string.Format("[XDRPC]String: {0}", this.Value);
    }

    // Token: 0x06000067 RID: 103 RVA: 0x00002F0C File Offset: 0x00001F0C
    public bool IsFloatingPointValue()
    {
        return false;
    }

    // Token: 0x06000068 RID: 104 RVA: 0x00002F0F File Offset: 0x00001F0F
    public int GetArgumentCount()
    {
        return 1;
    }

    // Token: 0x06000069 RID: 105 RVA: 0x00002F12 File Offset: 0x00001F12
    public ulong[] GetArgumentValues(ulong bufferAddress)
    {
        return null;
    }

    // Token: 0x0600006A RID: 106 RVA: 0x00002F15 File Offset: 0x00001F15
    public void SetReferenceAddress(ulong bufferAddress)
    {
    }

    // Token: 0x0600006B RID: 107 RVA: 0x00002F17 File Offset: 0x00001F17
    public int GetRequiredReferenceSize()
    {
        return 0;
    }

    // Token: 0x0600006C RID: 108 RVA: 0x00002F1A File Offset: 0x00001F1A
    public byte[] PackReferenceData()
    {
        return null;
    }

    // Token: 0x0600006D RID: 109 RVA: 0x00002F1D File Offset: 0x00001F1D
    public void UnpackReferenceData(byte[] data)
    {
    }

    // Token: 0x0400000E RID: 14
    private int _maxBufferSize;
}