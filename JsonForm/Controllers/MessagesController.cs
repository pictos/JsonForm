using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
using Newtonsoft.Json.Linq;

namespace JsonForm
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public static string json;
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                var Client = new HttpClient();
                
                string url = "https://gist.githubusercontent.com/pictos/70aa4fcc4fad5e48d28c933f42fd3a12/raw/d32457662a06fbff87625eb9e61bf7f89a909f69/teste.json";
                json = await Client.GetStringAsync(url);

                await Conversation.SendAsync(activity, JsonRootDialog);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }

        internal static IDialog<JObject> JsonRootDialog()
        {
            return Chain.From(() => FormDialog.FromForm(Form.BuildJsonForm))
                .Do(async (context, order) =>
                {
                    try
                    {
                        var fim = await order;
                        var prop = fim.Properties();

                        foreach (var teste in prop)
                        {
                            await context.PostAsync($"As propriedades: {teste}"); //Retorna propriedade e valor
                            await context.PostAsync(teste.Value.ToString()); // Retorna apenas o valor
                        }
                        //var item = fim.First;
                        ////item.Values("FootLong");
                        await context.PostAsync("Estou no Do...");
                    }
                    catch (FormCanceledException<JObject> e)
                    {

                        string resposta;
                        if (e.InnerException == null)
                            resposta = $"Você saiu no {e.Last}, talvez queira terminar o pedido depois";
                        else
                            resposta = "Desculpe, ocorreu algum problema interno. Por favor tente novamente";

                        await context.PostAsync(resposta);
                    }
                });


        }
    }
}