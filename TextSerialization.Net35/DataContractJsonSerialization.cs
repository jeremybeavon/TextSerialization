using System.Runtime.Serialization.Json;

namespace TextSerialization.Net35
{
    public sealed class DataContractJsonSerialization : AbstractXmlObjectSerialization
    {
        public DataContractJsonSerialization()
            : base(type => new DataContractJsonSerializer(type))
        {
        }

        public DataContractJsonSerialization(DataContractJsonSerializerFactoryMethod serializerFactory)
            : base(type => serializerFactory(type))
        {
        }
    }
}
