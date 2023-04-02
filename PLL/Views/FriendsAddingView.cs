using SocialNetwork.Business_Logic_Layer.Exceptions;
using SocialNetwork.Business_Logic_Layer.Models;
using SocialNetwork.Business_Logic_Layer.Services;
using SocialNetwork.Data_Access_Layer.Repositories;
using SocialNetwork.PLL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views
{
    internal class FriendsAddingView
    {
        FriendService friendService;
        UserService userService;
        public FriendsAddingView(FriendService friendService, UserService userService)
        {
            this.friendService = friendService;
            this.userService = userService;
        }

        public void Show(User user)
        {
            FriendRequestData friendRequestData = new FriendRequestData();

            Console.Write("Введите почтовый адрес друга: ");

            friendRequestData.FriendEmail = Console.ReadLine();
            friendRequestData.SenderId = user.Id;

            try
            {
                friendService.AddFriend(friendRequestData);
                SuccessMessage.Show("Друг успешно добавлен!");
                user = userService.FindById(user.Id);
            }

            catch (UserNotFoundException)
            {
                AlertMessage.Show("Пользователь не найден!");
            }
            catch (Exception)
            {
                AlertMessage.Show("Произошла ошибка при отправке запроса в друзья!");
            }

        }
    }
}
