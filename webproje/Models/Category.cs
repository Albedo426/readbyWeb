using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webproje.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public String Name{ get; set; }
        public virtual List<Book> book { get; set; }


    }
}