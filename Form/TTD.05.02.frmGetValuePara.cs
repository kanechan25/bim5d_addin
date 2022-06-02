using Autodesk.Revit.DB;
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
    public partial class frmGetValuePara : System.Windows.Forms.Form
    {
        UIApplication uiapp;
        Document doc;
        public frmGetValuePara(UIApplication uiapp, Document doc)
        {
            this.doc = doc;
            this.uiapp = uiapp;
            InitializeComponent();
        }

        private void btn_SetValue_Click(object sender, EventArgs e)
        {
            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Transaction Name");
                FilteredElementCollector collector = new FilteredElementCollector(doc);
                List<Element> listele = collector.WhereElementIsNotElementType().OfClass(typeof(FamilyInstance)).ToList();
                SystemorLoadable(doc);
                MessageBox.Show("Set Value " + listele.Count + " Kind Of Family", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                tx.Commit();
            }
        }
        private void SystemorLoadable(Document doc)
        {
            string para = "KindOfFamily";
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<Element> listele = collector.WhereElementIsNotElementType().OfClass(typeof(FamilyInstance)).ToList();
            FilteredElementCollector collector1 = new FilteredElementCollector(doc);
            List<Element> listall = collector1.WhereElementIsNotElementType().ToList();
            foreach (var ele in listele)
            {
                if (ele.LookupParameter(para) != null)
                {
                    ele.LookupParameter(para).Set("LoadableFamily");
                }
                continue;
            }
            foreach (var item in listall)
            {
                if (item.LookupParameter(para) != null && item.LookupParameter(para).AsString() != "LoadableFamily" && item.LookupParameter(para).IsReadOnly == false)
                {
                    item.LookupParameter(para).Set("SystemFamily");
                }
                continue;
            }  
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
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


        private void btn_CreateFamily_Click(object sender, EventArgs e)
        {
            List<Category> listcate = new List<Category>();
            listcate= FrmListElements.GetlistCategoryinViewAllversion(doc);
            List<BuiltInCategory> listbuilin = new List<BuiltInCategory>();
            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Transaction Name");
                try
                {
                    List<BuiltInCategory> listbuilincate = new List<BuiltInCategory>();
                    List<Category> listcategory = FrmListElements.GetlistCategoryinViewAllversion(doc);
                    //MessageBox.Show(listcategory.Count.ToString());
                    foreach (var cate in listcategory)
                    {
                        BuiltInCategory buil = ToBuiltinCategory(cate);
                        listbuilincate.Add(buil);                     
                    }
                    //Support.PARAMETER.SetNewProjectParameterToBuilinCategory(uiapp, doc, "5D", "Parameter for 5D", "KindOfFamily", listbuilincate);
                    //Support.PARAMETER.SetNewProjectParameterToBuilinCategory(uiapp, doc, "5D", "Parameter for 5D", "ADDITIONAL_INFO", listbuilincate);
                    SetNewProjectParameterToBuilinCategory(uiapp, doc, "5D", "Parameter for 5D", "KindOfFamily", listbuilincate);
                }
                catch { }
                MessageBox.Show("Successfully Create Parameter", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tx.Commit();
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


        public void SetNewProjectParameterToBuilinCategory(UIApplication app, Document doc, string group, string description, string paramname1, List<BuiltInCategory> listbuilin)
        {
            // create shared parameter file
            String modulePath = @"C:\E38EDM";
            String paramFile = modulePath + @"\SharedParameters.txt";
            if (File.Exists(paramFile))
            {
                File.Delete(paramFile);
            }
            FileStream fs = File.Create(paramFile);
            fs.Close();

            //List<Element> listele = Support.SELECTION.AllElement(doc);
            List<Element> listele = AllElement(doc);

            Parameter para1 = listele.First().LookupParameter(paramname1);
            app.Application.SharedParametersFilename = paramFile;
            // open shared parameter file
            DefinitionFile parafile = app.Application.OpenSharedParameterFile();

            //create new groups in the shared parameters file
            DefinitionGroups groups = parafile.Groups;

            //check to create new group
            var checkCreateGroup = true;
            DefinitionGroup defigroup = null;
            foreach (var gr in groups)
            {
                if (gr.Name == group)
                {
                    defigroup = gr;
                    checkCreateGroup = false;
                    break;
                }
            }
            if (checkCreateGroup)
            {
                defigroup = groups.Create(group);
            }

            //check to create new definition
            var definitions = defigroup.Definitions;
            var checkCreateTypeName = true;
            Definition definitionTypeName = null;
            Definition definitionFamilyName = null;
            foreach (var def in definitions)
            {
                if (def.Name == paramname1)
                {
                    definitionTypeName = def;
                    checkCreateTypeName = false;
                    break;
                }
            }
            if (checkCreateTypeName)
            {
                var definitionOp = new ExternalDefinitionCreationOptions(paramname1, ParameterType.Text);
                definitionTypeName = definitions.Create(definitionOp);
            }


            //check elements already has TTD parameters?
            //if not add parameters
            var categoriesTypeName = new List<string>();
            CategorySet categoriesToTypeName = uiapp.Application.Create.NewCategorySet();

            foreach (var ele in listele)
            {
                try
                {
                    if (categoriesTypeName.Contains(ele.Category.Name) == false)
                    {
                        if (ele.LookupParameter(paramname1) == null)
                        {
                            Category category = ele.Category;
                            if (category.AllowsBoundParameters == true)
                            {

                                categoriesTypeName.Add(ele.Category.Name);
                                categoriesToTypeName.Insert(category);
                            }
                        }
                    }
                }
                catch (Exception)
                {

                    continue;
                }
            }

            //create an object of InstanceBinding according to the categories
            var instanceBindingToTypeName = uiapp.Application.Create.NewInstanceBinding(categoriesToTypeName);

            //get the BindingMap of current document
            BindingMap bindingMapToTypeName = uiapp.ActiveUIDocument.Document.ParameterBindings;

            //bind the definitions to the document
            if (instanceBindingToTypeName.Categories.Size > 0)
            {
                //TaskDialog.Show("1", instanceBindingToTypeName.Categories.Size.ToString());
                bool instanceBindOkToTypeName = bindingMapToTypeName.Insert(definitionTypeName, instanceBindingToTypeName, BuiltInParameterGroup.PG_TEXT);

                //create an object of InstanceBinding according to the categories
                var instanceBindingToFamilyName = uiapp.Application.Create.NewInstanceBinding(categoriesToTypeName);

                //get the BindingMap of current document
                BindingMap bindingMapToFamilyName = uiapp.ActiveUIDocument.Document.ParameterBindings;

                //bind the definitions to the document
                bool instanceBindOkToFamilyName = bindingMapToFamilyName.Insert(definitionFamilyName, instanceBindingToFamilyName, BuiltInParameterGroup.PG_TEXT);
            }
            else
            {
                TaskDialog.Show("Warning", "Parameter Existing");
                //TaskDialog.Show("1", instanceBindingToTypeName.Categories.Size.ToString());
            }
        }


        private bool Checkbindingexis(Document doc, string paramName)
        {
            Definition definition = null;
            BindingMap bm = doc.ParameterBindings;
            DefinitionBindingMapIterator it = bm.ForwardIterator();
            ElementBinding elemBind = (ElementBinding)it.Current;
            while (it.MoveNext())
            {
                Definition def = it.Key;
                if (def.Name.Equals(paramName))
                {
                    definition = def;
                    break;
                }
            }
            if (definition == null)
            {
                return false;
            }
            return true;
        }
        string optionmode = "12345";
        string mode= @"C:\\E38EDM\ListElements\mode.txt";
        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (rd_qs.Checked)
            {
                optionmode = "Quantity Surveying Mode";
            }
            else if (rd_engi.Checked)
            {
                optionmode = "Engineering Mode";
            }
            StreamWriter swmetricafter = new StreamWriter(mode, true);
            swmetricafter.WriteLine(optionmode);
            swmetricafter.Close();
            MessageBox.Show("Mode changed, please open again this tool", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void rd_qs_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void frmGetValuePara_Load(object sender, EventArgs e)
        {
            File.WriteAllText(mode, String.Empty);
        }
    }
}
