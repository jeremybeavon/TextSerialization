using System;
using System.Runtime.Serialization;

namespace TextSerialization
{
    public delegate DataContractSerializer DataContractSerializerFactoryMethod(Type type);
}
