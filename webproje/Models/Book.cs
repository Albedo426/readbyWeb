using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webproje.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public String bookName { get; set; }
        public String summary { get; set; }
        public String text { get; set; }
        public String img { get; set; }
        public int price { get; set; }
        public int point { get; set; }

        public int UserId { get; set; }
        public User user { get; set; }

        public int LanguageId { get; set; }
        public Language language{ get; set; }

        public virtual List<Category> categories { get; set; }

    }
}