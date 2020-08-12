using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Management.Api.Models.GoodsModel;

namespace Management.Api.Models
{
    public class ManagementContext : DbContext
    {
        public ManagementContext(DbContextOptions<ManagementContext> options) : base(options)
        {

        }

        /// <summary>
        /// 物品
        /// </summary>
        public DbSet<Goods> Goods { get; set; }
        /// <summary>
        /// 物品扩展属性
        /// </summary>
        public DbSet<GoodsExtendAttribute> GoodsExtendAttributes { get; set; }
        /// <summary>
        /// 物品额外字段
        /// </summary>
        public DbSet<GoodsExtendField> GoodsExtendFields { get; set; }
        /// <summary>
        /// 物品扩展属性模板
        /// </summary>
        public DbSet<GoodsExtendFieldTemplate> GoodsExtendFieldTemplates { get; set; }
        /// <summary>
        /// 物品图片
        /// </summary>
        public DbSet<GoodsImage> GoodsImages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<GoodsExtendAttribute>().HasKey(key => key.Id);

            builder.Entity<GoodsExtendField>(entity =>
            {
                entity.HasKey(key => key.Id);
                entity.HasMany(many => many.GoodsExtendAttributes)
                    .WithOne(one => one.GoodsExtendField)
                    .HasPrincipalKey(key => key.Id)
                    .HasForeignKey(key => key.GoodsExtendFieldId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<GoodsExtendFieldTemplate>().HasKey(key => key.Id);

            builder.Entity<GoodsImage>().HasKey(key => key.Id);


            builder.Entity<Goods>(entity =>
            {
                entity.HasKey(key => key.Id);
                entity.HasMany(many => many.GoodsExtendAttributes)
                    .WithOne(one => one.Goods)
                    .HasPrincipalKey(key => key.Id)
                    .HasForeignKey(key => key.GoodsId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(many => many.GoodsImages)
                    .WithOne(one => one.Goods)
                    .HasPrincipalKey(key => key.Id)
                    .HasForeignKey(key => key.GoodsId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            base.OnModelCreating(builder);
        }
    }
}
