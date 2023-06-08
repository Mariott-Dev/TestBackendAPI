namespace BackendAPI
{
    public class BlockModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Argument
        {
            public string? controlType { get; set; }
            public string? dataType { get; set; }
            public string? description { get; set; }
            public string? name { get; set; }
        }

        public class BlockDoc
        {
            public List<Argument>? arguments { get; set; }
            public string? blockLibraryName { get; set; }
            public List<Channel>? channels { get; set; }
            public string? description { get; set; }
            public List<object>? signals { get; set; }
            public List<object>? slots { get; set; }
            public List<Statistic>? statistics { get; set; }
        }

        public class Channel
        {
            public string? dataType { get; set; }
            public string? description { get; set; }
            public string? direction { get; set; }
            public string? name { get; set; }
        }

        public class Header
        {
            public string? id { get; set; }
            public string? name { get; set; }
            public string? type { get; set; }
        }

        public class Parameters
        {
            public List<BlockDoc>? blockDocs { get; set; }
        }

        public class Response
        {
            public string? message { get; set; }
            public bool? status { get; set; }
        }

        public class Root
        {
            public Header? header { get; set; }
            public Parameters? parameters { get; set; }
            public Response? response { get; set; }
        }

        public class Statistic
        {
            public string? dataType { get; set; }
            public string? description { get; set; }
            public string? name { get; set; }
        }
    }
}
