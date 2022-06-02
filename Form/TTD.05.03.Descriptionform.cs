using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E38EDM.TTD
{
    public partial class Descriptionform : Form
    {
        public delegate void SendProjectsavemark(List<string> liststring);
        public SendProjectsavemark Senderprojectsavemark;
        List<string> listsavemark= new List<string>();
        UIApplication uiapp;
        public Descriptionform(UIApplication uiapp)
        {
            this.uiapp = uiapp;
            InitializeComponent();
            Senderprojectsavemark = new SendProjectsavemark(Getlistsavemark);
        }

        private void Descriptionform_Load(object sender, EventArgs e)
        {
            string userId = uiapp.Application.Username;
            for (int i = 0; i < listsavemark.Count; i++)
            {
                string file = listsavemark[i].Substring(listsavemark[i].LastIndexOf(@"\") + 1);
                string path = listsavemark[i].Substring(0, listsavemark[i].LastIndexOf(@"\"));
                string project= path.Substring(path.LastIndexOf(@"\") + 1);
                tb_project.Text = project;

                List<string> filePaths = Directory.GetFiles(path).ToList();
                List<string> listuser = Getlistuser(project);
                foreach (var item in filePaths)
                {
                    string filename = item.Substring(item.LastIndexOf(@"\") + 1);
                    if (filename == file + "&description.txt")
                    {
                        string datestring = DateTime.Now.ToString("hh:mm:ss tt", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                        List<string> datadescription = File.ReadAllLines(item).ToList();
                        for (int j = 0; j < datadescription.Count; j++)
                        {
                            tb_description.AppendText(datadescription[j] + "\r\n");
                        }
                    }
                }
            }
        }
        private void Getlistsavemark(List<string> listItemsavemark)
        {
            listsavemark = listItemsavemark;
        }
        private string GetfolderDriveStream()
        {
            string[] drives = Directory.GetLogicalDrives();
            List<string> listlast = new List<string>();

            foreach (string drive in drives)
            {
                try
                {
                    List<string> listfolder = Directory.GetDirectories(drive).ToList();
                    if (listfolder == null)
                    {
                        continue;
                    }
                    foreach (var foldershare in listfolder)
                    {
                        if (foldershare == drive + @"Shared drives")
                        {

                            List<string> listfoldershare = Directory.GetDirectories(foldershare).ToList();
                            foreach (var smallfolder in listfoldershare)
                            {
                                if (smallfolder == foldershare + @"\02.Collabo")
                                {

                                    listlast.Add(drive);
                                }
                            }
                        }
                    }
                }
                catch { }
            }
            return listlast.First();
        }
        private List<string> Getlistuser(string project)
        {
            string googlestream = GetfolderDriveStream();
            List<string> listuser = new List<string>();
            string nguoncentral = googlestream + @"Shared drives\BIM Project\Project Save\";
            List<string> filePaths = Directory.GetDirectories(nguoncentral+project).ToList();
            foreach (var path in filePaths)
            {
                string nameuser=path.Substring(path.LastIndexOf(@"\") + 1);
                listuser.Add(nameuser);
            }
            return listuser;
        }
        private void tb_description_TextChanged(object sender, EventArgs e)
        {

        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            string userId = uiapp.Application.Username;
            string datestring = DateTime.Now.ToString("dd / MM / yy, hh: mm tt", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            for (int i = 0; i < listsavemark.Count; i++)
            {
                string file = listsavemark[i].Substring(listsavemark[i].LastIndexOf(@"\") + 1);
                string path = listsavemark[i].Substring(0, listsavemark[i].LastIndexOf(@"\"));
                string project = path.Substring(path.LastIndexOf(@"\") + 1);
                List<string> listuser = Getlistuser(project);
                File.WriteAllText(listsavemark[i] + @"&description.txt", String.Empty);
                string[] lines = tb_description.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                for (int j = 0; j < lines.Count(); j++)
                {
                    if (lines[j] == string.Empty)
                    {
                        //lines[j] = "Last Edited";
                    }
                    else if (lines[j].Contains(":") == false)
                    {
                        lines[j]=(userId + ": " + lines[j] + " (" + datestring + ")");
                    }
                }
                File.AppendAllLines(listsavemark[i] + @"&description.txt", lines);
                tb_description.Clear();
                List<string> filePaths = Directory.GetFiles(path).ToList();
                foreach (var item in filePaths)
                {
                    string filename = item.Substring(item.LastIndexOf(@"\") + 1);
                    if (filename == file + "&description.txt")
                    {
                        List<string> datadescription = File.ReadAllLines(item).ToList();
                        for (int j = 0; j < datadescription.Count; j++)
                        {
                            if (datadescription[j]!=string.Empty)
                            {
                                tb_description.AppendText(datadescription[j] + "\r\n");
                            }
                        }
                    }
                }
            }
            MessageBox.Show("Add Description Successfull ", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
