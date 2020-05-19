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
using System.Xml;

namespace VisualGraph
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        List<Tuple<string, string, string, int>> lst; //полный список
        Form2 form2; //форма для отрисовки
        List<Tuple<string, string, string, int>> currentList; 
        int n;//количество кластеров

        private void Form1_Load(object sender, EventArgs e)
        {
            //Полный путь.
            const string path =
                "C:/Users/Пользователь/Desktop/CourseWork/table/traced-roi-connections.csv";
            string[] strings = File.ReadAllLines(path); //считываем по строкам
            lst = GraphHelper.GeneralPropertiesHelper.GetTuples(strings);


            btnDraw.Enabled = false;
            btnDRAW2.Enabled = false;
            btnDraw3.Enabled = false;
            comboBox1.Enabled = false;

            this.BackColor = Color.White;


        }

        private Form2 CreateForm2(int numberOfOperation)
        {
            Form2 form2 = new Form2();

            form2.form1 = this;
            form2.numberOfOperation = numberOfOperation;
            form2.currentList = this.currentList;
            form2.n = this.n;
            return form2;
        }


        #region Draw
        internal void btnDraw_Click(object sender, EventArgs e)
        {

            form2 = CreateForm2(1);
            form2.Show();

            Image img = new Bitmap(form2.pb.Width, form2.pb.Height);
            form2.pb.Image = img;
            Graphics gr = Graphics.FromImage(form2.pb.Image);
            gr.Clear(Color.White);
            Pen pen = new Pen(Color.FromArgb(0, 100, 100));


            //рисуем
            GraphHelper.VisualizationHelper.DrawGraphRandom
                (currentList, form2.pb, pen);

        }

        internal void btnDraw3_Click(object sender, EventArgs e)
        {
            form2 = CreateForm2(3);
            form2.Show();
        }
        internal void btnDRAW2_Click(object sender, EventArgs e)
        {
            form2 = CreateForm2(2);
            form2.Show();

            Image img = new Bitmap(form2.pb.Width, form2.pb.Height);
            form2.pb.Image = img;
            Graphics gr = Graphics.FromImage(form2.pb.Image);
            gr.Clear(Color.White);
            Pen pen = new Pen(Color.FromArgb(0, 100, 100));

            GraphHelper.VisualizationHelper.DrawGraphRandomClust
                (currentList, form2.pb, pen, n);

        }
        #endregion

        
        #region ClickButton
        private void btnMe_Click(object sender, EventArgs e)
        {
            labelOut.Text = "";
            btnDraw.Enabled = true;
            comboBox1.Enabled = true;

            btnMe.BackColor = Color.FromArgb(0, 220, 0);//делаем зеленой
            btnMe.Enabled = false; //делаем неактивной
            btnLO.BackColor = Color.Transparent; //сбрасываем цвета других кнопок
            btnLOP.BackColor = Color.Transparent;
            btnAME.BackColor = Color.Transparent;
            btnAOT.BackColor = Color.Transparent;
            btnLOP.Enabled = true;
            btnAOT.Enabled = true;
            btnAME.Enabled = true;
            btnLO.Enabled = true;

            currentList = GraphHelper.GroupHelper.FindRegions(lst,
                new string[] { "ME(L)", "ME(R)" });
        }

        private void btnLO_Click(object sender, EventArgs e)
        {
            labelOut.Text = "";
            btnDraw.Enabled = true;
            comboBox1.Enabled = true;

            btnLO.BackColor = Color.FromArgb(0, 220, 0);//делаем зеленой
            btnLO.Enabled = false; //делаем неактивной
            btnMe.BackColor = Color.Transparent; //сбрасываем цвета других кнопок
            btnLOP.BackColor = Color.Transparent;
            btnAME.BackColor = Color.Transparent;
            btnAOT.BackColor = Color.Transparent;
            btnAOT.Enabled = true;
            btnMe.Enabled = true;
            btnAME.Enabled = true;
            btnLOP.Enabled = true;

            currentList = GraphHelper.GroupHelper.FindRegions(lst,
                new string[] { "LO(L)", "LO(R)" });
        }

        private void btnLOP_Click(object sender, EventArgs e)
        {
            labelOut.Text = "";
            btnDraw.Enabled = true;
            comboBox1.Enabled = true;

            btnLOP.BackColor = Color.FromArgb(0, 220, 0);//делаем зеленой
            btnLOP.Enabled = false; //делаем неактивной
            btnLO.BackColor = Color.Transparent; //сбрасываем цвета других кнопок
            btnMe.BackColor = Color.Transparent;
            btnAME.BackColor = Color.Transparent;
            btnAOT.BackColor = Color.Transparent;
            btnAOT.Enabled = true;
            btnLO.Enabled = true;
            btnMe.Enabled = true;
            btnAME.Enabled = true;

            currentList = GraphHelper.GroupHelper.FindRegions(lst,
                new string[] { "LOP(L)", "LOP(R)" });
        }


        private void btnAME_Click(object sender, EventArgs e)
        {
            labelOut.Text = "";
            btnDraw.Enabled = true;
            comboBox1.Enabled = true;

            btnAME.BackColor = Color.FromArgb(0, 220, 0);//делаем зеленой
            btnAME.Enabled = false; //делаем неактивной

            btnLOP.BackColor = Color.Transparent; //сбрасываем цвета других кнопок
            btnMe.BackColor = Color.Transparent;
            btnLO.BackColor = Color.Transparent; //сбрасываем цвета других кнопок
            btnMe.BackColor = Color.Transparent;
            btnLO.Enabled = true;
            btnAOT.BackColor = Color.Transparent;
            btnAOT.Enabled = true;
            btnMe.Enabled = true;
            btnLOP.Enabled = true;

            currentList = GraphHelper.GroupHelper.FindRegions(lst,
                new string[] { "AME(L)", "AME(R)" });
        }

        private void btnAOT_Click(object sender, EventArgs e)
        {
            labelOut.Text = "";
            btnDraw.Enabled = true;
            comboBox1.Enabled = true;

            btnAOT.BackColor = Color.FromArgb(0, 220, 0);//делаем зеленой
            btnAOT.Enabled = false; //делаем неактивной

            btnLOP.BackColor = Color.Transparent; //сбрасываем цвета других кнопок
            btnMe.BackColor = Color.Transparent;
            btnLO.BackColor = Color.Transparent; //сбрасываем цвета других кнопок
            btnMe.BackColor = Color.Transparent;
            btnAME.BackColor = Color.Transparent;
            btnAME.Enabled = true;
            btnLO.Enabled = true;
            btnMe.Enabled = true;
            btnLOP.Enabled = true;

            currentList = GraphHelper.GroupHelper.FindRegions(lst,
                new string[] { "AOTU(L)", "AOTU(R)" });
        }

        #endregion


        #region Select
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if ((string)comboBox1.SelectedItem == "Количество вершин")
                labelOut.Text =
                    GraphHelper.PropertiesHelper.Vertexes(currentList).Count.ToString();

            if ((string)comboBox1.SelectedItem == "Количество рёбер")
                labelOut.Text =
                    currentList.Count.ToString();

            if ((string)comboBox1.SelectedItem == "Средняя валентность")
                labelOut.Text =
                    $"{GraphHelper.PropertiesHelper.AverageValences(currentList):f3}";

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
                    $"{GraphHelper.PropertiesHelper.AverageWeight(currentList):f3}";

            if ((string)comboBox1.SelectedItem == "Максимальный вес рёбер")
                labelOut.Text =
                    GraphHelper.PropertiesHelper.MaxWeight(currentList).ToString();

            if ((string)comboBox1.SelectedItem == "Минимальный вес рёбер")
                labelOut.Text =
                    GraphHelper.PropertiesHelper.MinWeight(currentList).ToString();

            if ((string)comboBox1.SelectedItem == "Количество петель")
                labelOut.Text =
                    GraphHelper.PropertiesHelper.AmountOfLoops(currentList).ToString();

            if ((string)comboBox1.SelectedItem == "Коэффициент кластеризации")
                labelOut.Text =
                    $"{GraphHelper.PropertiesHelper.CoeffClustering(currentList):f3}";
        }

        #endregion
        
        
        private void tbAmountOfClusts_TextChanged(object sender, EventArgs e)
        {
            string s = tbAmountOfClusts.Text;
            if (!int.TryParse(s, out n) || n<=0 ||
                n > GraphHelper.PropertiesHelper.Vertexes(currentList).Count)
            {
                btnDRAW2.Enabled = false;
                btnDraw3.Enabled = false;
            }
            else if (currentList != null)
            {
                btnDRAW2.Enabled = true;
                btnDraw3.Enabled = true;
            }

        }

    }
}
