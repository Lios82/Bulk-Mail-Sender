using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Mail;

namespace Mail_Sender
{
    public partial class Form2 : Form
    {
        private string mailId, password, smtp, filePath, subject,message,atch;

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string fileName;
                fileName = dlg.FileName;
                atch = fileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string fileName;
                fileName = dlg.FileName;
                textBox2.Text = fileName;
                filePath = fileName;
            }

        }

        public Form2(string id, string smtp, string pwd)
        {
            InitializeComponent();
            mailId = id+"@"+smtp+".com";
            this.smtp = "smtp."+smtp+".com";
            password = pwd;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            subject = textBox1.Text;
            message = richTextBox1.Text;
            var fileStream = new FileStream(@filePath, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                foreach (string line in File.ReadLines(@filePath, Encoding.UTF8))
                {
                    SendMail(line);
                }
            }
            MessageBox.Show("Sent");
        }

        private void SendMail(string toMailAddress)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(smtp);

                mail.From = new MailAddress(mailId);
                mail.To.Add(toMailAddress);// To
                mail.Subject = subject;// Subject
                mail.Body = message;// Message Body

                //Attachment
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(atch);
                mail.Attachments.Add(attachment);


                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(mailId, password);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Send failed");
            }
        }

    }
}
