using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E38EDM.TTD
{
    public partial class Mailform : Form
    {
        public delegate void SendProjectsavemark(List<string> liststring);
        public delegate void SendAttach(string attach);

        public SendProjectsavemark Senderprojectsavemark;
        public SendAttach Senderattach;
        List<string> listsavemark = new List<string>();
        string getattach = string.Empty;
        public Mailform()
        {
            InitializeComponent();
            Senderprojectsavemark = new SendProjectsavemark(Getlistsavemark);
            Senderattach = new SendAttach(Getattach);
        }
        private void Getlistsavemark(List<string> listItemsavemark)
        {
            listsavemark = listItemsavemark;
        }
        private void Getattach(string attach)
        {
            getattach = attach;
        }
        private List<string>Getlistdescription(List<string> listsavemark)
        {
            List<string> listdescription = new List<string>();
            for (int i = 0; i < listsavemark.Count; i++)
            {
                string file = listsavemark[i].Substring(listsavemark[i].LastIndexOf(@"\") + 1);
                string path = listsavemark[i].Substring(0, listsavemark[i].LastIndexOf(@"\"));
                string project = path.Substring(path.LastIndexOf(@"\") + 1);

                List<string> filePaths = Directory.GetFiles(path).ToList();
                foreach (var item in filePaths)
                {
                    string filename = item.Substring(item.LastIndexOf(@"\") + 1);
                    if (filename == file + "&description.txt")
                    {
                        listdescription.Add(path+@"\"+filename);
                    }
                }
            }
            return listdescription;
        }
        private void btn_send_Click(object sender, EventArgs e)
        {
            MailMessage mail = new MailMessage();
            if (tb_username.Text==string.Empty || tb_password.Text==string.Empty || tb_subject.Text==string.Empty || tb_sendto.Text==string.Empty)
            {
                MessageBox.Show("Please fill your Infomation ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                mail.From = new MailAddress(tb_username.Text);
                mail.To.Add(tb_sendto.Text);
                mail.Subject = tb_subject.Text;
                mail.Body = tb_content.Text.Replace(Environment.NewLine,"<br/>");
                mail.IsBodyHtml = true;
               
                List<string> listcc = GetlistCc(tb_cc.Text);
                List<string> listbcc = GetlistCc(tb_bcc.Text);

                foreach (var item in listcc)
                {
                    mail.CC.Add(item);
                }
                foreach (var item in listbcc)
                {
                    mail.Bcc.Add(item);
                }
                foreach (var item in listsavemark)
                {
                    string fileName = item + ".txt";
                    mail.Attachments.Add(new Attachment(fileName));
                }
                if (lb_attach.Text=="Attach Description")
                {
                    List<string> listdescription = Getlistdescription(listsavemark);
                    foreach (var item in listdescription)
                    {
                        string fileName = item;
                        mail.Attachments.Add(new Attachment(fileName));
                    }
                }
                
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new System.Net.NetworkCredential(tb_username.Text, tb_password.Text);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
                MessageBox.Show("Your Email Send", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private List<string>GetlistCc(string Cc)
        {
            List<string> listcc = new List<string>();
            if (Cc==string.Empty)
            {

            }
            else
            {
                if (Cc.Contains(","))
                {
                    listcc = Cc.Split(',').ToList();
                }
                else
                    listcc.Add(Cc);
            }
            return listcc;
        }
        private void tbn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        string infotext = @"C:\\E38EDM\Infomation\infomail.txt";
        private void Mailform_Load(object sender, EventArgs e)
        {
            lb_attach.Text = getattach;
            if (File.Exists(infotext))
            {
                RememberValue();
            }
            else
            {
                FileStream fs=File.Create(infotext);
                fs.Close();
                RememberValue();
            }
        }
        private void RememberValue()
        {
            List<string> listremember = File.ReadAllLines(infotext).ToList();
            if (listremember.Count > 0)
            {
                tb_username.Text = listremember[0];
                tb_password.Text = listremember[1];
                chb_remember.Checked = true;
                tb_password.PasswordChar = '*';
            }
            else
                chb_remember.Checked = false;
        }

        private void chb_remember_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_remember.Checked)
            {
                File.WriteAllText(infotext, string.Empty);
                StreamWriter swmetricafter = new StreamWriter(infotext, true);
                swmetricafter.WriteLine(tb_username.Text);
                swmetricafter.WriteLine(tb_password.Text);
                swmetricafter.Close();
            }
            else
            {
                File.WriteAllText(infotext, string.Empty);
            }
        }
    }
}
