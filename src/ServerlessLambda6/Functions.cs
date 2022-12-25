using System.Net;
using System.Text.Json;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;

namespace ServerlessLambda6;
public class Functions
{
    // Attribute to enable the Lambda function's JSON input to be converted into a .NET class.
    [LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
    /// <summary>
    /// A Lambda function to respond to HTTP Get methods from API Gateway
    /// </summary>
    /// <param name="request"></param>
    /// <returns>The API Gateway response.</returns>
    public static APIGatewayProxyResponse Main(APIGatewayProxyRequest request, ILambdaContext context)
    {
        context.Logger.LogInformation($"{request.HttpMethod} Request\n");

        var bodyRequest = JsonSerializer.Deserialize<JsonElement>(request.Body);
        var isNamed = bodyRequest.TryGetProperty("name", out var name);

        var bodyResponse = isNamed ?
            $"Hello {name}, Nice to see u :)!" :
            "Hello AWS Serverless, use body 'name' to greeting";

        var response = new APIGatewayProxyResponse
        {
            IsBase64Encoded = false,
            StatusCode = (int)HttpStatusCode.OK,
            Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } },
            Body = bodyResponse
        };

        return response;
    }
}
