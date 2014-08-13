using System;
using System.Xml.Serialization;

namespace TextSerialization
{
    public delegate XmlSerializer XmlSerializerFactoryMethod(Type type);
}
