using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webproje.Models;

namespace webproje.Controllers
{
    public class HomeController : Controller
    {
        private UserContext dbuser = new UserContext();
        private BookContext dbBook = new BookContext();
        private albooksContext blog = new albooksContext();
        private mybooks mybook = new mybooks();
        public ActionResult Index()
        {
            return View(dbBook);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult BlogSingle(String Id)
        {
            if (Id != null)
            {
                if (Id != "")
                {
                    if (Session["loguser"] != null)
                    {
                        if (mybook.getBookHaveorCrative(Session["loguser"].ToString(), Id)) {
                            Book b = blog.getbookById(Id);
                            return View(b);
                        }

                    }
                }
            }
            return RedirectToAction("Index", dbBook);
        }
        public ActionResult Login()
        {
            bool conlog = (Session["loguser"] == null ? false : true);
            if (conlog) {

                return RedirectToAction("Index", dbBook);

            }
            else {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Login(User u)
        {
            //List<User> list= dbsetvalure();
            //session oplduğu iiçin 
            if (u.name == null)
            {
                //login
                if (u.userName != "" && u.userPass != "" && u.userPass != null && u.userPass != null)
                {
                    //setsession (from user)
                    User us = dbuser.logControl(u);
                    if (us != null) {
                        Session["loguser"] = us.Id.ToString();
                        Session["Username"] = us.name.ToString();
                        ViewBag.user = dbuser.getUserIsId(Int16.Parse(Session["loguser"].ToString()));
                        return RedirectToAction("Index", dbBook);
                    }

                }
                else {
                    //nul input
                }

            }
            else {
                dbuser.userList.Add(u);

            }
            dbuser.SaveChanges();
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult AllBooks()
        {
            return View(blog);
        }
        [HttpPost]
        public ActionResult AllBooks(String SearchbookString) {

            blog.SearchbookString = SearchbookString;
            return View(blog);


        }
        [HttpPost]
        public ActionResult WriteBook(BookContext dbBookdat)
        {

            //here filename variable used because each image name store in that filename  
            var fileName = String.Empty;
            if (dbBookdat.ImageUpload != null)
            {
                //uploaded image filename get using below line  
                fileName = Path.GetFileName(dbBookdat.ImageUpload.FileName);
                //if youwant rename this file this like  
                fileName = fileName.Substring(0, fileName.IndexOf('.')) + "_." + fileName.Substring(fileName.IndexOf('.') + 1);
                //”Images ” this is folder created we are uploading image in that folder  
                var uploadDir = "/temp/readBy/assets/img/gallery/";
                //your folder name path and image name combined in Server.MapPath()  
                var imagePath = Path.Combine(Server.MapPath(uploadDir), fileName);
                //when you have Saveas() call and imagepath as parameter that time this image store in thif Folder.  
                dbBookdat.ImageUpload.SaveAs(imagePath);
            }
            dbBookdat.book.img = fileName;
            dbBookdat.book.UserId = int.Parse(Session["loguser"].ToString());
            //userId eklenecek
            dbBook.booklist.Add(dbBookdat.book);
            dbBook.SaveChanges();
            //lastId
            dbBookdat.comitcategory(dbBookdat.book.Id);
            return RedirectToAction("Index", dbBook);
        }
        public ActionResult WriteBook()
        {

            if (Session["loguser"] != null)
            {
                ViewBag.Message = "Your application description page.";
                List<Language> liss = dbBook.getAlllanguage();

                ViewBag.test = new SelectList(liss, "Id", "languagename");
                return View(dbBook);
            }

            return RedirectToAction("Index", dbBook);
        }
        public ActionResult Mybook()
        {
            //kendi kitaplığın için

            //eğer modelum nulsa indexe git 
            if (Session["loguser"] != null)
            {
                //loguserdeki ıddeki veriler
                return View(mybook);

            }
            else
            {
                return RedirectToAction("Index", dbBook);
            }
        }
        public ActionResult Mybooks()
        {
            //kendi aldıklarım
            if (Session["loguser"] != null)
            {
                mybook.Mycontroller = false;
                //loguserdeki ıddeki veriler
                return View("Mybook", mybook);

            }
            else
            {
                return RedirectToAction("Index", dbBook);
            }
        }
        //kitaplığım   aldıklarım bookshelf
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "delete")]
        public ActionResult delete(mybooks Id)
        {
            _ = Id;

            var vp = Id.booklist.Where(a => a.Id == Id.testerId);
            Id.booklist.RemoveRange(vp);
            Id.SaveChanges();
            return RedirectToAction("index", dbBook); ;
        }

        public ActionResult userProf()
        {
            if (Session["loguser"] != null)
            {
                dbuser.getUserIsId(int.Parse(Session["loguser"].ToString()));
                //loguserdeki ıddeki veriler
                return View(dbuser);

            }
            return RedirectToAction("Index", dbBook);
        }
        [HttpPost]
        public ActionResult userProf(UserContext Id)
        {

            if (Session["loguser"] != null)
            {

                Id.user.Id =int.Parse( Session["loguser"].ToString());
                User data = dbuser.userList.FirstOrDefault(x => x.Id == Id.user.Id);
                // Checking if any such record exist  
                if (data != null)
                {
                    data.name = Id.user.name;
                    data.lastName = Id.user.lastName;
                    data.userName = Id.user.userName;
                    data.userPass = Id.user.userPass;
                    data.eMail = Id.user.eMail;
                    data.Iban = Id.user.Iban;
                    dbuser.SaveChanges();
                }
                dbuser.getUserIsId(int.Parse(Session["loguser"].ToString()));
                return View(dbuser);
            }
            return RedirectToAction("Index", dbBook);
        }


    }
}