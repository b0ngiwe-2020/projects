using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SOSSservice
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {

        DataClasses1DataContext db = new DataClasses1DataContext();

        public Book getBook(int id)
        {
            var book = (from s in db.Books where s.Id.Equals(id) select s).FirstOrDefault();
            var SingleBook = new Book
            {
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                ISBN = book.ISBN,
                Available = book.Available,
                Condition = book.Condition,
                img = book.img,
                Category = book.Category,

            };

            return SingleBook;

        }

        public List<Book> getBooks()
        {
            var list = new List<Book>();

            var books = (from i in db.Books
                         where i.Available.Equals(1)
                         select i).DefaultIfEmpty();

            foreach (Book b in books)
            {
                list.Add(b);
            }
            return list;
        }


        public List<Book> getCategory(string category)
        {
            var list = new List<Book>();

            var books = (from i in db.Books
                         where i.Available.Equals(1) && i.Category == category
                         select i).DefaultIfEmpty();

            foreach (Book b in books)
            {
                list.Add(b);
            }
            return list;
        }

        public User Login(string email, string password)
        {
            var sysUser = (from u in db.Users
                           where u.Email.Equals(email) && u.Password.Equals(password)
                           select u).FirstOrDefault();

            if (sysUser != null)
            {
                var tempUser = new User
                {
                    Id = sysUser.Id,
                    Name = sysUser.Name,
                    Email = sysUser.Email,
                    Password = sysUser.Password,
                    Course = sysUser.Course,
                    Surname = sysUser.Surname,


                };
                return tempUser;
            }
            else
            {
                return null;
            }
        }

        public int Register(User u)
        {
            var sysUser = (from s in db.Users
                           where s.Email.Equals(u.Email) &&
                           s.Password.Equals(u.Password)
                           select s).FirstOrDefault();
            if (sysUser == null)
            {
                var newUser = new User
                {
                    Name = u.Name,
                    Email = u.Email,
                    Password = u.Password,
                    Surname = u.Surname,
                    Course = u.Course,
                };

                db.Users.InsertOnSubmit(newUser);
                try
                {
                    db.SubmitChanges();
                    return 1;

                }
                catch (Exception ex)
                {
                    ex.GetBaseException();
                    return -1;
                }
            }
            else
            {
                return 0;
                //user exists
            }
        }

        public int ResetPass(string email, string pass)
        {
            var user = (from u in db.Users
                        where u.Email.Equals(email)
                        select u).FirstOrDefault();

            if (user != null)
            {
                user.Password = pass;

                try
                {
                    db.SubmitChanges();
                    return 1;
                }
                catch (Exception ex)
                {
                    ex.GetBaseException();
                    return -1;
                }
            }
            else
            {
                return 0;
            }
        }

        public int UserExist(string email)
        {
            var user = (from u in db.Users
                        where u.Email.Equals(email)
                        select u).FirstOrDefault();

            if (user != null)
            {
                var tempUser = new User
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    Course = user.Course,
                    Surname = user.Surname,



                };
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}

