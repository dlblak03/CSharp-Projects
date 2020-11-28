namespace Project_4
{
    partial class TSP_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TSP_Form));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.TSP_Title = new System.Windows.Forms.Label();
            this.TSP_Topic = new System.Windows.Forms.Label();
            this.TSP_Description = new System.Windows.Forms.Label();
            this.Open_File = new System.Windows.Forms.Button();
            this.TSP_Chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Population_Size_Box = new System.Windows.Forms.ComboBox();
            this.Population_Size = new System.Windows.Forms.Label();
            this.Run_Program = new System.Windows.Forms.Button();
            this.Graph_Data = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Path = new System.Windows.Forms.Label();
            this.Cost = new System.Windows.Forms.Label();
            this.Path_Text_Box = new System.Windows.Forms.TextBox();
            this.Copy_Path = new System.Windows.Forms.Button();
            this.Cost_Text_Box = new System.Windows.Forms.TextBox();
            this.Copy_Cost = new System.Windows.Forms.Button();
            this.Crossover_Box = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Mutation_Method = new System.Windows.Forms.Label();
            this.Mutation_Method_Box = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.TSP_Chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Graph_Data)).BeginInit();
            this.SuspendLayout();
            // 
            // TSP_Title
            // 
            this.TSP_Title.AutoSize = true;
            this.TSP_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.TSP_Title.ForeColor = System.Drawing.Color.White;
            this.TSP_Title.Location = new System.Drawing.Point(812, 9);
            this.TSP_Title.Name = "TSP_Title";
            this.TSP_Title.Size = new System.Drawing.Size(289, 25);
            this.TSP_Title.TabIndex = 0;
            this.TSP_Title.Text = "Travelling Salesperson Problem";
            // 
            // TSP_Topic
            // 
            this.TSP_Topic.AutoSize = true;
            this.TSP_Topic.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.TSP_Topic.ForeColor = System.Drawing.Color.White;
            this.TSP_Topic.Location = new System.Drawing.Point(875, 44);
            this.TSP_Topic.Name = "TSP_Topic";
            this.TSP_Topic.Size = new System.Drawing.Size(163, 22);
            this.TSP_Topic.TabIndex = 1;
            this.TSP_Topic.Text = "- Genetic Algorithm";
            // 
            // TSP_Description
            // 
            this.TSP_Description.AutoSize = true;
            this.TSP_Description.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.TSP_Description.ForeColor = System.Drawing.Color.White;
            this.TSP_Description.Location = new System.Drawing.Point(791, 78);
            this.TSP_Description.Name = "TSP_Description";
            this.TSP_Description.Size = new System.Drawing.Size(337, 90);
            this.TSP_Description.TabIndex = 2;
            this.TSP_Description.Text = resources.GetString("TSP_Description.Text");
            // 
            // Open_File
            // 
            this.Open_File.Location = new System.Drawing.Point(855, 231);
            this.Open_File.Name = "Open_File";
            this.Open_File.Size = new System.Drawing.Size(203, 40);
            this.Open_File.TabIndex = 3;
            this.Open_File.Text = "Open TSP File";
            this.Open_File.UseVisualStyleBackColor = true;
            this.Open_File.Click += new System.EventHandler(this.Open_File_Click);
            // 
            // TSP_Chart
            // 
            this.TSP_Chart.BackColor = System.Drawing.Color.Black;
            chartArea1.BackColor = System.Drawing.Color.Black;
            chartArea1.Name = "ChartArea1";
            this.TSP_Chart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.TSP_Chart.Legends.Add(legend1);
            this.TSP_Chart.Location = new System.Drawing.Point(12, 9);
            this.TSP_Chart.Name = "TSP_Chart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.Color = System.Drawing.Color.Red;
            series1.IsVisibleInLegend = false;
            series1.LabelForeColor = System.Drawing.Color.Red;
            series1.Legend = "Legend1";
            series1.Name = "TSP Nodes";
            this.TSP_Chart.Series.Add(series1);
            this.TSP_Chart.Size = new System.Drawing.Size(763, 595);
            this.TSP_Chart.TabIndex = 4;
            this.TSP_Chart.Text = "chart1";
            // 
            // Population_Size_Box
            // 
            this.Population_Size_Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Population_Size_Box.FormattingEnabled = true;
            this.Population_Size_Box.Items.AddRange(new object[] {
            "600",
            "1200"});
            this.Population_Size_Box.Location = new System.Drawing.Point(855, 313);
            this.Population_Size_Box.Name = "Population_Size_Box";
            this.Population_Size_Box.Size = new System.Drawing.Size(202, 24);
            this.Population_Size_Box.TabIndex = 5;
            // 
            // Population_Size
            // 
            this.Population_Size.AutoSize = true;
            this.Population_Size.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Population_Size.ForeColor = System.Drawing.Color.White;
            this.Population_Size.Location = new System.Drawing.Point(897, 290);
            this.Population_Size.Name = "Population_Size";
            this.Population_Size.Size = new System.Drawing.Size(119, 20);
            this.Population_Size.TabIndex = 6;
            this.Population_Size.Text = "Population Size";
            // 
            // Run_Program
            // 
            this.Run_Program.Location = new System.Drawing.Point(854, 491);
            this.Run_Program.Name = "Run_Program";
            this.Run_Program.Size = new System.Drawing.Size(203, 40);
            this.Run_Program.TabIndex = 9;
            this.Run_Program.Text = "Run Program";
            this.Run_Program.UseVisualStyleBackColor = true;
            this.Run_Program.Click += new System.EventHandler(this.Run_Program_Click);
            // 
            // Graph_Data
            // 
            this.Graph_Data.BackColor = System.Drawing.Color.Black;
            chartArea2.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea2.AxisX.LabelStyle.Format = "#";
            chartArea2.AxisX.LineColor = System.Drawing.Color.White;
            chartArea2.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.AxisX.TitleForeColor = System.Drawing.Color.White;
            chartArea2.AxisY.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea2.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea2.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea2.AxisY.LineColor = System.Drawing.Color.White;
            chartArea2.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.AxisY.TitleForeColor = System.Drawing.Color.White;
            chartArea2.BackColor = System.Drawing.Color.Black;
            chartArea2.BackSecondaryColor = System.Drawing.Color.Black;
            chartArea2.Name = "ChartArea1";
            this.Graph_Data.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.Graph_Data.Legends.Add(legend2);
            this.Graph_Data.Location = new System.Drawing.Point(1142, 9);
            this.Graph_Data.Name = "Graph_Data";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "GA";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "GA+WoAC";
            this.Graph_Data.Series.Add(series2);
            this.Graph_Data.Series.Add(series3);
            this.Graph_Data.Size = new System.Drawing.Size(763, 595);
            this.Graph_Data.TabIndex = 10;
            this.Graph_Data.Text = "chart1";
            // 
            // Path
            // 
            this.Path.AutoSize = true;
            this.Path.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Path.ForeColor = System.Drawing.Color.White;
            this.Path.Location = new System.Drawing.Point(12, 617);
            this.Path.Name = "Path";
            this.Path.Size = new System.Drawing.Size(46, 20);
            this.Path.TabIndex = 11;
            this.Path.Text = "Path:";
            // 
            // Cost
            // 
            this.Cost.AutoSize = true;
            this.Cost.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Cost.ForeColor = System.Drawing.Color.White;
            this.Cost.Location = new System.Drawing.Point(12, 648);
            this.Cost.Name = "Cost";
            this.Cost.Size = new System.Drawing.Size(46, 20);
            this.Cost.TabIndex = 12;
            this.Cost.Text = "Cost:";
            // 
            // Path_Text_Box
            // 
            this.Path_Text_Box.Location = new System.Drawing.Point(58, 617);
            this.Path_Text_Box.Name = "Path_Text_Box";
            this.Path_Text_Box.Size = new System.Drawing.Size(1776, 20);
            this.Path_Text_Box.TabIndex = 13;
            // 
            // Copy_Path
            // 
            this.Copy_Path.Location = new System.Drawing.Point(1840, 617);
            this.Copy_Path.Name = "Copy_Path";
            this.Copy_Path.Size = new System.Drawing.Size(65, 20);
            this.Copy_Path.TabIndex = 14;
            this.Copy_Path.Text = "Copy";
            this.Copy_Path.UseVisualStyleBackColor = true;
            this.Copy_Path.Click += new System.EventHandler(this.Copy_Path_Click);
            // 
            // Cost_Text_Box
            // 
            this.Cost_Text_Box.Location = new System.Drawing.Point(58, 648);
            this.Cost_Text_Box.Name = "Cost_Text_Box";
            this.Cost_Text_Box.Size = new System.Drawing.Size(1776, 20);
            this.Cost_Text_Box.TabIndex = 15;
            // 
            // Copy_Cost
            // 
            this.Copy_Cost.Location = new System.Drawing.Point(1840, 647);
            this.Copy_Cost.Name = "Copy_Cost";
            this.Copy_Cost.Size = new System.Drawing.Size(65, 20);
            this.Copy_Cost.TabIndex = 16;
            this.Copy_Cost.Text = "Copy";
            this.Copy_Cost.UseVisualStyleBackColor = true;
            this.Copy_Cost.Click += new System.EventHandler(this.Copy_Cost_Click);
            // 
            // Crossover_Box
            // 
            this.Crossover_Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Crossover_Box.FormattingEnabled = true;
            this.Crossover_Box.Items.AddRange(new object[] {
            "PMX",
            "CX"});
            this.Crossover_Box.Location = new System.Drawing.Point(855, 375);
            this.Crossover_Box.Name = "Crossover_Box";
            this.Crossover_Box.Size = new System.Drawing.Size(202, 24);
            this.Crossover_Box.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(916, 352);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.TabIndex = 19;
            this.label1.Text = "Crossover";
            // 
            // Mutation_Method
            // 
            this.Mutation_Method.AutoSize = true;
            this.Mutation_Method.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Mutation_Method.ForeColor = System.Drawing.Color.White;
            this.Mutation_Method.Location = new System.Drawing.Point(892, 418);
            this.Mutation_Method.Name = "Mutation_Method";
            this.Mutation_Method.Size = new System.Drawing.Size(129, 20);
            this.Mutation_Method.TabIndex = 7;
            this.Mutation_Method.Text = "Mutation Method";
            // 
            // Mutation_Method_Box
            // 
            this.Mutation_Method_Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Mutation_Method_Box.FormattingEnabled = true;
            this.Mutation_Method_Box.Items.AddRange(new object[] {
            "Reverse Sequence Mutation"});
            this.Mutation_Method_Box.Location = new System.Drawing.Point(855, 441);
            this.Mutation_Method_Box.Name = "Mutation_Method_Box";
            this.Mutation_Method_Box.Size = new System.Drawing.Size(202, 24);
            this.Mutation_Method_Box.TabIndex = 8;
            // 
            // TSP_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1919, 675);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Crossover_Box);
            this.Controls.Add(this.Copy_Cost);
            this.Controls.Add(this.Cost_Text_Box);
            this.Controls.Add(this.Copy_Path);
            this.Controls.Add(this.Path_Text_Box);
            this.Controls.Add(this.Cost);
            this.Controls.Add(this.Path);
            this.Controls.Add(this.Graph_Data);
            this.Controls.Add(this.Run_Program);
            this.Controls.Add(this.Mutation_Method_Box);
            this.Controls.Add(this.Mutation_Method);
            this.Controls.Add(this.Population_Size);
            this.Controls.Add(this.Population_Size_Box);
            this.Controls.Add(this.TSP_Chart);
            this.Controls.Add(this.Open_File);
            this.Controls.Add(this.TSP_Description);
            this.Controls.Add(this.TSP_Topic);
            this.Controls.Add(this.TSP_Title);
            this.Name = "TSP_Form";
            this.Text = "Travelling Salesperson Problem";
            ((System.ComponentModel.ISupportInitialize)(this.TSP_Chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Graph_Data)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TSP_Title;
        private System.Windows.Forms.Label TSP_Topic;
        private System.Windows.Forms.Label TSP_Description;
        private System.Windows.Forms.Button Open_File;
        private System.Windows.Forms.DataVisualization.Charting.Chart TSP_Chart;
        private System.Windows.Forms.ComboBox Population_Size_Box;
        private System.Windows.Forms.Label Population_Size;
        private System.Windows.Forms.Button Run_Program;
        private System.Windows.Forms.DataVisualization.Charting.Chart Graph_Data;
        private System.Windows.Forms.Label Path;
        private System.Windows.Forms.Label Cost;
        private System.Windows.Forms.TextBox Path_Text_Box;
        private System.Windows.Forms.Button Copy_Path;
        private System.Windows.Forms.TextBox Cost_Text_Box;
        private System.Windows.Forms.Button Copy_Cost;
        private System.Windows.Forms.ComboBox Crossover_Box;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Mutation_Method;
        private System.Windows.Forms.ComboBox Mutation_Method_Box;
    }
}

