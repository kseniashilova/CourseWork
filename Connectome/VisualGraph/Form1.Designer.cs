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
            this.btnMe = new System.Windows.Forms.Button();
            this.btnLO = new System.Windows.Forms.Button();
            this.btnLOP = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.labelOut = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAME = new System.Windows.Forms.Button();
            this.btnDRAW2 = new System.Windows.Forms.Button();
            this.btnAOT = new System.Windows.Forms.Button();
            this.btnDraw3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbAmountOfClusts = new System.Windows.Forms.TextBox();
            this.labelAmountOfClust = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnDraw
            // 
            this.btnDraw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDraw.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDraw.Location = new System.Drawing.Point(490, 12);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(282, 92);
            this.btnDraw.TabIndex = 0;
            this.btnDraw.Text = "Нарисовать по вершинам";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // btnMe
            // 
            this.btnMe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMe.Location = new System.Drawing.Point(27, 38);
            this.btnMe.Name = "btnMe";
            this.btnMe.Size = new System.Drawing.Size(210, 55);
            this.btnMe.TabIndex = 2;
            this.btnMe.Text = "MEDULLA";
            this.btnMe.UseVisualStyleBackColor = true;
            this.btnMe.Click += new System.EventHandler(this.btnMe_Click);
            // 
            // btnLO
            // 
            this.btnLO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLO.Location = new System.Drawing.Point(27, 327);
            this.btnLO.Name = "btnLO";
            this.btnLO.Size = new System.Drawing.Size(210, 55);
            this.btnLO.TabIndex = 3;
            this.btnLO.Text = "LOBULA";
            this.btnLO.UseVisualStyleBackColor = true;
            this.btnLO.Click += new System.EventHandler(this.btnLO_Click);
            // 
            // btnLOP
            // 
            this.btnLOP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLOP.Location = new System.Drawing.Point(27, 177);
            this.btnLOP.Name = "btnLOP";
            this.btnLOP.Size = new System.Drawing.Size(210, 55);
            this.btnLOP.TabIndex = 4;
            this.btnLOP.Text = "LOBULA PLATE";
            this.btnLOP.UseVisualStyleBackColor = true;
            this.btnLOP.Click += new System.EventHandler(this.btnLOP_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
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
            "Коэффициент кластеризации"});
            this.comboBox1.Location = new System.Drawing.Point(268, 38);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(183, 21);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // labelOut
            // 
            this.labelOut.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelOut.AutoSize = true;
            this.labelOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelOut.Location = new System.Drawing.Point(329, 106);
            this.labelOut.Name = "labelOut";
            this.labelOut.Size = new System.Drawing.Size(0, 31);
            this.labelOut.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Location = new System.Drawing.Point(290, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Выбрать характеристику";
            // 
            // btnAME
            // 
            this.btnAME.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAME.Location = new System.Drawing.Point(27, 106);
            this.btnAME.Name = "btnAME";
            this.btnAME.Size = new System.Drawing.Size(210, 55);
            this.btnAME.TabIndex = 11;
            this.btnAME.Text = "ACCESSORY MEDULLA";
            this.btnAME.UseVisualStyleBackColor = true;
            this.btnAME.Click += new System.EventHandler(this.btnAME_Click);
            // 
            // btnDRAW2
            // 
            this.btnDRAW2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDRAW2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDRAW2.Location = new System.Drawing.Point(490, 110);
            this.btnDRAW2.Name = "btnDRAW2";
            this.btnDRAW2.Size = new System.Drawing.Size(282, 94);
            this.btnDRAW2.TabIndex = 12;
            this.btnDRAW2.Text = "Нарисовать кластеризованный граф";
            this.btnDRAW2.UseVisualStyleBackColor = true;
            this.btnDRAW2.Click += new System.EventHandler(this.btnDRAW2_Click);
            // 
            // btnAOT
            // 
            this.btnAOT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAOT.Location = new System.Drawing.Point(27, 249);
            this.btnAOT.Name = "btnAOT";
            this.btnAOT.Size = new System.Drawing.Size(210, 55);
            this.btnAOT.TabIndex = 13;
            this.btnAOT.Text = "ANTERIOR OPTIC TUBERCLE";
            this.btnAOT.UseVisualStyleBackColor = true;
            this.btnAOT.Click += new System.EventHandler(this.btnAOT_Click);
            // 
            // btnDraw3
            // 
            this.btnDraw3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDraw3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDraw3.Location = new System.Drawing.Point(490, 210);
            this.btnDraw3.Name = "btnDraw3";
            this.btnDraw3.Size = new System.Drawing.Size(282, 94);
            this.btnDraw3.TabIndex = 14;
            this.btnDraw3.Text = "Нарисовать кластеризованный граф по итерациям";
            this.btnDraw3.UseVisualStyleBackColor = true;
            this.btnDraw3.Click += new System.EventHandler(this.btnDraw3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(89, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Выбрать граф";
            // 
            // tbAmountOfClusts
            // 
            this.tbAmountOfClusts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAmountOfClusts.Location = new System.Drawing.Point(588, 362);
            this.tbAmountOfClusts.Name = "tbAmountOfClusts";
            this.tbAmountOfClusts.Size = new System.Drawing.Size(100, 20);
            this.tbAmountOfClusts.TabIndex = 16;
            this.tbAmountOfClusts.TextChanged += new System.EventHandler(this.tbAmountOfClusts_TextChanged);
            // 
            // labelAmountOfClust
            // 
            this.labelAmountOfClust.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAmountOfClust.AutoSize = true;
            this.labelAmountOfClust.Location = new System.Drawing.Point(551, 337);
            this.labelAmountOfClust.Name = "labelAmountOfClust";
            this.labelAmountOfClust.Size = new System.Drawing.Size(168, 13);
            this.labelAmountOfClust.TabIndex = 17;
            this.labelAmountOfClust.Text = "Выбрать количество кластеров";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelAmountOfClust);
            this.Controls.Add(this.tbAmountOfClusts);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDraw3);
            this.Controls.Add(this.btnAOT);
            this.Controls.Add(this.btnDRAW2);
            this.Controls.Add(this.btnAME);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelOut);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnLOP);
            this.Controls.Add(this.btnLO);
            this.Controls.Add(this.btnMe);
            this.Controls.Add(this.btnDraw);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.Button btnMe;
        private System.Windows.Forms.Button btnLO;
        private System.Windows.Forms.Button btnLOP;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label labelOut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAME;
        private System.Windows.Forms.Button btnDRAW2;
        private System.Windows.Forms.Button btnAOT;
        private System.Windows.Forms.Button btnDraw3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbAmountOfClusts;
        private System.Windows.Forms.Label labelAmountOfClust;
    }
}

