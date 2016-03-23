// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Collections.Generic;

namespace TfsRestServices
{
    public class TfsJsonWrapper<T>
    {
        public List<T> Value { get; set; }
    }

    public class TfsJsonComment
    {
        public string Message { get; set; }
    }

    public class TfsJsonBuild
    {
        public TfsJsonLinks _links;
        public int Id { get; set; }
        public TfsJsonBuildDefinition Definition { get; set; }
        public string Status { get; set; }
        public string Result { get; set; }
        public TfsJsonPerson RequestedFor { get; set; }
    }

    public class TfsJsonLinks
    {
        public TfsJsonLink Web { get; set; }
    }

    public class TfsJsonLink
    {
        public string Href { get; set; }
    }

    public class TfsJsonPerson
    {
        public string DisplayName { get; set; }
    }

    public class TfsJsonBuildDefinition
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Type { get; set; }
    }

    public class TfsJsonProjectCollection
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class TfsJsonProject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}