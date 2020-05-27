using System;
using System.Collections.Generic;

namespace torque.Common.Models
{
    public class ExecutionContext
    {
        public ExecutionContext(string fromConnString, string toConnString, bool deploy, IEnumerable<Type> objectsToDeploy)
        {
            this.FromConnString = fromConnString;
            this.ToConnString = toConnString;
            this.Deploy = deploy;
            this.ObjectsToDeploy = objectsToDeploy;
        }

        public string FromConnString { get; }

        public string ToConnString { get; }

        public bool Deploy { get; }

        public IEnumerable<Type> ObjectsToDeploy { get; }
    }
}
