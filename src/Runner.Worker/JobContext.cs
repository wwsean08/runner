﻿using System.Collections.Generic;
using GitHub.DistributedTask.Pipelines.ContextData;
using GitHub.Runner.Common.Util;
using GitHub.Runner.Common;
using GitHub.Runner.Sdk;

namespace GitHub.Runner.Worker
{
    public sealed class JobContext : DictionaryContextData
    {
        GlobalContext _globalContext;
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
                var permissions = _globalContext.Variables.Get("system.github.token.permissions") ?? "";
                if (!string.IsNullOrEmpty(permissions))
                {
                    var permissionsData = new DictionaryContextData();
                    this["permissions"] = permissionsData;
                    var permissionsJson = StringUtil.ConvertFromJson<Dictionary<string, string>>(permissions);
                    foreach (KeyValuePair<string, string> entry in permissionsJson)
                    {
                        permissionsData.Add(entry.Key, new StringContextData(entry.Value));
                    }
                    return this["permissions"] as DictionaryContextData;
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
