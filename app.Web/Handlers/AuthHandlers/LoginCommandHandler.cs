using app.Web.Commands.AuthCommands;
using app.Web.Models.DTO;
using app.Web.Service.IService;
using MediatR;

namespace app.Web.Handlers.AuthHandlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ResponseDTO>
    {
        private readonly IAuthService _authService;

        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<ResponseDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            ResponseDTO response = await _authService.Login(request.LoginRequestDTO);

            return response;

        }
    }
}
