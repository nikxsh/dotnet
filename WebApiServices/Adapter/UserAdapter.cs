using EFDataStorage.Contracts;
using System;
using System.Collections.Generic;
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
                var result = _userRepository.GetUsers(request.Data.PageSize, request.Data.PageNumber, request.Data.SearchString);
                var responseData = result.ToAPIUsers();
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
                var result = _userRepository.GetUserById(request.Data);
                var responseData = result.ToAPIUser();
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
                var result = _userRepository.GetUserCount();
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

        public ResponseBase<IEnumerable<KeyValuePair<Guid, string>>> GlobalSearch(RequestBase<string> request)
        {
            var response = new ResponseBase<IEnumerable<KeyValuePair<Guid, string>>>();

            try
            {
                var result = _userRepository.GlobalSearch(request.Data);
                return new ResponseBase<IEnumerable<KeyValuePair<Guid, string>>> { Status = true, ResponseData = result };
            }
            catch (Exception ex)
            {
                response = ex.ToAdapterResponseBase<IEnumerable<KeyValuePair<Guid, string>>>();
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
                var userData = new EFDataStorage.Entities.User
                {
                    Id = Guid.NewGuid(),
                    UserName = request.Data.UserName,
                    FirstName = request.Data.FirstName,
                    LastName = request.Data.LastName,
                    Email = request.Data.Email,
                    Dob = DateTime.Parse(request.Data.Dob)
                };

                var result = _userRepository.SaveUser(userData);
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
            var response = new ResponseBase();
            try
            {
                var userData = new EFDataStorage.Entities.User
                {
                    Id = request.Data.Id,
                    UserName = request.Data.UserName,
                    FirstName = request.Data.FirstName,
                    LastName = request.Data.LastName,
                    Email = request.Data.Email,
                    Dob = DateTime.Parse(request.Data.Dob)
                };

                var result = _userRepository.EditUser(userData);
                if (result.AffectedRecords > 0)
                    return new ResponseBase { Status = true, Message = "User updated" };
                else
                    return new ResponseBase { Status = false, Message = "Failed to update User" };
            }
            catch (Exception ex)
            {
                response = ex.ToAdapterResponseBase<IEnumerable<User>>();
                response.Errors = new List<Error> { new Error { ErrorCode = 520, ErrorMessage = ex.Message } };
                response.Status = false;
            }
            return response;
        }

        public ResponseBase DeleteUser(RequestBase<Guid> request)
        {
            var response = new ResponseBase();

            try
            {
                var result = _userRepository.DeleteUser(request.Data);
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