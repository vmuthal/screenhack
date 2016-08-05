using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenHack
{
    public partial class MainForm : Form
    {
        Bitmap bmp;
        Boolean isStarted = false;
        
        public MainForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
            {
                modifyImageTimer.Enabled = false;
                Application.Exit();
            }
            if (e.KeyCode == Keys.F10)
            {
                this.ShowInTaskbar = true;
                this.WindowState = FormWindowState.Minimized;

                //Create a new bitmap.
                var bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                               Screen.PrimaryScreen.Bounds.Height,
                                               PixelFormat.Format32bppArgb);

                // Create a graphics object from the bitmap.
                var gfxScreenshot = Graphics.FromImage(bmpScreenshot);

                // Take the screenshot from the upper left corner to the right bottom corner.
                gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                            Screen.PrimaryScreen.Bounds.Y,
                                            0,
                                            0,
                                            Screen.PrimaryScreen.Bounds.Size,
                                            CopyPixelOperation.SourceCopy);

                // Save the screenshot to the specified path that the user has chosen.
                bmpScreenshot.Save("Screenshot.png", ImageFormat.Png);

                bmp = (Bitmap)Bitmap.FromFile("Screenshot.png");
                pbImage.Image = bmp;

                labelAuthor.Visible = false;
                labelInfo.Visible = false;
                labelTitle.Visible = false;

                isStarted = true;

                this.ShowInTaskbar = false;
                this.WindowState = FormWindowState.Maximized;

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random rnd = new Random();

            for (int x = rnd.Next(0, bmp.Width); x < bmp.Width; x++)
            {
                for (int y = rnd.Next(0, bmp.Height); y < bmp.Height; y++)
                {
                    if (rnd.Next(0, 100) % 2 == 0)
                        bmp.SetPixel(x, y, Color.Black);                    
                }
            }

            pbImage.Image = bmp;
            
        }

        private void pbImage_Click(object sender, EventArgs e)
        {
            if (!modifyImageTimer.Enabled && isStarted)
            {
                isStarted = true;
                modifyImageTimer.Enabled = true;
            }
        }

    }
}
