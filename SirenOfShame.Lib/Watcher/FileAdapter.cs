using System.IO;

namespace SirenOfShame.Lib.Watcher
{
    public class FileAdapter
    {
        public virtual bool Exists(string location)
        {
            return File.Exists(location);
        }

        public virtual void AppendAllText(string location, string contents)
        {
            File.AppendAllText(location, contents);
        }

        public virtual string[] ReadAllLines(string location)
        {
            return File.ReadAllLines(location);
        }
    }
}
