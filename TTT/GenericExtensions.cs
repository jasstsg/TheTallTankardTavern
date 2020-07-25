using Newtonsoft.Json;

namespace TTT
{
    public static class GenericExtensions
    {
        public static OutputT GenericTypeCast<InputT, OutputT>(this InputT Input)
        {
            return JsonConvert.DeserializeObject<OutputT>(JsonConvert.SerializeObject(Input));
        }
    }
}
