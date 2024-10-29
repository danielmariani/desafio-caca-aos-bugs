using Dima.Core.Models;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace Dima.Core.Responses;

public class Response<TData>
{
    private readonly int _code;

    [JsonConstructor]
    public Response()
        => _code = Configuration.DefaultStatusCode;


    /// <summary>
    /// Construtor capaz de repassar o status code do back-end para o tratamento da tela.
    /// </summary>
    public static async Task<Response<TData?>?> FromHttpResponse(HttpResponseMessage httpMessage)
    {
        var responseContent = await httpMessage.Content.ReadFromJsonAsync<Response<TData?>>();
        return responseContent == null ? null
            : new Response<TData?>(responseContent.Data, httpMessage.StatusCode, responseContent.Message);
    }

    public Response(
        TData? data,
        HttpStatusCode code = HttpStatusCode.OK,
        string? message = null)
    {
        Data = data;
        Message = message;
        _code = (int)code;
    }

    public TData? Data { get; set; }
    public string? Message { get; set; }

    [JsonIgnore]
    public bool IsSuccess
        => _code is >= 200 and <= 299;
}