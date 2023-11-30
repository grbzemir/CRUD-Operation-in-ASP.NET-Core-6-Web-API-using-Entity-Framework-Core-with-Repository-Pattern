using CoreApiResponse;
using DotNet6ApiDemo.Context;
using DotNet6ApiDemo.Interfaces.Manager;
using DotNet6ApiDemo.Manager;
using DotNet6ApiDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace DotNet6ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : BaseController
    {
        //Action Result isteklere göre işlem yapıp isteğe göre sonuç döndüren yapıdır.

        ApplicationDbContext _dbContext;

        IPostManager _postManager;

        //public PostController(ApplicationDbContext dbContext)

        //{

        //    _dbContext = dbContext;
           



        //}

        public PostController(IPostManager postManager)

        {

            _postManager = postManager;

        }

        [HttpGet]

        public IActionResult GetAll()

        {

            try

            {
                // var posts =  _dbContext.Pots.ToList();

                var posts = _postManager.GetAll().OrderBy(c=>c.CreatedDate).ThenBy(c=>c.Title).ToList();

                return CustomResult("Data Loaded Successfully",posts);

            }

            catch(Exception ex)

            {

                return CustomResult("Post deleted Failed", HttpStatusCode.BadRequest);


            }

            

        }

        [HttpGet("title")]

        public IActionResult GetAll(string title)

        {

            try

            {

                var posts = _postManager.GetAll(title).OrderBy(c => c.CreatedDate).ThenBy(c => c.Title).ToList();
                return CustomResult("data loaded successfully", posts.ToList());


            }

            catch(Exception ex)

            {

                return CustomResult("Post deleted Failed", HttpStatusCode.BadRequest);


            }

        }

        [HttpGet]

        public IActionResult SearchPost(string text)

        {

            try

            {

                Expression<Func<Post, bool>> filter = c => c.Title.Contains(text) || c.Description.Contains(text);

                var posts = _postManager.Get(filter).OrderBy(c => c.CreatedDate).ThenBy(c => c.Title).ToList();

                return CustomResult("Data Loaded Successfully", posts);

            }

            catch (Exception ex)

            {

                return CustomResult("Post deleted Failed", HttpStatusCode.BadRequest);

            }
        }

        [HttpGet]

        public IActionResult GetPosts(int page , int pageSize)

        {

            try

            {

                var posts = _postManager.GetPosts(page, pageSize).OrderBy(c => c.CreatedDate).ThenBy(c => c.Title).ToList();

                return CustomResult("Data Loaded Successfully", posts);

            }

            catch (Exception ex)

            {

                return CustomResult("Post deleted Failed", HttpStatusCode.BadRequest);

            }

        }


        public IActionResult GetAllDesc()

        {

            try

            {

                // var posts =  _dbContext.Pots.ToList();

                var posts = _postManager.GetAll().OrderByDescending(c => c.CreatedDate).ThenByDescending(c=>c.Title).ToList();

                return CustomResult("Data Loaded Successfully", posts);

            }

            catch (Exception ex)

            {

                return CustomResult("Post deleted Failed", HttpStatusCode.BadRequest);

            }

        }

        [HttpGet("id")]   

        public IActionResult GetById(int id)

        {

            try


            {

                //var post = _dbContext.Pots.FirstOrDefault(x => x.Id == id);

                var post = _postManager.GetById(id);

                if(post == null)

                {

                    return CustomResult("Data not found",HttpStatusCode.NotFound);

                }

                return CustomResult("Data Loaded Successfully", post);


                return Ok(post);


            }

            catch(Exception ex)

            {

                return CustomResult(ex.Message, HttpStatusCode.BadRequest);

            }

        }

        [HttpPost]

        public IActionResult Add(Post post) 
        
        {

            try
            {

                post.CreatedDate = DateTime.Now;
                bool isSaved = _postManager.Add(post);
                //_dbContext.Pots.Add(post);
                //bool isSaved = _dbContext.SaveChanges() > 0;

                if (isSaved)

                {
                    //return Created("", post);

                    return CustomResult("Post has been created successfully", post);
                }

                return CustomResult("Post save failed." ,HttpStatusCode.BadRequest);



            }

            catch (Exception ex)

            {

                return CustomResult(ex.Message,HttpStatusCode.BadRequest);


            }





        }

        [HttpPut]

        public IActionResult Edit(Post post)

        {
            try
            {

                if (post.Id == 0)

                {

                    return CustomResult("Id is missing.", HttpStatusCode.BadRequest);

                }


                bool isUpdated = _postManager.Update(post);

                if (isUpdated)

                {

                    return CustomResult("Post updated successfully", post);

                }


                return CustomResult("Post update failed", HttpStatusCode.BadRequest);

            }

            catch (Exception ex)

            {

                return CustomResult("Post deleted Failed", HttpStatusCode.BadRequest);


            }
        }

        [HttpDelete]

        public IActionResult Delete(int id)

        {
            try
            {

                var post = _postManager.GetById(id);

                if (post == null)

                {

                    return CustomResult("Data not found", HttpStatusCode.NotFound);

                }

                bool isDeleted = _postManager.Delete(post);

                if (isDeleted)

                {

                    return CustomResult("Post deleted successfully");

                }

                return CustomResult("Post deleted Failed" ,HttpStatusCode.BadRequest);



            }

            catch (Exception ex)

            {

                return BadRequest(ex.Message);


            }

        }

    }
}
