using System.Runtime.Serialization;

namespace TextSerialization
{
    public interface ITextSerialization
    {
        string ToText<T>(T objectToSerialize);

        T FromText<T>(string text);
    }
}
