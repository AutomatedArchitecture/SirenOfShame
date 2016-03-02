using System.Collections.Generic;
using System.Threading.Tasks;
using SirenOfShame.Lib.Settings;

namespace TfsRestServices
{
    public class TfsRestBuildDefinition : MyBuildDefinition
    {
        public override string Id { get; }
        public override string Name { get; }
    }

    internal class TfsRestService
    {
        public async Task<List<TfsRestBuildDefinition>> GetProjects(string url, string username, string password)
        {
            await Task.Yield();
            return new List<TfsRestBuildDefinition>();
        }
    }
}