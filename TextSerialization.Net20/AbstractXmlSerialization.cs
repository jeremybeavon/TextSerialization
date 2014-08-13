using System.IO;
using System.Text;
using System.Xml;

namespace TextSerialization
{
    public abstract class AbstractXmlSerialization : ITextSerialization
    {
        public Optional<XmlReaderSettings> XmlReaderSettings { get; set; }

        public Optional<XmlParserContext> XmlParserContext { get; set; }

        public Optional<XmlWriterSettings> XmlWriterSettings { get; set; }

        public T FromText<T>(string text)
        {
            using (TextReader textReader = new StringReader(text))
            {
                using (XmlReader xmlReader = CreateXmlReader(textReader))
                {
                    return FromText<T>(xmlReader);
                }
            }
        }

        public string ToText<T>(T objectToSerialize)
        {
            StringBuilder text = new StringBuilder();
            using (XmlWriter xmlWriter = CreateXmlWriter(text))
            {
                ToText(xmlWriter, objectToSerialize);
            }

            return text.ToString();
        }

        protected abstract T FromText<T>(XmlReader xmlReader);

        protected abstract void ToText<T>(XmlWriter xmlWriter, T objectToSerialize);

        private XmlReader CreateXmlReader(TextReader textReader)
        {
            if (XmlReaderSettings != null && XmlParserContext != null)
            {
                return XmlReader.Create(textReader, XmlReaderSettings.Value, XmlParserContext.Value);
            }

            if (XmlReaderSettings != null)
            {
                return XmlReader.Create(textReader, XmlReaderSettings.Value);
            }

            return XmlReader.Create(textReader);
        }

        private XmlWriter CreateXmlWriter(StringBuilder text)
        {
            if (XmlWriterSettings != null)
            {
                return XmlWriter.Create(text, XmlWriterSettings.Value);
            }

            return XmlWriter.Create(text);
        }
    }
}
