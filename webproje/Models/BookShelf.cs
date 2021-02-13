using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webproje.Models
{
    public class BookShelf
    {
        [Key]
        public int BookshelfId { get; set; }
        public int UserId { get; set; }
        public User user { get; set; }

        public int BookId { get; set; }
        public Book book { get; set; }
    }
}