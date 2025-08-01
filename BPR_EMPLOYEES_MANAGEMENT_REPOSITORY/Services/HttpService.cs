using BPR_EMPLOYEES_MANAGEMENT_CORE.Enumerations;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Interface.Configuration;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Configuration;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Request;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Response;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPR_EMPLOYEES_MANAGEMENT_INFRASTRUCTURE.Services
{
    public class HttpService : IHttpService
    {

        private readonly IParseService _parseService;
        private readonly ILogService _logService;
        private readonly ConfigurationMessages _configurationMessages;
        private readonly ConfigurationWeb _configurationWeb;

        public HttpService(IParseService parseService, ILogService logService, IOptions<ConfigurationMessages> messagesDefault, IOptions<ConfigurationWeb> configurationWeb)
        {
            _parseService = parseService;
            _logService = logService;
            _configurationMessages = messagesDefault.Value;
            _configurationWeb = configurationWeb.Value;
        }

        public async Task<HttpServiceResponse<T>> POST<T>(HttpServiceRequest request)
        {
            HttpServiceResponse<T> response = new();
            try
            {
                HttpClient httpClient;
                if (!_configurationWeb.ValidCertificate)
                    httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }) { BaseAddress = request.Url };
                else
                    httpClient = new HttpClient { BaseAddress = request.Url };

                foreach (KeyValuePair<string, string> result in request.Headers)
                {
                    httpClient.DefaultRequestHeaders.Add(result.Key, result.Value);
                }

                httpClient.Timeout = TimeSpan.FromMilliseconds(request.TimeOut);

                StringContent content = new(request.Body, Encoding.UTF8, "application/json");

                using var responseAPI = await httpClient.PostAsync($"{request.Method}", content);
                string apiResponseString = await responseAPI.Content.ReadAsStringAsync();

                if (responseAPI.IsSuccessStatusCode)
                {
                    _logService.SaveLogApp($"[RESPONSE] [POST - HTTP CODE {responseAPI.StatusCode}] {apiResponseString}", LogType.Information);
                    response.ResponseCode = ResponseCode.Success;
                    response.DescriptionResponseCode = "Proceso realizado exitosamente.";
                    response.Data = _parseService.Deserealize<T>(apiResponseString);
                }
                else if (responseAPI.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    _logService.SaveLogApp($"[RESPONSE] [POST - HTTP CODE {responseAPI.StatusCode}] {apiResponseString}", LogType.Error);
                    response.ResponseCode = ResponseCode.Error;
                    response.DescriptionResponseCode = _parseService.Deserealize<BadRequestResponse>(apiResponseString).Message;
                }
                else
                {
                    _logService.SaveLogApp($"[RESPONSE] [POST - HTTP CODE {responseAPI.StatusCode}] {apiResponseString}", LogType.Error);
                    response.ResponseCode = ResponseCode.FatalError;
                    response.DescriptionResponseCode = _configurationMessages.FatalErrorMessage;
                }
            }
            catch (TaskCanceledException ex)
            {
                _logService.SaveLogApp($"[EXCEPTION] [POST - TimeoutException] {ex.Message} | {ex.StackTrace}", LogType.Error);
                response.ResponseCode = ResponseCode.TimeOut;
                response.DescriptionResponseCode = _configurationMessages.TimeoutMessage;
            }
            catch (Exception ex)
            {
                _logService.SaveLogApp($"[EXCEPTION] [POST - Exception] {ex.Message} | {ex.StackTrace}", LogType.Error);
                response.ResponseCode = ResponseCode.FatalError;
                response.DescriptionResponseCode = _configurationMessages.FatalErrorMessage;
            }
            return response;
        }

        public async Task<HttpServiceResponse<T>> GET<T>(HttpServiceRequest request)
        {
            HttpServiceResponse<T> response = new();
            try
            {
                HttpClient httpClient;
                if (!_configurationWeb.ValidCertificate)
                    httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }) { BaseAddress = request.Url };
                else
                    httpClient = new HttpClient { BaseAddress = request.Url };

                foreach (KeyValuePair<string, string> result in request.Headers)
                {
                    httpClient.DefaultRequestHeaders.Add(result.Key, result.Value);
                }

                httpClient.Timeout = TimeSpan.FromMilliseconds(request.TimeOut);

                StringContent content = new(request.Body, Encoding.UTF8, "application/json");

                _logService.SaveLogApp($"[REQUEST] [GET - HTTP CODE] {_parseService.Serialize(request)}", LogType.Information);

                using var responseAPI = await httpClient.GetAsync($"{request.Method}");
                string apiResponseString = await responseAPI.Content.ReadAsStringAsync();

                if (responseAPI.IsSuccessStatusCode)
                {
                    _logService.SaveLogApp($"[RESPONSE] [GET - HTTP CODE {responseAPI.StatusCode}] {apiResponseString}", LogType.Information);
                    response.ResponseCode = ResponseCode.Success;
                    response.DescriptionResponseCode = "Proceso realizado exitosamente.";
                    response.Data = _parseService.Deserealize<T>(apiResponseString);
                }
                else if (responseAPI.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    _logService.SaveLogApp($"[RESPONSE] [GET - HTTP CODE {responseAPI.StatusCode}] {apiResponseString}", LogType.Error);
                    response.ResponseCode = ResponseCode.Error;
                    response.DescriptionResponseCode = _parseService.Deserealize<BadRequestResponse>(apiResponseString).Message;
                }
                else
                {
                    _logService.SaveLogApp($"[RESPONSE] [GET - HTTP CODE {responseAPI.StatusCode}] {apiResponseString}", LogType.Error);
                    response.ResponseCode = ResponseCode.FatalError;
                    response.DescriptionResponseCode = _configurationMessages.FatalErrorMessage;
                }
            }
            catch (TaskCanceledException ex)
            {
                _logService.SaveLogApp($"[EXCEPTION] [GET - TimeoutException] {ex.Message} | {ex.StackTrace}", LogType.Error);
                response.ResponseCode = ResponseCode.TimeOut;
                response.DescriptionResponseCode = _configurationMessages.TimeoutMessage;
            }
            catch (Exception ex)
            {
                _logService.SaveLogApp($"[EXCEPTION] [GET - Exception] {ex.Message} | {ex.StackTrace}", LogType.Error);
                response.ResponseCode = ResponseCode.FatalError;
                response.DescriptionResponseCode = _configurationMessages.FatalErrorMessage;
            }
            return response;
        }

        private class BadRequestResponse
        {
            public string Message { get; set; }
        }

    }
}
