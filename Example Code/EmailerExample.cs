using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Net.Mail;
using System.Net;
using System.IO;


namespace ConsoleApplication1
{
    class Program
    {

        static bool sending = false;
        static int errorCount = 0;
        static int msgCount = 0;
        static bool isConnected = true;
        static List<string> emailAddresses = new List<string>();
        static string emailTxt = "";
        static int emailWait = 50;
        static int emailMin = 10;
        static int emailMax = 40;
        static MailMessage mail = new MailMessage();
        static SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
        static Thread sendThread = new Thread(new ThreadStart(sendFunc));
        static void Main(string[] args)
        {
            /*Ping p = new Ping();
            PingReply r = p.Send("64.233.160.212");
            IPStatus s = r.Status;
            if (s == IPStatus.DestinationHostUnreachable)
            {
                isConnected = false;
            }
            else
            {
                isConnected = true;
            }
            //getQuote();*/
            sendThread.Start();
            while (true)
            {
                if (isConnected == false)
                {
                    Console.WriteLine("You are not connected to the internet. \n Would you like to continue anyways?");
                    if (!(Console.ReadLine().ToLower()[0] == 'y'))  return; 
                }
                Console.WriteLine("What would you like to send?");
                emailTxt = Console.ReadLine();
                Console.WriteLine("To what email address? (for multiple seperate each address with a comma and no space.)");
                emailAddresses = Console.ReadLine().Split(',').ToList<string>();
                Console.WriteLine("How often(in MS)?");
                string waitStr = Console.ReadLine();
                if (waitStr != "random")
                {
                    emailWait = Convert.ToInt32(waitStr);
                }
                else
                {
                    Console.WriteLine("Minimum number of minutes?");
                    emailMin = Convert.ToInt32(Console.ReadLine()) * 2;
                    Console.WriteLine("Maximum number of minutes?");
                    emailMax = Convert.ToInt32(Console.ReadLine()) * 2;
                    emailWait = -1;
                }


                mail.From = new MailAddress("1573025s11@gmail.com");
                //mail.To.Add(emailAddress);
                mail.Body = emailTxt;
                SmtpServer.Host = "smtp.gmail.com";
                if(isConnected == false){
                    SmtpServer.Host = "localhost";
                }
                SmtpServer.Port = 587;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("1573025s11@gmail.com", "testdepass5");

                SmtpServer.EnableSsl = true;
                Console.WriteLine("Press any key to exit\n----------------------------\n");
                sending = true;
                Console.ReadKey();
                Console.WriteLine();
                sending = false;
                Console.WriteLine("You have stopped");
                Console.WriteLine("You sent {0} messages", msgCount);
                Console.WriteLine("\n----------------------------\nDo you wish to send another message?");
                if (!(Console.ReadLine().ToLower()[0] == 'y'))
                {
                    break;
                }
                else
                {
                    
                }
            }
            sendThread.Join();
            
        }

        static void sendFunc()
        {
            bool randomQuote = false;
            while (true)
            {
                int wait = emailWait;
                if (sending)
                {
                    string subject = "message";
                    if (mail.Body == "random quote" || mail.Body == "random quotes")
                    {
                        randomQuote = true;
                        subject = mail.Body;
                    }
                    msgCount = 0;
                    while (true && sending)
                    {
                        msgCount += 1;
                        if (randomQuote)
                        {
                            mail.Body = getQuote();
                        }
                        foreach (string address in emailAddresses)
                        {
                            mail.To.Clear();
                            try
                            {
                                mail.To.Add(address);
                            }
                            catch { }
                            Console.WriteLine("[{0}]  sending {1} to {2}", DateTime.Now.ToString("h:mm:ss tt"), subject, address);
                            SmtpServer.Send(mail);
                        }

                        if (emailWait == -1)
                        {
                            Random r = new Random();
                            wait = r.Next(emailMin, emailMax) * 30 * 1000;
                        }
                        double milliseconds = wait;
                        double seconds = wait / 1000;
                        double minutes = seconds / 60;
                        double  hours = minutes / 60;
                        milliseconds = milliseconds - Math.Floor(seconds) * 1000;
                        seconds = seconds - Math.Floor(minutes) * 60;
                        minutes = minutes - Math.Floor(hours) * 60;
                        if (sending) { Console.WriteLine("[{4}]  {0}:{1}:{2}.{3} until next message is sent", Math.Floor(hours), Math.Floor(minutes), Math.Floor(seconds), milliseconds, DateTime.Now.ToString("h:mm:ss tt")); }
                        Thread.Sleep(wait);
                    }
                    
                }
                if (emailWait == -1)
                {
                    Random r = new Random();
                    wait = r.Next(emailMin, emailMax) * 30 * 1000;
                    //Console.WriteLine("Next wait period is {0}MS", wait);
                }
                Thread.Sleep(wait);
            }
        }
        private static string GetWebText(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.UserAgent = "A .NET Web Crawler";
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string htmlText = reader.ReadToEnd();
            return htmlText;
        }
        private static string getQuote(){
            try
            {
                string html = GetWebText("http://www.quotationspage.com/random.php3");
                int indexOfQuote = html.IndexOf("<dt class=\"quote\"");
                int indexOfEnd = html.IndexOf("</dt");
                string quoteHtml = html.Substring(indexOfQuote);
                quoteHtml = quoteHtml.Substring(0, indexOfEnd - indexOfQuote);
                indexOfQuote = quoteHtml.IndexOf(">");
                quoteHtml = quoteHtml.Substring(indexOfQuote + 1);
                indexOfQuote = quoteHtml.IndexOf(">");
                quoteHtml = quoteHtml.Substring(indexOfQuote + 1);
                quoteHtml = quoteHtml.Substring(0, quoteHtml.IndexOf("<") - 1);

                int indexOfAuthor = html.IndexOf("<dd class=\"author\"");
                string authorHtml = html.Substring(indexOfAuthor);
                authorHtml = authorHtml.Substring(authorHtml.IndexOf("<b>"));
                indexOfEnd = authorHtml.IndexOf("</b");
                authorHtml = authorHtml.Substring(0, indexOfEnd);
                indexOfAuthor = authorHtml.IndexOf("<a");
                authorHtml = authorHtml.Substring(indexOfAuthor);
                indexOfAuthor = authorHtml.IndexOf(">");
                authorHtml = authorHtml.Substring(indexOfAuthor + 1);
                int location = authorHtml.IndexOf("</a");
                authorHtml = authorHtml.Substring(0, location);
                string quote = string.Format("{0}, {1}", quoteHtml, authorHtml);
                return (quote);
            }
            catch
            {
                errorCount += 1;
                string html = GetWebText(string.Format("http://www.brainyquote.com/quotes/keywords/random_{0}.html", errorCount % 10));
                int indexOfQuote = html.IndexOf("title=\"view quote\"");
                int indexOfEnd = html.IndexOf("</dt");
                string quoteHtml = html.Substring(indexOfQuote);

                quoteHtml = quoteHtml.Substring(quoteHtml.IndexOf("')\">") + 4);
                html = quoteHtml;
                quoteHtml = quoteHtml.Substring(0, quoteHtml.IndexOf("</a>"));
                quoteHtml.Replace("&#39;", "'");

                int indexOfAuthor = html.IndexOf("')\">") + 4;
                string authorHtml = html.Substring(indexOfAuthor);
                authorHtml = authorHtml.Substring(0, authorHtml.IndexOf("</a>"));
                string quote = string.Format("\"{0}\", {1}", quoteHtml, authorHtml);
                return (quote);
            }
        }
    }
}
