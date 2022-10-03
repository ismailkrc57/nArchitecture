using Application.Features.Auth.Commands.Register;
using Application.Features.Auth.Dtos;
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
using Core.Security.Dtos;
using Core.Security.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest request)
    {
        GetListModelQuery getListModelQuery = new() { PageRequest = request };
        ModelListModel result = await Mediator.Send(getListModelQuery);
        return Ok(result);
    }

    [HttpPost(nameof(Register))]
    public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
    {
        RegisterCommand registerCommand = new() { UserForRegisterDto = userForRegisterDto, IpAddress = GetIpAddress() };
        RegisteredDto result = await Mediator.Send(registerCommand);
        setRefreshTokenCookie(result.RefreshToken);
        return Created("", result.AccessToken);
    }

    private void setRefreshTokenCookie(RefreshToken refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.Now.AddDays(7)
        };
        Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
    }
}