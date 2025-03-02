using app.Web.Models.DTO;
using app.Web.Service.IService;
using app.Web.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace app.Web.Service
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


        public async Task<ResponseDTO?> SendAsync(RequestDTO requestDTO, bool withBearer = true)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("appAPI");

                HttpRequestMessage message = new HttpRequestMessage();

                message.Headers.Add("Accept", "application/json");

                //token
                if (withBearer)
                {
                    var token = _tokenProvider.GetToken();

                    message.Headers.Add("Authorization", $"Bearer {token}");
                }

                message.RequestUri = new Uri(requestDTO.Url);

                if (requestDTO.Data != null)
                {
                    if (requestDTO.Data is AddCarDTO carDTO && carDTO.Image != null)
                    {                       
                        var content = new MultipartFormDataContent();

                        var imageContent = new StreamContent(carDTO.Image.OpenReadStream());

                        imageContent.Headers.ContentType = new MediaTypeHeaderValue(carDTO.Image.ContentType);

                        content.Add(imageContent, "ImageFile", carDTO.Image.FileName);

                        content.Add(new StringContent(carDTO.Make), "Make");

                        content.Add(new StringContent(carDTO.Model), "Model");

                        content.Add(new StringContent(carDTO.PricePerDay.ToString()), "PricePerDay");

                        content.Add(new StringContent(carDTO.Type), "Type");

                        content.Add(new StringContent(carDTO.Description), "Description");

                        content.Add(new StringContent(carDTO.Year.ToString()), "Year");

                        message.Content = content; // Ustaw zawartość tuta
                    }
                    else
                    {
                        message.Content = new StringContent(JsonConvert.SerializeObject(requestDTO.Data), Encoding.UTF8, "application/json"); //converts data from object in text in json format
                    }
                    
                }

                HttpResponseMessage apiResponse = null;

                switch (requestDTO.ApiType)
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
