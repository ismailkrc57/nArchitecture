using Application.Features.Brands.Dtos;
using MediatR;

namespace Application.Features.Brands.Commands.CreateBrand;

public class CreateBrandCommand : IRequest<CreatedBrandDto>
{
    public string Name { get; set; }
}