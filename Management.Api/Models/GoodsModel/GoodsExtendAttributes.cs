using System;

namespace Management.Api.Models.GoodsModel
{
    /// <summary>
    /// 物品类型
    /// </summary>
    public class GoodsExtendAttribute
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 属性字段Id
        /// </summary>
        public Guid GoodsExtendFieldId { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 属性字段
        /// </summary>
        public virtual GoodsExtendField GoodsExtendField { get; set; }
    }
}