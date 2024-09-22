using System.Diagnostics.CodeAnalysis;
using System.Net;
using ADITUS.CodeChallenge.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ADITUS.CodeChallenge.Infrastructure.HttpServices
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseHttpService<TRequest, TResponse>
        : IHttpService<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<IHttpService<TRequest, TResponse>> _logger;

        protected BaseHttpService(HttpClient httpClient, ILogger<IHttpService<TRequest, TResponse>> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<TResponse> Send(TRequest request = null)
        {
            var response = await DoCall(request);
            return response;
        }

        public abstract HttpRequestMessage GetHttpRequestMessage(TRequest request = null);

        private async Task<TResponse> DoCall(TRequest request = null)
        {
            try
            {
                var httpRequestMessage = GetHttpRequestMessage(request);
                _logger.LogInformation($"==> Sending request to endpoint: {JsonConvert.SerializeObject(httpRequestMessage.RequestUri)}");
                _logger.LogInformation($"==> Request to send: {JsonConvert.SerializeObject(request)}");

                var response = await _httpClient.SendAsync(httpRequestMessage);
                response.EnsureSuccessStatusCode();

                _logger.LogInformation($"==> Endpoint Response : {response.ToString()}");
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return null;
                }

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var model = JsonConvert.DeserializeObject<TResponse>(responseContent);
                    _logger.LogInformation($"==> Endpoint Success Response : {responseContent}");
                    return model;
                }

                throw new ApplicationException($@"Getting from {httpRequestMessage.RequestUri} failed. 
                            StatusCode: {response.StatusCode} 
                            Reason: {response.ReasonPhrase}
                            ResponseContent: {responseContent}");
            }
            catch (Exception ex)
            {
                // Catches error when API is in circuit-opened mode
                _logger.LogError($"==> API is inoperative, please try later on.");
                _logger.LogError($"==> Error sending the request : Exception:{ex.Message}");
                _logger.LogError($"==>                           : InnerException:{ex.InnerException}");
                throw;
            }
        }
    }
}