using System;
using System.Runtime.Serialization;

namespace TextSerialization
{
    public delegate XmlObjectSerializer XmlObjectSerializerFactoryMethod(Type type);
}
