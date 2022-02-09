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
using Microsoft.Win32;

using kinect1;
using System.IO;
using System.Xml.Serialization;

namespace open1
{
    public partial class MainWindow : Window
    {
        Info info1 = new Info();
        Info info2 = new Info();
        Info path = new Info();
        string[] path1 = new string[10];
        string[] path2 = new string[10];
        int horizon = 100;
        int vertic = 70;
        int limitH = 5;
        int sceneW = 80, sceneH = 60, sceneLeft = 780, sceneTop = 30, sceneIdx = 0, hIdx = 0, vIdx = 0;
        Image[] scene = new Image[50];
        string[] scenePath = new string[50];
        string[] data = new string[50];
        int idx = 0;
        bool sceneB = false;

        public MainWindow()
        {
            InitializeComponent();
            StartBtn.IsEnabled = false;
            checkImage();
            saveP1.IsEnabled = false;
            saveP2.IsEnabled = false;
        }

        private void checkImage()
        {
            for (int i = 0; i < 10; i++)
            {
                if (path1[i] == null)
                {
                    return;
                }
            }
            saveP1.IsEnabled = true;

            for (int i = 0; i < 10; i++)
            {
                if (path2[i] == null)
                {
                    return;
                }
            }
            saveP2.IsEnabled = true;

            if (sceneB == false)
            {
                return;
            }

            StartBtn.IsEnabled = true;
        }

        private void cKepala1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == true)
            {
                srcKepala1.Source = new BitmapImage(new Uri(op.FileName));
                path1[0] = op.FileName;
            }
            checkImage();
        }

        private void cLenganAtasKanan1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == true)
            {
                srcLenganAtasKanan1.Source = new BitmapImage(new Uri(op.FileName));
                path1[4] = op.FileName;
                checkImage();
            }
        }

        private void cLenganBawahKanan1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == true)
            {
                srcLenganBawahKanan1.Source = new BitmapImage(new Uri(op.FileName));
                path1[5] = op.FileName;
                checkImage();
            }
        }

        private void cKakiAtasKanan1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == true)
            {
                srcKakiAtasKanan1.Source = new BitmapImage(new Uri(op.FileName));
                path1[8] = op.FileName;
                checkImage();
            }
        }

        private void cKakiBawahKanan1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == true)
            {
                srcKakiBawahKanan1.Source = new BitmapImage(new Uri(op.FileName));
                path1[9] = op.FileName;
                checkImage();
            }
        }

        private void cTubuh1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == true)
            {
                srcTubuh1.Source = new BitmapImage(new Uri(op.FileName));
                path1[1] = op.FileName;
                checkImage();
            }
        }

        private void cLenganAtasKiri1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == true)
            {
                srcLenganAtasKiri1.Source = new BitmapImage(new Uri(op.FileName));
                path1[2] = op.FileName;
                checkImage();
            }
        }

        private void cLenganBawahKiri1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == true)
            {
                srcLenganBawahKiri1.Source = new BitmapImage(new Uri(op.FileName));
                path1[3] = op.FileName;
                checkImage();
            }
        }

        private void cKakiAtasKiri1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == true)
            {
                srcKakiAtasKiri1.Source = new BitmapImage(new Uri(op.FileName));
                path1[6] = op.FileName;
                checkImage();
            }
        }

        private void cKakiBawahKiri1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == true)
            {
                srcKakiBawahKiri1.Source = new BitmapImage(new Uri(op.FileName));
                path1[7] = op.FileName;
                checkImage();
            }
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            info1.kepalaPath = path1[0];
            info1.tubuhPath = path1[1];
            info1.LKiAPath = path1[2];
            info1.LKiBPath = path1[3];
            info1.LKaAPath = path1[4];
            info1.LKaBPath = path1[5];
            info1.KKiAPath = path1[6];
            info1.KKiBPath = path1[7];
            info1.KKaAPath = path1[8];
            info1.KKaBPath = path1[9];
            xml.saveData(info1, "SemuaPlayer1.xml");

            info2.kepalaPath = path2[0];
            info2.tubuhPath = path2[1];
            info2.LKiAPath = path2[2];
            info2.LKiBPath = path2[3];
            info2.LKaAPath = path2[4];
            info2.LKaBPath = path2[5];
            info2.KKiAPath = path2[6];
            info2.KKiBPath = path2[7];
            info2.KKaAPath = path2[8];
            info2.KKaBPath = path2[9];
            xml.saveData(info2, "SemuaPlayer2.xml");

            path.ScenePath = data;
            xml.saveData(path, "semuaSceneJadiSatu.xml");

            kinect1.MainWindow kinect = new kinect1.MainWindow();
            kinect.Show();
            this.Close();
        }

        private void cKepala2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == true)
            {
                srcKepala2.Source = new BitmapImage(new Uri(op.FileName));
                path2[0] = op.FileName;
            }
            checkImage();
        }

        private void cTubuh2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == true)
            {
                srcTubuh2.Source = new BitmapImage(new Uri(op.FileName));
                path2[1] = op.FileName;
                checkImage();
            }
        }

        private void cLenganAtasKiri2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == true)
            {
                srcLenganAtasKiri2.Source = new BitmapImage(new Uri(op.FileName));
                path2[2] = op.FileName;
                checkImage();
            }
        }

        private void cLenganBawahKiri2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == true)
            {
                srcLenganBawahKiri2.Source = new BitmapImage(new Uri(op.FileName));
                path2[3] = op.FileName;
                checkImage();
            }
        }

        private void cLenganAtasKanan2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == true)
            {
                srcLenganAtasKanan2.Source = new BitmapImage(new Uri(op.FileName));
                path2[4] = op.FileName;
                checkImage();
            }
        }

        private void cLenganBawahKanan2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == true)
            {
                srcLenganBawahKanan2.Source = new BitmapImage(new Uri(op.FileName));
                path2[5] = op.FileName;
                checkImage();
            }
        }

        private void cKakiAtasKiri2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == true)
            {
                srcKakiAtasKiri2.Source = new BitmapImage(new Uri(op.FileName));
                path2[6] = op.FileName;
                checkImage();
            }
        }

        private void cKakiAtasKanan2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == true)
            {
                srcKakiAtasKanan2.Source = new BitmapImage(new Uri(op.FileName));
                path2[8] = op.FileName;
                checkImage();
            }
        }

        private void cKakiBawahKiri2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == true)
            {
                srcKakiBawahKiri2.Source = new BitmapImage(new Uri(op.FileName));
                path2[7] = op.FileName;
                checkImage();
            }
        }

        private void cKakiBawahKanan2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == true)
            {
                srcKakiBawahKanan2.Source = new BitmapImage(new Uri(op.FileName));
                path2[9] = op.FileName;
                checkImage();
            }
        }
        
        private void tScene_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == true)
            {
                scene[sceneIdx] = new Image();
                scene[sceneIdx].Height = sceneH;
                scene[sceneIdx].Width = sceneW;
                scene[sceneIdx].Stretch = Stretch.Fill;
                Canvas.SetLeft(scene[sceneIdx], sceneLeft + (hIdx * horizon));
                Canvas.SetTop(scene[sceneIdx], sceneTop + (vIdx * vertic));
                mainCanvas.Children.Add(scene[sceneIdx]);
                scenePath[sceneIdx] = op.FileName;
                scene[sceneIdx].Source = new BitmapImage(new Uri(op.FileName));

                sceneIdx++;
                hIdx++;
                if (hIdx == 5)
                {
                    vIdx++;
                    hIdx = 0;
                }
            }
        }

        private void rScene_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < sceneIdx; i++)
            {
                scene[i].Source = null;
            }
            sceneIdx = 0;
            hIdx = 0;
            vIdx = 0;
        }

        private void sScene_Click(object sender, RoutedEventArgs e)
        {
            data = new string[sceneIdx];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = scenePath[i];
            }

            SaveFileDialog sd = new SaveFileDialog();
            sd.Filter = "XML Files |*.xml";
            sd.ShowDialog();
            if (sd.FileName != "")
            {
                path.ScenePath = data;
                xml.saveData(path, sd.FileName);
                sceneB = true;
            }
        }

        private void saveP1_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.Filter = "XML Files |*.xml";
            sd.ShowDialog();
            if (sd.FileName != "")
            {
                info1.kepalaPath = path1[0];
                info1.tubuhPath = path1[1];
                info1.LKiAPath = path1[2];
                info1.LKiBPath = path1[3];
                info1.LKaAPath = path1[4];
                info1.LKaBPath = path1[5];
                info1.KKiAPath = path1[6];
                info1.KKiBPath = path1[7];
                info1.KKaAPath = path1[8];
                info1.KKaBPath = path1[9];
                xml.saveData(info1, sd.FileName);
            }
        }

        private void loadP1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "XML Files |*xml";
            if(od.ShowDialog() == true)
            {
                XmlSerializer rd = new XmlSerializer(typeof(Info));
                FileStream read = new FileStream(od.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                Info info = (Info)rd.Deserialize(read);
                srcKepala1.Source = new BitmapImage(new Uri(info.kepalaPath));
                path1[0] = info.kepalaPath;
                srcTubuh1.Source = new BitmapImage(new Uri(info.tubuhPath));
                path1[1] = info.tubuhPath;
                srcLenganAtasKiri1.Source = new BitmapImage(new Uri(info.LKiAPath));
                path1[2] = info.LKiAPath;
                srcLenganBawahKiri1.Source = new BitmapImage(new Uri(info.LKiBPath));
                path1[3] = info.LKiBPath;
                srcLenganAtasKanan1.Source = new BitmapImage(new Uri(info.LKaAPath));
                path1[4] = info.LKaAPath;
                srcLenganBawahKanan1.Source = new BitmapImage(new Uri(info.LKaBPath));
                path1[5] = info.LKaBPath;
                srcKakiAtasKiri1.Source = new BitmapImage(new Uri(info.KKiAPath));
                path1[6] = info.KKiAPath;
                srcKakiBawahKiri1.Source = new BitmapImage(new Uri(info.KKiBPath));
                path1[7] = info.KKiBPath;
                srcKakiAtasKanan1.Source = new BitmapImage(new Uri(info.KKaAPath));
                path1[8] = info.KKaAPath;
                srcKakiBawahKanan1.Source = new BitmapImage(new Uri(info.KKaBPath));
                path1[9] = info.KKaBPath;

                read.Dispose();
            }

            checkImage();
        }

        private void saveP2_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.Filter = "XML Files |*.xml";
            sd.ShowDialog();
            if (sd.FileName != "")
            {
                info2.kepalaPath = path2[0];
                info2.tubuhPath = path2[1];
                info2.LKiAPath = path2[2];
                info2.LKiBPath = path2[3];
                info2.LKaAPath = path2[4];
                info2.LKaBPath = path2[5];
                info2.KKiAPath = path2[6];
                info2.KKiBPath = path2[7];
                info2.KKaAPath = path2[8];
                info2.KKaBPath = path2[9];
                xml.saveData(info2, sd.FileName);
            }
        }

        private void loadP2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "XML Files |*xml";
            if (od.ShowDialog() == true)
            {
                XmlSerializer rd = new XmlSerializer(typeof(Info));
                FileStream read = new FileStream(od.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                Info info = (Info)rd.Deserialize(read);
                srcKepala2.Source = new BitmapImage(new Uri(info.kepalaPath));
                path2[0] = info.kepalaPath;
                srcTubuh2.Source = new BitmapImage(new Uri(info.tubuhPath));
                path2[1] = info.tubuhPath;
                srcLenganAtasKiri2.Source = new BitmapImage(new Uri(info.LKiAPath));
                path2[2] = info.LKiAPath;
                srcLenganBawahKiri2.Source = new BitmapImage(new Uri(info.LKiBPath));
                path2[3] = info.LKiBPath;
                srcLenganAtasKanan2.Source = new BitmapImage(new Uri(info.LKaAPath));
                path2[4] = info.LKaAPath;
                srcLenganBawahKanan2.Source = new BitmapImage(new Uri(info.LKaBPath));
                path2[5] = info.LKaBPath;
                srcKakiAtasKiri2.Source = new BitmapImage(new Uri(info.KKiAPath));
                path2[6] = info.KKiAPath;
                srcKakiBawahKiri2.Source = new BitmapImage(new Uri(info.KKiBPath));
                path2[7] = info.KKiBPath;
                srcKakiAtasKanan2.Source = new BitmapImage(new Uri(info.KKaAPath));
                path2[8] = info.KKaAPath;
                srcKakiBawahKanan2.Source = new BitmapImage(new Uri(info.KKaBPath));
                path2[9] = info.KKaBPath;

                read.Dispose();
            }

            checkImage();
        }

        private void lScene_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "XML Files |*.xml";
            if (od.ShowDialog() == true)
            {
                XmlSerializer rd = new XmlSerializer(typeof(Info));
                FileStream read = new FileStream(od.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                Info info = (Info)rd.Deserialize(read);
                scenePath = info.ScenePath;
                for (int i = 0; i < scenePath.Length; i++)
                {
                    scene[sceneIdx] = new Image();
                    scene[sceneIdx].Height = sceneH;
                    scene[sceneIdx].Width = sceneW;
                    scene[sceneIdx].Stretch = Stretch.Fill;
                    Canvas.SetLeft(scene[sceneIdx], sceneLeft + (hIdx * horizon));
                    Canvas.SetTop(scene[sceneIdx], sceneTop + (vIdx * vertic));
                    mainCanvas.Children.Add(scene[sceneIdx]);
                    scene[sceneIdx].Source = new BitmapImage(new Uri(scenePath[i]));

                    sceneIdx++;
                    hIdx++;
                    if (hIdx == 5)
                    {
                        vIdx++;
                        hIdx = 0;
                    }
                }

                data = new string[sceneIdx];
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = scenePath[i];
                }

                sceneB = true;
            }

            checkImage();
        }
    }
}
