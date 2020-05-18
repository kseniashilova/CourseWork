using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualGraph
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            this.Size = SystemInformation.PrimaryMonitorSize;
            pb.BackColor = Color.White;
            this.BackColor = Color.White;
        }


        internal int numberOfOperation; //какой из методов рисует
        internal Form1 form1;
        //из предыдущей формы
        internal List<Tuple<string, string, string, int>> currentList;


        int iteration = 0;
        List<List<string>> clusts;//считаем в методе
        List<string> vert; //считаем в методе
        double average;//считаем в методе
        internal int n; //количество кластеров
        List<double> prevAverage; //считаем в методе
        List<int> prevAmount;//считаем в методе

        private void Form2_Load(object sender, EventArgs e)
        {
            if (numberOfOperation == 3)
            {
                MessageBox.Show("Для отрисовки следующей итерации" +
                    " метода кластеризации кликните по экрану.");
                FirstIteration();
            }
            //показываем инструкцию
            MessageBox.Show("Для сохранения картинки " +
                "кликните дважды по экрану.");
        }

        private void FirstIteration()
        {
            Image img = new Bitmap(pb.Width, pb.Height);
            pb.Image = img;

            average = GraphHelper.PropertiesHelper.AverageWeight(currentList);
            vert = GraphHelper.PropertiesHelper.Vertexes(currentList); //находим вершины

            clusts = new List<List<string>>(); //лист кластеров

            /*
             * поместим все вершины в n кластеров (фиксированное количество)
             * затем поочередно рассматривая каждую вершину будем вычислять
             * новое среднее расстояние в кластере 
             * среднее расстояние будем вычислять как средний вес образовавшихся ребер
             * вершину будем помещать в тот кластер, у которого итоговый средний вес ближе всего к среднему по графу
             */
            
            //создаем n начальных кластеров
            for (int i = 0; i < n; i++)
            {
                clusts.Add(new List<string>());
            }
            for (int i = 0; i < vert.Count; i++)
            {
                clusts[i % n].Add(vert[i]);
            }

            prevAverage = new List<double>();
            prevAmount = new List<int>();
            for (int i = 0; i < n; i++)
            {
                int prev;
                //вычисляем первоначальный средний вес ребер в кластерах
                prevAverage.Add(GraphHelper.PropertiesHelper.AverageWeightVertexes(
                    out prev, clusts[i], currentList));
                prevAmount.Add(prev);
            }
            GraphHelper.VisualizationHelper.DrawGroups(clusts, pb, new Pen(Color.Chocolate));
        }

        //рисуем следующий шаг
        
        private void pb_Click(object sender, EventArgs e)
        {
            if (numberOfOperation == 3 && iteration < vert.Count)
            {
                GraphHelper.VisualizationHelper.DrawNextIteration
                    (iteration, n, clusts, vert,
                    ref prevAmount, ref prevAverage,
                    currentList, average, pb, new Pen(Color.Chocolate));
                iteration++;
            }
        }


        /// <summary>
        /// Сохранение картинки из PB
        /// </summary>
        private void pb_DoubleClick(object sender, EventArgs e)
        {

            if (pb.Image != null) //если в pictureBox есть изображение
            {
                Image image = pb.Image;
                //создание диалогового окна "Сохранить как..", для сохранения изображения
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.Title = "Сохранить картинку как...";
                //отображать ли предупреждение, если пользователь указывает имя уже существующего файла
                savedialog.OverwritePrompt = true;
                //отображать ли предупреждение, если пользователь указывает несуществующий путь
                savedialog.CheckPathExists = true;
                //список форматов файла, отображаемый в поле "Тип файла"
                savedialog.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                //отображается ли кнопка "Справка" в диалоговом окне
                savedialog.ShowHelp = true;
                if (savedialog.ShowDialog() == DialogResult.OK) //если в диалоговом окне нажата кнопка "ОК"
                {
                    try
                    {
                        image.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        
    }
}
