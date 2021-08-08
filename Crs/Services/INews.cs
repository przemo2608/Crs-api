using Crs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crs.Services
{
    public interface INews
    {
        void CreateNews(string title, string content);
       IEnumerable <NewsDto> GetNews();
        void DeleteNews(int newsId);
    }
}
