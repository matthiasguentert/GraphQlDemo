using Microsoft.EntityFrameworkCore;

namespace GraphQlDemo.Data
{
    public class DemoContext : DbContext
    {
        public DemoContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var posts = new List<Post>
            {
                new Post { PostId = 1, BlogId = 1, Title = "Post1", Content = "Content" },
                new Post { PostId = 2, BlogId = 1, Title = "Post2", Content = "Content" },
                new Post { PostId = 3, BlogId = 1, Title = "Post3", Content = "Content" },
                new Post { PostId = 4, BlogId = 1, Title = "Post4", Content = "Content" },
            };

            var blog = new Blog
            {
                BlogId = 1,
                Url = "https://foobar.com",
            };

            builder.Entity<Blog>().HasData(blog);
            builder.Entity<Post>().HasData(posts);
        }

        public DbSet<Blog> Blogs { get; set; }
    }

    public class Blog
    {
        public int BlogId { get; set; }

        public string Url { get; set; }

        public List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int BlogId { get; set; }

        public Blog Blog { get; set; }
    }
}
