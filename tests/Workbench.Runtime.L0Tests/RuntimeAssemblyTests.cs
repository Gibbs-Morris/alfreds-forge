using FluentAssertions;

using Workbench.Runtime;


namespace Workbench.Runtime.L0Tests;

/// <summary>
///     Verifies the Workbench runtime marker.
/// </summary>
public sealed class RuntimeAssemblyTests
{
    /// <summary>
    ///     Ensures the runtime marker keeps the canonical product name.
    /// </summary>
    [Fact]
    public void ValueShouldEqualProductName()
    {
        RuntimeAssemblyMarker.Value.Should().Be("AlfredsForge");
        typeof(RuntimeAssemblyMarker).Assembly.GetName().Name.Should().Be("Workbench.Runtime");
    }
}