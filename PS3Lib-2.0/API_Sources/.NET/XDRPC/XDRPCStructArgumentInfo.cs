// Token: 0x0200000E RID: 14
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace XDRPC;

public class XDRPCStructArgumentInfo<T> : IXDRPCArgumentInfo where T : struct
{
    // Token: 0x06000086 RID: 134 RVA: 0x00003487 File Offset: 0x00002487
    public XDRPCStructArgumentInfo(T value) : this(value, ArgumentType.ByRef)
    {
    }

    // Token: 0x06000087 RID: 135 RVA: 0x00003494 File Offset: 0x00002494
    public XDRPCStructArgumentInfo(T value, ArgumentType t)
    {
        this.Value = value;
        this.PassBy = t;
        this.PrimitiveSize = this.PopulateStructBufferRecursive(typeof(T), this.Value, this._structBufferDataList, 0, string.Empty);
        this._structBufferSize = this.GetCurrentOffset(this._structBufferDataList, 1);
        this._referenceBufferSize = this.GetCurrentOffset(this._referenceBufferDataList, 8);
    }

    // Token: 0x17000010 RID: 16
    // (get) Token: 0x06000088 RID: 136 RVA: 0x0000351E File Offset: 0x0000251E
    // (set) Token: 0x06000089 RID: 137 RVA: 0x00003526 File Offset: 0x00002526
    public T Value { get; private set; }

    // Token: 0x17000011 RID: 17
    // (get) Token: 0x0600008A RID: 138 RVA: 0x0000352F File Offset: 0x0000252F
    // (set) Token: 0x0600008B RID: 139 RVA: 0x00003537 File Offset: 0x00002537
    public ArgumentType PassBy { get; private set; }

    // Token: 0x17000012 RID: 18
    // (get) Token: 0x0600008C RID: 140 RVA: 0x00003540 File Offset: 0x00002540
    public int Size
    {
        get
        {
            return this._structBufferSize;
        }
    }

    // Token: 0x17000013 RID: 19
    // (get) Token: 0x0600008D RID: 141 RVA: 0x00003548 File Offset: 0x00002548
    // (set) Token: 0x0600008E RID: 142 RVA: 0x00003550 File Offset: 0x00002550
    public int PrimitiveSize { get; private set; }

    // Token: 0x0600008F RID: 143 RVA: 0x00003559 File Offset: 0x00002559
    public override string ToString()
    {
        return string.Format("[XDRPC]{0}: {1}", typeof(T).Name, this.Value);
    }

    // Token: 0x06000090 RID: 144 RVA: 0x00003580 File Offset: 0x00002580
    public ulong GetArgumentValue(ulong bufferAddress)
    {
        switch (this.PassBy)
        {
            case ArgumentType.ByValue:
                return this.GetArgumentValues(0UL)[0];
            case ArgumentType.ByRef:
            case ArgumentType.Out:
                return bufferAddress;
            default:
                return 0UL;
        }
    }

    // Token: 0x06000091 RID: 145 RVA: 0x000035B8 File Offset: 0x000025B8
    public ulong[] GetArgumentValues(ulong bufferAddress)
    {
        if (this.PassBy == ArgumentType.ByValue)
        {
            int argumentCount = this.GetArgumentCount();
            byte[] array = this.PackBufferData(this._structBufferDataList, this._structBufferSize);
            ulong[] array2 = new ulong[argumentCount];
            for (int i = 0; i < argumentCount; i++)
            {
                byte[] array3 = new byte[8];
                int num = i * 8;
                int num2 = array.Length - num;
                if (num2 > array3.Length)
                {
                    num2 = array3.Length;
                }
                Array.Copy(array, num, array3, 0, num2);
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(array3);
                }
                array2[i] = BitConverter.ToUInt64(array3, 0);
            }
            return array2;
        }
        return null;
    }

    // Token: 0x06000092 RID: 146 RVA: 0x00003646 File Offset: 0x00002646
    public int GetArgumentCount()
    {
        if (this.PassBy == ArgumentType.ByValue)
        {
            return (this._structBufferSize + 8 - 1) / 8;
        }
        return 1;
    }

    // Token: 0x06000093 RID: 147 RVA: 0x00003660 File Offset: 0x00002660
    public int GetRequiredBufferSize()
    {
        switch (this.PassBy)
        {
            case ArgumentType.ByValue:
                return 0;
            case ArgumentType.ByRef:
            case ArgumentType.Out:
                return this._structBufferSize;
            default:
                return 0;
        }
    }

    // Token: 0x06000094 RID: 148 RVA: 0x00003692 File Offset: 0x00002692
    public byte[] PackBufferData()
    {
        if (this.PassBy == ArgumentType.ByRef || this.PassBy == ArgumentType.Out)
        {
            return this.PackBufferData(this._structBufferDataList, this._structBufferSize);
        }
        return null;
    }

    // Token: 0x06000095 RID: 149 RVA: 0x000036BC File Offset: 0x000026BC
    public void UnpackBufferData(byte[] incomingData)
    {
        if (this.PassBy == ArgumentType.ByRef || this.PassBy == ArgumentType.Out)
        {
            this.UnpackBufferData(this._structBufferDataList, incomingData);
            if (this._referenceBufferSize == 0)
            {
                List<XDRPCStructArgumentInfo<T>.StructBufferData>.Enumerator enumerator = this._structBufferDataList.GetEnumerator();
                enumerator.MoveNext();
                this.Value = (T)((object)this.UnpackValuesRecursive(typeof(T), this.Value, ref enumerator));
            }
        }
    }

    // Token: 0x06000096 RID: 150 RVA: 0x0000372C File Offset: 0x0000272C
    public bool IsFloatingPointValue()
    {
        return false;
    }

    // Token: 0x06000097 RID: 151 RVA: 0x0000372F File Offset: 0x0000272F
    public void SetReferenceAddress(ulong bufferAddress)
    {
        this.PopulateReferencePointers(this._structBufferDataList, bufferAddress);
        this.PopulateReferencePointers(this._referenceBufferDataList, bufferAddress);
    }

    // Token: 0x06000098 RID: 152 RVA: 0x0000374B File Offset: 0x0000274B
    public int GetRequiredReferenceSize()
    {
        return this._referenceBufferSize;
    }

    // Token: 0x06000099 RID: 153 RVA: 0x00003753 File Offset: 0x00002753
    public byte[] PackReferenceData()
    {
        return this.PackBufferData(this._referenceBufferDataList, this._referenceBufferSize);
    }

    // Token: 0x0600009A RID: 154 RVA: 0x00003768 File Offset: 0x00002768
    public void UnpackReferenceData(byte[] incomingData)
    {
        this.UnpackBufferData(this._referenceBufferDataList, incomingData);
        List<XDRPCStructArgumentInfo<T>.StructBufferData>.Enumerator enumerator = this._structBufferDataList.GetEnumerator();
        enumerator.MoveNext();
        this.Value = (T)((object)this.UnpackValuesRecursive(typeof(T), this.Value, ref enumerator));
    }

    // Token: 0x0600009B RID: 155 RVA: 0x000037C0 File Offset: 0x000027C0
    private byte[] PackBufferData(List<XDRPCStructArgumentInfo<T>.StructBufferData> bufferDataList, int bufferSize)
    {
        byte[] array = null;
        if (bufferSize > 0)
        {
            array = new byte[bufferSize];
            foreach (XDRPCStructArgumentInfo<T>.StructBufferData structBufferData in bufferDataList)
            {
                if (!structBufferData.Ignore)
                {
                    byte[] array2 = structBufferData.Info.PackBufferData();
                    int length = (array2.Length < structBufferData.BufferSize) ? array2.Length : structBufferData.BufferSize;
                    Array.Copy(array2, 0, array, structBufferData.BufferOffset, length);
                }
            }
        }
        return array;
    }

    // Token: 0x0600009C RID: 156 RVA: 0x00003858 File Offset: 0x00002858
    private void UnpackBufferData(List<XDRPCStructArgumentInfo<T>.StructBufferData> bufferDataList, byte[] incomingData)
    {
        foreach (XDRPCStructArgumentInfo<T>.StructBufferData structBufferData in bufferDataList)
        {
            byte[] array = new byte[structBufferData.BufferSize];
            Array.Copy(incomingData, structBufferData.BufferOffset, array, 0, structBufferData.BufferSize);
            structBufferData.Info.UnpackBufferData(array);
        }
    }

    // Token: 0x0600009D RID: 157 RVA: 0x000038D0 File Offset: 0x000028D0
    private int PopulateStructBufferRecursive(Type structType, object structToMarshal, List<XDRPCStructArgumentInfo<T>.StructBufferData> bufferDataList, int depth, string unionWritingField)
    {
        if (depth > 3)
        {
            throw new XDRPCInvalidTypeException(structType, string.Format("Struct of type {0} has too many nested structs (more than {1} deep) which is not supported.", typeof(T).Name, 3));
        }
        if (!XDRPCStructArgumentInfo<T>.builtInStructAllowlist.Contains(structType))
        {
            object[] customAttributes = structType.GetCustomAttributes(typeof(XDRPCStructAttribute), false);
            if (customAttributes == null || customAttributes.Length == 0)
            {
                throw new XDRPCInvalidTypeException(structType, string.Format("Struct of type {0} doesn't have the XDRPCStruct attribute.", structType.Name));
            }
        }
        FieldInfo[] fields = structType.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        int packAttribute = (structType.StructLayoutAttribute.Value == LayoutKind.Sequential) ? 8 : structType.StructLayoutAttribute.Pack;
        int num = 0;
        int num2 = 0;
        bool flag = false;
        foreach (FieldInfo fieldInfo in fields)
        {
            Type fieldType = fieldInfo.FieldType;
            MarshalAsAttribute marshalAsAttribute = null;
            XDRPCStructArgumentInfo<T>.StructBufferData structBufferData = default(XDRPCStructArgumentInfo<T>.StructBufferData);
            object[] customAttributes2 = fieldInfo.GetCustomAttributes(typeof(MarshalAsAttribute), false);
            if (customAttributes2 != null && customAttributes2.Length != 0)
            {
                marshalAsAttribute = (MarshalAsAttribute)customAttributes2[0];
            }
            if (fieldType == typeof(string) || fieldType.IsArray)
            {
                if (marshalAsAttribute == null)
                {
                    throw new XDRPCInvalidTypeException(fieldType, string.Format("Field {0} of type {1} in struct type {2} doesn't have the required MarshalAsAttribute.", fieldInfo.Name, fieldType.Name, structType.Name));
                }
                if (marshalAsAttribute.Value != UnmanagedType.ByValArray && marshalAsAttribute.Value != UnmanagedType.LPArray)
                {
                    throw new XDRPCInvalidTypeException(fieldType, string.Format("Field {0} of type {1} in struct type {2} with its MarshalAs attribute not using the required UnmanagedType.ByValArray or UnmanagedType.LPArray.", fieldInfo.Name, fieldType.Name, structType.Name));
                }
                bool flag2 = marshalAsAttribute.Value == UnmanagedType.LPArray;
                int num3 = marshalAsAttribute.SizeConst;
                int num5;
                int num6;
                if (fieldType.IsArray)
                {
                    if (fieldType.GetArrayRank() > 1)
                    {
                        throw new XDRPCInvalidTypeException(fieldType, string.Format("Field {0} in struct type {1} is a multidimensional array which is not supported by XDRPCStructArgumentInfo.", fieldInfo.Name, structType.Name));
                    }
                    Type elementType = fieldType.GetElementType();
                    if (!XDRPCMarshaler.IsValidValueType(elementType))
                    {
                        throw new XDRPCInvalidTypeException(fieldType, string.Format("Field {0} in struct type {1} is an array of type {2} which is not supported by XDRPCStructArgumentInfo.", fieldInfo.Name, structType.Name, elementType.Name));
                    }
                    int num4 = MarshalingUtils.SizeOf(elementType);
                    if (num4 == 0)
                    {
                        throw new XDRPCInvalidTypeException(fieldType, string.Format("Field {0} in struct type {1} is an array of type {2} which is not supported by XDRPCStructArgumentInfo.", fieldInfo.Name, structType.Name, elementType.Name));
                    }
                    num5 = num4;
                    if (XDRPCMarshaler.IsValidStructType(elementType))
                    {
                        num5 = MarshalingUtils.AlignmentOf(elementType);
                    }
                    Array array2 = (Array)fieldInfo.GetValue(structToMarshal);
                    if (flag2)
                    {
                        if (array2 != null && num3 < array2.Length)
                        {
                            num3 = array2.Length;
                        }
                    }
                    else if (num3 == 0)
                    {
                        throw new XDRPCInvalidTypeException(fieldType, string.Format("Field {0} in struct type {1} is an array being passed by value that doesn't have the required SizeConst part of the attribute defined.", fieldInfo.Name, structType.Name));
                    }
                    num6 = num4 * num3;
                    structBufferData.Info = XDRPCMarshaler.GenerateArgumentInfo(fieldType, array2, ArgumentType.ByRef, num3);
                }
                else
                {
                    if (!XDRPCStructArgumentInfo<T>.EncodingMap.ContainsKey(marshalAsAttribute.ArraySubType))
                    {
                        throw new XDRPCInvalidTypeException(fieldType, string.Format("Field {0} in struct type {1} is a string with a MarshalAs attribute without a string UnmanagedType for its ArraySubType.", fieldInfo.Name, structType.Name));
                    }
                    Encoding encoding = XDRPCStructArgumentInfo<T>.EncodingMap[marshalAsAttribute.ArraySubType];
                    int num4 = (encoding == Encoding.Unicode) ? 2 : 1;
                    num5 = num4;
                    num6 = num3 * num4;
                    string text = (string)fieldInfo.GetValue(structToMarshal);
                    if (flag2)
                    {
                        int num7 = 0;
                        if (text != null)
                        {
                            num7 = encoding.GetByteCount(text + "\0");
                        }
                        if (num6 < num7)
                        {
                            num6 = num7;
                        }
                    }
                    else if (num6 == 0)
                    {
                        throw new XDRPCInvalidTypeException(fieldType, string.Format("Field {0} in struct type {1} is a string being passed by value that doesn't have the required SizeConst part of the attribute defined.", fieldInfo.Name, structType.Name));
                    }
                    structBufferData.Info = new XDRPCStringArgumentInfo(text, encoding, ArgumentType.ByRef, num6, CountType.Byte);
                }
                int requiredReferenceSize = structBufferData.Info.GetRequiredReferenceSize();
                if (requiredReferenceSize > 0)
                {
                    XDRPCStructArgumentInfo<T>.StructBufferData item = default(XDRPCStructArgumentInfo<T>.StructBufferData);
                    item.BufferOffset = this.GetCurrentOffset(this._referenceBufferDataList, 8);
                    item.BufferSize = requiredReferenceSize;
                    item.ReferenceIndex = -1;
                    item.Info = null;
                    structBufferData.ReferenceIndex = this._referenceBufferDataList.Count;
                    this._referenceBufferDataList.Add(item);
                }
                else
                {
                    structBufferData.ReferenceIndex = -1;
                }
                structBufferData.BufferSize = num6;
                if (flag2)
                {
                    if (num6 > 0)
                    {
                        XDRPCStructArgumentInfo<T>.StructBufferData item2 = structBufferData;
                        item2.BufferOffset = this.GetCurrentOffset(this._referenceBufferDataList, num5);
                        this.FillPointerBufferData(out structBufferData, bufferDataList, packAttribute);
                        this._referenceBufferDataList.Add(item2);
                    }
                    else
                    {
                        this.FillPointerBufferData(out structBufferData, bufferDataList, packAttribute);
                        structBufferData.Info = new XDRPCArgumentInfo<uint>(0U, ArgumentType.ByRef);
                        structBufferData.ReferenceIndex = -1;
                    }
                    num5 = structBufferData.BufferSize;
                }
                else
                {
                    int packAlignment = this.CalculatePackAlignment(packAttribute, num5);
                    structBufferData.BufferOffset = this.GetCurrentOffset(bufferDataList, packAlignment);
                }
                num = Math.Max(num, num5);
                num2 = Math.Max(num2, structBufferData.BufferSize);
                if (this.FillUnionBufferData(ref structBufferData, fieldInfo, unionWritingField))
                {
                    flag = true;
                }
                bufferDataList.Add(structBufferData);
            }
            else if (fieldType.IsPrimitive)
            {
                int num8 = MarshalingUtils.SizeOf(fieldType);
                if (num8 == 0)
                {
                    throw new XDRPCInvalidTypeException(fieldType, string.Format("Field {0} in struct type {1} is primitive type {2} which is not supported by XDRPCStructArgumentInfo.", fieldInfo.Name, structType.Name, fieldType.Name));
                }
                structBufferData.Info = XDRPCMarshaler.GenerateArgumentInfo(fieldType, fieldInfo.GetValue(structToMarshal), ArgumentType.ByRef);
                if (marshalAsAttribute != null && marshalAsAttribute.Value == UnmanagedType.LPStruct)
                {
                    XDRPCStructArgumentInfo<T>.StructBufferData item3 = default(XDRPCStructArgumentInfo<T>.StructBufferData);
                    item3.Info = structBufferData.Info;
                    item3.BufferOffset = this.GetCurrentOffset(this._referenceBufferDataList, num8);
                    item3.BufferSize = num8;
                    item3.ReferenceIndex = -1;
                    this.FillPointerBufferData(out structBufferData, bufferDataList, packAttribute);
                    this._referenceBufferDataList.Add(item3);
                }
                else
                {
                    int packAlignment2 = this.CalculatePackAlignment(packAttribute, num8);
                    structBufferData.BufferOffset = this.GetCurrentOffset(bufferDataList, packAlignment2);
                    structBufferData.BufferSize = num8;
                    structBufferData.ReferenceIndex = -1;
                }
                num = Math.Max(num, structBufferData.BufferSize);
                num2 = Math.Max(num2, structBufferData.BufferSize);
                if (this.FillUnionBufferData(ref structBufferData, fieldInfo, unionWritingField))
                {
                    flag = true;
                }
                bufferDataList.Add(structBufferData);
            }
            else
            {
                if (!fieldType.IsValueType)
                {
                    throw new XDRPCInvalidTypeException(fieldType, string.Format("Type {0} found in struct type {1} is not supported by XDRPCStructArgumentInfo.", fieldType.Name, structType.Name));
                }
                List<XDRPCStructArgumentInfo<T>.StructBufferData> list = new List<XDRPCStructArgumentInfo<T>.StructBufferData>();
                string unionWritingField2 = string.Empty;
                customAttributes2 = fieldInfo.GetCustomAttributes(typeof(XDRPCUnionAttribute), false);
                if (customAttributes2 != null && customAttributes2.Length != 0)
                {
                    unionWritingField2 = ((XDRPCUnionAttribute)customAttributes2[0]).Value;
                }
                int num9 = this.PopulateStructBufferRecursive(fieldType, fieldInfo.GetValue(structToMarshal), list, depth + 1, unionWritingField2);
                if (marshalAsAttribute != null && marshalAsAttribute.Value == UnmanagedType.LPStruct)
                {
                    this.FillPointerBufferData(out structBufferData, bufferDataList, packAttribute);
                    if (this.FillUnionBufferData(ref structBufferData, fieldInfo, unionWritingField))
                    {
                        flag = true;
                    }
                    bufferDataList.Add(structBufferData);
                    int currentOffset = this.GetCurrentOffset(this._referenceBufferDataList, 8);
                    for (int j = 0; j < list.Count; j++)
                    {
                        XDRPCStructArgumentInfo<T>.StructBufferData item4 = list[j];
                        item4.BufferOffset += currentOffset;
                        if (item4.NextOffset != 0)
                        {
                            item4.NextOffset += currentOffset;
                        }
                        this._referenceBufferDataList.Add(item4);
                    }
                    num = structBufferData.BufferSize;
                }
                else
                {
                    structBufferData.BufferOffset = this.GetCurrentOffset(bufferDataList, this.CalculatePackAlignment(packAttribute, num9));
                    if (this.FillUnionBufferData(ref structBufferData, fieldInfo, unionWritingField))
                    {
                        flag = true;
                    }
                    for (int k = 0; k < list.Count; k++)
                    {
                        XDRPCStructArgumentInfo<T>.StructBufferData item5 = list[k];
                        item5.BufferOffset += structBufferData.BufferOffset;
                        if (item5.NextOffset != 0)
                        {
                            structBufferData.BufferSize = item5.NextOffset;
                            item5.NextOffset += structBufferData.BufferOffset;
                        }
                        if (structBufferData.Ignore)
                        {
                            item5.Ignore = true;
                        }
                        bufferDataList.Add(item5);
                    }
                }
                num = Math.Max(num, num9);
                num2 = Math.Max(num2, structBufferData.BufferSize);
            }
        }
        if (!string.IsNullOrEmpty(unionWritingField))
        {
            if (!flag)
            {
                throw new XDRPCInvalidTypeException(structType, string.Format("Struct of type {0} has the XDRPCUnion attribute with the non-existant field {1} specified, please provide a correct field name.", structType.Name, unionWritingField));
            }
            this.SetNextOffset(bufferDataList, num2);
        }
        int num10 = this.CalculatePackAlignment(packAttribute, num);
        this.CalculateNextOffset(bufferDataList, num10);
        return num10;
    }

    // Token: 0x0600009E RID: 158 RVA: 0x000040E4 File Offset: 0x000030E4
    private int GetCurrentOffset(List<XDRPCStructArgumentInfo<T>.StructBufferData> bufferDataList, int packAlignment)
    {
        int num = 0;
        if (bufferDataList.Count > 0)
        {
            XDRPCStructArgumentInfo<T>.StructBufferData structBufferData = bufferDataList[bufferDataList.Count - 1];
            num = structBufferData.NextOffset;
            if (num == 0)
            {
                num = structBufferData.BufferOffset + structBufferData.BufferSize;
            }
            num = this.AlignOffset(num, packAlignment);
        }
        return num;
    }

    // Token: 0x0600009F RID: 159 RVA: 0x00004130 File Offset: 0x00003130
    private void SetNextOffset(List<XDRPCStructArgumentInfo<T>.StructBufferData> bufferDataList, int nextOffset)
    {
        if (bufferDataList.Count > 0)
        {
            int index = bufferDataList.Count - 1;
            XDRPCStructArgumentInfo<T>.StructBufferData value = bufferDataList[index];
            value.NextOffset = nextOffset;
            bufferDataList[index] = value;
        }
    }

    // Token: 0x060000A0 RID: 160 RVA: 0x00004168 File Offset: 0x00003168
    private void CalculateNextOffset(List<XDRPCStructArgumentInfo<T>.StructBufferData> bufferDataList, int packAlignment)
    {
        if (bufferDataList.Count > 0)
        {
            int index = bufferDataList.Count - 1;
            XDRPCStructArgumentInfo<T>.StructBufferData value = bufferDataList[index];
            int num = value.NextOffset;
            if (num == 0)
            {
                num = value.BufferOffset + value.BufferSize;
            }
            value.NextOffset = this.AlignOffset(num, packAlignment);
            bufferDataList[index] = value;
        }
    }

    // Token: 0x060000A1 RID: 161 RVA: 0x000041C1 File Offset: 0x000031C1
    private int CalculatePackAlignment(int packAttribute, int primitiveSize)
    {
        return Math.Min(packAttribute, primitiveSize);
    }

    // Token: 0x060000A2 RID: 162 RVA: 0x000041CC File Offset: 0x000031CC
    private int AlignOffset(int currOffset, int packAlignment)
    {
        int num = currOffset % packAlignment;
        if (num != 0)
        {
            currOffset += packAlignment - num;
        }
        return currOffset;
    }

    // Token: 0x060000A3 RID: 163 RVA: 0x000041E8 File Offset: 0x000031E8
    private void FillPointerBufferData(out XDRPCStructArgumentInfo<T>.StructBufferData data, List<XDRPCStructArgumentInfo<T>.StructBufferData> bufferDataList, int packAttribute)
    {
        data = default(XDRPCStructArgumentInfo<T>.StructBufferData);
        data.BufferSize = MarshalingUtils.SizeOf(typeof(uint));
        int packAlignment = this.CalculatePackAlignment(packAttribute, data.BufferSize);
        data.BufferOffset = this.GetCurrentOffset(bufferDataList, packAlignment);
        data.ReferenceIndex = this._referenceBufferDataList.Count;
        data.Info = null;
    }

    // Token: 0x060000A4 RID: 164 RVA: 0x00004248 File Offset: 0x00003248
    private bool FillUnionBufferData(ref XDRPCStructArgumentInfo<T>.StructBufferData data, FieldInfo fieldInfo, string unionWritingField)
    {
        bool result = false;
        if (!string.IsNullOrEmpty(unionWritingField))
        {
            data.BufferOffset = 0;
            if (unionWritingField != fieldInfo.Name)
            {
                data.Ignore = true;
            }
            else
            {
                result = true;
            }
        }
        return result;
    }

    // Token: 0x060000A5 RID: 165 RVA: 0x00004280 File Offset: 0x00003280
    private void PopulateReferencePointers(List<XDRPCStructArgumentInfo<T>.StructBufferData> bufferDataList, ulong bufferAddress)
    {
        for (int i = 0; i < bufferDataList.Count; i++)
        {
            XDRPCStructArgumentInfo<T>.StructBufferData value = bufferDataList[i];
            if (value.Info == null)
            {
                if (value.ReferenceIndex != -1)
                {
                    uint v = (uint)((long)this._referenceBufferDataList[value.ReferenceIndex].BufferOffset + (long)bufferAddress);
                    value.Info = new XDRPCArgumentInfo<uint>(v, ArgumentType.ByRef);
                    bufferDataList[i] = value;
                }
            }
            else if (value.Info.GetRequiredReferenceSize() > 0)
            {
                XDRPCStructArgumentInfo<T>.StructBufferData value2 = this._referenceBufferDataList[value.ReferenceIndex];
                ulong referenceAddress = (ulong)((long)value2.BufferOffset + (long)bufferAddress);
                value.Info.SetReferenceAddress(referenceAddress);
                value2.Info = new XDRPCArrayArgumentInfo<byte[]>(value.Info.PackReferenceData(), ArgumentType.ByRef);
                this._referenceBufferDataList[value.ReferenceIndex] = value2;
            }
        }
    }

    // Token: 0x060000A6 RID: 166 RVA: 0x00004360 File Offset: 0x00003360
    private object UnpackValuesRecursive(Type structType, object structToMarshal, ref List<XDRPCStructArgumentInfo<T>.StructBufferData>.Enumerator bufferEnum)
    {
        FieldInfo[] fields = structType.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            Type fieldType = fieldInfo.FieldType;
            XDRPCStructArgumentInfo<T>.StructBufferData structBufferData = bufferEnum.Current;
            if (fieldType == typeof(string) || fieldType.IsArray || fieldType.IsPrimitive)
            {
                if (structBufferData.ReferenceIndex != -1)
                {
                    if (structBufferData.Info.GetRequiredReferenceSize() == 0)
                    {
                        structBufferData = this._referenceBufferDataList[structBufferData.ReferenceIndex];
                    }
                    if (structBufferData.Info.GetRequiredReferenceSize() > 0)
                    {
                        XDRPCArrayArgumentInfo<byte[]> xdrpcarrayArgumentInfo = (XDRPCArrayArgumentInfo<byte[]>)this._referenceBufferDataList[structBufferData.ReferenceIndex].Info;
                        structBufferData.Info.UnpackReferenceData(xdrpcarrayArgumentInfo.Value);
                    }
                }
                object value = this.ValidateArgInfoNullValue(fieldType, structBufferData.Info);
                fieldInfo.SetValue(structToMarshal, value);
                bufferEnum.MoveNext();
            }
            else if (fieldType.IsValueType)
            {
                object value2;
                if (structBufferData.ReferenceIndex != -1)
                {
                    List<XDRPCStructArgumentInfo<T>.StructBufferData>.Enumerator enumerator = this._referenceBufferDataList.GetEnumerator();
                    do
                    {
                        enumerator.MoveNext();
                    }
                    while (enumerator.Current.Info != this._referenceBufferDataList[structBufferData.ReferenceIndex].Info);
                    value2 = this.UnpackValuesRecursive(fieldType, fieldInfo.GetValue(structToMarshal), ref enumerator);
                    bufferEnum.MoveNext();
                }
                else
                {
                    value2 = this.UnpackValuesRecursive(fieldType, fieldInfo.GetValue(structToMarshal), ref bufferEnum);
                }
                fieldInfo.SetValue(structToMarshal, value2);
            }
        }
        return structToMarshal;
    }

    // Token: 0x060000A7 RID: 167 RVA: 0x000044DC File Offset: 0x000034DC
    private object ValidateArgInfoNullValue(Type fieldType, XDRPCArgumentInfo argInfo)
    {
        if (argInfo.GetType().IsGenericType)
        {
            Type type = argInfo.GetType().GetGenericArguments()[0];
            if (!type.Equals(fieldType) && type.Equals(typeof(uint)) && ((XDRPCArgumentInfo<uint>)argInfo).Value == 0U)
            {
                return null;
            }
        }
        return XDRPCMarshaler.GetArgumentInfoValue(fieldType, argInfo);
    }

    // Token: 0x0400001E RID: 30
    private const int MaxRecursionDepth = 3;

    // Token: 0x0400001F RID: 31
    private const int DefaultPackingValue = 8;

    // Token: 0x04000020 RID: 32
    private const int NoReferenceIndex = -1;

    // Token: 0x04000021 RID: 33
    private const BindingFlags StructFieldFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

    // Token: 0x04000022 RID: 34
    private int _structBufferSize;

    // Token: 0x04000023 RID: 35
    private int _referenceBufferSize;

    // Token: 0x04000024 RID: 36
    private List<XDRPCStructArgumentInfo<T>.StructBufferData> _structBufferDataList = new List<XDRPCStructArgumentInfo<T>.StructBufferData>();

    // Token: 0x04000025 RID: 37
    private List<XDRPCStructArgumentInfo<T>.StructBufferData> _referenceBufferDataList = new List<XDRPCStructArgumentInfo<T>.StructBufferData>();

    // Token: 0x04000026 RID: 38
    private static Dictionary<UnmanagedType, Encoding> EncodingMap = new Dictionary<UnmanagedType, Encoding>
    {
        {
            UnmanagedType.LPStr,
            Encoding.ASCII
        },
        {
            UnmanagedType.LPWStr,
            Encoding.Unicode
        },
        {
            UnmanagedType.LPTStr,
            Encoding.UTF8
        }
    };

    // Token: 0x04000027 RID: 39
    private static Type[] builtInStructAllowlist = new Type[]
    {
        typeof(Guid)
    };

    // Token: 0x0200000F RID: 15
    private struct StructBufferData
    {
        // Token: 0x0400002B RID: 43
        public int BufferOffset;

        // Token: 0x0400002C RID: 44
        public int BufferSize;

        // Token: 0x0400002D RID: 45
        public int NextOffset;

        // Token: 0x0400002E RID: 46
        public XDRPCArgumentInfo Info;

        // Token: 0x0400002F RID: 47
        public int ReferenceIndex;

        // Token: 0x04000030 RID: 48
        public bool Ignore;
    }
}
