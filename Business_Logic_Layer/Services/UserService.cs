using SocialNetwork.Business_Logic_Layer.Exceptions;
using SocialNetwork.Business_Logic_Layer.Models;
using SocialNetwork.Data_Access_Layer.Entities;
using SocialNetwork.Data_Access_Layer.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Business_Logic_Layer.Services
{
    public class UserService
    {
        MessageService messageService;
        IUserRepository userRepository { get; set; }
        FriendService friendService;

        public UserService()
        {
            messageService = new MessageService();
            userRepository = new UserRepository();
            friendService = new FriendService();
        }
        public void Register(UserRegistrationData userRegistrationData)
        {
            if (String.IsNullOrEmpty(userRegistrationData.FirstName)) throw new
                    ArgumentNullException();
            if (String.IsNullOrEmpty(userRegistrationData.LastName)) throw new
                    ArgumentNullException();
            if (String.IsNullOrEmpty(userRegistrationData.Email)) throw new
                    ArgumentNullException();
            if (String.IsNullOrEmpty(userRegistrationData.Password)) throw new
                    ArgumentNullException();
            if (userRegistrationData.Password.Length < 8) throw new
                    ArgumentNullException();
            if (!new EmailAddressAttribute().IsValid(userRegistrationData.Email)) throw new
                    ArgumentNullException();
            if(userRepository.FindByEmail(userRegistrationData.Email) != null) throw new
                    ArgumentNullException();

            var userEntity = new UserEntity()
            {
                firstname = userRegistrationData.FirstName,
                lastname = userRegistrationData.LastName,
                email = userRegistrationData.Email,
                password = userRegistrationData.Password
            };

            if (this.userRepository.Create(userEntity) == 0)
                throw new Exception();
        }
        
        public User Authenticate(UserAuthenticationData userAuthenticationData)
        {
            var findUserEntity =
                userRepository.FindByEmail(userAuthenticationData.Email);
            if (findUserEntity is null) throw new
                    UserNotFoundException("Пользователь с таким email не найден!");

            if (findUserEntity.password != userAuthenticationData.Password)
                throw new WrongPasswordException("Пароль неверный!");

            return ConstructUserModel(findUserEntity);
        }
        public User FindByEmail(string email)
        {
            var findUserEntity = userRepository.FindByEmail(email);
            if (findUserEntity is null) throw new UserNotFoundException();

            return ConstructUserModel(findUserEntity);
        }
        public User FindById(int id)
        {
            var findUserEntity = userRepository.FindById(id);
            if (findUserEntity is null) throw new UserNotFoundException();

            return ConstructUserModel(findUserEntity);
        }
        public void Update(User user)
        {
            var updatableUserEntity = new UserEntity()
            {
                id = user.Id,
                firstname = user.FirstName,
                lastname = user.LastName,
                email = user.Email,
                password = user.Password,
                photo = user.Photo,
                favorite_book = user.FavoriteBook,
                favorite_movie = user.FavoriteMovie
            };

            if (this.userRepository.Update(updatableUserEntity) == 0) 
                throw new Exception();
        }
        private User ConstructUserModel(UserEntity userEntity)
        {
            var incomingMessages = messageService.GetIncomingMessagesByUserId(userEntity.id);
            var outgoingMessages = messageService.GetOutcomingMessagesByUserId(userEntity.id);
            var friendList = friendService.GetFriendsList(userEntity.id);

            return new User(userEntity.id,
                userEntity.firstname,
                userEntity.lastname,
                userEntity.password,
                userEntity.email,
                userEntity.photo,
                userEntity.favorite_book,
                userEntity.favorite_movie,
                incomingMessages,
                outgoingMessages,
                friendList);
        }
    }
}
