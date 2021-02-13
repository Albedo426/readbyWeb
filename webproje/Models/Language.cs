using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webproje.Models
{
    public class Language
    {
        [Key]
        public int Id { get; set; }
        public String languagename { get; set; }
    }
}   