using Application.Features.Models.Models;
using Application.Features.Models.Queries.GetListModel;
using Application.Features.Models.Queries.GetListModelDynamic;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using FluentAssertions.Equivalency;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Models.Queries.QueryHandlers;

public class GetListModelGetListModelByDynamicQueryHandlerHandler : IRequestHandler<GetListModelByDynamicQuery, ModelListModel>

{
    private readonly IMapper _mapper;
    private readonly IModelRepository _modelRepository;

    public GetListModelGetListModelByDynamicQueryHandlerHandler(IMapper mapper, IModelRepository modelRepository)
    {
        _mapper = mapper;
        _modelRepository = modelRepository;
    }


    public async Task<ModelListModel> Handle(GetListModelByDynamicQuery request, CancellationToken cancellationToken)
    {
        IPaginate<Model> models = await _modelRepository.GetListByDynamicAsync(request.Dynamic,include:
            m => m.Include(c => c.Brand)!,
            index: request.PageRequest.Page,
            size: request.PageRequest.PageSize, cancellationToken: cancellationToken);
        ModelListModel mappedModel = _mapper.Map<ModelListModel>(models);
        return mappedModel;
    }
}