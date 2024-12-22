using Soenneker.Azure.OpenAI.Client.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Azure.OpenAI.Client.Tests;

[Collection("Collection")]
public class AzureOpenAIClientUtilTests : FixturedUnitTest
{
    private readonly IAzureOpenAIClientUtil _util;

    public AzureOpenAIClientUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<IAzureOpenAIClientUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
