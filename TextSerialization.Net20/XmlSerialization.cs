using System;
using System.Xml;
using System.Xml.Serialization;

namespace TextSerialization
{
    public sealed class XmlSerialization : AbstractXmlSerialization
    {
        private readonly XmlSerializerFactoryMethod serializerFactory;

        public XmlSerialization()
            : this(type => new XmlSerializer(type))
        {
        }

        public XmlSerialization(XmlSerializerFactoryMethod serializerFactory)
        {
            this.serializerFactory = serializerFactory;
        }

        public Optional<string> DeserializationEncodingStyle { get; set; }

        public Optional<string> SerializationEncodingStyle { get; set; }

        public XmlDeserializationEvents? XmlDeserializationEvents { get; set; }

        public Optional<XmlSerializerNamespaces> XmlSerializerNamespaces { get; set; }

        public Optional<string> Id { get; set; }

        protected override T FromText<T>(XmlReader xmlReader)
        {
            XmlSerializer serializer = serializerFactory(typeof(T));
            if (DeserializationEncodingStyle != null && XmlDeserializationEvents != null)
            {
                return (T)serializer.Deserialize(
                    xmlReader,
                    DeserializationEncodingStyle.Value,
                    XmlDeserializationEvents.Value);
            }

            if (DeserializationEncodingStyle != null)
            {
                return (T)serializer.Deserialize(xmlReader, DeserializationEncodingStyle.Value);
            }

            if (XmlDeserializationEvents != null)
            {
                return (T)serializer.Deserialize(xmlReader, XmlDeserializationEvents.Value);
            }

            return (T)serializer.Deserialize(xmlReader);
        }

        protected override void ToText<T>(XmlWriter xmlWriter, T objectToSerialize)
        {
            XmlSerializer serializer = serializerFactory(typeof(T));
            if (Id != null)
            {
                Assert(XmlSerializerNamespaces != null, "Expected XmlSerializerNamespaces");
                Assert(SerializationEncodingStyle != null, "Expected SerializationEncodingStyle");
                serializer.Serialize(
                    xmlWriter,
                    objectToSerialize,
                    XmlSerializerNamespaces.Value,
                    SerializationEncodingStyle.Value,
                    Id.Value);
            }
            else if (SerializationEncodingStyle != null)
            {
                Assert(XmlSerializerNamespaces != null, "Expected XmlSerializerNamespaces");
                serializer.Serialize(
                    xmlWriter,
                    objectToSerialize,
                    XmlSerializerNamespaces.Value,
                    SerializationEncodingStyle.Value);
            }
            else if (XmlSerializerNamespaces != null)
            {
                serializer.Serialize(xmlWriter, objectToSerialize, XmlSerializerNamespaces.Value);
            }
            else
            {
                serializer.Serialize(xmlWriter, objectToSerialize);
            }
        }

        private static void Assert(bool condition, string message)
        {
            if (!condition)
            {
                throw new InvalidOperationException(message);
            }
        }
    }
}
