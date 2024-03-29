﻿using System.Collections.Generic;
using System.Threading.Tasks;
using FitAppServer.Auth.DTOs;
using FitAppServer.Services.DTOs.Users;
using FitAppServer.Services.Models;
using FitAppServer.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitAppServer.Auth.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _service;

    public UsersController(IUsersService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("", Name = "GetUserAsync")]
    public async Task<IActionResult> GetUserAsync(int id)
    {
        return Ok();
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> RegisterAsync(UserRegisterRequest user)
    {
        var errors = new List<UserRegisterError>();

        // Add checks to see if it's an actual email address
        if (string.IsNullOrEmpty(user.Email))
            errors.Add(new UserRegisterError
            {
                FieldName = "email",
                ErrorMessage = "Email is required.",
                ErrorCode = AuthErrorCode.InvalidEmail
            });

        if (string.IsNullOrEmpty(user.Username) || user.Username.Length < 3)
            errors.Add(new UserRegisterError
            {
                FieldName = "username",
                ErrorMessage = "Username must have at least 3 characters.",
                ErrorCode = AuthErrorCode.UsernameTooShort
            });

        if (string.IsNullOrEmpty(user.Password) || user.Password.Length < 6)
            errors.Add(new UserRegisterError
            {
                FieldName = "password",
                ErrorMessage = "Password must have at least 6 characters.",
                ErrorCode = AuthErrorCode.PasswordTooShort
            });
        else if (string.IsNullOrEmpty(user.PasswordConfirm) || user.Password != user.PasswordConfirm)
            errors.Add(new UserRegisterError
            {
                FieldName = "passwordConfirm",
                ErrorMessage = "Passwords do not match.",
                ErrorCode = AuthErrorCode.PasswordsNotEqual
            });

        var existingUser = await _service.GetByUsernameOrEmailAsync(user.Username, user.Email);

        // Check if user does not exist already
        if (existingUser != null)
        {
            if (existingUser.Email == user.Email)
                errors.Add(new UserRegisterError
                {
                    FieldName = "email",
                    ErrorMessage = "Email already exists.",
                    ErrorCode = AuthErrorCode.EmailAlreadyExists
                });

            if (existingUser.Username == user.Username)
                errors.Add(new UserRegisterError
                {
                    FieldName = "username",
                    ErrorMessage = "Username is already taken.",
                    ErrorCode = AuthErrorCode.UsernameAlreadyExists
                });
        }

        // Return model validation errors
        if (errors.Count > 0) return BadRequest(new { errors });

        var usr = new NewUser
        {
            Username = user.Username,
            Email = user.Email,
            Password = user.Password,
            PasswordConfirm = user.PasswordConfirm
        };

        var res = await _service.RegisterUserAsync(usr);

        if (res.ErrorCode == AuthErrorCode.GenericError)
            errors.Add(new UserRegisterError
            {
                FieldName = "general",
                ErrorMessage = "An error has occurred.",
                ErrorCode = res.ErrorCode
            });

        if (errors.Count > 0) return BadRequest(new { errors });


        return CreatedAtRoute("GetUserAsync", new
        {
            id = res.ID
        }, new
        {
            res.ID, res.SignInToken
        });
    }
}