using Blog.Controllers.Repository;
using Blog.Data.FileManager;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [Authorize(Roles ="admin")]
    public class PanelController : Controller
    {
        public IRepository _repo { get; set; }

        private IFileManager _fileManager;

        public PanelController(IRepository repo,IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;
        }
        public IActionResult Index()
        {
            var posts = _repo.GetAllPosts();
            return View(posts);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View(new PostViewModel());
            }
            var post = _repo.GetPost(id.Value);
            return View(new PostViewModel()
            {
                Title = post.Title,
                Body = post.Body,
                Id = post.Id
            }) ;
        }
        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel vm)
        {



            var post = new Post()
            {
                Title = vm.Title,
                Body = vm.Body,
                Id = vm.Id,
                Image = await _fileManager.SaveImage(vm.Image)
            };

            if (post.Id > 0)
            {
                _repo.UpdatePost(post);
            }
            else
            {
                _repo.AddPost(post);

            }

            if (await _repo.SaveChangesAsync())
            {
                return RedirectToAction("Index");
            }

            return View("Post");
            

        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            _repo.RemovePost(id);
            await _repo.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
