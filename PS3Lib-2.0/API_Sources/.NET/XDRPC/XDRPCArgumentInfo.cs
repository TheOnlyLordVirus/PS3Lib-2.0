namespace XDRPC;

// Token: 0x0200000A RID: 10
public class XDRPCArgumentInfo<T> : IXDRPCArgumentInfo where T : struct
{
    // Token: 0x0600003B RID: 59 RVA: 0x00002598 File Offset: 0x00001598
    public XDRPCArgumentInfo(T v) : this(v, ArgumentType.ByValue)
    {
    }

    // Token: 0x0600003C RID: 60 RVA: 0x000025A4 File Offset: 0x000015A4
    public XDRPCArgumentInfo(T v, ArgumentType t)
    {
        Type typeFromHandle = typeof(T);
        if (typeFromHandle.IsPrimitive)
        {
            this.Value = v;
            this.PassBy = t;
            return;
        }
        if (XDRPCMarshaler.IsValidStructType(typeFromHandle))
        {
            throw new XDRPCInvalidTypeException(typeFromHandle, string.Format("Type {0} is not supported by XDRPCArgumentInfo, use XDRPCStructArgumentInfo instead.", typeFromHandle.Name));
        }
        throw new XDRPCInvalidTypeException(typeFromHandle, string.Format("Type {0} is not supported by XDRPC.", typeFromHandle.Name));
    }

    // Token: 0x17000006 RID: 6
    // (get) Token: 0x0600003D RID: 61 RVA: 0x0000260E File Offset: 0x0000160E
    // (set) Token: 0x0600003E RID: 62 RVA: 0x00002616 File Offset: 0x00001616
    public T Value { get; private set; }

    // Token: 0x17000007 RID: 7
    // (get) Token: 0x0600003F RID: 63 RVA: 0x0000261F File Offset: 0x0000161F
    // (set) Token: 0x06000040 RID: 64 RVA: 0x00002627 File Offset: 0x00001627
    public ArgumentType PassBy { get; private set; }

    // Token: 0x06000041 RID: 65 RVA: 0x00002630 File Offset: 0x00001630
    public ulong GetArgumentValue(ulong bufferAddress)
    {
        switch (this.PassBy)
        {
            case ArgumentType.ByValue:
                return MarshalingUtils.ConvertToUInt64(this.Value);
            case ArgumentType.ByRef:
            case ArgumentType.Out:
                return bufferAddress;
            default:
                return 0UL;
        }
    }

    // Token: 0x06000042 RID: 66 RVA: 0x00002670 File Offset: 0x00001670
    public int GetRequiredBufferSize()
    {
        switch (this.PassBy)
        {
            case ArgumentType.ByRef:
            case ArgumentType.Out:
                return MarshalingUtils.SizeOf(typeof(T));
            default:
                return 0;
        }
    }

    // Token: 0x06000043 RID: 67 RVA: 0x000026A8 File Offset: 0x000016A8
    public byte[] PackBufferData()
    {
        if (this.PassBy == ArgumentType.ByRef || this.PassBy == ArgumentType.Out)
        {
            byte[] bytes = XDRPCArgumentInfo<T>.GetBytes(this.Value);
            if (BitConverter.IsLittleEndian)
            {
                MarshalingUtils.ReverseBytes(bytes);
            }
            return bytes;
        }
        return null;
    }

    // Token: 0x06000044 RID: 68 RVA: 0x000026E8 File Offset: 0x000016E8
    public void UnpackBufferData(byte[] data)
    {
        if (this.PassBy == ArgumentType.ByRef || this.PassBy == ArgumentType.Out)
        {
            if (BitConverter.IsLittleEndian)
            {
                MarshalingUtils.ReverseBytes(data);
            }
            object value = XDRPCArgumentInfo<T>.GetValue(data);
            object obj;
            if ((obj = value) == null)
            {
                obj = default(T);
            }
            this.Value = (T)((object)obj);
        }
    }

    // Token: 0x06000045 RID: 69 RVA: 0x00002739 File Offset: 0x00001739
    public bool IsFloatingPointValue()
    {
        return this.PassBy == ArgumentType.ByValue && (typeof(T) == typeof(float) || typeof(T) == typeof(double));
    }

    // Token: 0x06000046 RID: 70 RVA: 0x00002774 File Offset: 0x00001774
    private static byte[] GetBytes(object o)
    {
        if (typeof(T) == typeof(bool))
        {
            return BitConverter.GetBytes(((bool)o) ? 1 : 0);
        }
        if (typeof(T) == typeof(byte))
        {
            return new byte[]
            {
                (byte)o
            };
        }
        if (typeof(T) == typeof(short))
        {
            return BitConverter.GetBytes((short)o);
        }
        if (typeof(T) == typeof(int))
        {
            return BitConverter.GetBytes((int)o);
        }
        if (typeof(T) == typeof(long))
        {
            return BitConverter.GetBytes((long)o);
        }
        if (typeof(T) == typeof(ushort))
        {
            return BitConverter.GetBytes((ushort)o);
        }
        if (typeof(T) == typeof(uint))
        {
            return BitConverter.GetBytes((uint)o);
        }
        if (typeof(T) == typeof(ulong))
        {
            return BitConverter.GetBytes((ulong)o);
        }
        if (typeof(T) == typeof(double))
        {
            return BitConverter.GetBytes((double)o);
        }
        if (typeof(T) == typeof(float))
        {
            return BitConverter.GetBytes((float)o);
        }
        return null;
    }

    // Token: 0x06000047 RID: 71 RVA: 0x000028E4 File Offset: 0x000018E4
    private static object GetValue(byte[] buffer)
    {
        if (typeof(T) == typeof(bool))
        {
            return BitConverter.ToUInt32(buffer, 0) != 0U;
        }
        if (typeof(T) == typeof(byte))
        {
            if (buffer.Length <= 0)
            {
                return null;
            }
            return buffer[0];
        }
        else
        {
            if (typeof(T) == typeof(short))
            {
                return BitConverter.ToInt16(buffer, 0);
            }
            if (typeof(T) == typeof(int))
            {
                return BitConverter.ToInt32(buffer, 0);
            }
            if (typeof(T) == typeof(long))
            {
                return BitConverter.ToInt64(buffer, 0);
            }
            if (typeof(T) == typeof(ushort))
            {
                return BitConverter.ToUInt16(buffer, 0);
            }
            if (typeof(T) == typeof(uint))
            {
                return BitConverter.ToUInt32(buffer, 0);
            }
            if (typeof(T) == typeof(ulong))
            {
                return BitConverter.ToUInt64(buffer, 0);
            }
            if (typeof(T) == typeof(double))
            {
                return BitConverter.ToDouble(buffer, 0);
            }
            if (typeof(T) == typeof(float))
            {
                return BitConverter.ToSingle(buffer, 0);
            }
            return null;
        }
    }

    // Token: 0x06000048 RID: 72 RVA: 0x00002A5C File Offset: 0x00001A5C
    private static T ConvertFromUInt64(ulong v)
    {
        object obj = default(T);
        if (typeof(T) == typeof(bool))
        {
            obj = ((v & 65535UL) != 0UL);
        }
        else if (typeof(T) == typeof(byte))
        {
            obj = (byte)v;
        }
        else if (typeof(T) == typeof(short))
        {
            obj = (short)v;
        }
        else if (typeof(T) == typeof(int))
        {
            obj = (int)v;
        }
        else if (typeof(T) == typeof(long))
        {
            obj = (long)v;
        }
        else if (typeof(T) == typeof(ushort))
        {
            obj = (ushort)v;
        }
        else if (typeof(T) == typeof(uint))
        {
            obj = (uint)v;
        }
        else if (typeof(T) == typeof(ulong))
        {
            obj = v;
        }
        else if (typeof(T) == typeof(float) || typeof(T) == typeof(double))
        {
            obj = BitConverter.Int64BitsToDouble((long)v);
        }
        return (T)((object)obj);
    }

    // Token: 0x06000049 RID: 73 RVA: 0x00002BD1 File Offset: 0x00001BD1
    public override string ToString()
    {
        return string.Format("[XDRPC]{0}: {1}", typeof(T).Name, this.Value);
    }

    // Token: 0x0600004A RID: 74 RVA: 0x00002BF7 File Offset: 0x00001BF7
    public int GetArgumentCount()
    {
        return 1;
    }

    // Token: 0x0600004B RID: 75 RVA: 0x00002BFC File Offset: 0x00001BFC
    public ulong[] GetArgumentValues(ulong bufferAddress)
    {
        if (this.PassBy == ArgumentType.ByValue)
        {
            return new ulong[]
            {
                this.GetArgumentValue(bufferAddress)
            };
        }
        return null;
    }

    // Token: 0x0600004C RID: 76 RVA: 0x00002C25 File Offset: 0x00001C25
    public void SetReferenceAddress(ulong bufferAddress)
    {
    }

    // Token: 0x0600004D RID: 77 RVA: 0x00002C27 File Offset: 0x00001C27
    public int GetRequiredReferenceSize()
    {
        return 0;
    }

    // Token: 0x0600004E RID: 78 RVA: 0x00002C2A File Offset: 0x00001C2A
    public byte[] PackReferenceData()
    {
        return null;
    }

    // Token: 0x0600004F RID: 79 RVA: 0x00002C2D File Offset: 0x00001C2D
    public void UnpackReferenceData(byte[] data)
    {
    }
}