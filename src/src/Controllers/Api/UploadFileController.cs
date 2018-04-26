﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using src.Data;
using src.Models;
using src.Services;

namespace src.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/UploadFile")]
    public class UploadFileController : Controller
    {
        private readonly IDotnetdesk _dotnetdesk;
        private readonly IHostingEnvironment _env;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UploadFileController(IDotnetdesk dotnetdesk,
            IHostingEnvironment env,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _dotnetdesk = dotnetdesk;
            _env = env;
            _userManager = userManager;
            _context = context;
        }

        [HttpPost]
        [RequestSizeLimit(5000000)]
        public async Task<IActionResult> PostUploadFile(List<IFormFile> files)
        {
            var fileName = await _dotnetdesk.UploadFile(files, _env);
            //try to update the user profile pict
            ApplicationUser appUser = await _userManager.GetUserAsync(User);
            appUser.ProfilePictureUrl = "/uploads/" + fileName;
            _context.Update(appUser);
            _context.SaveChanges();
            return Ok(fileName);
        }
    }
}