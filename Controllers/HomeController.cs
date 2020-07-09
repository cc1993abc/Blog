using Blog.Controllers.Repository;
using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        public IRepository _repo { get; set; }
        public HomeController(IRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            var posts = _repo.GetAllPosts();
            return View(posts);
        }
        public IActionResult Post(int id)
        {
            var post = _repo.GetPost(id);
            return View(post);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
            return View(new Post());
            }
            var post = _repo.GetPost(id.Value);
            return View(post);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
            _repo.AddPost(post);

            if(await _repo.SaveChangesAsync())
            {
                return RedirectToAction("Index");
            }

            return View("Post");
        }
    }
}
