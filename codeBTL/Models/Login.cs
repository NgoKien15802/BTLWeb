using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace codeBTL.Models
{
    public class Login
    {
        [DisplayName("Username:")]
        public string? username
        {
            get; set;
        }
        [DataType(DataType.Password)]


        [DisplayName("Password")]
        public string? password
        {
            get; set;
        }
    }
}
