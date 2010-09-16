using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;
//using Norm.

namespace example
{
    public class Post
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public DateTime PostDate { get; set; }
        public string Content { get; set; }
        public IEnumerable<string> Tag { get; set;}
        public IEnumerable<string> Category { get; set; }
    }

    public class Comment
    {
        public string Author { get; set; }
        public string Text { get; set; }
    }

    public class Examples
    {
        public static void Main()
        {
            Run();
        }

        public static void Run()
        {
            using (var db = Mongo.Create("mongodb://localhost/BlogDB"))
            {
                //get LINQ access to your Posts
                //Mongo created the database and collection on the fly!
                var posts = db.GetCollection<Post>();
                var postsQ = posts.AsQueryable();

                //insert into collection
                posts.Insert(new Post
                {
                    ID = 1,
                    Title = "Hello NoRM",
                    Content = "Hello MongoDB"
                });

                //find a post by id
                var p1 = postsQ.FirstOrDefault(p => p.ID == 1);

                //update the post.
                p1.Title = "NoRM, a perfect complement to MongoDB!";
                p1.Comments = new[]
                              {
                                  new Comment() {Author = "Jon", Text = "this is a comment"},
                                  new Comment() {Author = "Jon", Text = "this is a comment 2"},
                              };
                posts.Save(p1);
            }
        }
}
}
