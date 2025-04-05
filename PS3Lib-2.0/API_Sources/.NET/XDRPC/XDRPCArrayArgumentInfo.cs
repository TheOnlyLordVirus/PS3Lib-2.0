// Token: 0x0200000C RID: 12
namespace XDRPC;

public class XDRPCArrayArgumentInfo<T> : IXDRPCArgumentInfo where T : class
{
    // Token: 0x0600006E RID: 110 RVA: 0x00002F1F File Offset: 0x00001F1F
    public XDRPCArrayArgumentInfo(T v) : this(v, ArgumentType.ByValue, 0)
    {
    }

    // Token: 0x0600006F RID: 111 RVA: 0x00002F2A File Offset: 0x00001F2A
    public XDRPCArrayArgumentInfo(T v, ArgumentType t) : this(v, t, 0)
    {
    }

    // Token: 0x06000070 RID: 112 RVA: 0x00002F38 File Offset: 0x00001F38
    public XDRPCArrayArgumentInfo(T v, ArgumentType t, int maxArrayLength)
    {
        Type typeFromHandle = typeof(T);
        if (!typeFromHandle.IsArray)
        {
            throw new XDRPCInvalidTypeException(typeFromHandle, "Array type must be provided for XDRPCArrayArgumentInfo.");
        }
        if (!XDRPCMarshaler.IsValidArrayType(typeFromHandle))
        {
            throw new XDRPCInvalidTypeException(typeFromHandle, string.Format("Array type {0} is not supported by XDRPC.", typeFromHandle.Name));
        }
        this.Value = v;
        if (this.Value != null)
        {
            this._arrayLength = (this.Value as Array).Length;
        }
        else
        {
            this._arrayLength = 0;
        }
        if (t == ArgumentType.Out && maxArrayLength <= 0)
        {
            throw new XDRPCException("The max array length must be specified for an Out type array.");
        }
        this.PassBy = t;
        this._elementSize = MarshalingUtils.SizeOf(typeFromHandle.GetElementType());
        if (this.PassBy == ArgumentType.ByRef)
        {
            this.MaxArrayLength = Math.Max(this._arrayLength, maxArrayLength);
        }
        else if (this.PassBy == ArgumentType.Out)
        {
            this.MaxArrayLength = maxArrayLength;
        }
        else
        {
            this.MaxArrayLength = this._arrayLength;
        }
        this.ConstructArgumentInfoArray();
    }

    // Token: 0x1700000D RID: 13
    // (get) Token: 0x06000071 RID: 113 RVA: 0x0000302D File Offset: 0x0000202D
    // (set) Token: 0x06000072 RID: 114 RVA: 0x00003035 File Offset: 0x00002035
    public T Value { get; private set; }

    // Token: 0x1700000E RID: 14
    // (get) Token: 0x06000073 RID: 115 RVA: 0x0000303E File Offset: 0x0000203E
    // (set) Token: 0x06000074 RID: 116 RVA: 0x00003046 File Offset: 0x00002046
    public ArgumentType PassBy { get; private set; }

    // Token: 0x1700000F RID: 15
    // (get) Token: 0x06000075 RID: 117 RVA: 0x0000304F File Offset: 0x0000204F
    // (set) Token: 0x06000076 RID: 118 RVA: 0x00003057 File Offset: 0x00002057
    public int MaxArrayLength { get; private set; }

    // Token: 0x06000077 RID: 119 RVA: 0x00003060 File Offset: 0x00002060
    public ulong GetArgumentValue(ulong bufferAddress)
    {
        switch (this.PassBy)
        {
            case ArgumentType.ByValue:
            case ArgumentType.ByRef:
            case ArgumentType.Out:
                if (this.MaxArrayLength == 0)
                {
                    return 0UL;
                }
                return bufferAddress;
            default:
                return 0UL;
        }
    }

    // Token: 0x06000078 RID: 120 RVA: 0x00003097 File Offset: 0x00002097
    public int GetRequiredBufferSize()
    {
        return this.MaxArrayLength * this._elementSize;
    }

    // Token: 0x06000079 RID: 121 RVA: 0x000030A8 File Offset: 0x000020A8
    public byte[] PackBufferData()
    {
        byte[] array = null;
        if (this.MaxArrayLength > 0)
        {
            int num = 0;
            array = new byte[this._elementSize * this.MaxArrayLength];
            for (int i = 0; i < this.MaxArrayLength; i++)
            {
                MarshalingUtils.PushBufferData(this._arrayElementData[i].Info.PackBufferData(), array, ref num);
            }
        }
        return array;
    }

    // Token: 0x0600007A RID: 122 RVA: 0x00003108 File Offset: 0x00002108
    public void UnpackBufferData(byte[] data)
    {
        if (this.PassBy == ArgumentType.ByRef || this.PassBy == ArgumentType.Out)
        {
            for (int i = 0; i < this.MaxArrayLength; i++)
            {
                byte[] array = new byte[this._elementSize];
                Array.Copy(data, i * this._elementSize, array, 0, this._elementSize);
                this._arrayElementData[i].Info.UnpackBufferData(array);
            }
            if (this._referenceSize == 0)
            {
                this.UpdateArrayValue();
            }
        }
    }

    // Token: 0x0600007B RID: 123 RVA: 0x0000317F File Offset: 0x0000217F
    public bool IsFloatingPointValue()
    {
        return false;
    }

    // Token: 0x0600007C RID: 124 RVA: 0x00003182 File Offset: 0x00002182
    public override string ToString()
    {
        return string.Format("[XDRPC]{0}: {1}", typeof(T).Name, this.Value);
    }

    // Token: 0x0600007D RID: 125 RVA: 0x000031A8 File Offset: 0x000021A8
    public int GetArgumentCount()
    {
        return 1;
    }

    // Token: 0x0600007E RID: 126 RVA: 0x000031AB File Offset: 0x000021AB
    public ulong[] GetArgumentValues(ulong bufferAddress)
    {
        return null;
    }

    // Token: 0x0600007F RID: 127 RVA: 0x000031B0 File Offset: 0x000021B0
    private void ConstructArgumentInfoArray()
    {
        Type elementType = typeof(T).GetElementType();
        Array array = this.Value as Array;
        this._arrayElementData = new XDRPCArrayArgumentInfo<T>.ArrayElementData[this.MaxArrayLength];
        int i = 0;
        int referenceSize = 0;
        while (i < this._arrayLength)
        {
            IXDRPCArgumentInfo info = XDRPCMarshaler.GenerateArgumentInfo(elementType, array.GetValue(i), ArgumentType.ByRef);
            this._arrayElementData[i] = this.GenerateArrayBufferData(info, ref referenceSize);
            i++;
        }
        while (i < this.MaxArrayLength)
        {
            IXDRPCArgumentInfo info2 = XDRPCMarshaler.GenerateArgumentInfo(elementType, Activator.CreateInstance(elementType), ArgumentType.ByRef);
            this._arrayElementData[i] = this.GenerateArrayBufferData(info2, ref referenceSize);
            i++;
        }
        if (this.MaxArrayLength > 0)
        {
            this._referenceSize = referenceSize;
            return;
        }
        this._referenceSize = 0;
    }

    // Token: 0x06000080 RID: 128 RVA: 0x00003280 File Offset: 0x00002280
    private XDRPCArrayArgumentInfo<T>.ArrayElementData GenerateArrayBufferData(IXDRPCArgumentInfo info, ref int referenceOffset)
    {
        XDRPCArrayArgumentInfo<T>.ArrayElementData result = default(XDRPCArrayArgumentInfo<T>.ArrayElementData);
        result.Info = info;
        result.ReferenceOffset = referenceOffset;
        result.ReferenceSize = info.GetRequiredReferenceSize();
        referenceOffset += result.ReferenceSize;
        XDRPCExecutionState.AlignBufferOffset(ref referenceOffset);
        return result;
    }

    // Token: 0x06000081 RID: 129 RVA: 0x000032C8 File Offset: 0x000022C8
    public void SetReferenceAddress(ulong bufferAddress)
    {
        for (int i = 0; i < this.MaxArrayLength; i++)
        {
            this._arrayElementData[i].Info.SetReferenceAddress(bufferAddress + (ulong)((long)this._arrayElementData[i].ReferenceOffset));
        }
    }

    // Token: 0x06000082 RID: 130 RVA: 0x00003310 File Offset: 0x00002310
    public int GetRequiredReferenceSize()
    {
        return this._referenceSize;
    }

    // Token: 0x06000083 RID: 131 RVA: 0x00003318 File Offset: 0x00002318
    public byte[] PackReferenceData()
    {
        byte[] array = null;
        if (this._referenceSize > 0)
        {
            array = new byte[this._referenceSize];
            for (int i = 0; i < this.MaxArrayLength; i++)
            {
                byte[] array2 = this._arrayElementData[i].Info.PackReferenceData();
                if (array2 != null)
                {
                    int referenceOffset = this._arrayElementData[i].ReferenceOffset;
                    MarshalingUtils.PushBufferData(array2, array, ref referenceOffset);
                }
            }
        }
        return array;
    }

    // Token: 0x06000084 RID: 132 RVA: 0x00003384 File Offset: 0x00002384
    public void UnpackReferenceData(byte[] data)
    {
        for (int i = 0; i < this.MaxArrayLength; i++)
        {
            if (this._arrayElementData[i].ReferenceSize > 0)
            {
                byte[] array = new byte[this._arrayElementData[i].ReferenceSize];
                Array.Copy(data, this._arrayElementData[i].ReferenceOffset, array, 0, array.Length);
                this._arrayElementData[i].Info.UnpackReferenceData(array);
            }
        }
        this.UpdateArrayValue();
    }

    // Token: 0x06000085 RID: 133 RVA: 0x00003408 File Offset: 0x00002408
    private void UpdateArrayValue()
    {
        if (this.MaxArrayLength > 0)
        {
            Type elementType = typeof(T).GetElementType();
            Array array = Array.CreateInstance(elementType, this.MaxArrayLength);
            for (int i = 0; i < this.MaxArrayLength; i++)
            {
                array.SetValue(XDRPCMarshaler.GetArgumentInfoValue(elementType, this._arrayElementData[i].Info), i);
            }
            this.Value = (array as T);
            this._arrayLength = array.Length;
        }
    }

    // Token: 0x04000014 RID: 20
    private int _elementSize;

    // Token: 0x04000015 RID: 21
    private int _arrayLength;

    // Token: 0x04000016 RID: 22
    private int _referenceSize;

    // Token: 0x04000017 RID: 23
    private XDRPCArrayArgumentInfo<T>.ArrayElementData[] _arrayElementData;

    // Token: 0x0200000D RID: 13
    private struct ArrayElementData
    {
        // Token: 0x0400001B RID: 27
        public int ReferenceOffset;

        // Token: 0x0400001C RID: 28
        public int ReferenceSize;

        // Token: 0x0400001D RID: 29
        public IXDRPCArgumentInfo Info;
    }
}
