using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Dtos;
using Application.Features.Brands.Models;
using Application.Features.Brands.Queries.GetByIdBrand;
using Application.Features.Brands.Queries.GetListBrand;
using Application.Features.Models.Models;
using Application.Features.Models.Queries.GetListModel;
using Application.Features.Models.Queries.GetListModelDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ModelsController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest request)
    {
        GetListModelQuery getListModelQuery = new() { PageRequest = request };
        ModelListModel result = await Mediator.Send(getListModelQuery);
        return Ok(result);
    }

    [HttpPost(nameof(GetListByDynamic))]
    public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest request, [FromBody] Dynamic dynamic)
    {
        GetListModelByDynamicQuery getListDynamicQuery = new() { PageRequest = request, Dynamic = dynamic };
        ModelListModel result = await Mediator.Send(getListDynamicQuery);
        return Ok(result);
    }
}