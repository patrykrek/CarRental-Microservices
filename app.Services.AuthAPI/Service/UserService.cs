using app.Services.AuthAPI.Models.DTO;
using app.Services.AuthAPI.Repository.Interface;
using app.Services.AuthAPI.Service.IService;
using AutoMapper;
using System.Data.Common;

namespace app.Services.AuthAPI.Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ResponseDTO _response;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _response = new ResponseDTO();  
        }

        public async Task<ResponseDTO> DeleteUser(string userId)
        {
            try
            {
                var findUser = await _userRepository.FindUser(userId);

                if (findUser == null)
                {
                    _response.IsSuccess = false;

                    _response.Message = "User not found";
                }

                await _userRepository.DeleteUser(findUser);
            }
            catch (DbException dbEx)
            {
                _response.IsSuccess = false;

                _response.Message = dbEx.Message;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;

                _response.Message = ex.Message;
            }


            return _response;
           
        }

        public async Task<ResponseDTO> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetAllUsers();

                _response.Result =  users.Select(u => _mapper.Map<GetUserDTO>(u)).ToList();
                
            }
            catch(DbException dbEx)
            {
                _response.IsSuccess = false;

                _response.Message = dbEx.Message;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;

                _response.Message = ex.Message;
            }

            return _response;
        }
    }
}
