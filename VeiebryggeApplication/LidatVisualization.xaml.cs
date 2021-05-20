using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;

namespace VeiebryggeApplication
{
    /// <summary>
    /// Interaction logic for LidatVisualization.xaml
    /// </summary>
    public partial class LidatVisualization : Page
    {
        int footprintPoint = 0;
        List<double> clickPosition = new List<double>();
        List<double> footprint1 = new List<double> { 0, 0, 0 }, footprint2 = new List<double> { 0, 0, 0 }, footprint3 = new List<double> { 0, 0, 0 }, footprint4 = new List<double> { 0, 0, 0 };
        List<List<double>> footprints = new List<List<double>>();

        // Variables that a user might want to change
        // ------
        //Theese two variables are used in calculations and have to be correct
        double lidarHeightStep = 0.1; //(In meters) Decimal that determines how much the lidar sensor is elevated for every step
        double lidarDegreeStep = 0.1; //(In degrees) Decimal that determines how many degrees the lidar is rotatet horizontally for every meassurement
        // ------
        double footprintStep = 0.02; //(In meters) Decimal that determines how much each button click moves th footrpint corner by 
        double CameraZoomAmount = 10; //(In meters) Decimal that determines how much the camrea zooms in/out for every scroll
        double CameraRotateAmount = 5; //(In meters) Decimal that determines how much the camrea zooms in/out for every scroll
        // ------
        string platformColor = "#eeeeee", footprintColor = "#00ff00", vehicleColor = "";
        // Variables that a user might want to change



        public LidatVisualization()
        {
            InitializeComponent();
            // (Z, Y, X)
            footprints.Add(footprint1);
            footprints.Add(footprint2);
            footprints.Add(footprint3);
            footprints.Add(footprint4);

            List<List<double>> platformPositions = new List<List<double>>();
            List<List<double>> footprintPositions = new List<List<double>>();

            List<List<double>> lidarData = new List<List<double>>();

            MeshGeometry3D emptyMeshGeometry3D = new MeshGeometry3D();
            MeshGeometry3D PlatformMeshGeometry3D = new MeshGeometry3D();
            Point3DCollection PlatformPositionCollection = new Point3DCollection();

            materialPlatform = new DiffuseMaterial(new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString(platformColor)));
            modelPlatform3D.Material = materialPlatform;

            PlatformMeshGeometry3D.Positions = PlatformPositionCollection;
            modelPlatform3D.Geometry = PlatformMeshGeometry3D;

            // Positions for the platform itself
            platformPositions.Add(new List<double> { 0, 0, 0 });
            platformPositions.Add(new List<double> { 16, 0, 0 });
            platformPositions.Add(new List<double> { 16, 0, 4 });
            platformPositions.Add(new List<double> { 0, 0, 4 });
            addPlane(PlatformPositionCollection, platformPositions[0], platformPositions[1], platformPositions[2], platformPositions[3]);
        }

        // List with lidar data. For every horizontal scan a list of doubles is created, so for each list the sensor rises with x meters
        // 
        void drawVehicle(Point3DCollection ptCollection, List<List<double>> lidarDistances)
        {
            List<List<List<double>>> lidarPositions = new List<List<List<double>>>();

            // Variable that describes how many meters the lidar moves in the y direction per step (From left to right)
            double stepY = lidarHeightStep;
            // Variable that states how many degrees the lidar rotates per sample
            double stepDeg = lidarDegreeStep;


            //Loop that converts all distances to coordinates and put them in a list
            for (int i = 0; i > lidarDistances.Count; i++)
            {
                for (int o = 0; o > lidarDistances[i].Count; o++)
                {
                    double temp = lidarDistances[i][o];

                    //Calculates coordinates from distance in X and Y
                    double x = temp * Math.Cos(((stepDeg * o) * (Math.PI / 180)));
                    double z = temp * Math.Sin(((stepDeg * o) * (Math.PI / 180)));
                    
                    //Adds coordinates to a list
                    lidarPositions[i].Add(new List<double> { x, i*stepY, z });
                }
            }

            for (int i = 0; i > lidarPositions.Count; i++)
            {
                addPlane(ptCollection, lidarPositions[i][i], lidarPositions[i][i+1], lidarPositions[i+1][i], lidarPositions[i+1][i+1]);

                for (int o = 0; o > lidarPositions[i].Count; o++)
                {

                }
            }

        }

        // Function that adds a plane where outer points are the given X Y Z coordinates of the lists.
        // Points are given in the order A B C D, bottom left, bottom right, top right, top left


        void addPlane(Point3DCollection ptCollection, List<double> p1, List<double> p2, List<double> p3, List<double> p4)
        {
            if(p1.Count != 3 || p2.Count != 3 || p3.Count != 3 || p4.Count != 3)
            {
                Console.WriteLine("ERROR: Function addPlane only takes 4 lists with 3 points each. (X, Y, Z)");
                return;
            }
            //Draws two triangles that form a plane with corners in the X, Y, Z positions defined In the 4 lists
            ptCollection.Add(new Point3D(p1[2], p1[1], p1[0]));
            ptCollection.Add(new Point3D(p2[2], p2[1], p2[0]));
            ptCollection.Add(new Point3D(p4[2], p4[1], p4[0]));

            ptCollection.Add(new Point3D(p3[2], p3[1], p3[0]));
            ptCollection.Add(new Point3D(p4[2], p4[1], p4[0]));
            ptCollection.Add(new Point3D(p2[2], p2[1], p2[0]));
            
            //Draws the same plane again in reverse order to make it visible from both sides
            ptCollection.Add(new Point3D(p4[2], p4[1], p4[0]));
            ptCollection.Add(new Point3D(p2[2], p2[1], p2[0]));
            ptCollection.Add(new Point3D(p1[2], p1[1], p1[0]));

            ptCollection.Add(new Point3D(p2[2], p2[1], p2[0]));
            ptCollection.Add(new Point3D(p4[2], p4[1], p4[0]));
            ptCollection.Add(new Point3D(p3[2], p3[1], p3[0]));
        }

        private void footprintToggle(object sender, RoutedEventArgs e)
        {
            if (FootprintCheck.IsChecked == true)
            {
                materialFootprint = new DiffuseMaterial(new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString(footprintColor)));
            }
            else
            {
                materialFootprint = new DiffuseMaterial();
            }
            modelFootprint3D.Material = materialFootprint;
        }

        private void platformToggle(object sender, RoutedEventArgs e)
        {
            if (PlatformCheck.IsChecked == true)
            {
                materialPlatform = new DiffuseMaterial(new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString(platformColor)));
            }
            else
            {
                materialPlatform = new DiffuseMaterial();
            }
            modelPlatform3D.Material = materialPlatform;
        }

        private void vehicleToggle(object sender, RoutedEventArgs e)
        {
            if (VehicleCheck.IsChecked == true)
            {
                materialVehicle = new DiffuseMaterial(new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#00ff00")));
            }
            else
            {
                materialVehicle = new DiffuseMaterial();
            }
            modelVehicle3D.Material = materialVehicle;
        }


        private void viewport3D_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point mousePos = e.GetPosition(viewport3D);
            PointHitTestParameters hitParams = new PointHitTestParameters(mousePos);
            VisualTreeHelper.HitTest(viewport3D, null, ResultCallback, hitParams);

            //If the user has selected a footprint to change
            if(footprintPoint > 0)
            {
                txtFootprintPoint.Text = "Punkt " + footprintPoint + " plassering:\nX: " + clickPosition[0] + "\nZ: " + clickPosition[2];

                //Changes the footprint ponts based on what point the user selected
                //Autamatically goes to the next footprintpoint so the user does not have to press each button
                switch (footprintPoint)
                {
                    case 1:
                        footprints[footprintPoint - 1] = new List<double> { clickPosition[0], clickPosition[1], clickPosition[2] };
                        footprintPoint++;
                        break;
                    case 2:
                        footprints[footprintPoint - 1] = new List<double> { clickPosition[0], clickPosition[1], clickPosition[2] };
                        footprintPoint++;
                        break;
                    case 3:
                        footprints[footprintPoint - 1] = new List<double> { clickPosition[0], clickPosition[1], clickPosition[2] };
                        footprintPoint++;
                        break;
                    case 4:
                        footprints[footprintPoint - 1] = new List<double> { clickPosition[0], clickPosition[1], clickPosition[2] };
                        footprintPoint = 0;
                        break;
                    default:
                        Console.WriteLine("Default case");
                        break;
                }
            }
            else
            {
                txtFootprintPoint.Text = "Trykk plassering:\nX: " + clickPosition[0] + "\nZ: " + clickPosition[2];
            }


            selectedFootprintPoint(footprintPoint);
            if (footprints[3][0] != 0)
            {
                drawFootprint();
            }

        }


        //Function that draws and refreshes the user defined footprint
        void drawFootprint() {

            MeshGeometry3D FootprintMeshGeometry3D = new MeshGeometry3D();
            Point3DCollection FootprintPositionCollection = new Point3DCollection();

            FootprintMeshGeometry3D.Positions = FootprintPositionCollection;
            modelFootprint3D.Geometry = FootprintMeshGeometry3D;

            materialFootprint = new DiffuseMaterial(new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString(footprintColor)));
            modelFootprint3D.Material = materialFootprint;

            addPlane(FootprintPositionCollection, footprints[0], footprints[1], footprints[2], footprints[3]);
        }


        HitTestResultBehavior ResultCallback(HitTestResult result)
        {
            RayHitTestResult rayResult = result as RayHitTestResult;
            if (rayResult != null)
            {
                RayMeshGeometry3DHitTestResult rayMeshResult = rayResult as RayMeshGeometry3DHitTestResult;

                //Gets X, Y and Z coordinates of users click and saves them in variables
                clickPosition = new List<double> { Math.Round(rayMeshResult.PointHit.Z, 4), 0.01, Math.Round(rayMeshResult.PointHit.X, 4) };
            }
            return HitTestResultBehavior.Continue;
        }

        private void btnPup_Click(object sender, RoutedEventArgs e)
        {
            if (footprintPoint > 0)
            {
                footprints[footprintPoint - 1][2]+= footprintStep;
                drawFootprint();
            }
        }

        private void btnPdown_Click(object sender, RoutedEventArgs e)
        {
            if (footprintPoint > 0)
            {
                footprints[footprintPoint - 1][2] -= footprintStep;
                drawFootprint();
            }
        }

        private void btnPright_Click(object sender, RoutedEventArgs e)
        {
            if (footprintPoint > 0)
            {
                footprints[footprintPoint - 1][0] += footprintStep;
                drawFootprint();
            }
        }

        private void btnPleft_Click(object sender, RoutedEventArgs e)
        {
            if (footprintPoint > 0)
            {
                footprints[footprintPoint - 1][0] -= footprintStep;
                drawFootprint();
            }
        }

        private void pOff_btnCLick(object sender, RoutedEventArgs e)
        {
            footprintPoint = 0;
            selectedFootprintPoint(footprintPoint);
        }

        private void p1_btnCLick(object sender, RoutedEventArgs e)
        {
            footprintPoint = 1;
            selectedFootprintPoint(footprintPoint);
        }

        private void p2_btnCLick(object sender, RoutedEventArgs e)
        {
            footprintPoint = 2;
            selectedFootprintPoint(footprintPoint);
        }

        private void p3_btnCLick(object sender, RoutedEventArgs e)
        {
            footprintPoint = 3;
            selectedFootprintPoint(footprintPoint);
        }
        private void p4_btnCLick(object sender, RoutedEventArgs e)
        {
            footprintPoint = 4;
            selectedFootprintPoint(footprintPoint);
        }

        void selectedFootprintPoint(int P)
        {
            btnP1.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF404040"));
            btnP2.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF404040"));
            btnP3.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF404040"));
            btnP4.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF404040"));
            btnPoff.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF404040"));

            switch (P)
            {
                case 1:
                    btnP1.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#cccccc"));
                    break;
                case 2:
                    btnP2.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#cccccc"));
                    break;
                case 3:
                    btnP3.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#cccccc"));
                    break;
                case 4:
                    btnP4.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#cccccc"));
                    break;
                default:
                    btnPoff.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#cccccc"));
                    break;
            }
        }

        private void MouseWheelEvent(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                MoveCameraForwards(CameraZoomAmount);
            }
            else
            {
                MoveCameraForwards(-CameraZoomAmount);
            }
        }


        // The 4 default camera positions 
        // Focus point - camera point
        private void btnPreset1_Click(object sender, RoutedEventArgs e)
        {
            camMain.Position = new Point3D(-20, 6, 8);
            camMain.LookDirection = new Vector3D(14, -3, 0);
        }

        private void btnPreset2_Click(object sender, RoutedEventArgs e)
        {
            camMain.Position = new Point3D(-5, 4, -5);
            camMain.LookDirection = new Vector3D(1, -0.5, 1.4);
        }

        private void btnPreset3_Click(object sender, RoutedEventArgs e)
        {
            camMain.Position = new Point3D(2, 20, 8);
            camMain.LookDirection = new Vector3D(0.1, -20, 0);
        }

        private void btnPreset4_Click(object sender, RoutedEventArgs e)
        {
            camMain.Position = new Point3D(2, 4, -10);
            camMain.LookDirection = new Vector3D(-0.8, -3, 16);
        }





        private void btnRotateRight_Click(object sender, RoutedEventArgs e)
        {
            RotateCameraHorizontal(CameraRotateAmount);
        }

        private void btnRotateLeft_Click(object sender, RoutedEventArgs e)
        {
            RotateCameraHorizontal(-CameraRotateAmount);
        }

        private void btnRotateUp_Click(object sender, RoutedEventArgs e)
        {
            RotateCameraVertical(CameraRotateAmount);
        }

        private void btnRotateDown_Click(object sender, RoutedEventArgs e)
        {
            RotateCameraVertical(-CameraRotateAmount);
        }



        // ------
        // https://stackoverflow.com/questions/43375372/moving-perspectivecamera-in-the-direction-it-is-facing-in-c-sharp
        public void RotateCameraHorizontal(double d)
        {
            double u = 0.1;
            double angleD = u * d;
            Vector3D lookDirection = camMain.LookDirection;

            var m = new Matrix3D();
            m.Rotate(new Quaternion(camMain.UpDirection, -angleD)); // Rotate about the camera's up direction to look left/right
            camMain.LookDirection = m.Transform(camMain.LookDirection);
        }

        public void RotateCameraVertical(double d)
        {
            double u = 0.1;
            double angleD = u * d;
            Vector3D lookDirection = camMain.LookDirection;

            // Cross Product gets a vector that is perpendicular to the passed in vectors (order does matter, reverse the order and the vector will point in the reverse direction)
            var cp = Vector3D.CrossProduct(camMain.UpDirection, lookDirection);
            cp.Normalize();

            var m = new Matrix3D();
            m.Rotate(new Quaternion(cp, -angleD)); // Rotate about the vector from the cross product
            camMain.LookDirection = m.Transform(camMain.LookDirection);
        }
        // ------

        public void MoveCameraForwards(double d)
        {
            double u = 0.05;
            Vector3D lookDirection = camMain.LookDirection;
            Point3D position = camMain.Position;

            lookDirection.Normalize();
            position = position + u * lookDirection * d;

            camMain.Position = position;
        }
    }
}
