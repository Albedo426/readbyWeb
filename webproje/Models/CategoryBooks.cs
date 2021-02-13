using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webproje.Models
{
    public class CategoryBooks
    {
        [Key]
        public int CBId { get; set; }

        public int Category_Id { get; set; }
        public Category category { get; set; }
        public int Book_Id { get; set; }
        public Book book { get; set; }
    }
}