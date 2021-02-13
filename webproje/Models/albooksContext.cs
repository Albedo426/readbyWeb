using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace webproje.Models
{
    public class albooksContext:DbContext
    {
        public albooksContext() : base("conString")
        {

        }
        public DbSet<Book> booklist { get; set; }

        private String searchbookString="";

        public String SearchbookString
        {
            get { return searchbookString; }
            set { searchbookString = value; }
        }

        public DbSet<User> userlist { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<CategoryBooks> CategoryBooks { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {

            
            builder.Entity<Book>()
                        .HasMany<Category>(s => s.categories)
                        .WithMany(c => c.book)
                        .Map(cs =>
                        {
                            cs.MapLeftKey("Category_Id");
                            cs.MapRightKey("Book_Id");
                            cs.ToTable("CategoryBooks");
                        });

        }

        internal Book getbookByI(string ıd)
        {
            throw new NotImplementedException();
        }

        public List<Category> getCategory() {
            return Category.ToList();
        }
        public Category getCategory(int id)
        {
            return Category.Where(c => c.Id == id).ToList()[0];
        }
        internal Book getbookById(string Id)
        {
            UserContext dbusers = new UserContext();
            int Idint = int.Parse(Id);
            List<Book> listt = booklist.Where(i => i.Id == Idint).ToList();
            foreach (var item in listt)
            {
                item.user = dbusers.getUserIsId(item.UserId);
                item.categories = getCategoryById(item.Id);

            }
            return listt[0];
        }
        public List<Category> getCategoryById(int i)
        {
            List<Category> mainlistt = new List<Category>();

            List<CategoryBooks> testlist = CategoryBooks.SqlQuery("exec getCategoryBooks @id", new SqlParameter("@id", i)).ToList();
            foreach (CategoryBooks item in testlist)
            {
                mainlistt.Add(getCategory(item.Category_Id));
            }
            return mainlistt;
        }
        public List<Book> getbookOrder(int pice)
        {
            UserContext dbusers = new UserContext();
            List<Book> listt = booklist.Take(pice).OrderByDescending(i => i.Id).ToList();
            foreach (var item in listt)
            {
                item.user = dbusers.getUserIsId(item.UserId);
            }
            return listt;
        }
        public List<Book> getbookOrderScrol()
        {
            UserContext dbusers = new UserContext();
            List<Book> listt;
            List<Category> listt2 = new List<Category>();
            if (searchbookString == "")
            {
                listt = booklist.OrderBy(i => i.point).ToList();
            }
            else {
                listt= booklist.OrderBy(i => i.point).Where(i=>i.summary.Contains(searchbookString)).ToList();
            }
            foreach (var item in listt)
            {
                item.user = dbusers.getUserIsId(item.UserId);
                item.categories = getCategoryById(item.Id);
            }
            
            return listt;
        }

    }
}