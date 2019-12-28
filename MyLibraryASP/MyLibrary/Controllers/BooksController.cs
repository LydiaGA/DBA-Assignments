using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyLibrary.Controllers
{
    public class BooksController : Controller
    {
        MyLibraryDataContext db = new MyLibraryDataContext();
        
        public ActionResult Index()
        {           

            return View(db.books);
        }


        public ActionResult Create()
        {
            ViewBag.Message = "Create a New Book";

            return View();
        }

        public ActionResult Store(book book)
        {

            book toSave = new book();
            toSave.title = book.title;
            toSave.description = book.description;
            toSave.content = book.content;
            toSave.last_edited = DateTime.Now;

            db.books.InsertOnSubmit(toSave);
            db.SubmitChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Read(int book_id)
        {
            var result = from b in db.books where b.book_id == book_id select b;

            return View(result.First());

        }

        public ActionResult Edit(int book_id)
        {
            var result = from b in db.books where b.book_id == book_id select b;

            return View(result.First());

        }

        public ActionResult Update(book book)
        {
            var result = from b in db.books where b.book_id == book.book_id select b;
            result.FirstOrDefault().title = book.title;
            result.FirstOrDefault().description = book.description;
            result.FirstOrDefault().content = book.content;
            result.FirstOrDefault().last_edited = DateTime.Now;
            db.SubmitChanges();

            return RedirectToAction("Index");

        }

        public ActionResult Delete(int book_id)
        {

            var result = from b in db.books where b.book_id == book_id select b;
            db.books.DeleteOnSubmit(result.First());
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}