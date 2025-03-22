using app.Web.Models.DTO;
using app.Web.Queries.UserQueries;
using app.Web.Service;
using app.Web.Service.IService;
using MediatR;

namespace app.Web.Handlers.UserHandlers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ResponseDTO>
    {
        private readonly IUserService _userService;

        public GetAllUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ResponseDTO> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDTO();

            response = await _userService.GetUsers();

            return response;
        }
    }
}
