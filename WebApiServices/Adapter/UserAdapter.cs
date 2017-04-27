using EFDataStorage.Contracts;
using System;
using System.Collections.Generic;
using WebApiServices.Contracts;
using WebApiServices.Helper;
using WebApiServices.Mapper;
using WebApiServices.Models;

namespace WebApiServices.Adapter
{
    public class UserAdapter : IUserAdapter
    {
        private readonly IUserRepository _userRepository;

        public UserAdapter(IUserRepository UserReposiorty)
        {
            _userRepository = UserReposiorty;
        }

        public ResponseBase<IEnumerable<User>> GetUsers(RequestBase<PagingRequest> request)
        {
            var response = new ResponseBase<IEnumerable<User>>();

            try
            {
                var result = _userRepository.Select(request.Data.ToRepositoryPagingRequest());
                var responseData = result.BuildAPIUserList();
                return new ResponseBase<IEnumerable<User>> { Status = true, ResponseData = responseData };
            }
            catch (Exception ex)
            {
                response = ex.ToAdapterResponseBase<IEnumerable<User>>();
                response.Errors = new List<Error> { new Error { ErrorCode = 520, ErrorMessage = ex.Message } };
                response.Status = false;
            }

            return response;
        }

        public ResponseBase<User> GetUserById(RequestBase<Guid> request)
        {
            var response = new ResponseBase<User>();

            try
            {
                var result = _userRepository.Select(new EFDataStorage.Helper.GetUserById { UserId = request.Data });
                var responseData = result.BuildAPIUser();
                return new ResponseBase<User> { Status = true, ResponseData = responseData };
            }
            catch (Exception ex)
            {
                response = ex.ToAdapterResponseBase<User>();
                response.Errors = new List<Error> { new Error { ErrorCode = 520, ErrorMessage = ex.Message } };
                response.Status = false;
            }

            return response;
        }

        public ResponseBase<int> GetUserCount(RequestBase request)
        {
            var response = new ResponseBase<int>();

            try
            {
                var result = _userRepository.Select();
                return new ResponseBase<int> { Status = true, ResponseData = result };
            }
            catch (Exception ex)
            {
                response = ex.ToAdapterResponseBase<int>();
                response.Errors = new List<Error> { new Error { ErrorCode = 520, ErrorMessage = ex.Message } };
                response.Status = false;
            }

            return response;
        }

        public ResponseBase SaveUser(RequestBase<User> request)
        {
            var response = new ResponseBase();
            try
            {
                request.Data.Id = Guid.NewGuid();
                var user = request.Data.BuildAddUserCommand(); 
                var result = _userRepository.Execute(user);
                if (result.AffectedRecords > 0)
                    return new ResponseBase { Status = true, Message = "User added" };
                else
                    return new ResponseBase { Status = false, Message = "Failed to add new User" };
            }
            catch (Exception ex)
            {
                response = ex.ToAdapterResponseBase<IEnumerable<User>>();
                response.Errors = new List<Error> { new Error { ErrorCode = 520, ErrorMessage = ex.Message } };
                response.Status = false;
            }
            return response;
        }


        public ResponseBase EditUser(RequestBase<User> request)
        {
            try
            {
                var user = request.Data.BuildEditUserCommand();
                var result = _userRepository.Execute(user);
                if (result.AffectedRecords > 0)
                    return new ResponseBase { Status = true, Message = "User updated" };
                else
                    return new ResponseBase { Status = false, Message = "Failed to update User" };
            }
            catch (Exception ex)
            {
                var response = ex.ToAdapterResponseBase<IEnumerable<User>>();
                response.Errors = new List<Error> { new Error { ErrorCode = 520, ErrorMessage = ex.Message } };
                response.Status = false;
                return response;
            }
        }

        public ResponseBase DeleteUser(RequestBase<Guid> request)
        {
            var response = new ResponseBase();

            try
            {
                var result = _userRepository.Execute(new EFDataStorage.Helper.DeleteUser { UserId = request.Data });
                if (result.AffectedRecords > 0)
                    return new ResponseBase { Status = true, Message = "User Deleted" };
                else
                    return new ResponseBase { Status = false , Message = "Failed to Delete User" };
            }
            catch (Exception ex)
            {
                response = ex.ToAdapterResponseBase<IEnumerable<User>>();
                response.Errors = new List<Error> { new Error { ErrorCode = 520, ErrorMessage = ex.Message } };
                response.Status = false;
            }
            return response;
        }
    }
}