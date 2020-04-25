namespace VisualGraph
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnDraw = new System.Windows.Forms.Button();
            this.pb = new System.Windows.Forms.PictureBox();
            this.btnMe = new System.Windows.Forms.Button();
            this.btnLO = new System.Windows.Forms.Button();
            this.btnLOP = new System.Windows.Forms.Button();
            this.labelVert = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.labelOut = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(713, 423);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(75, 23);
            this.btnDraw.TabIndex = 0;
            this.btnDraw.Text = "Calculate";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // pb
            // 
            this.pb.Location = new System.Drawing.Point(1, 2);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(582, 444);
            this.pb.TabIndex = 1;
            this.pb.TabStop = false;
            // 
            // btnMe
            // 
            this.btnMe.Location = new System.Drawing.Point(589, 12);
            this.btnMe.Name = "btnMe";
            this.btnMe.Size = new System.Drawing.Size(57, 23);
            this.btnMe.TabIndex = 2;
            this.btnMe.Text = "ME";
            this.btnMe.UseVisualStyleBackColor = true;
            this.btnMe.Click += new System.EventHandler(this.btnMe_Click);
            // 
            // btnLO
            // 
            this.btnLO.Location = new System.Drawing.Point(715, 12);
            this.btnLO.Name = "btnLO";
            this.btnLO.Size = new System.Drawing.Size(57, 23);
            this.btnLO.TabIndex = 3;
            this.btnLO.Text = "LO";
            this.btnLO.UseVisualStyleBackColor = true;
            this.btnLO.Click += new System.EventHandler(this.btnLO_Click);
            // 
            // btnLOP
            // 
            this.btnLOP.Location = new System.Drawing.Point(652, 12);
            this.btnLOP.Name = "btnLOP";
            this.btnLOP.Size = new System.Drawing.Size(57, 23);
            this.btnLOP.TabIndex = 4;
            this.btnLOP.Text = "LOP";
            this.btnLOP.UseVisualStyleBackColor = true;
            this.btnLOP.Click += new System.EventHandler(this.btnLOP_Click);
            // 
            // labelVert
            // 
            this.labelVert.AutoSize = true;
            this.labelVert.Location = new System.Drawing.Point(589, 54);
            this.labelVert.Name = "labelVert";
            this.labelVert.Size = new System.Drawing.Size(107, 13);
            this.labelVert.TabIndex = 5;
            this.labelVert.Text = "Количество вершин";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(592, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Количество ребер";
            // 
            // comboBox1
            // 
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Количество вершин",
            "Количество рёбер",
            "Средняя валентность",
            "Максимальная валентность",
            "Минимальная валентность",
            "Количество вершин валентности 1",
            "Средний вес рёбер",
            "Максимальный вес рёбер",
            "Минимальный вес рёбер",
            "Количество петель",
            "Количество двойных ребер",
            "Коэффициент кластеризации"});
            this.comboBox1.Location = new System.Drawing.Point(595, 103);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(177, 21);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // labelOut
            // 
            this.labelOut.AutoSize = true;
            this.labelOut.Location = new System.Drawing.Point(656, 239);
            this.labelOut.Name = "labelOut";
            this.labelOut.Size = new System.Drawing.Size(35, 13);
            this.labelOut.TabIndex = 8;
            this.labelOut.Text = "label2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelOut);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelVert);
            this.Controls.Add(this.btnLOP);
            this.Controls.Add(this.btnLO);
            this.Controls.Add(this.btnMe);
            this.Controls.Add(this.pb);
            this.Controls.Add(this.btnDraw);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.PictureBox pb;
        private System.Windows.Forms.Button btnMe;
        private System.Windows.Forms.Button btnLO;
        private System.Windows.Forms.Button btnLOP;
        private System.Windows.Forms.Label labelVert;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label labelOut;
    }
}

