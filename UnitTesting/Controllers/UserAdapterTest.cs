using EFDataStorage.Contracts;
using Moq;
using NUnit.Framework;
using UnitTesting.Fakes;

namespace UnitTesting.Controllers
{
    [TestFixture]
    class UserAdapterTest
    {
        private FakeUsersRepository _fakeUserData;
        private Mock<IUserRepository> _userRepository;

        [SetUp]
        public void SetUp()
        {
            _fakeUserData = new FakeUsersRepository();
            _userRepository = new Mock<IUserRepository>();
        }
    }
}
