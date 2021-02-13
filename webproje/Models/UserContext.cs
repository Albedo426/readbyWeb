using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace webproje.Models
{
    public class UserContext:DbContext
    {
        public UserContext():base("conString")
        {
                    
        }
        public DbSet<User> userList { get; set; }
        public User user { get; set; }
        public User logControl(User u) {

            List<User> listt = userList.Where(i => i.userName.Equals(u.userName) && i.userPass.Equals(u.userPass)).ToList();
            if (listt.Count != 0)
            {
                return listt[0];
            }
            return null;
        }

        public User getUserIsId(int userId)
        {

            List<User> listt = userList.Where(i => i.Id==userId).ToList();
            if (listt.Count != 0)
            {
                user =listt[0];
                return listt[0];
            }
            return null;
        }
    }
}