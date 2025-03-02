using app.Services.ReservationAPI.Models.DTO;
using app.Services.ReservationAPI.Service.Interface;
using app.Services.ReservationAPI.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;

namespace app.Services.ReservationAPI.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _tokenProvider;
        public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            _httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
        }

        public async Task<ResponseDTO> SendAsync(RequestDTO request, bool withBearer = true)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("reservAPI");

                HttpRequestMessage message = new HttpRequestMessage();

                message.Headers.Add("Accept", "application/json");

                //token
                if (withBearer)
                {
                    var token = _tokenProvider.GetToken();

                    message.Headers.Add("Authorization", $"Bearer {token}");
                }

                message.RequestUri = new Uri(request.Url);

                if (request.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8, "application/json"); //converts data from object in text in json format
                }

                HttpResponseMessage apiResponse = null;

                switch (request.ApiType)
                {
                    case SD.ApiType.POST:

                        message.Method = HttpMethod.Post;

                        break;

                    case SD.ApiType.PUT:

                        message.Method = HttpMethod.Put;

                        break;

                    case SD.ApiType.DELETE:

                        message.Method = HttpMethod.Delete;

                        break;

                    default:
                        message.Method = HttpMethod.Get;

                        break;

                }

                apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };

                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };

                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Access Denied" };

                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" };

                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();

                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);

                        return apiResponseDto;

                }
            }
            catch (Exception ex)
            {
                var dto = new ResponseDTO
                {
                    IsSuccess = false,

                    Message = ex.Message.ToString()

                };
                return dto;
                
            }
            
        }


    }
}
