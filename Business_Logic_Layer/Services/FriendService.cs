using SocialNetwork.Business_Logic_Layer.Exceptions;
using SocialNetwork.Business_Logic_Layer.Models;
using SocialNetwork.Data_Access_Layer.Entities;
using SocialNetwork.Data_Access_Layer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Business_Logic_Layer.Services
{
    public class FriendService
    {
        IUserRepository userRepository;
        IFriendRepository friendRepository;
        public FriendService()
        {
            this.userRepository = new UserRepository();
            this.friendRepository = new FriendRepository();
        }
        public void AddFriend(FriendRequestData friendRequestData)
        {
            var findUserEntity = this.userRepository.FindByEmail(friendRequestData.FriendEmail);

            if (findUserEntity is null)
                throw new UserNotFoundException();

            var findFriendEntity = this.friendRepository.FindByFriendId(findUserEntity.id);

            if (findFriendEntity != null)
                throw new Exception();

            var friendEntity = new FriendEntity()
            {
                friend_id = findUserEntity.id,
                user_id = friendRequestData.SenderId,
            };

            if (this.friendRepository.Create(friendEntity) == 0)
                throw new Exception();
        }
        public IEnumerable<Friend> GetFriendsList(int userId)
        {
            var friends = new List<Friend>();

            friendRepository.FindAllByUserId(userId).ToList().ForEach(m =>
            {
                UserEntity userEntity = userRepository.FindById(m.friend_id);
                friends.Add(new Friend(m.id, userEntity.email,userEntity.firstname, userEntity.lastname));
            });
            return friends;
        }
    }
}
