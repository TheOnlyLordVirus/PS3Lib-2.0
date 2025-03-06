using PS3Lib2.Interfaces;

namespace PS3Lib2;

abstract class Api_Wrapper : IPlaystationApi
{
    //public static PlaystationApiSupportAttribute<Api_Wrapper> GetApiSupportAttribute() =>
    //    (PlaystationApiSupportAttribute<Api_Wrapper>)Attribute.GetCustomAttribute(typeof(Api_Wrapper), typeof(PlaystationApiSupportAttribute<Api_Wrapper>));
}
