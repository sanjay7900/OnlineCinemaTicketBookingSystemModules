using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace OnlineCinemaTicketBookingSystemModules
{
    public class LoginModule
    {
        private FileStream _file;
        private StreamReader _reader;
         private int _id;
        private int password;
        public bool IsAuth(string usertype)
        {
            Console.WriteLine("Enter The "+ usertype +" Id : ");
             _id=Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the Admin PassWord:");
             password=Convert.ToInt32(Console.ReadLine());   
            _file=new FileStream(@"D:\aspdotnet\CSharpOPPsRepo\OnlineCinemaTicketBookingSystemModules\Auth.txt", FileMode.Open,FileAccess.Read);
            _reader=new StreamReader(_file);
            while(_reader.Peek() > 0)
            {
                string lines=_reader.ReadLine();
               
                if (lines!="")
                {
                    string[] line = lines.Split(',');
                    if (Convert.ToInt32(line[0]) ==_id && Convert.ToInt32(line[2]) == password )
                    {
                        
                        if(line[4].Trim() == usertype)
                        {
                            Admin.id = _id;
                            _reader.Dispose();
                            _reader.Close();
                            _file.Close();
                            return true;
                        }
                        else if(line[4] == usertype)
                        {
                            Customers.id = Convert.ToInt32(line[0]);
                            _reader.Dispose();
                            _reader.Close();
                            _file.Close();  
                            return true;
                        }
                        

                    }
                }
                
            }
             return false;
        }


    }
}
