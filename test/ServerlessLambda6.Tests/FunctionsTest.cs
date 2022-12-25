using Xunit;
using System.Text.Json;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.APIGatewayEvents;

namespace ServerlessLambda6.Tests;
public class FunctionTest
{
    [Fact]
    public void TestGetMethodUnnamed()
    {
        TestLambdaContext context;
        APIGatewayProxyRequest request;
        APIGatewayProxyResponse response;

        // Mock request data
        request = new APIGatewayProxyRequest()
        {
            Body = "{}"
        };
        context = new TestLambdaContext();
        response = Functions.Main(request, context);
        Assert.Equal(200, response.StatusCode);
        Assert.Equal("Hello AWS Serverless, use body 'name' to greeting", response.Body);
    }

    [Fact]
    public void TestGetMethodNamed()
    {
        TestLambdaContext context;
        APIGatewayProxyRequest request;
        APIGatewayProxyResponse response;

        // Mock request data
        request = new APIGatewayProxyRequest()
        {
            Body = JsonSerializer.Serialize(
                new
                {
                    name = "Gamal"
                })
        };
        context = new TestLambdaContext();
        response = Functions.Main(request, context);
        Assert.Equal(200, response.StatusCode);
        Assert.Equal("Hello Gamal, Nice to see u :)!", response.Body);
    }
}
