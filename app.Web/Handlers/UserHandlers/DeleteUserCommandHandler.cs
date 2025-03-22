using app.Web.Commands.UserCommands;
using app.Web.Models.DTO;
using app.Web.Service.IService;
using MediatR;

namespace app.Web.Handlers.UserHandlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ResponseDTO>
    {
        private readonly IUserService _userService;

        public DeleteUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ResponseDTO> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDTO();

            response.Result = await _userService.DeleteUsers(request.Id);

            return response;
        }
    }
}
