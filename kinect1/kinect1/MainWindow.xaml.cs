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
using Microsoft.Kinect;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;
using Microsoft.Win32;

namespace kinect1
{
    public partial class MainWindow : Window
    {
        KinectSensor kinect;
        Skeleton[] skeletons = new Skeleton[20];
        int i = 0;
        double KepalaScale1, TubuhScale1, LenganKananAtasScale1, LenganKananBawahScale1, LenganKiriAtasScale1, LenganKiriBawahScale1, KakiKananAtasScale1, KakiKananBawahScale1, KakiKiriAtasScale1, KakiKiriBawahScale1;
        double KepalaScale2, TubuhScale2, LenganKananAtasScale2, LenganKananBawahScale2, LenganKiriAtasScale2, LenganKiriBawahScale2, KakiKananAtasScale2, KakiKananBawahScale2, KakiKiriAtasScale2, KakiKiriBawahScale2;
        int oldId1, oldId2, newId1, newId2;
        bool similar = false;
        string[] scenePath = new string[50];
        int idx = 0;
        int kepala1Rasio, tubuh1Rasio, lKi1Rasio, tKi1Rasio, lKa1Rasio, tKa1Rasio, pKi1Rasio, kKi1Rasio, pKa1Rasio, kKa1Rasio;
        int kepala2Rasio, tubuh2Rasio, lKi2Rasio, tKi2Rasio, lKa2Rasio, tKa2Rasio, pKi2Rasio, kKi2Rasio, pKa2Rasio, kKa2Rasio;
        double kepala1H, tubuh1H, LKi1H, TKi1H, LKa1H, TKa1H, PKi1H, KKi1H, PKa1H, KKa1H;
        double kepala2H, tubuh2H, LKi2H, TKi2H, LKa2H, TKa2H, PKi2H, KKi2H, PKa2H, KKa2H;
        bool showwP1 = true;
        bool showwP2 = true;

        int HeadToCShoulderCal1, CShoulderToCHipCal1;
        int CShoulderToRShoulderCal1, RShoulderToRElbowCal1, RElbowToRHandCal1;
        int CShoulderToLShoulderCal1, LShoulderToLElbowCal1, LElbowToLHandCal1;
        int CHipToRHipCal1, RHipToRKneeCal1, RKneeToRAnkleCal1;
        int CHipToLHipCal1, LHipToLKneeCal1, LKneeToLAnkleCal1;

        int HeadToCShoulderCal2, CShoulderToCHipCal2;
        int CShoulderToRShoulderCal2, RShoulderToRElbowCal2, RElbowToRHandCal2;
        int CShoulderToLShoulderCal2, LShoulderToLElbowCal2, LElbowToLHandCal2;
        int CHipToRHipCal2, RHipToRKneeCal2, RKneeToRAnkleCal2;
        int CHipToLHipCal2, LHipToLKneeCal2, LKneeToLAnkleCal2;

        public struct newBone
        {
            public double length, angle;
            public Point midPoint;
        }

        public struct Player1Config
        {
            public int kepala1Rasio, tubuh1Rasio, lKi1Rasio, tKi1Rasio, lKa1Rasio, tKa1Rasio, pKi1Rasio, kKi1Rasio, pKa1Rasio, kKa1Rasio;
            public int HeadToCShoulderCal1, CShoulderToCHipCal1, CShoulderToRShoulderCal1, RShoulderToRElbowCal1, RElbowToRHandCal1, CShoulderToLShoulderCal1, LShoulderToLElbowCal1, LElbowToLHandCal1, CHipToRHipCal1, RHipToRKneeCal1, RKneeToRAnkleCal1, CHipToLHipCal1, LHipToLKneeCal1, LKneeToLAnkleCal1;
        }

        public struct Player2Config
        {
            public int kepala2Rasio, tubuh2Rasio, lKi2Rasio, tKi2Rasio, lKa2Rasio, tKa2Rasio, pKi2Rasio, kKi2Rasio, pKa2Rasio, kKa2Rasio;
            public int HeadToCShoulderCal2, CShoulderToCHipCal2, CShoulderToRShoulderCal2, RShoulderToRElbowCal2, RElbowToRHandCal2, CShoulderToLShoulderCal2, LShoulderToLElbowCal2, LElbowToLHandCal2, CHipToRHipCal2, RHipToRKneeCal2, RKneeToRAnkleCal2, CHipToLHipCal2, LHipToLKneeCal2, LKneeToLAnkleCal2;
        }

        Info.Player1Config p1con = new Info.Player1Config();
        Info.Player2Config p2con = new Info.Player2Config();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void updateScale()
        {
            KepalaScale1 = Kepala1.Width / Kepala1.Height;
            kepala1H = Kepala1.Height;
            TubuhScale1 = Tubuh1.Width / Tubuh1.Height;
            tubuh1H = Tubuh1.Height;
            LenganKananAtasScale1 = LenganKananAtas1.Width / LenganKananAtas1.Height;
            LKa1H = LenganKananAtas1.Height;
            LenganKananBawahScale1 = LenganKananBawah1.Width / LenganKananBawah1.Height;
            TKa1H = LenganKananBawah1.Height;
            LenganKiriAtasScale1 = LenganKiriAtas1.Width / LenganKiriAtas1.Height;
            LKi1H = LenganKiriAtas1.Height;
            LenganKiriBawahScale1 = LenganKiriBawah1.Width / LenganKiriBawah1.Height;
            TKi1H = LenganKiriBawah1.Height;
            KakiKananAtasScale1 = KakiKananAtas1.Width / KakiKananAtas1.Height;
            PKa1H = KakiKananAtas1.Height;
            KakiKananBawahScale1 = KakiKananBawah1.Width / KakiKananBawah1.Height;
            KKa1H = KakiKananBawah1.Height;
            KakiKiriAtasScale1 = KakiKiriAtas1.Width / KakiKiriAtas1.Height;
            PKi1H = KakiKiriAtas1.Height;
            KakiKiriBawahScale1 = KakiKiriBawah1.Width / KakiKiriBawah1.Height;
            KKi1H = KakiKiriBawah1.Height;

            KepalaScale2 = Kepala2.Width / Kepala2.Height;
            kepala2H = Kepala2.Height;
            TubuhScale2 = Tubuh2.Width / Tubuh2.Height;
            tubuh2H = Tubuh2.Height;
            LenganKananAtasScale2 = LenganKananAtas2.Width / LenganKananAtas2.Height;
            LKa2H = LenganKananAtas2.Height;
            LenganKananBawahScale2 = LenganKananBawah2.Width / LenganKananBawah2.Height;
            TKa2H = LenganKananBawah2.Height;
            LenganKiriAtasScale2 = LenganKiriAtas2.Width / LenganKiriAtas2.Height;
            LKi2H = LenganKiriAtas2.Height;
            LenganKiriBawahScale2 = LenganKiriBawah2.Width / LenganKiriBawah2.Height;
            TKi2H = LenganKiriBawah2.Height;
            KakiKananAtasScale2 = KakiKananAtas2.Width / KakiKananAtas2.Height;
            PKa2H = KakiKananAtas2.Height;
            KakiKananBawahScale2 = KakiKananBawah2.Width / KakiKananBawah2.Height;
            KKa2H = KakiKananBawah2.Height;
            KakiKiriAtasScale2 = KakiKiriAtas2.Width / KakiKiriAtas2.Height;
            PKi2H = KakiKiriAtas2.Height;
            KakiKiriBawahScale2 = KakiKiriBawah2.Width / KakiKiriBawah2.Height;
            KKi2H = KakiKiriBawah2.Height;
        }

        private BitmapImage getBitmap(string path, Image source, int limitWidth)
        {
            BitmapImage img = new BitmapImage();
            img = new BitmapImage(new Uri(path));
            double scale = img.Width / limitWidth;
            source.Width = img.Width / scale;
            source.Height = img.Height / scale;

            return img;
        }

        private void loadPath()
        {
            if (File.Exists("SemuaPlayer1.xml"))
            {
                XmlSerializer rd = new XmlSerializer(typeof(Info));
                FileStream read = new FileStream("SemuaPlayer1.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                Info info = (Info)rd.Deserialize(read);
                Kepala1.Source = getBitmap(info.kepalaPath, Kepala1, 80);
                Tubuh1.Source = getBitmap(info.tubuhPath, Tubuh1, 80);
                LenganKananAtas1.Source = getBitmap(info.LKaAPath, LenganKananAtas1, 50);
                LenganKananBawah1.Source = getBitmap(info.LKaBPath, LenganKananBawah1, 50);
                LenganKiriAtas1.Source = getBitmap(info.LKiAPath, LenganKiriAtas1, 50);
                LenganKiriBawah1.Source = getBitmap(info.LKiBPath, LenganKiriBawah1, 50);
                KakiKananAtas1.Source = getBitmap(info.KKaAPath, KakiKananAtas1, 50);
                KakiKananBawah1.Source = getBitmap(info.KKaBPath, KakiKananBawah1, 50);
                KakiKiriAtas1.Source = getBitmap(info.KKiAPath, KakiKiriAtas1, 50);
                KakiKiriBawah1.Source = getBitmap(info.KKiBPath, KakiKiriBawah1, 50); 
            }

            if (File.Exists("SemuaPlayer2.xml"))
            {
                XmlSerializer rd = new XmlSerializer(typeof(Info));
                FileStream read = new FileStream("SemuaPlayer2.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                Info info = (Info)rd.Deserialize(read);
                Kepala2.Source = getBitmap(info.kepalaPath, Kepala2, 80);
                Tubuh2.Source = getBitmap(info.tubuhPath, Tubuh2, 80);
                LenganKananAtas2.Source = getBitmap(info.LKaAPath, LenganKananAtas2, 50);
                LenganKananBawah2.Source = getBitmap(info.LKaBPath, LenganKananBawah2, 50);
                LenganKiriAtas2.Source = getBitmap(info.LKiAPath, LenganKiriAtas2, 50);
                LenganKiriBawah2.Source = getBitmap(info.LKiBPath, LenganKiriBawah2, 50);
                KakiKananAtas2.Source = getBitmap(info.KKaAPath, KakiKananAtas2, 50);
                KakiKananBawah2.Source = getBitmap(info.KKaBPath, KakiKananBawah2, 50);
                KakiKiriAtas2.Source = getBitmap(info.KKiAPath, KakiKiriAtas2, 50);
                KakiKiriBawah2.Source = getBitmap(info.KKiBPath, KakiKiriBawah2, 50);
            }

            if (File.Exists("semuaSceneJadiSatu.xml"))
            {
                XmlSerializer rd = new XmlSerializer(typeof(Info));
                FileStream read = new FileStream("semuaSceneJadiSatu.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                Info info = (Info)rd.Deserialize(read);
                scenePath = new string[info.ScenePath.Length];
                for (int i = 0; i < scenePath.Length; i++)
                {
                    scenePath[i] = info.ScenePath[i];
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Height = 780;
            loadPath();
            updateScale();

            if (KinectSensor.KinectSensors.Count > 0)
            {
                kinect = KinectSensor.KinectSensors[0];
            }
            if (kinect.Status == KinectStatus.Connected)
            {
                kinect.ColorStream.Enable();
                kinect.DepthStream.Enable();
                kinect.SkeletonStream.Enable();
                kinect.SkeletonStream.EnableTrackingInNearRange = true;
                kinect.SkeletonStream.TrackingMode = SkeletonTrackingMode.Default;
                kinect.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(kinect_AllFramesReady);
                kinect.Start();
            }

            Canvas.SetLeft(source, 0);
            Canvas.SetTop(source, 0);
        }

        void kinect_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            using (ColorImageFrame colorFrame = e.OpenColorImageFrame())
            {
                if (colorFrame == null)
                {
                    return;
                }

                byte[] pixels = new byte[colorFrame.PixelDataLength];
                colorFrame.CopyPixelDataTo(pixels);
                int stride = colorFrame.Width * 4;
                source.Source = new BitmapImage(new Uri(scenePath[idx]));
                mask.Source = BitmapSource.Create(640, 480, 96, 96, PixelFormats.Bgr32, null, pixels, stride);
            }

            Skeleton skeleton1 = null;
            Skeleton skeleton2 = null;

            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame == null)
                {
                    return;
                }

                skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                skeletonFrame.CopySkeletonDataTo(skeletons);

                if (showwP1)
                {
                    skeleton1 = (from s in skeletons where s.TrackingState == SkeletonTrackingState.Tracked select s).FirstOrDefault();
                }

                if (showwP2)
                {
                    skeleton2 = (from s in skeletons where s.TrackingState == SkeletonTrackingState.Tracked select s).LastOrDefault();
                }
                /*
                #region get newId
                if (skeleton1 != null)
                {
                    newId1 = skeleton1.TrackingId;
                }

                if (skeleton2 != null)
                {
                    newId2 = skeleton2.TrackingId;
                }
                #endregion

                #region check similarity
                if (newId1 == newId2)
                {
                    similar = true;
                }
                else
                {
                    similar = false;
                }
                #endregion

                if (similar)
                {
                }
                else
                {
                }
                */
            }

            #region visibility
            if (skeleton1 == null)
            {
                LenganKiriBawah1.Visibility = System.Windows.Visibility.Hidden;
                LenganKananBawah1.Visibility = System.Windows.Visibility.Hidden;
                LenganKiriAtas1.Visibility = System.Windows.Visibility.Hidden;
                LenganKananAtas1.Visibility = System.Windows.Visibility.Hidden;
                Tubuh1.Visibility = System.Windows.Visibility.Hidden;
                Kepala1.Visibility = System.Windows.Visibility.Hidden;
                KakiKiriAtas1.Visibility = System.Windows.Visibility.Hidden;
                KakiKiriBawah1.Visibility = System.Windows.Visibility.Hidden;
                KakiKananAtas1.Visibility = System.Windows.Visibility.Hidden;
                KakiKananBawah1.Visibility = System.Windows.Visibility.Hidden;
            }

            if (skeleton2 == null)
            {
                LenganKiriBawah2.Visibility = System.Windows.Visibility.Hidden;
                LenganKananBawah2.Visibility = System.Windows.Visibility.Hidden;
                LenganKiriAtas2.Visibility = System.Windows.Visibility.Hidden;
                LenganKananAtas2.Visibility = System.Windows.Visibility.Hidden;
                Tubuh2.Visibility = System.Windows.Visibility.Hidden;
                Kepala2.Visibility = System.Windows.Visibility.Hidden;
                KakiKiriAtas2.Visibility = System.Windows.Visibility.Hidden;
                KakiKiriBawah2.Visibility = System.Windows.Visibility.Hidden;
                KakiKananAtas2.Visibility = System.Windows.Visibility.Hidden;
                KakiKananBawah2.Visibility = System.Windows.Visibility.Hidden;
            }
            #endregion

            #region main process
            if (skeleton1 != null)
            {
                LenganKiriBawah1.Visibility = System.Windows.Visibility.Visible;
                LenganKananBawah1.Visibility = System.Windows.Visibility.Visible;
                LenganKiriAtas1.Visibility = System.Windows.Visibility.Visible;
                LenganKananAtas1.Visibility = System.Windows.Visibility.Visible;
                Tubuh1.Visibility = System.Windows.Visibility.Visible;
                Kepala1.Visibility = System.Windows.Visibility.Visible;
                KakiKiriAtas1.Visibility = System.Windows.Visibility.Visible;
                KakiKiriBawah1.Visibility = System.Windows.Visibility.Visible;
                KakiKananAtas1.Visibility = System.Windows.Visibility.Visible;
                KakiKananBawah1.Visibility = System.Windows.Visibility.Visible;
                getCameraPoint1(skeleton1, e);
            }

            if (skeleton2 != null)
            {
                LenganKiriBawah2.Visibility = System.Windows.Visibility.Visible;
                LenganKananBawah2.Visibility = System.Windows.Visibility.Visible;
                LenganKiriAtas2.Visibility = System.Windows.Visibility.Visible;
                LenganKananAtas2.Visibility = System.Windows.Visibility.Visible;
                Tubuh2.Visibility = System.Windows.Visibility.Visible;
                Kepala2.Visibility = System.Windows.Visibility.Visible;
                KakiKiriAtas2.Visibility = System.Windows.Visibility.Visible;
                KakiKiriBawah2.Visibility = System.Windows.Visibility.Visible;
                KakiKananAtas2.Visibility = System.Windows.Visibility.Visible;
                KakiKananBawah2.Visibility = System.Windows.Visibility.Visible;
                getCameraPoint2(skeleton2, e);
            }
            #endregion
        }

        private void drawLineBone1(PointCollection pc, Brush color, int thickness)
        {
            Polyline poly = new Polyline();
            poly.Points = pc;
            poly.Stroke = color;
            poly.StrokeThickness = thickness;
        }

        private void drawLineBone2(PointCollection pc, Brush color, int thickness)
        {
            Polyline poly = new Polyline();
            poly.Points = pc;
            poly.Stroke = color;
            poly.StrokeThickness = thickness;
        }

        private Point getPointFromJoint(DepthImageFrame depth, Skeleton s, JointType joint)
        {
            Point output;
            DepthImagePoint depthPoint = depth.MapFromSkeletonPoint(s.Joints[joint].Position);
            ColorImagePoint colorPoint = depth.MapToColorImagePoint(depthPoint.X, depthPoint.Y, ColorImageFormat.RgbResolution640x480Fps30);
            output = new Point(colorPoint.X, colorPoint.Y);
            return output;
        }

        private newBone getNewBone(Point fromm, Point to)
        {
            newBone output;
            double midX, midY;
            output.length = Math.Sqrt(Math.Pow(fromm.X - to.X, 2) + Math.Pow(fromm.Y - to.Y, 2));
            output.angle = (Math.Atan2(fromm.Y - to.Y, fromm.X - to.X) - Math.Atan2(to.Y - to.Y, (to.X + 10) - to.X)) * (180.0 / Math.PI);
            midX = to.X + (Math.Cos(output.angle * (Math.PI / 180.0)) * (output.length / 2));
            midY = to.Y + (Math.Sin(output.angle * (Math.PI / 180.0)) * (output.length / 2));
            output.midPoint = new Point(midX, midY);
            return output;
        }

        private void drawBone(Point from, Point to, Image objek)
        {
            double scale;
            double height;
            int rasio = 0;
            #region check scale
            if (objek == LenganKiriBawah1)
            {
                scale = LenganKiriBawahScale1;
                rasio = tKi1Rasio;
                height = TKi1H + rasio;
            }
            else if (objek == LenganKananBawah1)
            {
                scale = LenganKananBawahScale1;
                rasio = tKa1Rasio;
                height = TKa1H + rasio;
            }
            else if (objek == LenganKiriAtas1)
            {
                scale = LenganKiriAtasScale1;
                rasio = lKi1Rasio;
                height = LKi1H + rasio;
            }
            else if (objek == LenganKananAtas1)
            {
                scale = LenganKananAtasScale1;
                rasio = lKa1Rasio;
                height = LKa1H + rasio;
            }
            else if (objek == Tubuh1)
            {
                scale = TubuhScale1;
                rasio = tubuh1Rasio;
                height = tubuh1H + rasio;
            }
            else if (objek == Kepala1)
            {
                scale = KepalaScale1;
                rasio = kepala1Rasio;
                height = kepala1H + rasio;
            }
            else if (objek == KakiKananAtas1)
            {
                scale = KakiKananAtasScale1;
                rasio = pKa1Rasio;
                height = PKa1H + rasio;
            }
            else if (objek == KakiKananBawah1)
            {
                scale = KakiKananBawahScale1;
                rasio = kKa1Rasio;
                height = KKa1H + rasio;
            }
            else if (objek == KakiKiriAtas1)
            {
                scale = KakiKiriAtasScale1;
                rasio = pKi1Rasio;
                height = PKi1H + rasio;
            }
            else if (objek == KakiKiriBawah1)
            {
                scale = KakiKiriBawahScale1; //
                rasio = kKi1Rasio;
                height = KKi1H + rasio;
            }
            else if (objek == LenganKiriBawah2)
            {
                scale = LenganKiriBawahScale2;
                rasio = tKi2Rasio;
                height = TKi2H + rasio;
            }
            else if (objek == LenganKananBawah2)
            {
                scale = LenganKananBawahScale2;
                rasio = tKa2Rasio;
                height = TKa2H + rasio;
            }
            else if (objek == LenganKiriAtas2)
            {
                scale = LenganKiriAtasScale2;
                rasio = lKi2Rasio;
                height = LKi2H + rasio;
            }
            else if (objek == LenganKananAtas2)
            {
                scale = LenganKananAtasScale2;
                rasio = lKa2Rasio;
                height = LKa2H + rasio;
            }
            else if (objek == Tubuh2)
            {
                scale = TubuhScale2;
                rasio = tubuh2Rasio;
                height = tubuh2H + rasio;
            }
            else if (objek == Kepala2)
            {
                scale = KepalaScale2;
                rasio = kepala2Rasio;
                height = kepala2H + rasio;
            }
            else if (objek == KakiKananAtas2)
            {
                scale = KakiKananAtasScale2;
                rasio = pKa2Rasio;
                height = PKa2H + rasio;
            }
            else if (objek == KakiKananBawah2)
            {
                scale = KakiKananBawahScale2;
                rasio = kKa2Rasio;
                height = KKa2H + rasio;
            }
            else if (objek == KakiKiriAtas2)
            {
                scale = KakiKiriAtasScale2;
                rasio = pKi2Rasio;
                height = PKi2H + rasio;
            }
            else if (objek == KakiKiriBawah2)
            {
                scale = KakiKiriBawahScale2;
                rasio = kKi2Rasio;
                height = KKi2H + rasio;
            }
            else
            {
                scale = 0;
                height = 0;
            }
            #endregion

            newBone bone = getNewBone(from, to);
            if (height < 0)
            {
                height = 0;
            }
            objek.Height = height;
            objek.Width = objek.Height * scale;
            Canvas.SetLeft(objek, bone.midPoint.X - (objek.Width / 2));
            Canvas.SetTop(objek, bone.midPoint.Y - (objek.Height / 2));
            objek.RenderTransform = new RotateTransform(bone.angle + 90);
        }

        private Point newPoint(Point from, newBone acuan, int cal)
        {
            Point newPointSystem = new Point();
            newPointSystem.X = from.X + (Math.Cos(acuan.angle * (Math.PI / 180.0)) * (acuan.length + cal));
            newPointSystem.Y = from.Y + (Math.Sin(acuan.angle * (Math.PI / 180.0)) * (acuan.length + cal));

            return newPointSystem;
        }

        private void getCameraPoint1(Skeleton skeleton, AllFramesReadyEventArgs e)
        {
            using (DepthImageFrame depthFrame = e.OpenDepthImageFrame())
            {
                if (depthFrame == null || kinect == null)
                {
                    return;
                }

                #region limiter
                if (skeleton.ClippedEdges.HasFlag(FrameEdges.Bottom) || skeleton.ClippedEdges.HasFlag(FrameEdges.Left) || skeleton.ClippedEdges.HasFlag(FrameEdges.Right) || skeleton.ClippedEdges.HasFlag(FrameEdges.Top))
                {
                    LenganKiriBawah1.Visibility = System.Windows.Visibility.Hidden;
                    LenganKananBawah1.Visibility = System.Windows.Visibility.Hidden;
                    LenganKiriAtas1.Visibility = System.Windows.Visibility.Hidden;
                    LenganKananAtas1.Visibility = System.Windows.Visibility.Hidden;
                    Tubuh1.Visibility = System.Windows.Visibility.Hidden;
                    Kepala1.Visibility = System.Windows.Visibility.Hidden;
                    KakiKiriAtas1.Visibility = System.Windows.Visibility.Hidden;
                    KakiKiriBawah1.Visibility = System.Windows.Visibility.Hidden;
                    KakiKananAtas1.Visibility = System.Windows.Visibility.Hidden;
                    KakiKananBawah1.Visibility = System.Windows.Visibility.Hidden;
                    return;
                }
                #endregion

                source.Width = depthFrame.Width;
                source.Height = depthFrame.Height;

                #region get point
                Point leftWrist = getPointFromJoint(depthFrame, skeleton, JointType.WristLeft);
                Point leftElbow = getPointFromJoint(depthFrame, skeleton, JointType.ElbowLeft);
                Point leftShoulder = getPointFromJoint(depthFrame, skeleton, JointType.ShoulderLeft);
                Point leftHand = getPointFromJoint(depthFrame, skeleton, JointType.HandLeft);
                Point rightWrist = getPointFromJoint(depthFrame, skeleton, JointType.WristRight);
                Point rightElbow = getPointFromJoint(depthFrame, skeleton, JointType.ElbowRight);
                Point rightShoulder = getPointFromJoint(depthFrame, skeleton, JointType.ShoulderRight);
                Point rightHand = getPointFromJoint(depthFrame, skeleton, JointType.HandRight);

                Point centerShoulder = getPointFromJoint(depthFrame, skeleton, JointType.ShoulderCenter);
                Point centerHip = getPointFromJoint(depthFrame, skeleton, JointType.HipCenter);
                Point head = getPointFromJoint(depthFrame, skeleton, JointType.Head);

                Point leftHip = getPointFromJoint(depthFrame, skeleton, JointType.HipLeft);
                Point leftKnee = getPointFromJoint(depthFrame, skeleton, JointType.KneeLeft);
                Point leftAnkle = getPointFromJoint(depthFrame, skeleton, JointType.AnkleLeft);
                Point rightHip = getPointFromJoint(depthFrame, skeleton, JointType.HipRight);
                Point rightKnee = getPointFromJoint(depthFrame, skeleton, JointType.KneeRight);
                Point rightAnkle = getPointFromJoint(depthFrame, skeleton, JointType.AnkleRight);
                #endregion

                #region kepala
                newBone HeadToCShoulder = new newBone();
                HeadToCShoulder = getNewBone(head, centerShoulder);
                Point HeadC = newPoint(centerShoulder, HeadToCShoulder, HeadToCShoulderCal1);
                #endregion

                #region tubuh
                newBone CShoulderToCHip = new newBone();
                CShoulderToCHip = getNewBone(centerShoulder, centerHip);
                Point cHipC = new Point();
                cHipC.X = centerShoulder.X + (Math.Cos((CShoulderToCHip.angle + 180) * (Math.PI / 180)) * (CShoulderToCHip.length + CShoulderToCHipCal1));
                cHipC.Y = centerShoulder.Y + (Math.Sin((CShoulderToCHip.angle + 180) * (Math.PI / 180)) * (CShoulderToCHip.length + CShoulderToCHipCal1));
                #endregion

                #region tangan kanan
                newBone CShoulderToRShoulder = new newBone();
                CShoulderToRShoulder = getNewBone(rightShoulder, centerShoulder);
                Point rShoulderC = new Point();
                rShoulderC.X = centerShoulder.X + (Math.Cos(CShoulderToRShoulder.angle * (Math.PI / 180.0)) * (CShoulderToRShoulder.length + CShoulderToRShoulderCal1));
                rShoulderC.Y = centerShoulder.Y + (Math.Sin(CShoulderToRShoulder.angle * (Math.PI / 180.0)) * (CShoulderToRShoulder.length + CShoulderToRShoulderCal1));

                newBone RShoulderToRElbow = new newBone();
                RShoulderToRElbow = getNewBone(rightShoulder, rightElbow);
                Point rElbowC = new Point();
                rElbowC.X = rShoulderC.X + (Math.Cos((RShoulderToRElbow.angle + 180) * (Math.PI / 180.0)) * (RShoulderToRElbow.length + RShoulderToRElbowCal1));
                rElbowC.Y = rShoulderC.Y + (Math.Sin((RShoulderToRElbow.angle + 180) * (Math.PI / 180.0)) * (RShoulderToRElbow.length + RShoulderToRElbowCal1));

                newBone RElbowToRHand = new newBone();
                RElbowToRHand = getNewBone(rightElbow, rightHand);
                Point rHandC = new Point();
                rHandC.X = rElbowC.X + (Math.Cos((RElbowToRHand.angle + 180) * (Math.PI / 180.0)) * (RElbowToRHand.length + RElbowToRHandCal1));
                rHandC.Y = rElbowC.Y + (Math.Sin((RElbowToRHand.angle + 180) * (Math.PI / 180.0)) * (RElbowToRHand.length + RElbowToRHandCal1));
                #endregion

                #region tangan kiri
                newBone CShoulderToLShoulder = new newBone();
                CShoulderToLShoulder = getNewBone(leftShoulder, centerShoulder);
                Point lShoulderC = new Point();
                lShoulderC.X = centerShoulder.X + (Math.Cos(CShoulderToLShoulder.angle * (Math.PI / 180.0)) * (CShoulderToLShoulder.length + CShoulderToLShoulderCal1));
                lShoulderC.Y = centerShoulder.Y + (Math.Sin(CShoulderToLShoulder.angle * (Math.PI / 180.0)) * (CShoulderToLShoulder.length + CShoulderToLShoulderCal1));

                newBone LShoulderToLElbow = new newBone();
                LShoulderToLElbow = getNewBone(leftShoulder, leftElbow);
                Point lElbowC = new Point();
                lElbowC.X = lShoulderC.X + (Math.Cos((LShoulderToLElbow.angle + 180) * (Math.PI / 180.0)) * (LShoulderToLElbow.length + LShoulderToLElbowCal1));
                lElbowC.Y = lShoulderC.Y + (Math.Sin((LShoulderToLElbow.angle + 180) * (Math.PI / 180.0)) * (LShoulderToLElbow.length + LShoulderToLElbowCal1));

                newBone LElbowToLHand = new newBone();
                LElbowToLHand = getNewBone(leftElbow, leftHand);
                Point lHandC = new Point();
                lHandC.X = lElbowC.X + (Math.Cos((LElbowToLHand.angle + 180) * (Math.PI / 180.0)) * (LElbowToLHand.length + LElbowToLHandCal1));
                lHandC.Y = lElbowC.Y + (Math.Sin((LElbowToLHand.angle + 180) * (Math.PI / 180.0)) * (LElbowToLHand.length + LElbowToLHandCal1));
                #endregion

                #region pinggang
                newBone CHipToRHip = new newBone();
                CHipToRHip = getNewBone(rightHip, centerHip);
                Point rHipC = new Point();
                //rHipC.X = centerHip.X + (Math.Cos(CHipToRHip.angle * (Math.PI / 180)) * (CHipToRHip.length + CHipToRHipCal1));
                //rHipC.Y = centerHip.Y + (Math.Sin(CHipToRHip.angle * (Math.PI / 180)) * (CHipToRHip.length + CHipToRHipCal1));
                rHipC.X = rightHip.X;
                rHipC.Y = rightHip.Y + CHipToRHipCal1;

                newBone CHipToLHip = new newBone();
                CHipToLHip = getNewBone(leftHip, centerHip);
                Point lHipC = new Point();
                //lHipC.X = centerHip.X + (Math.Cos(CHipToLHip.angle * (Math.PI / 180)) * (CHipToLHip.length + CHipToLHipCal1));
                //lHipC.Y = centerHip.Y + (Math.Sin(CHipToLHip.angle * (Math.PI / 180)) * (CHipToLHip.length + CHipToLHipCal1));
                lHipC.X = leftHip.X;
                lHipC.Y = leftHip.Y + CHipToLHipCal1;
                #endregion

                #region kaki kanan
                newBone RHipToRKnee = new newBone();
                RHipToRKnee = getNewBone(rightHip, rightKnee);
                Point rKneeC = new Point();
                rKneeC.X = rHipC.X + (Math.Cos((RHipToRKnee.angle + 180) * (Math.PI / 180)) * (RHipToRKnee.length + RHipToRKneeCal1));
                rKneeC.Y = rHipC.Y + (Math.Sin((RHipToRKnee.angle + 180) * (Math.PI / 180)) * (RHipToRKnee.length + RHipToRKneeCal1));

                newBone RKneeToRAnkle = new newBone();
                RKneeToRAnkle = getNewBone(rightKnee, rightAnkle);
                Point rAnkleC = new Point();
                rAnkleC.X = rKneeC.X + (Math.Cos((RKneeToRAnkle.angle + 180) * (Math.PI / 180)) * (RKneeToRAnkle.length + RKneeToRAnkleCal1));
                rAnkleC.Y = rKneeC.Y + (Math.Sin((RKneeToRAnkle.angle + 180) * (Math.PI / 180)) * (RKneeToRAnkle.length + RKneeToRAnkleCal1));
                #endregion

                #region kaki kiri
                newBone LHipToLKnee = new newBone();
                LHipToLKnee = getNewBone(leftHip, leftKnee);
                Point lKneeC = new Point();
                lKneeC.X = lHipC.X + (Math.Cos((LHipToLKnee.angle + 180) * (Math.PI / 180)) * (LHipToLKnee.length + LHipToLKneeCal1));
                lKneeC.Y = lHipC.Y + (Math.Sin((LHipToLKnee.angle + 180) * (Math.PI / 180)) * (LHipToLKnee.length + LHipToLKneeCal1));

                newBone LKneeToLAnkle = new newBone();
                LKneeToLAnkle = getNewBone(leftKnee, leftAnkle);
                Point lAnkleC = new Point();
                lAnkleC.X = lKneeC.X + (Math.Cos((LKneeToLAnkle.angle + 180) * (Math.PI / 180)) * (LKneeToLAnkle.length + LKneeToLAnkleCal1));
                lAnkleC.Y = lKneeC.Y + (Math.Sin((LKneeToLAnkle.angle + 180) * (Math.PI / 180)) * (LKneeToLAnkle.length + LKneeToLAnkleCal1));
                #endregion

                #region drawBone
                drawBone(lElbowC, lHandC, LenganKiriBawah1); //
                drawBone(rElbowC, rHandC, LenganKananBawah1); //
                drawBone(lShoulderC, lElbowC, LenganKiriAtas1); //
                drawBone(rShoulderC, rElbowC, LenganKananAtas1); //
                drawBone(centerShoulder, cHipC, Tubuh1); //
                drawBone(HeadC, centerShoulder, Kepala1); //
                drawBone(lHipC, lKneeC, KakiKiriAtas1); //
                drawBone(lKneeC, lAnkleC, KakiKiriBawah1); //
                drawBone(rHipC, rKneeC, KakiKananAtas1); //
                drawBone(rKneeC, rAnkleC, KakiKananBawah1); //
                #endregion
                
            }
        }

        private void getCameraPoint2(Skeleton skeleton, AllFramesReadyEventArgs e)
        {
            using (DepthImageFrame depthFrame = e.OpenDepthImageFrame())
            {
                if (depthFrame == null || kinect == null)
                {
                    return;
                }

                #region limiter
                if (skeleton.ClippedEdges.HasFlag(FrameEdges.Bottom) || skeleton.ClippedEdges.HasFlag(FrameEdges.Left) || skeleton.ClippedEdges.HasFlag(FrameEdges.Right) || skeleton.ClippedEdges.HasFlag(FrameEdges.Top))
                {
                    LenganKiriBawah2.Visibility = System.Windows.Visibility.Hidden;
                    LenganKananBawah2.Visibility = System.Windows.Visibility.Hidden;
                    LenganKiriAtas2.Visibility = System.Windows.Visibility.Hidden;
                    LenganKananAtas2.Visibility = System.Windows.Visibility.Hidden;
                    Tubuh2.Visibility = System.Windows.Visibility.Hidden;
                    Kepala2.Visibility = System.Windows.Visibility.Hidden;
                    KakiKiriAtas2.Visibility = System.Windows.Visibility.Hidden;
                    KakiKiriBawah2.Visibility = System.Windows.Visibility.Hidden;
                    KakiKananAtas2.Visibility = System.Windows.Visibility.Hidden;
                    KakiKananBawah2.Visibility = System.Windows.Visibility.Hidden;
                    return;
                }
                #endregion

                #region get point
                Point leftWrist = getPointFromJoint(depthFrame, skeleton, JointType.WristLeft);
                Point leftElbow = getPointFromJoint(depthFrame, skeleton, JointType.ElbowLeft);
                Point leftShoulder = getPointFromJoint(depthFrame, skeleton, JointType.ShoulderLeft);
                Point leftHand = getPointFromJoint(depthFrame, skeleton, JointType.HandLeft);
                Point rightWrist = getPointFromJoint(depthFrame, skeleton, JointType.WristRight);
                Point rightElbow = getPointFromJoint(depthFrame, skeleton, JointType.ElbowRight);
                Point rightShoulder = getPointFromJoint(depthFrame, skeleton, JointType.ShoulderRight);
                Point rightHand = getPointFromJoint(depthFrame, skeleton, JointType.HandRight);

                Point centerShoulder = getPointFromJoint(depthFrame, skeleton, JointType.ShoulderCenter);
                Point centerHip = getPointFromJoint(depthFrame, skeleton, JointType.HipCenter);
                Point head = getPointFromJoint(depthFrame, skeleton, JointType.Head);

                Point leftHip = getPointFromJoint(depthFrame, skeleton, JointType.HipLeft);
                Point leftKnee = getPointFromJoint(depthFrame, skeleton, JointType.KneeLeft);
                Point leftAnkle = getPointFromJoint(depthFrame, skeleton, JointType.AnkleLeft);
                Point rightHip = getPointFromJoint(depthFrame, skeleton, JointType.HipRight);
                Point rightKnee = getPointFromJoint(depthFrame, skeleton, JointType.KneeRight);
                Point rightAnkle = getPointFromJoint(depthFrame, skeleton, JointType.AnkleRight);
                #endregion

                /*
                #region mask bone
                PointCollection leftShoul = new PointCollection();
                leftShoul.Add(leftShoulder);
                leftShoul.Add(centerShoulder);
                drawLineBone2(leftShoul, Brushes.Black, 15);

                PointCollection rightShoul = new PointCollection();
                rightShoul.Add(rightShoulder);
                rightShoul.Add(centerShoulder);
                drawLineBone2(rightShoul, Brushes.Black, 15);

                PointCollection neck = new PointCollection();
                neck.Add(head);
                neck.Add(centerShoulder);
                drawLineBone2(neck, Brushes.Black, 15);

                PointCollection leftH = new PointCollection();
                leftH.Add(centerHip);
                leftH.Add(leftHip);
                drawLineBone2(leftH, Brushes.Black, 15);

                PointCollection rightH = new PointCollection();
                rightH.Add(centerHip);
                rightH.Add(rightHip);
                drawLineBone2(rightH, Brushes.Black, 15);

                PointCollection leftEl = new PointCollection();
                leftEl.Add(leftWrist);
                leftEl.Add(leftElbow);
                drawLineBone2(leftEl, Brushes.Black, 15);

                PointCollection rightEl = new PointCollection();
                rightEl.Add(rightWrist);
                rightEl.Add(rightElbow);
                drawLineBone2(rightEl, Brushes.Black, 15);

                PointCollection leftAn = new PointCollection();
                leftAn.Add(leftAnkle);
                leftAn.Add(leftKnee);
                drawLineBone2(leftAn, Brushes.Black, 15);

                PointCollection rightAn = new PointCollection();
                rightAn.Add(rightAnkle);
                rightAn.Add(rightKnee);
                drawLineBone2(rightAn, Brushes.Black, 15);
                #endregion
                */

                #region kepala
                newBone HeadToCShoulder = new newBone();
                HeadToCShoulder = getNewBone(head, centerShoulder);
                Point HeadC = newPoint(centerShoulder, HeadToCShoulder, HeadToCShoulderCal2);
                #endregion

                #region tubuh
                newBone CShoulderToCHip = new newBone();
                CShoulderToCHip = getNewBone(centerShoulder, centerHip);
                Point cHipC = new Point();
                cHipC.X = centerShoulder.X + (Math.Cos((CShoulderToCHip.angle + 180) * (Math.PI / 180)) * (CShoulderToCHip.length + CShoulderToCHipCal2));
                cHipC.Y = centerShoulder.Y + (Math.Sin((CShoulderToCHip.angle + 180) * (Math.PI / 180)) * (CShoulderToCHip.length + CShoulderToCHipCal2));
                #endregion

                #region tangan kanan
                newBone CShoulderToRShoulder = new newBone();
                CShoulderToRShoulder = getNewBone(rightShoulder, centerShoulder);
                Point rShoulderC = new Point();
                rShoulderC.X = centerShoulder.X + (Math.Cos(CShoulderToRShoulder.angle * (Math.PI / 180.0)) * (CShoulderToRShoulder.length + CShoulderToRShoulderCal2));
                rShoulderC.Y = centerShoulder.Y + (Math.Sin(CShoulderToRShoulder.angle * (Math.PI / 180.0)) * (CShoulderToRShoulder.length + CShoulderToRShoulderCal2));

                newBone RShoulderToRElbow = new newBone();
                RShoulderToRElbow = getNewBone(rightShoulder, rightElbow);
                Point rElbowC = new Point();
                rElbowC.X = rShoulderC.X + (Math.Cos((RShoulderToRElbow.angle + 180) * (Math.PI / 180.0)) * (RShoulderToRElbow.length + RShoulderToRElbowCal2));
                rElbowC.Y = rShoulderC.Y + (Math.Sin((RShoulderToRElbow.angle + 180) * (Math.PI / 180.0)) * (RShoulderToRElbow.length + RShoulderToRElbowCal2));

                newBone RElbowToRHand = new newBone();
                RElbowToRHand = getNewBone(rightElbow, rightHand);
                Point rHandC = new Point();
                rHandC.X = rElbowC.X + (Math.Cos((RElbowToRHand.angle + 180) * (Math.PI / 180.0)) * (RElbowToRHand.length + RElbowToRHandCal2));
                rHandC.Y = rElbowC.Y + (Math.Sin((RElbowToRHand.angle + 180) * (Math.PI / 180.0)) * (RElbowToRHand.length + RElbowToRHandCal2));
                #endregion

                #region tangan kiri
                newBone CShoulderToLShoulder = new newBone();
                CShoulderToLShoulder = getNewBone(leftShoulder, centerShoulder);
                Point lShoulderC = new Point();
                lShoulderC.X = centerShoulder.X + (Math.Cos(CShoulderToLShoulder.angle * (Math.PI / 180.0)) * (CShoulderToLShoulder.length + CShoulderToLShoulderCal2));
                lShoulderC.Y = centerShoulder.Y + (Math.Sin(CShoulderToLShoulder.angle * (Math.PI / 180.0)) * (CShoulderToLShoulder.length + CShoulderToLShoulderCal2));

                newBone LShoulderToLElbow = new newBone();
                LShoulderToLElbow = getNewBone(leftShoulder, leftElbow);
                Point lElbowC = new Point();
                lElbowC.X = lShoulderC.X + (Math.Cos((LShoulderToLElbow.angle + 180) * (Math.PI / 180.0)) * (LShoulderToLElbow.length + LShoulderToLElbowCal2));
                lElbowC.Y = lShoulderC.Y + (Math.Sin((LShoulderToLElbow.angle + 180) * (Math.PI / 180.0)) * (LShoulderToLElbow.length + LShoulderToLElbowCal2));

                newBone LElbowToLHand = new newBone();
                LElbowToLHand = getNewBone(leftElbow, leftHand);
                Point lHandC = new Point();
                lHandC.X = lElbowC.X + (Math.Cos((LElbowToLHand.angle + 180) * (Math.PI / 180.0)) * (LElbowToLHand.length + LElbowToLHandCal2));
                lHandC.Y = lElbowC.Y + (Math.Sin((LElbowToLHand.angle + 180) * (Math.PI / 180.0)) * (LElbowToLHand.length + LElbowToLHandCal2));
                #endregion

                #region pinggang
                newBone CHipToRHip = new newBone();
                CHipToRHip = getNewBone(rightHip, centerHip);
                Point rHipC = new Point();
                //rHipC.X = centerHip.X + (Math.Cos(CHipToRHip.angle * (Math.PI / 180)) * (CHipToRHip.length + CHipToRHipCal2));
                //rHipC.Y = centerHip.Y + (Math.Sin(CHipToRHip.angle * (Math.PI / 180)) * (CHipToRHip.length + CHipToRHipCal2));
                rHipC.X = rightHip.X;
                rHipC.Y = rightHip.Y + CHipToRHipCal2;

                newBone CHipToLHip = new newBone();
                CHipToLHip = getNewBone(leftHip, centerHip);
                Point lHipC = new Point();
                //lHipC.X = centerHip.X + (Math.Cos(CHipToLHip.angle * (Math.PI / 180)) * (CHipToLHip.length + CHipToLHipCal2));
                //lHipC.Y = centerHip.Y + (Math.Sin(CHipToLHip.angle * (Math.PI / 180)) * (CHipToLHip.length + CHipToLHipCal2));
                lHipC.X = leftHip.X;
                lHipC.Y = leftHip.Y + CHipToLHipCal2;
                #endregion

                #region kaki kanan
                newBone RHipToRKnee = new newBone();
                RHipToRKnee = getNewBone(rightHip, rightKnee);
                Point rKneeC = new Point();
                rKneeC.X = rHipC.X + (Math.Cos((RHipToRKnee.angle + 180) * (Math.PI / 180)) * (RHipToRKnee.length + RHipToRKneeCal2));
                rKneeC.Y = rHipC.Y + (Math.Sin((RHipToRKnee.angle + 180) * (Math.PI / 180)) * (RHipToRKnee.length + RHipToRKneeCal2));

                newBone RKneeToRAnkle = new newBone();
                RKneeToRAnkle = getNewBone(rightKnee, rightAnkle);
                Point rAnkleC = new Point();
                rAnkleC.X = rKneeC.X + (Math.Cos((RKneeToRAnkle.angle + 180) * (Math.PI / 180)) * (RKneeToRAnkle.length + RKneeToRAnkleCal2));
                rAnkleC.Y = rKneeC.Y + (Math.Sin((RKneeToRAnkle.angle + 180) * (Math.PI / 180)) * (RKneeToRAnkle.length + RKneeToRAnkleCal2));
                #endregion

                #region kaki kiri
                newBone LHipToLKnee = new newBone();
                LHipToLKnee = getNewBone(leftHip, leftKnee);
                Point lKneeC = new Point();
                lKneeC.X = lHipC.X + (Math.Cos((LHipToLKnee.angle + 180) * (Math.PI / 180)) * (LHipToLKnee.length + LHipToLKneeCal2));
                lKneeC.Y = lHipC.Y + (Math.Sin((LHipToLKnee.angle + 180) * (Math.PI / 180)) * (LHipToLKnee.length + LHipToLKneeCal2));

                newBone LKneeToLAnkle = new newBone();
                LKneeToLAnkle = getNewBone(leftKnee, leftAnkle);
                Point lAnkleC = new Point();
                lAnkleC.X = lKneeC.X + (Math.Cos((LKneeToLAnkle.angle + 180) * (Math.PI / 180)) * (LKneeToLAnkle.length + LKneeToLAnkleCal2));
                lAnkleC.Y = lKneeC.Y + (Math.Sin((LKneeToLAnkle.angle + 180) * (Math.PI / 180)) * (LKneeToLAnkle.length + LKneeToLAnkleCal2));
                #endregion

                #region drawBone
                drawBone(lElbowC, lHandC, LenganKiriBawah2); //
                drawBone(rElbowC, rHandC, LenganKananBawah2); //
                drawBone(lShoulderC, lElbowC, LenganKiriAtas2); //
                drawBone(rShoulderC, rElbowC, LenganKananAtas2); //
                drawBone(centerShoulder, cHipC, Tubuh2); //
                drawBone(HeadC, centerShoulder, Kepala2); //
                drawBone(lHipC, lKneeC, KakiKiriAtas2); //
                drawBone(lKneeC, lAnkleC, KakiKiriBawah2); //
                drawBone(rHipC, rKneeC, KakiKananAtas2); //
                drawBone(rKneeC, rAnkleC, KakiKananBawah2); //
                #endregion
            }
        }

        private void getSkeleton(AllFramesReadyEventArgs e, ref Skeleton skeleton)
        {
            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame == null)
                {
                    return;
                }

                int count = skeletonFrame.SkeletonArrayLength;
                skeletons = new Skeleton[count];
                skeletonFrame.CopySkeletonDataTo(skeletons);
                skeleton = (from s in skeletons where s.TrackingState == SkeletonTrackingState.Tracked select s).FirstOrDefault();
            }
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (kinect.Status == KinectStatus.Connected)
            {
                kinect.Dispose();
            }
        }

        private void nScene_Click(object sender, RoutedEventArgs e)
        {
            idx++;
            if (idx == scenePath.Length)
            {
                idx = scenePath.Length - 1;
            }
        }

        private void pScene_Click(object sender, RoutedEventArgs e)
        {
            idx--;
            if (idx < 0)
            {
                idx = 0;
            }
        }

        #region rasio part
        private void expand_Collapsed(object sender, RoutedEventArgs e)
        {
            this.Width = 650;
        }

        private void expand_Expanded(object sender, RoutedEventArgs e)
        {
            this.Width = 1000;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            kepala1Rasio += 5;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            kepala1Rasio -= 5;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            tubuh1Rasio += 5;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            tubuh1Rasio -= 5;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
           lKa1Rasio += 5;
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            lKa1Rasio -= 5;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            tKa1Rasio += 5;
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            tKa1Rasio -= 5;
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            lKi1Rasio += 5;
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            lKi1Rasio -= 5;
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            tKi1Rasio += 5;
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            tKi1Rasio -= 5;
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            pKa1Rasio += 5;
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            pKa1Rasio -= 5;
        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            kKa1Rasio += 5;
        }

        private void Button_Click_16(object sender, RoutedEventArgs e)
        {
            kKa1Rasio -= 5;
        }

        private void Button_Click_17(object sender, RoutedEventArgs e)
        {
            pKi1Rasio += 5;
        }

        private void Button_Click_18(object sender, RoutedEventArgs e)
        {
            pKi1Rasio -= 5;
        }

        private void Button_Click_19(object sender, RoutedEventArgs e)
        {
            kKi1Rasio += 5;
        }

        private void Button_Click_20(object sender, RoutedEventArgs e)
        {
            kKi1Rasio -= 5;
        }

        private void Button_Click_21(object sender, RoutedEventArgs e)
        {
            kepala2Rasio += 5;
        }

        private void Button_Click_22(object sender, RoutedEventArgs e)
        {
            kepala2Rasio -= 5;
        }

        private void Button_Click_23(object sender, RoutedEventArgs e)
        {
            tubuh2Rasio += 5;
        }

        private void Button_Click_24(object sender, RoutedEventArgs e)
        {
            tubuh2Rasio -= 5; 
        }

        private void Button_Click_25(object sender, RoutedEventArgs e)
        {
            lKa2Rasio += 5;
        }

        private void Button_Click_26(object sender, RoutedEventArgs e)
        {
            lKa2Rasio -= 5;
        }

        private void Button_Click_27(object sender, RoutedEventArgs e)
        {
            tKa2Rasio += 5;
        }

        private void Button_Click_28(object sender, RoutedEventArgs e)
        {
            tKa2Rasio -= 5;
        }

        private void Button_Click_29(object sender, RoutedEventArgs e)
        {
            lKi2Rasio += 5;
        }

        private void Button_Click_30(object sender, RoutedEventArgs e)
        {
            lKi2Rasio -= 5;
        }

        private void Button_Click_31(object sender, RoutedEventArgs e)
        {
            tKi2Rasio += 5;
        }

        private void Button_Click_32(object sender, RoutedEventArgs e)
        {
            tKi2Rasio -= 5;
        }

        private void Button_Click_33(object sender, RoutedEventArgs e)
        {
            pKa2Rasio += 5;
        }

        private void Button_Click_34(object sender, RoutedEventArgs e)
        {
            pKa2Rasio -= 5;
        }

        private void Button_Click_35(object sender, RoutedEventArgs e)
        {
            kKa2Rasio += 5;
        }

        private void Button_Click_36(object sender, RoutedEventArgs e)
        {
            kKa2Rasio -= 5;
        }

        private void Button_Click_37(object sender, RoutedEventArgs e)
        {
            pKi2Rasio += 5;
        }

        private void Button_Click_38(object sender, RoutedEventArgs e)
        {
            pKi2Rasio -= 5;
        }

        private void Button_Click_39(object sender, RoutedEventArgs e)
        {
            kKi2Rasio += 5;
        }

        private void Button_Click_40(object sender, RoutedEventArgs e)
        {
            kKi2Rasio -= 5;
        }
        #endregion

        private void showP1_Click(object sender, RoutedEventArgs e)
        {
            if (showwP1)
            {
                showwP1 = !showwP1;
                showP1.Background = Brushes.Black;
            }
            else
            {
                showwP1 = !showwP1;
                showP1.Background = Brushes.Red;
            }
        }

        private void showP2_Click(object sender, RoutedEventArgs e)
        {
            if (showwP2)
            {
                showwP2 = !showwP2;
                showP2.Background = Brushes.Black;
            }
            else
            {
                showwP2 = !showwP2;
                showP2.Background = Brushes.Red;
            }
        }

        #region calibrasi titik
        private void Button_Click_41(object sender, RoutedEventArgs e)
        {
            HeadToCShoulderCal1++;
        }

        private void Button_Click_42(object sender, RoutedEventArgs e)
        {
            HeadToCShoulderCal1--;
        }

        private void Button_Click_43(object sender, RoutedEventArgs e)
        {
            CShoulderToCHipCal1++;
        }

        private void Button_Click_44(object sender, RoutedEventArgs e)
        {
            CShoulderToCHipCal1--;
        }

        private void Button_Click_45(object sender, RoutedEventArgs e)
        {
            RShoulderToRElbowCal1++;
        }

        private void Button_Click_46(object sender, RoutedEventArgs e)
        {
            RShoulderToRElbowCal1--;
        }

        private void Button_Click_47(object sender, RoutedEventArgs e)
        {
            RElbowToRHandCal1++;
        }

        private void Button_Click_48(object sender, RoutedEventArgs e)
        {
            RElbowToRHandCal1--;
        }

        private void Button_Click_49(object sender, RoutedEventArgs e)
        {
            LShoulderToLElbowCal1++;
        }

        private void Button_Click_50(object sender, RoutedEventArgs e)
        {
            LShoulderToLElbowCal1--;
        }

        private void Button_Click_51(object sender, RoutedEventArgs e)
        {
            LElbowToLHandCal1++;
        }

        private void Button_Click_52(object sender, RoutedEventArgs e)
        {
            LElbowToLHandCal1--;
        }

        private void Button_Click_53(object sender, RoutedEventArgs e)
        {
            RHipToRKneeCal1++;
        }

        private void Button_Click_54(object sender, RoutedEventArgs e)
        {
            RHipToRKneeCal1--;
        }

        private void Button_Click_55(object sender, RoutedEventArgs e)
        {
            RKneeToRAnkleCal1++;
        }

        private void Button_Click_56(object sender, RoutedEventArgs e)
        {
            RKneeToRAnkleCal1--;
        }

        private void Button_Click_57(object sender, RoutedEventArgs e)
        {
            LHipToLKneeCal1++;
        }

        private void Button_Click_58(object sender, RoutedEventArgs e)
        {
            LHipToLKneeCal1--;
        }

        private void Button_Click_59(object sender, RoutedEventArgs e)
        {
            LKneeToLAnkleCal1++;
        }

        private void Button_Click_60(object sender, RoutedEventArgs e)
        {
            LKneeToLAnkleCal1--;
        }

        private void Button_Click_61(object sender, RoutedEventArgs e)
        {
            CShoulderToRShoulderCal1++;
        }

        private void Button_Click_62(object sender, RoutedEventArgs e)
        {
            CShoulderToRShoulderCal1--;
        }

        private void Button_Click_63(object sender, RoutedEventArgs e)
        {
            CShoulderToLShoulderCal1++;
        }

        private void Button_Click_64(object sender, RoutedEventArgs e)
        {
            CShoulderToLShoulderCal1--;
        }

        private void Button_Click_65(object sender, RoutedEventArgs e)
        {
            CHipToRHipCal1++;
        }

        private void Button_Click_66(object sender, RoutedEventArgs e)
        {
            CHipToRHipCal1--;
        }

        private void Button_Click_67(object sender, RoutedEventArgs e)
        {
            CHipToLHipCal1++;
        }

        private void Button_Click_68(object sender, RoutedEventArgs e)
        {
            CHipToLHipCal1--;
        }

        private void Button_Click_69(object sender, RoutedEventArgs e)
        {
            HeadToCShoulderCal2++;
        }

        private void Button_Click_70(object sender, RoutedEventArgs e)
        {
            HeadToCShoulderCal2--;
        }

        private void Button_Click_71(object sender, RoutedEventArgs e)
        {
            CShoulderToCHipCal2++;
        }

        private void Button_Click_72(object sender, RoutedEventArgs e)
        {
            CShoulderToCHipCal2--;
        }

        private void Button_Click_73(object sender, RoutedEventArgs e)
        {
            RShoulderToRElbowCal2++;
        }

        private void Button_Click_74(object sender, RoutedEventArgs e)
        {
            RShoulderToRElbowCal2--;
        }

        private void Button_Click_75(object sender, RoutedEventArgs e)
        {
            RElbowToRHandCal2++;
        }

        private void Button_Click_76(object sender, RoutedEventArgs e)
        {
            RElbowToRHandCal2--;
        }

        private void Button_Click_77(object sender, RoutedEventArgs e)
        {
            LShoulderToLElbowCal2++;
        }

        private void Button_Click_78(object sender, RoutedEventArgs e)
        {
            LShoulderToLElbowCal2--;
        }

        private void Button_Click_79(object sender, RoutedEventArgs e)
        {
            LElbowToLHandCal2++;
        }

        private void Button_Click_80(object sender, RoutedEventArgs e)
        {
            LElbowToLHandCal2--;
        }

        private void Button_Click_81(object sender, RoutedEventArgs e)
        {
            RHipToRKneeCal2++;
        }

        private void Button_Click_82(object sender, RoutedEventArgs e)
        {
            RHipToRKneeCal2--;
        }

        private void Button_Click_83(object sender, RoutedEventArgs e)
        {
            RKneeToRAnkleCal2++;
        }

        private void Button_Click_84(object sender, RoutedEventArgs e)
        {
            RKneeToRAnkleCal2--;
        }

        private void Button_Click_85(object sender, RoutedEventArgs e)
        {
            LHipToLKneeCal2++;
        }

        private void Button_Click_86(object sender, RoutedEventArgs e)
        {
            LHipToLKneeCal2--;
        }

        private void Button_Click_87(object sender, RoutedEventArgs e)
        {
            LKneeToLAnkleCal2++;
        }

        private void Button_Click_88(object sender, RoutedEventArgs e)
        {
            LKneeToLAnkleCal2--;
        }

        private void Button_Click_89(object sender, RoutedEventArgs e)
        {
            CShoulderToRShoulderCal2++;
        }

        private void Button_Click_90(object sender, RoutedEventArgs e)
        {
            CShoulderToRShoulderCal2--;
        }

        private void Button_Click_91(object sender, RoutedEventArgs e)
        {
            CShoulderToLShoulderCal2++;
        }

        private void Button_Click_92(object sender, RoutedEventArgs e)
        {
            CShoulderToLShoulderCal2--;
        }

        private void Button_Click_93(object sender, RoutedEventArgs e)
        {
            CHipToRHipCal2++;
        }

        private void Button_Click_94(object sender, RoutedEventArgs e)
        {
            CHipToRHipCal2--;
        }

        private void Button_Click_95(object sender, RoutedEventArgs e)
        {
            CHipToLHipCal2++;
        }

        private void Button_Click_96(object sender, RoutedEventArgs e)
        {
            CHipToLHipCal2--;
        }
        #endregion

        private void Button_Click_97(object sender, RoutedEventArgs e)
        {
            komponen.Children.RemoveRange(0, komponen.Children.Count);

            this.Left = 0;
            this.Top = 0;
            double width = SystemParameters.PrimaryScreenWidth;
            double height = SystemParameters.PrimaryScreenHeight;
            double scaleY = height / canvas.Height;
            double scaleX = width / canvas.Width;
            this.Width = width;
            this.Height = height;
            ScaleTransform scale = new ScaleTransform(scaleX, scaleY);
            canvas.RenderTransform = scale;
            //canvas.RenderTransform = scale;
        }

        private void Button_Click_98(object sender, RoutedEventArgs e)
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private void saveCon1_Click(object sender, RoutedEventArgs e)
        {
            p1con.CHipToLHipCal1 = CHipToLHipCal1;
            p1con.CHipToRHipCal1 = CHipToRHipCal1;
            p1con.CShoulderToCHipCal1 = CShoulderToCHipCal1;
            p1con.CShoulderToLShoulderCal1 = CShoulderToLShoulderCal1;
            p1con.CShoulderToRShoulderCal1 = CShoulderToRShoulderCal1;
            p1con.HeadToCShoulderCal1 = HeadToCShoulderCal1;
            p1con.kepala1Rasio = kepala1Rasio;
            p1con.kKa1Rasio = kKa1Rasio;
            p1con.kKi1Rasio = kKi1Rasio;
            p1con.LElbowToLHandCal1 = LElbowToLHandCal1;
            p1con.LHipToLKneeCal1 = LHipToLKneeCal1;
            p1con.lKa1Rasio = lKa1Rasio;
            p1con.lKi1Rasio = lKi1Rasio;
            p1con.LKneeToLAnkleCal1 = LKneeToLAnkleCal1;
            p1con.LShoulderToLElbowCal1 = LShoulderToLElbowCal1;
            p1con.pKa1Rasio = pKa1Rasio;
            p1con.pKi1Rasio = pKi1Rasio;
            p1con.RElbowToRHandCal1 = RElbowToRHandCal1;
            p1con.RHipToRKneeCal1 = RHipToRKneeCal1;
            p1con.RKneeToRAnkleCal1 = RKneeToRAnkleCal1;
            p1con.RShoulderToRElbowCal1 = RShoulderToRElbowCal1;
            p1con.tKa1Rasio = tKa1Rasio;
            p1con.tKi1Rasio = tKi1Rasio;
            p1con.tubuh1Rasio = tubuh1Rasio;

            SaveFileDialog sd = new SaveFileDialog();
            sd.Filter = "XML Files |*.xml";
            sd.ShowDialog();
            if (sd.FileName != "")
            {
                xml.saveData(p1con, sd.FileName);
            }
        }

        private void loadCon1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "XML Files |*.xml";
            if (od.ShowDialog() == true)
            {
                XmlSerializer rd = new XmlSerializer(typeof(Player1Config));
                FileStream read = new FileStream(od.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                Player1Config info = (Player1Config)rd.Deserialize(read);
                
                tubuh1Rasio = info.tubuh1Rasio;
                kepala1Rasio = info.kepala1Rasio;
                tKa1Rasio = info.tKa1Rasio;
                tKi1Rasio = info.tKi1Rasio;
                kKa1Rasio = info.kKa1Rasio;
                kKi1Rasio = info.kKi1Rasio;
                pKa1Rasio = info.pKa1Rasio;
                pKi1Rasio = info.pKi1Rasio;
                lKa1Rasio = info.lKa1Rasio;
                lKi1Rasio = info.lKi1Rasio;
                CHipToLHipCal1 = info.CHipToLHipCal1;
                CHipToRHipCal1 = info.CHipToRHipCal1;
                CShoulderToCHipCal1 = info.CShoulderToCHipCal1;
                CShoulderToLShoulderCal1 = info.CShoulderToLShoulderCal1;
                CShoulderToRShoulderCal1 = info.CShoulderToRShoulderCal1;
                HeadToCShoulderCal1 = info.HeadToCShoulderCal1;
                LElbowToLHandCal1 = info.LElbowToLHandCal1;
                LHipToLKneeCal1 = info.LHipToLKneeCal1;
                LKneeToLAnkleCal1 = info.LKneeToLAnkleCal1;
                LShoulderToLElbowCal1 = info.LShoulderToLElbowCal1;
                RElbowToRHandCal1 = info.RElbowToRHandCal1;
                RHipToRKneeCal1 = info.RHipToRKneeCal1;
                RKneeToRAnkleCal1 = info.RKneeToRAnkleCal1;
                RShoulderToRElbowCal1 = info.RShoulderToRElbowCal1;

                read.Dispose();
            }
        }

        private void saveCon2_Click(object sender, RoutedEventArgs e)
        {
            p2con.CHipToLHipCal2 = CHipToLHipCal2;
            p2con.CHipToRHipCal2 = CHipToRHipCal2;
            p2con.CShoulderToCHipCal2 = CShoulderToCHipCal2;
            p2con.CShoulderToLShoulderCal2 = CShoulderToLShoulderCal2;
            p2con.CShoulderToRShoulderCal2 = CShoulderToRShoulderCal2;
            p2con.HeadToCShoulderCal2 = HeadToCShoulderCal2;
            p2con.LElbowToLHandCal2 = LElbowToLHandCal2;
            p2con.LHipToLKneeCal2 = LHipToLKneeCal2;
            p2con.LKneeToLAnkleCal2 = LKneeToLAnkleCal2;
            p2con.LShoulderToLElbowCal2 = LShoulderToLElbowCal2;
            p2con.RElbowToRHandCal2 = RElbowToRHandCal2;
            p2con.RHipToRKneeCal2 = RHipToRKneeCal2;
            p2con.RKneeToRAnkleCal2 = RKneeToRAnkleCal2;
            p2con.RShoulderToRElbowCal2 = RShoulderToRElbowCal2;
            p2con.tKa2Rasio = tKa2Rasio;
            p2con.tKi2Rasio = tKi2Rasio;
            p2con.tubuh2Rasio = tubuh2Rasio;
            p2con.kepala2Rasio = kepala2Rasio;
            p2con.kKa2Rasio = kKa2Rasio;
            p2con.kKi2Rasio = kKi2Rasio;
            p2con.pKa2Rasio = pKa2Rasio;
            p2con.pKi2Rasio = pKi2Rasio;
            p2con.lKa2Rasio = lKa2Rasio;
            p2con.lKi2Rasio = lKi2Rasio;

            SaveFileDialog sd = new SaveFileDialog();
            sd.Filter = "XML Files |*.xml";
            sd.ShowDialog();
            if (sd.FileName != "")
            {
                xml.saveData(p2con, sd.FileName);
            }
        }

        private void loadCon2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "XML Files |*.xml";
            if (od.ShowDialog() == true)
            {
                XmlSerializer rd = new XmlSerializer(typeof(Player2Config));
                FileStream read = new FileStream(od.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                Player2Config info = (Player2Config)rd.Deserialize(read);

                tubuh2Rasio = info.tubuh2Rasio;
                kepala2Rasio = info.kepala2Rasio;
                tKa2Rasio = info.tKa2Rasio;
                tKi2Rasio = info.tKi2Rasio;
                kKa2Rasio = info.kKa2Rasio;
                kKi2Rasio = info.kKi2Rasio;
                pKa2Rasio = info.pKa2Rasio;
                pKi2Rasio = info.pKi2Rasio;
                lKa2Rasio = info.lKa2Rasio;
                lKi2Rasio = info.lKi2Rasio;
                CHipToLHipCal2 = info.CHipToLHipCal2;
                CHipToRHipCal2 = info.CHipToRHipCal2;
                CShoulderToCHipCal2 = info.CShoulderToCHipCal2;
                CShoulderToLShoulderCal2 = info.CShoulderToLShoulderCal2;
                CShoulderToRShoulderCal2 = info.CShoulderToRShoulderCal2;
                HeadToCShoulderCal2 = info.HeadToCShoulderCal2;
                LElbowToLHandCal2 = info.LElbowToLHandCal2;
                LHipToLKneeCal2 = info.LHipToLKneeCal2;
                LKneeToLAnkleCal2 = info.LKneeToLAnkleCal2;
                LShoulderToLElbowCal2 = info.LShoulderToLElbowCal2;
                RElbowToRHandCal2 = info.RElbowToRHandCal2;
                RHipToRKneeCal2 = info.RHipToRKneeCal2;
                RKneeToRAnkleCal2 = info.RKneeToRAnkleCal2;
                RShoulderToRElbowCal2 = info.RShoulderToRElbowCal2;

                read.Dispose();
            }
        }
    }
}
