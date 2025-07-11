// <copyright file="ServiceCollectionExtensions.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.SnakeGame
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Extensions.DependencyInjection;
    using Sde.ConsoleGems.Painters;

    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/> to register services for the Snake Game.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the Maze Game services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The updated service collection.</returns>
        [ExcludeFromCodeCoverage]
        public static IServiceCollection AddSnakeGame(
            this IServiceCollection services)
        {
            services.AddSingleton<ISnakeGameController, SnakeGameController>();
            services.AddSingleton<IStatusPainter, StatusPainter>();
            services.AddSingleton<ISnakeGamePainter, SnakeGamePainter>();
            services.AddSingleton<IGame, Game>();
            services.AddSingleton<ISnake, Snake>();
            services.AddSingleton<ISnakeGameRandomiser, SnakeGameRandomiser>();
            return services;
        }
    }
}
