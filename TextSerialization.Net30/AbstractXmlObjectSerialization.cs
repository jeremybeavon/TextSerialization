using System.Runtime.Serialization;
using System.Xml;

namespace TextSerialization
{
    public abstract class AbstractXmlObjectSerialization : AbstractXmlSerialization
    {
        private readonly XmlObjectSerializerFactoryMethod serializerFactory;

        protected AbstractXmlObjectSerialization(XmlObjectSerializerFactoryMethod serializerFactory)
        {
            this.serializerFactory = serializerFactory;
        }

        public bool? VerifyObjectName { get; set; }

        protected override T FromText<T>(XmlReader xmlReader)
        {
            XmlObjectSerializer serializer = serializerFactory(typeof(T));
            return VerifyObjectName.HasValue ?
                (T)serializer.ReadObject(xmlReader, VerifyObjectName.Value) :
                (T)serializer.ReadObject(xmlReader);
        }

        protected override void ToText<T>(XmlWriter xmlWriter, T objectToSerialize)
        {
            serializerFactory(typeof(T)).WriteObject(xmlWriter, objectToSerialize);
        }
    }
}
