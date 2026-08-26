using FluentAssertions;


namespace Workbench.Client.L0Tests;

/// <summary>
///     Verifies the Workbench client identity.
/// </summary>
public sealed class AppTests
{
    /// <summary>
    ///     Ensures the application shell belongs to the Workbench client assembly.
    /// </summary>
    [Fact]
    public void AppShouldUseWorkbenchAssembly()
    {
        typeof(App).Assembly.GetName().Name.Should().Be("Workbench.Client");
    }
}