namespace XDRPC;

public interface IXDRPCArgumentInfo
{
    // Token: 0x06000010 RID: 16
    ulong GetArgumentValue(ulong bufferAddress);

    // Token: 0x06000011 RID: 17
    ulong[] GetArgumentValues(ulong bufferAddress);

    // Token: 0x06000012 RID: 18
    void SetReferenceAddress(ulong bufferAddress);

    // Token: 0x06000013 RID: 19
    int GetArgumentCount();

    // Token: 0x06000014 RID: 20
    int GetRequiredBufferSize();

    // Token: 0x06000015 RID: 21
    int GetRequiredReferenceSize();

    // Token: 0x06000016 RID: 22
    byte[] PackBufferData();

    // Token: 0x06000017 RID: 23
    byte[] PackReferenceData();

    // Token: 0x06000018 RID: 24
    void UnpackBufferData(byte[] data);

    // Token: 0x06000019 RID: 25
    void UnpackReferenceData(byte[] data);

    // Token: 0x0600001A RID: 26
    bool IsFloatingPointValue();

    // Token: 0x17000002 RID: 2
    // (get) Token: 0x0600001B RID: 27
    ArgumentType PassBy { get; }
}

