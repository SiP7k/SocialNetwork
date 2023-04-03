using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Business_Logic_Layer.Models
{
    public class FriendRequestData
    {
        public int SenderId { get; set; }
        public string FriendEmail { get; set; }
    }
}
