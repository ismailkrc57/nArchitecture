using Application.Features.Brands.Dtos;
using Application.Features.Brands.Models;
using Application.Features.Brands.Queries.GetByIdBrand;
using Application.Features.Brands.Queries.GetListBrand;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Queries.QueryHandlers;

public class GetByIdBrandQueryHandler : IRequestHandler<GetByIdBrandQuery, BrandGetByIdDto>
{
    private IBrandRepository _brandRepository;
    private IMapper _mapper;
    private BrandBusinessRules _businessRules;

    public GetByIdBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper, BrandBusinessRules businessRules)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
        _businessRules = businessRules;
    }

    public async Task<BrandGetByIdDto> Handle(GetByIdBrandQuery request, CancellationToken cancellationToken)
    {
        Brand? brand = await _brandRepository.GetAsync(b => b.Id == request.Id);
        _businessRules.BrandShouldExistWhenRequested(brand);
        BrandGetByIdDto brandGetByIdDto = _mapper.Map<BrandGetByIdDto>(brand);
        return brandGetByIdDto;
    }
}