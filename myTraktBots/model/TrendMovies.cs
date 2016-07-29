
using System;

namespace myTraktBots.model
{
    public class TrendingMovie
    {
        public int watchers { get; set; }
        public Movie movie { get; set; }
    }

    public class Movie
    {
        public string title { get; set; }
        public int year { get; set; }
        public Ids ids { get; set; }
    }

    //get number of season for show
    public class season
    {
        public int number { get; set; }
        public Ids ids { get; set; }
    }

    //id for show
    public class seasonIds
    {
        public int trakt { get; set; }
        public int? tvdb { get; set; }
        public int? tmdb { get; set; }
        public object tvrage { get; set; }
    }

    public class Ids
    {
        public int trakt { get; set; }
        public string slug { get; set; }
        public string imdb { get; set; }
        public int tmdb { get; set; }
    }
    //show Trending
    public class showTrending
    {
        public long watchers { get; set; }
        public show show { get; set; }

    }
    //Show Trending
    public class showids
    {
        public long trakt { get; set; }
        public string slug { get; set; }="";
        public long tvdb { get; set; }
        public string imdb { get; set; }
        public long tmdb { get; set; }
        public long tvrage { get; set; }
    }


    //shows Trending
    public class show
    {
        public string title { get; set; }
        public long year { get; set; }
        public showids ids { get; set; }
        public override string ToString()
        {
            return $"{ title}-{year}";

        }
    }
    //most played,collected,watched movies
    public class played
    {
        public long watcher_count { get; set; }
        public long play_count { get; set; }
        public long collected_count { get; set; }
        public Movie movie { get; set; }
    }
    //most played,collected,watched series
    public class showplayed
    {
        public long watcher_count { get; set; }
        public long play_count { get; set; }
        public long collected_count { get; set; }
        public long collector_count { get; set; }
        public show show { get; set; }
    }
    //calendar
    public class calendar
    {
        public string released { get; set; }
        public Movie movie { get; set; }
    }
    //search for Movies 
    public class search
    {
        public string type { get; set; }
        public long score { get; set; }
        public Movie movie { get; set; }

    }
    //search show
    public class search2
    {
        public string type { get; set; }
        public long score { get; set; }
        public show show { get; set; }

    }
    public class box
    {
        public long revenue { get; set; }
        public Movie movie { get; set; }
    }



    //comments
    public class comments
    {
        public int id { get; set; }
        public string comment { get; set; }
        public bool spoiler { get; set; }
        public bool review { get; set; }
        public int parent_id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public int replies { get; set; }
        public int likes { get; set; }
        public int user_rating { get; set; }
        public User user { get; set; }
    }

    public class User
    {
        public string username { get; set; }
        public bool _private { get; set; }
        public string name { get; set; }
        public bool vip { get; set; }
        public bool vip_ep { get; set; }
    }

    //get movies Images....
    public class GetImages
    {
        public string type { get; set; }
        public object score { get; set; }
        public Movies movie { get; set; }
    }

    public class Movies
    {
        public string title { get; set; }
        public int year { get; set; }
        public IdsImage ids { get; set; }
        public Images images { get; set; }
    }

    public class IdsImage
    {
        public int trakt { get; set; }
        public string slug { get; set; }
        public string imdb { get; set; }
        public int tmdb { get; set; }
    }

    public class Images
    {
        public Fanart fanart { get; set; }
        public Poster poster { get; set; }
        public Logo logo { get; set; }
        public Clearart clearart { get; set; }
        public Banner banner { get; set; }
        public Thumb thumb { get; set; }
    }

    public class Fanart
    {
        public string full { get; set; }
        public string medium { get; set; }
        public string thumb { get; set; }
    }

    public class Poster
    {
        public string full { get; set; }
        public string medium { get; set; }
        public string thumb { get; set; }
    }

    public class Logo
    {
        public string full { get; set; }
    }

    public class Clearart
    {
        public string full { get; set; }
    }

    public class Banner
    {
        public string full { get; set; }
    }

    public class Thumb
    {
        public string full { get; set; }
    }

    //get shows images.....
    public class GetShowImages
    {
        public string type { get; set; }
        public object score { get; set; }
        public shows show { get; set; }
    }

    public class shows
    {
        public string title { get; set; }
        public int year { get; set; }
        public showids ids { get; set; }
        public Images images { get; set; }
    }



}