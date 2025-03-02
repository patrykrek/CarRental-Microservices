using app.Web.Commands.AuthCommands;
using app.Web.Models.DTO;
using app.Web.Service.IService;
using MediatR;

namespace app.Web.Handlers.AuthHandlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ResponseDTO>
    {
        private readonly IAuthService _authService;
        private readonly ResponseDTO _response;

        public RegisterCommandHandler(IAuthService authService)
        {
            _authService = authService;
            _response = new ResponseDTO();
        }

        public async Task<ResponseDTO> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {

            _response.Result = await _authService.Register(request.RequestDTO);

            return _response;
        }
    }
}
