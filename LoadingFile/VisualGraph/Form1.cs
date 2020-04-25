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
        List<string> regs;
        private void Form1_Load(object sender, EventArgs e)
        {
            //Полный путь.
            const string path =
                "C:/Users/Пользователь/Desktop/CourseWork/table/traced-roi-connections.csv";
            strings = File.ReadAllLines(path); //считываем по строкам
            lst = GraphHelper.GeneralPropertiesHelper.GetTuples(strings);

            regs = GraphHelper.GeneralPropertiesHelper.GetRegions(lst);
            regs = GraphHelper.GroupHelper.SortRegionsBySize(regs, lst);
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            Image img = new Bitmap(pb.Width, pb.Height);
            pb.Image = img;
            Graphics gr = Graphics.FromImage(pb.Image);
            Pen pen = new Pen(Color.FromArgb(0, 100, 100));



            //рисуем
            if(currentList.Count<1000)
            GraphHelper.VisualizationHelper.DrawSmallGraphRandom
                (currentList, pb, pen);
            else
                GraphHelper.VisualizationHelper.DrawBigGraphRandom
                (currentList, pb, pen);

        }

        List<Tuple<string, string, string, int>> currentList;
        private void btnMe_Click(object sender, EventArgs e)
        {
            currentList = GraphHelper.GroupHelper.FindRegions(lst,
                new string[] { "ME(L)", "ME(R)" });
        }

        private void btnLO_Click(object sender, EventArgs e)
        {
            currentList = GraphHelper.GroupHelper.FindRegions(lst,
                new string[] { "LO(L)", "LO(R)" });
        }

        private void btnLOP_Click(object sender, EventArgs e)
        {
            currentList = GraphHelper.GroupHelper.FindRegions(lst,
                new string[] { "LOP(L)", "LOP(R)" });
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
//Количество петель
//Количество двойных ребер
//Коэффициент кластеризации
            if ((string)comboBox1.SelectedItem == "Количество вершин")
                labelOut.Text = 
                    GraphHelper.PropertiesHelper.Vertexes(currentList).Count.ToString();

            if ((string)comboBox1.SelectedItem == "Количество рёбер")
                labelOut.Text =
                    currentList.Count.ToString();

            if ((string)comboBox1.SelectedItem == "Средняя валентность")
                labelOut.Text =
                    GraphHelper.PropertiesHelper.AverageValences(currentList).ToString();

            if ((string)comboBox1.SelectedItem == "Максимальная валентность")
                labelOut.Text =
                    GraphHelper.PropertiesHelper.MaxValence(currentList).ToString();

            if ((string)comboBox1.SelectedItem == "Минимальная валентность")
                labelOut.Text =
                    GraphHelper.PropertiesHelper.MinValence(currentList).ToString();

            if ((string)comboBox1.SelectedItem == "Количество вершин валентности 1")
                labelOut.Text =
                    GraphHelper.PropertiesHelper.AmountOfVal1(currentList).ToString();

            if ((string)comboBox1.SelectedItem == "Средний вес рёбер")
                labelOut.Text =
                    GraphHelper.PropertiesHelper.AverageWeight(currentList).ToString();

            if ((string)comboBox1.SelectedItem == "Максимальный вес рёбер")
                labelOut.Text =
                    GraphHelper.PropertiesHelper.MaxWeight(currentList).ToString();

            if ((string)comboBox1.SelectedItem == "Минимальный вес рёбер")
                labelOut.Text =
                    GraphHelper.PropertiesHelper.MinWeight(currentList).ToString();

            if ((string)comboBox1.SelectedItem == "Количество петель")
                labelOut.Text =
                    GraphHelper.PropertiesHelper.AmountOfLoops(currentList).ToString();

            if ((string)comboBox1.SelectedItem == "Количество двойных ребер")
                labelOut.Text =
                    GraphHelper.PropertiesHelper.AmountOfDoubleEdges(currentList).ToString();

            if ((string)comboBox1.SelectedItem == "Коэффициент кластеризации")
                labelOut.Text =
                    GraphHelper.PropertiesHelper.CoeffClustering(currentList).ToString();
        }
    }
}
