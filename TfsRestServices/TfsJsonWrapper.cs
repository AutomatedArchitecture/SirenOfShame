// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Collections.Generic;

namespace TfsRestServices
{
    public class TfsJsonWrapper<T>
    {
        public List<T> Value { get; set; }
    }

    public class TfsJsonBuild
    {
        public int Id { get; set; }
        public TfsJsonBuildDefinition Definition { get; set; }
        public string Status { get; set; }
        public string Result { get; set; }
        public TfsJsonPerson RequestedFor { get; set; }
    }

    public class TfsJsonPerson
    {
        public string DisplayName { get; set; }
    }

    public class TfsJsonBuildDefinition
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}