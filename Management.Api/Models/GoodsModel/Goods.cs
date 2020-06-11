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
        public string GoodsName { get; set; }
        /// <summary>
        /// 封面图片
        /// </summary>
        public Guid CoverImageId { get; set; }
        /// <summary>
        /// 物品图片
        /// </summary>
        public GoodsImage[] GoodsImages { get; set; }

    }
    /// <summary>
    /// 物品类型
    /// </summary>
    public class GoodsType
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        public string Name { get; set; }
        public GoodsFidle[] GoodsFidles { get; set; }
        public string CreateUser { get; set; }
        public string LastUpdateUser { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }

    public class GoodsFidle
    {
        public Guid Id { get; set; }
        public string FidleName { get; set; }
        public string FidleKey { get; set; }

        public string CreateUser { get; set; }
        public string LastUpdateUser { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdateTime { get; set; }

    }


    public class GoodsImage
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 后缀名
        /// </summary>
        public string Extension { get; set; }
        /// <summary>
        /// 本地显示名称
        /// </summary>
        public string LocalName { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

    }
}
