using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Management.Api.Models.GoodsModel
{
    /// <summary>
    /// 物品扩展属性模板
    /// </summary>
    public class GoodsExtendFieldTemplate
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 扩展字段(使用[,]分割)
        /// </summary>
        public string GoodsExtendFields { get; set; }
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
    }
}
