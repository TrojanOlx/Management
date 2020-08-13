using Management.Common.Models;
using Snowflake.Core;
using System;
using System.Collections.Generic;

namespace Management.Models.GoodsModel
{
    public class Goods
    {
        public Goods(string name)
        {
            Name = name;
            Id = GetSnowflakeId.Worker.NextId();
        }

        /// <summary>
        /// 物品编号
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 物品名称
        /// </summary>
        public string Name { get; set; }
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
        /// 物品图片
        /// </summary>
        public ICollection<GoodsImage> GoodsImages { get; set; }
        /// <summary>
        /// 扩展属性
        /// </summary>
        public ICollection<GoodsExtendAttribute> GoodsExtendAttributes { get; set; }
    }
}
