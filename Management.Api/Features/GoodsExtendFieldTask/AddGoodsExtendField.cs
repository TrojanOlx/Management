using AutoMapper;
using Management.Api.Models;
using Management.Models.GoodsModel;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Management.Common.Helpers;


namespace Management.Api.Features.GoodsExtendFieldTask
{
    public class AddGoodsExtendField
    {
        public class AddGoodsExtendFieldCommand : IRequest<GoodsExtendField>
        {
            /// <summary>
            /// 字段名称
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 数据类型
            /// </summary>
            public ExtendFieldDateType DateType { get; set; }
            /// <summary>
            /// 字段提示
            /// </summary>
            public string Hint { get; set; }
        }

        public class Handler : IRequestHandler<AddGoodsExtendFieldCommand, GoodsExtendField>
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

            public async Task<GoodsExtendField> Handle(AddGoodsExtendFieldCommand request, CancellationToken cancellationToken)
            {

                var model = _mapper.Map<GoodsExtendField>(request);
                model.Key = request.Name.GetPinyin();

                model.CreateTime = DateTime.Now;
                // TODO: User
                model.CreateUser = _httpContext.HttpContext.User.Claims.FirstOrDefault(f => f.Type == "UserName").Value;
                model.LastUpdateTime = DateTime.Now;
                // TODO: User
                model.LastUpdateUser = "";
                await _managementContext.GoodsExtendFields.AddAsync(model);

                if (await _managementContext.SaveChangesAsync() > 0)
                {
                    return model;
                }
                return null;
            }
        }

    }
}
