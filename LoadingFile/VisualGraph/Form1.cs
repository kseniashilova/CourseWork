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

namespace VisualGraph
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string[] strings;
        List<Tuple<string, string, string, int>> lst;
        private void Form1_Load(object sender, EventArgs e)
        {
            //Полный путь.
            const string path =
                "C:/Users/Пользователь/Desktop/CourseWork/table/traced-roi-connections.csv";
            strings = File.ReadAllLines(path); //считываем по строкам
            lst = GraphHelper.GeneralPropertiesHelper.GetTuples(strings);
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            Image img = new Bitmap(pb.Width, pb.Height);
            pb.Image = img;
            Graphics gr = Graphics.FromImage(pb.Image);
            Pen pen = new Pen(Color.FromArgb(0, 100, 100));


            List<Tuple<string, string, string, int>> ME =
                GraphHelper.GroupHelper.FindRegions(lst,
                new string[] { "LO(L)", "LO(R)" });
            //рисуем
            GraphHelper.VisualizationHelper.DrawSmallGraphRandom
                (ME, pb, pen, Color.FromArgb(50, 50, 50), Color.FromArgb(255, 90, 60));
            //GraphHelper.VisualizationHelper.DrawBigGraphRandom
            //    (ME, pb, pen);
        }
    }
}
