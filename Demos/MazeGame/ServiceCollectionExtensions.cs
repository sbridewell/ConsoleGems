// <copyright file="ServiceCollectionExtensions.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame
{
    using Microsoft.Extensions.DependencyInjection;
    using Sde.ConsoleGems.FullScreen;
    using Sde.MazeGame.CharacterProviders;
    using Sde.MazeGame.KeyPressHandlers;
    using Sde.MazeGame.Painters.Map;
    using Sde.MazeGame.Painters.Pov;
    using Sde.MazeGame.Painters.Status;

    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/> to register services for the Maze Game.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the Maze Game services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddMazeGame(
            this IServiceCollection services)
        {
            services.AddSingleton<MazeGameKeyPressMappings>();
            services.AddSingleton<IStatusPainter, StatusPainter>();
            services.AddTransient<IBorderPainter, BorderPainter>(); // transient because we have multiple instances which need to act independently
            services.AddSingleton<IMazePainterMap, MazePainterMap>();
            services.AddSingleton<IMazePainterPov, MazePainterPov>();
            services.AddSingleton<IColumnRenderer, ColumnRenderer>();
            services.AddSingleton<ISectionRenderer, SectionRenderer>();
            ////services.AddSingleton<IMazePainter3D, SimpleMazePainter3D>();
            services.AddSingleton<IGameController, GameController>();
            services.AddSingleton<ILimitOfViewProvider>(new LimitOfViewProvider(5));
            services.AddSingleton<MazeVisibilityUpdater>(); // TODO: IMazeVisibilityUpdater?
            services.AddSingleton<IWallCharacterProvider, LinesWallCharacterProvider>();
            ////services.AddSingleton<IWallCharacterProvider, DiagonalCrossWallCharacterProvider>();
            ////services.AddSingleton<IPlayerCharacterProvider, TrianglePlayerCharacterProvider>();
            services.AddSingleton<IPlayerCharacterProvider, ArrowPlayerCharacterProvider>();
            ////services.AddSingleton<IRayDirectionsProvider, StraightAheadDirectionsProvider>();
            ////services.AddSingleton<IRayDirectionsProvider, StraightAheadAndDiagonalsDirectionsProvider>();
            return services;
        }
    }
}
