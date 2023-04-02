using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Business_Logic_Layer.Models
{
    public class Friend
    {
        public string FirstName { get;}
        public string LastName { get;}
        public int Id { get; }
        public string Email { get; }

        public Friend(int id, string email,string firstName, string lastName)
        {
            this.Id = id;
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
        }
    }
}
