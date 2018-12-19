using DevBuildMovie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace DevBuildMovie.Controllers
{
    public class DatabaseController : Controller
    {
        // GET: Database
        public ActionResult Index()
        {
            DevBuildMovieDBEntities ORM = new DevBuildMovieDBEntities();
            ViewBag.MovieList = ORM.Movies.ToList();

            return View();
        }

        public ActionResult AddMovie()
        {
            return View();
        }

        public ActionResult SaveNewMovie(Movie newMovie, Director newDirector)
        {
            DevBuildMovieDBEntities ORM = new DevBuildMovieDBEntities();
            if (newMovie != null)
            {
                ORM.Movies.Add(newMovie);
                ORM.SaveChanges();
            }

            if (newDirector != null)
            {
                List<Director> directorsByUniqueNames = 
                    ORM.Directors.Where(c => c.LastName == newDirector.LastName 
                    && c.FirstName == newDirector.FirstName).ToList();
                if (directorsByUniqueNames is null)
                {
                    ORM.Directors.Add(newDirector);
                    ORM.SaveChanges();
                }
                
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditMovie(int movieID)
        {
            DevBuildMovieDBEntities ORM = new DevBuildMovieDBEntities();
            Movie found = ORM.Movies.Find(movieID);

            return View(found);
        }

        public ActionResult SaveMovieChanges(Movie updatedMovie)
        {
            DevBuildMovieDBEntities ORM = new DevBuildMovieDBEntities();
            Movie oldMovie = ORM.Movies.Find(updatedMovie.MovieID);

            oldMovie.MovieTitle = updatedMovie.MovieTitle;
            oldMovie.Genre = updatedMovie.Genre;
            oldMovie.Year = updatedMovie.Year;
            oldMovie.Watched = updatedMovie.Watched;

            ORM.Entry(oldMovie).State = System.Data.Entity.EntityState.Modified;
            ORM.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteMovie(int movieID)
        {
            DevBuildMovieDBEntities ORM = new DevBuildMovieDBEntities();
            Movie deletedMovie = ORM.Movies.Find(movieID);

            ORM.Movies.Remove(deletedMovie);
            ORM.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}