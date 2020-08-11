using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Management.Api.Models.GoodsModel
{
    public class Goods
    {
        /// <summary>
        /// 物品编号
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 物品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 物品图片
        /// </summary>
        public ICollection<GoodsImage> GoodsImages { get; set; }
        /// <summary>
        /// 扩展属性
        /// </summary>
        public ICollection<GoodsExtendAttribute> GoodsExtendAttributes { get; set; }
    }
}
