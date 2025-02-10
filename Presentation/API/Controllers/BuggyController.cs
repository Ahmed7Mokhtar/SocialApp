using System;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace API.Controllers;

public class BuggyController : BaseController
{
    private readonly DataContext _context;
    public BuggyController(DataContext context)
    {
        _context = context;
        
    }

    [HttpGet("auth")]
    [Authorize]
    public ActionResult<string> GetSecret()
    {
        return "secret text";
    }

    [HttpGet("not-found")]    
    public ActionResult<AppUser> GetNotFound()
    {
        var thing = _context.AppUsers.Find("asdad");
        if(thing is null)
            return NotFound();
        
        return thing;
    }

    [HttpGet("server-error")]
    public ActionResult<string> GetServerError()
    {
        var thing = _context.AppUsers.Find("asdad");

        var thingToReturn = thing.ToString();

        return thingToReturn;
    }

    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest()
    {
        return BadRequest("This was not a good request");
    }
}
