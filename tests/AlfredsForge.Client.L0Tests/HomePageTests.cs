using AlfredsForge.Client.Pages;

using Bunit;

using FluentAssertions;

namespace AlfredsForge.Client.L0Tests;

/// <summary>
///     Verifies the initial client shell.
/// </summary>
public sealed class HomePageTests
{
    /// <summary>
    ///     Ensures the home page renders the product heading.
    /// </summary>
    [Fact]
    public void HomePageShouldRenderProductHeading()
    {
        using BunitContext context = new();

        using IRenderedComponent<Home> rendered = context.Render<Home>();

        rendered.Find("h1").TextContent.Should().Be("Alfred's Forge");
    }
}
