using System;
using System.Runtime.Serialization.Json;

namespace TextSerialization.Net35
{
    public delegate DataContractJsonSerializer DataContractJsonSerializerFactoryMethod(Type type);
}
