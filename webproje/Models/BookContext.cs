using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace webproje.Models
{
    public class BookContext: DbContext
    {
        public BookContext() : base("conString")
        {

        }
        public DbSet<Book> booklist { get; set; }
        public Book book { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<User> userlist { get; set; }
        public DbSet<Language> language { get; set; }
        public List<bool> selected { get; set; }
        public HttpPostedFileBase ImageUpload {get;set;}
        internal void comitcategory(int BookId)
        {
            albooksContext n = new albooksContext();
            for (int i = 0; i < getAlllanguage().Count; i++)
            {
                if (selected[i]==true) {
                    String s = "INSERT INTO CategoryBooks(Book_Id,Category_Id)VALUES("+ BookId + ", " + getAlllanguage()[i].Id +")";
                     n.Database.ExecuteSqlCommand(s);
                   n.SaveChanges();
                }
            }
        }
       
        public List<Book> getbooksPiece(int pice) {
            UserContext dbusers = new UserContext();
            List<Book> listt = booklist.Take(pice).OrderByDescending(i => i.Id).ToList();
            foreach (var item in listt)
            {
                item.user=dbusers.getUserIsId(item.UserId);
            }
            if (listt==null)
            {
                listt = new List<Book>();
            }
            return listt;
        }

        public List<Category> getAllcategory() { 
            return Category.ToList();
        }
        public List<Language> getAlllanguage()
        {
            return language.ToList();
        }

        public List<Book> getbookOrder(int pice) {
            UserContext dbusers = new UserContext();
            List<Book> listt = booklist.Take(pice).OrderByDescending(i => i.Id).ToList();
            foreach (var item in listt)
            {
                item.user = dbusers.getUserIsId(item.UserId);
            }
            return listt;
        }
        public List<Book> getbookOrderScrol(int pice)
        {
            UserContext dbusers = new UserContext();
            List<Book> listt = booklist.Take(pice).OrderByDescending(i => i.point).ToList();
            foreach (var item in listt)
            {
                item.user = dbusers.getUserIsId(item.UserId);
            }
            return listt;
        }

      
    }
}   