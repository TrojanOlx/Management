using System;

namespace Management.Api.Models.GoodsModel
{
    /// <summary>
    /// 物品扩展属性
    /// </summary>
    public class GoodsExtendAttribute
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 物品Id
        /// </summary>
        public Guid GoodsId { get; set; }
        /// <summary>
        /// 属性字段Id
        /// </summary>
        public Guid GoodsExtendFieldId { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 物品
        /// </summary>
        public virtual Goods Goods { get; set; }
        /// <summary>
        /// 属性字段
        /// </summary>
        public virtual GoodsExtendField GoodsExtendField { get; set; }
    }
}