using app.Web.Commands.AuthCommands;
using app.Web.Models.DTO;
using app.Web.Service.IService;
using MediatR;

namespace app.Web.Handlers.AuthHandlers
{
    public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, ResponseDTO>
    {
        private readonly IAuthService _authService;

        public AssignRoleCommandHandler(IAuthService authService)
        {
            _authService = authService;

        }

        public async Task<ResponseDTO> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDTO();

            response.Result = await _authService.AssignRole(request.request);

            return response;
        }
    }
}
