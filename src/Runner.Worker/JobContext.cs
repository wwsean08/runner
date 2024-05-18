using System.Collections.Generic;
using GitHub.DistributedTask.Pipelines.ContextData;
using GitHub.Runner.Common.Util;
using GitHub.Runner.Common;
using GitHub.Runner.Sdk;

namespace GitHub.Runner.Worker
{
    public sealed class JobContext : DictionaryContextData
    {
        public ActionResult? Status
        {
            get
            {
                if (this.TryGetValue("status", out var status) && status is StringContextData statusString)
                {
                    return EnumUtil.TryParse<ActionResult>(statusString);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this["status"] = new StringContextData(value.ToString().ToLowerInvariant());
            }
        }

        public DictionaryContextData Services
        {
            get
            {
                if (this.TryGetValue("services", out var services) && services is DictionaryContextData servicesDictionary)
                {
                    return servicesDictionary;
                }
                else
                {
                    this["services"] = new DictionaryContextData();
                    return this["services"] as DictionaryContextData;
                }
            }
        }

        public DictionaryContextData Permissions
        {
            get
            {
                if (this.TryGetValue("permissions", out var services) && services is DictionaryContextData permissionsDictionary)
                {
                    return permissionsDictionary;
                }
                else
                {
                    this["permissions"] = new DictionaryContextData();
                    return this["permissions"] as DictionaryContextData;
                }
            }
        }

        public DictionaryContextData Container
        {
            get
            {
                if (this.TryGetValue("container", out var container) && container is DictionaryContextData containerDictionary)
                {
                    return containerDictionary;
                }
                else
                {
                    this["container"] = new DictionaryContextData();
                    return this["container"] as DictionaryContextData;
                }
            }
        }
    }
}
