using Soenneker.Azure.OpenAI.Client.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;


namespace Soenneker.Azure.OpenAI.Client.Tests;

[Collection("Collection")]
public class AzureOpenAIClientTests : FixturedUnitTest
{
    private readonly IAzureOpenAIClientUtil _util;

    public AzureOpenAIClientTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<IAzureOpenAIClientUtil>(true);
    }
}
