namespace XDRPC;

// Token: 0x0200001E RID: 30
// (Invoke) Token: 0x060000DA RID: 218
public delegate void RPCEventHandler(object source);

// Token: 0x0200001F RID: 31
// (Invoke) Token: 0x060000DE RID: 222
public delegate void RPCEventHandler<T>(object source, T info);