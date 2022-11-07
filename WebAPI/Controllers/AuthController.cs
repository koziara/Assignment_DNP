﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Services;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WebAPI.Controllers;
[ApiController]
[Route("/auth")] 
public class AuthController : ControllerBase 
{ 
    private readonly IConfiguration config; 
    private readonly IAuthLogic authLogic;
    public AuthController(IConfiguration config, IAuthLogic authService)
                              {
                                  this.config = config;
                                  authLogic = authService;
                              }
                              
                              private List<Claim> GenerateClaims(User user)
                              {
                                  var claims = new[]
                                  {
                                      new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"]),
                                      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                      new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                                      new Claim(ClaimTypes.Name, user.UserName),
                                      new Claim("Id", user.Id.ToString())
                                  };
                                  return claims.ToList();
                              }
                              
                              private string GenerateJwt(User user)
                              {
                                  List<Claim> claims = GenerateClaims(user);
                              
                                  SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
                                  SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
                              
                                  JwtHeader header = new JwtHeader(signIn);
                              
                                  JwtPayload payload = new JwtPayload(
                                      config["Jwt:Issuer"],
                                      config["Jwt:Audience"],
                                      claims, 
                                      null,
                                      DateTime.UtcNow.AddMinutes(60));
                              
                                  JwtSecurityToken token = new JwtSecurityToken(header, payload);
                              
                                  string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
                                  return serializedToken;
                              }
                              [HttpPost, Route("login")]
                              public async Task<ActionResult> Login([FromBody] UserLoginDto userLoginDto)
                              {
                                  try
                                  {
                                      User user = await authLogic.ValidateUser(userLoginDto.Username, userLoginDto.Password);
                                      string token = GenerateJwt(user);
                              
                                      return Ok(token);
                                  }
                                  catch (Exception e)
                                  {
                                      return BadRequest(e.Message);
                                  }
                              }
                              
}