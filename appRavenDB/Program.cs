using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client.Documents.Session;

namespace appRavenDB
{
    class Program
    {
        static void Main(string[] args)
        {
            var startDB = DocumentManager.Store;
            using var session = startDB.OpenSession();
            Insert(session);
            Modified(session);
            Delete(session);
        }

        private static void Delete(IDocumentSession session)
        {
            Author results = session
                .Query<Author>()
                .FirstOrDefault();

            if (results == null) 
                throw new Exception("Erro ao buscar documento");
                
           
            session.Delete(results);
            session.SaveChanges();
        }

        private static void Modified(IDocumentSession session)
        {
            Author results = session
                .Query<Author>()
                .FirstOrDefault();

            if (results == null) 
                throw new Exception("Erro ao buscar documento");
                
            
            results.Name = "Renato Cantarino";
            session.SaveChanges();
        }

        private static void Insert(IDocumentSession session)
        {
            var author = new Author()
            {
                Id = Guid.NewGuid().ToString(),
                Email = "xpto@teste.com.br",
                Name = "Renato",
                Post = new List<Post>()
                {
                    new Post()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Article = "Lorem ipsum",
                        DtPost = DateTime.Now,
                        Title = "Novo Artigo de Teste",
                        Comments = new List<Comment>()
                        {
                            new Comment()
                            {
                                Title = "Novo Comentário",
                                Text = "Lorem Ipsum",
                                CommentUser = "User Comment",
                                DtComment = DateTime.Now,
                                Id = Guid.NewGuid().ToString(),
                            }
                        }
                    }
                }
            };
            
            session.Store(author);
            session.SaveChanges();
        }
    }
    
    #region domain
    public class Author
    {
        public string Id { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public List<Post> Post { get; set; }
    }
    public class Post
    {
        public string Id { get; set; }
        public String Title { get; set; }
        public String Article { get; set; }
        public DateTime DtPost { get; set; }
        public List<Comment> Comments { get; set; }
    }
    public class Comment
    {
        public string Id { get; set; }
        public String Title { get; set; }
        public String Text { get; set; }
        public String CommentUser { get; set; }
        public DateTime DtComment { get; set; }
    }
    #endregion
}
 
    
   