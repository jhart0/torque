using System;
using System.Collections.Generic;
using System.Resources;

namespace torque.Common.Models
{
    public class ExecutionContext
    {
        public ExecutionContext(string fromConnString, string toConnString, 
            bool deploy, IEnumerable<string> objectsToDeploy,
            ResourceManager rm)
        {
            this.FromConnString = fromConnString;
            this.ToConnString = toConnString;
            this.Deploy = deploy;
            this.ObjectsToDeploy = objectsToDeploy;
            this.ResourceManager = rm;
        }

        public string FromConnString { get; }

        public string ToConnString { get; }

        public bool Deploy { get; }

        public IEnumerable<string> ObjectsToDeploy { get; }

        public ResourceManager ResourceManager { get; }
    }
}
