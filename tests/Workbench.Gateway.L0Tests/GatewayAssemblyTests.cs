using FluentAssertions;


namespace Workbench.Gateway.L0Tests;

/// <summary>
///     Verifies the Workbench gateway assembly identity.
/// </summary>
public sealed class GatewayAssemblyTests
{
    /// <summary>
    ///     Ensures the gateway host is built as the Workbench gateway assembly.
    /// </summary>
    [Fact]
    public void GatewayHostShouldUseWorkbenchAssembly()
    {
        typeof(Workbench.Gateway.GatewayAssemblyMarker).Assembly.GetName().Name.Should().Be("Workbench.Gateway");
    }
}
