﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Nest;

namespace ElasticSearchHelper.Nest
{
    /// <summary>
    /// Es基础实体自动映射
    /// </summary>
    public abstract class EsBaseDataAutoMapping<T> : EsBaseDataMapping<T> where T : class, new()
    {
        /// <summary>
        /// 实体映射
        /// </summary>
        /// <param name="client">es客户端</param>
        /// <param name="index">索引名称</param>
        public override void EntityMapping(ElasticClient client, string index)
        {
            client.Map<T>(m => m.AutoMap(new AllStringToKeywordValuesPropertyVisitor()).AllField(a => a.Enabled(false)).Index(index));
        }
    }

    /// <summary>
    /// es字段映射string类型观察者
    /// </summary>
    public class AllStringToKeywordValuesPropertyVisitor : NoopPropertyVisitor
    {
        public override void Visit(
            ITextProperty type,
            PropertyInfo propertyInfo,
            ElasticsearchPropertyAttributeBase attribute)
        {
            type.Type = "keyword";
            type.Fields = null;
        }
    }
}
