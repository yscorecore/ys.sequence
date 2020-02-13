using System;
using System.Collections.Generic;
using System.Text;

namespace YS.Sequence.Impl.Rest.Client
{
    [OptionsClass("ApiServices")]
    public class ApiServicesOptions
    {
        public int Timeout { get; set; } = 60;
        public Dictionary<string, ServiceOptions> Services { get; set; } = new Dictionary<string, ServiceOptions>();
    }
    public class ServiceOptions
    {
        public string BaseAddress { get; set; }
    }
}
