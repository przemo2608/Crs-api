using Crs.Data;
using Crs.Exceptions;
using Crs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crs.Services
{
    public class NewsService : INews
    {

        private readonly ICrsContext db;


        public NewsService(ICrsContext db)
        {

            this.db = db;
        }

        public void CreateNews(string title, string content)
        {
            var news = new NewsDb
            {
                Title = title,
                Content = content,
                CreateDate = DateTime.Now.ToString()
            };
            db.News.Add(news);
            db.SaveChanges();
        }

        public void DeleteNews(int newsId)
        {
            var news = db.News.FirstOrDefault(x => x.Id == newsId);
            if (news == null)
                throw new ObjectNotFoundException();
            db.News.Remove(news);
            db.SaveChanges();
        }

        public IEnumerable <NewsDto> GetNews()
        {
            var news = db.News.ToList();
            if (news == null)
                throw new NewsNotFoundException();



            return news.Select(x => new NewsDto()
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
            });
        }

       
    }
}
