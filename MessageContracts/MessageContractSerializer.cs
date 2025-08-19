using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
