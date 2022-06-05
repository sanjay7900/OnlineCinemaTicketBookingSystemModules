using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;



namespace OnlineCinemaTicketBookingSystemModules
{
    public class Customers
    {
        private int flag=0;
        private FileStream _file;
        private StreamReader _reader;
        private StreamWriter _writer;
        private FileStream bookmoviefile;
        private StreamWriter bookWriter;
        public static int id;
        private string[] data=new string[5];
        private int genrateTicketId;
        

        private void BookingTicket()
        {
            
            data = CustomerDetails();
            findAgain:
            Console.WriteLine("Please Enter the name Which Movie's Ticket You want to Book");
            string findmovie;
            findmovie =Console.ReadLine();  
            if (findmovie !="")
            {
                MovieListAvailable(findmovie);
                Console.WriteLine("Please Choose A Movie Id What you Want Book Ticket");
                int movieId;
                movieId =Convert.ToInt32(Console.ReadLine());
            numberOfTicketAgain:
                Console.WriteLine(" Enter the Number Of Ticket ");
                int numberOfTicket = Convert.ToInt32(Console.ReadLine());
                if (numberOfTicket > 0)
                {
                    
                }
                else
                {
                    Console.WriteLine("please enter Vailid number");
                    goto numberOfTicketAgain;
                }
                if (BookOrNot(movieId,numberOfTicket))
                {
                    DecrementBy1Seats(movieId, numberOfTicket);
                    Console.WriteLine("         Your Ticket Booked SuccessFully        ");
                    Console.WriteLine("**************Your Ticket Id *******************");
                    Console.WriteLine("*          Track your Ticket with This Id      *");
                    Console.WriteLine("*    Please Note This Number for  Future Need  *");
                    Console.WriteLine("*          Or Take ScreenShot                  *");
                    Console.WriteLine("*                                              *");
                    Console.WriteLine("* NUmber ->"+    genrateTicketId             +"*");
                    Console.WriteLine("*                                              *");
                    Console.WriteLine("*                                              *");
                    Console.WriteLine("*                                              *");
                    Console.WriteLine("************************************************");




                }

            }
            else
            {
                Console.WriteLine(" Enter A movie name... Do you want to Again press 1");
                int press=Convert.ToInt32(Console.ReadLine());
                if (press == 1)
                {
                    goto findAgain;
                }
                else
                {
                    Console.WriteLine(" We could not Booked your ticket... try latter");
                }
               
            }
            



        }
        private string[] CustomerDetails()
        {
            string[] result=new string[6];
            try
            {
                _file = new FileStream(@"D:\aspdotnet\CSharpOPPsRepo\OnlineCinemaTicketBookingSystemModules\Auth.txt", FileMode.Open, FileAccess.Read);
                _reader = new StreamReader(_file);
                while(_reader.Peek() > 0)
                {
                    string lines= _reader.ReadLine();
                    if(lines != "")
                    {
                        string[] arr = lines.Split(',');
                        if(Convert.ToInt32(arr[0])==id)
                        {
                            result = arr;   

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message+"try later !!!.");
                _reader.Dispose();
                _reader.Close();
                _file.Close();
            }
            finally
            {
                _reader.Dispose();
                _reader.Close();
                _file.Close();

            }


            return result;
        }
        private void MovieListAvailable(string movieName)
        {
            Console.WriteLine("This movie is present on these dates... ");
            try
            {
                Console.WriteLine("Available Movies");
                _file=new FileStream(@"D:\aspdotnet\CSharpOPPsRepo\OnlineCinemaTicketBookingSystemModules\Movies.txt", FileMode.Open, FileAccess.Read);
                _reader=new StreamReader(_file);
                string date = DateTime.Now.ToString("dd/MM/yyyy");
                DateTime dateCampare=DateTime.Parse(date);
                while (_reader.Peek() > 0)
                {
                    string lines=_reader.ReadLine();
                    if (lines != "")
                    {
                        string[] movieList = lines.Split(',');
                        DateTime dateTimeCampareto=DateTime.Parse(movieList[4]);
                        if ((dateCampare.Equals(dateTimeCampareto)|| dateCampare<dateTimeCampareto)&&movieList[1]==movieName)
                        {
                            for(int i = 0; i < movieList.Length; i++)
                            {
                                Console.WriteLine(movieList[i]+"\t     ");
                            }

                        }
                    }
                }


                //string[] data = movieName.Split()

            }
            catch (Exception ex)
            {
                Console.WriteLine("Something  gone Wrong (:"+ex.Message);
                _reader.Dispose();

                _reader.Close();
                _file.Close();

            }
            finally
            {
                _reader.Dispose();
                _reader.Close();
                _file.Close();

            }
        }
        private bool BookOrNot(int movieid,int ticket)
        {
            try
            {
                _file=new FileStream(@"D:\aspdotnet\CSharpOPPsRepo\OnlineCinemaTicketBookingSystemModules\Movies.txt", FileMode.Open,FileAccess.Read);
                _reader=new StreamReader(_file);    

                while(_reader.Peek() > 0)
                {
                    string lines = _reader.ReadLine();
                    if(lines != "")
                    {
                        string[] movieFind = lines.Split(',');
                        if(movieid==Convert.ToInt32(movieFind[0])&&(Convert.ToInt32(movieFind[8])>0))
                        {
                            int totalPrice = Convert.ToInt32(movieFind[6]) * ticket;

                            
                            try
                            {
                                bookmoviefile = new FileStream(@"D:\aspdotnet\CSharpOPPsRepo\OnlineCinemaTicketBookingSystemModules\BookedTicket.txt", FileMode.Append,FileAccess.Write);
                                bookWriter = new StreamWriter(bookmoviefile);
                                for(int i = 0; i < movieFind.Length; i++)
                                {
                                    bookWriter.Write(movieFind[i]+",");
                                }
                                for(int i = 0; i < data.Length-1; i++)
                                {
                                    bookWriter.Write(data[i]+",");
                                }
                                string currendate=DateTime.Now.ToShortDateString();   
                                bookWriter.Write(currendate);
                                bookWriter.Write("," + totalPrice.ToString());
                                bookWriter.Write(","+ticket.ToString());
                                Random random = new Random();
                                genrateTicketId = random.Next();
                                bookWriter.Write(","+genrateTicketId);  
                                bookWriter.Write("\n");

                                bookWriter.Dispose();
                                bookWriter.Close();
                                bookmoviefile.Close();
                                _reader.Dispose();
                                _reader.Close();
                                _file.Close();
                               
                                flag = 1;
                                return true;


                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine("Something gone Wrong Could not book the youe Ticket Try Latter...");
                                Console.WriteLine(ex.Message);
                                bookWriter.Dispose();
                                bookWriter.Close();
                                bookmoviefile.Close();


                            }
                            finally
                            {
                                bookWriter.Dispose();
                                bookWriter.Close();
                                bookmoviefile.Close();

                            }
                        }
                        else
                        {
                            Console.WriteLine("You Entered Wrong Movie Id");
                            Console.WriteLine("Try Again...");
                            return false;
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(" something Wrong "+ex.Message);
                _reader.Dispose();
                _reader.Close();
                _file.Close();  

            }
            finally
            {
                _reader.Dispose();
                _reader.Close();
                _file.Close();

            }
            return false;

        }
        private void DecrementBy1Seats(int id,int ticket)
        {
            List<string> seats = new List<string>();
            try
            {
                _file = new FileStream(@"D:\aspdotnet\CSharpOPPsRepo\OnlineCinemaTicketBookingSystemModules\Movies.txt", FileMode.Open, FileAccess.Read);
                _reader = new StreamReader(_file);
                while (_reader.Peek() > 0)
                {
                    string seat = _reader.ReadLine();
                    if (seat != "")
                    {
                        seat = seat.Trim();
                        seats.Add(seat);
                    }
                }
                _reader.Dispose();
                _reader.Close();
                _file.Close();
                _file = new FileStream(@"D:\aspdotnet\CSharpOPPsRepo\OnlineCinemaTicketBookingSystemModules\Movies.txt", FileMode.Open, FileAccess.Write);
                _writer=new StreamWriter(_file);    
                for(int i = 0; i < seats.Count; i++)
                {
                    string[] checkdata=seats[i].Split(',');
                    if (Convert.ToInt32(checkdata[0]) == id)
                    {
                        int seatdcrement=Convert.ToInt32(checkdata[8]);
                        seatdcrement-=ticket;
                        _writer.Write(checkdata[0]+","+ checkdata[1] + "," + checkdata[2] + "," + checkdata[3] + "," + checkdata[4] + "," + checkdata[5] + "," + checkdata[6] + "," + checkdata[7] + ","+seatdcrement.ToString()+"," + checkdata[9] +"\n");
                    }
                    else
                    {
                        _writer.Write(seats[i].ToString());
                        _writer.Write("\n");
                    }
                }
                _writer.Dispose();
                _writer.Close();
                _file.Close();

            }
            catch (Exception ex)
            {
                _reader.Dispose();
                _reader.Close();
                _file.Close();
                
                Console.WriteLine("......."+ex.Message );

            }
            finally
            {
                _reader.Dispose();
                _reader.Close();
                _file.Close();
               

            }

        }
        private void MyTicketHistory()
        {
            try
            {
                _file=new FileStream(@"D:\aspdotnet\CSharpOPPsRepo\OnlineCinemaTicketBookingSystemModules\BookedTicket.txt", FileMode.Open,FileAccess.Read);
                _reader=new StreamReader(_file);
                while(_reader.Peek()>0)
                {
                    string lines = _reader.ReadLine();
                    if (lines!="")
                    {
                        string[] tokensTicket = lines.Split(',');
                        if (Convert.ToInt32(tokensTicket[10]) == id)
                        {
                            for(int i = 0; i < tokensTicket.Length; i++)
                            {
                                Console.Write(tokensTicket[i]+"  ");
                            }
                            Console.WriteLine();    
                        }


                    }


                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                _reader.Dispose();
                _reader.Close();
                _file.Close();  

            }
            finally
            {
                _reader.Dispose();
                _reader.Close();
                _file.Close();

            }
        }
        private void MyTicketWithTicketId(int ticketid)
        {
            try
            {
                _file = new FileStream(@"D:\aspdotnet\CSharpOPPsRepo\OnlineCinemaTicketBookingSystemModules\BookedTicket.txt", FileMode.Open, FileAccess.Read);
                _reader = new StreamReader(_file);
                while (_reader.Peek() > 0)
                {
                    string lines = _reader.ReadLine();
                    if (lines != "")
                    {
                        string[] tokensTicket = lines.Split(',');
                        if (Convert.ToInt32(tokensTicket[10]) == id && Convert.ToInt32(tokensTicket[tokensTicket.Length-1])==ticketid)
                        {
                            for (int i = 0; i < tokensTicket.Length; i++)
                            {
                                Console.Write(tokensTicket[i] + "  ");
                            }
                            Console.WriteLine();
                        }


                    }


                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _reader.Dispose();
                _reader.Close();
                _file.Close();

            }
            finally
            {
                _reader.Dispose();
                _reader.Close();
                _file.Close();

            }
        }
        public void PerformAllActionOfCustomer()
        {
            Mainmenu:
            CustomerMenu();
            Console.Write(" Enter Your Choise   :  ");
            int choise = Convert.ToInt32(Console.ReadLine());
            switch (choise)
            {
                case 1:
                    onemore:
                    BookingTicket();
                    Console.WriteLine(" Do You Want More Press 1 Else Other");
                    int more=Convert.ToInt32(Console.ReadLine());   
                    if(more == 1)
                    {
                        goto onemore;
                    }
                    goto Mainmenu;
                case 2:
                    MyTicketHistory();  
                    goto Mainmenu;
                case 3:
                    Console.Write("Enter the Ticket Id : ");
                    int tid=Convert.ToInt32(Console.ReadLine()); 
                    if(tid > 1)
                    {
                        MyTicketWithTicketId(tid);

                    }
                    else
                    {
                        Console.WriteLine("You Entered SomeThing Wrong (:");
                    }
                   
                    goto Mainmenu;
                case 4:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("You Entered Wrong Choise (:");
                    goto Mainmenu;  



            }


        }
        private void CustomerMenu()
        {
            Console.WriteLine("Press 1 : For the Book a Ticket");
            Console.WriteLine("Press 2 : For the Show Ticket History");
            Console.WriteLine("Press 3 : For the Your Ticket Status By Ticket Id ");
            Console.WriteLine("Press 4 : Exit... ");
        }


    }
}
