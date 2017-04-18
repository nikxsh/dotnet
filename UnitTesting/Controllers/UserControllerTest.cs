using EFDataStorage.Contracts;
using Moq;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using UnitTesting.Fakes;
using WebApiServices.Controllers;

namespace UnitTesting.Controllers
{
    [TestFixture]
    public class UserControllerTest
    {
        private FakeUsersRepository _fakeUserData;
        private Mock<IUserRepository> _userRepository;

        [SetUp]
        public void SetUp()
        {
            _fakeUserData = new FakeUsersRepository();
            _userRepository = new Mock<IUserRepository>();
        }

        [Test]
        public void Get_Users_API_Is_Called()
        {
            //Arrange
            var pagingReqeust = new WebApiServices.Models.PagingRequest { PageNumber = 0, PageSize = 5, SearchString = string.Empty };
            var fakeUsers = _fakeUserData.GetUsers(pagingReqeust.PageSize, pagingReqeust.PageNumber, string.Empty);
            _userRepository.Setup(x => x.GetUsers(pagingReqeust.PageSize, pagingReqeust.PageNumber, string.Empty)).Returns(fakeUsers);
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
            var pagingReqeust = new WebApiServices.Models.PagingRequest { PageNumber = 0, PageSize = 5, SearchString = string.Empty };
            var fakeUsers = _fakeUserData.GetUsers(pagingReqeust.PageSize, pagingReqeust.PageNumber, string.Empty);
            _userRepository.Setup(x => x.GetUsers(pagingReqeust.PageSize, pagingReqeust.PageNumber, string.Empty)).Returns(fakeUsers);
            var controller = new UsersController(_userRepository.Object);


            //Act
            IHttpActionResult actionResult = controller.GetUsers(pagingReqeust);

            //Assert

            var result = actionResult as OkNegotiatedContentResult<IEnumerable<EFDataStorage.Entities.User>>;
            Assert.IsNotEmpty(result.Content);
            Assert.IsTrue(result.Content.Count() > 0);

        }

        [Test]
        public void Get_Users_Count()
        {
            _userRepository.Setup(x => x.GetUserCount()).Returns(_fakeUserData.FakeUsers.Count());
            var controller = new UsersController(_userRepository.Object);


            IHttpActionResult actionResult = controller.GetUserCount();


            var result = actionResult as OkNegotiatedContentResult<int>;
            Assert.AreEqual(result.Content, 6);
        }
    }
}
