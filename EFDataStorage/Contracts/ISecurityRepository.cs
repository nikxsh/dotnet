using EFDataStorage.Helper;
using EFDataStorage.Patterns;

namespace EFDataStorage.Contracts
{
    public interface ISecurityRepository :
        ISelect<LoginRequest, LoginResponse>
    {
    }
}
