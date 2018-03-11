using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace JsonForm.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            // return our reply to the user
            await context.PostAsync($"You sent {activity.Text} which was {length} characters");



            context.Wait(MessageReceivedAsync);

            //var client = new HttpClient();
            //string url = "https://apiconversaojson.azurewebsites.net/api/Values/Content/";
            ////var endpoint = " http://api.promasters.net.br/cotacao/v1/valores";
            //var retorno = await client.GetStringAsync(url).ConfigureAwait(false);
        }
    }
}