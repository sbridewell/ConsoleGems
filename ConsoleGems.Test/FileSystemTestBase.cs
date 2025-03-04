// <copyright file="FileSystemTestBase.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test
{
    using Xunit.Abstractions;

    /// <summary>
    /// Abstract base class for unit tests classes which need
    /// to work with the filesystem.
    /// </summary>
    public abstract class FileSystemTestBase : IDisposable
    {
        private readonly ITestOutputHelper output;
        private readonly bool writeToTestOutput = false;
        private bool disposedValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemTestBase"/> class.
        /// </summary>
        /// <param name="output">For writing to the test output window.</param>
        protected FileSystemTestBase(ITestOutputHelper output)
        {
            this.output = output;
            var guid = Guid.NewGuid().ToString();
            this.WorkingDirectory = new DirectoryInfo(Path.Combine(Path.GetTempPath(), "ConsoleInfrastructure.Test", guid));
            if (!this.WorkingDirectory.Exists)
            {
                if (this.writeToTestOutput)
                {
                    this.output.WriteLine($"Constructor: Creating directory {this.WorkingDirectory.FullName}");
                }

                this.WorkingDirectory.Create();
            }

            this.DeleteWorkingDirectory("Constructor");
            this.CreateDirectory("Constructor", "subdir");
            this.CreateDirectory("Constructor", "subdir2");
            this.CreateFile("Constructor", "file.txt", 1);
            this.CreateFile("Constructor", "file2.txt", 42);
            this.CreateFile("Constructor", "file.log", 127);
            this.CreateFile("Constructor", "file.bmp", 65535);
        }

        /// <summary>
        /// Gets the root folder where the tests act.
        /// </summary>
        protected DirectoryInfo WorkingDirectory { get; }

        /// <inheritdoc cref="IDisposable"/>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc cref="IDisposable"/>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    // free managed resources (managed objects)
                }

                // free unmanaged resources (unmanaged objects) and override finalizer
                // set large fields to null
                this.DeleteWorkingDirectory("Destructor");
                this.disposedValue = true;
            }
        }

        private void DeleteWorkingDirectory(string actor)
        {
            if (!Directory.Exists(this.WorkingDirectory.FullName))
            {
                return;
            }

            foreach (var file in this.WorkingDirectory.GetFiles("*", SearchOption.AllDirectories))
            {
                if (this.writeToTestOutput)
                {
                    this.output.WriteLine($"{actor}: Deleting file {file.FullName}");
                }

                file.Delete();
            }

            foreach (var directory in this.WorkingDirectory.GetDirectories("*", SearchOption.AllDirectories))
            {
                if (this.writeToTestOutput)
                {
                    this.output.WriteLine($"{actor}: Deleting directory {directory.FullName}");
                }

                directory.Delete();
            }

            if (this.writeToTestOutput)
            {
                this.output.WriteLine($"{actor}: Deleting directory {this.WorkingDirectory.FullName}");
            }

            this.WorkingDirectory.Delete();
        }

        private void CreateDirectory(string actor, string directoryName)
        {
            var directoryToCreate = Path.Join(this.WorkingDirectory.FullName, directoryName);
            this.output.WriteLine($"{actor}: Creating directory {directoryToCreate}");
            Directory.CreateDirectory(directoryToCreate);
        }

        private void CreateFile(string actor, string filename, int size)
        {
            var fileToCreate = Path.Join(this.WorkingDirectory.FullName, filename);
            this.output.WriteLine($"{actor}: Creating file {fileToCreate}");
            var bytes = new byte[size];
            File.WriteAllBytes(fileToCreate, bytes);
        }
    }
}
