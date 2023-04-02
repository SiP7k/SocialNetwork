using SocialNetwork.Business_Logic_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views
{
    internal class FriendsListView
    {
        public void Show(IEnumerable<Friend> friendList)
        {
            Console.WriteLine("Список друзей");


            if (friendList.Count() == 0)
            {
                Console.WriteLine("У вас пока нет друзей:(");
                return;
            }

            friendList.ToList().ForEach(friend =>
            {
                Console.WriteLine($"{friend.FirstName} {friend.LastName}\n");
            });
        }
    }
}
