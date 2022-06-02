using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using DocumentFormat.OpenXml.Drawing;
using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using Microsoft.CSharp.RuntimeBinder;


namespace E38EDM.TTD
{

    public partial class FrmListElements : System.Windows.Forms.Form
    {
        UIApplication uiapp;
        Document doc;
        string family = string.Empty;
        string type = string.Empty;
        string level = string.Empty;
        string assemblycode = string.Empty;
        string assemblydescription = string.Empty;
        string keynote = string.Empty;
        string description = string.Empty;

        public FrmListElements(UIApplication uiapp, Document doc)
        {
            this.doc = doc;
            this.uiapp = uiapp;
            InitializeComponent();
        }

        public static class InternetConnection
        {
            [DllImport("wininet.dll")]
            private extern static bool InternetGetConnectedState(out int description, int reservedValuine);
            public static bool IsConnectedToInternet()
            {
                int desc;
                return InternetGetConnectedState(out desc, 0);
            }
        }


        private static string GetfolderDriveStream()
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
                                if (smallfolder == foldershare + @"\BIM Project")
                                {
                                    listlast.Add(drive);
                                }
                            }
                        }
                    }
                }
                catch { }
            }
            if (listlast.Count == 0)
            {
                MessageBox.Show("Can't connect to Google drive of TTD", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                listlast.Add("");
            }
            return listlast.First();
        }
        private static string googlestream = GetfolderDriveStream();
        string nguon = @"C:\\E38EDM\ListElements\";
        string nguoncentral = googlestream + @"Shared drives\BIM Project\Project Save\";
        string colourdata = @"C:\\E38EDM\Update\colourdata.txt";
        private List<Element> GetlistElement(Document doc, BuiltInCategory builtin)
        {
            FilteredElementCollector fi = new FilteredElementCollector(doc);
            List<Element> listele = fi.OfCategory(builtin).WhereElementIsNotElementType().ToElements().ToList();
            return listele;
        }
        private void HeaderOptions(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.Khaki;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 35;
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
        List<Element> AllElement(Document doc)
        {
            List<Element> elements = new List<Element>();

            FilteredElementCollector collector
              = new FilteredElementCollector(doc)
                .WhereElementIsNotElementType();

            foreach (Element e in collector)
            {
                if (null != e.Category
                  && e.Category.HasMaterialQuantities)
                {
                    elements.Add(e);
                }
            }
            return elements;
        }
        private void frmListElements_Load(object sender, EventArgs e)
        {
            DeleteImage();
            if (googlestream == "")
            {
                lb_connect.Text = "Disconnect";
                rbn_connect.BackColor = System.Drawing.Color.Red;
                //MessageBox.Show("Can't connect to Google drive of TTD", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                File.WriteAllText(@"C:\\E38EDM\ListElements\mode.txt", String.Empty);
                lb_connect.Text = "Connected";
                rbn_connect.BackColor = System.Drawing.Color.LightGreen;
                File.WriteAllText(colourdata, String.Empty);
                lv_savemark.Items.Clear();
                lv_savemark.View = System.Windows.Forms.View.Details;
                cb_projectcollaborate.Items.Clear();

                List<string> listname = Directory.GetFiles(nguon).ToList();
                Removelistname(listname);
                foreach (string name in listname)
                {
                    ListViewItem item = lv_savemark.Items.Add(GetNameFromFullName(GetNameFromPath(name)));
                    item.SubItems.Add(GetNameFromFullName(GetNameFromPath(name)));
                }

                cb_family.Text = "None";
                cb_code1tocode6.Text = "None";
                string updatedata = @"C:\\E38EDM\Update\updatedata.txt";
                File.WriteAllText(updatedata, String.Empty);
                List<Category> listcatelink = new List<Category>();
                List<Category> listcategory = new List<Category>();

                HeaderOptions(grv_category);
                HeaderOptions(grv_element);
                List<Category> listcategorydoc = GetlistCategoryinViewAllversion(doc);
                foreach (var item in listcategorydoc)
                {
                    listcategory.Add(item);
                }
                foreach (var item in listcatelink)
                {
                    listcategory.Add(item);
                }

                for (int i = 0; i < listcategory.Count; i++)
                {
                    for (int j = listcategory.Count - 1; j > i; j--)
                    {
                        if (listcategory[j].Name == listcategory[i].Name)
                        {
                            listcategory.RemoveAt(j);
                        }
                    }
                }
                grv_category.DataSource = GetnameCategory(listcategory);
                //Add list to combobox:


                //get all levels in doc.
                List<Element> listele = new FilteredElementCollector(doc).OfClass(typeof(Level)).WhereElementIsNotElementType().ToElements().ToList();


                List<Level> listlevel = new List<Level>();
                foreach (Element item in listele)
                {
                    if (item != null)
                    {
                        Level lv = item as Level;
                        listlevel.Add(lv);
                    }
                }
                listlevel = listlevel.OrderBy(x => x.Elevation).ToList();
                foreach (Level level in listlevel)
                {
                    if (level != null)
                        cb_level.Items.Add(level.Name);
                }
                cb_level.DisplayMember = "Name";

                string folderorigin = googlestream + @"Shared drives\BIM Project\Project Save\";
                List<string> directories = Directory.GetDirectories(folderorigin).ToList();
                directories = directories.OrderBy(x => x.ToString()).ToList();
                foreach (var folder in directories)
                {
                    if (folder != null)
                    {
                        cb_project.Items.Add(folder.Substring(folder.LastIndexOf(@"\") + 1));
                        cb_projectcollaborate.Items.Add(folder.Substring(folder.LastIndexOf(@"\") + 1));
                    }
                }

                //Get Elements from model
                List<Element> listel = new List<Element>();
                List<ElementId> listId = uiapp.ActiveUIDocument.Selection.GetElementIds().ToList();
                foreach (var id in listId)
                {
                    Element el = doc.GetElement(id);
                    if (el != null)
                    {
                        listel.Add(el);
                    }
                }
                grv_element.DataSource = TransferAllElements(listel);
            }
        }
        private void Addvaluetocombobox()
        {
            cb_family.Items.Clear();
            cb_code1tocode6.Items.Clear();
            string rowvalue = grv_category.CurrentRow.Cells[0].Value.ToString();

            for (int j = 0; j < grv_element.ColumnCount; j++)
            {
                string header = grv_element.Columns[j].HeaderText;
                if (header == "Family" && grv_element.ColumnCount == 10)
                {
                    for (int i = 0; i < grv_element.Rows.Count; i++)
                    {
                        string data = (string)grv_element[2, i].Value;
                        if (data != null)
                        {
                            cb_family.Items.Add(data);
                            object[] distinctItems = (from object o in cb_family.Items select o).Distinct().ToArray();
                            cb_family.Items.Clear();
                            cb_family.Items.AddRange(distinctItems);
                        }
                    }
                }

                else if (header == "Code1ToCode6" && grv_element.ColumnCount == 10)
                {
                    for (int i = 0; i < grv_element.Rows.Count; i++)
                    {
                        string data = (string)grv_element[8, i].Value;
                        if (data != null)
                        {
                            cb_code1tocode6.Items.Add(data);
                            object[] distinctItems = (from object o in cb_code1tocode6.Items select o).Distinct().ToArray();
                            cb_code1tocode6.Items.Clear();
                            cb_code1tocode6.Items.AddRange(distinctItems);
                        }
                    }
                }
                else if (header == "Family" && grv_element.ColumnCount == 11)
                {
                    for (int i = 0; i < grv_element.Rows.Count; i++)
                    {
                        string data = (string)grv_element[3, i].Value;
                        if (data != null)
                        {
                            cb_family.Items.Add(data);
                            object[] distinctItems = (from object o in cb_family.Items select o).Distinct().ToArray();
                            cb_family.Items.Clear();
                            cb_family.Items.AddRange(distinctItems);
                        }
                    }
                }

                else if (header == "Code1ToCode6" && grv_element.ColumnCount == 11)
                {
                    for (int i = 0; i < grv_element.Rows.Count; i++)
                    {
                        string data = (string)grv_element[9, i].Value;
                        if (data != null)
                        {
                            cb_code1tocode6.Items.Add(data);
                            object[] distinctItems = (from object o in cb_code1tocode6.Items select o).Distinct().ToArray();
                            cb_code1tocode6.Items.Clear();
                            cb_code1tocode6.Items.AddRange(distinctItems);
                        }
                    }
                }
                else if (header == "Family" && grv_element.ColumnCount == 15)
                {
                    for (int i = 0; i < grv_element.Rows.Count; i++)
                    {
                        string data = (string)grv_element[7, i].Value;
                        if (data != null)
                        {
                            cb_family.Items.Add(data);
                            object[] distinctItems = (from object o in cb_family.Items select o).Distinct().ToArray();
                            cb_family.Items.Clear();
                            cb_family.Items.AddRange(distinctItems);
                        }
                    }
                }

                else if (header == "Code1ToCode6" && grv_element.ColumnCount == 15)
                {
                    for (int i = 0; i < grv_element.Rows.Count; i++)
                    {
                        string data = (string)grv_element[13, i].Value;
                        if (data != null)
                        {
                            cb_code1tocode6.Items.Add(data);
                            object[] distinctItems = (from object o in cb_code1tocode6.Items select o).Distinct().ToArray();
                            cb_code1tocode6.Items.Clear();
                            cb_code1tocode6.Items.AddRange(distinctItems);
                        }
                    }
                }
            }
            cb_family.DisplayMember = "Name";
            cb_code1tocode6.DisplayMember = "Name";
        }
        #region: TransferClass
        private string CheckParameter(Element ele, string para)
        {
            string result = string.Empty;
            if (ele.LookupParameter(para) != null)
            {
                result = ele.LookupParameter(para).AsValueString();
            }
            else
            {
                result = "N/A";
            }
            return result;
        }
        private string CheckDescription(Element ele)
        {
            string para = string.Empty;
            string familyname = string.Empty;
            string typename = string.Empty;
            if (ele.Category.Name == "Floors" && ele is Floor)
            {

            }
            else
            {
                Element element = doc.GetElement(ele.GetTypeId());
                ElementType eletype = element as ElementType;
                if (eletype.CanBeCopied == false)
                {
                    para = element.LookupParameter("Description").AsString();
                }
            }
            return para;
        }
        private void GetFamilyandType(string familyname, out string family, out string type)
        {
            family = string.Empty;
            type = string.Empty;
            try
            {
                int vt = familyname.IndexOf(':');
                family = familyname.Substring(0, vt);
                type = familyname.Substring(vt + 2);
            }
            catch { }
        }
        private void GetAssemblyandKeynote(Element ele, out string assemblycode, out string assemblydescription, out string keynote)
        {
            assemblycode = string.Empty;
            assemblydescription = string.Empty;
            keynote = string.Empty;
            if (ele.Category.Name == "Floors" && ele is Floor)
            {
                Floor flo = ele as Floor;
                assemblycode = flo.FloorType.LookupParameter("Assembly Code").AsString();
                assemblydescription = flo.FloorType.LookupParameter("Assembly Description").AsString();
                keynote = flo.FloorType.LookupParameter("Keynote").AsString();
            }
            else
            {
                Element element = doc.GetElement(ele.GetTypeId());
                assemblycode = element.LookupParameter("Assembly Code").AsString();
                assemblydescription = element.LookupParameter("Assembly Description").AsString();
                keynote = element.LookupParameter("Keynote").AsString();
            }
        }
        private string GetCode1toCode6(Element element)
        {
            string Code1ToCode6 = string.Empty;
            string family = string.Empty;
            string type = string.Empty;
            string assemblycode = string.Empty;
            string assemblydescription = string.Empty;
            string keynote = string.Empty;
            GetAssemblyandKeynote(element, out assemblycode, out assemblydescription, out keynote);
            string familyname = CheckParameter(element, "Family and Type");
            GetFamilyandType(familyname, out family, out type);
            string description = CheckDescription(element);
            if (description == string.Empty && type.Contains("_"))
            {
                Code1ToCode6 = assemblycode + "." + type.Substring(type.LastIndexOf('_') + 1);
            }

            else if (description != string.Empty && type.Contains("_"))
            {
                Code1ToCode6 = assemblycode + "." + description;
                if (assemblycode.EndsWith("."))
                {
                    assemblycode = string.Empty;
                }
            }
            else if (type.Contains("_") == false && description == string.Empty)
                Code1ToCode6 = string.Empty;
            return Code1ToCode6;
        }
        #region: MEP
        private List<Conduits> TransferConduits(List<Element> list)
        {
            List<Conduits> listCus = new List<Conduits>();
            try
            {
                foreach (Element element in list)
                {
                    Conduits cus = new Conduits();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    GetAssemblyandKeynote(element, out assemblycode, out assemblydescription, out keynote);
                    description = CheckDescription(element);
                    cus.ReferenceLevel = CheckParameter(element, "Reference Level");
                    cus.SystemOrLoadable = element.LookupParameter("KindOfFamily").AsString();
                    cus.Family = family;
                    cus.AssemblyCode = assemblycode;
                    cus.AssemblyDescription = assemblydescription;
                    cus.Type = type;
                    cus.KeyNote = keynote;
                    cus.Description = description;
                    if (cus.Type.Contains("_") && cus.Description == string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + cus.Type.Substring(cus.Type.LastIndexOf('_') + 1);
                    }
                    else if (cus.Description != string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + description;
                        if (cus.Code1ToCode6.EndsWith("."))
                        {
                            cus.Code1ToCode6 = string.Empty;
                        }
                    }
                    else if (cus.Type.Contains("_") == false && cus.Description == string.Empty)
                        cus.Code1ToCode6 = string.Empty;
                    cus.ID = element.Id.ToString();
                    if (level == string.Empty || level == null)
                    {
                        listCus.Add(cus);
                    }
                    else
                    {
                        if (cus.ReferenceLevel == level)
                        {
                            listCus.Add(cus);
                        }
                    }
                }
            }
            catch { }
            return listCus;
        }
        private List<PipeFittings> TransferPipeFittings(List<Element> list)
        {
            List<PipeFittings> listCus = new List<PipeFittings>();
            try
            {
                foreach (Element element in list)
                {
                    PipeFittings cus = new PipeFittings();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    GetAssemblyandKeynote(element, out assemblycode, out assemblydescription, out keynote);
                    description = CheckDescription(element);
                    cus.Level = CheckParameter(element, "Level");
                    cus.Family = family;
                    cus.AssemblyCode = assemblycode;
                    cus.AssemblyDescription = assemblydescription;
                    cus.Type = type;
                    cus.KeyNote = keynote;
                    cus.SystemOrLoadable = element.LookupParameter("KindOfFamily").AsString();
                    Floor floor = element as Floor;
                    cus.Description = description;
                    if (cus.Type.Contains("_"))
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + cus.Type.Substring(cus.Type.LastIndexOf('_') + 1);
                    }
                    else if (cus.Description != string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + description;
                        if (cus.Code1ToCode6.EndsWith("."))
                        {
                            cus.Code1ToCode6 = string.Empty;
                        }
                    }
                    else if (cus.Type.Contains("_") == false && cus.Description == string.Empty)
                        cus.Code1ToCode6 = string.Empty;
                    cus.ID = element.Id.ToString();
                    if (level == string.Empty || level == null)
                    {
                        listCus.Add(cus);
                    }
                    else
                    {
                        if (cus.Level == level)
                        {
                            listCus.Add(cus);
                        }
                    }
                }
            }
            catch { }
            return listCus;
        }
        private List<Ducts> TransferDucts(List<Element> list)
        {
            List<Ducts> listCus = new List<Ducts>();
            try
            {
                foreach (Element element in list)
                {
                    Ducts cus = new Ducts();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    GetAssemblyandKeynote(element, out assemblycode, out assemblydescription, out keynote);
                    description = CheckDescription(element);
                    cus.ReferenceLevel = CheckParameter(element, "Reference Level");
                    cus.SystemOrLoadable = element.LookupParameter("KindOfFamily").AsString();
                    cus.Family = family;
                    cus.AssemblyCode = assemblycode;
                    cus.AssemblyDescription = assemblydescription;
                    cus.Type = type;
                    cus.KeyNote = keynote;
                    cus.Description = description;
                    if (cus.Type.Contains("_") && cus.Description == string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + cus.Type.Substring(cus.Type.LastIndexOf('_') + 1);
                    }
                    else if (cus.Description != string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + description;
                        if (cus.Code1ToCode6.EndsWith("."))
                        {
                            cus.Code1ToCode6 = string.Empty;
                        }
                    }
                    else if (cus.Type.Contains("_") == false && cus.Description == string.Empty)
                        cus.Code1ToCode6 = string.Empty;
                    cus.ID = element.Id.ToString();
                    if (level == string.Empty || level == null)
                    {
                        listCus.Add(cus);
                    }
                    else
                    {
                        if (cus.ReferenceLevel == level)
                        {
                            listCus.Add(cus);
                        }
                    }
                }
            }
            catch { }
            return listCus;
        }
        private List<DuctsFittings> TransferDuctFittings(List<Element> list)
        {
            List<DuctsFittings> listCus = new List<DuctsFittings>();
            try
            {
                foreach (Element element in list)
                {
                    DuctsFittings cus = new DuctsFittings();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    GetAssemblyandKeynote(element, out assemblycode, out assemblydescription, out keynote);
                    description = CheckDescription(element);
                    cus.Level = CheckParameter(element, "Level");
                    cus.Family = family;
                    cus.AssemblyCode = assemblycode;
                    cus.AssemblyDescription = assemblydescription;
                    cus.Type = type;
                    cus.KeyNote = keynote;
                    cus.SystemOrLoadable = element.LookupParameter("KindOfFamily").AsString();
                    Floor floor = element as Floor;
                    cus.Description = description;
                    if (cus.Type.Contains("_"))
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + cus.Type.Substring(cus.Type.LastIndexOf('_') + 1);
                    }
                    else if (cus.Description != string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + description;
                        if (cus.Code1ToCode6.EndsWith("."))
                        {
                            cus.Code1ToCode6 = string.Empty;
                        }
                    }
                    else if (cus.Type.Contains("_") == false && cus.Description == string.Empty)
                        cus.Code1ToCode6 = string.Empty;
                    cus.ID = element.Id.ToString();
                    if (level == string.Empty || level == null)
                    {
                        listCus.Add(cus);
                    }
                    else
                    {
                        if (cus.Level == level)
                        {
                            listCus.Add(cus);
                        }
                    }
                }
            }
            catch { }
            return listCus;
        }
        private List<CableTrays> TransferCableTrays(List<Element> list)
        {
            List<CableTrays> listCus = new List<CableTrays>();
            try
            {
                foreach (Element element in list)
                {
                    CableTrays cus = new CableTrays();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    cus.Level = CheckParameter(element, "Level");
                    cus.ReferenceLevel = CheckParameter(element, "Reference Level");
                    cus.Family = family;
                    cus.Type = type;
                    cus.Material = CheckParameter(element, "Material");
                    cus.Width = CheckParameter(element, "Width");
                    cus.Height = CheckParameter(element, "Height");
                    cus.Length = CheckParameter(element, "Length");
                    listCus.Add(cus);
                }
            }
            catch { }
            return listCus;
        }
        private List<FlexDucts> TransferFlexDucts(List<Element> list)
        {
            List<FlexDucts> listCus = new List<FlexDucts>();
            try
            {
                foreach (Element element in list)
                {
                    FlexDucts cus = new FlexDucts();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    cus.Level = CheckParameter(element, "Level");
                    cus.ReferenceLevel = CheckParameter(element, "Reference Level");
                    cus.Family = family;
                    cus.Type = type;
                    cus.Material = CheckParameter(element, "Material");
                    cus.SystemType = CheckParameter(element, "System Type");
                    cus.Diameter = CheckParameter(element, "Diameter");
                    cus.Length = CheckParameter(element, "Length");
                    listCus.Add(cus);
                }
            }
            catch { }
            return listCus;
        }
        private List<Pipes> TransferPipes(List<Element> list)
        {
            List<Pipes> listCus = new List<Pipes>();
            try
            {
                foreach (Element element in list)
                {
                    Pipes cus = new Pipes();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    GetAssemblyandKeynote(element, out assemblycode, out assemblydescription, out keynote);
                    description = CheckDescription(element);
                    cus.ReferenceLevel = CheckParameter(element, "Reference Level");
                    cus.SystemOrLoadable = element.LookupParameter("KindOfFamily").AsString();
                    cus.Family = family;
                    cus.AssemblyCode = assemblycode;
                    cus.AssemblyDescription = assemblydescription;
                    cus.Type = type;
                    cus.KeyNote = keynote;
                    cus.Description = description;
                    if (cus.Type.Contains("_") && cus.Description == string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + cus.Type.Substring(cus.Type.LastIndexOf('_') + 1);
                    }
                    else if (cus.Description != string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + description;
                        if (cus.Code1ToCode6.EndsWith("."))
                        {
                            cus.Code1ToCode6 = string.Empty;
                        }
                    }
                    else if (cus.Type.Contains("_") == false && cus.Description == string.Empty)
                        cus.Code1ToCode6 = string.Empty;
                    cus.ID = element.Id.ToString();
                    if (level == string.Empty || level == null)
                    {
                        listCus.Add(cus);
                    }
                    else
                    {
                        if (cus.ReferenceLevel == level)
                        {
                            listCus.Add(cus);
                        }
                    }
                }
            }
            catch { }
            return listCus;
        }
        private List<ConduitFittings> TransferConduitFittings(List<Element> list)
        {
            List<ConduitFittings> listCus = new List<ConduitFittings>();
            try
            {
                foreach (Element element in list)
                {
                    ConduitFittings cus = new ConduitFittings();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    GetAssemblyandKeynote(element, out assemblycode, out assemblydescription, out keynote);
                    description = CheckDescription(element);
                    cus.Level = CheckParameter(element, "Level");
                    cus.Family = family;
                    cus.AssemblyCode = assemblycode;
                    cus.AssemblyDescription = assemblydescription;
                    cus.Type = type;
                    cus.KeyNote = keynote;
                    cus.SystemOrLoadable = element.LookupParameter("KindOfFamily").AsString();
                    Floor floor = element as Floor;
                    cus.Description = description;
                    if (cus.Type.Contains("_"))
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + cus.Type.Substring(cus.Type.LastIndexOf('_') + 1);
                    }
                    else if (cus.Description != string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + description;
                        if (cus.Code1ToCode6.EndsWith("."))
                        {
                            cus.Code1ToCode6 = string.Empty;
                        }
                    }
                    else if (cus.Type.Contains("_") == false && cus.Description == string.Empty)
                        cus.Code1ToCode6 = string.Empty;
                    cus.ID = element.Id.ToString();
                    if (level == string.Empty || level == null)
                    {
                        listCus.Add(cus);
                    }
                    else
                    {
                        if (cus.Level == level)
                        {
                            listCus.Add(cus);
                        }
                    }
                }
            }
            catch { }
            return listCus;
        }
        private List<ElectricalEquipment> TransferElectricalEquipment(List<Element> list)
        {
            List<ElectricalEquipment> listCus = new List<ElectricalEquipment>();
            try
            {
                foreach (Element element in list)
                {
                    ElectricalEquipment cus = new ElectricalEquipment();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    GetAssemblyandKeynote(element, out assemblycode, out assemblydescription, out keynote);
                    description = CheckDescription(element);
                    cus.Level = CheckParameter(element, "Level");
                    cus.Family = family;
                    cus.AssemblyCode = assemblycode;
                    cus.AssemblyDescription = assemblydescription;
                    cus.Type = type;
                    cus.KeyNote = keynote;
                    cus.SystemOrLoadable = element.LookupParameter("KindOfFamily").AsString();
                    Floor floor = element as Floor;
                    cus.Description = description;
                    if (cus.Type.Contains("_"))
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + cus.Type.Substring(cus.Type.LastIndexOf('_') + 1);
                    }
                    else if (cus.Description != string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + description;
                        if (cus.Code1ToCode6.EndsWith("."))
                        {
                            cus.Code1ToCode6 = string.Empty;
                        }
                    }
                    else if (cus.Type.Contains("_") == false && cus.Description == string.Empty)
                        cus.Code1ToCode6 = string.Empty;
                    cus.ID = element.Id.ToString();
                    if (level == string.Empty || level == null)
                    {
                        listCus.Add(cus);
                    }
                    else
                    {
                        if (cus.Level == level)
                        {
                            listCus.Add(cus);
                        }
                    }
                }
            }
            catch { }
            return listCus;
        }
        private List<FireAlarmDevices> TransferFireAlarmDevices(List<Element> list)
        {
            List<FireAlarmDevices> listCus = new List<FireAlarmDevices>();
            try
            {
                foreach (Element element in list)
                {
                    FireAlarmDevices cus = new FireAlarmDevices();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    GetAssemblyandKeynote(element, out assemblycode, out assemblydescription, out keynote);
                    description = CheckDescription(element);
                    cus.Level = CheckParameter(element, "Level");
                    cus.Family = family;
                    cus.AssemblyCode = assemblycode;
                    cus.AssemblyDescription = assemblydescription;
                    cus.Type = type;
                    cus.KeyNote = keynote;
                    cus.SystemOrLoadable = element.LookupParameter("KindOfFamily").AsString();
                    Floor floor = element as Floor;
                    cus.Description = description;
                    if (cus.Type.Contains("_"))
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + cus.Type.Substring(cus.Type.LastIndexOf('_') + 1);
                    }
                    else if (cus.Description != string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + description;
                        if (cus.Code1ToCode6.EndsWith("."))
                        {
                            cus.Code1ToCode6 = string.Empty;
                        }
                    }
                    else if (cus.Type.Contains("_") == false && cus.Description == string.Empty)
                        cus.Code1ToCode6 = string.Empty;
                    cus.ID = element.Id.ToString();
                    if (level == string.Empty || level == null)
                    {
                        listCus.Add(cus);
                    }
                    else
                    {
                        if (cus.Level == level)
                        {
                            listCus.Add(cus);
                        }
                    }
                }
            }
            catch { }
            return listCus;
        }
        private List<MechanicalEquipment> TransferMechanicalEquipment(List<Element> list)
        {
            List<MechanicalEquipment> listCus = new List<MechanicalEquipment>();
            try
            {
                foreach (Element element in list)
                {
                    MechanicalEquipment cus = new MechanicalEquipment();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    GetAssemblyandKeynote(element, out assemblycode, out assemblydescription, out keynote);
                    description = CheckDescription(element);
                    cus.Level = CheckParameter(element, "Level");
                    cus.Family = family;
                    cus.AssemblyCode = assemblycode;
                    cus.AssemblyDescription = assemblydescription;
                    cus.Type = type;
                    cus.KeyNote = keynote;
                    cus.SystemOrLoadable = element.LookupParameter("KindOfFamily").AsString();

                    cus.Description = description;
                    if (cus.Type.Contains("_"))
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + cus.Type.Substring(cus.Type.LastIndexOf('_') + 1);
                    }
                    else if (cus.Description != string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + description;
                        if (cus.Code1ToCode6.EndsWith("."))
                        {
                            cus.Code1ToCode6 = string.Empty;
                        }
                    }
                    else if (cus.Type.Contains("_") == false && cus.Description == string.Empty)
                        cus.Code1ToCode6 = string.Empty;
                    cus.ID = element.Id.ToString();
                    if (level == string.Empty || level == null)
                    {
                        listCus.Add(cus);
                    }
                    else
                    {
                        if (cus.Level == level)
                        {
                            listCus.Add(cus);
                        }
                    }
                }
            }

            catch { }
            return listCus;
        }
        private List<PipeAccessories> TransferPipeAccessories(List<Element> list)
        {
            List<PipeAccessories> listCus = new List<PipeAccessories>();
            try
            {
                foreach (Element element in list)
                {
                    PipeAccessories cus = new PipeAccessories();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    GetAssemblyandKeynote(element, out assemblycode, out assemblydescription, out keynote);
                    description = CheckDescription(element);
                    cus.Level = CheckParameter(element, "Level");
                    cus.Family = family;
                    cus.AssemblyCode = assemblycode;
                    cus.AssemblyDescription = assemblydescription;
                    cus.Type = type;
                    cus.KeyNote = keynote;
                    cus.SystemOrLoadable = element.LookupParameter("KindOfFamily").AsString();
                    Floor floor = element as Floor;
                    cus.Description = description;
                    if (cus.Type.Contains("_"))
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + cus.Type.Substring(cus.Type.LastIndexOf('_') + 1);
                    }
                    else if (cus.Description != string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + description;
                        if (cus.Code1ToCode6.EndsWith("."))
                        {
                            cus.Code1ToCode6 = string.Empty;
                        }
                    }
                    else if (cus.Type.Contains("_") == false && cus.Description == string.Empty)
                        cus.Code1ToCode6 = string.Empty;
                    cus.ID = element.Id.ToString();
                    if (level == string.Empty || level == null)
                    {
                        listCus.Add(cus);
                    }
                    else
                    {
                        if (cus.Level == level)
                        {
                            listCus.Add(cus);
                        }
                    }
                }
            }
            catch { }
            return listCus;
        }
        private List<DuctAccessories> TransferDuctAccessories(List<Element> list)
        {
            List<DuctAccessories> listCus = new List<DuctAccessories>();
            try
            {
                foreach (Element element in list)
                {
                    DuctAccessories cus = new DuctAccessories();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    GetAssemblyandKeynote(element, out assemblycode, out assemblydescription, out keynote);
                    description = CheckDescription(element);
                    cus.Level = CheckParameter(element, "Level");
                    cus.Family = family;
                    cus.AssemblyCode = assemblycode;
                    cus.AssemblyDescription = assemblydescription;
                    cus.Type = type;
                    cus.KeyNote = keynote;
                    cus.SystemOrLoadable = element.LookupParameter("KindOfFamily").AsString();
                    Floor floor = element as Floor;
                    cus.Description = description;
                    if (cus.Type.Contains("_"))
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + cus.Type.Substring(cus.Type.LastIndexOf('_') + 1);
                    }
                    else if (cus.Description != string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + description;
                        if (cus.Code1ToCode6.EndsWith("."))
                        {
                            cus.Code1ToCode6 = string.Empty;
                        }
                    }
                    else if (cus.Type.Contains("_") == false && cus.Description == string.Empty)
                        cus.Code1ToCode6 = string.Empty;
                    cus.ID = element.Id.ToString();
                    if (level == string.Empty || level == null)
                    {
                        listCus.Add(cus);
                    }
                    else
                    {
                        if (cus.Level == level)
                        {
                            listCus.Add(cus);
                        }
                    }
                }
            }
            catch { }
            return listCus;
        }
        private List<Sprinklers> TransferPipSprinklers(List<Element> list)
        {
            List<Sprinklers> listCus = new List<Sprinklers>();
            try
            {
                foreach (Element element in list)
                {
                    Sprinklers cus = new Sprinklers();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    GetAssemblyandKeynote(element, out assemblycode, out assemblydescription, out keynote);
                    description = CheckDescription(element);
                    cus.Level = CheckParameter(element, "Level");
                    cus.Family = family;
                    cus.AssemblyCode = assemblycode;
                    cus.AssemblyDescription = assemblydescription;
                    cus.Type = type;
                    cus.KeyNote = keynote;
                    cus.SystemOrLoadable = element.LookupParameter("KindOfFamily").AsString();
                    Floor floor = element as Floor;
                    cus.Description = description;
                    if (cus.Type.Contains("_"))
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + cus.Type.Substring(cus.Type.LastIndexOf('_') + 1);
                    }
                    else if (cus.Description != string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + description;
                        if (cus.Code1ToCode6.EndsWith("."))
                        {
                            cus.Code1ToCode6 = string.Empty;
                        }
                    }
                    else if (cus.Type.Contains("_") == false && cus.Description == string.Empty)
                        cus.Code1ToCode6 = string.Empty;
                    cus.ID = element.Id.ToString();
                    if (level == string.Empty || level == null)
                    {
                        listCus.Add(cus);
                    }
                    else
                    {
                        if (cus.Level == level)
                        {
                            listCus.Add(cus);
                        }
                    }
                }
            }
            catch { }
            return listCus;
        }
        private List<PlumbingFixtures> TransferPipPlumbingFixtures(List<Element> list)
        {
            List<PlumbingFixtures> listCus = new List<PlumbingFixtures>();
            try
            {
                foreach (Element element in list)
                {
                    PlumbingFixtures cus = new PlumbingFixtures();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    GetAssemblyandKeynote(element, out assemblycode, out assemblydescription, out keynote);
                    description = CheckDescription(element);
                    cus.Level = CheckParameter(element, "Level");
                    cus.Family = family;
                    cus.AssemblyCode = assemblycode;
                    cus.AssemblyDescription = assemblydescription;
                    cus.Type = type;
                    cus.KeyNote = keynote;
                    cus.SystemOrLoadable = element.LookupParameter("KindOfFamily").AsString();
                    Floor floor = element as Floor;
                    cus.Description = description;
                    if (cus.Type.Contains("_"))
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + cus.Type.Substring(cus.Type.LastIndexOf('_') + 1);
                    }
                    else if (cus.Description != string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + description;
                        if (cus.Code1ToCode6.EndsWith("."))
                        {
                            cus.Code1ToCode6 = string.Empty;
                        }
                    }
                    else if (cus.Type.Contains("_") == false && cus.Description == string.Empty)
                        cus.Code1ToCode6 = string.Empty;
                    cus.ID = element.Id.ToString();
                    if (level == string.Empty || level == null)
                    {
                        listCus.Add(cus);
                    }
                    else
                    {
                        if (cus.Level == level)
                        {
                            listCus.Add(cus);
                        }
                    }
                }
            }
            catch { }
            return listCus;
        }
        private List<AirTerminals> TransferAirTerminals(List<Element> list)
        {
            List<AirTerminals> listCus = new List<AirTerminals>();
            try
            {
                foreach (Element element in list)
                {
                    AirTerminals cus = new AirTerminals();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    GetAssemblyandKeynote(element, out assemblycode, out assemblydescription, out keynote);
                    description = CheckDescription(element);
                    cus.Level = CheckParameter(element, "Level");
                    cus.Family = family;
                    cus.AssemblyCode = assemblycode;
                    cus.AssemblyDescription = assemblydescription;
                    cus.Type = type;
                    cus.KeyNote = keynote;
                    cus.SystemOrLoadable = element.LookupParameter("KindOfFamily").AsString();
                    Floor floor = element as Floor;
                    cus.Description = description;
                    if (cus.Type.Contains("_"))
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + cus.Type.Substring(cus.Type.LastIndexOf('_') + 1);
                    }
                    else if (cus.Description != string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + description;
                        if (cus.Code1ToCode6.EndsWith("."))
                        {
                            cus.Code1ToCode6 = string.Empty;
                        }
                    }
                    else if (cus.Type.Contains("_") == false && cus.Description == string.Empty)
                        cus.Code1ToCode6 = string.Empty;
                    cus.ID = element.Id.ToString();
                    if (level == string.Empty || level == null)
                    {
                        listCus.Add(cus);
                    }
                    else
                    {
                        if (cus.Level == level)
                        {
                            listCus.Add(cus);
                        }
                    }
                }
            }
            catch { }
            return listCus;
        }
        private List<PipeInsulations> TransferPipeInsulations(List<Element> list)
        {
            List<PipeInsulations> listCus = new List<PipeInsulations>();
            try
            {
                foreach (Element element in list)
                {
                    PipeInsulations cus = new PipeInsulations();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    GetAssemblyandKeynote(element, out assemblycode, out assemblydescription, out keynote);
                    description = CheckDescription(element);
                    cus.Family = family;
                    cus.AssemblyCode = assemblycode;
                    cus.AssemblyDescription = assemblydescription;
                    cus.Type = type;
                    cus.KeyNote = keynote;
                    cus.SystemOrLoadable = element.LookupParameter("KindOfFamily").AsString();
                    Floor floor = element as Floor;
                    cus.Description = description;
                    if (cus.Type.Contains("_"))
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + cus.Type.Substring(cus.Type.LastIndexOf('_') + 1);
                    }
                    else if (cus.Description != string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + description;
                        if (cus.Code1ToCode6.EndsWith("."))
                        {
                            cus.Code1ToCode6 = string.Empty;
                        }
                    }
                    else if (cus.Type.Contains("_") == false && cus.Description == string.Empty)
                        cus.Code1ToCode6 = string.Empty;
                    cus.ID = element.Id.ToString();
                    if (level == string.Empty || level == null)
                    {
                        listCus.Add(cus);
                    }
                    else
                    {
                        if (cus.Level == level)
                        {
                            listCus.Add(cus);
                        }
                    }
                }
            }
            catch { }
            return listCus;
        }
        private List<DuctInsulations> TransferDuctInsulations(List<Element> list)
        {
            List<DuctInsulations> listCus = new List<DuctInsulations>();
            try
            {
                foreach (Element element in list)
                {
                    DuctInsulations cus = new DuctInsulations();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    GetAssemblyandKeynote(element, out assemblycode, out assemblydescription, out keynote);
                    description = CheckDescription(element);
                    cus.Family = family;
                    cus.AssemblyCode = assemblycode;
                    cus.AssemblyDescription = assemblydescription;
                    cus.Type = type;
                    cus.KeyNote = keynote;
                    cus.SystemOrLoadable = element.LookupParameter("KindOfFamily").AsString();
                    Floor floor = element as Floor;
                    cus.Description = description;
                    if (cus.Type.Contains("_"))
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + cus.Type.Substring(cus.Type.LastIndexOf('_') + 1);
                    }
                    else if (cus.Description != string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + description;
                        if (cus.Code1ToCode6.EndsWith("."))
                        {
                            cus.Code1ToCode6 = string.Empty;
                        }
                    }
                    else if (cus.Type.Contains("_") == false && cus.Description == string.Empty)
                        cus.Code1ToCode6 = string.Empty;
                    cus.ID = element.Id.ToString();
                    if (level == string.Empty || level == null)
                    {
                        listCus.Add(cus);
                    }
                    else
                    {
                        if (cus.Level == level)
                        {
                            listCus.Add(cus);
                        }
                    }
                }
            }
            catch { }
            return listCus;
        }
        #endregion
        #region: Structural
        private List<StructuralFraming> TransferStructuralFraming(List<Element> list)
        {
            List<StructuralFraming> listCus = new List<StructuralFraming>();
            try
            {
                foreach (Element element in list)
                {
                    StructuralFraming cus = new StructuralFraming();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    GetAssemblyandKeynote(element, out assemblycode, out assemblydescription, out keynote);
                    description = CheckDescription(element);
                    cus.ReferenceLevel = CheckParameter(element, "Reference Level");
                    cus.SystemOrLoadable = element.LookupParameter("KindOfFamily").AsString();
                    cus.Family = family;
                    cus.AssemblyCode = assemblycode;
                    cus.AssemblyDescription = assemblydescription;
                    cus.Type = type;
                    cus.KeyNote = keynote;
                    cus.Description = description;
                    if (cus.Type.Contains("_") && cus.Description == string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + cus.Type.Substring(cus.Type.LastIndexOf('_') + 1);
                    }
                    else if (cus.Description != string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + description;
                        if (cus.Code1ToCode6.EndsWith("."))
                        {
                            cus.Code1ToCode6 = string.Empty;
                        }
                    }
                    else if (cus.Type.Contains("_") == false && cus.Description == string.Empty)
                        cus.Code1ToCode6 = string.Empty;
                    cus.ID = element.Id.ToString();
                    if (level == string.Empty || level == null)
                    {
                        listCus.Add(cus);
                    }
                    else
                    {
                        if (cus.ReferenceLevel == level)
                        {
                            listCus.Add(cus);
                        }
                    }
                }
            }
            catch { }
            return listCus;
        }
        private List<StructuralColumn> TransferStructuralColumn(List<Element> list)
        {
            List<StructuralColumn> listCus = new List<StructuralColumn>();
            try
            {
                foreach (Element element in list)
                {
                    StructuralColumn cus = new StructuralColumn();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    GetAssemblyandKeynote(element, out assemblycode, out assemblydescription, out keynote);
                    description = CheckDescription(element);
                    cus.BaseLevel = CheckParameter(element, "Base Level");
                    cus.TopLevel = CheckParameter(element, "Top Level");
                    cus.SystemOrLoadable = element.LookupParameter("KindOfFamily").AsString();
                    cus.Family = family;
                    cus.AssemblyCode = assemblycode;
                    cus.AssemblyDescription = assemblydescription;
                    cus.Type = type;
                    cus.KeyNote = keynote;
                    cus.Description = description;
                    if (cus.Type.Contains("_"))
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + cus.Type.Substring(cus.Type.LastIndexOf('_') + 1);
                    }
                    else if (cus.Description != string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + description;
                        if (cus.Code1ToCode6.EndsWith("."))
                        {
                            cus.Code1ToCode6 = string.Empty;
                        }
                    }
                    else if (cus.Type.Contains("_") == false && cus.Description == string.Empty)
                        cus.Code1ToCode6 = string.Empty;
                    cus.ID = element.Id.ToString();
                    if (level == string.Empty || level == null)
                    {
                        listCus.Add(cus);
                    }
                    else
                    {
                        if (cus.BaseLevel == level)
                        {
                            listCus.Add(cus);
                        }
                    }
                }
            }
            catch { }
            return listCus;
        }
        private List<StructuralWall> TransferWall(List<Element> list)
        {
            List<StructuralWall> listCus = new List<StructuralWall>();
            try
            {
                foreach (Element element in list)
                {
                    StructuralWall cus = new StructuralWall();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    GetAssemblyandKeynote(element, out assemblycode, out assemblydescription, out keynote);
                    description = CheckDescription(element);
                    cus.BaseConstraint = CheckParameter(element, "Base Constraint");
                    cus.TopConstraint = CheckParameter(element, "Top Constraint");
                    cus.SystemOrLoadable = element.LookupParameter("KindOfFamily").AsString();
                    cus.Family = family;
                    cus.AssemblyCode = assemblycode;
                    cus.AssemblyDescription = assemblydescription;
                    cus.Type = type;
                    cus.KeyNote = keynote;
                    Wall wall = element as Wall;
                    cus.Description = description;
                    if (cus.Type.Contains("_"))
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + cus.Type.Substring(cus.Type.LastIndexOf('_') + 1);
                    }
                    else if (cus.Description != string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + description;
                        if (cus.Code1ToCode6.EndsWith("."))
                        {
                            cus.Code1ToCode6 = string.Empty;
                        }
                    }
                    else if (cus.Type.Contains("_") == false && cus.Description == string.Empty)
                        cus.Code1ToCode6 = string.Empty;
                    cus.ID = element.Id.ToString();
                    if (level == string.Empty || level == null)
                    {
                        listCus.Add(cus);
                    }
                    else
                    {
                        if (cus.BaseConstraint == level)
                        {
                            listCus.Add(cus);
                        }
                    }
                }
            }
            catch { }
            return listCus;
        }
        private List<StructuralFoundations> TransferStructuralFoundations(List<Element> list)
        {
            List<StructuralFoundations> listCus = new List<StructuralFoundations>();
            try
            {
                foreach (Element element in list)
                {
                    StructuralFoundations cus = new StructuralFoundations();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    GetAssemblyandKeynote(element, out assemblycode, out assemblydescription, out keynote);
                    description = CheckDescription(element);
                    cus.Family = family;
                    cus.AssemblyCode = assemblycode;
                    cus.AssemblyDescription = assemblydescription;
                    cus.Type = type;
                    cus.KeyNote = keynote;
                    cus.SystemOrLoadable = element.LookupParameter("KindOfFamily").AsString();
                    cus.Description = description;
                    if (cus.Type.Contains("_"))
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + cus.Type.Substring(cus.Type.LastIndexOf('_') + 1);
                    }
                    else if (cus.Description != string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + description;
                        if (cus.Code1ToCode6.EndsWith("."))
                        {
                            cus.Code1ToCode6 = string.Empty;
                        }
                    }
                    else if (cus.Type.Contains("_") == false && cus.Description == string.Empty)
                        cus.Code1ToCode6 = string.Empty;
                    cus.ID = element.Id.ToString();
                    if (level == string.Empty || level == null)
                    {
                        listCus.Add(cus);
                    }
                    else
                    {
                        if (cus.Level == level)
                        {
                            listCus.Add(cus);
                        }
                    }
                }
            }
            catch { }
            return listCus;
        }
        private List<FLoors> TransferFLoors(List<Element> list)
        {
            List<FLoors> listCus = new List<FLoors>();
            try
            {
                foreach (Element element in list)
                {
                    FLoors cus = new FLoors();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    GetAssemblyandKeynote(element, out assemblycode, out assemblydescription, out keynote);
                    description = CheckDescription(element);
                    cus.Level = CheckParameter(element, "Level");
                    cus.Family = family;
                    cus.AssemblyCode = assemblycode;
                    cus.AssemblyDescription = assemblydescription;
                    cus.Type = type;
                    cus.KeyNote = keynote;
                    cus.SystemOrLoadable = element.LookupParameter("KindOfFamily").AsString();
                    Floor floor = element as Floor;
                    cus.Description = description;
                    if (cus.Type.Contains("_"))
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + cus.Type.Substring(cus.Type.LastIndexOf('_') + 1);
                    }
                    else if (cus.Description != string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + description;
                        if (cus.Code1ToCode6.EndsWith("."))
                        {
                            cus.Code1ToCode6 = string.Empty;
                        }
                    }
                    else if (cus.Type.Contains("_") == false && cus.Description == string.Empty)
                        cus.Code1ToCode6 = string.Empty;
                    cus.ID = element.Id.ToString();
                    if (level == string.Empty || level == null)
                    {
                        listCus.Add(cus);
                    }
                    else
                    {
                        if (cus.Level == level)
                        {
                            listCus.Add(cus);
                        }
                    }
                }
            }
            catch { }
            return listCus;
        }
        #endregion
        #region: Architectural
        private List<ArchitecColumn> TransferColumns(List<Element> list)
        {
            List<ArchitecColumn> listCus = new List<ArchitecColumn>();
            try
            {
                foreach (Element element in list)
                {
                    ArchitecColumn cus = new ArchitecColumn();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    GetAssemblyandKeynote(element, out assemblycode, out assemblydescription, out keynote);
                    description = CheckDescription(element);
                    cus.BaseLevel = CheckParameter(element, "Base Level");
                    cus.TopLevel = CheckParameter(element, "Top Level");
                    cus.SystemOrLoadable = element.LookupParameter("KindOfFamily").AsString();
                    cus.Family = family;
                    cus.AssemblyCode = assemblycode;
                    cus.AssemblyDescription = assemblydescription;
                    cus.Type = type;
                    cus.KeyNote = keynote;
                    FamilyInstance fi = element as FamilyInstance;
                    cus.Description = description;
                    if (cus.Type.Contains("_"))
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + cus.Type.Substring(cus.Type.LastIndexOf('_') + 1);
                    }
                    else if (cus.Description != string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + description;
                        if (cus.Code1ToCode6.EndsWith("."))
                        {
                            cus.Code1ToCode6 = string.Empty;
                        }
                    }
                    else if (cus.Type.Contains("_") == false && cus.Description == string.Empty)
                        cus.Code1ToCode6 = string.Empty;
                    cus.ID = element.Id.ToString();
                    if (level == string.Empty || level == null)
                    {
                        listCus.Add(cus);
                    }
                    else
                    {
                        if (cus.BaseLevel == level)
                        {
                            listCus.Add(cus);
                        }
                    }
                }
            }
            catch { }
            return listCus;
        }
        private List<Ramps> TransferRamps(List<Element> list)
        {
            List<Ramps> listCus = new List<Ramps>();
            try
            {
                foreach (Element element in list)
                {
                    Ramps cus = new Ramps();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    GetAssemblyandKeynote(element, out assemblycode, out assemblydescription, out keynote);
                    description = CheckDescription(element);
                    cus.BaseLevel = CheckParameter(element, "Base Level");
                    cus.TopLevel = CheckParameter(element, "Top Level");
                    cus.SystemOrLoadable = element.LookupParameter("KindOfFamily").AsString();
                    cus.Family = family;
                    cus.AssemblyCode = assemblycode;
                    cus.AssemblyDescription = assemblydescription;
                    cus.Type = type;
                    cus.KeyNote = keynote;
                    FamilyInstance fi = element as FamilyInstance;
                    cus.Description = description;
                    if (cus.Type.Contains("_"))
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + cus.Type.Substring(cus.Type.LastIndexOf('_') + 1);
                    }
                    else if (cus.Description != string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + description;
                        if (cus.Code1ToCode6.EndsWith("."))
                        {
                            cus.Code1ToCode6 = string.Empty;
                        }
                    }
                    else if (cus.Type.Contains("_") == false && cus.Description == string.Empty)
                        cus.Code1ToCode6 = string.Empty;
                    cus.ID = element.Id.ToString();
                    if (level == string.Empty || level == null)
                    {
                        listCus.Add(cus);
                    }
                    else
                    {
                        if (cus.BaseLevel == level)
                        {
                            listCus.Add(cus);
                        }
                    }
                }
            }
            catch { }
            return listCus;
        }
        private List<Doors> TransferDoors(List<Element> list)
        {
            List<Doors> listCus = new List<Doors>();
            try
            {
                foreach (Element element in list)
                {
                    Doors cus = new Doors();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    GetAssemblyandKeynote(element, out assemblycode, out assemblydescription, out keynote);
                    description = CheckDescription(element);
                    cus.Level = CheckParameter(element, "Level");
                    cus.Family = family;
                    cus.Type = type;
                    cus.SystemOrLoadable = element.LookupParameter("KindOfFamily").AsString();
                    cus.AssemblyCode = assemblycode;
                    cus.AssemblyDescription = assemblydescription;
                    cus.KeyNote = keynote;
                    cus.Description = description;
                    if (cus.Type.Contains("_") && cus.Description == string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + cus.Type.Substring(cus.Type.LastIndexOf('_') + 1);
                    }
                    else if (cus.Description != string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + description;
                        if (cus.Code1ToCode6.EndsWith("."))
                        {
                            cus.Code1ToCode6 = string.Empty;
                        }
                    }
                    else if (cus.Type.Contains("_") == false && cus.Description == string.Empty)
                        cus.Code1ToCode6 = string.Empty;
                    cus.ID = element.Id.ToString();
                    if (level == string.Empty || level == null)
                    {
                        listCus.Add(cus);
                    }
                    else
                    {
                        if (cus.Level == level)
                        {
                            listCus.Add(cus);
                        }
                    }
                }
            }
            catch { }
            return listCus;
        }
        private List<Ceilings> TransferCeiling(List<Element> list)
        {
            List<Ceilings> listCus = new List<Ceilings>();
            try
            {
                foreach (Element element in list)
                {
                    Ceilings cus = new Ceilings();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    GetAssemblyandKeynote(element, out assemblycode, out assemblydescription, out keynote);
                    description = CheckDescription(element);
                    cus.Level = CheckParameter(element, "Level");
                    cus.Family = family;
                    cus.AssemblyCode = assemblycode;
                    cus.AssemblyDescription = assemblydescription;
                    cus.Type = type;
                    cus.KeyNote = keynote;
                    cus.SystemOrLoadable = element.LookupParameter("KindOfFamily").AsString();
                    Floor floor = element as Floor;
                    cus.Description = description;
                    if (cus.Type.Contains("_") && cus.Description == string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + cus.Type.Substring(cus.Type.LastIndexOf('_') + 1);
                    }
                    else if (cus.Description != string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + description;
                        if (cus.Code1ToCode6.EndsWith("."))
                        {
                            cus.Code1ToCode6 = string.Empty;
                        }
                    }
                    else if (cus.Type.Contains("_") == false && cus.Description == string.Empty)
                        cus.Code1ToCode6 = string.Empty;
                    cus.ID = element.Id.ToString();
                    if (level == string.Empty || level == null)
                    {
                        listCus.Add(cus);
                    }
                    else
                    {
                        if (cus.Level == level)
                        {
                            listCus.Add(cus);
                        }
                    }
                }
            }
            catch { }
            return listCus;
        }
        private List<Stair> TransferStairs(List<Element> list)
        {
            List<Stair> listCus = new List<Stair>();
            try
            {
                foreach (Element element in list)
                {
                    Stair cus = new Stair();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    GetAssemblyandKeynote(element, out assemblycode, out assemblydescription, out keynote);
                    description = CheckDescription(element);
                    cus.BaseLevel = CheckParameter(element, "Base Level");
                    cus.TopLevel = CheckParameter(element, "Top Level");
                    cus.SystemOrLoadable = element.LookupParameter("KindOfFamily").AsString();
                    cus.Family = family;
                    cus.AssemblyCode = assemblycode;
                    cus.AssemblyDescription = assemblydescription;
                    cus.Type = type;
                    cus.KeyNote = keynote;
                    cus.Description = description;
                    if (cus.Type.Contains("_"))
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + cus.Type.Substring(cus.Type.LastIndexOf('_') + 1);
                    }
                    else if (cus.Description != string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + description;
                        if (cus.Code1ToCode6.EndsWith("."))
                        {
                            cus.Code1ToCode6 = string.Empty;
                        }
                    }
                    else if (cus.Type.Contains("_") == false && cus.Description == string.Empty)
                        cus.Code1ToCode6 = string.Empty;
                    cus.ID = element.Id.ToString();
                    if (level == string.Empty || level == null)
                    {
                        listCus.Add(cus);
                    }
                    else
                    {
                        if (cus.BaseLevel == level)
                        {
                            listCus.Add(cus);
                        }
                    }
                }
            }
            catch { }
            return listCus;
        }
        private List<Windows> TransferWindows(List<Element> list)
        {
            List<Windows> listCus = new List<Windows>();
            try
            {
                foreach (Element element in list)
                {
                    Windows cus = new Windows();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    GetAssemblyandKeynote(element, out assemblycode, out assemblydescription, out keynote);
                    description = CheckDescription(element);
                    cus.Level = CheckParameter(element, "Level");
                    cus.Family = family;
                    cus.AssemblyCode = assemblycode;
                    cus.AssemblyDescription = assemblydescription;
                    cus.Type = type;
                    cus.KeyNote = keynote;
                    cus.SystemOrLoadable = element.LookupParameter("KindOfFamily").AsString();
                    cus.Description = description;
                    if (cus.Type.Contains("_"))
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + cus.Type.Substring(cus.Type.LastIndexOf('_') + 1);
                    }
                    else if (cus.Description != string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + description;
                        if (cus.Code1ToCode6.EndsWith("."))
                        {
                            cus.Code1ToCode6 = string.Empty;
                        }
                    }
                    else if (cus.Type.Contains("_") == false && cus.Description == string.Empty)
                        cus.Code1ToCode6 = string.Empty;
                    cus.ID = element.Id.ToString();
                    if (level == string.Empty || level == null)
                    {
                        listCus.Add(cus);
                    }
                    else
                    {
                        if (cus.Level == level)
                        {
                            listCus.Add(cus);
                        }
                    }
                }
            }
            catch { }
            return listCus;
        }
        private List<Roof> TransferRoof(List<Element> list)
        {
            List<Roof> listCus = new List<Roof>();
            try
            {
                foreach (Element element in list)
                {
                    Roof cus = new Roof();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    GetAssemblyandKeynote(element, out assemblycode, out assemblydescription, out keynote);
                    description = CheckDescription(element);
                    cus.BaseLevel = CheckParameter(element, "Base Level");
                    cus.SystemOrLoadable = element.LookupParameter("KindOfFamily").AsString();
                    cus.Family = family;
                    cus.AssemblyCode = assemblycode;
                    cus.AssemblyDescription = assemblydescription;
                    cus.Type = type;
                    cus.KeyNote = keynote;
                    cus.Description = description;
                    if (cus.Type.Contains("_") && cus.Description == string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + cus.Type.Substring(cus.Type.LastIndexOf('_') + 1);
                    }
                    else if (cus.Description != string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + description;
                        if (cus.Code1ToCode6.EndsWith("."))
                        {
                            cus.Code1ToCode6 = string.Empty;
                        }
                    }
                    else if (cus.Type.Contains("_") == false && cus.Description == string.Empty)
                        cus.Code1ToCode6 = string.Empty;
                    cus.ID = element.Id.ToString();
                    if (level == string.Empty || level == null)
                    {
                        listCus.Add(cus);
                    }
                    else
                    {
                        if (cus.BaseLevel == level)
                        {
                            listCus.Add(cus);
                        }
                    }
                }
            }
            catch { }
            return listCus;
        }
        #endregion
        #region: All Elements
        private List<AllEles> TransferAllElements(List<Element> list)
        {
            List<AllEles> listCus = new List<AllEles>();
            try
            {
                foreach (Element element in list)
                {
                    AllEles cus = new AllEles();
                    string familyname = CheckParameter(element, "Family and Type");
                    GetFamilyandType(familyname, out family, out type);
                    GetAssemblyandKeynote(element, out assemblycode, out assemblydescription, out keynote);
                    description = CheckDescription(element);
                    cus.Level = CheckParameter(element, "Level");
                    cus.ReferenceLevel = CheckParameter(element, "Reference Level");
                    cus.BaseLevel = CheckParameter(element, "Base Level");
                    cus.TopLevel = CheckParameter(element, "Top Level");
                    cus.BaseConstraint = CheckParameter(element, "Base Constraint");
                    cus.TopConstraint = CheckParameter(element, "Top Constraint");
                    cus.SystemOrLoadable = element.LookupParameter("KindOfFamily").AsString();
                    cus.Family = family;
                    cus.AssemblyCode = assemblycode;
                    cus.AssemblyDescription = assemblydescription;
                    cus.Type = type;
                    cus.KeyNote = keynote;
                    cus.Description = description;

                    if (cus.Type.Contains("_") && cus.Description == string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + cus.Type.Substring(cus.Type.LastIndexOf('_') + 1);
                    }
                    else if (cus.Description != string.Empty)
                    {
                        cus.Code1ToCode6 = cus.AssemblyCode + "." + description;
                        if (cus.Code1ToCode6.EndsWith("."))
                        {
                            cus.Code1ToCode6 = string.Empty;
                        }
                    }
                    else if (cus.Type.Contains("_") == false && cus.Description == string.Empty)
                        cus.Code1ToCode6 = string.Empty;

                    cus.ID = element.Id.ToString();
                    if (level == string.Empty || level == null)
                    {
                        listCus.Add(cus);
                    }
                    else
                    {
                        if (cus.BaseLevel == level || cus.Level == level || cus.ReferenceLevel == level || cus.BaseConstraint == level)
                        {
                            listCus.Add(cus);
                        }
                    }
                }
            }
            catch { }
            return listCus;
        }
        #endregion

        #endregion
        private List<CategoryName> GetnameCategory(List<Category> list)
        {
            List<CategoryName> listcus = new List<CategoryName>();
            foreach (var cate in list)
            {
                CategoryName catename = new CategoryName();
                catename.CategoryNames = cate.Name;
                listcus.Add(catename);
            }
            listcus = listcus.OrderBy(x => x.CategoryNames).ToList();
            return listcus;
        }

        //private Excel._Workbook GetWorkbook()
        //{
        //    return workbook;
        //}

        private void button1_Click(object sender, EventArgs e, Excel._Workbook wb)
        {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application  
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            // creating new Excelsheet in workbook  
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            // see the excel sheet behind the program  
            app.Visible = true;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            // changing the name of active sheet  
            worksheet.Name = "Exported";
            // storing header part in Excel  
            for (int i = 1; i < grv_element.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = grv_element.Columns[i - 1].HeaderText;
            }
            // storing Each row and column value to excel sheet   
            for (int i = 0; i < grv_element.Rows.Count; i++)
            {
                for (int j = 0; j < grv_element.Columns.Count; j++)
                {
                    if (grv_element.Rows[i].Cells[j].Value == null || grv_element.Rows[i].Cells[j] == null || grv_element.Rows[i].Cells[j].Value.ToString() == "")
                    {
                        grv_element.Rows[i].Cells[j].Value = " ";
                        worksheet.Cells[i + 2, j + 1] = " ";
                    }
                    else
                        worksheet.Cells[i + 2, j + 1] = grv_element.Rows[i].Cells[j].Value.ToString();
                }
            }
        }
        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (checkbox_check.Checked == true)
            {
                level = cb_level.SelectedItem.ToString();
            }
            else
                level = string.Empty;
            ResetData();
        }
        private void AddvaluetoCategory(String rowvalue, List<Element> listelement, List<Element> listelementreverse)
        {
            #region: MEP
            if (rowvalue == "Pipes")
            {
                grv_element.DataSource = TransferPipes(listelement);
            }
            else if (rowvalue == "Conduit Fittings")
            {
                grv_element.DataSource = TransferConduitFittings(listelement);
            }
            else if (rowvalue == "Mechanical Equipment")
            {
                grv_element.DataSource = TransferMechanicalEquipment(listelement);
            }
            else if (rowvalue == "Pipe Insulations")
            {
                grv_element.DataSource = TransferPipeInsulations(listelement);
            }
            else if (rowvalue == "Duct Insulations")
            {
                grv_element.DataSource = TransferDuctInsulations(listelement);
            }
            else if (rowvalue == "Air Terminals")
            {
                grv_element.DataSource = TransferAirTerminals(listelement);
            }
            else if (rowvalue == "Plumbing Fixtures")
            {
                grv_element.DataSource = TransferPipPlumbingFixtures(listelement);
            }
            else if (rowvalue == "Pipe Accessories")
            {
                grv_element.DataSource = TransferPipeAccessories(listelement);
            }
            else if (rowvalue == "Duct Accessories")
            {
                grv_element.DataSource = TransferDuctAccessories(listelement);
            }
            else if (rowvalue == "Fire Alarm Devices")
            {
                grv_element.DataSource = TransferFireAlarmDevices(listelement);
            }
            else if (rowvalue == "Electrical Equipment")
            {
                grv_element.DataSource = TransferElectricalEquipment(listelement);
            }
            else if (rowvalue == "Sprinklers")
            {
                grv_element.DataSource = TransferPipSprinklers(listelement);
            }
            else if (rowvalue == "Pipe Fittings")
            {
                grv_element.DataSource = TransferPipeFittings(listelement);
            }
            else if (rowvalue == "Ducts")
            {
                grv_element.DataSource = TransferDucts(listelement);
            }
            else if (rowvalue == "Duct Fittings")
            {
                grv_element.DataSource = TransferDuctFittings(listelement);
            }
            else if (rowvalue == "Cable Trays")
            {
                grv_element.DataSource = TransferCableTrays(listelement);
            }
            else if (rowvalue == "Flex Ducts")
            {
                grv_element.DataSource = TransferFlexDucts(listelement);
            }
            else if (rowvalue == "Conduits")
            {
                grv_element.DataSource = TransferConduits(listelement);
            }
            #endregion
            #region: Structural
            else if (rowvalue == "Structural Framing")
            {
                if (TransferStructuralFraming(listelement).Count == 0)
                {
                    grv_element.DataSource = TransferStructuralFraming(listelementreverse);
                }
                else grv_element.DataSource = TransferStructuralFraming(listelement);
            }
            else if (rowvalue == "Structural Columns")
            {
                if (TransferStructuralColumn(listelement).Count == 0)
                {
                    grv_element.DataSource = TransferStructuralColumn(listelementreverse);
                }
                else grv_element.DataSource = TransferStructuralColumn(listelement);
            }
            else if (rowvalue == "Walls")
            {
                if (TransferWall(listelement).Count == 0)
                {
                    grv_element.DataSource = TransferWall(listelementreverse);
                }
                else grv_element.DataSource = TransferWall(listelement);
            }
            else if (rowvalue == "Floors")
            {
                if (TransferFLoors(listelement).Count == 0)
                {
                    grv_element.DataSource = TransferFLoors(listelementreverse);
                }
                else grv_element.DataSource = TransferFLoors(listelement);
            }
            else if (rowvalue == "Structural Foundations")
            {
                if (TransferStructuralFoundations(listelement).Count == 0)
                {
                    grv_element.DataSource = TransferStructuralFoundations(listelementreverse);
                }
                else grv_element.DataSource = TransferStructuralFoundations(listelement);
            }
            #endregion
            #region: Architec
            else if (rowvalue == "Columns")
            {
                if (TransferColumns(listelement).Count == 0)
                {
                    grv_element.DataSource = TransferColumns(listelementreverse);
                }
                else grv_element.DataSource = TransferColumns(listelement);
            }

            else if (rowvalue == "Ramps")
            {
                if (TransferColumns(listelement).Count == 0)
                {
                    grv_element.DataSource = TransferRamps(listelementreverse);
                }
                else grv_element.DataSource = TransferRamps(listelement);
            }

            else if (rowvalue == "Doors")
            {
                if (TransferDoors(listelement).Count == 0)
                {
                    grv_element.DataSource = TransferDoors(listelementreverse);
                }
                else grv_element.DataSource = TransferDoors(listelement);
            }
            else if (rowvalue == "Ceilings")
            {
                if (TransferCeiling(listelement).Count == 0)
                {
                    grv_element.DataSource = TransferCeiling(listelementreverse);
                }
                else grv_element.DataSource = TransferCeiling(listelement);
            }
            else if (rowvalue == "Stairs")
            {
                if (TransferStairs(listelement).Count == 0)
                {
                    grv_element.DataSource = TransferStairs(listelementreverse);
                }
                else grv_element.DataSource = TransferStairs(listelement);
            }

            else if (rowvalue == "Windows")
            {
                if (TransferWindows(listelement).Count == 0)
                {
                    grv_element.DataSource = TransferWindows(listelementreverse);
                }
                else grv_element.DataSource = TransferWindows(listelement);
            }
            else if (rowvalue == "Roofs")
            {
                if (TransferRoof(listelement).Count == 0)
                {
                    grv_element.DataSource = TransferRoof(listelementreverse);
                }
                else grv_element.DataSource = TransferRoof(listelement);
            }
            #endregion

        }
        private void ResetData()
        {
            grv_element.Columns.Clear();
            if (grv_element.Columns.Count > 0)
            {
                for (int i = 0; i < grv_element.Columns.Count; i++)
                {
                    grv_element.Columns.RemoveAt(i);
                }
            }
            grv_element.DataSource = null;
            grv_element.Refresh();
            List<Element> listelement = new List<Element>();
            List<Element> listelementreverse = new List<Element>();
            List<Element> listelementlink = new List<Element>();
            string rowvalue = grv_category.CurrentRow.Cells[0].Value.ToString();
            List<Element> listelementproject = TransferStringtoBuiltInCate(doc, rowvalue);
            if (listelementproject == null)
            {
                listelement = listelementlink;
            }
            else if (listelementlink == null)
            {
                listelement = listelementproject;
            }
            else if (listelementlink != null && listelementproject != null)
            {
                listelement = listelementlink.Concat(listelementproject).ToList();
                listelementreverse = listelementproject.Concat(listelementlink).ToList();
            }
            AddvaluetoCategory(rowvalue, listelement, listelementproject);
        }

        private void ResetDataToFind()
        {
            grv_element.Columns.Clear();
            if (grv_element.Columns.Count > 0)
            {
                for (int i = 0; i < grv_element.Columns.Count; i++)
                {
                    grv_element.Columns.RemoveAt(i);
                }
            }
            grv_element.DataSource = null;
            grv_element.Refresh();
            List<Element> listelement = new List<Element>();
            List<Element> listelementreverse = new List<Element>();
            List<Element> listelementlink = new List<Element>();
            string rowvalue = grv_category.CurrentRow.Cells[0].Value.ToString();

            List<Element> listelementproject = TransferStringtoBuiltInCate(doc, rowvalue);
            if (listelementproject == null)
            {
                listelement = listelementlink;
            }
            else if (listelementlink == null)
            {
                listelement = listelementproject;
            }
            else if (listelementlink != null && listelementproject != null)
            {
                listelement = listelementlink.Concat(listelementproject).ToList();
                listelementreverse = listelementproject.Concat(listelementlink).ToList();
            }
            AddvaluetoCategory(rowvalue, listelement, listelementproject);
        }
        private List<Element> TransferStringtoBuiltInCate(Document doc, string str)
        {
            List<Element> listchosen = new List<Element>();
            List<Element> listel = AllElement(doc);

            try
            {
                string familytransfer = string.Empty;
                string typetransfer = string.Empty;
                foreach (var item in listel)
                {
                    Category cat = item.Category;
                    if (cat.Name == str)
                    {
                        listchosen.Add(item);
                    }
                }
            }
            catch { }
            return listchosen;
        }
        private List<Element> TransferStringtoBuiltInCateFind(Document doc, string cate)
        {
            List<Element> listchosen = new List<Element>();
            try
            {
                List<string> liststring = GetlistIDCurrentGridview();
                List<Element> listele = new List<Element>();
                string familytransfer = string.Empty;
                string typetransfer = string.Empty;

                foreach (string item in liststring)
                {
                    int idInt1 = Convert.ToInt32(item);
                    ElementId id1 = new ElementId(idInt1);

                    Element ele = doc.GetElement(id1);
                    string code = GetCode1toCode6(ele);
                    Category cat = ele.Category;
                    string familyname = CheckParameter(ele, "Family and Type");
                    GetFamilyandType(familyname, out familytransfer, out typetransfer);
                    if (cat.Name == cate)
                    {
                        try
                        {
                            if (cb_family.SelectedItem.ToString() == familytransfer && cb_code1tocode6.SelectedItem == null)
                            {
                                listchosen.Add(ele);
                            }
                            else if (cb_family.SelectedItem == null && cb_code1tocode6.SelectedItem.ToString() == code)
                            {
                                //MessageBox.Show("1");
                                listchosen.Add(ele);
                            }
                            else if (cb_family.SelectedItem.ToString() == familytransfer && cb_code1tocode6.SelectedItem.ToString() == GetCode1toCode6(ele))
                            {
                                listchosen.Add(ele);
                            }
                            else if (cb_family.Text == string.Empty && cb_code1tocode6.Text == string.Empty)
                            {
                                MessageBox.Show("Please choose the value for Family or Code1ToCode6", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        catch { }
                    }

                }
            }
            catch { }
            return listchosen;
        }
        private List<Element> TransferStringtoBuiltInCateMark(Document doc)
        {
            //MessageBox.Show(cb_family.Text);
            List<Element> listchosen = new List<Element>();
            try
            {
                List<string> liststring = GetlistIDCurrentGridview();
                List<Element> listele = new List<Element>();
                string familytransfer = string.Empty;
                string typetransfer = string.Empty;

                foreach (string item in liststring)
                {
                    int idInt1 = Convert.ToInt32(item);
                    ElementId id1 = new ElementId(idInt1);

                    Element ele = doc.GetElement(id1);
                    string code = GetCode1toCode6(ele);
                    Category cat = ele.Category;
                    string familyname = CheckParameter(ele, "Family and Type");
                    GetFamilyandType(familyname, out familytransfer, out typetransfer);
                    try
                    {
                        if (cb_family.SelectedItem.ToString() == familytransfer && cb_code1tocode6.SelectedItem == null)
                        {
                            listchosen.Add(ele);
                        }
                        else if (cb_family.SelectedItem == null && cb_code1tocode6.SelectedItem.ToString() == code)
                        {
                            //MessageBox.Show("1");
                            listchosen.Add(ele);
                        }
                        else if (cb_family.SelectedItem.ToString() == familytransfer && cb_code1tocode6.SelectedItem.ToString() == GetCode1toCode6(ele))
                        {
                            listchosen.Add(ele);
                        }
                        else if (cb_family.Text == string.Empty && cb_code1tocode6.Text == string.Empty)
                        {
                            MessageBox.Show("Please choose the value for Family or Code1ToCode6", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch { }
                }
            }
            catch { }
            return listchosen;
        }
        private List<Element> TransferStringtoBuiltInCateFindmistake(Document doc)
        {
            List<Element> listlastele = new List<Element>();
            try
            {
                List<string> liststring = GetlistIDCurrentGridview();
                List<Element> listele = new List<Element>();
                foreach (string item in liststring)
                {
                    int idInt1 = Convert.ToInt32(item);
                    ElementId id1 = new ElementId(idInt1);

                    Element ele = doc.GetElement(id1);
                    listele.Add(ele);
                    Category cat = ele.Category;

                    for (int i = 0; i < listele.Count; i++)
                    {
                        string familytransferi = string.Empty;
                        string typetransferi = string.Empty;
                        string familynamei = CheckParameter(listele[i], "Family and Type");
                        GetFamilyandType(familynamei, out familytransferi, out typetransferi);
                        string codei = GetCode1toCode6(listele[i]);
                        for (int j = 0; j < listele.Count; j++)
                        {
                            string familytransferj = string.Empty;
                            string typetransferj = string.Empty;
                            string familynamej = CheckParameter(listele[j], "Family and Type");
                            GetFamilyandType(familynamej, out familytransferj, out typetransferj);
                            string codej = GetCode1toCode6(listele[j]);
                            if (typetransferi != typetransferj && familytransferi == familytransferj && codei == codej && listele[i].Id != listele[j].Id && codej != string.Empty)
                            {
                                listlastele.Add(listele[j]);
                            }
                        }
                    }
                }
            }
            catch { }
            for (int i = 0; i < listlastele.Count; i++)
            {
                string familytransferi = string.Empty;
                string typetransferi = string.Empty;
                string familynamei = CheckParameter(listlastele[i], "Family and Type");
                GetFamilyandType(familynamei, out familytransferi, out typetransferi);
                for (int j = listlastele.Count - 1; j > i; j--)
                {
                    string familytransferj = string.Empty;
                    string typetransferj = string.Empty;
                    string familynamej = CheckParameter(listlastele[j], "Family and Type");
                    GetFamilyandType(familynamej, out familytransferj, out typetransferj);
                    if (listlastele[i].Id == listlastele[j].Id || typetransferi == typetransferj)
                    {
                        listlastele.RemoveAt(j);
                    }
                }
            }
            return listlastele;
        }
        private List<Element> TransferStringtoBuiltInCateFindmistakeSameValue(Document doc)
        {
            List<Element> listlastele = new List<Element>();
            try
            {
                List<string> liststring = GetlistIDCurrentGridview();
                List<Element> listele = new List<Element>();
                foreach (string item in liststring)
                {
                    int idInt1 = Convert.ToInt32(item);
                    ElementId id1 = new ElementId(idInt1);

                    Element ele = doc.GetElement(id1);
                    Category cat = ele.Category;
                    string familytransferi = string.Empty;
                    string typetransferi = string.Empty;
                    string familynamei = CheckParameter(ele, "Family and Type");
                    GetFamilyandType(familynamei, out familytransferi, out typetransferi);
                    string codei = GetCode1toCode6(ele);
                    string description = CheckDescription(ele);
                    string number = typetransferi.Substring(typetransferi.LastIndexOf('_') + 1);
                    //TaskDialog.Show("n", "Type"+typetransferi+"Des"+description+"Code"+codei);
                    if (typetransferi.Contains("_"))
                    {
                        if (description != string.Empty)
                        {
                            if (codei.Contains(number))
                            {
                                listlastele.Add(ele);
                            }
                        }
                    }
                }
            }
            catch { }
            return listlastele;
        }
        private List<Element> TransferStringtoBuiltInCateSort(Document doc)
        {
            List<Element> listlastele = new List<Element>();
            try
            {
                List<string> liststring = GetlistIDCurrentGridview();
                List<Element> listele = new List<Element>();
                foreach (string item in liststring)
                {
                    int idInt1 = Convert.ToInt32(item);
                    ElementId id1 = new ElementId(idInt1);
                    Element ele = doc.GetElement(id1);
                    listele.Add(ele);
                }
                if (rd_atoz.Checked)
                {
                    if (cb_dataheader.SelectedItem.ToString() == "SystemOrLoadable")
                    {
                        listlastele = listele.OrderBy(x => x.LookupParameter("KindOfFamily").AsString()).ToList();
                    }
                    else if (cb_dataheader.SelectedItem.ToString() == "Family")
                    {
                        listlastele = listele.OrderBy(x =>
                        {
                            string familytransferi = string.Empty;
                            string typetransferi = string.Empty;
                            string familynamei = CheckParameter(x, "Family and Type");
                            GetFamilyandType(familynamei, out familytransferi, out typetransferi);

                            return familytransferi;
                        }
                        ).ToList();
                    }
                    else if (cb_dataheader.SelectedItem.ToString() == "AssemblyCode")
                    {
                        listlastele = listele.OrderBy(x =>
                        {
                            string assemblycode = string.Empty;
                            string assemblydescription = string.Empty;
                            string keynote = string.Empty;
                            GetAssemblyandKeynote(x, out assemblycode, out assemblydescription, out keynote);
                            return assemblycode;
                        }
                        ).ToList();
                    }
                    else if (cb_dataheader.SelectedItem.ToString() == "AssemblyDescription")
                    {
                        listlastele = listele.OrderBy(x =>
                        {
                            string assemblycode = string.Empty;
                            string assemblydescription = string.Empty;
                            string keynote = string.Empty;
                            GetAssemblyandKeynote(x, out assemblycode, out assemblydescription, out keynote);
                            return assemblydescription;
                        }
                        ).ToList();
                    }
                    else if (cb_dataheader.SelectedItem.ToString() == "Type")
                    {
                        listlastele = listele.OrderBy(x =>
                        {
                            string familytransferi = string.Empty;
                            string typetransferi = string.Empty;
                            string familynamei = CheckParameter(x, "Family and Type");
                            GetFamilyandType(familynamei, out familytransferi, out typetransferi);

                            return typetransferi;
                        }
                        ).ToList();
                    }
                    else if (cb_dataheader.SelectedItem.ToString() == "KeyNote")
                    {
                        listlastele = listele.OrderBy(x =>
                        {
                            string assemblycode = string.Empty;
                            string assemblydescription = string.Empty;
                            string keynote = string.Empty;
                            GetAssemblyandKeynote(x, out assemblycode, out assemblydescription, out keynote);
                            return keynote;
                        }
                        ).ToList();
                    }
                    else if (cb_dataheader.SelectedItem.ToString() == "Description")
                    {
                        listlastele = listele.OrderBy(x =>
                        {
                            string des = CheckDescription(x);
                            return des;
                        }
                        ).ToList();
                    }
                    else if (cb_dataheader.SelectedItem.ToString() == "Code1ToCode6")
                    {
                        listlastele = listele.OrderBy(x =>
                        {
                            string des = GetCode1toCode6(x);
                            return des;
                        }
                        ).ToList();
                    }
                    else if (cb_dataheader.SelectedItem.ToString() == "ID")
                    {
                        listlastele = listele.OrderBy(x => x.Id.ToString()).ToList();
                    }
                }
                else if (rd_ztoa.Checked)
                {
                    if (cb_dataheader.SelectedItem.ToString() == "SystemOrLoadable")
                    {
                        listlastele = listele.OrderByDescending(x => x.LookupParameter("KindOfFamily").AsString()).ToList();
                    }
                    else if (cb_dataheader.SelectedItem.ToString() == "Family")
                    {
                        listlastele = listele.OrderByDescending(x =>
                        {
                            string familytransferi = string.Empty;
                            string typetransferi = string.Empty;
                            string familynamei = CheckParameter(x, "Family and Type");
                            GetFamilyandType(familynamei, out familytransferi, out typetransferi);

                            return familytransferi;
                        }
                        ).ToList();
                    }
                    else if (cb_dataheader.SelectedItem.ToString() == "AssemblyCode")
                    {
                        listlastele = listele.OrderByDescending(x =>
                        {
                            string assemblycode = string.Empty;
                            string assemblydescription = string.Empty;
                            string keynote = string.Empty;
                            GetAssemblyandKeynote(x, out assemblycode, out assemblydescription, out keynote);
                            return assemblycode;
                        }
                        ).ToList();
                    }
                    else if (cb_dataheader.SelectedItem.ToString() == "AssemblyDescription")
                    {
                        listlastele = listele.OrderByDescending(x =>
                        {
                            string assemblycode = string.Empty;
                            string assemblydescription = string.Empty;
                            string keynote = string.Empty;
                            GetAssemblyandKeynote(x, out assemblycode, out assemblydescription, out keynote);
                            return assemblydescription;
                        }
                        ).ToList();
                    }
                    else if (cb_dataheader.SelectedItem.ToString() == "Type")
                    {
                        listlastele = listele.OrderByDescending(x =>
                        {
                            string familytransferi = string.Empty;
                            string typetransferi = string.Empty;
                            string familynamei = CheckParameter(x, "Family and Type");
                            GetFamilyandType(familynamei, out familytransferi, out typetransferi);

                            return typetransferi;
                        }
                        ).ToList();
                    }
                    else if (cb_dataheader.SelectedItem.ToString() == "KeyNote")
                    {
                        listlastele = listele.OrderByDescending(x =>
                        {
                            string assemblycode = string.Empty;
                            string assemblydescription = string.Empty;
                            string keynote = string.Empty;
                            GetAssemblyandKeynote(x, out assemblycode, out assemblydescription, out keynote);
                            return keynote;
                        }
                        ).ToList();
                    }
                    else if (cb_dataheader.SelectedItem.ToString() == "Description")
                    {
                        listlastele = listele.OrderByDescending(x =>
                        {
                            string des = CheckDescription(x);
                            return des;
                        }
                        ).ToList();
                    }
                    else if (cb_dataheader.SelectedItem.ToString() == "Code1ToCode6")
                    {
                        listlastele = listele.OrderByDescending(x =>
                        {
                            string des = GetCode1toCode6(x);
                            return des;
                        }
                        ).ToList();
                    }
                    else if (cb_dataheader.SelectedItem.ToString() == "ID")
                    {
                        listlastele = listele.OrderByDescending(x => x.Id.ToString()).ToList();
                    }
                }
            }
            catch { }
            return listlastele;
        }
        private List<Element> TransferStringtoLoadElements(Document doc)
        {
            List<string> liststring = new List<string>();
            List<Element> listelement = new List<Element>();
            var listItem = lv_savemark.CheckedItems;
            foreach (ListViewItem item in listItem)
            {
                liststring.Add(item.Text);
            }

            foreach (var item in liststring)
            {
                string save = nguon + cb_project.SelectedItem.ToString() + @"\" + item + ".txt";
                string[] st = File.ReadAllLines(save);
                foreach (var iditeam in st)
                {
                    try
                    {
                        iditeam.Trim(' ');
                        int idInt = Convert.ToInt32(iditeam);
                        ElementId id = new ElementId(idInt);
                        Element eFromId = doc.GetElement(id);
                        listelement.Add(eFromId);
                    }
                    catch { };
                }
            }
            for (int i = 0; i < listelement.Count; i++)
            {
                for (int j = listelement.Count - 1; j > i; j--)
                {
                    if (listelement[j].Id == listelement[i].Id)
                    {
                        listelement.RemoveAt(j);
                    }
                }
            }
            return listelement;
        }
        private List<string> GetlistIDCurrentGridview()
        {
            int clID = 0;
            for (int j = 0; j < grv_element.ColumnCount; j++)
            {
                string header = grv_element.Columns[j].HeaderText;
                if (header == "ID")
                {
                    clID = j;
                }
            }
            List<string> liststring = new List<string>();
            for (int i = 0; i < grv_element.Rows.Count; i++)
            {
                string rowvalue = grv_element.Rows[i].Cells[clID].Value.ToString();
                if (rowvalue != null)
                {
                    liststring.Add(rowvalue);
                }
            }
            return liststring;
        }
        public static BuiltInCategory ToBuiltinCategory(Category cat)
        {
            BuiltInCategory result = BuiltInCategory.INVALID;
            try
            {
                result = (BuiltInCategory)Enum.Parse(typeof(BuiltInCategory), ((object)cat.Id).ToString());
                return result;
            }
            catch
            {
                return result;
            }
        }

        private BuiltInCategory TransferStringtoBuiltInCate(Document doc)
        {
            BuiltInCategory result = BuiltInCategory.INVALID;


            List<Element> listel = AllElement(doc);
            try
            {
                foreach (var item in listel)
                {
                    Category cat = item.Category;
                    result = ToBuiltinCategory(cat);
                }
            }
            catch { }
            return result;
        }
        private List<Element> ListelementInCate(Document doc)
        {
            List<Element> listchosen = new List<Element>();
            List<Element> listel = AllElement(doc);
            List<Category> listcate = GetlistCategoryinViewAllversion(doc);
            try
            {
                foreach (var cate in listcate)
                {
                    foreach (var item in listel)
                    {
                        Category cat = item.Category;
                        if (cat.Name == cate.Name)
                        {
                            listchosen.Add(item);
                        }
                    }
                }
            }
            catch { }
            return listchosen;
        }
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public static List<Category> GetlistCategoryinViewAllversion(Document doc)
        {
            List<Element> elements = new List<Element>();
            List<Category> listcategory = new List<Category>();
            FilteredElementCollector collector = new FilteredElementCollector(doc).WhereElementIsNotElementType();

            foreach (Element e in collector)
            {
                Options opt = new Options();

                if (null != e.Category && e.Category.Name != "Cameras" && e.Category.Name != "Center line" &&
                    e.Category.Name != "Center Line" && e.Category.Name.Contains("Analytical") == false
                    && e.Category.Name != "Duct Systems" && e.Category.Name != "Piping Systems" && e.Category.Name != "<Room Separation>"
                    && e.Category.Name != "Lines" && e.Category.Name != "Multistory Stairs" && e.Category.Name != "Shaft Openings"
                    && e.Category.Name != "Section Boxes" && e.Category.Name != "<Sketch>" && e.Category.Name != "Area Schemes"
                    && e.Category.Name != "Building Type Settings" && e.Category.Name != "Color Fill Schema"
                    && e.Category.Name != "Dimensions" && e.Category.Name != "Electrical Demand Factor Definitions"
                    && e.Category.Name != "Electrical Load Classification Parameter Element" && e.Category.Name != "Electrical Load Classifications"
                    && e.Category.Name != "Elevations" && e.Category.Name != "Grids" && e.Category.Name != "HVAC Load Schedules"
                    && e.Category.Name != "Internal Origin" && e.Category.Name != "Legend Components" && e.Category.Name != "Levels"
                    && e.Category.Name != "Material Assets" && e.Category.Name != "Materials" && e.Category.Name != "Panel Schedule Templates - Branch Panel"
                    && e.Category.Name != "Panel Schedule Templates - Data Panel" && e.Category.Name != "Panel Schedule Templates - Switchboard"
                    && e.Category.Name != "Phases" && e.Category.Name != "Primary Contours" && e.Category.Name != "Project Base Point"
                    && e.Category.Name != "Project Information" && e.Category.Name != "Revision" && e.Category.Name != "RVT Links"
                    && e.Category.Name != "Shared Site" && e.Category.Name != "Space Type Settings" && e.Category.Name != "Structural Load Cases"
                    && e.Category.Name != "Sun Path" && e.Category.Name != "Survey Point" && e.Category.Name != "Work Plane Grid"
                    && e.Category.Name != "Views" && e.Category.Name != "Pipe Segments" && e.Category.Name != "<Area Boundary>"
                    && e.Category.Name != "HVAC Zones" && e.Category.Name != "Schedule Graphics" && e.Category.Name != "Schedules"
                    && e.Category.Name != "Sheets" && e.Category.Name != "Text Notes" && e.Category.Name != "Title Blocks"
                    && e.Category.Name != "Viewports" && e.Category.Name != "Automatic Sketch Dimensions" && e.Category.Name != "Balusters"
                    && e.Category.Name != "Design Option Sets" && e.Category.Name != "Design Options" && e.Category.Name != "Detail Items"
                    && e.Category.Name != "Multi-Rebar Annotations" && e.Category.Name != "Railing Rail Path Extension Lines"
                    && e.Category.Name != "Reference Planes" && e.Category.Name != "Revision Clouds" && e.Category.Name != "Spot Elevations"
                    && e.Category.Name != "Spot Slopes" && e.Category.Name != "Stair Paths" && e.Category.Name != "Structural Area Reinforcement"
                    && (e.Category.Name.Contains("Tag") == false) && e.Category.Name != "Top Rails"
                    && e.Category.Name != "Structural Rebar"
                    && (e.Category.Name.Contains(".dwg") == false) && (e.Category.Name.Contains("Grid") == false)
                    && e.Category.Name != "Constraints" && e.Category.Name != "Model Groups" && (e.Category.Name.Contains("Sketch") == false
                    && e.Category.Name != "Plan Region" && e.Category.Name != "Curtain Panels" && e.Category.Name != "Curtain Wall Mullions"
                    && e.Category.Name != "Generic Models" && e.Category.Name != "Curtain Systems" && e.Category.Name != "Raster Images"
                    && e.Category.Name != "Scope Boxes"))
                {
                    elements.Add(e);
                }
            }
            foreach (var el in elements)
            {
                if (el.Category != null)
                {
                    listcategory.Add(el.Category);
                }
            }
            for (int i = 0; i < listcategory.Count; i++)
            {
                for (int j = listcategory.Count - 1; j > i; j--)
                {
                    if (listcategory[j].Name == listcategory[i].Name)
                    {
                        listcategory.RemoveAt(j);
                    }
                }
            }
            return listcategory;
        }

        private void btn_Options_Click(object sender, EventArgs e)
        {
            using (frmGetValuePara frm = new frmGetValuePara(uiapp, doc))
            {
                frm.ShowDialog();
            }
        }
        private void ZoomtoElement(UIApplication uiapp, ICollection<ElementId> eleid)
        {
            try
            {
                uiapp.ActiveUIDocument.ShowElements(eleid);
            }

            catch
            {

            }
        }
        string select;
        string cells;
        string updatedata = @"C:\\E38EDM\Update\updatedata.txt";
        private void grv_element_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            select = grv_element.CurrentRow.Cells[grv_element.Columns.Count - 1].Value.ToString();
        }

        private void btn_Zoomto_Click(object sender, EventArgs e)
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            ICollection<ElementId> listeleid = new List<ElementId>();
            int idInt1 = Convert.ToInt32(select);
            ElementId id1 = new ElementId(idInt1);
            if (id1 != null)
            {
                listeleid.Add(id1);
            }
            uidoc.Selection.SetElementIds(listeleid);
            ZoomtoElement(uiapp, listeleid);
        }

        private void btn_searchall_Click(object sender, EventArgs e)
        {
            grv_element.Columns.Clear();
            if (grv_element.Columns.Count > 0)
            {
                for (int i = 0; i < grv_element.Columns.Count; i++)
                {
                    grv_element.Columns.RemoveAt(i);
                }
            }
            grv_element.DataSource = null;
            grv_element.Refresh();
            List<Element> listelement = new List<Element>();
            List<Element> listelementreverse = new List<Element>();
            List<Element> listelementlink = new List<Element>();

            List<Element> listelementproject = ListelementInCate(doc);

            if (listelementproject == null)
            {
                listelement = listelementlink;
            }
            else if (listelementlink == null)
            {
                listelement = listelementproject;
            }
            else if (listelementlink != null && listelementproject != null)
            {
                listelement = listelementlink.Concat(listelementproject).ToList();
                listelementreverse = listelementproject.Concat(listelementlink).ToList();
            }
            if (TransferAllElements(listelement).Count == 0)
            {
                grv_element.DataSource = TransferAllElements(listelementreverse);
            }
            else grv_element.DataSource = TransferAllElements(listelement);
        }

        private void cb_level_SelectedValueChanged(object sender, EventArgs e)
        {
            level = cb_level.SelectedItem.ToString();
        }

        private void checkbox_check_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbox_check.Checked == true)
            {
                cb_level.Enabled = true;
            }
            else
            {
                cb_level.Text = string.Empty;
                level = string.Empty;
                cb_level.Enabled = false;
            }
        }

        private void grv_element_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string value = grv_element.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            string headerRowText = grv_element.Rows[0].Cells[e.ColumnIndex].ColumnIndex.ToString();
            //MessageBox.Show(value);
            StreamWriter swmetricafter = new StreamWriter(updatedata, true);
            swmetricafter.WriteLine(value + "/" + select + "/" + headerRowText);
            swmetricafter.Close();
        }
        private int GetIntColumnByHeader(string header)
        {
            int clID = 0;
            for (int j = 0; j < grv_element.ColumnCount; j++)
            {
                if (grv_element.Columns[j].HeaderText == header)
                {
                    clID = j;
                }
            }
            return clID;
        }
        private void UpdateData()
        {
            string[] lines = File.ReadAllLines(updatedata);
            string value = string.Empty;
            string eleid = string.Empty;
            string clnumber = string.Empty;
            if (lines.Count() > 0)
            {
                foreach (string line in lines)
                {
                    GetData(line, out value, out eleid, out clnumber);
                    int inclnumber = Convert.ToInt32(clnumber);
                    string header = grv_element.Columns[inclnumber].HeaderText;
                    int idInt1 = Convert.ToInt32(eleid);
                    ElementId id1 = new ElementId(idInt1);
                    Element ele = doc.GetElement(id1);
                    if (header == "AssemblyCode")
                    {
                        FixAssemblyCode(ele, value);
                    }
                    else if (header == "Type")
                    {
                        FixTypename(ele, value);
                    }
                    else if (header == "KeyNote")
                    {
                        FixKeyNote(ele, value);
                    }
                    else if (header == "Description")
                    {
                        FixDescription(ele, value);
                    }
                }
                MessageBox.Show("Update Successfully", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DialogResult result = MessageBox.Show("Update Data", "Do you want to Update all Data?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    int ClID = GetIntColumnByHeader("ID");
                    int ClAssemblyCode = GetIntColumnByHeader("AssemblyCode");
                    int ClType = GetIntColumnByHeader("Type");
                    int ClKeyNote = GetIntColumnByHeader("KeyNote");
                    int ClDescription = GetIntColumnByHeader("Description");

                    for (int i = 0; i < grv_element.Rows.Count; i++)
                    {
                        int inclnumber = Convert.ToInt32(grv_element.Rows[i].Cells[ClID].Value);
                        ElementId id1 = new ElementId(inclnumber);
                        Element ele = doc.GetElement(id1);
                        try
                        {
                            FixAssemblyCode(ele, string.Empty);
                            FixTypename(ele, string.Empty);
                            FixKeyNote(ele, string.Empty);
                            FixDescription(ele, string.Empty);

                            FixAssemblyCode(ele, grv_element.Rows[i].Cells[ClAssemblyCode].Value.ToString());
                            FixTypename(ele, grv_element.Rows[i].Cells[ClType].Value.ToString());
                            FixKeyNote(ele, grv_element.Rows[i].Cells[ClKeyNote].Value.ToString());
                            FixDescription(ele, grv_element.Rows[i].Cells[ClDescription].Value.ToString());


                            if (grv_define.Rows.Count > 0)
                            {
                                for (int j = 0; j < grv_define.Rows.Count; j++)
                                {
                                    string headeradd = grv_define.Rows[j].Cells[grv_define.Columns.Count - 1].Value.ToString();
                                    int Cladd = GetIntColumnByHeader(headeradd);
                                    FixNewParameter(ele, headeradd, grv_element.Rows[i].Cells[Cladd].Value.ToString());
                                }
                            }
                        }
                        catch { }
                    }
                    MessageBox.Show("Update Successfully", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void FixAssemblyCode(Element ele, string fix)
        {
            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Transaction Name");
                try
                {
                    if (ele.Category.Name == "Floors" && ele is Floor)
                    {
                        Floor flo = ele as Floor;
                        Parameter paraassemblycode = flo.FloorType.LookupParameter("Assembly Code");
                        paraassemblycode.Set(fix);
                    }
                    else
                    {
                        Element element = doc.GetElement(ele.GetTypeId());
                        Parameter paraassemblycode = element.LookupParameter("Assembly Code");
                        paraassemblycode.Set(fix);
                    }
                }
                catch { }
                tx.Commit();
            }

        }
        private void FixKeyNote(Element ele, string fix)
        {
            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Transaction Name");
                if (ele.Category.Name == "Floors" && ele is Floor)
                {
                    Floor flo = ele as Floor;
                    Parameter paraassemblycode = flo.FloorType.LookupParameter("Keynote");
                    paraassemblycode.Set(fix);
                }
                else
                {
                    Element element = doc.GetElement(ele.GetTypeId());
                    Parameter paraassemblycode = element.LookupParameter("Keynote");
                    paraassemblycode.Set(fix);
                }
                tx.Commit();
            }

        }
        private void FixDescription(Element ele, string fix)
        {
            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Transaction Name");
                if (ele.Category.Name == "Floors" && ele is Floor)
                {
                    Floor flo = ele as Floor;
                    Parameter paraassemblycode = flo.FloorType.LookupParameter("Description");
                    paraassemblycode.Set(fix);
                }
                else
                {
                    Element element = doc.GetElement(ele.GetTypeId());
                    Parameter paraassemblycode = element.LookupParameter("Description");
                    paraassemblycode.Set(fix);
                }
                tx.Commit();
            }

        }
        private void FixTypename(Element ele, string fix)
        {
            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Transaction Name");
                ElementId id = ele.GetTypeId();
                ElementType elemess = doc.GetElement(id) as ElementType;
                elemess.Name = fix;
                tx.Commit();
            }

        }
        private void GetData(string origin, out string value, out string eleid, out string clnumber)
        {
            value = string.Empty;
            eleid = string.Empty;
            clnumber = string.Empty;
            value = origin.Substring(0, origin.IndexOf('/'));
            clnumber = origin.Substring(origin.LastIndexOf('/') + 1);
            eleid = origin.Substring(origin.IndexOf('/') + 1, origin.Length - value.Length - clnumber.Length - 2);
        }
        private void FixNewParameter(Element ele, string paraname, string fix)
        {
            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Transaction Name");
                if (ele.Category.Name == "Floors" && ele is Floor)
                {
                    Floor flo = ele as Floor;
                    Parameter paraassemblycode = flo.FloorType.LookupParameter(paraname);
                    paraassemblycode.Set(fix);
                }
                else
                {
                    try
                    {
                        Parameter paraassemblycode2 = ele.LookupParameter(paraname);
                        if (paraassemblycode2 != null)
                        {
                            paraassemblycode2.Set(fix);
                        }
                        else
                        {
                            Element element = doc.GetElement(ele.GetTypeId());
                            Parameter paraassemblycode1 = element.LookupParameter(paraname);
                            paraassemblycode1.Set(fix);
                        }
                    }
                    catch
                    {

                    }
                }
                tx.Commit();
            }

        }
        private void btn_updatedata_Click(object sender, EventArgs e)
        {
            UpdateData();
            if (grv_element.ColumnCount == 15)
            {
                ResetDataToMark();
            }
            else if (grv_element.ColumnCount < 15)
            {
                ResetData();
            }


        }

        private void btn_import_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grv_element.Rows.Count; i++)
            {
                string id = grv_element.Rows[i].Cells[grv_element.ColumnCount - 1].Value.ToString();
                if (grv_element.Rows[i].Selected == true && grv_element.Rows[i].DefaultCellStyle.BackColor != System.Drawing.Color.LightPink)
                {
                    grv_element.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.LightPink;
                    tb_showmark.AppendText("Marked Elements ID: " + id + "\r\n");
                }
                else if (grv_element.Rows[i].Selected == true && grv_element.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.LightPink)
                {
                    grv_element.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                    tb_showmark.AppendText("UnMarked Elements ID: " + id + "\r\n");
                }
            }

        }

        private void btn_apply_Click(object sender, EventArgs e)
        {
            Addvaluetocombobox();
            cb_code1tocode6.Enabled = false;
            if (grv_element.ColumnCount == 0)
            {
                MessageBox.Show("No Data To Get", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Get Data Successfully", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_filterserch_Click(object sender, EventArgs e)
        {
            if (grv_element.ColumnCount == 15)
            {
                //ResetDataToMark();
                //grv_element.DataSource = null;
                //grv_element.Refresh();
                List<Element> listelement = new List<Element>();
                List<Element> listelementreverse = new List<Element>();
                List<Element> listelementlink = new List<Element>();

                List<Element> listelementproject = TransferStringtoBuiltInCateMark(doc);

                if (listelementproject == null)
                {
                    listelement = listelementlink;
                }
                else if (listelementlink == null)
                {
                    listelement = listelementproject;
                }
                else if (listelementlink != null && listelementproject != null)
                {
                    listelement = listelementlink.Concat(listelementproject).ToList();
                    listelementreverse = listelementproject.Concat(listelementlink).ToList();
                }
                if (TransferAllElements(listelement).Count == 0)
                {
                    grv_element.DataSource = TransferAllElements(listelementreverse);
                }
                else grv_element.DataSource = TransferAllElements(listelement);
            }
            else if (grv_element.ColumnCount < 15)
            {
                ResetDataToFind();
                List<Element> listelement = new List<Element>();
                List<Element> listelementreverse = new List<Element>();
                List<Element> listelementlink = new List<Element>();
                string rowvalue = grv_category.CurrentRow.Cells[0].Value.ToString();
                List<Element> listelementproject = TransferStringtoBuiltInCateFind(doc, rowvalue);
                if (listelementproject == null)
                {
                    listelement = listelementlink;
                }
                else if (listelementlink == null)
                {
                    listelement = listelementproject;
                }
                else if (listelementlink != null && listelementproject != null)
                {
                    listelement = listelementlink.Concat(listelementproject).ToList();
                    listelementreverse = listelementproject.Concat(listelementlink).ToList();
                }
                AddvaluetoCategory(rowvalue, listelement, listelementproject);
            }
        }

        private void cb_family_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb_code1tocode6.Enabled = true;
        }

        private void btn_findmistake_Click(object sender, EventArgs e)
        {
            if (grv_element.ColumnCount == 15)
            {
                if (chb_sortall.Checked)
                {
                    ResetDataToSearchAll();
                }
                else if (chb_sortall.Checked == false)
                {

                    ResetDataToMark();
                }
                List<Element> listelement = new List<Element>();
                List<Element> listelementreverse = new List<Element>();
                List<Element> listelementlink = new List<Element>();
                foreach (Document doclink in uiapp.Application.Documents)
                {
                    if (doclink.IsLinked)
                    {
                        foreach (var item in TransferStringtoBuiltInCateFindmistake(doclink))
                        {
                            listelementlink.Add(item);
                        }
                    }
                }
                List<Element> listelementproject = TransferStringtoBuiltInCateFindmistake(doc);

                if (listelementproject == null)
                {
                    listelement = listelementlink;
                }
                else if (listelementlink == null)
                {
                    listelement = listelementproject;
                }
                else if (listelementlink != null && listelementproject != null)
                {
                    listelement = listelementlink.Concat(listelementproject).ToList();
                    listelementreverse = listelementproject.Concat(listelementlink).ToList();
                }
                if (TransferAllElements(listelement).Count == 0)
                {
                    grv_element.DataSource = TransferAllElements(listelementreverse);
                }
                else grv_element.DataSource = TransferAllElements(listelement);
            }
            else if (grv_element.ColumnCount < 15)
            {
                ResetDataToFind();
                List<Element> listelement = new List<Element>();
                List<Element> listelementreverse = new List<Element>();
                List<Element> listelementlink = new List<Element>();
                string rowvalue = grv_category.CurrentRow.Cells[0].Value.ToString();

                List<Element> listelementproject = TransferStringtoBuiltInCateFindmistake(doc);
                if (listelementproject == null)
                {
                    listelement = listelementlink;
                }
                else if (listelementlink == null)
                {
                    listelement = listelementproject;
                }
                else if (listelementlink != null && listelementproject != null)
                {
                    listelement = listelementlink.Concat(listelementproject).ToList();
                    listelementreverse = listelementproject.Concat(listelementlink).ToList();
                }
                AddvaluetoCategory(rowvalue, listelement, listelementproject);
            }
        }

        private void btn_findsamevaluetype_Click(object sender, EventArgs e)
        {
            if (grv_element.ColumnCount == 15)
            {
                if (chb_sortall.Checked)
                {
                    ResetDataToSearchAll();
                }
                else if (chb_sortall.Checked == false)
                {

                    ResetDataToMark();
                }
                List<Element> listelement = new List<Element>();
                List<Element> listelementreverse = new List<Element>();
                List<Element> listelementlink = new List<Element>();
                foreach (Document doclink in uiapp.Application.Documents)
                {
                    if (doclink.IsLinked)
                    {
                        foreach (var item in TransferStringtoBuiltInCateFindmistakeSameValue(doclink))
                        {
                            listelementlink.Add(item);
                        }
                    }
                }
                List<Element> listelementproject = TransferStringtoBuiltInCateFindmistakeSameValue(doc);

                if (listelementproject == null)
                {
                    listelement = listelementlink;
                }
                else if (listelementlink == null)
                {
                    listelement = listelementproject;
                }
                else if (listelementlink != null && listelementproject != null)
                {
                    listelement = listelementlink.Concat(listelementproject).ToList();
                    listelementreverse = listelementproject.Concat(listelementlink).ToList();
                }
                if (TransferAllElements(listelement).Count == 0)
                {
                    grv_element.DataSource = TransferAllElements(listelementreverse);
                }
                else grv_element.DataSource = TransferAllElements(listelement);
            }
            else if (grv_element.ColumnCount < 15)
            {
                ResetDataToFind();
                List<Element> listelement = new List<Element>();
                List<Element> listelementreverse = new List<Element>();
                List<Element> listelementlink = new List<Element>();
                string rowvalue = grv_category.CurrentRow.Cells[0].Value.ToString();
                foreach (Document doclink in uiapp.Application.Documents)
                {
                    if (doclink.IsLinked)
                    {
                        foreach (var item in TransferStringtoBuiltInCateFindmistakeSameValue(doclink))
                        {
                            listelementlink.Add(item);
                        }
                    }
                }
                List<Element> listelementproject = TransferStringtoBuiltInCateFindmistakeSameValue(doc);
                if (listelementproject == null)
                {
                    listelement = listelementlink;
                }
                else if (listelementlink == null)
                {
                    listelement = listelementproject;
                }
                else if (listelementlink != null && listelementproject != null)
                {
                    listelement = listelementlink.Concat(listelementproject).ToList();
                    listelementreverse = listelementproject.Concat(listelementlink).ToList();
                }
                AddvaluetoCategory(rowvalue, listelement, listelementproject);
            }
        }

        private void btn_getdataheader_Click(object sender, EventArgs e)
        {
            cb_dataheader.Items.Clear();
            for (int j = 0; j < grv_element.ColumnCount; j++)
            {
                string header = grv_element.Columns[j].HeaderText;
                cb_dataheader.Items.Add(header);

                if (header.Equals("ReferenceLevel") || header.Equals("BaseConstraint") || header.Equals("TopConstraint") || header.Equals("BaseLevel") || header.Equals("TopLevel") || header.Equals("Level"))
                {
                    cb_dataheader.Items.Remove(header);
                }
            }
            //cb_dataheader.DisplayMember = "Name";
            MessageBox.Show("Get Data Successfully", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_sort_Click(object sender, EventArgs e)
        {
            if (grv_element.ColumnCount == 15)
            {
                if (chb_sortall.Checked)
                {
                    ResetDataToSearchAll();
                }
                else if (chb_sortall.Checked == false)
                {
                    ResetDataToMark();
                }

                List<Element> listelement = new List<Element>();
                List<Element> listelementreverse = new List<Element>();
                List<Element> listelementlink = new List<Element>();

                List<Element> listelementproject = TransferStringtoBuiltInCateSort(doc);
                if (listelementproject == null)
                {
                    listelement = listelementlink;
                }
                else if (listelementlink == null)
                {
                    listelement = listelementproject;
                }
                else if (listelementlink != null && listelementproject != null)
                {
                    listelement = listelementlink.Concat(listelementproject).ToList();
                    listelementreverse = listelementproject.Concat(listelementlink).ToList();
                }
                if (TransferAllElements(listelement).Count == 0)
                {
                    grv_element.DataSource = TransferAllElements(listelementreverse);
                }
                else grv_element.DataSource = TransferAllElements(listelement);
            }
            else if (grv_element.ColumnCount < 15)
            {
                ResetDataToFind();
                List<Element> listelement = new List<Element>();
                List<Element> listelementreverse = new List<Element>();
                List<Element> listelementlink = new List<Element>();
                string rowvalue = grv_category.CurrentRow.Cells[0].Value.ToString();

                List<Element> listelementproject = TransferStringtoBuiltInCateSort(doc);
                if (listelementproject == null)
                {
                    listelement = listelementlink;
                }
                else if (listelementlink == null)
                {
                    listelement = listelementproject;
                }
                else if (listelementlink != null && listelementproject != null)
                {
                    listelement = listelementlink.Concat(listelementproject).ToList();
                    listelementreverse = listelementproject.Concat(listelementlink).ToList();
                }
                AddvaluetoCategory(rowvalue, listelement, listelementproject);
            }
        }

        string file;
        string save;
        private void btn_savemark_Click(object sender, EventArgs e)
        {
            string userId = uiapp.Application.Username;
            lv_savemark.Items.Clear();
            if (cb_project.SelectedItem == null)
            {
                MessageBox.Show("Please select Project first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string folderlocal = nguon + cb_project.SelectedItem.ToString() + @"\";
                if (Directory.Exists(folderlocal))
                {

                }
                else
                {
                    Directory.CreateDirectory(folderlocal);
                }
                if (tb_savemark.Text.Contains("#") || tb_savemark.Text.Contains("$") || tb_savemark.Text.Contains("%") || tb_savemark.Text.Contains("&") || tb_savemark.Text.Contains("@") || tb_savemark.Text.Contains("*") || tb_savemark.Text.Contains(":"))
                {
                    MessageBox.Show("Name can't Contains special charecters [#,%,$,&...]", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    List<string> filePaths = Directory.GetFiles(nguon + cb_project.SelectedItem.ToString() + @"\").ToList();
                    List<string> readcolourdata = File.ReadAllLines(colourdata).ToList();
                    file = cb_project.SelectedItem.ToString() + @"\(" + userId + ")" + "_" + tb_savemark.Text + ".txt";
                    save = nguon + file;
                    bool b = filePaths.Any(save.Contains);
                    if (b == true)
                    {
                        DialogResult result = MessageBox.Show("Data Name exist!", "Do you want to Replace?", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            StreamWriter saveid = new StreamWriter(save, false);
                            for (int i = 0; i < readcolourdata.Count(); i++)
                            {
                                for (int j = readcolourdata.Count() - 1; j > i; j--)
                                {
                                    if (readcolourdata[j] == readcolourdata[i])
                                    {
                                        readcolourdata.RemoveAt(j);
                                    }
                                }
                            }

                            for (int i = 0; i < readcolourdata.Count(); i++)
                            {
                                saveid.WriteLine(readcolourdata[i]);
                            }
                            saveid.Close();

                            lv_savemark.View = System.Windows.Forms.View.Details;

                            List<string> listname = Directory.GetFiles(nguon + cb_project.SelectedItem.ToString() + @"\").ToList();
                            List<string> lastlistname = Removelistname(listname);
                            foreach (string name in lastlistname)
                            {
                                ListViewItem item = lv_savemark.Items.Add(GetNameFromFullName(GetNameFromPath(name)));
                                item.SubItems.Add(GetNameFromFullName(GetNameFromPath(name)));
                            }

                            File.WriteAllText(colourdata, String.Empty);
                            tb_savemark.Text = string.Empty;
                            for (int i = 0; i < grv_element.Rows.Count; i++)
                            {
                                grv_element.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                            }
                            tb_showmark.Clear();
                            MessageBox.Show("Save Successfully", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        StreamWriter saveid = new StreamWriter(save, false);
                        for (int i = 0; i < readcolourdata.Count(); i++)
                        {
                            for (int j = readcolourdata.Count() - 1; j > i; j--)
                            {
                                if (readcolourdata[j] == readcolourdata[i])
                                {
                                    readcolourdata.RemoveAt(j);
                                }
                            }
                        }

                        for (int i = 0; i < readcolourdata.Count(); i++)
                        {
                            saveid.WriteLine(readcolourdata[i]);
                        }
                        saveid.Close();

                        lv_savemark.View = System.Windows.Forms.View.Details;

                        List<string> listname = Directory.GetFiles(nguon + cb_project.SelectedItem.ToString() + @"\").ToList();
                        List<string> lastlistname = Removelistname(listname);
                        foreach (string name in lastlistname)
                        {
                            ListViewItem item = lv_savemark.Items.Add(GetNameFromFullName(GetNameFromPath(name)));
                            item.SubItems.Add(GetNameFromFullName(GetNameFromPath(name)));
                        }

                        File.WriteAllText(colourdata, String.Empty);
                        tb_savemark.Text = string.Empty;
                        for (int i = 0; i < grv_element.Rows.Count; i++)
                        {
                            grv_element.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                        }
                        tb_showmark.Clear();
                        MessageBox.Show("Save Successfully", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void grv_element_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            List<string> listid = new List<string>();
            StreamWriter swmetricafter = new StreamWriter(colourdata, true);
            for (int i = 0; i < grv_element.Rows.Count; i++)
            {
                try
                {
                    string id = grv_element.Rows[i].Cells[grv_element.ColumnCount - 1].Value.ToString();
                    if (grv_element.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.LightPink)
                    {
                        listid.Add(id);
                    }
                    else if (grv_element.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.LightPink)
                    {
                        listid.Remove(id);
                    }
                }
                catch { }
            }
            foreach (var item in listid)
            {
                swmetricafter.WriteLine(item);
            }
            swmetricafter.Close();
        }

        private void btn_clearmark_Click(object sender, EventArgs e)
        {
            File.WriteAllText(colourdata, String.Empty);
            for (int i = 0; i < grv_element.Rows.Count; i++)
            {
                if (grv_element.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.LightPink)
                {
                    grv_element.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                }
            }
            tb_showmark.Clear();
            tb_showmark.AppendText("Deleted all marked Elements");
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            string userId = uiapp.Application.Username;
            List<string> liststringdelete = new List<string>();
            var listItem = lv_savemark.CheckedItems;
            foreach (ListViewItem item in listItem)
            {
                liststringdelete.Add(item.Text);
            }
            if (lb_swich.Text == "Local")
            {
                foreach (var item in liststringdelete)
                {
                    string save = nguon + cb_project.SelectedItem.ToString() + @"\" + item + ".txt";
                    string savedescription = nguon + cb_project.SelectedItem.ToString() + @"\" + item + "&description.txt";
                    if (File.Exists(save))
                    {
                        File.Delete(save);
                        File.Delete(savedescription);
                    }
                }

                lv_savemark.Items.Clear();
                lv_savemark.View = System.Windows.Forms.View.Details;

                List<string> listname = Directory.GetFiles(nguon + cb_project.SelectedItem.ToString() + @"\").ToList();
                List<string> lastlistname = Removelistname(listname);
                foreach (string name in lastlistname)
                {
                    ListViewItem item = lv_savemark.Items.Add(GetNameFromFullName(GetNameFromPath(name)));
                    item.SubItems.Add(GetNameFromFullName(GetNameFromPath(name)));
                }
            }
            else if (lb_swich.Text == "Central")
            {
                foreach (var item in liststringdelete)
                {
                    string save = nguoncentral + cb_project.SelectedItem.ToString() + @"\" + userId + @"\" + item + ".txt";
                    string savedescription = nguoncentral + cb_project.SelectedItem.ToString() + @"\" + userId + @"\" + item + "&description.txt";
                    if (File.Exists(save))
                    {
                        File.Delete(save);
                        File.Delete(savedescription);
                    }
                }

                lv_savemark.Items.Clear();
                lv_savemark.View = System.Windows.Forms.View.Details;

                List<string> listname = Directory.GetFiles(nguoncentral + cb_project.SelectedItem.ToString() + @"\" + userId + @"\").ToList();
                List<string> lastlistname = Removelistname(listname);
                foreach (string name in lastlistname)
                {
                    ListViewItem item = lv_savemark.Items.Add(GetNameFromFullName(GetNameFromPath(name)));
                    item.SubItems.Add(GetNameFromFullName(GetNameFromPath(name)));
                }
            }

        }
        internal static string GetNameFromPath(string PathGoc)
        {
            return PathGoc.Substring(PathGoc.LastIndexOf(@"\") + 1);
        }
        internal static string GetNameFromFullName(string pFullName)
        {
            return pFullName.Substring(0, pFullName.LastIndexOf("."));
        }

        private void ResetDataToSearchAll()
        {
            if (grv_element.Columns.Count > 0)
            {
                for (int i = 0; i < grv_element.Columns.Count; i++)
                {
                    grv_element.Columns.RemoveAt(i);
                }
            }
            grv_element.DataSource = null;
            grv_element.Refresh();
            List<Element> listelement = new List<Element>();
            List<Element> listelementreverse = new List<Element>();
            List<Element> listelementlink = new List<Element>();

            List<Element> listelementproject = ListelementInCate(doc);

            if (listelementproject == null)
            {
                listelement = listelementlink;
            }
            else if (listelementlink == null)
            {
                listelement = listelementproject;
            }
            else if (listelementlink != null && listelementproject != null)
            {
                listelement = listelementlink.Concat(listelementproject).ToList();
                listelementreverse = listelementproject.Concat(listelementlink).ToList();
            }
            if (TransferAllElements(listelement).Count == 0)
            {
                grv_element.DataSource = TransferAllElements(listelementreverse);
            }
            else grv_element.DataSource = TransferAllElements(listelement);
        }
        private void ResetDataToMark()
        {
            grv_element.Columns.Clear();
            if (grv_element.Columns.Count > 0)
            {
                for (int i = 0; i < grv_element.Columns.Count; i++)
                {
                    grv_element.Columns.RemoveAt(i);
                }
            }
            grv_element.DataSource = null;
            grv_element.Refresh();
            List<Element> listelement = new List<Element>();
            List<Element> listelementreverse = new List<Element>();
            List<Element> listelementlink = new List<Element>();
            foreach (Document doclink in uiapp.Application.Documents)
            {
                if (doclink.IsLinked)
                {
                    foreach (var item in TransferStringtoLoadElements(doclink))
                    {
                        listelementlink.Add(item);
                    }
                }
            }
            List<Element> listelementproject = TransferStringtoLoadElements(doc);

            if (listelementproject == null)
            {
                listelement = listelementlink;
            }
            else if (listelementlink == null)
            {
                listelement = listelementproject;
            }
            else if (listelementlink != null && listelementproject != null)
            {
                listelement = listelementlink.Concat(listelementproject).ToList();
                listelementreverse = listelementproject.Concat(listelementlink).ToList();
            }
            if (TransferAllElements(listelement).Count == 0)
            {
                grv_element.DataSource = TransferAllElements(listelementreverse);
            }
            else grv_element.DataSource = TransferAllElements(listelement);
        }
        private void btn_loadmark_Click(object sender, EventArgs e)
        {
            grv_element.Columns.Clear();
            if (grv_element.Columns.Count > 0)
            {
                for (int i = 0; i < grv_element.Columns.Count; i++)
                {
                    grv_element.Columns.RemoveAt(i);
                }
            }
            grv_element.DataSource = null;
            grv_element.Refresh();
            List<Element> listelement = new List<Element>();
            List<Element> listelementreverse = new List<Element>();
            List<Element> listelementlink = new List<Element>();

            List<Element> listelementproject = TransferStringtoLoadElements(doc);

            if (listelementproject == null)
            {
                listelement = listelementlink;
            }
            else if (listelementlink == null)
            {
                listelement = listelementproject;
            }
            else if (listelementlink != null && listelementproject != null)
            {
                listelement = listelementlink.Concat(listelementproject).ToList();
                listelementreverse = listelementproject.Concat(listelementlink).ToList();
            }
            if (TransferAllElements(listelement).Count == 0)
            {
                grv_element.DataSource = TransferAllElements(listelementreverse);
            }
            else grv_element.DataSource = TransferAllElements(listelement);
        }

        private void btn_swich_BackColorChanged(object sender, EventArgs e)
        {
            if (btn_swich.BackColor == System.Drawing.Color.LightGreen)
            {
                lb_swich.Text = "Local";
                btn_savemark.Enabled = true;
                btn_savemark.Visible = true;
                btn_loadmark.Enabled = true;
                btn_loadmark.Visible = true;

                lv_savemark.Items.Clear();
                lv_username.Items.Clear();
                //lv_savemark.View = System.Windows.Forms.View.Details;
                //List<string> listname = Directory.GetFiles(nguon).ToList();
                //Removelistname(listname);
                //foreach (string name in listname)
                //{
                //    ListViewItem item = lv_savemark.Items.Add(GetNameFromFullName(GetNameFromPath(name)));
                //    item.SubItems.Add(GetNameFromFullName(GetNameFromPath(name)));
                //}
            }
            else if (btn_swich.BackColor == System.Drawing.Color.Tomato)
            {
                lb_swich.Text = "Central";
                cb_project.Enabled = true;
                btn_savemark.Enabled = false;
                btn_savemark.Visible = false;
                btn_loadmark.Enabled = false;
                btn_loadmark.Visible = false;
            }
        }

        private void btn_swich_Click(object sender, EventArgs e)
        {
            lv_savemark.Items.Clear();
            cb_project.Items.Clear();
            cb_project.Text = "";
            if (btn_swich.BackColor == System.Drawing.Color.LightGreen)
            {
                btn_swich.BackColor = System.Drawing.Color.Tomato;
                string folderorigin = googlestream + @"Shared drives\BIM Project\Project Save\";
                List<string> directories = Directory.GetDirectories(folderorigin).ToList();
                directories = directories.OrderBy(x => x.ToString()).ToList();
                foreach (var folder in directories)
                {
                    if (folder != null)
                    {
                        cb_project.Items.Add(folder.Substring(folder.LastIndexOf(@"\") + 1));
                    }
                }
                cb_level.DisplayMember = "Name";
            }
            else if (btn_swich.BackColor == System.Drawing.Color.Tomato)
            {
                btn_swich.BackColor = System.Drawing.Color.LightGreen;
                string folderlocal = nguoncentral;
                List<string> directories = Directory.GetDirectories(folderlocal).ToList();
                directories = directories.OrderBy(x => x.ToString()).ToList();
                foreach (var folder in directories)
                {
                    if (folder != null)
                    {
                        cb_project.Items.Add(folder.Substring(folder.LastIndexOf(@"\") + 1));
                    }
                }
                cb_level.DisplayMember = "Name";
            }
        }

        private void cb_project_SelectedValueChanged(object sender, EventArgs e)
        {
            string userId = uiapp.Application.Username;
            lv_savemark.Items.Clear();
            lv_savemark.View = System.Windows.Forms.View.Details;
            try
            {

                if (lb_swich.Text == "Central")
                {
                    lv_savemark.Items.Clear();
                    List<string> listname = Directory.GetFiles(nguoncentral + cb_project.SelectedItem.ToString() + @"\" + userId).ToList();
                    foreach (string name in listname)
                    {
                        if (GetNameFromFullName(GetNameFromPath(name)).Contains("&") == false)
                        {
                            ListViewItem item = lv_savemark.Items.Add(GetNameFromFullName(GetNameFromPath(name)));
                            item.SubItems.Add(GetNameFromFullName(GetNameFromPath(name)));
                        }
                    }
                }
                else if (lb_swich.Text == "Local")
                {
                    lv_savemark.Items.Clear();
                    List<string> listname = Directory.GetFiles(nguon + cb_project.SelectedItem.ToString()).ToList();
                    List<string> lastlistname = Removelistname(listname);
                    foreach (string name in lastlistname)
                    {
                        ListViewItem item = lv_savemark.Items.Add(GetNameFromFullName(GetNameFromPath(name)));
                        item.SubItems.Add(GetNameFromFullName(GetNameFromPath(name)));
                    }
                }
            }
            catch { }
        }
        private List<string> Removelistname(List<string> listname)
        {
            List<string> newlist = new List<string>();
            foreach (var item in listname)
            {
                if (item.Contains("&") == false)
                {
                    newlist.Add(item);
                }
            }
            return newlist;
        }
        private void btn_selectuser_Click(object sender, EventArgs e)
        {
            lv_username.Items.Clear();
            lv_username.View = System.Windows.Forms.View.Details;
            try
            {
                string folderorigin = nguoncentral + cb_projectcollaborate.SelectedItem.ToString() + @"\";
                List<string> directories = Directory.GetDirectories(folderorigin).ToList();
                foreach (var item in directories)
                {
                    ListViewItem lvitem = lv_username.Items.Add(GetNameFromPath(item));
                    lvitem.SubItems.Add(GetNameFromPath(item));
                }
            }
            catch { }
        }

        private void btn_createlocal_Click(object sender, EventArgs e)
        {
            string userId = uiapp.Application.Username;
            try
            {
                string folderorigin = nguoncentral + cb_projectcollaborate.SelectedItem.ToString() + @"\" + userId;
                if (Directory.Exists(folderorigin))
                {
                    MessageBox.Show("Local file already exists in Project: " + cb_projectcollaborate.SelectedItem.ToString(), "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Directory.CreateDirectory(folderorigin);
                    MessageBox.Show("Successfully created Local " + userId + " in Project: " + cb_projectcollaborate.SelectedItem.ToString(), "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch { }
        }

        private void bnt_sync_Click(object sender, EventArgs e)
        {
            string userId = uiapp.Application.Username;
            List<Element> listelement = new List<Element>();
            var listItemsavemark = lv_savemark.CheckedItems;
            List<string> listsavemark = new List<string>();
            List<string> listusername = new List<string>();

            try
            {
                string folderlocal = nguon + cb_projectcollaborate.SelectedItem.ToString() + @"\";
                if (Directory.Exists(folderlocal))
                {

                }
                else
                {
                    Directory.CreateDirectory(folderlocal);
                }
            }
            catch { }

            var listItemusername = lv_username.CheckedItems;
            foreach (ListViewItem item in listItemusername)
            {
                listusername.Add(item.Text);
            }

            if (cb_projectcollaborate.SelectedItem == null)
            {
                MessageBox.Show("Please Select Project Central First ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (listItemusername.Count == 0)
                {
                    MessageBox.Show("Please Select User Data To Download ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    foreach (var name in listusername)
                    {
                        List<string> filePaths = Directory.GetFiles(nguoncentral + cb_projectcollaborate.SelectedItem.ToString() + @"\" + name).ToList();
                        foreach (String filename in filePaths)
                        {

                            string local = nguon + cb_projectcollaborate.SelectedItem.ToString() + @"\" + GetNameFromFullName(GetNameFromPath(filename)) + ".txt";
                            string central = filename;
                            File.Copy(central, local, true);
                            if (File.Exists(filename.Remove(filename.LastIndexOf("."), 4)))
                            {
                                string localdes = nguon + cb_projectcollaborate.SelectedItem.ToString() + @"\" + GetNameFromFullName(GetNameFromPath(filename)) + "&description.txt";
                                string centraldes = filename.Remove(filename.LastIndexOf("."), 4);
                                File.Copy(centraldes, localdes, true);
                            }
                        }
                    }
                }
                if (cb_project.SelectedItem != null)
                {
                    if (listItemsavemark.Count == 0)
                    {
                        MessageBox.Show("Please Select Data To Upload ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        foreach (ListViewItem item in listItemsavemark)
                        {
                            if (Directory.Exists(nguoncentral + cb_project.SelectedItem.ToString() + @"\" + userId))
                            {
                                listsavemark.Add(item.Text);
                                string local = nguon + cb_project.SelectedItem.ToString() + @"\" + item.Text + ".txt";
                                string central = nguoncentral + cb_project.SelectedItem.ToString() + @"\" + userId + @"\" + item.Text + ".txt";
                                File.Copy(local, central, true);
                                if (File.Exists(nguon + cb_project.SelectedItem.ToString() + @"\" + item.Text + "&description.txt"))
                                {
                                    string localdes = nguon + cb_project.SelectedItem.ToString() + @"\" + item.Text + "&description.txt";
                                    string centraldes = nguoncentral + cb_project.SelectedItem.ToString() + @"\" + userId + @"\" + item.Text + "&description.txt";
                                    File.Copy(localdes, centraldes, true);
                                }

                            }
                            else
                            {
                                MessageBox.Show("Please Create Project in Central First ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                        }
                        CheckforRunProgress();
                    }
                }
                else
                {
                    MessageBox.Show("Please Select Project Local First ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        private void descriptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Descriptionform form = new Descriptionform(uiapp))
            {
                string userId = uiapp.Application.Username;
                var listItemsavemark = lv_savemark.CheckedItems;
                List<string> listsavemark = new List<string>();
                if (lb_swich.Text == "Local")
                {
                    if (listItemsavemark.Count > 1)
                    {
                        MessageBox.Show("Show Description for One Data ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (listItemsavemark.Count == 0)
                    {
                        MessageBox.Show("Choose One Data ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        foreach (ListViewItem item in listItemsavemark)
                        {
                            listsavemark.Add(nguon + cb_project.SelectedItem.ToString() + @"\" + item.Text);
                        }
                        form.Senderprojectsavemark(listsavemark);
                        form.ShowDialog();
                    }
                }
                else if (lb_swich.Text == "Central")
                {
                    MessageBox.Show("Please using Local mode for add Description ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_download_Click(object sender, EventArgs e)
        {
            List<string> listusername = new List<string>();
            try
            {
                string folderlocal = nguon + cb_projectcollaborate.SelectedItem.ToString() + @"\";
                if (Directory.Exists(folderlocal))
                {

                }
                else
                {
                    Directory.CreateDirectory(folderlocal);
                }
            }
            catch { }

            var listItemusername = lv_username.CheckedItems;
            foreach (ListViewItem item in listItemusername)
            {
                listusername.Add(item.Text);
            }
            if (cb_projectcollaborate.SelectedItem == null)
            {
                MessageBox.Show("Please Select Project First ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (listItemusername.Count == 0)
                {
                    MessageBox.Show("Please Select User Data To Download ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    foreach (var name in listusername)
                    {
                        List<string> filePaths = Directory.GetFiles(nguoncentral + cb_projectcollaborate.SelectedItem.ToString() + @"\" + name).ToList();
                        foreach (String filename in filePaths)
                        {

                            string local = nguon + cb_projectcollaborate.SelectedItem.ToString() + @"\" + GetNameFromFullName(GetNameFromPath(filename)) + ".txt";
                            string central = filename;
                            File.Copy(central, local, true);
                            if (File.Exists(filename.Remove(filename.LastIndexOf("."), 4)))
                            {
                                string localdes = nguon + cb_projectcollaborate.SelectedItem.ToString() + @"\" + GetNameFromFullName(GetNameFromPath(filename)) + "&description.txt";
                                string centraldes = filename.Remove(filename.LastIndexOf("."), 4);
                                File.Copy(centraldes, localdes, true);
                            }
                        }
                    }
                    CheckforRunProgress();
                }

            }
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            try
            {
                string folderlocal = nguon + cb_projectcollaborate.SelectedItem.ToString() + @"\";
                if (Directory.Exists(folderlocal))
                {

                }
                else
                {
                    Directory.CreateDirectory(folderlocal);
                }
            }
            catch { }

            string userId = uiapp.Application.Username;
            var listItemsavemark = lv_savemark.CheckedItems;
            List<string> listsavemark = new List<string>();

            if (cb_project.SelectedItem != null)
            {
                if (listItemsavemark.Count == 0)
                {
                    MessageBox.Show("Please Select Data To Upload ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (Directory.Exists(nguoncentral + cb_project.SelectedItem.ToString() + @"\" + userId))
                    {
                        foreach (ListViewItem item in listItemsavemark)
                        {
                            listsavemark.Add(item.Text);
                            string local = nguon + cb_project.SelectedItem.ToString() + @"\" + item.Text + ".txt";
                            string central = nguoncentral + cb_project.SelectedItem.ToString() + @"\" + userId + @"\" + item.Text + ".txt";
                            File.Copy(local, central, true);
                            if (File.Exists(nguon + cb_project.SelectedItem.ToString() + @"\" + item.Text + "&description.txt"))
                            {
                                string localdes = nguon + cb_project.SelectedItem.ToString() + @"\" + item.Text + "&description.txt";
                                string centraldes = nguoncentral + cb_project.SelectedItem.ToString() + @"\" + userId + @"\" + item.Text + "&description.txt";
                                File.Copy(localdes, centraldes, true);
                            }
                        }
                        CheckforRunProgress();
                    }
                    else
                    {
                        MessageBox.Show("Please Create Project in Central First ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Select Project First ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void CheckforRunProgress()
        {
            if (InternetConnection.IsConnectedToInternet())
            {
                if (Directory.Exists(nguoncentral))
                {
                    //form custom => chịu.
                    //using (ProgressBar form = new ProgressBar(uiapp))
                    //{form.ShowDialog();}
                }
                else
                    MessageBox.Show("Can't Connect to TTD Google Drive", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Please Check your Connection to the Internet", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cb_projectcollaborate_SelectedValueChanged(object sender, EventArgs e)
        {
            lv_username.Items.Clear();
            lv_username.View = System.Windows.Forms.View.Details;
            try
            {

                string folderorigin = nguoncentral + cb_projectcollaborate.SelectedItem.ToString() + @"\";
                List<string> directories = Directory.GetDirectories(folderorigin).ToList();
                foreach (var item in directories)
                {
                    ListViewItem lvitem = lv_username.Items.Add(GetNameFromPath(item));
                    lvitem.SubItems.Add(GetNameFromPath(item));
                }
            }
            catch { }
        }

        private void tb_savemark_Click(object sender, EventArgs e)
        {
            //tb_savemark.Text= uiapp.Application.Username+": ";
        }

        private void cb_projectcollaborate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_projectcollaborate.SelectedItem != null)
            {
                try
                {
                    if (cb_project.SelectedItem.ToString() == cb_projectcollaborate.SelectedItem.ToString())
                    {
                        bnt_sync.Enabled = false;
                        bnt_sync.Visible = false;
                    }
                    else
                    {
                        bnt_sync.Enabled = true;
                        bnt_sync.Visible = true;
                    }
                }
                catch { }
            }
        }

        private void cb_project_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_project.SelectedItem != null)
            {
                try
                {
                    if (cb_project.SelectedItem.ToString() == cb_projectcollaborate.SelectedItem.ToString())
                    {
                        bnt_sync.Enabled = false;
                        bnt_sync.Visible = false;
                    }
                    else
                    {
                        bnt_sync.Enabled = true;
                        bnt_sync.Visible = true;
                    }
                }
                catch { }
            }
        }

        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            ICollection<ElementId> listeleid = new List<ElementId>();

            var listItemsavemark = lv_savemark.CheckedItems;
            List<string> listsavemark = new List<string>();
            if (lb_swich.Text == "Local")
            {
                if (listItemsavemark.Count == 0)
                {
                    MessageBox.Show("Choose One Data ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    foreach (ListViewItem item in listItemsavemark)
                    {
                        //listsavemark.Add(nguon + cb_project.SelectedItem.ToString() + @"\" + item.Text);
                        List<string> st = File.ReadAllLines(nguon + cb_project.SelectedItem.ToString() + @"\" + item.Text + ".txt").ToList();
                        foreach (var idstring in st)
                        {
                            if (idstring != string.Empty)
                            {
                                listsavemark.Add(idstring);
                            }
                        }
                    }
                }
            }
            foreach (var item in listsavemark)
            {
                int idInt1 = Convert.ToInt32(item);
                ElementId id1 = new ElementId(idInt1);
                if (id1 != null)
                {
                    listeleid.Add(id1);
                }
            }
            uidoc.Selection.SetElementIds(listeleid);
        }

        private void grv_element_SelectionChanged(object sender, EventArgs e)
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            try
            {
                string selectrow = grv_element.CurrentRow.Cells[grv_element.Columns.Count - 1].Value.ToString();
                ICollection<ElementId> listeleid = new List<ElementId>();
                int idInt1 = Convert.ToInt32(selectrow);
                ElementId id1 = new ElementId(idInt1);
                if (id1 != null)
                {
                    listeleid.Add(id1);
                }
                uidoc.Selection.SetElementIds(listeleid);
            }
            catch { }
        }

        string attach = string.Empty;
        private void attachDescriptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            attach = "Attach Description";
            using (Mailform form = new Mailform())
            {
                string userId = uiapp.Application.Username;
                var listItemsavemark = lv_savemark.CheckedItems;
                List<string> listsavemark = new List<string>();
                if (lb_swich.Text == "Local")
                {
                    if (listItemsavemark.Count > 0)
                    {
                        foreach (ListViewItem item in listItemsavemark)
                        {
                            listsavemark.Add(nguon + cb_project.SelectedItem.ToString() + @"\" + item.Text);
                        }
                        form.Senderprojectsavemark(listsavemark);
                        form.Senderattach(attach);
                        form.ShowDialog();
                    }
                    else if (listItemsavemark.Count == 0)
                    {
                        MessageBox.Show("Choose One Data ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (lb_swich.Text == "Central")
                {
                    MessageBox.Show("Please using Local mode for add Description ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void noDescriptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            attach = "No Description";
            using (Mailform form = new Mailform())
            {
                string userId = uiapp.Application.Username;
                var listItemsavemark = lv_savemark.CheckedItems;
                List<string> listsavemark = new List<string>();
                if (lb_swich.Text == "Local")
                {
                    if (listItemsavemark.Count > 0)
                    {
                        foreach (ListViewItem item in listItemsavemark)
                        {
                            listsavemark.Add(nguon + cb_project.SelectedItem.ToString() + @"\" + item.Text);
                        }
                        form.Senderprojectsavemark(listsavemark);
                        form.Senderattach(attach);
                        form.ShowDialog();
                    }
                    else if (listItemsavemark.Count == 0)
                    {
                        MessageBox.Show("Choose One Data ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (lb_swich.Text == "Central")
                {
                    MessageBox.Show("Please using Local mode for add Description ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void btn_importtxt_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Title = "Select a file";
            openDlg.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            openDlg.RestoreDirectory = true;

            DialogResult result = openDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                tb_importtxt.Text = openDlg.FileName;
            }
        }

        private List<Element> StringtoLoadElements(Document doc)
        {
            List<Element> listelement = new List<Element>();
            string save = tb_importtxt.Text;
            string[] st = File.ReadAllLines(save);
            foreach (var iditeam in st)
            {
                try
                {
                    iditeam.Trim(' ');
                    int idInt = Convert.ToInt32(iditeam);
                    ElementId id = new ElementId(idInt);
                    Element eFromId = doc.GetElement(id);
                    listelement.Add(eFromId);
                }
                catch { };
            }
            for (int i = 0; i < listelement.Count; i++)
            {
                for (int j = listelement.Count - 1; j > i; j--)
                {
                    if (listelement[j].Id == listelement[i].Id)
                    {
                        listelement.RemoveAt(j);
                    }
                }
            }
            return listelement;
        }

        private void btn_loadimporttxt_Click(object sender, EventArgs e)
        {
            List<Element> listelementproject = StringtoLoadElements(doc);
            grv_element.DataSource = TransferAllElements(listelementproject);
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            grv_element.Columns.Clear();
            if (grv_element.Columns.Count > 0)
            {
                for (int i = 0; i < grv_element.Columns.Count; i++)
                {
                    grv_element.Columns.RemoveAt(i);
                }
            }

            grv_element.DataSource = null;
            grv_element.Refresh();
            string filename = string.Empty;
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Select file";
            fdlg.InitialDirectory = @"c:\";
            fdlg.FileName = filename;
            fdlg.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                filename = fdlg.FileName;
            }

            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelApp.Visible = false;

            var excelBook = excelApp.Workbooks.Open(filename);
            var excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelBook.Sheets[1];
            var lastrowR = excelSheet.Cells.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell).Row;
            var lastrowC = excelSheet.Cells.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell).Column;

            for (int i = 1; i <= lastrowC; i++)
            {
                grv_element.Columns.Add("Column" + i.ToString(), excelSheet.Cells[1, i].Value.ToString());
            }

            for (int j = 1; j <= lastrowR; j++)
            {
                grv_element.Rows.Add();
            }

            for (int x = 1; x < lastrowR; x++)
            {
                for (int y = 0; y < lastrowC; y++)
                {
                    try
                    {
                        grv_element.Rows[x - 1].Cells[y].Value = excelSheet.Cells[x + 1, y + 1].Value.ToString();
                    }
                    catch { }
                }
            }

            excelBook.Close();
            excelApp.Quit();
            File.WriteAllText(updatedata, String.Empty);
        }

        private void btn_adddefine_Click(object sender, EventArgs e)
        {
            grv_define.Rows.Add();

        }

        private void btn_minusdefine_Click(object sender, EventArgs e)
        {
            int x = grv_define.CurrentRow.Index;

            grv_define.Rows.RemoveAt(x);
        }

        private void btn_removealldefine_Click(object sender, EventArgs e)
        {
            grv_define.Rows.Clear();
            grv_define.DataSource = null;
        }

        private void grv_define_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            for (int j = 0; j < grv_define.Rows.Count; j++)
            {
                grv_element.Columns.Add("Column" + grv_define.Rows[j].Cells[grv_define.Columns.Count - 1].Value.ToString(), grv_define.Rows[j].Cells[grv_define.Columns.Count - 1].Value.ToString());
            }
        }

        private void btn_findviews_Click(object sender, EventArgs e)
        {
            //cũng là form tương tự như thằng trên
            using (frmFindReferringView frm = new frmFindReferringView(doc, uiapp))
            {
                frm.ShowDialog();
            }
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            Autodesk.Revit.UI.Result retRes = Autodesk.Revit.UI.Result.Failed;
            ElementSet seletion = new ElementSet();
            foreach (ElementId elementId in uiapp.ActiveUIDocument.Selection.GetElementIds())
            {
                seletion.Insert(uiapp.ActiveUIDocument.Document.GetElement(elementId));
            }
            using (Transaction t = new Transaction(doc, "Show Parameters"))
            {
                t.Start();
                Units u = new Units(UnitSystem.Metric);
                doc.SetUnits(u);
                // we need to make sure that only one element is selected.
                if (seletion.Size == 1)
                {
                    // we need to get the first and only element in the selection. Do this by getting 
                    // an iterator. MoveNext and then get the current element.
                    ElementSetIterator it = seletion.ForwardIterator();
                    it.MoveNext();
                    Element element = it.Current as Element;

                    // Next we need to iterate through the parameters of the element,
                    // as we iterating, we will store the strings that are to be displayed
                    // for the parameters in a string list "parameterItems"
                    List<string> parameterItems = new List<string>();
                    List<string> parameterItemstype = new List<string>();
                    ParameterSet parameters = element.Parameters;
                    Element ele = doc.GetElement(element.GetTypeId());
                    ParameterSet parameterstype = ele.Parameters;
                    foreach (Parameter param in parameters)
                    {
                        if (param == null) continue;

                        // We will make a string that has the following format,
                        // name type value
                        // create a StringBuilder object to store the string of one parameter
                        // using the character '\t' to delimit parameter name, type and value 
                        StringBuilder sb = new StringBuilder();

                        // the name of the parameter can be found from its definition.
                        sb.AppendFormat("{0}\t", param.Definition.Name);

                        // Revit parameters can be one of 5 different internal storage types:
                        // double, int, string, Autodesk.Revit.DB.ElementId and None. 
                        // if it is double then use AsDouble to get the double value
                        // then int AsInteger, string AsString, None AsStringValue.
                        // Switch based on the storage type
                        switch (param.StorageType)
                        {
                            case Autodesk.Revit.DB.StorageType.Double:
                                // append the type and value
                                sb.AppendFormat("double\t{0}", param.AsValueString());
                                break;
                            case Autodesk.Revit.DB.StorageType.ElementId:
                                // for element ids, we will try and retrieve the element from the 
                                // document if it can be found we will display its name.
                                //sb.Append("Element\t");

                                // using ActiveDocument.GetElement(the element id) to 
                                // retrieve the element from the active document
                                Autodesk.Revit.DB.ElementId elemId = new ElementId(param.AsElementId().IntegerValue);
                                //Element elem = app.ActiveUIDocument.Document.GetElement(elemId);

                                // if there is an element then display its name, 
                                // otherwise display the fact that it is not set
                                //sb.Append(elem != null ? elem.Name : "Not set");
                                sb.AppendFormat("string\t{0}", param.AsValueString());
                                break;
                            case Autodesk.Revit.DB.StorageType.Integer:
                                // append the type and value
                                sb.AppendFormat("int\t{0}", param.AsValueString());
                                break;
                            case Autodesk.Revit.DB.StorageType.String:
                                // append the type and value
                                sb.AppendFormat("string\t{0}", param.AsString());
                                break;
                            case Autodesk.Revit.DB.StorageType.None:
                                // append the type and value
                                sb.AppendFormat("none\t{0}", param.AsValueString());
                                break;
                            default:
                                break;
                        }

                        // add the completed line to the string list
                        parameterItems.Add(sb.ToString());
                    }
                    foreach (Parameter param in parameterstype)
                    {
                        if (param == null) continue;

                        // We will make a string that has the following format,
                        // name type value
                        // create a StringBuilder object to store the string of one parameter
                        // using the character '\t' to delimit parameter name, type and value 
                        StringBuilder sb = new StringBuilder();

                        // the name of the parameter can be found from its definition.
                        sb.AppendFormat("{0}\t", param.Definition.Name);

                        // Revit parameters can be one of 5 different internal storage types:
                        // double, int, string, Autodesk.Revit.DB.ElementId and None. 
                        // if it is double then use AsDouble to get the double value
                        // then int AsInteger, string AsString, None AsStringValue.
                        // Switch based on the storage type
                        switch (param.StorageType)
                        {
                            case Autodesk.Revit.DB.StorageType.Double:
                                // append the type and value
                                sb.AppendFormat("double\t{0}", param.AsValueString());
                                break;
                            case Autodesk.Revit.DB.StorageType.ElementId:
                                // for element ids, we will try and retrieve the element from the 
                                // document if it can be found we will display its name.
                                //sb.Append("Element\t");

                                // using ActiveDocument.GetElement(the element id) to 
                                // retrieve the element from the active document
                                Autodesk.Revit.DB.ElementId elemId = new ElementId(param.AsElementId().IntegerValue);
                                //Element elem = app.ActiveUIDocument.Document.GetElement(elemId);

                                // if there is an element then display its name, 
                                // otherwise display the fact that it is not set
                                //sb.Append(elem != null ? elem.Name : "Not set");
                                sb.AppendFormat("string\t{0}", param.AsValueString());
                                break;
                            case Autodesk.Revit.DB.StorageType.Integer:
                                // append the type and value
                                sb.AppendFormat("int\t{0}", param.AsValueString());
                                break;
                            case Autodesk.Revit.DB.StorageType.String:
                                // append the type and value
                                sb.AppendFormat("string\t{0}", param.AsString());
                                break;
                            case Autodesk.Revit.DB.StorageType.None:
                                // append the type and value
                                sb.AppendFormat("none\t{0}", param.AsValueString());
                                break;
                            default:
                                break;
                        }

                        // add the completed line to the string list
                        parameterItemstype.Add(sb.ToString());
                    }

                    // Create our dialog, passing it the parameters array for display.

                    //Thiếu cái form này. => xem lại cái giao diện của nó rồi build lại. //
                    CEGVN.ShowAllParameters propertiesForm = new CEGVN.ShowAllParameters(parameterItems.ToArray(), parameterItemstype.ToArray(), doc, element);
                    propertiesForm.StartPosition = FormStartPosition.CenterParent;
                    propertiesForm.ShowDialog();
                    retRes = Autodesk.Revit.UI.Result.Succeeded;
                }
                else
                {
                    MessageBox.Show("Please select only one element");
                }

                t.Commit();
            }
        }

        private void btn_adddefine_Click_1(object sender, EventArgs e)
        {
            grv_define.Rows.Add();
        }

        private void btn_minusdefine_Click_1(object sender, EventArgs e)
        {
            int x = grv_define.CurrentRow.Index;
            grv_define.Rows.RemoveAt(x);
        }

        private void btn_removealldefine_Click_1(object sender, EventArgs e)
        {
            grv_define.Rows.Clear();
            grv_define.DataSource = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Table start.
            string html = "<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:arial'>";

            //Adding HeaderRow.
            html += "<tr>";
            foreach (DataGridViewColumn column in grv_element.Columns)
            {
                html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>" + column.HeaderText + "</th>";
            }
            html += "</tr>";

            //Adding DataRow.
            foreach (DataGridViewRow row in grv_element.Rows)
            {
                html += "<tr>";
                foreach (DataGridViewCell cell in row.Cells)
                {
                    try
                    {
                        if (cell.Value == null || cell.Value.ToString() == null || cell.Value.ToString() == "")
                        {
                            html += "<td style='width:120px;border: 1px solid #ccc'>" + " " + "</td>";
                        }
                        else
                        {
                            html += "<td style='width:120px;border: 1px solid #ccc'>" + cell.Value.ToString() + "</td>";
                        }

                    }
                    catch { }
                }
                html += "</tr>";
            }

            //Table end.
            html += "</table>";

            //File.WriteAllText(@"C:\E38EDM\DataGridView.htm", html);

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "HTML|*.htm";
            saveFileDialog1.Title = "Save File";
            //saveFileDialog1.ShowDialog();

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile())
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.Default))
                    {
                        sw.Write(html);
                    }
                }
            }

            //MessageBox.Show("Export HTML Successful", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private StringBuilder DataGridtoHTML(DataGridView dg)
        {

            StringBuilder strB = new StringBuilder();
            //create html & table
            strB.AppendLine("<html><body><center><" +
                            "table border='1' cellpadding='0' cellspacing='0'>");
            strB.AppendLine("<tr>");
            //cteate table header
            for (int i = 0; i < dg.Columns.Count; i++)
            {
                strB.AppendLine("<td align='center' valign='middle'>" +
                                dg.Columns[i].HeaderText + "</td>");
            }
            //create table body
            strB.AppendLine("<tr>");
            for (int i = 0; i < dg.Rows.Count; i++)
            {
                strB.AppendLine("<tr>");
                foreach (DataGridViewCell dgvc in dg.Rows[i].Cells)
                {
                    strB.AppendLine("<td align='center' valign='middle'>" +
                                    dgvc.Value.ToString() + "</td>");
                }
                strB.AppendLine("</tr>");

            }
            //table footer & end of html file
            strB.AppendLine("</table></center></body></html>");
            return strB;
        }
        private void lb_mode_Click(object sender, EventArgs e)
        {

        }
        private List<Point3D> listPoints(XYZ[] p)
        {
            List<Point3D> listP = new List<Point3D>();

            foreach (XYZ points in p)
            {
                Point3D p1 = new Point3D(points.X * 304.8, points.Y * 304.8, points.Z * 304.8);

                listP.Add(p1);
            }

            return listP;
        }
        private void btn_view3D_Click(object sender, EventArgs e)
        {
            try
            {
                Window1 mw = new Window1();
                {
                    var meshBuilder = new MeshBuilder();

                    GeometryModel3D wall = new GeometryModel3D();
                    List<ElementId> listselectid =uiapp.ActiveUIDocument.Selection.GetElementIds().ToList();
                    List<Element> listselect = new List<Element>();
                    foreach (ElementId eleid in listselectid)
                    {
                        if (eleid!=null)
                        {
                            listselect.Add(doc.GetElement(eleid));
                        }
                    }

                    //IList<Reference> reference = uiapp.ActiveUIDocument.Selection.PickObjects(ObjectType.Element, "Select Elements to convert Geometry");

                    foreach (Element import in listselect)
                    {
                        //var import = doc.GetElement(r);

                        Autodesk.Revit.DB.Options opt = new Options();
                        Autodesk.Revit.DB.GeometryElement geomElem = import.get_Geometry(opt);
                        foreach (GeometryObject geomObj in geomElem)
                        {
                            Solid geomSolid = geomObj as Solid;
                            if (null != geomSolid)
                            {
                                foreach (Face geomFace in geomSolid.Faces)
                                {
                                    Mesh mesh = geomFace.Triangulate();

                                    XYZ[] triangleCorners = new XYZ[3];

                                    for (int i = 0; i < mesh.NumTriangles; ++i)
                                    {
                                        MeshTriangle triangle = mesh.get_Triangle(i);

                                        triangleCorners[0] = triangle.get_Vertex(0);
                                        triangleCorners[1] = triangle.get_Vertex(1);
                                        triangleCorners[2] = triangle.get_Vertex(2);

                                        meshBuilder.AddPolygon(listPoints(triangleCorners));
                                    }
                                }
                            }
                        }
                    }

                    ModelVisual3D visual = new ModelVisual3D();
                    wall.Geometry = meshBuilder.ToMesh();
                    wall.Material = new DiffuseMaterial(new SolidColorBrush(Colors.Gray));
                    wall.BackMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.Gray));

                    Model3DGroup modelGroup = new Model3DGroup();
                    modelGroup.Children.Add(wall);

                    visual.Content = modelGroup;
                    mw.viewPort3d.Children.Add(visual);

                    mw.Show();
                }
            }

            catch (Exception ex)
            {
                string exMessage = "No exception message was included";
                if (ex.Message != null)
                {
                    if (ex.Message.Length > 0)
                    {
                        exMessage = ex.Message;
                    }
                }
                TaskDialog.Show("error", exMessage);
            }
        }
    }
}



class Conduits : Structural
{
    public String ReferenceLevel { get; set; }
}
class PipeFittings : Structural
{
    public String Level { get; set; }
}
class DuctAccessories : Structural
{
    public String Level { get; set; }
}
class DuctInsulations : Structural
{
    public String Level { get; set; }
}
class PipeInsulations : Structural
{
    public String Level { get; set; }
}
class Sprinklers : Structural
{
    public String Level { get; set; }
}
class AirTerminals : Structural
{
    public String Level { get; set; }
}
class Ducts : Structural
{
    public String ReferenceLevel { get; set; }
}
class DuctsFittings : Structural
{
    public String Level { get; set; }
}
class CableTrays
{
    public String Level { get; set; }
    public String ReferenceLevel { get; set; }
    public String SystemOrLoadable { get; set; }
    public String Family { get; set; }
    public String Type { get; set; }
    public String Material { get; set; }
    public String Width { get; set; }
    public String Height { get; set; }
    public String Length { get; set; }
    public String AdditionalInfo { get; set; }
    public String Lookup { get; set; }
    public String Report { get; set; }
}
class FlexDucts
{
    public String Level { get; set; }
    public String ReferenceLevel { get; set; }
    public String SystemOrLoadable { get; set; }
    public String Family { get; set; }
    public String Type { get; set; }
    public String Material { get; set; }
    public String SystemType { get; set; }
    public String Diameter { get; set; }
    public String Length { get; set; }
    public String AdditionalInfo { get; set; }
    public String Lookup { get; set; }
    public String Report { get; set; }
}
class Pipes : Structural
{
    public String ReferenceLevel { get; set; }
}
class ConduitFittings : Structural
{
    public String Level { get; set; }
}
class PlumbingFixtures : Structural
{
    public String Level { get; set; }
}
class PipeAccessories : Structural
{
    public String Level { get; set; }
}
class MechanicalEquipment : Structural
{
    public String Level { get; set; }
}
class FireAlarmDevices : Structural
{
    public String Level { get; set; }
}
class ElectricalEquipment : Structural
{
    public String Level { get; set; }
}


class StructuralFraming : Structural
{
    public String ReferenceLevel { get; set; }
}
class StructuralColumn : Structural
{
    public String BaseLevel { get; set; }
    public String TopLevel { get; set; }
}
class ArchitecColumn : Structural
{
    public String BaseLevel { get; set; }
    public String TopLevel { get; set; }
}
class Ramps : Structural
{
    public String BaseLevel { get; set; }
    public String TopLevel { get; set; }
}
class Roofs : Structural
{
    public String BaseLevel { get; set; }
}
class StructuralWall : Structural
{
    public String BaseConstraint { get; set; }
    public String TopConstraint { get; set; }
}
class FLoors : Structural
{
    public String Level { get; set; }
}
class Doors : Structural
{
    public String Level { get; set; }
}
class Ceilings : Structural
{
    public String Level { get; set; }
}
class Stair : Structural
{
    public String BaseLevel { get; set; }
    public String TopLevel { get; set; }
}
class StructuralFoundations : Structural
{
    public String Level { get; set; }
}
class Windows : Structural
{
    public String Level { get; set; }
}
class Roof : Structural
{
    public String BaseLevel { get; set; }
}

class AllEles : Structural
{
    public String Level { get; set; }
    public String ReferenceLevel { get; set; }
    public String BaseLevel { get; set; }
    public String TopLevel { get; set; }
    public String BaseConstraint { get; set; }
    public String TopConstraint { get; set; }
}
class Structural
{
    public String SystemOrLoadable { get; set; }
    public String Family { get; set; }
    public String AssemblyCode { get; set; }
    public String AssemblyDescription { get; set; }
    public String Type { get; set; }
    public String KeyNote { get; set; }
    public String Description { get; set; }
    public String Code1ToCode6 { get; set; }
    public String ID { get; set; }
}
class CategoryName
{
    public string CategoryNames { get; set; }
}
class ExportToExcel
{

    public void Export(DataGridView gv1, string sheetName, string title)
    {
        //Tạo các đối tượng Excel

        Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();

        Microsoft.Office.Interop.Excel.Workbooks oBooks;

        Microsoft.Office.Interop.Excel.Sheets oSheets;

        Microsoft.Office.Interop.Excel.Workbook oBook;

        Microsoft.Office.Interop.Excel.Worksheet oSheet;

        //Tạo mới một Excel WorkBook 

        oExcel.Visible = true;

        oExcel.DisplayAlerts = false;

        oExcel.Application.SheetsInNewWorkbook = 1;

        oBooks = oExcel.Workbooks;

        oBook = (Microsoft.Office.Interop.Excel.Workbook)(oExcel.Workbooks.Add(Type.Missing));

        oSheets = oBook.Worksheets;

        oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oSheets.get_Item(1);

        oSheet.Name = sheetName;

        // Tạo phần đầu nếu muốn

        Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "C1");

        head.MergeCells = true;

        head.Value2 = title;

        head.Font.Bold = true;

        head.Font.Name = "Tahoma";

        head.Font.Size = "18";

        head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

        // Tạo tiêu đề cột 

        Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");

        cl1.Value2 = "Mã đơn vị";

        cl1.ColumnWidth = 13.5;

        Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");

        cl2.Value2 = "Tên đơn vị";

        cl2.ColumnWidth = 25.0;

        Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");

        cl3.Value2 = "Chức năng";

        cl3.ColumnWidth = 40.0;

        Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "C3");

        rowHead.Font.Bold = true;

        // Kẻ viền

        rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

        // Thiết lập màu nền

        rowHead.Interior.ColorIndex = 15;

        rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

        // Tạo mẳng đối tượng để lưu dữ toàn bồ dữ liệu trong DataTable,

        // vì dữ liệu được được gán vào các Cell trong Excel phải thông qua object thuần.

        DataTable dt = Convert(gv1);

        object[,] arr = new object[dt.Rows.Count, dt.Columns.Count];

        //Chuyển dữ liệu từ DataTable vào mảng đối tượng

        for (int r = 0; r < dt.Rows.Count; r++)
        {
            DataRow dr = dt.Rows[r];

            for (int c = 0; c < dt.Columns.Count; c++)

            {
                arr[r, c] = dr[c];
            }
        }

        //Thiết lập vùng điền dữ liệu

        int rowStart = 4;

        int columnStart = 1;

        int rowEnd = rowStart + dt.Rows.Count - 1;

        int columnEnd = dt.Columns.Count;

        // Ô bắt đầu điền dữ liệu

        Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, columnStart];

        // Ô kết thúc điền dữ liệu

        Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnEnd];

        // Lấy về vùng điền dữ liệu

        Microsoft.Office.Interop.Excel.Range range = oSheet.get_Range(c1, c2);

        //Điền dữ liệu vào vùng đã thiết lập

        range.Value2 = arr;

        // Kẻ viền

        range.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

        // Căn giữa cột STT

        Microsoft.Office.Interop.Excel.Range c3 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnStart];

        Microsoft.Office.Interop.Excel.Range c4 = oSheet.get_Range(c1, c3);

        oSheet.get_Range(c3, c4).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
    }
    private DataTable Convert(DataGridView dataGridView1)
    {
        //Creating DataTable.
        DataTable dt = new DataTable();

        //Adding the Columns.
        foreach (DataGridViewColumn column in dataGridView1.Columns)
        {
            dt.Columns.Add(column.HeaderText, column.ValueType);
        }

        //Adding the Rows.
        foreach (DataGridViewRow row in dataGridView1.Rows)
        {
            dt.Rows.Add();
            foreach (DataGridViewCell cell in row.Cells)
            {
                dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
            }
        }
        return dt;
    }
}
