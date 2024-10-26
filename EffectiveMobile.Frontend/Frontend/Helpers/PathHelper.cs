using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Helpers
{
    public static class PathHelper
    {
        public static bool IsFullPath(string path)
        {
            return !string.IsNullOrWhiteSpace(path)
                   && path.IndexOfAny([.. Path.GetInvalidPathChars()]) == -1
                   && Path.IsPathRooted(path)
                   && !Path.GetPathRoot(path).Equals(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal);
        }

        public static string GetFullPath(string path)
        {
            return IsFullPath(path)
                ? path
                : Environment.CurrentDirectory + path;
        }
    }
}
