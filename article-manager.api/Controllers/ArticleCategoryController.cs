using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using article_manager.api.Repositories;
using backendlib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace article_manager.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleCategoryController : ControllerBase
    {
        private readonly ICRUDRepository<ArticleCategory> _articleCategoryRepository;
        private readonly IConfiguration _configuration;
        // private readonly SignInManager<IdentityUser> _signInManager;

        public ArticleCategoryController(ICRUDRepository<ArticleCategory> articleCategoryRepository, IConfiguration configuration)
        //    SignInManager<IdentityUser> signInManager)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _configuration = configuration;
            // _signInManager = signInManager;
        }


        // [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetArticleCategorysAsync()
        {
            return Ok(await _articleCategoryRepository.GetList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetArticleCategoryAsync(int id)
        {
            return Ok(await _articleCategoryRepository.Get(id));
        }

    }
}