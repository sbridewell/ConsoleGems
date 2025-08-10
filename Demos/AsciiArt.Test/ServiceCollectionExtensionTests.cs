// <copyright file="ServiceCollectionExtensionTests.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.Test
{
    using System;
    using System.Runtime.Versioning;
    using FluentAssertions;
    using Microsoft.Extensions.DependencyInjection;
    using Sde.AsciiArt;
    using Sde.AsciiArt.Generators;
    using Xunit;

    /// <summary>
    /// Contains unit tests for the ServiceCollectionExtension class.
    /// </summary>
    public class ServiceCollectionExtensionTests
    {
        /// <summary>
        /// Verifies that AddAsciiArt registers IAsciiArtGenerator and IConsoleColourMapper in the DI container.
        /// </summary>
        [SupportedOSPlatform("windows")]
        [Fact]
        public void AddAsciiArt_RegistersIAsciiArtGenerator()
        {
            var services = new ServiceCollection();
            services.AddAsciiArt();
            var provider = services.BuildServiceProvider();
            var generator = provider.GetService<IAsciiArtGenerator>();
            generator.Should().NotBeNull();
            generator.Should().BeOfType<AsciiArtGenerator>();
        }
    }
}
