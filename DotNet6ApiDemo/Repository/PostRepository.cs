using DotNet6ApiDemo.Context;
using DotNet6ApiDemo.Interfaces.Repository;
using DotNet6ApiDemo.Models;
using EF.Core.Repository.Repository;

namespace DotNet6ApiDemo.Repository
{
    public class PostRepository :CommonRepository<Post>,IPostRepository
    {


        public PostRepository(ApplicationDbContext dbcontext) : base(dbcontext)
        {



        }


    }
}
