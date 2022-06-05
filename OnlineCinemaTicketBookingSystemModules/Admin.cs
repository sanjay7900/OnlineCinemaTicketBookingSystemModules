using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace OnlineCinemaTicketBookingSystemModules
{
    public class Admin
    {
        private FileStream _stream; 
        private StreamReader _reader;
        private StreamWriter _writer;
        public static int id;
        public void menu()
        {
            Console.WriteLine("1 : Show the Today's Booked Ticket");
            Console.WriteLine("2 : Show All the Movie As Date Wise");
            Console.WriteLine("3 : Show the History Booked List");
            Console.WriteLine("4 : Add A Movie");
            Console.WriteLine("5 : Delete A Movie");
            Console.WriteLine("6 : Exit...");
        }
        public void ShowDateWise()
        {
            string date;
            Console.WriteLine(" Enter the date (dd/mm/yyyy): ");
            date =Console.ReadLine(); 
            _stream = new FileStream(@"D:\aspdotnet\CSharpOPPsRepo\OnlineCinemaTicketBookingSystemModules\Movies.txt", FileMode.Open, FileAccess.Read);
            _reader = new StreamReader(_stream);
          //  string[] lines = _reader.ReadToEnd().Split('\n');
            while(_reader.Peek()>0)
            {
                string line=_reader.ReadLine();
                if (line!= "")
                {
                    string [] parts = line.Split(',');
                    if (date == parts[4].ToString().Trim())
                    {
                        for(int i = 0; i < parts.Length; i++)
                        {
                            Console.Write(parts[i] + " \t") ;
                        }
                    }
                }
                Console.WriteLine();    
            }
            _reader.Dispose();
            _reader.Close();
            _stream.Close();
            

        }
        public void TodaysBookedTicket()
        {
            DateTime date= DateTime.Now;
            string dateStr = date.ToString();
            
            _stream = new FileStream(@"D:\aspdotnet\CSharpOPPsRepo\OnlineCinemaTicketBookingSystemModules\Ticket.txt", FileMode.Open, FileAccess.Read);
            _reader = new StreamReader(_stream);
            //string[] lines = _reader.ReadToEnd().Split('\n');
            //Console.WriteLine(lines.Length);
            while(_reader.Peek()>0)
            {
               string lines= _reader.ReadLine();
                if (lines != "")
                {
                    string[] parts = lines.Split(',');
                    if (dateStr == parts[5].ToString())
                    {
                        foreach (string part in parts)
                        {
                            Console.Write(part + " \t");
                        }
                    }
                }
                Console.WriteLine();
            }
            _reader.Dispose();
            _reader.Close();
            _stream.Close();

        }
        public void HistoryBookedTicket()
        {
            _stream = new FileStream(@"D:\aspdotnet\CSharpOPPsRepo\OnlineCinemaTicketBookingSystemModules\Movies.txt", FileMode.Open, FileAccess.Read);
            _reader = new StreamReader(_stream);
            //string[] lines = _reader.ReadToEnd().Split('\n');
            while(_reader.Peek() > 0)
            {
                string line= _reader.ReadLine();
            
                //Console.WriteLine(line);
                if (line!="")
                {
                    string[] parts = line.Split(',');
                    
                    for(int i = 0; i < parts.Length; i++)
                    {
                        Console.Write(parts[i]+"\t");
                    }
                        //foreach (string part in parts)
                        //{
                        //    Console.Write(part + " \t");
                        //}
                    
                }
                Console.WriteLine();
            }
            _reader.Dispose();
            _reader.Close();
            _stream.Close();

        }
        public void AddAMovie()
        {
            try
            {

           
            _stream=new FileStream(@"D:\aspdotnet\CSharpOPPsRepo\OnlineCinemaTicketBookingSystemModules\Movies.txt",FileMode.Append, FileAccess.Write);
            _writer = new StreamWriter(_stream);
            maintask:

                Console.WriteLine("Enter The Movie Id :");
                int ids = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter The Movie Name : ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter The Mall Name On Which It Will Show");
                string mallname = Console.ReadLine();
                Console.WriteLine("Enter the  city ");
                string city = Console.ReadLine();
               
                string date = DateTime.Now.ToString("dd/MM/yyyy");
                Console.WriteLine("Enter The Show time(h:m to h:m pm/am)");

                string time = Console.ReadLine();
                Console.WriteLine("Enter The Price For One Ticket :");
                int price = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the Total Seats :");
                int mallseat = Convert.ToInt32(Console.ReadLine());
                try
                {
                    _writer.Write(ids.ToString()+","+ name + "," + mallname + "," + city + "," + date + "," + time.ToString() + "," + price.ToString() + "," + mallseat.ToString() + "," + mallseat.ToString() + "," + Admin.id.ToString()+"\n");
                    Console.WriteLine(" Added Successfully...");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Message : " + ex.Message);
                }
                Console.WriteLine("Do you Want to Add More ? 1:0");
                int more = Convert.ToInt32(Console.ReadLine());
                if (more == 1)
                {
                    goto maintask;
                }


                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Message: "+ex.Message);
                _writer.Close();
                _stream.Close();

            }
           



            _writer.Dispose();
            _writer.Close();
            _stream.Close();    


        }
        public void DeleteAmovie()
        {
            List<string> Datalist = new List<string>();
            try
            {
                _stream = new FileStream(@"D:\aspdotnet\CSharpOPPsRepo\OnlineCinemaTicketBookingSystemModules\Movies.txt", FileMode.Open, FileAccess.Read);
                _reader = new StreamReader(_stream);
                //string[] lines = _reader.ReadToEnd().Split('\n');
                
                while(_reader.Peek() > 0)
                {
                    string line = _reader.ReadLine();
                    Datalist.Add(line); 

                }
                foreach(string line in Datalist)
                {
                    Console.WriteLine(line);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Message: " + ex.Message);

                _reader.Close();
                _stream.Close();

            }
            finally
            {
                _reader.Dispose();
                _reader.Close();
                _stream.Close();
            }
            Console.WriteLine("Enter the Movie Id To Delete :");

            int DeleteId = Convert.ToInt32(Console.ReadLine());
            try
            {
                _stream = new FileStream(@"D:\aspdotnet\CSharpOPPsRepo\OnlineCinemaTicketBookingSystemModules\Movies.txt", FileMode.Create, FileAccess.Write);
                _writer = new StreamWriter(_stream);
                
                for(int i = 0; i < Datalist.Count; i++)
                {
                    if (Datalist[i]!= "")
                    {
                        string[] mydata = Datalist[i].Split(',');
                        if (Convert.ToInt32(mydata[0]) != DeleteId)
                        {
                            _writer.Write(Datalist[i] + "\n");

                            

                         }
                        else
                        {
                            Console.WriteLine("Movie Deleted Sucessfully...");
                           // _writer.Write("");
                            //continue;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Message :" + ex.Message);
            }
            finally 
            {
                _writer.Dispose();
                _writer.Close();
                _stream.Close();
            }
            


        }
       
        public void PerFormAction()
        {
        upper:
            menu();
            int choise=Convert.ToInt32(Console.ReadLine()); ;
            switch (choise)
            {
                case 1:
                    TodaysBookedTicket();
                    goto upper;
                case 2:
                    ShowDateWise();
                    goto upper;
                case 3:
                    HistoryBookedTicket();
                    goto upper;
                case 4:
                    AddAMovie();
                    goto upper;
                case 5:
                    DeleteAmovie();
                    goto upper;
                case 6:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Wrong Choise (: Try Again");
                    goto upper;


            }
        }


    }
}
