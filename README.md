# Overview
Provide an ITextSerialization interface which serializes/deserializes an object to/from text and provides implementations for each of the native .NET serializers.

# Usage

**Interface:**
```csharp
namespace TextSerialization
{
    public interface ITextSerialization
    {
        string ToText<T>(T objectToSerialize);

        T FromText<T>(string text);
    }
}
```

**Implementations:**
* BinarySerialization: Uses System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
* DataContractSerialization: Uses System.Runtime.Serialization.DataContractSerializer (only .NET 3.0 and above)
* DataContractJsonSerialization: Uses System.Runtime.Serialization.DataContractJsonSerialization (only .NET 3.5 and above)
* NetDataContractSerialization: Uses System.Runtime.Serialization.NetDataContractSerialization (only .NET 3.0 and above)
* XmlSerialization: Uses System.Xml.Serialization.XmlSerializer
