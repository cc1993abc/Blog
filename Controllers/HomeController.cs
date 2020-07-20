using Blog.Controllers.Repository;
using Blog.Data;
using Blog.Data.FileManager;
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

        private IFileManager _fileManager;

        public HomeController(IRepository repo,IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;
        }
        public IActionResult Index(string category) => View(string.IsNullOrWhiteSpace(category) ? _repo.GetAllPosts() : _repo.GetAllPosts(category));

        //public IActionResult Index(string category)
        //{
        //    var posts = string.IsNullOrWhiteSpace(category)? _repo.GetAllPosts():_repo.GetAllPosts(category);
        //    return View(posts);
        //}

        public IActionResult Post(int id) =>  View(_repo.GetPost(id));



        [HttpGet("/Image/{image}")]
        [ResponseCache(CacheProfileName ="monthly")]
        public IActionResult Image(string image) => new FileStreamResult(_fileManager.ImageStream(image), $"image/{image.Substring(image.LastIndexOf('.'))}");
        

        //[HttpGet("/Image/{image}")]
        //public IActionResult Image(string image)
        //{
        //    var mime = image.Substring(image.LastIndexOf('.'));
        //    return new FileStreamResult(_fileManager.ImageStream(image),$"image/{mime}");
        //}
    }

}
