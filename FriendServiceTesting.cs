
using NUnit.Framework;
using SocialNetwork.Business_Logic_Layer.Models;
using SocialNetwork.Business_Logic_Layer.Services;


namespace SocialNetwork.Tests
{
    [TestFixture]
    public class FriendServiceTesting
    {
        [Test]
        public void AddFriendShouldCreateFriendWithRightEmail()
        {
            UserService userService = new UserService();
            FriendService friendService = new FriendService();

            userService.Register(new UserRegistrationData { Email = "ca@gmail.com", Password = "savva123", FirstName = "Savva", LastName = "Chalcev" });
            userService.Register(new UserRegistrationData { Email = "ca2@gmail.com", Password = "savva1234", FirstName = "Irma", LastName = "Chalceva" });
           

            
            FriendRequestData friendData = new FriendRequestData { SenderId = 1, FriendEmail = "ca2@gmail.com" };
            friendService.AddFriend(friendData);
            Assert.That(friendService.GetFriendsList(1).First().Email == "ca2@gmail.com");
        }
    }
    
}