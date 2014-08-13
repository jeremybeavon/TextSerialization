using System.Runtime.Serialization;

namespace TextSerialization
{
    public sealed class DataContractSerialization : AbstractXmlObjectSerialization
    {
        public DataContractSerialization()
            : base(type => new DataContractSerializer(type))
        {
        }

        public DataContractSerialization(DataContractSerializerFactoryMethod serializerFactory)
            : base(type => serializerFactory(type))
        {
        }
    }
}
