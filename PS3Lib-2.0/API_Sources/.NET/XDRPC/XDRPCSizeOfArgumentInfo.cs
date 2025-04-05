namespace XDRPC;

public class XDRPCSizeOfArgumentInfo : IXDRPCArgumentInfo
{
    // Token: 0x17000004 RID: 4
    // (get) Token: 0x0600002A RID: 42 RVA: 0x00002504 File Offset: 0x00001504
    // (set) Token: 0x0600002B RID: 43 RVA: 0x0000250C File Offset: 0x0000150C
    public IXDRPCArgumentInfo TargetArgument { get; set; }

    // Token: 0x0600002C RID: 44 RVA: 0x00002515 File Offset: 0x00001515
    public XDRPCSizeOfArgumentInfo()
    {
        this.TargetArgument = null;
    }

    // Token: 0x0600002D RID: 45 RVA: 0x00002524 File Offset: 0x00001524
    public XDRPCSizeOfArgumentInfo(IXDRPCArgumentInfo target)
    {
        this.TargetArgument = target;
    }

    // Token: 0x0600002E RID: 46 RVA: 0x00002533 File Offset: 0x00001533
    public ulong GetArgumentValue(ulong bufferAddress)
    {
        if (this.TargetArgument == null)
        {
            return 0UL;
        }
        return (ulong)((long)this.TargetArgument.GetRequiredBufferSize());
    }

    // Token: 0x0600002F RID: 47 RVA: 0x0000254C File Offset: 0x0000154C
    public int GetRequiredBufferSize()
    {
        return 0;
    }

    // Token: 0x06000030 RID: 48 RVA: 0x0000254F File Offset: 0x0000154F
    public byte[] PackBufferData()
    {
        return null;
    }

    // Token: 0x06000031 RID: 49 RVA: 0x00002552 File Offset: 0x00001552
    public void UnpackBufferData(byte[] data)
    {
    }

    // Token: 0x06000032 RID: 50 RVA: 0x00002554 File Offset: 0x00001554
    public override string ToString()
    {
        return string.Format("[XDRPC]SizeOf({0})", this.TargetArgument);
    }

    // Token: 0x06000033 RID: 51 RVA: 0x00002566 File Offset: 0x00001566
    public bool IsFloatingPointValue()
    {
        return false;
    }

    // Token: 0x06000034 RID: 52 RVA: 0x00002569 File Offset: 0x00001569
    public int GetArgumentCount()
    {
        return 1;
    }

    // Token: 0x06000035 RID: 53 RVA: 0x0000256C File Offset: 0x0000156C
    public ulong[] GetArgumentValues(ulong bufferAddress)
    {
        return new ulong[]
        {
            this.GetArgumentValue(bufferAddress)
        };
    }

    // Token: 0x17000005 RID: 5
    // (get) Token: 0x06000036 RID: 54 RVA: 0x0000258B File Offset: 0x0000158B
    public ArgumentType PassBy
    {
        get
        {
            return ArgumentType.ByValue;
        }
    }

    // Token: 0x06000037 RID: 55 RVA: 0x0000258E File Offset: 0x0000158E
    public void SetReferenceAddress(ulong bufferAddress)
    {
    }

    // Token: 0x06000038 RID: 56 RVA: 0x00002590 File Offset: 0x00001590
    public int GetRequiredReferenceSize()
    {
        return 0;
    }

    // Token: 0x06000039 RID: 57 RVA: 0x00002593 File Offset: 0x00001593
    public byte[] PackReferenceData()
    {
        return null;
    }

    // Token: 0x0600003A RID: 58 RVA: 0x00002596 File Offset: 0x00001596
    public void UnpackReferenceData(byte[] data)
    {
    }
}