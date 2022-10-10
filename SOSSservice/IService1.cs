using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SOSSservice
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<Book> getBooks();

        [OperationContract]
        List<Book> getCategory(String category);

        [OperationContract]
        int Register(User u);
        [OperationContract]
        User Login(String email, String password);

        [OperationContract]
        int UserExist(String email);

        [OperationContract]
        int ResetPass(String email, String pass);
        [OperationContract]
        Book getBook(int id);
    }
}
