using Moq;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using UnitTesting.Fakes;
using WebApiServices.Controllers;
using WebApiServices.Adapter;
using WebApiServices.Helper;
using WebApiServices.Models;

namespace UnitTesting.Controllers
{
    [TestFixture]
    public class UserControllerTest
    {
        private FakeUsersRepository _fakeUserData;
        private Mock<IUserAdapter> _userRepository;

        [SetUp]
        public void SetUp()
        {
            _fakeUserData = new FakeUsersRepository();
            _userRepository = new Mock<IUserAdapter>();
        }

        [Test]
        public void Get_users_api_should_be_called()
        {
            /// Arrange
            /// Create your mock object and pass that to System Under Test (SUT)
            _userRepository.Setup(x => x.GetUsers(It.IsAny<RequestBase<PagingRequest>>()));
            var controller = new UsersController(_userRepository.Object);
            
            /// Act
            /// Execute SUT
            IHttpActionResult actionResult = controller.GetUsers(new PagingRequest { });

            /// Assert
            /// Verify SUT's interaction with the mock object
            _userRepository.VerifyAll();
        }

        [Test]
        public void Save_should_be_called_per_user()
        {
            /// Arrange
            /// Create your mock object and pass that to System Under Test (SUT)
            var users = new List<User>()
            {
                new User { FirstName = "test1" },
                new User { FirstName = "test2" },
                new User { FirstName = "test3" },
                new User { FirstName = "test4" },
            };
            var controller = new UsersController(_userRepository.Object);

            /// Act
            /// Execute SUT
            IHttpActionResult actionResult = controller.BulkSave(users);

            /// Assert
            /// Verify SUT's interaction with the mock object
            _userRepository.Verify(x => x.SaveUser(It.IsAny<RequestBase<User>>()), Times.Exactly(users.Count));

        }

        [Test]
        public void Get_Filtered_Users()
        {
            //Arrange
            var pagingReqeust = new PagingRequest { PageNumber = 0, PageSize = 5, SearchString = string.Empty };

            var request = UserManager.PrepareRequest(new RequestBase<PagingRequest>(pagingReqeust));
            var fakeUsers = _fakeUserData.GetUsers(request);
            _userRepository.Setup(x => x.GetUsers(request)).Returns(fakeUsers);

            var controller = new UsersController(_userRepository.Object);


            //Act
            IHttpActionResult actionResult = controller.GetUsers(pagingReqeust);

            //Assert

            var result = actionResult as OkNegotiatedContentResult<ResponseBase<IEnumerable<User>>>;
            Assert.IsNotEmpty(result.Content.ResponseData);
            Assert.IsTrue(result.Content.ResponseData.Count() > 0);

        }

        [Test]
        public void Get_Users_Count()
        {

            var request = UserManager.PrepareRequest(new RequestBase());
            var fakeResponse = new ResponseBase<int>() { ResponseData = _fakeUserData.FakeUsers.Count(), Status = true };
            _userRepository.Setup(x => x.GetUserCount(request)).Returns(fakeResponse);
            var controller = new UsersController(_userRepository.Object);


            IHttpActionResult actionResult = controller.GetUserCount();


            var result = actionResult as OkNegotiatedContentResult<ResponseBase<int>>;
            Assert.AreEqual(result.Content.ResponseData, 6);
        }
    }
}
