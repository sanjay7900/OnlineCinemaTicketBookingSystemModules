using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCinemaTicketBookingSystemModules
{
    internal class Program
    {
        static void Main(string[] args)
        {

            WrongChoise:
            Console.WriteLine("Login As Customer Press  1 :  ");
            Console.WriteLine("Login AS Admin Press     2 :  ");
            Console.WriteLine(" Exit Press 3              :  ");
            int choise=Convert.ToInt32(Console.ReadLine());
            LoginModule loginModule = new LoginModule();
            switch (choise)
            {
                case 1:
                   
                    if (loginModule.IsAuth("customers"))
                    {
                        Customers customers = new Customers();
                        customers.PerformAllActionOfCustomer();

                    }
                    else
                    {

                    }
                    break;
                  case 2:
                    if (loginModule.IsAuth("Admin"))
                    {
                        Admin admin = new Admin();
                        admin.PerFormAction();

                    }
                    else
                    {

                    }
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                    default:
                    Console.WriteLine("Wrong Choise ... try Again  ");
                    goto WrongChoise;
                   
                   
                    
            }


            Console.ReadLine();
        }
    }
}
