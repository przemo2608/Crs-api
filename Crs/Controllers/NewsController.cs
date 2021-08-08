using Crs.Authorization;
using Crs.Model;
using Crs.Model.View;
using Crs.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private INews newsService;

        public NewsController(INews newsService)
        {
            this.newsService = newsService;
        }
        
        [HttpPost]
        [Route("createNews")]
        public IActionResult CreateNews(CreateNewsModel createNewsModel)
        {
            if (!ModelState.IsValid)
                throw new Exception("Invalid model");
            var newsDto = new NewsDto
            {
                Title = createNewsModel.Title,
                Content = createNewsModel.Content
            };
            
            newsService.CreateNews(createNewsModel.Title, createNewsModel.Content);
            return new OkResult();
           
        }

        [HttpGet]
        [Route("getNews")]
        public IEnumerable<NewsModel> GetNews()
        {
            var news = newsService.GetNews();

            return news.Select(x => new NewsModel()
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content
            });
        }
        [AdminRole]
        [HttpDelete]
        [Route("deleteNews")]
        public IActionResult DeleteNews(DeleteNewsModel deleteNewsModel)
        {
            if (!ModelState.IsValid)
                throw new Exception("Invalid model");

            newsService.DeleteNews(deleteNewsModel.newsId);

            return new OkResult();
        }
    }

}
