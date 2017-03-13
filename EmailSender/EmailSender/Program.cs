using System;
using System.IO;
using System.Net.Mail;

class Program {
    static void Main() {
        var count = 0;
        var error_count = 0;

        var emailBody = System.IO.File.ReadAllText(string.Format("{0}\\{1}", System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "\\Templates\\EmailTemplate.html"));

        using (var client = new SmtpClient(EmailSender.Properties.Settings.Default.SmtpServer)) {
            client.UseDefaultCredentials = false;

            string email = string.Empty;

            //emails
            using (var emails = new StreamReader("Resources\\emails_prod.txt")) {
                while (emails.Peek() >= 0) {
                    count += 1;
                    email = emails.ReadLine();

                    //empty line #
                    if (string.IsNullOrEmpty(email)) {
                        count -= 1;
                        continue;
                    }

                    try {
                        var mm = new MailMessage(new MailAddress("no-reply@yourdns.com", "YOUR COMPANY NAME"), new MailAddress(email));
                        mm.Subject = "YOUR COMPANY NAME - Good News";
                        mm.IsBodyHtml = true;
                        mm.Body = emailBody;

                        Console.WriteLine(string.Format("{0}. {1}", count, email));
                        Console.WriteLine("Sending...");
                        client.Send(mm);
                        Console.WriteLine("Sending Complete!");
                    } catch (Exception e) {
                        error_count += 1;

                        var error = new MailMessage("no-reply@yourdns.com", "to_email@yourdns.com", string.Format("Error sending email to: {0}, Line #{1}", email, count), e.Message);
                        client.Send(error);
                    }
                }
            }

            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine(string.Format("Total sent: {0} | error: {1}.", (count - error_count), error_count));
            Console.WriteLine("");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("---------------------------------------------");
            Console.ReadLine();
        }
    }
}
