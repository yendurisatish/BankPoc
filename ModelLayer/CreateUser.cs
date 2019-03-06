using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class CreateUser 
    {
        public string UserName { get; set; }
        public Int64 AccountNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Dob { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Aadhar { get; set; }
        public string AccountType { get; set; }
        public float Balance { get; set; }
        public string Address { get; set; }
        // public string Password { get; set; }
        public bool IsAdmin { get; set; }
    
    //2nd comment
    public string Password { get; set; }
    }
}
