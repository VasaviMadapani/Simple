using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourism_Management_System_API.DTO;
using Tourism_Management_System_API.Models;
using Tourism_Management_System_API.Services;
using Tourism_Management_System_API_Project_.Data;

namespace Tourism_Management_test
{
    [TestFixture]
    public class UserRepositoryTest
    {
        private UserRepository _userRepository;
        //private UserTestDbContext _context;

        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<UserSearchDto> _userSearchDtoMock;
        private Mock<TourManagementSystemContext> _context;


        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userSearchDtoMock = new Mock<UserSearchDto>();
            _userRepositoryMock.Setup(obj => obj.SearchUsers(_userSearchDtoMock.Object)).Returns(GetUsers);
            _context = new Mock<TourManagementSystemContext>();
        }

        private IEnumerable<UserManagement> GetUsers()
        {
            List<UserManagement> users = new List<UserManagement>();
            users.Add(new UserManagement() { UserId = 1, Address = "abc", Email = "", FirstName = "", LastName = "", Role = "", Username = "", PhoneNumber = "", Password = "" });
            return users;
        }

        [TearDown]
        public void TearDown()
        {


        }

        [Test]
        public async Task AddUserAsync_ShouldAddUser()
        {
            // Arrange
            var user = new UserManagement
            {
                Username = "testuser",
                Email = "test@example.com",
                Address = "",
                FirstName = "",
                LastName = "",
                Password = "",
                PhoneNumber = "",
                Role = "",
                UserId = 1
            };

            // Act
            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveChangesAsync();

            // Assert
            var retrievedUser = await _userRepository.GetUserByEmailAsync("test@example.com");
            Assert.That(retrievedUser, Is.Not.Null, "The user should be found in the database.");
            Assert.That(retrievedUser.Username, Is.EqualTo("testuser"), "The retrieved username should match the added username.");
        }

        [Test]
        public async Task UserExistsByEmailAsync_ShouldReturnTrueIfUserExists()
        {
            // Arrange
            var user = new UserManagement
            {
                Username = "Pujitha SHARMA",
                Email = "pujitha@gmail.com",
                Address = "",
                FirstName = "",
                LastName = "",
                Password = "",
                PhoneNumber = "",
                Role = "",
                UserId = 1
            };
            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveChangesAsync();

            // Act
            var exists = await _userRepository.UserExistsByEmailAsync("pujitha@gmail.com");

            // Assert
            Assert.That(exists, Is.True, "The user should exist in the database.");
        }

        [Test]
        public async Task UserExistsByEmailAsync_ShouldReturnFalseIfUserDoesNotExist()
        {
            // Act
            var exists = await _userRepository.UserExistsByEmailAsync("nonexistent@example.com");

            // Assert
            Assert.That(exists, Is.False, "The user should not exist in the database.");
        }

        [Test]
        public async Task DeleteUserAsync_ShouldRemoveUser()
        {
            // Arrange
            var user = new UserManagement
            {
                Username = "deleteuser",
                Email = "delete@example.com",
                Address = "",
                FirstName = "",
                LastName = "",
                Password = "",
                PhoneNumber = "",
                Role = "",
                UserId = 1
            };
            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveChangesAsync();

            // Act
            await _userRepository.DeleteUserAsync(user);
            await _userRepository.SaveChangesAsync();

            // Assert
            var exists = await _userRepository.UserExistsByEmailAsync("delete@example.com");
            Assert.That(exists, Is.False, "The user should have been deleted from the database.");
        }
    }
}

