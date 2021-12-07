using System;

namespace MartinDrozdik.Services.FilePathProvider
{
    public interface IFilePathProvider
    {
        /// <summary>
        /// Creates path to a resource. 
        /// </summary>
        /// <param name="path">Target absolute path, f.e. (/Fonts/OpenSans.svg)</param>
        /// <returns>Full path</returns>
        string PathTo(string path);

        /// <summary>
        /// Creates path to a resource places in a RazorClassLibrary
        /// </summary>
        /// <param name="path">Target absolute path, f.e. (/Fonts/OpenSans.svg)</param>
        /// <param name="razorLibName">The target library name</param>
        /// <returns>Full path</returns>
        string PathTo(string path, string razorLibName);

        /// <summary>
        /// Creates clean and unmodified path to a resource
        /// </summary>
        /// <param name="path">Target path</param>
        /// <returns>Full path</returns>
        string CleanPathTo(string path);

        /// <summary>
        /// Creates clean and unmodified path to a resource placed in a RazorClassLibrary
        /// </summary>
        /// <param name="path">Target absolute path, f.e. (/Fonts/OpenSans.svg)</param>
        /// <param name="razorLibName">The target library name</param>
        /// <returns>Full path</returns>
        string CleanPathTo(string path, string razorLibName);
    }
}
