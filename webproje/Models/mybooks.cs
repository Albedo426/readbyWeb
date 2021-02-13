using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace webproje.Models
{
    public class mybooks : DbContext
    {
        public mybooks() : base("conString")
        {

        }
        public DbSet<Book> booklist { get; set; }

        public DbSet<User> userlist { get; set; }
        public DbSet<BookShelf> BookShelflist { get; set; }
        private bool controller = true;
        public bool Mycontroller
        {
            get { return controller; }
            set { controller  = value; }
        }

        public Book book { get; set; }
        public int testerId{ get; set; }
        public int userId { get; set; }
        public List<Book> getbookByID(String Id)
        {
            if (controller)
            {
                userId = int.Parse(Id);
                return booklist.Where(i => i.UserId == userId).ToList();
            }
            else {
                userId = int.Parse(Id);
                 List<Book> testlist = booklist.SqlQuery("exec getBooks @id", new SqlParameter("@id", userId)).ToList();
                return testlist;

            }
        }
        public bool getBookHaveorCrative(String Id, String bookId)
        {
            userId = int.Parse(Id);
            int testlist = 0;
            testlist = BookShelflist.SqlQuery("select * from UserBook where  UserBook.UserId = @id and UserBook.BookId=@bookid ", new SqlParameter("@id", userId), new SqlParameter("@bookid", bookId)).ToList().Count;
            testlist+= booklist.SqlQuery("select * from Books where   Id=@bookid  and UserId=@id",  new SqlParameter("@id", userId), new SqlParameter("@bookid", bookId)).ToList().Count;
            if (testlist==0) {
                return false;
            }
            return true;
        }

    }
}