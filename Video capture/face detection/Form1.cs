using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace face_detection
{
    public partial class videocapture : Form
    {
        VideoCapture capture;
        bool Pause = false;

        public videocapture()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog()==DialogResult.OK )
            {
                capture = new VideoCapture(ofd.FileName);
                Mat m = new Mat();
                capture.Read(m);                        //read method reads the data from frame to frame
                pictureBox1.Image = m.Bitmap;
            }
        }

        private async void playToolStripMenuItem_Click(object sender, EventArgs e)  // async to create delay
        {
            if (capture==null)
            {
                return;
            }

            try
            {

                while (!Pause)
                {
                    Mat m = new Mat();
                    capture.Read(m);

                    if (!m.IsEmpty)
                    {
                        pictureBox2.Image = m.Bitmap;
                        double fps = capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps);
                        await Task.Delay(1000/Convert.ToInt32(fps));
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch
            {

            }
        }
    }
}
