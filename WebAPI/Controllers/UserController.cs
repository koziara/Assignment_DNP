using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;


[ApiController]
[Route("/users")]
public class UserController : ControllerBase
{
    private readonly IUserLogic userLogic;

    public UserController(IUserLogic userLogic)
    {
        this.userLogic = userLogic;
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateAsync(UserCreationDto dto)
    {
        try
        {
            User user = await userLogic.CreateAsync(dto);
            return Created($"/users/{user.Id}", user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAsync()
    {
        try
        {
            
            IEnumerable<User> users = await userLogic.GetAsync();
            return Ok(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("/user")]
    public async Task<ActionResult<User>> GetByNameAsync(string username)
    {
        try
        {
            User? user = await userLogic.GetByNameAsync(username);
            return Created($"/{username}", user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}