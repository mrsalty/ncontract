using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebApi.Domain
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PizzaType
    {
        Margherita,
        Marinara,
        QuattroStagioni,
        Napoli,
        Diavola //NOT PEPPERONI!
    }
}