using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Watcher
{
    public class SosDbFake : SosDb
    {
        private readonly FileAdapterFake _fileAdapterFake = new FileAdapterFake();
        
        protected override FileAdapter FileAdapter
        {
            get
            {
                return _fileAdapterFake;
            }
        }
    }
}