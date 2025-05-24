// <copyright file="PainterOrchestrator.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.FullScreen
{
    /// <summary>
    /// Orchestrates multiple <see cref="IPainter"/> implementations which paint
    /// content to the same console window.
    /// </summary>
    /// <param name="console">The console to write to.</param>
    public class PainterOrchestrator(IConsole console)
        : IPainterOrchestrator
    {
        /// <inheritdoc/>
        public List<IPainter> Painters { get; } = new ();

        /// <inheritdoc/>
        public void Paint()
        {
            this.WaitUntilWindowIsLargeEnough();
            this.CheckForOverlappingPainters();
            foreach (var painter in this.Painters)
            {
                painter.Paint();
            }
        }

        private void CheckForOverlappingPainters()
        {
            var outerBounds = this.Painters.Select(painter => painter.OuterBounds).ToList();
            for (int i = 0; i < outerBounds.Count; i++)
            {
                for (int j = i + 1; j < outerBounds.Count; j++)
                {
                    if (outerBounds[i].OverlapsWith(outerBounds[j]))
                    {
                        throw new InvalidOperationException($"Painters '{this.Painters[i]}' and '{this.Painters[j]}' overlap.");
                    }
                }
            }
        }

        private void WaitUntilWindowIsLargeEnough()
        {
            var requiredWindowSize = this.GetRequiredWindowSize();
            while (true)
            {
                var windowWidth = console.WindowWidth;
                var windowHeight = console.WindowHeight;
                if (windowWidth >= requiredWindowSize.Width &&
                    windowHeight >= requiredWindowSize.Height)
                {
                    break;
                }

                console.Clear();
                var msg = $"Please resize the console window to at least {requiredWindowSize.Width}x{requiredWindowSize.Height}. "
                    + $"Current window size is {windowWidth}x{windowHeight}.";
                console.Write(msg, ConsoleOutputType.Error);
                Thread.Sleep(100);
            }
        }

        private ConsoleSize GetRequiredWindowSize()
        {
            var height = 0;
            var width = 0;
            var outerBounds = this.Painters.Select(painter => painter.OuterBounds).ToList();
            foreach (var bounds in outerBounds)
            {
                width = Math.Max(width, bounds.Right + 1);
                height = Math.Max(height, bounds.Bottom + 1);
            }

            return new ConsoleSize(width, height);
        }
    }
}
