using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DevBuildMovie.Controllers
{
    public class APIController : Controller
    {
        const string userAgent = "Mozilla / 5.0(Windows NT 6.1; Win64; x64; rv: 47.0) Gecko / 20100101 Firefox / 47.0";

        enum Genres {Action, Animation, Drama, Horror, Superhero, Thriller };

        // GET list of movies
        [HttpGet]
        public ActionResult ListMovies()
        {
            HttpWebRequest request = WebRequest.CreateHttp($"http://localhost:54478/api/Movie/GetMovies");
            request.UserAgent = userAgent;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                StreamReader data = new StreamReader(response.GetResponseStream());

                //do stuff with the data here
                string jsonData = data.ReadToEnd();
                JObject movieData = JObject.Parse("{movies:" + jsonData + "}");
                ViewBag.Movies = movieData;
            }

            return View();
        }

        // GET list of movies in specific category

        [HttpGet]
        public ActionResult ListMoviesByGenre(string genre)
        {
            HttpWebRequest request = WebRequest.CreateHttp($"http://localhost:54478/api/Movie/GetMovies");
            request.UserAgent = userAgent;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                StreamReader data = new StreamReader(response.GetResponseStream());

                string jsonData = data.ReadToEnd();
                JObject movieData = JObject.Parse("{movies:" + jsonData + "}");

                List<JToken> moviesByGenre = new List<JToken>();

                for (int i = 0; i < movieData["movies"].Count(); i++)
                {
                    if (movieData["movies"][i]["Genre"].ToString() == genre)
                    {
                        moviesByGenre.Add(movieData["movies"][i]);
                    }
                }

                ViewBag.MoviesByGenre = moviesByGenre;
                ViewBag.Genre = genre;
            }

            return View();
        }

        // GET a random movie pick

        [HttpGet]
        public ActionResult RandomMovie()
        {
            HttpWebRequest request = WebRequest.CreateHttp($"http://localhost:54478/api/Movie/GetMovies");
            request.UserAgent = userAgent;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                StreamReader data = new StreamReader(response.GetResponseStream());

                string jsonData = data.ReadToEnd();
                JObject movieData = JObject.Parse("{movies:" + jsonData + "}");

                Random rng = new Random();
                ViewBag.RandomPick = movieData["movies"][rng.Next(movieData["movies"].Count())];
            }

            return View();
        }

        // GET a random movie pick in specific category
    }
}