using System.Runtime.Serialization;

namespace TextSerialization
{
    public sealed class NetDataContractSerialization : AbstractXmlObjectSerialization
    {
        public NetDataContractSerialization()
            : this(() => new NetDataContractSerializer())
        {
        }

        public NetDataContractSerialization(NetDataContractSerializerFactoryMethod serializerFactory)
            : base(type => serializerFactory())
        {

        }
    }
}
