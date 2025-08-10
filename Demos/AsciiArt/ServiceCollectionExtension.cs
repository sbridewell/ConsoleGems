// <copyright file="ServiceCollectionExtension.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt
{
    using System.Runtime.Versioning;
    using Microsoft.Extensions.DependencyInjection;
    using Sde.AsciiArt.Generators;

    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/> to register services for the Maze Game.
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Adds the ASCII art services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The updated service collection.</returns>
        [SupportedOSPlatform("windows")]
        public static IServiceCollection AddAsciiArt(
            this IServiceCollection services)
        {
            services.AddSingleton<IColourMapperPrompter, ColourMapperPrompter>();
            services.AddSingleton<ICharacterBlenderPrompter, CharacterBlenderPrompter>();
            services.AddSingleton<IAsciiArtGenerator, AsciiArtGenerator>();
            Directory.SetCurrentDirectory("TestImages");
            return services;
        }
    }
}
