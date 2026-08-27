using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting.Testing;

using FluentAssertions;

using Projects;


namespace Workbench.L2Tests;

/// <summary>
///     Verifies the Workbench Aspire composition.
/// </summary>
public sealed class WorkbenchCompositionTests
{
    /// <summary>
    ///     Ensures the gateway starts and exposes its health endpoint.
    /// </summary>
    /// <returns>A task that represents the composition test.</returns>
    [Fact]
    public async Task GatewayShouldReportHealthy()
    {
        await using IDistributedApplicationTestingBuilder builder =
            await DistributedApplicationTestingBuilder.CreateAsync<Workbench_AppHost>();
        await using DistributedApplication application = await builder.BuildAsync();
        await application.StartAsync();
        await application.ResourceNotifications.WaitForResourceAsync("workbench-gateway", KnownResourceStates.Running);
        using HttpClient client = application.CreateHttpClient("workbench-gateway", "http");
        using HttpResponseMessage response = await client.GetAsync(new Uri("/health", UriKind.Relative));
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}