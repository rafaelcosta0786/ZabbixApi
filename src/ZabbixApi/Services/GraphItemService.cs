﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZabbixApi.Entities;
using ZabbixApi.Helper;
using ZabbixApi;

namespace ZabbixApi.Services
{
    public interface IGraphItemService
    {
        IEnumerable<GraphItem> Get(object filter = null, IEnumerable<GraphItemInclude> include = null, Dictionary<string, object> @params = null);
    }

    public class GraphItemService : ServiceBase<GraphItem>, IGraphItemService
    {
        public GraphItemService(IContext context) : base(context, "graphitem") { }

        public IEnumerable<GraphItem> Get(object filter = null, IEnumerable<GraphItemInclude> include = null, Dictionary<string, object> @params = null)
        {
            var includeHelper = new IncludeHelper(include == null ? 1 : include.Sum(x => (int)x));
            if(@params == null)
                @params = new Dictionary<string, object>();

            @params.AddOrReplace("output", "extend");
            @params.AddOrReplace("selectGraphs", includeHelper.WhatShouldInclude(GraphItemInclude.Graphs));

            @params.AddOrReplace("filter", filter);
            
            return BaseGet(@params);
        }
    }

    public enum GraphItemInclude
    {
        All = 1,
        None = 2,
        Graphs = 4
    }
}
