using Soenneker.Azure.OpenAI.Client.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Azure.OpenAI.Client.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class AzureOpenAIClientUtilTests : HostedUnitTest
{
    private readonly IAzureOpenAIClientUtil _util;

    public AzureOpenAIClientUtilTests(Host host) : base(host)
    {
        _util = Resolve<IAzureOpenAIClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
