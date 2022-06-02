using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FoxLearn.License;
using System.Net;
using System.ComponentModel;

using System.Windows.Media.Media3D;
using System.Windows.Media;
using HelixToolkit.Wpf;

namespace E38EDM.TTD
{
    [Transaction(TransactionMode.Manual)]
    class View3D : IExternalCommand
    {
        public Result Execute(ExternalCommandData data, ref string message, ElementSet elements)
        {
            try
            {
                Window1 mw = new Window1();

                UIApplication uiapp = data.Application;
                UIDocument uidoc = uiapp.ActiveUIDocument;
                Application app = uiapp.Application;
                Document doc = uidoc.Document;
                {
                    var meshBuilder = new MeshBuilder();

                    GeometryModel3D wall = new GeometryModel3D();

                    IList<Reference> reference = uidoc.Selection.PickObjects(ObjectType.Element, "Select Elements to convert Geometry");

                    foreach (Reference r in reference)
                    {
                        var import = doc.GetElement(r);

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
            return Result.Succeeded;
        }
        public List<Point3D> listPoints(XYZ[] p)
        {
            List<Point3D> listP = new List<Point3D>();

            foreach (XYZ points in p)
            {
                Point3D p1 = new Point3D(points.X * 304.8, points.Y * 304.8, points.Z * 304.8);

                listP.Add(p1);
            }

            return listP;
        }

    }
    public class ImportSelectionFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            return elem.GetType() == typeof(Wall);
        }

        public bool AllowReference(Reference r, XYZ p)
        {
            return false;
        }
    }
}
