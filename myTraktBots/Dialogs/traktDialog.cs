using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
//using traktDialog.Dialogs;
using myTraktBots.model;

namespace myTraktBots.Dialogs
{
    [LuisModel("3de59934-106f-4fbe-a8e4-f4949462544d", "e686ad6ca03b4cf29d26288783935568")]
    [Serializable]
    public class traktDialog : LuisDialog<object>
    {
        public traktDialog()
        {

        }

        public traktDialog(ILuisService service) : base(service)
        {

        }
        string[] counta = { "a.", "b.", "c.", "d.", "e.", "f.", "g.", "h.", "i.", "j.", "k.", "l.", "m.", "n.", "o.", "p.", "q.", "r.", "s.", "t.", "u.", "y.", "z." };
        
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            long sYear = 0; long sW = 0; string ss = "";
            long tredYear = 0; long movieW = 0; string tredMovies = "";
            long reYear = 0; String RelaShow = ""; string slung="";
            int numSeason = 0; int count = 1;int counta1 = 1; int countaa2 = 1; int countaaa = 1; int countaaaa = 1;

            if (result.Query.ToLower().Contains("resert")||result.Query.ToLower().Contains("reset")||result.Query.ToLower().Contains("hi") || result.Query.ToLower().Contains("hellow") || result.Query.ToLower().Contains("hello"))
            {
                   string message = ($"####Hi :smile:!!,my name is traktBots and i can help you with the following infor" + $"{Environment.NewLine}{Environment.NewLine}" + $"#####a).**Popular Movies and tv Shows**" + $"{Environment.NewLine}{Environment.NewLine}" + "#####b). **Most Played/Watched Movies and tv shows**" + $"{Environment.NewLine}{Environment.NewLine}" +
                   "#####c).**Trending movies and tv shows**" + $"{Environment.NewLine}{Environment.NewLine}" + "#####d).**Box office Movies**" + $"{Environment.NewLine}{Environment.NewLine}" +
                   "#####e).**Search the movie or show by title e.g** **The Flash**" + $"{Environment.NewLine}{Environment.NewLine}" + "#####f)**.Get movie by movie released date e.g** **2016-01-01/7**  **movies**" + $"{Environment.NewLine}{Environment.NewLine}" +
                   "#####g).Resert"+  $"{Environment.NewLine}{Environment.NewLine}" + "#####h).Option 6 to go to trakt website for more information" + $"{Environment.NewLine}{Environment.NewLine}" + $"#####**NOTE:by Searching the  tv show by title,you can also get related shows!!! :+1:**");
                   await context.PostAsync(message);
                   context.Wait(MessageReceived);
            }
           //good bye
          else  if(result.Query.ToLower().Contains("bye")||result.Query.ToLower().Contains("goodbye"))
            {
                var ImagesUrl1 = new Uri("http://www.greetingsfromheart.com/images/bye_bye/bye_bye.gif");
                string message = ($"[![Thank you](" + ImagesUrl1 + ")](" + ImagesUrl1 + ")");
                await context.PostAsync(message);
                context.Wait(MessageReceived);

            }

            else
            {
               
                //searching for movie
                using (var client2 = new HttpClient { BaseAddress = new Uri("https://api.trakt.tv") })
                {
                    client2.DefaultRequestHeaders.Add("trakt-api-key", "80cf41a8390988e19f93394bb11894f27969267aad51b4978ffaaffb621bcce9");

                    var responseMovie = await client2.GetAsync("search/movie?query=" + result.Query.ToString());
                    var responseStringMovie = await responseMovie.Content.ReadAsStringAsync();
                    var resMovie = Newtonsoft.Json.JsonConvert.DeserializeObject<List<search>>(responseStringMovie);
                    if (resMovie.Count == 0)
                    {
                        //searching show
                        var response2 = await client2.GetAsync("search/show?query=" + result.Query.ToString());
                        var responseString2 = await response2.Content.ReadAsStringAsync();
                        var res2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<search2>>(responseString2);
                        if (res2.Count == 0)
                        {
                            string msg1 = "nothing found";
                            await context.PostAsync(msg1);
                            context.Wait(MessageReceived);
                        }
                        for (int i = 0; i < res2.Count; i++)
                        {
                            sYear = res2[i].show.year;
                            slung = res2[0].show.ids.slug;
                            string url2 = getshowImageUrl(res2[0].show.ids.imdb);
                            string urll = "https://www.themoviedb.org/tv/" + res2[0].show.ids.tmdb+"-"+slung;
                            if (i == 0)
                             {
                                ss += $"{Environment.NewLine }{ Environment.NewLine }#####" + counta1 + ".**" + res2[i].show.title.ToString() + "**\t\t\t\t\t" + $"**:year:**" + "\t\t\t\t\t**" + sYear + "\t\t\t\t\t" + $"**{Environment.NewLine }{ Environment.NewLine }" + ($"[![More Infor](" + url2 + ")](" + urll + ")");
                             }
                             ss += $"{Environment.NewLine }{ Environment.NewLine }#####" + counta1 + ".**" + res2[i].show.title.ToString() + "**\t\t\t\t\t" + $"**:year:**" + "\t\t\t\t\t**" + sYear + "\t\t\t\t\t" + $"**{Environment.NewLine }{ Environment.NewLine }";
                             counta1 = counta1 + 1;
                        }

                        //Related shows
                        var responsee = await client2.GetAsync("shows/" + slung + "/related");
                        var responseStringg = await responsee.Content.ReadAsStringAsync();
                        var ress = Newtonsoft.Json.JsonConvert.DeserializeObject<List<show>>(responseStringg);
                        if (ress.Count == 0)
                        {
                            string messager = "no related shows";
                            await context.PostAsync(messager);
                            context.Wait(MessageReceived);
                        }
                        for (int i = 0; i < ress.Count; i++)
                        {
                            reYear = ress[i].year;
                            string url3 = getshowImageUrl(ress[0].ids.imdb);
                            string slug = res2[0].show.ids.slug;
                            string urll ="https://www.themoviedb.org/tv/"+ress[0].ids.tmdb +"-"+slug;
                            if (i==0)
                            {
                                RelaShow += $"{Environment.NewLine }{ Environment.NewLine }#####" + countaaa + ".**" + ress[i].title.ToString() + "**\t\t\t\t\t" + $"**:year:**" + "\t\t\t\t\t**" + reYear + "\t\t\t\t\t" + $"**{Environment.NewLine }{ Environment.NewLine }" + ($"[![More Infor](" + url3 + ")](" + urll + ")");
                            }
                            RelaShow += $"{Environment.NewLine }{ Environment.NewLine }#####" + countaaa + ".**" + ress[i].title.ToString() + "**\t\t\t\t\t" + $"**:year:**" + "\t\t\t\t\t**" + reYear + "\t\t\t\t\t" + $"**{Environment.NewLine }{ Environment.NewLine }";
                            countaaa = countaaa + 1;

                        }
                        string message4 = $"####Result  for Shows:####" + ss + $"{Environment.NewLine }{ Environment.NewLine }" + $"####related shows:#####" + RelaShow;
                        await context.PostAsync(message4);
                        context.Wait(MessageReceived);
                    }
                    for (int i = 0;i<resMovie.Count; i++)
                    {
                        sYear = resMovie[i].movie.year;
                        string url2 = getImaggeUrl(resMovie[0].movie.ids.imdb);
                       if(i==0)
                       {
                            tredMovies += $"{Environment.NewLine }{ Environment.NewLine }#####" + counta1 + ".**" + resMovie[i].movie.title.ToString() + "**\t\t\t\t\t" + $"**:year:**" + "\t\t\t\t\t**" + sYear + "\t\t\t\t\t" + $"**{Environment.NewLine }{ Environment.NewLine }" + ($"[![View full Image](" + url2 + ")](" + url2 + ")"); ;
                       }
                        tredMovies += $"{Environment.NewLine }{ Environment.NewLine }#####" + counta1 + ".**" + resMovie[i].movie.title.ToString() + "**\t\t\t\t\t" + $"**:year:**" + "\t\t\t\t\t**" + sYear + "\t\t\t\t\t" + $"**{Environment.NewLine }{ Environment.NewLine }";
                        counta1 = counta1 + 1;
                    }
                    string messageMovie = $"####Result for Movies:####" + tredMovies + $"{Environment.NewLine }{ Environment.NewLine }";
                    await context.PostAsync(messageMovie);
                    context.Wait(MessageReceived);
                }


                //shows
                using (var client = new HttpClient { BaseAddress = new Uri("https://api.trakt.tv") })
                {
                    client.DefaultRequestHeaders.Add("trakt-api-key", "80cf41a8390988e19f93394bb11894f27969267aad51b4978ffaaffb621bcce9");
                    //search for Show
                    var response2 = await client.GetAsync("search/show?query=" + result.Query.ToString());
                    var responseString2 = await response2.Content.ReadAsStringAsync();
                    var res2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<search2>>(responseString2);

                    if (res2.Count == 0)
                    {
                        string messager = "no shows found";
                        await context.PostAsync(messager);
                        context.Wait(MessageReceived);
                    }
                    for (int i = 0; i < res2.Count; i++)
                    {
                        sYear = res2[i].show.year;
                        slung = res2[0].show.ids.slug;
                        string url2 = getshowImageUrl(res2[0].show.ids.imdb);
                        string urll ="https://www.themoviedb.org/tv/"+res2[0].show.ids.tmdb+"-"+slung;
                       if (i == 0)
                       {
                            ss += $"{Environment.NewLine }{ Environment.NewLine }#####" + countaa2 + ".**" + res2[i].show.title.ToString() + "**\t\t\t\t\t" + $"**:year:**" + "\t\t\t\t\t**" + sYear + "\t\t\t\t\t" + $"**{Environment.NewLine }{ Environment.NewLine }" + ($"[![More infor](" + url2 + ")](" + urll + ")");
                       }
                        ss += $"{Environment.NewLine }{ Environment.NewLine }#####" + countaa2 + ".**" + res2[i].show.title.ToString() + "**\t\t\t\t\t" + $"**:year:**" + "\t\t\t\t\t**" + sYear + "\t\t\t\t\t" + $"**{Environment.NewLine }{ Environment.NewLine }";
                        countaa2 = countaa2 + 1;
                    }

                    //Related shows
                    var responsee = await client.GetAsync("shows/" + slung + "/related");
                    var responseStringg = await responsee.Content.ReadAsStringAsync();
                    var ress = Newtonsoft.Json.JsonConvert.DeserializeObject<List<show>>(responseStringg);
                    if (ress.Count == 0)
                    {
                        string messager ="no related shows";
                        await context.PostAsync(messager);
                        context.Wait(MessageReceived);
                    }
                    for (int i = 0; i < ress.Count; i++)
                    {
                        reYear = ress[i].year;
                        string url3 = getshowImageUrl(ress[0].ids.imdb);
                        string slug = res2[0].show.ids.slug;
                        string urll ="https://www.themoviedb.org/tv/"+ress[0].ids.tmdb+"-"+slug;
                        if (i == 0)
                        {
                           RelaShow += $"{Environment.NewLine }{ Environment.NewLine }#####" + countaaa + ".**" + ress[i].title.ToString() + "**\t\t\t\t\t" + $"**:year:**" + "\t\t\t\t\t**" + reYear + "\t\t\t\t\t" + $"**{Environment.NewLine }{ Environment.NewLine }" + ($"[![More Infor]("+url3+")]("+urll+")");
                        }
                        RelaShow += $"{Environment.NewLine }{ Environment.NewLine }#####" + countaaa + ".**" + ress[i].title.ToString() + "**\t\t\t\t\t" + $"**:year:**" + "\t\t\t\t\t**" + reYear + "\t\t\t\t\t" + $"**{Environment.NewLine }{ Environment.NewLine }";
                        countaaa = countaaa + 1;
                    }
                    string message4 = $"####Result  for Shows:####" + ss + $"{Environment.NewLine }{ Environment.NewLine }" + $"####related shows:#####" + RelaShow;
                    await context.PostAsync(message4);
                    context.Wait(MessageReceived);

                }

            }

        }
        //greating
        [LuisIntent("intent.myTraktBots.hi")]
        public async Task greating(IDialogContext context, LuisResult result)
        {
            if (result.Query.ToLower().Contains("resert") || result.Query.ToLower().Contains("reset") || result.Query.ToLower().Contains("hi") || result.Query.ToLower().Contains("hellow") || result.Query.ToLower().Contains("hello"))
            {
                   string message = ($"####Hi :smile:!!,my name is traktBots and i can help you with the following infor" + $"{Environment.NewLine}{Environment.NewLine}" + $"#####a).**Popular Movies and tv Shows**" + $"{Environment.NewLine}{Environment.NewLine}" + "#####b). **Most Played/Watched Movies and tv shows**" + $"{Environment.NewLine}{Environment.NewLine}" +
                   "#####c).**Trending movies and tv shows**" + $"{Environment.NewLine}{Environment.NewLine}" + "#####d).**Box office Movies**" + $"{Environment.NewLine}{Environment.NewLine}" +
                   "#####e).**Search the movie or show by title e.g** **The Flash**" + $"{Environment.NewLine}{Environment.NewLine}" + "#####f)**.Get movie by movie released date e.g** **2016-01-01/7**  **movies**" + $"{Environment.NewLine}{Environment.NewLine}" +
                   "#####g).Resert"+ $"{Environment.NewLine}{Environment.NewLine}" + "#####h).Option 6 to go to trakt website for more information" + $"{Environment.NewLine}{Environment.NewLine}" + $"#####**NOTE:by Searching the  tv show by title,you can also get related shows!!! :+1:**");
                   await context.PostAsync(message);
                   context.Wait(MessageReceived);

            }

        }

        //Get all Images
        public String getImaggeUrl(String imdb)
        {
            String url = "";
            if (imdb == null)
            {
                url = "http://www.jakesonline.org/black.gif";
            }
            else
            {
                using (var client = new HttpClient { BaseAddress = new Uri("https://api.trakt.tv/") })
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation("trakt-api-version", "2");

                    client.DefaultRequestHeaders.TryAddWithoutValidation("trakt-api-key", "80cf41a8390988e19f93394bb11894f27969267aad51b4978ffaaffb621bcce9");
                    using (var response = client.GetAsync("search/imdb/" + imdb + "?extended=images").Result)
                    {
                        var responseString = response.Content.ReadAsStringAsync().Result;

                        var responseJSON = Newtonsoft.Json.JsonConvert.DeserializeObject<List<GetImages>>(responseString);
                        for (int i=0;i<responseJSON.Count;i++)
                        {
                            if (responseJSON[i].movie.images.fanart.full == null || responseJSON[i].movie.images.fanart.medium == null)
                            {
                                url = "http://www.jakesonline.org/black.gif";
                            }
                            else
                            {
                                url = responseJSON[0].movie.images.fanart.full.ToString();
                            }

                        }
                        
                    }
                }
            }
            return url;
        }

        //get show images
        public String getshowImageUrl(String imdb)
        {
            String url = "";
            if (imdb == null)
            {
                url = "http://www.jakesonline.org/black.gif";
            }
            else
            {
                using (var client = new HttpClient { BaseAddress = new Uri("https://api.trakt.tv/") })
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation("trakt-api-version", "2");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("trakt-api-key", "80cf41a8390988e19f93394bb11894f27969267aad51b4978ffaaffb621bcce9");
                    using (var response = client.GetAsync("search/imdb/" + imdb + "?extended=images").Result)
                    {
                        var responseString = response.Content.ReadAsStringAsync().Result;

                        var responseJSON = Newtonsoft.Json.JsonConvert.DeserializeObject<List<GetShowImages>>(responseString);
                        for (int i = 0; i < responseJSON.Count; i++)
                        {

                            if (responseJSON[i].show.images.fanart.full == null || responseJSON[i].show.images.fanart.medium == null)
                            {
                                url ="http://www.jakesonline.org/black.gif";
                            }
                            else
                            {
                                url = responseJSON[0].show.images.fanart.full.ToString();
                            }
                        }  
                    }
                }
            }
            return url;
        }


        //Trending Movies
        [LuisIntent("intent.myTraktBots.movie.Trending")]
        public async Task TrenMovie(IDialogContext context, LuisResult result)
        {
            long tredYear = 0; long movieW = 0; string tredMovies = "";string slug="" ;int count = 1;
            using (var client = new HttpClient { BaseAddress = new Uri("https://api.trakt.tv") })
            {
                client.DefaultRequestHeaders.Add("trakt-api-key", "80cf41a8390988e19f93394bb11894f27969267aad51b4978ffaaffb621bcce9");
                var response = await client.GetAsync("movies/trending");
                var responseString = await response.Content.ReadAsStringAsync();
                var res = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TrendingMovie>>(responseString);
                for (int i = 0; i < res.Count; i++)
                {
                    tredYear = res[i].movie.year;
                    movieW = res[i].watchers;

                    slug = res[i].movie.ids.slug;
                    string url = getImaggeUrl(res[i].movie.ids.imdb);
                    tredMovies+= $"{Environment.NewLine }{ Environment.NewLine }#####" + count + ".**" + res[i].movie.title.ToString() + "**\t\t\t\t\t" + $"**:Year:**" + "\t\t\t\t\t**" + tredYear+"\t\t\t\t\t" + "Watchers:" + "\t\t\t\t\t" + movieW + $"**{Environment.NewLine }{ Environment.NewLine }" + ($"[![View full Image](" + url + ")](" + url + ")");
                    count = count + 1;
                }

            }
            string message = $"####Trending  movies:####" + tredMovies + $"{Environment.NewLine }{ Environment.NewLine }>" + $"#####NOTE:if the movie you looking is not on this list,you can search it by typing it title!!!";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }



        //Trending Shows
        [LuisIntent("intent.myTraktBots.show.Trending")]
        public async Task TreShows(IDialogContext context, LuisResult result)
        {
            string tredShow = ""; long tredYear = 0; long showW = 0; int count = 1; string slug = "";
            using (var client = new HttpClient { BaseAddress = new Uri("https://api.trakt.tv") })
            {
                client.DefaultRequestHeaders.Add("trakt-api-key", "80cf41a8390988e19f93394bb11894f27969267aad51b4978ffaaffb621bcce9");
                var responseShow = await client.GetAsync("shows/trending");
                var responseStringShow = await responseShow.Content.ReadAsStringAsync();
                var resShow = Newtonsoft.Json.JsonConvert.DeserializeObject<List<showTrending>>(responseStringShow);
                for (int i = 0; i < resShow.Count; i++)
                {
                   
                    tredYear = resShow[i].show.year;
                    showW = resShow[i].watchers;
                    slug = resShow[i].show.ids.slug;

                    string url = getshowImageUrl(resShow[i].show.ids.imdb);
                    string urll = "https://www.themoviedb.org/tv/" + resShow[i].show.ids.tmdb+ "-"+slug;

                    tredShow += $"{Environment.NewLine }{ Environment.NewLine }#####" + count + ".**" + resShow[i].show.title.ToString() + "**\t\t\t\t\t" + $"**:Year:**" + "\t\t\t\t\t**" + tredYear+"**\t\t\t\t\t" + $"**:Watchers:**" + "\t\t\t\t\t**" + showW + $"**{Environment.NewLine }{ Environment.NewLine }" + ($"[![More infor](" + url + ")](" + urll + ")");
                    count = count + 1;
                }
            }
            string message = $"####Trending Tv shows:####" + tredShow + $"{Environment.NewLine }{ Environment.NewLine }>" + $"#####NOTE:if the series(show) you looking is not on this list,you can search it by typing it title!!!";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }




        //Popular Movies
        [LuisIntent("intent.myTraktBots.movie.popular")]
        public async Task popMovie(IDialogContext context, LuisResult result)
        {
            string popMovies = ""; long popYear = 0; int count = 1;
            using (var client = new HttpClient { BaseAddress = new Uri("https://api.trakt.tv") })
            {
                client.DefaultRequestHeaders.Add("trakt-api-key", "80cf41a8390988e19f93394bb11894f27969267aad51b4978ffaaffb621bcce9");
                var responseShow = await client.GetAsync("movies/popular");
                var responseStringShow = await responseShow.Content.ReadAsStringAsync();
                var resShow = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Movie>>(responseStringShow);
                for (int i = 0; i <resShow.Count; i++)
                {
                  
                    popYear = resShow[i].year;
                    string url = getImaggeUrl(resShow[i].ids.imdb);
                    popMovies += $"{Environment.NewLine }{ Environment.NewLine }#####" + count + ".**" + resShow[i].title.ToString() + "**\t\t\t\t\t" + $"**:Year:**" + "\t\t\t\t\t**" + popYear+ $"**{Environment.NewLine }{ Environment.NewLine }"+($"[![View full Image](" + url + ")](" + url + ")");
                    count = count + 1;
                }
            }
            string message = $"####Popular Movies:####" + popMovies + $"{Environment.NewLine }{ Environment.NewLine }>" + "#####NOTE:if the movie you looking is not on this list,you can search it by typing it title";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }


        
        //Popular Shows
        [LuisIntent("intent.myTraktBots.shows.popular")]
        public async Task popularshows(IDialogContext context, LuisResult result)
        {
            string slung = "";
            string popshow = ""; long showyear = 0; int count = 1;
            using (var client = new HttpClient { BaseAddress = new Uri("https://api.trakt.tv") })
            {
                client.DefaultRequestHeaders.Add("trakt-api-key", "80cf41a8390988e19f93394bb11894f27969267aad51b4978ffaaffb621bcce9");
                var responseShow = await client.GetAsync("shows/popular");
                var responseStringShow = await responseShow.Content.ReadAsStringAsync();
                var resShow = Newtonsoft.Json.JsonConvert.DeserializeObject<List<show>>(responseStringShow);
                for (int i = 0; i < resShow.Count; i++)
                {
                   
                    showyear = resShow[i].year;
                    string url = getshowImageUrl(resShow[i].ids.imdb);
                    string urll = "https://www.themoviedb.org/tv/" + resShow[i].ids.tmdb + "-" +slung;
                     popshow += $"{Environment.NewLine }{ Environment.NewLine }#####" + count + ".**" + resShow[i].title.ToString() + "**\t\t\t\t\t" + $"**:Year:**" + "\t\t\t\t\t**" + showyear + $"**{Environment.NewLine }{ Environment.NewLine }" + ($"[![More infor](" + url + ")](" + urll + ")");
                     count = count + 1;
                }
            }
            string message = $"#### Popular shows:####" + popshow + $"{Environment.NewLine }{ Environment.NewLine }>" + "#####NOTE:if the Tv show you looking is not on this list,you can search it by  it title";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }


        //Most Watched Movies
        [LuisIntent("intent.myTraktBots.movies.played")]
        public async Task watchedMovies(IDialogContext context, LuisResult result)
        {
            string playedMovies = ""; long playedYear = 0; int count = 1; long playedMoviesw = 0;
            using (var client = new HttpClient { BaseAddress = new Uri("https://api.trakt.tv") })
            {
                client.DefaultRequestHeaders.Add("trakt-api-key", "80cf41a8390988e19f93394bb11894f27969267aad51b4978ffaaffb621bcce9");
                var responseShow = await client.GetAsync("movies/played");
                var responseStringShow = await responseShow.Content.ReadAsStringAsync();
                var resShow = Newtonsoft.Json.JsonConvert.DeserializeObject<List<played>>(responseStringShow);
                for (int i = 0; i < resShow.Count; i++)
                {
                   
                    playedYear = resShow[i].movie.year;
                    playedMoviesw = resShow[i].watcher_count;
                    string url = getImaggeUrl(resShow[i].movie.ids.imdb);
                    playedMovies += $"{Environment.NewLine }{ Environment.NewLine }#####" + count + ".**" + resShow[i].movie.title.ToString() + "**\t\t\t\t\t" + $"**:Year:**" + "\t\t\t\t\t**" + playedYear+"\t\t\t\t\t" + ":Watchers:" + "\t\t\t\t\t" + playedMoviesw + $"**{Environment.NewLine }{ Environment.NewLine }" + ($"[![View full Image](" + url + ")](" + url + ")");
                    count = count + 1;

                    
                }
            }
            string message = $"####Most Played Movies####" + playedMovies + $"{Environment.NewLine }{ Environment.NewLine }>" + "#####**NOTE:if the movie you looking is not on this list,you can search it by typing it name";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }


        //most watched series
        [LuisIntent("intent.myTraktBots.shows.watched")]
        public async Task watchedshows(IDialogContext context, LuisResult result)
        {
            string playedshow = ""; long playedYearshow = 0; int count = 1; long playedshoww = 0;string slung = "";
            using (var client = new HttpClient { BaseAddress = new Uri("https://api.trakt.tv") })
            {
                client.DefaultRequestHeaders.Add("trakt-api-key", "80cf41a8390988e19f93394bb11894f27969267aad51b4978ffaaffb621bcce9");
                var responseShow = await client.GetAsync("shows/watched");
                var responseStringShow = await responseShow.Content.ReadAsStringAsync();
                var resShow = Newtonsoft.Json.JsonConvert.DeserializeObject<List<showplayed>>(responseStringShow);
                for (int i = 0; i < resShow.Count; i++)
                {
                    
                    playedYearshow = resShow[i].show.year;
                    playedshoww = resShow[i].watcher_count;
                    string url = getshowImageUrl(resShow[i].show.ids.imdb);
                    string urll = "https://www.themoviedb.org/tv/" + resShow[i].show.ids.tmdb + "-" + slung;
                    playedshow += $"{Environment.NewLine }{ Environment.NewLine }#####" + count + ".**" + resShow[i].show.title.ToString() + "**\t\t\t\t\t" + $"**:Year:**" + "\t\t\t\t\t**" + playedYearshow +"\t\t\t\t\t" + ":Watchers:" + "\t\t\t\t\t"+ playedshoww+ $"**{Environment.NewLine }{ Environment.NewLine }" + ($"[![More infor](" + url + ")](" + urll + ")");
                    count = count + 1;

                }
            }
            string message = $"####Most Watched shows:####" + playedshow + $"{Environment.NewLine }{ Environment.NewLine }>" + "#####NOTE:if the tv show you looking is not on this list,you can search it by typing it name";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        // movies/boxoffice
        [LuisIntent("intent.myTraktBots.movies.boxoffice")]
        public async Task boxOffice(IDialogContext context, LuisResult result)
        {
            string boxOfficeMo = ""; long boxYearmovies = 0; int count = 1; long playedshoww = 0;
            using (var client = new HttpClient { BaseAddress = new Uri("https://api.trakt.tv") })
            {
                client.DefaultRequestHeaders.Add("trakt-api-key", "80cf41a8390988e19f93394bb11894f27969267aad51b4978ffaaffb621bcce9");
                var responseShow = await client.GetAsync("movies/boxoffice");
                var responseStringShow = await responseShow.Content.ReadAsStringAsync();
                var resShow = Newtonsoft.Json.JsonConvert.DeserializeObject<List<box>>(responseStringShow);
                for (int i = 0; i < resShow.Count; i++)
                {
                   
                    boxYearmovies = resShow[i].movie.year;
                    string url = getImaggeUrl(resShow[i].movie.ids.imdb);
                    boxOfficeMo += $"{Environment.NewLine }{ Environment.NewLine }#####" + count + ".**" + resShow[i].movie.title.ToString() + "**\t\t\t\t\t" + $"**:Year:**" + "\t\t\t\t\t**" + boxYearmovies + "\t\t\t\t\t" +$"**{Environment.NewLine }{ Environment.NewLine }" + ($"[![View full Image](" + url + ")](" + url + ")");
                    count = count + 1;
                }
            }
            string message = $"####Box Office Movies:####" + boxOfficeMo + $"{Environment.NewLine }{ Environment.NewLine }>" +"#####NOTE:this list is for box office movies only,you can get ather movies by movie title";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }


        //Released Movies//2016
        [LuisIntent("intent.myTraktBots.movie.Date")]
        public async Task releasedMovies(IDialogContext context, LuisResult result)
        {
            string caleMovies = ""; string released = ""; int count = 1; long playedshoww = 0;
            string t = "calendars/all/movies/" + result.Query.ToString();
            using (var client = new HttpClient { BaseAddress = new Uri("https://api.trakt.tv") })
            {
                client.DefaultRequestHeaders.Add("trakt-api-key", "80cf41a8390988e19f93394bb11894f27969267aad51b4978ffaaffb621bcce9");
                var responseShow = await client.GetAsync(t);
                var responseStringShow = await responseShow.Content.ReadAsStringAsync();
                var resShow = Newtonsoft.Json.JsonConvert.DeserializeObject<List<calendar>>(responseStringShow);
                for (int i = 0; i < resShow.Count; i++)
                {
                    released = resShow[i].released;
                    string url = getImaggeUrl(resShow[0].movie.ids.imdb);
                    if(i==0)
                    {
                       caleMovies += $"{Environment.NewLine }{ Environment.NewLine }#####" + count + ".**" + resShow[i].movie.title.ToString() + "**\t\t\t\t\t" + $"**:released date:**" + "\t\t\t\t\t**" + released + "\t\t\t\t\t" + $"**{Environment.NewLine }{ Environment.NewLine }" + ($"[![View full Image](" + url + ")](" + url + ")");
                    }
                    caleMovies += $"{Environment.NewLine }{ Environment.NewLine }#####" + count + ".**" + resShow[i].movie.title.ToString() + "**\t\t\t\t\t" + $"**:released date:**" + "\t\t\t\t\t**" + released + "\t\t\t\t\t" + $"**{Environment.NewLine }{ Environment.NewLine }";
                    count = count + 1;

                }

            }
            string message = $"####2016 movies:####" + caleMovies + $"{Environment.NewLine }{ Environment.NewLine }>" + "#####NOTE:if the movie you looking is not on this list,you can search it by typing it name";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }


        //2014 movies
        [LuisIntent("intent.myTraktBots.movie.Date2")]
        public async Task releasedMovies2(IDialogContext context, LuisResult result)
        {
            string caleMovies2 = ""; string released2 = ""; int count = 1; long playedshoww2 = 0;
            string t = "calendars/all/movies/" + result.Query.ToString();
            using (var client = new HttpClient { BaseAddress = new Uri("https://api.trakt.tv") })
            {
                client.DefaultRequestHeaders.Add("trakt-api-key", "80cf41a8390988e19f93394bb11894f27969267aad51b4978ffaaffb621bcce9");
                var responseShow = await client.GetAsync(t);
                var responseStringShow = await responseShow.Content.ReadAsStringAsync();
                var resShow = Newtonsoft.Json.JsonConvert.DeserializeObject<List<calendar>>(responseStringShow);
                for (int i = 0; i < resShow.Count; i++)
                {
                   
                    released2 = resShow[i].released;
                   // string url = getImaggeUrl(resShow[0].movie.ids.imdb);
                   // if(i==0)
                   /// {
                        //caleMovies2 += $"{Environment.NewLine }{ Environment.NewLine }#####" + count + ".**" + resShow[i].movie.title.ToString() + "**\t\t\t\t\t" + $"**:released date:**" + "\t\t\t\t\t**" + released2 + "\t\t\t\t\t" + $"**{Environment.NewLine }{ Environment.NewLine }" + ($"[![View full Image](" + url + ")](" + url + ")");
                    //}
                    caleMovies2 += $"{Environment.NewLine }{ Environment.NewLine }#####" + count + ".**" + resShow[i].movie.title.ToString() + "**\t\t\t\t\t" + $"**:released date:**" + "\t\t\t\t\t**" + released2 + "\t\t\t\t\t" + $"**{Environment.NewLine }{ Environment.NewLine }";
                    count = count + 1;
                }

            }
            string message = $"####2014 movies:####" + caleMovies2 + $"{Environment.NewLine }{ Environment.NewLine }" + "#####NOTE:if the movie you looking is not on this list,you can search it by typing it name";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }


        //2015 movies
        [LuisIntent("intent.myTraktBots.movie.Date3")]
        public async Task releasedMovies3(IDialogContext context, LuisResult result)
        {
            string caleMovies2 = ""; string released2 = ""; int count = 1; long playedshoww2 = 0;
            string t = "calendars/all/movies/" + result.Query.ToString();
            using (var client = new HttpClient { BaseAddress = new Uri("https://api.trakt.tv") })
            {
                client.DefaultRequestHeaders.Add("trakt-api-key", "80cf41a8390988e19f93394bb11894f27969267aad51b4978ffaaffb621bcce9");
                var responseShow = await client.GetAsync(t);
                var responseStringShow = await responseShow.Content.ReadAsStringAsync();
                var resShow = Newtonsoft.Json.JsonConvert.DeserializeObject<List<calendar>>(responseStringShow);
                for (int i = 0; i < resShow.Count; i++)
                {
                    released2 = resShow[i].released;
                    string url = getImaggeUrl(resShow[0].movie.ids.imdb);
                    if(i==0)
                    {
                        caleMovies2 += $"{Environment.NewLine }{ Environment.NewLine }#####" + count + ".**" + resShow[i].movie.title.ToString() + "**\t\t\t\t\t" + $"**:released date:**" + "\t\t\t\t\t**" + released2 + "\t\t\t\t\t" + $"**{Environment.NewLine }{ Environment.NewLine }" + ($"[![View full Image](" + url + ")](" + url + ")");
                    }
                    caleMovies2 += $"{Environment.NewLine }{ Environment.NewLine }#####" + count + ".**" + resShow[i].movie.title.ToString() + "**\t\t\t\t\t" + $"**:released date:**" + "\t\t\t\t\t**" + released2 + "\t\t\t\t\t" + $"**{Environment.NewLine }{ Environment.NewLine }";
                    count = count + 1;
                }

            }
            string message = $"####2015 Movies:####" + caleMovies2 + $"{Environment.NewLine }{ Environment.NewLine }>" + "#####NOTE:if the movie you looking is not on this list,you can search it by typing it name";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }


        //validation
        [LuisIntent("intent.myTraktBots.6")]
        public async Task homePage(IDialogContext context, LuisResult result)
        {
            if (result.Query.Equals("6"))
            {

                string message =($"Are you sure you want to go to trakt websites??");
                await context.PostAsync(message);
                context.Wait(MessageReceived);

            }
        }

        //yes to trakt home page
        [LuisIntent("intent.myTraktBots.yes")]
        public async Task homeyes(IDialogContext context, LuisResult result)
        {
           
            System.Diagnostics.Process.Start("https://trakt.tv/home");
            string message = "successfully directed to trakt home page :wink: :+1:";
            await context.PostAsync(message);
            context.Wait(MessageReceived);

        }
        //bye bye
        [LuisIntent("intent.myTraktBots.bye")]
        public async Task bye(IDialogContext context, LuisResult result)
        {
            var ImagesUrl1=new Uri("http://www.greetingsfromheart.com/images/bye_bye/bye_bye.gif");
            string message=($"[![Thank you]("+ImagesUrl1+")]("+ImagesUrl1+")");
            await context.PostAsync(message);
            context.Wait(MessageReceived);

        }

        //no websites
        [LuisIntent("intent.myTraktBots.no")]
        public async Task homeno(IDialogContext context, LuisResult result)
        {
            string message=
            ($"#####*What is Trakt:?*#####" + $"{ Environment.NewLine }{ Environment.NewLine }" +
            $"#####Trakt is a platform that does many things,but primarily keeps track of TV shows and movies you watch.It integrates with your media center or home theater PC to enable scrobbling so everything is automatic." + $"{ Environment.NewLine }{ Environment.NewLine }" + $"#####Some people like to check in from their phone, so we enable that too.Discover new shows and movies," +
            $"follow people with similar tastes and voice your opinion on by using the Trakt website or one of the many community apps that were built using our API." + $"{ Environment.NewLine } { Environment.NewLine }"+$"{ Environment.NewLine } { Environment.NewLine }"+$"#####Complete feature list Trakt is free to use!If you really enjoy it, consider upgrading your account to VIP which unlocks some awesome VIP only features like no ads," +
            $"year in review, iCal feeds, advanced filtering, list cloning, and widgets.Stop reading and sign up now!#####");
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
    }
  }
