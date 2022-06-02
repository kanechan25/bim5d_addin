using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using View = Autodesk.Revit.DB.View;

namespace E38EDM.TTD

{

    public partial class frmFindReferringView : System.Windows.Forms.Form
    {
        UIApplication uiapp;
        Document doc;
        List<Element> listchosen = new List<Element>();
        List<View> listview = new List<View>();
        List<View> viewtrue = new List<View>();
        List<View> viewfalse = new List<View>();

        public frmFindReferringView(Document doc, UIApplication uiapp)
        {
            this.uiapp = uiapp;
            this.doc = doc;
            InitializeComponent();
            string cbchange = string.Empty;
            Settings docsetting = doc.Settings;
            Categories listcategory = docsetting.Categories;
            List<string> liststring = new List<string>();
            FilteredElementCollector allframing = new FilteredElementCollector(doc);
            IList<Element> listfamily = allframing.OfClass(typeof(Family)).ToElements();
        }

     

        private List<int> listIndex = new List<int>();

        private void frmFindReferringView_Load(object sender, EventArgs e)
        {
           
        }
        public bool IsElementVisibleInView(View view, Element el)
        {
            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            if (el == null)
            {
                throw new ArgumentNullException(nameof(el));
            }

            Document doc = el.Document;

            ElementId elId = el.Id;

            FilterRule idRule = ParameterFilterRuleFactory.CreateEqualsRule(new ElementId(BuiltInParameter.ID_PARAM), elId);

            var idFilter = new ElementParameterFilter(idRule);

            Category cat = el.Category; var catFilter = new ElementCategoryFilter(cat.Id);

            FilteredElementCollector collector = new FilteredElementCollector(doc, view.Id).WhereElementIsNotElementType().WherePasses(catFilter).WherePasses(idFilter);
            return collector.Any();
        }
        private string Getnamelement(IList<Element> listelement, int stt)
        {
            string name = string.Empty;
            name = listelement[stt].Name;
            return name;
        }

        private void btn_Open_Click(object sender, EventArgs e)
        {
            var listItem = listView1.SelectedItems;

            foreach (var item in listItem)
            {
                ListViewItem lvitem = item as ListViewItem;
                int index = int.Parse(lvitem.Text);

                //get viewsheet
                View vs = viewtrue[index - 1];

                if (vs != null) openView(vs, uiapp);
            }
        }

        private void openView(Autodesk.Revit.DB.View view, UIApplication uiapp)
        {
            ElementId selectedId = uiapp.ActiveUIDocument.Selection.GetElementIds().ToList().First();
            Element ele = doc.GetElement(selectedId);
            if (chb_Zoomto.Checked==true)
            {
                uiapp.ActiveUIDocument.ActiveView = view;
                ZoomtoElement(uiapp, ele.Id);
            }
            else
            {
                uiapp.ActiveUIDocument.ActiveView = view;
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ZoomtoElement(UIApplication uiapp, ElementId eleid)
        {
            try
            {
                uiapp.ActiveUIDocument.ShowElements(eleid);
            }

            catch
            {

            }
        }
        private List<View>Getlistview(Document doc, List<Element>listviewport, List<View> listview)
        {
            List<View> listlastview = new List<View>();
            foreach (var el in listviewport)
            {
                Viewport vp = el as Viewport;
                Element ele = doc.GetElement(vp.ViewId);
                foreach (var view in listview)
                {
                    if (view.Id == ele.Id)
                    {
                        listlastview.Add(view);
                    }
                }
            }
            return listlastview;
        }

        public Boolean radionallview_Check()
        {
            if (rd_allview.Checked == false)
            {
                return false;
            }
            rd_allview.Checked = true;

            return true;
        }

        public Boolean radionviewinsheet_Check()
        {
            if (rd_viewinsheet.Checked == false)
            {
                return false;
            }
            rd_viewinsheet.Checked = true;

            return true;
        }

        private void rd_allview_CheckedChanged(object sender, EventArgs e)
        {
            viewtrue.Clear();
            int stt = 0;
            ElementId selectedId = uiapp.ActiveUIDocument.Selection.GetElementIds().ToList().First();
            Element ele = doc.GetElement(selectedId);
            List<View> listview = new List<View>();
            List<View> listviewall = new FilteredElementCollector(doc).OfClass(typeof(View)).WhereElementIsNotElementType().ToElements().Cast<View>().ToList();
            List<Element> listviewport = new FilteredElementCollector(doc).OfClass(typeof(Viewport)).WhereElementIsNotElementType().ToElements().ToList();
            listview = Getlistview(doc, listviewport, listviewall);
            if (rd_allview.Checked==true)
            {
                listView1.Items.Clear();
                listview = listviewall;
                listView1.View = System.Windows.Forms.View.Details;
                foreach (var view in listview)
                {
                    try
                    {
                        if (IsElementVisibleInView(view, ele) == true)
                        {
                            viewtrue.Add(view);
                        }
                        else
                        {
                            viewfalse.Add(view);
                        }
                    }
                    catch { };
                }

                foreach (var ass in viewtrue)
                {
                    ListViewItem item = new ListViewItem();
                    stt++;
                    item.Text = stt + "";
                    if (ass != null)
                    {
                        item.SubItems.Add(ass.Name);
                    }
                    listView1.Items.Add(item);
                }
            }
        }

        private void rd_viewinsheet_CheckedChanged(object sender, EventArgs e)
        {
            viewtrue.Clear();
            int stt = 0;
            ElementId selectedId = uiapp.ActiveUIDocument.Selection.GetElementIds().ToList().First();
            Element ele = doc.GetElement(selectedId);
            List<View> listview = new List<View>();
            List<View> listviewall = new FilteredElementCollector(doc).OfClass(typeof(View)).WhereElementIsNotElementType().ToElements().Cast<View>().ToList();
            List<Element> listviewport = new FilteredElementCollector(doc).OfClass(typeof(Viewport)).WhereElementIsNotElementType().ToElements().ToList();
            listview = Getlistview(doc, listviewport, listviewall);

            if (rd_viewinsheet.Checked==true)
            {
                listView1.Items.Clear();
                listview = Getlistview(doc, listviewport, listviewall);
                listView1.View = System.Windows.Forms.View.Details;
                foreach (var view in listview)
                {
                    try
                    {
                        if (IsElementVisibleInView(view, ele) == true)
                        {
                            viewtrue.Add(view);
                        }
                        else
                        {
                            viewfalse.Add(view);
                        }
                    }
                    catch { };
                }

                foreach (var ass in viewtrue)
                {
                    ListViewItem item = new ListViewItem();
                    stt++;
                    item.Text = stt + "";
                    if (ass != null)
                    {
                        item.SubItems.Add(ass.Name);
                    }
                    listView1.Items.Add(item);
                }
            }
        }
    }
}
