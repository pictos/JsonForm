using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.FormFlow.Json;
using Newtonsoft.Json.Linq;

namespace JsonForm
{
    public class Form
    {
        public static IForm<JObject> BuildJsonForm()
        {
            var esquema = JObject.Parse(MessagesController.json);
            return new FormBuilderJson(esquema)
              .AddRemainingFields()
              //.OnCompletion(async (context, order) =>
              //{
              //    var fim =  order;
              //    var prop = fim.Properties();

              //    foreach (var teste in prop)
              //    {
              //        await context.PostAsync($"As propriedades: {teste}"); //Retorna propriedade e valor
              //        await context.PostAsync(teste.Value.ToString()); // Retorna apenas o valor
              //    }
              //    var item = fim.First;
              //    //item.Values("FootLong");
              //    await context.PostAsync($"Do... {item.ToString()}");
              //})
              .Build();
        }
    }
}