using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Sica.Handler.common;

public static class LambdaExceptionHandler
{
    public static async Task<APIGatewayProxyResponse> Handle(Func<Task<LambdaResponseDto<object>>> action)
    {
        try
        {
            var response = await action();
            return CreateResponse(HttpStatusCode.OK, response);
        }
        catch (JsonException ex)
        {
            //Logger.LogError(ex, "Invalid JSON.");
            return CreateResponse(HttpStatusCode.BadRequest, LambdaResponseDto<string>.Fail("Invalid JSON format."));
        }
        catch (HttpRequestException ex)
        {
           // Logger.LogError(ex, "External API error.");
            return CreateResponse(HttpStatusCode.BadGateway, LambdaResponseDto<string>.Fail("External service error: " + ex.Message));
        }
        catch (ArgumentException ex)
        {
            //Logger.LogWarning(ex, "Validation error.");
            return CreateResponse(HttpStatusCode.BadRequest, LambdaResponseDto<string>.Fail(ex.Message));
        }
        catch (Exception ex)
        {
            //Logger.LogError(ex, "Unexpected error.");
            return CreateResponse(HttpStatusCode.InternalServerError, LambdaResponseDto<string>.Fail("Internal server error."));
        }
    }

    private static APIGatewayProxyResponse CreateResponse<T>(HttpStatusCode statusCode, LambdaResponseDto<T> body)
    {
        return new APIGatewayProxyResponse
        {
            StatusCode = (int)statusCode,
            Body = JsonSerializer.Serialize(body),
            Headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" },
                { "Access-Control-Allow-Origin", "*" }
            }
        };
    }
}
