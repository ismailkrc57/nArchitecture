using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.CommandHandlers;

public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandDto>
{
    private readonly IBrandRepository _repository;
    private readonly IMapper _mapper;
    private readonly BrandBusinessRules _businessRules;

    public CreateBrandCommandHandler(IBrandRepository repository, IMapper mapper, BrandBusinessRules businessRules)
    {
        _repository = repository;
        _mapper = mapper;
        _businessRules = businessRules;
    }


    public async Task<CreatedBrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        await _businessRules.BrandNameCanNotBeDuplicatedWhenInserted(request.Name);

        Brand mappedBrand = _mapper.Map<Brand>(request);
        Brand createdBrand = await _repository.AddAsync(mappedBrand);
        CreatedBrandDto createdBrandDto = _mapper.Map<CreatedBrandDto>(createdBrand);

        return createdBrandDto;
    }
}