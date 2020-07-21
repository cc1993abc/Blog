using Blog.Models;
using Blog.Models.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Controllers.Repository
{
    public interface IRepository
    {
        Post GetPost(int id);
        List<Post> GetAllPosts();
        List<Post> GetAllPosts(string category);
        void AddPost(Post post);
        void RemovePost(int id);
        void UpdatePost(Post post);

        void AddSubComment(SubComment comment);

        Task<bool> SaveChangesAsync();
    }
}
