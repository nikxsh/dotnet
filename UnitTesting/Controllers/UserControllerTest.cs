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
        public void Get_Users_API_Is_Called()
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

            //Ok()
            Assert.IsInstanceOf(typeof(OkNegotiatedContentResult<IEnumerable<EFDataStorage.Entities.User>>), actionResult);
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

            var result = actionResult as OkNegotiatedContentResult<ResponseBase<IEnumerable<EFDataStorage.Entities.User>>>;
            Assert.IsNotEmpty(result.Content.Data);
            Assert.IsTrue(result.Content.Data.Count() > 0);

        }

        [Test]
        public void Get_Users_Count()
        {

            var request = UserManager.PrepareRequest(new RequestBase());
            _userRepository.Setup(x => x.GetUserCount(request)).Returns(new ResponseBase<int>() { Data = _fakeUserData.FakeUsers.Count(), Status = true });
            var controller = new UsersController(_userRepository.Object);


            IHttpActionResult actionResult = controller.GetUserCount();


            var result = actionResult as OkNegotiatedContentResult<ResponseBase<int>>;
            Assert.AreEqual(result.Content, 6);
        }
    }
}
