using AutoMapper;
using Management.Api.Models;
using Management.Models.GoodsModel;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace Management.Api.Features.GoodsTask
{
    public class AddGoods
    {
        public class AddGoodsCommand : IRequest<bool>
        {
            public string Name { get; set; }
            public Dictionary<string, object> ExtendValues { get; set; }
        }


        public class Handler : IRequestHandler<AddGoodsCommand, bool>
        {
            private readonly ManagementContext _managementContext;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContext;

            public Handler(ManagementContext managementContext, IMapper mapper, IHttpContextAccessor httpContext)
            {
                _managementContext = managementContext;
                _mapper = mapper;
                _httpContext = httpContext;
            }

            public async Task<bool> Handle(AddGoodsCommand request, CancellationToken cancellationToken)
            {
                var goods = new Goods(request.Name);
                goods.CreateTime = DateTime.Now;
                // TODO: User
                goods.CreateUser = _httpContext.HttpContext.User.Claims.FirstOrDefault(f => f.Type == "UserName").Value;
                goods.LastUpdateTime = DateTime.Now;
                // TODO: User
                goods.LastUpdateUser = "";


                var ImageIds = JsonConvert.DeserializeObject<List<Guid>>(request.ExtendValues["images"].ToString());

                var keys = request.ExtendValues.Keys;
                var extendFields = _managementContext.GoodsExtendFields.Where(w => keys.Contains(w.Key));
                List<GoodsExtendAttribute> extendAttributes = new List<GoodsExtendAttribute>();
                foreach (var extendField in extendFields)
                {
                    string value = request.ExtendValues[extendField.Key]?.ToString();
                    if (!string.IsNullOrEmpty(value))
                    {
                        extendAttributes.Add(new GoodsExtendAttribute
                        {
                            GoodsId = goods.Id,
                            GoodsExtendFieldId = extendField.Id,
                            Value = value
                        });
                    }
                }

                // 事务
                using IDbContextTransaction tran = _managementContext.Database.BeginTransaction();
                try
                {

                    // 更新图片
                    await _managementContext.GoodsImages.Where(w => w.GoodsId == goods.Id).ForEachAsync(f => f.GoodsId = null);
                    await _managementContext.GoodsImages.Where(w => ImageIds.Contains(w.Id)).ForEachAsync(f => f.GoodsId = goods.Id);
                    // 保存物品
                    await _managementContext.Goods.AddAsync(goods);
                    await _managementContext.GoodsExtendAttributes.AddRangeAsync(extendAttributes);
                    var result = await _managementContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return result > 0;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    // 保存错误 or 抛出错误
                }
                return false;

            }
        }

    }
}
