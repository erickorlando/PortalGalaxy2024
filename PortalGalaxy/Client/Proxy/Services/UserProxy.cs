﻿using PortalGalaxy.Client.Proxy.Interfaces;
using PortalGalaxy.Shared.Request;
using PortalGalaxy.Shared.Response;

namespace PortalGalaxy.Client.Proxy.Services;

public class UserProxy : RestBase, IUserProxy
{
    public UserProxy(HttpClient httpClient) 
        : base("api/Users", httpClient)
    {
    }

    public async Task<LoginDtoResponse> Login(LoginDtoRequest request)
    {
        var response = await SendAsync<LoginDtoRequest, LoginDtoResponse>(request, HttpMethod.Post, "Login");
        return response;
    }

    public async Task Register(RegistrarUsuarioDto request)
    {
        var response = await SendAsync<RegistrarUsuarioDto, BaseResponse>(request, HttpMethod.Post, "Register");

        if (!response.Success)
            throw new ApplicationException(response.ErrorMessage);
    }

    public async Task SendTokenToResetPassword(GenerateTokenToResetDtoRequest request)
    {
        var response =
            await SendAsync<GenerateTokenToResetDtoRequest, BaseResponse>(request, HttpMethod.Post,
                "SendTokenToResetPassword");
        if (!response.Success)
            throw new ApplicationException(response.ErrorMessage);
    }

    public async Task ResetPassword(ResetPasswordDtoRequest request)
    {
        var response =
            await SendAsync<ResetPasswordDtoRequest, BaseResponse>(request, HttpMethod.Post,
                "ResetPassword");
        if (!response.Success)
            throw new ApplicationException(response.ErrorMessage);
    }
}