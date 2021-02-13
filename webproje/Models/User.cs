using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webproje.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public String userName { get; set; }
        public String userPass { get; set; }
        public String name { get; set; }
        public String lastName { get; set; }
        public String eMail { get; set; }
        public String Iban { get; set; }


    }
}