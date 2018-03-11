using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.FormFlow.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace JsonForm
{
    public class Form
    {
        public static IForm<JObject> BuildJsonForm()
        {
            var objeto = Assembly.GetExecutingAssembly();
                var esquema = JObject.Parse(MessagesController.json);
                return new FormBuilderJson(esquema)
                  .AddRemainingFields()
                  .Build();
        }
    }
}