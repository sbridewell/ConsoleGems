// <copyright file="MazePainter3D.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Painters.RayTracing
{
    using System.Text;
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.Text;
    using Sde.MazeGame.Models;

    /// <summary>
    /// Paints a £D representation of the maze as seen from the player's
    /// point of view.
    /// </summary>
    public class MazePainter3D(IConsole console)
        : IMazePainter3D
    {
        private readonly int screenWidth = 40;
        private readonly int screenHeight = 20;
        private readonly float fov = (float)Math.PI / 4.0f; // 90 degrees
        private readonly float visibleDistance = 5.0f; // TODO: parameterise
        private readonly Dictionary<Direction, float> compassDirectionToRadians
            = new ()
            {
                { Direction.East, 0.0f },
                { Direction.South, (float)Math.PI / 2.0f },
                { Direction.West, (float)Math.PI },
                { Direction.North, 3.0f * (float)Math.PI / 2.0f },
            };

        private ConsolePoint origin;
        private Maze? maze;

        /// <inheritdoc/>
        public void SetOrigin(ConsolePoint origin)
        {
            this.origin = origin;
        }

        /// <inheritdoc/>
        public void Render(Maze maze, Player player)
        {
            // https://github.com/dotnet/dotnet-console-games/tree/main/Projects/First%20Person%20Shooter
            this.maze = maze;
            CheckScreenSize();
            var playerFacingAngle = compassDirectionToRadians[player.FacingDirection];
            var screenBuffer = new char[screenWidth, screenHeight];

            // For each horizontal position on the screen, trace a ray out from the
            // player's position until an obstacle (e.g. a wall) is hit.
            var rays = new List<Ray>();
            for (int screenX = 0; screenX < screenWidth; screenX++)
            {
                var rayAngle = CalculateRayAngle(playerFacingAngle, screenX);
                var rayTracer = new RayTracer(); // TODO: get RayTracer from DI
                var ray = rayTracer.Trace(
                    player,
                    this.maze,
                    visibleDistance,
                    rayAngle);
                rays.Add(ray);
                PopulateScreenBufferWithWallsForGivenScreenX(
                    screenBuffer,
                    screenX,
                    ray);
            }

            var debugPainter = new RayTracingDebugPainter();
            debugPainter.Paint(console, rays);

            // TODO: repurpose this bit to display the maze exits?
            PopulateScreenBufferWithEnemies();
            PopulateScreenBufferWithStats();
            PopulateScreenBufferWith2DMaze();
            PopulateScreenBufferWithWeapon();
            CheckForGameOver();
            PaintScreen(screenBuffer);
        }

        // TODO: restore screen size check - maybe belongs in something which orchestrates all the painters?
        private void CheckScreenSize()
        {
            ////if (!screenLargeEnough)
            ////{
            ////    Console.CursorVisible = false;
            ////    Console.SetCursorPosition(0, 0);
            ////    Console.WriteLine($"Increase console size...");
            ////    Console.WriteLine($"Current Size: {consoleWidth}x{consoleHeight}");
            ////    Console.WriteLine($"Minimum Size: {screenWidth}x{screenHeight}");
            ////    return;
            ////}
        }

        /// <summary>
        /// Calculates the angle of the ray we're tracing, heading out
        /// from the player's current position until it hits an obstruction.
        /// This angle is in radians and is relative to the direction the
        /// player is facing in.
        /// </summary>
        /// <param name="playerFacingAngle">
        /// The angle of the direction that the player is facing.
        /// </param>
        /// <param name="screenX">
        /// Horizontal screen co-ordinate that we want to calculate the ray
        /// angle for.
        /// </param>
        /// <returns>
        /// The angle of the ray in radians.
        /// Lower values represent directions to the left, higher values
        /// represent directions to the right.
        /// </returns>
        private float CalculateRayAngle(float playerFacingAngle, int screenX)
        {
            var rayAngle

                // The direction the player is facing
                = playerFacingAngle

                // Subtract half the angle of the field of view, to get the
                // angle of the leftmost direction in the field of view
                - fov / 2.0f

                // and add an increment from the leftmost angle in the field
                // of view to the angle that we want to trace.
                + screenX / (float)screenWidth * fov;

            if (rayAngle < 0)
            {
                rayAngle += (float)(Math.PI * 2);
            }

            return rayAngle;
        }

        private void PopulateScreenBufferWithWallsForGivenScreenX(
            char[,] screenBuffer,
            int screenX,
            Ray ray)
        {
            var provider = new Maze3DCharProvider(visibleDistance); // TODO: get provider from DI
            //var ceiling
            //    = (this.screenHeight / 2.0f)
            //    - (this.screenHeight / ray.Distance);
            var ceiling = (int)Math.Round(screenHeight / 2.0f - screenHeight / ray.Distance);
            var floor = screenHeight - ceiling;

            for (int screenY = 0; screenY < screenHeight; screenY++)
            {
                if (screenY < ceiling)
                {
                    screenBuffer[screenX, screenY] = provider.GetCeilingChar(screenY, screenHeight);
                }
                else if (screenY == ceiling)
                {
                    screenBuffer[screenX, screenY] = provider.GetCornerChar(ray);
                }
                else if (screenY > ceiling && screenY < floor)
                {
                    // TODO: use chars like └ where walls meet ceiling or floor?
                    screenBuffer[screenX, screenY] = provider.GetWallChar(ray);
                }
                else if (screenY == floor)
                {
                    screenBuffer[screenX, screenY] = provider.GetCornerChar(ray);
                }
                else
                {
                    screenBuffer[screenX, screenY] = provider.GetFloorChar(ray, screenY, screenHeight);
                }
            }
        }

        private void PopulateScreenBufferWithEnemies()
        {
            // TODO: remove - no enemies in this game
            ////float fovAngleA = playerA - fov / 2;
            ////float fovAngleB = playerA + fov / 2;
            ////if (fovAngleA < 0) fovAngleA += 2 * (float)Math.PI;

            ////foreach (var enemy in enemies)
            ////{
            ////    float angle = (float)Math.Atan2(enemy.Y - playerY, enemy.X - playerX);
            ////    if (angle < 0) angle += 2f * (float)Math.PI;

            ////    float distance = Vector2.Distance(new(playerX, playerY), new(enemy.X, enemy.Y));

            ////    int ceiling = (int)((float)(screenHeight / 2.0f) - screenHeight / ((float)distance));
            ////    int floor = screenHeight - ceiling;

            ////    string[] enemySprite = distance switch
            ////    {
            ////        <= 01f => enemySprite8,
            ////        <= 02f => enemySprite7,
            ////        <= 03f => enemySprite6,
            ////        <= 04f => enemySprite5,
            ////        <= 05f => enemySprite4,
            ////        <= 06f => enemySprite3,
            ////        <= 07f => enemySprite2,
            ////        _ => enemySprite1
            ////    };

            ////    float diff = angle < fovAngleA && fovAngleA - 2f * (float)Math.PI + fov > angle ? angle + 2f * (float)Math.PI - fovAngleA : angle - fovAngleA;
            ////    float ratio = diff / fov;
            ////    int enemyScreenX = (int)(screenWidth * ratio);
            ////    int enemyScreenY = Math.Min(floor, screen.GetLength(1));

            ////    for (int y = 0; y < enemySprite.Length; y++)
            ////    {
            ////        for (int x = 0; x < enemySprite[y].Length; x++)
            ////        {
            ////            if (enemySprite[y][x] is not '!')
            ////            {
            ////                int screenX = x - enemySprite[y].Length / 2 + enemyScreenX;
            ////                int screenY = y - enemySprite.Length + enemyScreenY;
            ////                if (0 <= screenX && screenX <= screenWidth - 1 && 0 <= screenY && screenY <= screenHeight - 1 && depthBuffer[screenX, screenY] > distance)
            ////                {
            ////                    screen[screenX, screenY] = enemySprite[y][x];
            ////                    depthBuffer[screenX, screenY] = distance;
            ////                }
            ////            }
            ////        }
            ////    }
            ////}
        }

        private void PopulateScreenBufferWithStats()
        {
            //// TODO: remove - no need to display stats (for now)
            ////    if (statsVisible)
            ////    {
            ////        string[] stats =
            ////        [
            ////            $"x={playerX:0.00}",
            ////    $"y={playerY:0.00}",
            ////    $"a={playerA:0.00}",
            ////    $"fps={fps:0.}",
            ////    $"score={score}",
            ////    $"time={(int)gameTimeStopwatch.Elapsed.TotalSeconds}/{(int)gameTime.TotalSeconds}",
            ////];
            ////        for (int i = 0; i < stats.Length; i++)
            ////        {
            ////            for (int j = 0; j < stats[i].Length; j++)
            ////            {
            ////                screen[screenWidth - stats[i].Length + j, i] = stats[i][j];
            ////            }
            ////        }
            ////    }
        }

        private void PopulateScreenBufferWith2DMaze()
        {
            //// TODO: remove - 2D map is rendered separately
            ////if (mapVisible)
            ////{
            ////    for (int y = 0; y < map.Length; y++)
            ////    {
            ////        for (int x = 0; x < map[y].Length; x++)
            ////        {
            ////            screen[x, y] = map[y][x] is '^' or '<' or '>' or 'v' ? ' ' : map[y][x];
            ////        }
            ////    }
            ////    foreach (var enemy in enemies)
            ////    {
            ////        screen[(int)enemy.X, (int)enemy.Y] = 'X';
            ////    }
            ////    screen[(int)playerX, (int)playerY] = playerA switch
            ////    {
            ////        >= 0.785f and < 2.356f => 'v',
            ////        >= 2.356f and < 3.927f => '<',
            ////        >= 3.927f and < 5.498f => '^',
            ////        _ => '>',
            ////    };
            ////}
        }

        private void PopulateScreenBufferWithWeapon()
        {
            //// TODO: remove - no weapons in this game
            ////string[] player =
            ////    equippedWeapon is Weapon.Pistol && stopwatchShoot is not null && stopwatchShoot.Elapsed < pistolShootAnimationTime ? playerPistolShoot :
            ////    equippedWeapon is Weapon.Shotgun && stopwatchShoot is not null && stopwatchShoot.Elapsed < shotgunShootAnimationTime ? playerShotgunShoot :
            ////    equippedWeapon is Weapon.Pistol ? playerPistol :
            ////    equippedWeapon is Weapon.Shotgun ? playerShotgun :
            ////    throw new NotImplementedException();
            ////for (int y = 0; y < player.Length; y++)
            ////{
            ////    for (int x = 0; x < player[y].Length; x++)
            ////    {
            ////        if (player[y][x] is not '!')
            ////        {
            ////            screen[x + screenWidth / 2 - player[y].Length / 2, screenHeight - player.Length + y] = player[y][x];
            ////        }
            ////    }
            ////}
        }

        private void CheckForGameOver()
        {
            //// TODO: remove - game over is handled outside of rendering
            ////    if (gameOver)
            ////    {
            ////        string[] gameOverMessage =
            ////        [
            ////            $"                                        ",
            ////    $"               GAME OVER!               ",
            ////    $"                Score: {score}                ",
            ////    $"   Press [enter] to return to menu...   ",
            ////    $"                                        ",
            ////];
            ////        int gameOverMessageY = screenHeight / 2 - gameOverMessage.Length / 2;
            ////        foreach (string line in gameOverMessage)
            ////        {
            ////            int gameOverMessageX = screenWidth / 2 - line.Length / 2;
            ////            foreach (char c in line)
            ////            {
            ////                screen[gameOverMessageX, gameOverMessageY] = c;
            ////                gameOverMessageX++;
            ////            }
            ////            gameOverMessageY++;
            ////        }
            ////    }
        }

        private void PaintScreen(char[,] screenBuffer)
        {
            console.CursorVisible = false;
            for (int screenY = 0; screenY < screenBuffer.GetLength(1); screenY++)
            {
                var render = new StringBuilder();
                for (int screenX = 0; screenX < screenBuffer.GetLength(0); screenX++)
                {
                    render.Append(screenBuffer[screenX, screenY]);
                }

                console.CursorLeft = origin.X;
                console.CursorTop = origin.Y + screenY;
                console.Write(render.ToString());
            }
        }

        #region Copilot suggestion - really slow

        ////public void Render(Maze maze, Player player)
        ////{
        ////    var screenHeight = 20;
        ////    var screenWidth = 40;
        ////    console.CursorLeft = 0;
        ////    console.CursorTop = 0;
        ////    var maxDepth = 5;
        ////    var screenBuffer = new char[screenHeight, screenWidth];

        ////    //for (int x = 0; x < screenWidth; x++)
        ////    Parallel.For(0, screenWidth, x =>
        ////    {
        ////        // Calculate the angle of the ray
        ////        double rayAngle = (ToRadians(player.FacingDirection) - fov / 2) + (x / (double)screenWidth) * fov;

        ////        // Cast the ray
        ////        var distanceToWall = CastRayDDA(maze, player, rayAngle, maxDepth);

        ////        // Calculate the height of the wall based on distance
        ////        int ceiling = (int)(screenHeight / 2.0 - screenHeight / distanceToWall);
        ////        int floor = screenHeight - ceiling;

        ////        // Render the column
        ////        for (int y = 0; y < screenHeight; y++)
        ////        {
        ////            // Set the cursor position for the current column and row
        ////            console.CursorLeft = x;
        ////            console.CursorTop = y;

        ////            if (y < ceiling)
        ////            {
        ////                screenBuffer[y, x] = this.skyChar; // Sky
        ////            }
        ////            else if (y >= ceiling && y <= floor)
        ////            {
        ////                screenBuffer[y, x] = this.wallChar; // Wall
        ////            }
        ////            else
        ////            {
        ////                screenBuffer[y, x] = this.floorChar; // Floor
        ////            }
        ////        }
        ////    });

        ////    // Write the buffer to the console row by row
        ////    var rowBuilder = new StringBuilder(screenWidth);
        ////    for (int y = 0; y < screenHeight; y++)
        ////    {
        ////        rowBuilder.Clear();
        ////        for (int x = 0; x < screenWidth; x++)
        ////        {
        ////            rowBuilder.Append(screenBuffer[y, x]);
        ////        }

        ////        // Set the cursor position and write the row
        ////        console.CursorLeft = 0;
        ////        console.CursorTop = y;
        ////        console.Write(rowBuilder.ToString());
        ////    }
        ////}

        ////private static double ToRadians(Direction direction)
        ////{
        ////    return direction switch
        ////    {
        ////        Direction.North => 0,
        ////        Direction.East => Math.PI / 2,
        ////        Direction.South => Math.PI,
        ////        Direction.West => 3 * Math.PI / 2,
        ////        _ => throw new ArgumentOutOfRangeException(nameof(direction), "Invalid direction"),
        ////    };
        ////}

        ////private static double CastRayDDA(Maze maze, Player player, double rayAngle, double maxDepth)
        ////{
        ////    double eyeX = Math.Cos(rayAngle);
        ////    double eyeY = Math.Sin(rayAngle);

        ////    int mapX = player.Position.X;
        ////    int mapY = player.Position.Y;

        ////    double deltaDistX = Math.Abs(1 / eyeX);
        ////    double deltaDistY = Math.Abs(1 / eyeY);

        ////    int stepX = eyeX < 0 ? -1 : 1;
        ////    int stepY = eyeY < 0 ? -1 : 1;

        ////    double sideDistX = eyeX < 0
        ////        ? (player.Position.X - mapX) * deltaDistX
        ////        : (mapX + 1.0 - player.Position.X) * deltaDistX;

        ////    double sideDistY = eyeY < 0
        ////        ? (player.Position.Y - mapY) * deltaDistY
        ////        : (mapY + 1.0 - player.Position.Y) * deltaDistY;

        ////    double distanceToWall = 0;
        ////    bool hitWall = false;

        ////    while (!hitWall && distanceToWall < maxDepth)
        ////    {
        ////        if (sideDistX < sideDistY)
        ////        {
        ////            sideDistX += deltaDistX;
        ////            mapX += stepX;
        ////        }
        ////        else
        ////        {
        ////            sideDistY += deltaDistY;
        ////            mapY += stepY;
        ////        }

        ////        if (maze.PositionIsWithinMaze(new ConsolePoint(mapX, mapY)) &&
        ////            maze.GetMazePoint(mapX, mapY).PointType == MazePointType.Wall)
        ////        {
        ////            hitWall = true;
        ////            distanceToWall = Math.Min(sideDistX, sideDistY);
        ////        }
        ////    }

        ////    return distanceToWall;
        ////}

        #endregion
    }
}
