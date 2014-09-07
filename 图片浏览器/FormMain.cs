using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace 图片浏览器
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void showPicByFolder(string folderPath)
        {
            string[] fileNames = Directory.GetFiles(folderPath,"*jpg");//获取指定目录文件中的文件名称
            if (fileNames == null || fileNames.Length == 0)
            {
                return;
            }

            int pictureCount = fileNames.Length;//获取所取图片的张数

            int columnCount=6;//每行显示6张图片

            //得到适合所取图片张数的显示行数
            int rowCount = (pictureCount % columnCount == 0) ? pictureCount / columnCount : (pictureCount / columnCount) + 1;

            int padding = 2;
            int pictureWidth = this.panPicShow.Width / columnCount - 2 * padding;//图片的宽
            int pictureHeight = pictureWidth * 9 / 16;//图片为16*9比例

            
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex <columnCount ; columnIndex++)
                {
                    int index = rowIndex * columnCount + columnIndex;//图片下标
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;//图片按其原有的比例加载
                    //设置pictureBox的大小和图片一样
                    pictureBox.Width = pictureWidth;
                    pictureBox.Height = pictureHeight;

                    if(index >= pictureCount)
                    {
                        return ;
                    }
                    pictureBox.Image = Image.FromFile(fileNames[index]);//将图片加载到pictureBox中

                    Point pictureLoction = new Point();
                    pictureLoction.X = padding * (columnIndex + 1) + pictureWidth * columnIndex;
                    pictureLoction.Y = padding * (rowIndex + 1) + pictureHeight * rowIndex;
                    pictureBox.Location = pictureLoction;

                    pictureBox.DoubleClick += pictureBox_DoubleClick;

                    this.panPicShow.Controls.Add(pictureBox);


                }   
            }
        }

        void pictureBox_DoubleClick(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            Image image = pictureBox.Image;//获取该PictureBox控件中的图像
            FormLarge formLarge = new FormLarge();
            formLarge.BackgroundImage = image;
            formLarge.BackgroundImageLayout = ImageLayout.Zoom;
            formLarge.Show();
        }


        private void btnOk_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog(); //新建打开对话框

            //folderDlg.SelectedPath = "F:\\照片\\乱七八糟\\贴图"; //设置默认路径
            // folderDlg.SelectedPath = @"F:\照片\乱七八糟\贴图";
            folderDlg.ShowNewFolderButton = false; //取消“新建文件夹”按钮
            DialogResult dlgResult = folderDlg.ShowDialog(); //得到所取图片
            

            if (dlgResult == DialogResult.Cancel)
            {
                return;
            }
            string path = folderDlg.SelectedPath;

            this.txtWay.Text = path;
            panPicShow.BackColor = Color.Black;
            showPicByFolder(path);
        
        }
    }
}
