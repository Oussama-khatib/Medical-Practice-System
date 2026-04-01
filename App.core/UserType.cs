using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.core
{
    public class UserType
    {
        [Key]
        public int UserTypeId { get; set; }
        public string Usertype { get; set; }
        public ICollection<User>? users { get; set; }
    }
}
