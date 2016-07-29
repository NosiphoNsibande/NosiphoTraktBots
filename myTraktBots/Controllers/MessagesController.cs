using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using myTraktBots.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
//using traktDialog.Dialogs;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
namespace myTraktBots
{
    //[BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        //search for Movies 
        [ResponseType(typeof(void))]
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity != null)
            {
                // one of these will have an interface and process it
                string PlayedMovie = ""; string popmovie = ""; string popshow = ""; string movieW = ""; long movieWatchers = 0; long showWatchers = 0;
                long playedMoviYear = 0; long showyear = 0; long yearMovie = 0; int count = 1; long yearW = 0;
                long playedMovieW = 0;
                string[] counta = { "a.", "b.", "c.", "d.", "e.", "f.", "g.", "h.", "i.", "j.", "k.", "l.", "m.", "n.", "o.", "p.", "q.", "r.", "s.", "t.", "u.", "y.", "z." };
                switch (activity.GetActivityType())
                {
                    case ActivityTypes.Message:
                    await Conversation.SendAsync(activity, () => new traktDialog());
                    break;
                    case ActivityTypes.ConversationUpdate:
                    case ActivityTypes.ContactRelationUpdate:
                    case ActivityTypes.Typing:
                    case ActivityTypes.DeleteUserData:
                    default:
                        Trace.TraceError($"Unknown activity type ignored: {activity.GetActivityType()}");
                        break;

                }
            }
            return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
        }
    }
}