using System.Web.Routing;
using System.Web.Mvc;
using RestfulRouting;
using Demo.Controllers;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Demo.Routes), "Start")]

namespace Demo
{
    public class Routes : RouteSet
    {
        public override void Map(IMapper map)
        {
            map.DebugRoute("routedebug");

            map.Route(new Route("",
                                new RouteValueDictionary(new { controller = "Home", action = "Index" }),
                                new MvcRouteHandler()));

            map.Resources<UsersController>(users => {
                users.Except("create", "destroy");
                users.Resources<AccountsController>(accounts => {
                    accounts.Only("index", "show", "destroy");
                    accounts.Resources<TransactionsController>(transactions => {
                        transactions.Only("index", "create");
                    });
                });
            });
            /*
             * TODO: Add your routes here.
             * 
            map.Root<HomeController>(x => x.Index());
            
            map.Resources<BlogsController>(blogs =>
            {
                blogs.As("weblogs");
                blogs.Only("index", "show");
                blogs.Collection(x => x.Get("latest"));

                blogs.Resources<PostsController>(posts =>
                {
                    posts.Except("create", "update", "destroy");
                    posts.Resources<CommentsController>(c => c.Except("destroy"));
                });
            });

            map.Area<Controllers.Admin.BlogsController>("admin", admin =>
            {
                admin.Resources<Controllers.Admin.BlogsController>();
                admin.Resources<Controllers.Admin.PostsController>();
            });
             */
        }

        public static void Start()
        {
            var routes = RouteTable.Routes;
            routes.MapRoutes<Routes>();
        }
    }
}