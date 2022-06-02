using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

using Autodesk.Revit.DB;

namespace E38EDM.CEGVN
{
    public partial class ShowAllParameters : System.Windows.Forms.Form
    {
        Document doc;
        Element el;
        public ShowAllParameters()
        {
            InitializeComponent();
        }
        private BitmapSource ConvertBitmapToBitmapSource(Bitmap bmp)
        {
            BitmapSource bms = null;
            try
            {
                bms= System.Windows.Interop.Imaging
             .CreateBitmapSourceFromHBitmap(
               bmp.GetHbitmap(),
               IntPtr.Zero,
               System.Windows.Int32Rect.Empty,
               BitmapSizeOptions.FromEmptyOptions());
            }
            catch { }
            return bms;
        }
        private void AddtoPicturebox(Element el)
        {
            ElementType type = doc.GetElement(el.GetTypeId()) as ElementType;

            System.Drawing.Size imgSize = new System.Drawing.Size(200, 200);

            Bitmap image = type.GetPreviewImage(imgSize);

            // encode image to jpeg for test display purposes:

            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            try
            {
                encoder.Frames.Add(BitmapFrame.Create(ConvertBitmapToBitmapSource(image)));

                encoder.QualityLevel = 25;

                string filename = @"C:\E38EDM\Images\" + el.Id.ToString() + ".jpg";

                FileStream file = new FileStream(filename, FileMode.Create, FileAccess.Write);
                encoder.Save(file);
                file.Close();
                Image img = Image.FromFile(filename);
                picbox_display.Image = img;
            }

            catch { }
        }
        public ShowAllParameters(string[] information,string [] informationtype, Document doc, Element el)
           : this()
        {
            this.doc = doc;
            this.el = el;

            // we need to add each string in to each row of the list view, and split the string
            // into substrings delimited by '\t' then put them into the columns of the row.

            // create three columns with "Name", "Type" and "Value"
            propertyListView.Columns.Add("Name");
            propertyListView.Columns.Add("Type");
            propertyListView.Columns.Add("Value");

            propertyListViewtype.Columns.Add("Name");
            propertyListViewtype.Columns.Add("Type");
            propertyListViewtype.Columns.Add("Value");

            // loop all the strings, split them, and add them to rows of the list view
            foreach (string row in information)
            {
                if (row == null) continue;
                ListViewItem lvi = new ListViewItem(row.Split('\t'));
                propertyListView.Items.Add(lvi);
            }

            foreach (string row in informationtype)
            {
                if (row == null) continue;
                ListViewItem lvi = new ListViewItem(row.Split('\t'));
                propertyListViewtype.Items.Add(lvi);
            }

            // The following code is used to sort and resize the columns within the list view 
            // so that the data can be viewed better.

            // sort the items in the list view ordered by ascending.
            propertyListView.Sorting = SortOrder.Ascending;
            propertyListViewtype.Sorting = SortOrder.Ascending;

            // make the column width fit the content
            propertyListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            propertyListViewtype.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            // increase the width of columns by 40, make them a litter wider
            int span = 40;
            foreach (ColumnHeader ch in propertyListView.Columns)
            {
                ch.Width += span;
            }
            foreach (ColumnHeader ch in propertyListViewtype.Columns)
            {
                ch.Width += span;
            }

            // the last column fit the rest of the list view
            propertyListView.Columns[propertyListView.Columns.Count - 1].Width = -2;
            propertyListViewtype.Columns[propertyListViewtype.Columns.Count - 1].Width = -2;

            AddtoPicturebox(el);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DeleteImage()
        {
            string path = @"C:\E38EDM\Images";
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] Files = d.GetFiles("*.jpg");
            foreach (FileInfo item in Files)
            {
                File.Delete(item.FullName);
            }
        }

        private void ShowAllParameters_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
