﻿using Authorization.Data;
using Authorization.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly DataContext _db;

        public BuggyController(DataContext db)
        {
            this._db = db;
        }
        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "Secret Text";
        }
        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var thing = _db.PensionUser.Find(-1);
            if(thing == null) return NotFound();
            return Ok(thing);
        }
        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var thing = _db.PensionUser.Find(-1);
            var thingToReturn = thing.ToString();
            return thingToReturn;
        }
        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This was not a good request");
        }
    }
}
