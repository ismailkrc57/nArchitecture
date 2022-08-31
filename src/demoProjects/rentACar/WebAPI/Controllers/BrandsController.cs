﻿using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Dtos;
using Application.Features.Brands.Models;
using Application.Features.Brands.Queries.GetByIdBrand;
using Application.Features.Brands.Queries.GetListBrand;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateBrandCommand createBrandCommand)
    {
        CreatedBrandDto result = await Mediator.Send(createBrandCommand);
        return Created("", result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest request)
    {
        GetListBrandQuery getListBrandQuery = new() {PageRequest = request};
        BrandListModel result = await Mediator.Send(getListBrandQuery);
        return Ok(result);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetByIdBrand([FromRoute] GetByIdBrandQuery getByIdBrandQuery)
    {
        BrandGetByIdDto brandGetByIdDto = await Mediator.Send(getByIdBrandQuery);
        return Ok(brandGetByIdDto);
    }
}