using System.Text.Json;

namespace MessageContracts
{
    public class MessageContractSerializer
    {
        public static string Serialize<TMessageContractType>(TMessageContractType messageContract)
            where TMessageContractType : IMessageContract
        {
            return JsonSerializer.Serialize(messageContract);
        }

        public static TMessageContractType Deserialize<TMessageContractType>(string messageContract)
            where TMessageContractType : IMessageContract
        {
            return JsonSerializer.Deserialize<TMessageContractType>(messageContract)!;
        }
    }
}
