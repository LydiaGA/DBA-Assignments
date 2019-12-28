using MyLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace MyLibrary.Controllers
{
    public class BooksController : Controller
    {
        public ActionResult Index()
        {
            XmlDocument XmlDocObj = new XmlDocument();
            XmlDocObj.Load(Server.MapPath("MyLibrary.xml"));
            XmlNode RootNode = XmlDocObj.SelectSingleNode("books");
            XmlNodeList books = RootNode.ChildNodes;

            return View(books);
           
        }


        public ActionResult Create(book book)
        {
            ViewBag.Message = "Create a New Book";

            return View();
        }

        public ActionResult Store(book book)
        {
            XmlDocument XmlDocObj = new XmlDocument();
            XmlDocObj.Load(Server.MapPath("../MyLibrary.xml"));
            XmlNode RootNode = XmlDocObj.SelectSingleNode("books");

            int booksNumber = RootNode.ChildNodes.Count;
            XmlNode bookNode = RootNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "book", ""));

            bookNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "Id", "")).InnerText = booksNumber.ToString();
            bookNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "Title", "")).InnerText = book.title;
            bookNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "Description", "")).InnerText = book.description;
            bookNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "Content", "")).InnerText = book.content;
            bookNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "LastEdited", "")).InnerText = DateTime.Now.ToString();

            XmlDocObj.Save(Server.MapPath("../MyLibrary.xml"));


            return RedirectToAction("Index");
        }

        public ActionResult Read(String id)
        {
            XmlDocument XmlDocObj = new XmlDocument();
            XmlDocObj.Load(Server.MapPath("../../MyLibrary.xml"));
            XmlNode RootNode = XmlDocObj.SelectSingleNode("books");
            XmlNodeList books = RootNode.ChildNodes;

            XmlNode selectedBook = books[0];

            foreach(XmlNode book in books)
            {
                if(book["Id"].InnerText == id)
                {
                    selectedBook = book;
                }
            }

            return View(selectedBook);

        }

        public ActionResult Edit(String id)
        {
            XmlDocument XmlDocObj = new XmlDocument();
            XmlDocObj.Load(Server.MapPath("../../MyLibrary.xml"));
            XmlNode RootNode = XmlDocObj.SelectSingleNode("books");
            XmlNodeList books = RootNode.ChildNodes;

            XmlNode selectedBook = books[0];

            foreach (XmlNode book in books)
            {
                if (book["Id"].InnerText == id)
                {
                    selectedBook = book;
                }
            }

            return View(selectedBook);

        }

        public ActionResult Update(book book)
        {
            XmlDocument XmlDocObj = new XmlDocument();
            XmlDocObj.Load(Server.MapPath("../MyLibrary.xml"));
            XmlNode RootNode = XmlDocObj.SelectSingleNode("books");
            XmlNodeList books = RootNode.ChildNodes;

            XmlNode selectedBook = books[0];

            foreach (XmlNode item in books)
            {
                if (item["Id"].InnerText == book.book_id.ToString())
                {
                    selectedBook = item;
                }
            }

            selectedBook["Title"].InnerText = book.title;
            selectedBook["Description"].InnerText = book.description;
            selectedBook["Content"].InnerText = book.content;

            XmlDocObj.Save(Server.MapPath("../MyLibrary.xml"));

            return RedirectToAction("Index");

        }

        public ActionResult Delete(String id)
        {
            XmlDocument XmlDocObj = new XmlDocument();
            XmlDocObj.Load(Server.MapPath("../../MyLibrary.xml"));
            XmlNode RootNode = XmlDocObj.SelectSingleNode("books");
            XmlNodeList books = RootNode.ChildNodes;

            XmlNode selectedBook = books[0];

            foreach (XmlNode book in books)
            {
                if (book["Id"].InnerText == id)
                {
                    selectedBook = book;
                }
            }

            RootNode.RemoveChild(selectedBook);

            XmlDocObj.Save(Server.MapPath("../../MyLibrary.xml"));

            return RedirectToAction("Index");
        }
    }
}