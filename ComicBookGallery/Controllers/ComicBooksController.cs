using ComicBookGallery.Data;
using ComicBookGallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace ComicBookGallery.Controllers
{
    public class ComicBooksController : Controller
    {
        private ComicBookRepository _comicBookRepository = null;

        // constructor -  no return value and shares the same name as the class name
        public ComicBooksController ()
        {
            _comicBookRepository = new ComicBookRepository();
        }

        // index - list page
        public ActionResult Index()
        {
            var comicBooks = _comicBookRepository.GetComicBooks().OrderBy(x => x.SeriesTitle).ToList(); ;
            return View(comicBooks);
        }

        // allow nullable
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var comicBook = _comicBookRepository.GetComicBook((int)id);

            // strongly typed view - by putting object into the view vs. ViewBag.ComicBook = comicBook;
            return View(comicBook);  // will automatically look in the views folder
        }

        public ActionResult Detail2()
        {
            if (DateTime.Today.DayOfWeek == DayOfWeek.Friday)
            {
                // return new RedirectResult("/");
                return Redirect("/");
            }

            //return new ContentResult()
            //{
            //    Content = "This is a test."
            //};
            return Content("This is a test.");
        }

        public FileResult downloadPdf()
        {
            string fileName = System.IO.Path.GetTempPath() + "los_vengadores_acoso_nunca_mas_marvel_panini_is4k.pdf";
            
            using (var client = new WebClient())
            {
                client.DownloadFile(@"https://www.is4k.es/sites/default/files/contenidos/los_vengadores_acoso_nunca_mas_marvel_panini_is4k.pdf", fileName);                
            }
            return File(fileName, "application/pdf");
        }
    }
}