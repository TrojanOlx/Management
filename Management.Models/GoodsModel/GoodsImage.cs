using System;

namespace Management.Models.GoodsModel
{
    public class GoodsImage
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 物品Id
        /// </summary>
        public Guid GoodsId { get; set; }
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

        /// <summary>
        /// 物品
        /// </summary>
        public virtual Goods Goods { get; set; }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <returns></returns>
        public string GetImageName()
        {
            return Id + Extension;
        }
    }
}