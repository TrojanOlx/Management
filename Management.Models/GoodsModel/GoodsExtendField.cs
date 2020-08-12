using System;
using System.Collections.Generic;

namespace Management.Models.GoodsModel
{
    /// <summary>
    /// 物品额外字段
    /// </summary>
    public class GoodsExtendField
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 字段Key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public ExtendFieldDateType DateType { get; set; }
        /// <summary>
        /// 字段提示
        /// </summary>
        public string Hint { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 最后更新人
        /// </summary>
        public string LastUpdateUser { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 物品扩展属性
        /// </summary>
        public ICollection<GoodsExtendAttribute> GoodsExtendAttributes { get; set; }
    }

    public enum ExtendFieldDateType
    {
        Int,
        Double,
        String,
        Select
    }
}