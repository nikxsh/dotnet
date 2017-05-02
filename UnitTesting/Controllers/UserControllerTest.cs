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
using System;
using NUnit.Framework.Constraints;
using WebApiServices.Contracts;

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
        public void Bulk_save_should_be_called_per_user()
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
        public void Bulk_save_should_be_called_mulitiple_methods()
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

            var pagingReqeust = new PagingRequest { PageNumber = 0, PageSize = 5, SearchString = string.Empty };
            var request = UserManager.PrepareRequest(new RequestBase<PagingRequest>(pagingReqeust));

            var fakeUsers = _fakeUserData.GetUsers(request);
            _userRepository.Setup(x => x.GetUsers(It.IsAny<RequestBase<PagingRequest>>())).Returns(fakeUsers);

            _userRepository.Setup(x => x.SaveUser(It.IsAny<RequestBase<User>>())).Returns((RequestBase<User> user) =>
            {
                //if (user.Data.FirstName == "test2")
                    return new ResponseBase { Status = true };
                //else
                    //return new ResponseBase { Status = false };
            });

            /// Act
            /// Execute SUT
            IHttpActionResult actionResult = controller.BulkSave(users);

            /// Assert
            /// Verify SUT's interaction with the mock object
            _userRepository.Verify(x => x.SaveUser(It.IsAny<RequestBase<User>>()), Times.Exactly(users.Count));

            _userRepository.Verify(x => x.GetUsers(It.IsAny<RequestBase<PagingRequest>>()), Times.Exactly(users.Count));

        }
        [Test]
        public void Get_Filtered_Users()
        {
            //Arrange
            var pagingReqeust = new PagingRequest { PageNumber = 0, PageSize = 5, SearchString = string.Empty };
            var request = UserManager.PrepareRequest(new RequestBase<PagingRequest>(pagingReqeust));

            var fakeUsers = _fakeUserData.GetUsers(request);
            _userRepository.Setup(x => x.GetUsers(It.IsAny<RequestBase<PagingRequest>>())).Returns(fakeUsers);

            var controller = new UsersController(_userRepository.Object);


            //Act
            IHttpActionResult actionResult = controller.GetUsers(pagingReqeust);

            //Assert
            _userRepository.Verify(x => x.GetUsers(It.IsAny<RequestBase<PagingRequest>>())); //Verify this method should get called
            var result = actionResult as OkNegotiatedContentResult<ResponseBase<IEnumerable<User>>>;
            Assert.IsNotEmpty(result.Content.ResponseData);
            Assert.IsTrue(result.Content.ResponseData.Count() > 0);

        }

        [Test]
        public void Get_Users_Count()
        {
            //Arrange
            var request = UserManager.PrepareRequest(new RequestBase());
            var fakeResponse = new ResponseBase<int>() { ResponseData = _fakeUserData.FakeUsers.Count(), Status = true };
            _userRepository.Setup(x => x.GetUserCount(It.IsAny<RequestBase>())).Returns(fakeResponse);
            var controller = new UsersController(_userRepository.Object);

            //Act
            IHttpActionResult actionResult = controller.GetUserCount();

            //Assert
            _userRepository.VerifyAll(); //Verify this method should get called
            var result = actionResult as OkNegotiatedContentResult<ResponseBase<int>>;
            Assert.AreEqual(result.Content.ResponseData, 6);
        }

        [Test]
        public void Correct_arguments_should_be_passed_to_method()
        {
            //Arrange
            var userDTO1 = new User { FirstName = "test1" };
            var userDTO2 = new User { FirstName = "test2" };
            _userRepository.Setup(x => x.SaveUser(It.IsAny<RequestBase<User>>()));
            var controller = new UsersController(_userRepository.Object);

            //Act
            IHttpActionResult actionResult = controller.Save(userDTO1);

            //Assert
            _userRepository.Verify(x => x.SaveUser(It.Is<RequestBase<User>>(y => y.Data.Equals(userDTO1))));
        }

        [Test]
        public void Correct_condtion_should_be_executed()
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

            _userRepository.Setup(x => x.SaveUser(It.Is<RequestBase<User>>(y => y.Data.FirstName == "test1"))).Returns(new ResponseBase { Status = true });

            var controller = new UsersController(_userRepository.Object);

            /// Act
            /// Execute SUT
            IHttpActionResult actionResult = controller.BulkSave(users);

            /// Assert
            /// Verify SUT's interaction with the mock object
            _userRepository.Verify(x => x.GetUsers(It.IsAny<RequestBase<PagingRequest>>()));
        }

        [Test]
        public void Exeception_should_be_raised_fro_empty_user()
        {
            /// Arrange
            _userRepository.Setup(x => x.SaveUser(It.IsAny<RequestBase<User>>()));

            var controller = new UsersController(_userRepository.Object);

            /// Act
            var exception = Assert.Throws<NullReferenceException>(() => controller.Save(null));
            Assert.That(exception.Message, Does.Contain("User Object can not be Null"));
        }        
    }
}
