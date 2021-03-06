﻿using Blog.Data;
using Blog.Models;
using Blog.Models.Comment;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Controllers.Repository
{
    public class Repository : IRepository
    {
        private AppDbContext _ctx { get; set; }
        public Repository(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        public void AddPost(Post post)
        {
           _ctx.posts.Add(post);
        }

        public List<Post> GetAllPosts()
        {
            return _ctx.posts.ToList();
        }
        public List<Post> GetAllPosts(string category)
        {
            return _ctx.posts.Where(item => item.Category.ToLower().Equals(category.ToLower())).ToList();
        }

        public Post GetPost(int id)
        {
            return _ctx.posts.Include(p=> p.MainComments).ThenInclude(mc => mc.SubComments).FirstOrDefault(p => p.Id == id);

        }

        public void RemovePost(int id)
        {
            _ctx.posts.Remove(GetPost(id));
        }

        public void UpdatePost(Post post)
        {
            _ctx.posts.Update(post);
        }

        public async Task<bool> SaveChangesAsync()
        {
            if(await _ctx.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public void AddSubComment(SubComment comment)
        {
            _ctx.subComments.Add(comment);
        }
    }
}
