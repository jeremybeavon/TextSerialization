using System;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters.Binary;

namespace TextSerialization
{
    public sealed class BinarySerialization : ITextSerialization
    {
        private readonly BinarySerializerFactoryMethod serializerFactory;

        public BinarySerialization()
            : this(() => new BinaryFormatter())
        {
        }

        public BinarySerialization(BinarySerializerFactoryMethod serializerFactory)
        {
            this.serializerFactory = serializerFactory;
        }

        public int? StreamCapacity { get; set; }

        public Optional<Header[]> Headers { get; set; }

        public Optional<HeaderHandler> HeaderHandler { get; set; }

        public Optional<IMethodCallMessage> MethodCallMessage { get; set; }

        public T FromText<T>(string text)
        {
            BinaryFormatter serializer = serializerFactory();
            using (Stream stream = new MemoryStream(Convert.FromBase64String(text)))
            {
                if (MethodCallMessage != null)
                {
                    if (HeaderHandler == null)
                    {
                        throw new InvalidOperationException("Expected HeaderHandler");
                    }

                    return (T)serializer.DeserializeMethodResponse(stream, HeaderHandler.Value, MethodCallMessage.Value);
                }

                if (HeaderHandler != null)
                {
                    return (T)serializer.Deserialize(stream, HeaderHandler.Value);
                }

                return (T)serializer.Deserialize(stream);
            }
        }

        public string ToText<T>(T objectToSerialize)
        {
            BinaryFormatter serializer = serializerFactory();
            using (MemoryStream stream = CreateMemoryStream())
            {
                if (Headers == null)
                {
                    serializer.Serialize(stream, objectToSerialize);
                }
                else
                {
                    serializer.Serialize(stream, objectToSerialize, Headers.Value);
                }

                return Convert.ToBase64String(stream.ToArray());
            }
        }

        private MemoryStream CreateMemoryStream()
        {
            return StreamCapacity.HasValue ? new MemoryStream(StreamCapacity.Value) : new MemoryStream();
        }
    }
}
