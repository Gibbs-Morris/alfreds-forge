using FluentAssertions;


namespace AlfredsForge.Domain.L0Tests;

/// <summary>
///     Verifies baseline domain assembly marker behavior.
/// </summary>
public sealed class DomainAssemblyMarkerTests
{
    /// <summary>
    ///     Ensures the marker constant keeps the canonical product name.
    /// </summary>
    [Fact]
    public void ValueShouldEqualProductName()
    {
        DomainAssemblyMarker.Value.Should().Be("AlfredsForge");
    }
}