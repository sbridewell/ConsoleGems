// <copyright file="RayTracerTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Test
{
    using FluentAssertions;
    using Sde.MazeGame.Models;
    using Sde.MazeGame.Painters.RayTracing;

    /// <summary>
    /// Unit tests for the <see cref="RayTracer"/> class.
    /// </summary>
    public class RayTracerTest()
    {
        private static readonly float Pi = (float)Math.PI;

        private static readonly Dictionary<string, string[]> Mazes = new ()
        {
            // Wall distance 1
            {
                "Wall distance 1, facing east",
                new string[]
                {
                    "##",
                    " #",
                    "##",
                }
            },
            {
                "Wall distance 1, facing south",
                new string[]
                {
                    "# #",
                    "###",
                }
            },
            {
                "Wall distance 1, facing west",
                new string[]
                {
                    "##",
                    "# ",
                    "##",
                }
            },
            {
                "Wall distance 1, facing north",
                new string[]
                {
                    "###",
                    "# #",
                }
            },

            // Wall distance 2
            {
                "Wall distance 2, facing east",
                new string[]
                {
                    "###",
                    "   #",
                    "   #",
                    "###",
                }
            },
            {
                "Wall distance 2, facing south",
                new string[]
                {
                    "#   #",
                    "#   #",
                    "#####",
                }
            },
            {
                "Wall distance 2, facing west",
                new string[]
                {
                    "###",
                    "#  ",
                    "#  ",
                    "###",
                }
            },
            {
                "Wall distance 2, facing north",
                new string[]
                {
                    "#####",
                    "#   #",
                    "#   #",
                }
            },
        };

        /// <summary>
        /// This is where the test data is defined.
        /// Xunit doesn't seem to be able to serialize this shape in a way which
        /// allows the VS test explorer to display each test case as an individual
        /// test, so the test data property consumed by the test method is instead
        /// a list of the names of the test cases (the keys of this dictionary).
        /// </summary>
        private static readonly Dictionary<
            string,
            (string[] MazeData, Player Player, float VisibleDistance, List<Ray> ExpectedRays)>
            TestData = new ()
        {
            // Visible distance 0.4, wall distance 1
            {
                "visible distance 0.4, wall distance 1, east (no hit)",
                (Mazes["Wall distance 1, facing east"],
                new Player { Position = new (0, 1), FacingDirection = Direction.East },
                0.4f, // visible distance
                new List<Ray>
                {
                    new () { Distance = 0.40999988f, Direction = Pi * -0.5f, HasHitAWall = false }, // 90 degrees left
                    new () { Distance = 0.40999988f, Direction = Pi * -0.25f, HasHitAWall = false },
                    new () { Distance = 0.40999988f, Direction = Pi * 0, HasHitAWall = false },
                    new () { Distance = 0.40999988f, Direction = Pi * 0.25f, HasHitAWall = false },
                    new () { Distance = 0.40999988f, Direction = Pi * 0.5f, HasHitAWall = false }, // 90 degrees right
                })
            },

            // Visible distance 1, wall distance 1
            {
                "visible distance 1, wall distance 1, east (hit)",
                (Mazes["Wall distance 1, facing east"],
                new Player { Position = new (0, 1), FacingDirection = Direction.East },
                1.0f, // visible distance
                new List<Ray>
                {
                    new () { Distance = 0.5099998f, Direction = Pi * -0.5f, HasHitAWall = true }, // 90 degrees left
                    new () { Distance = 0.7099996f, Direction = Pi * -0.25f, HasHitAWall = true },
                    new () { Distance = 0.5099998f, Direction = Pi * 0, HasHitAWall = true },
                    new () { Distance = 0.7099996f, Direction = Pi * 0.25f, HasHitAWall = true },
                    new () { Distance = 0.5099998f, Direction = Pi * 0.5f, HasHitAWall = true }, // 90 degrees right
                })
            },
            {
                "visible distance 1, wall distance 1, south (hit)",
                (Mazes["Wall distance 1, facing south"],
                new Player { Position = new (1, 0), FacingDirection = Direction.South },
                1.0f, // visible distance
                new List<Ray>
                {
                    new () { Distance = 0.5099998f, Direction = Pi * -0.5f, HasHitAWall = true }, // 90 degrees left
                    new () { Distance = 0.7099996f, Direction = Pi * -0.25f, HasHitAWall = true },
                    new () { Distance = 0.5099998f, Direction = Pi * 0, HasHitAWall = true },
                    new () { Distance = 0.7099996f, Direction = Pi * 0.25f, HasHitAWall = true },
                    new () { Distance = 0.5099998f, Direction = Pi * 0.5f, HasHitAWall = true }, // 90 degrees right
                })
            },
            {
                "visible distance 1, wall distance 1, west (hit)",
                (Mazes["Wall distance 1, facing west"],
                new Player { Position = new (1, 1), FacingDirection = Direction.West },
                1.0f, // visible distance
                new List<Ray>
                {
                    new () { Distance = 0.5099998f, Direction = Pi * -0.5f, HasHitAWall = true }, // 90 degrees left
                    new () { Distance = 0.7099996f, Direction = Pi * -0.25f, HasHitAWall = true },
                    new () { Distance = 0.5099998f, Direction = Pi * 0, HasHitAWall = true },
                    new () { Distance = 0.7099996f, Direction = Pi * 0.25f, HasHitAWall = true },
                    new () { Distance = 0.5099998f, Direction = Pi * 0.5f, HasHitAWall = true }, // 90 degrees right
                })
            },
            {
                "visible distance 1, wall distance 1, north (hit)",
                (Mazes["Wall distance 1, facing north"],
                new Player { Position = new (1, 1), FacingDirection = Direction.North },
                1.0f, // visible distance
                new List<Ray>
                {
                    new () { Distance = 0.5099998f, Direction = Pi * -0.5f, HasHitAWall = true }, // 90 degrees left
                    new () { Distance = 0.7099996f, Direction = Pi * -0.25f, HasHitAWall = true },
                    new () { Distance = 0.5099998f, Direction = Pi * 0, HasHitAWall = true },
                    new () { Distance = 0.7099996f, Direction = Pi * 0.25f, HasHitAWall = true },
                    new () { Distance = 0.5099998f, Direction = Pi * 0.5f, HasHitAWall = true }, // 90 degrees right
                })
            },

            // Visible distance 1, wall distance 2
            {
                "visible distance 1, wall distance 2, east (no hit)",
                (Mazes["Wall distance 2, facing east"],
                new Player { Position = new (0, 2), FacingDirection = Direction.East },
                1.0f, // visible distance
                new List<Ray>
                {
                    new () { Distance = 1.0099994f, Direction = Pi * -0.5f, HasHitAWall = false }, // 90 degrees left
                    new () { Distance = 1.0099994f, Direction = Pi * -0.25f, HasHitAWall = false },

                    // FIXME: returning direction 0.785 for this ray
                    new () { Distance = 0.7099996f, Direction = Pi * 0, HasHitAWall = true },
                    new () { Distance = 0.5099998f, Direction = Pi * 0.25f, HasHitAWall = true },
                    new () { Distance = 1.0099994f, Direction = Pi * 0.5f, HasHitAWall = false }, // 90 degrees right
                })
            },
            // TODO: tests after this not currently passing or realistic
            {
                "South, visible distance 3, walls are 2 squares away",
                (Mazes["Wall distance 2, facing south"],
                new Player { Position = new (2, 0), FacingDirection = Direction.South },
                3.0f, // visible distance
                new List<Ray>
                {
                    // TODO: inconsistent ray distances for straight left / straight right, and for 45 degrees left and right
                    new () { Distance = 1.1f, Direction = Pi * -0.5f, HasHitAWall = true }, // 90 degrees left
                    new () { Distance = 1.1f, Direction = Pi * -0.25f, HasHitAWall = true },
                    new () { Distance = 1.5000001f, Direction = Pi * 0, HasHitAWall = true },
                    new () { Distance = 2.2f, Direction = Pi * 0.25f, HasHitAWall = true },
                    new () { Distance = 1.5000001f, Direction = Pi * 0.5f, HasHitAWall = true }, // 90 degrees right
                })
            },
            {
                "West, visible distance 3, walls are 2 squares away",
                (Mazes["Wall distance 2, facing west"],
                new Player { Position = new (2, 2), FacingDirection = Direction.West },
                3.0f, // visible distance
                new List<Ray>
                {
                    // TODO: how can the ray distance be the same when the walls are 1 and 2 squares away?
                    new () { Distance = 1.5000001f, Direction = Pi * -0.5f, HasHitAWall = true }, // 90 degrees left
                    new () { Distance = 1.1f, Direction = Pi * -0.25f, HasHitAWall = true },
                    new () { Distance = 1.1f, Direction = Pi * 0, HasHitAWall = true },
                    new () { Distance = 1.1f, Direction = Pi * 0.25f, HasHitAWall = true },
                    new () { Distance = 1.5000001f, Direction = Pi * 0.5f, HasHitAWall = true }, // 90 degrees right
                })
            },
            {
                "North, visible distance 3, walls are 2 squares away",
                (Mazes["Wall distance 2, facing north"],
                new Player { Position = new (2, 2), FacingDirection = Direction.North },
                3.0f, // visible distance
                new List<Ray>
                {
                    // TODO: inconsistent ray distances for straight ahead and 90 degrees right
                    // TODO: inconsistent ray distances for 45 degrees left and right
                    new () { Distance = 1.5000001f, Direction = Pi * -0.5f, HasHitAWall = true }, // 90 degrees left
                    new () { Distance = 2.2f, Direction = Pi * -0.25f, HasHitAWall = true },
                    new () { Distance = 1.5000001f, Direction = Pi * 0, HasHitAWall = true },
                    new () { Distance = 1.1f, Direction = Pi * 0.25f, HasHitAWall = true },
                    new () { Distance = 1.1f, Direction = Pi * 0.5f, HasHitAWall = true }, // 90 degrees right
                })
            },
            {
                "East, visible distance 4, walls are 3 squares away",
                (new string[]
                {
                    "####",
                    "   #",
                    "   #",
                    "   #",
                    "   #",
                    "   #",
                    "####",
                },
                new PlayerProxy { Position = new (0, 3), FacingDirection = Direction.East },
                4.0f, // visible distance
                new List<Ray>
                {
                    new () { Distance = 2.5999997f, Direction = Pi * -0.5f, HasHitAWall = true }, // 90 degrees left
                    new () { Distance = 3.5999987f, Direction = Pi * -0.25f, HasHitAWall = true },
                    new () { Distance = 2.5999997f, Direction = Pi * 0, HasHitAWall = true },
                    new () { Distance = 3.5999987f, Direction = Pi * 0.25f, HasHitAWall = true },

                    // TODO: distance for 90 degrees right is different to that for 90 degrees left
                    new () { Distance = 2.4999997f, Direction = Pi * 0.5f, HasHitAWall = true }, // 90 degrees right
                })
            },
            {
                "South, visible distance 4, walls are 3 squares away",
                (new string[]
                {
                    "#     #",
                    "#     #",
                    "#     #",
                    "#######",
                },
                new PlayerProxy { Position = new (3, 0), FacingDirection = Direction.South },
                4.0f, // visible distance
                new List<Ray>
                {
                    // TODO: different ray distances for equivalent directions
                    new () { Distance = 1.1f, Direction = Pi * -0.5f, HasHitAWall = true }, // 90 degrees left
                    new () { Distance = 1.1f, Direction = Pi * -0.25f, HasHitAWall = true },
                    new () { Distance = 2.4999998f, Direction = Pi * 0, HasHitAWall = true },
                    new () { Distance = 3.5999987f, Direction = Pi * 0.25f, HasHitAWall = true },
                    new () { Distance = 2.5999997f, Direction = Pi * 0.5f, HasHitAWall = true }, // 90 degrees right
                })
            },
            {
                "West, visible distance 4, walls are 3 squares away",
                (new string[]
                {
                    "####",
                    "#   ",
                    "#   ",
                    "#   ",
                    "#   ",
                    "#   ",
                    "####",
                },
                new PlayerProxy { Position = new (3, 3), FacingDirection = Direction.West },
                4.0f, // visible distance
                new List<Ray>
                {
                    // TODO: ray distance should be more than 1.1 surely?
                    new () { Distance = 2.5999997f, Direction = Pi * -0.5f, HasHitAWall = true }, // 90 degrees left
                    new () { Distance = 1.1f, Direction = Pi * -0.25f, HasHitAWall = true },
                    new () { Distance = 1.1f, Direction = Pi * 0, HasHitAWall = true },
                    new () { Distance = 1.1f, Direction = Pi * 0.25f, HasHitAWall = true },
                    new () { Distance = 2.4999997f, Direction = Pi * 0.5f, HasHitAWall = true }, // 90 degrees right
                })
            },
            {
                "North, visible distance 4, walls are 3 squares away",
                (new string[]
                {
                    "#######",
                    "#     #",
                    "#     #",
                    "#     #",
                },
                new PlayerProxy { Position = new (3, 3), FacingDirection = Direction.North },
                4.0f, // visible distance
                new List<Ray>
                {
                    // TODO: different ray distances for equivalent directions
                    new () { Distance = 2.5999997f, Direction = Pi * -0.5f, HasHitAWall = true }, // 90 degrees left
                    new () { Distance = 3.5999987f, Direction = Pi * -0.25f, HasHitAWall = true },
                    new () { Distance = 2.4999998f, Direction = Pi * 0, HasHitAWall = true },
                    new () { Distance = 1.1f, Direction = Pi * 0.25f, HasHitAWall = true },
                    new () { Distance = 1.1f, Direction = Pi * 0.5f, HasHitAWall = true }, // 90 degrees right
                })
            },
            {
                "East, visible distance 5, walls are 4 squares away",
                (new string[]
                {
                    "#####",
                    "    #",
                    "    #",
                    "    #",
                    "    #",
                    "    #",
                    "    #",
                    "    #",
                    "#####",
                },
                new PlayerProxy { Position = new (0, 4), FacingDirection = Direction.East },
                5.0f, // visible distance
                new List<Ray>
                {
                    new () { Distance = 3.5999987f, Direction = Pi * -0.5f, HasHitAWall = true }, // 90 degrees left
                    new () { Distance = 4.9999976f, Direction = Pi * -0.25f, HasHitAWall = true },
                    new () { Distance = 3.5999987f, Direction = Pi * 0, HasHitAWall = true },
                    new () { Distance = 4.9999976f, Direction = Pi * 0.25f, HasHitAWall = true },
                    new () { Distance = 3.5999987f, Direction = Pi * 0.5f, HasHitAWall = true }, // 90 degrees right
                })
            },
            {
                "South, visible distance 5, walls are 4 squares away",
                (new string[]
                {
                    "#       #",
                    "#       #",
                    "#       #",
                    "#       #",
                    "#########",
                },
                new PlayerProxy { Position = new (4, 0), FacingDirection = Direction.South },
                5.0f, // visible distance
                new List<Ray>
                {
                    // TODO: ray distance should be more than 1.1 surely?
                    new () { Distance = 1.1f, Direction = Pi * -0.5f, HasHitAWall = true }, // 90 degrees left
                    new () { Distance = 1.1f, Direction = Pi * -0.25f, HasHitAWall = true },
                    new () { Distance = 3.5999987f, Direction = Pi * 0, HasHitAWall = true },
                    new () { Distance = 4.9999976f, Direction = Pi * 0.25f, HasHitAWall = true },
                    new () { Distance = 3.5999987f, Direction = Pi * 0.5f, HasHitAWall = true }, // 90 degrees right
                })
            },
            {
                "West, visible distance 5, walls are 4 squares away",
                (new string[]
                {
                    "#####",
                    "#    ",
                    "#    ",
                    "#    ",
                    "#    ",
                    "#    ",
                    "#    ",
                    "#    ",
                    "#####",
                },
                new PlayerProxy { Position = new (4, 4), FacingDirection = Direction.West },
                5.0f, // visible distance
                new List<Ray>
                {
                    new () { Distance = 3.5999987f, Direction = Pi * -0.5f, HasHitAWall = true }, // 90 degrees left
                    new () { Distance = 1.1f, Direction = Pi * -0.25f, HasHitAWall = true },
                    new () { Distance = 1.1f, Direction = Pi * 0, HasHitAWall = true },
                    new () { Distance = 1.1f, Direction = Pi * 0.25f, HasHitAWall = true },
                    new () { Distance = 3.5999987f, Direction = Pi * 0.5f, HasHitAWall = true }, // 90 degrees right
                })
            },
            {
                "North, visible distance 5, walls are 4 squares away",
                (new string[]
                {
                    "#########",
                    "#       #",
                    "#       #",
                    "#       #",
                    "#       #",
                },
                new PlayerProxy { Position = new (4, 4), FacingDirection = Direction.South },
                5.0f, // visible distance
                new List<Ray>
                {
                    new () { Distance = 3.5999987f, Direction = Pi * -0.5f, HasHitAWall = true }, // 90 degrees left
                    new () { Distance = 4.9999976f, Direction = Pi * -0.25f, HasHitAWall = true },
                    new () { Distance = 3.5999987f, Direction = Pi * 0, HasHitAWall = true },

                    // TODO: ray distance should be more than 1.1 surely?
                    new () { Distance = 1.1f, Direction = Pi * 0.25f, HasHitAWall = true },
                    new () { Distance = 1.1f, Direction = Pi * 0.5f, HasHitAWall = true }, // 90 degrees right
                })
            },
        };

        /// <summary>
        /// Gets the names of the test cases, to be passed to the MemberData
        /// attribute on the test method.
        /// The test method must then look up the actual test data using the name.
        /// </summary>
        public static IEnumerable<object[]> MazeDataNames
        {
            get
            {
                foreach (var key in TestData.Keys)
                {
                    yield return new object[] { key };
                }
            }
        }

        /// <summary>
        /// Tests that the Trace method returns a Ray with the correct properties.
        /// </summary>
        /// <param name="name">The name of the test case.</param>
        [Theory]
        [MemberData(nameof(MazeDataNames))]
        public void Test(string name)
        {
            // Arrange
            var testData = TestData[name];
            var maze = CreateMaze(testData.MazeData);
            var tracer = new RayTracer();
            var actualRays = new List<Ray>();

            // Act
            foreach (var expectedRay in testData.ExpectedRays)
            {
                var ray = tracer.Trace(testData.Player, maze, testData.VisibleDistance, expectedRay.Direction);
                actualRays.Add(ray);
            }

            // Assert
            actualRays.Should().BeEquivalentTo(testData.ExpectedRays);
        }

        private static Maze CreateMaze(string[] mazeData)
        {
            var factory = new MazeFactory();
            var maze = factory.CreateFromStringArray(mazeData);
            return maze;
        }
    }
}
