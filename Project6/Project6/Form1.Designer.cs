namespace Project6
{
    partial class Partitioning_Form
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.User_Input_List = new System.Windows.Forms.TextBox();
            this.NumberOfElements = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Max_Number_Input = new System.Windows.Forms.TextBox();
            this.Default_Label_2 = new System.Windows.Forms.Label();
            this.Default_Label_1 = new System.Windows.Forms.Label();
            this.Population_Size_Label = new System.Windows.Forms.Label();
            this.Default_Label_3 = new System.Windows.Forms.Label();
            this.Population_Size_Box = new System.Windows.Forms.ComboBox();
            this.Run_Program = new System.Windows.Forms.Button();
            this.Generated_List_Label = new System.Windows.Forms.Label();
            this.Generated_List_Box = new System.Windows.Forms.TextBox();
            this.Best_Solution_Label = new System.Windows.Forms.Label();
            this.Sublist_One_Label = new System.Windows.Forms.Label();
            this.Sublist_One_Box = new System.Windows.Forms.TextBox();
            this.Sublist_Two_Label = new System.Windows.Forms.Label();
            this.Sublist_Two_Box = new System.Windows.Forms.TextBox();
            this.Sum_Label1 = new System.Windows.Forms.Label();
            this.Sum_Box_One = new System.Windows.Forms.TextBox();
            this.Sum_Label2 = new System.Windows.Forms.Label();
            this.Sum_Box_Two = new System.Windows.Forms.TextBox();
            this.Difference_Label = new System.Windows.Forms.Label();
            this.Difference_Box = new System.Windows.Forms.TextBox();
            this.PP_Chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.PP_Chart)).BeginInit();
            this.SuspendLayout();
            // 
            // User_Input_List
            // 
            this.User_Input_List.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.User_Input_List.Location = new System.Drawing.Point(29, 53);
            this.User_Input_List.Name = "User_Input_List";
            this.User_Input_List.Size = new System.Drawing.Size(193, 26);
            this.User_Input_List.TabIndex = 0;
            // 
            // NumberOfElements
            // 
            this.NumberOfElements.AutoSize = true;
            this.NumberOfElements.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.NumberOfElements.ForeColor = System.Drawing.Color.White;
            this.NumberOfElements.Location = new System.Drawing.Point(12, 9);
            this.NumberOfElements.Name = "NumberOfElements";
            this.NumberOfElements.Size = new System.Drawing.Size(227, 24);
            this.NumberOfElements.TabIndex = 1;
            this.NumberOfElements.Text = "Number of elements in list";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(44, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 24);
            this.label1.TabIndex = 5;
            this.label1.Text = "Max number in list\r\n";
            // 
            // Max_Number_Input
            // 
            this.Max_Number_Input.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Max_Number_Input.Location = new System.Drawing.Point(29, 136);
            this.Max_Number_Input.Name = "Max_Number_Input";
            this.Max_Number_Input.Size = new System.Drawing.Size(193, 26);
            this.Max_Number_Input.TabIndex = 6;
            // 
            // Default_Label_2
            // 
            this.Default_Label_2.AutoSize = true;
            this.Default_Label_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Default_Label_2.ForeColor = System.Drawing.Color.White;
            this.Default_Label_2.Location = new System.Drawing.Point(80, 116);
            this.Default_Label_2.Name = "Default_Label_2";
            this.Default_Label_2.Size = new System.Drawing.Size(91, 17);
            this.Default_Label_2.TabIndex = 8;
            this.Default_Label_2.Text = "(Default 100)";
            // 
            // Default_Label_1
            // 
            this.Default_Label_1.AutoSize = true;
            this.Default_Label_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Default_Label_1.ForeColor = System.Drawing.Color.White;
            this.Default_Label_1.Location = new System.Drawing.Point(80, 33);
            this.Default_Label_1.Name = "Default_Label_1";
            this.Default_Label_1.Size = new System.Drawing.Size(91, 17);
            this.Default_Label_1.TabIndex = 9;
            this.Default_Label_1.Text = "(Default 100)";
            // 
            // Population_Size_Label
            // 
            this.Population_Size_Label.AutoSize = true;
            this.Population_Size_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.Population_Size_Label.ForeColor = System.Drawing.Color.White;
            this.Population_Size_Label.Location = new System.Drawing.Point(55, 182);
            this.Population_Size_Label.Name = "Population_Size_Label";
            this.Population_Size_Label.Size = new System.Drawing.Size(140, 24);
            this.Population_Size_Label.TabIndex = 10;
            this.Population_Size_Label.Text = "Population Size";
            // 
            // Default_Label_3
            // 
            this.Default_Label_3.AutoSize = true;
            this.Default_Label_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Default_Label_3.ForeColor = System.Drawing.Color.White;
            this.Default_Label_3.Location = new System.Drawing.Point(80, 206);
            this.Default_Label_3.Name = "Default_Label_3";
            this.Default_Label_3.Size = new System.Drawing.Size(91, 17);
            this.Default_Label_3.TabIndex = 11;
            this.Default_Label_3.Text = "(Default 100)";
            // 
            // Population_Size_Box
            // 
            this.Population_Size_Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Population_Size_Box.FormattingEnabled = true;
            this.Population_Size_Box.Items.AddRange(new object[] {
            "100",
            "200"});
            this.Population_Size_Box.Location = new System.Drawing.Point(29, 226);
            this.Population_Size_Box.Name = "Population_Size_Box";
            this.Population_Size_Box.Size = new System.Drawing.Size(193, 28);
            this.Population_Size_Box.TabIndex = 12;
            // 
            // Run_Program
            // 
            this.Run_Program.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Run_Program.Location = new System.Drawing.Point(53, 281);
            this.Run_Program.Name = "Run_Program";
            this.Run_Program.Size = new System.Drawing.Size(142, 26);
            this.Run_Program.TabIndex = 16;
            this.Run_Program.Text = "Run Program";
            this.Run_Program.UseVisualStyleBackColor = true;
            this.Run_Program.Click += new System.EventHandler(this.Run_Program_Click);
            // 
            // Generated_List_Label
            // 
            this.Generated_List_Label.AutoSize = true;
            this.Generated_List_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.Generated_List_Label.ForeColor = System.Drawing.Color.White;
            this.Generated_List_Label.Location = new System.Drawing.Point(492, 9);
            this.Generated_List_Label.Name = "Generated_List_Label";
            this.Generated_List_Label.Size = new System.Drawing.Size(131, 24);
            this.Generated_List_Label.TabIndex = 17;
            this.Generated_List_Label.Text = "Generated List";
            // 
            // Generated_List_Box
            // 
            this.Generated_List_Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Generated_List_Box.Location = new System.Drawing.Point(286, 36);
            this.Generated_List_Box.Name = "Generated_List_Box";
            this.Generated_List_Box.Size = new System.Drawing.Size(738, 26);
            this.Generated_List_Box.TabIndex = 18;
            // 
            // Best_Solution_Label
            // 
            this.Best_Solution_Label.AutoSize = true;
            this.Best_Solution_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.Best_Solution_Label.ForeColor = System.Drawing.Color.White;
            this.Best_Solution_Label.Location = new System.Drawing.Point(498, 92);
            this.Best_Solution_Label.Name = "Best_Solution_Label";
            this.Best_Solution_Label.Size = new System.Drawing.Size(119, 24);
            this.Best_Solution_Label.TabIndex = 19;
            this.Best_Solution_Label.Text = "Best Solution";
            // 
            // Sublist_One_Label
            // 
            this.Sublist_One_Label.AutoSize = true;
            this.Sublist_One_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Sublist_One_Label.ForeColor = System.Drawing.Color.White;
            this.Sublist_One_Label.Location = new System.Drawing.Point(512, 116);
            this.Sublist_One_Label.Name = "Sublist_One_Label";
            this.Sublist_One_Label.Size = new System.Drawing.Size(91, 20);
            this.Sublist_One_Label.TabIndex = 20;
            this.Sublist_One_Label.Text = "Sublist One";
            // 
            // Sublist_One_Box
            // 
            this.Sublist_One_Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Sublist_One_Box.Location = new System.Drawing.Point(286, 139);
            this.Sublist_One_Box.Name = "Sublist_One_Box";
            this.Sublist_One_Box.Size = new System.Drawing.Size(594, 26);
            this.Sublist_One_Box.TabIndex = 21;
            // 
            // Sublist_Two_Label
            // 
            this.Sublist_Two_Label.AutoSize = true;
            this.Sublist_Two_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Sublist_Two_Label.ForeColor = System.Drawing.Color.White;
            this.Sublist_Two_Label.Location = new System.Drawing.Point(512, 186);
            this.Sublist_Two_Label.Name = "Sublist_Two_Label";
            this.Sublist_Two_Label.Size = new System.Drawing.Size(90, 20);
            this.Sublist_Two_Label.TabIndex = 22;
            this.Sublist_Two_Label.Text = "Sublist Two";
            // 
            // Sublist_Two_Box
            // 
            this.Sublist_Two_Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Sublist_Two_Box.Location = new System.Drawing.Point(286, 209);
            this.Sublist_Two_Box.Name = "Sublist_Two_Box";
            this.Sublist_Two_Box.Size = new System.Drawing.Size(594, 26);
            this.Sublist_Two_Box.TabIndex = 23;
            // 
            // Sum_Label1
            // 
            this.Sum_Label1.AutoSize = true;
            this.Sum_Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Sum_Label1.ForeColor = System.Drawing.Color.White;
            this.Sum_Label1.Location = new System.Drawing.Point(940, 116);
            this.Sum_Label1.Name = "Sum_Label1";
            this.Sum_Label1.Size = new System.Drawing.Size(42, 20);
            this.Sum_Label1.TabIndex = 24;
            this.Sum_Label1.Text = "Sum";
            // 
            // Sum_Box_One
            // 
            this.Sum_Box_One.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Sum_Box_One.Location = new System.Drawing.Point(898, 139);
            this.Sum_Box_One.Name = "Sum_Box_One";
            this.Sum_Box_One.Size = new System.Drawing.Size(126, 26);
            this.Sum_Box_One.TabIndex = 25;
            // 
            // Sum_Label2
            // 
            this.Sum_Label2.AutoSize = true;
            this.Sum_Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Sum_Label2.ForeColor = System.Drawing.Color.White;
            this.Sum_Label2.Location = new System.Drawing.Point(940, 186);
            this.Sum_Label2.Name = "Sum_Label2";
            this.Sum_Label2.Size = new System.Drawing.Size(42, 20);
            this.Sum_Label2.TabIndex = 26;
            this.Sum_Label2.Text = "Sum";
            // 
            // Sum_Box_Two
            // 
            this.Sum_Box_Two.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Sum_Box_Two.Location = new System.Drawing.Point(898, 209);
            this.Sum_Box_Two.Name = "Sum_Box_Two";
            this.Sum_Box_Two.Size = new System.Drawing.Size(126, 26);
            this.Sum_Box_Two.TabIndex = 27;
            // 
            // Difference_Label
            // 
            this.Difference_Label.AutoSize = true;
            this.Difference_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Difference_Label.ForeColor = System.Drawing.Color.White;
            this.Difference_Label.Location = new System.Drawing.Point(920, 252);
            this.Difference_Label.Name = "Difference_Label";
            this.Difference_Label.Size = new System.Drawing.Size(83, 20);
            this.Difference_Label.TabIndex = 28;
            this.Difference_Label.Text = "Difference";
            // 
            // Difference_Box
            // 
            this.Difference_Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Difference_Box.Location = new System.Drawing.Point(898, 275);
            this.Difference_Box.Name = "Difference_Box";
            this.Difference_Box.Size = new System.Drawing.Size(126, 26);
            this.Difference_Box.TabIndex = 29;
            // 
            // PP_Chart
            // 
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            chartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            chartArea1.AxisY.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            chartArea1.AxisY.MinorTickMark.Enabled = true;
            chartArea1.AxisY.MinorTickMark.TickMarkStyle = System.Windows.Forms.DataVisualization.Charting.TickMarkStyle.AcrossAxis;
            chartArea1.Name = "ChartArea1";
            this.PP_Chart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.PP_Chart.Legends.Add(legend1);
            this.PP_Chart.Location = new System.Drawing.Point(286, 316);
            this.PP_Chart.Name = "PP_Chart";
            series1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.Black;
            series1.Legend = "Legend1";
            series1.Name = "GA";
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.Black;
            series2.Legend = "Legend1";
            series2.Name = "WOC";
            this.PP_Chart.Series.Add(series1);
            this.PP_Chart.Series.Add(series2);
            this.PP_Chart.Size = new System.Drawing.Size(738, 456);
            this.PP_Chart.TabIndex = 30;
            this.PP_Chart.Text = "chart1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(69, 574);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 24);
            this.label2.TabIndex = 31;
            this.label2.Text = "Runtime GA";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(69, 598);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 24);
            this.label3.TabIndex = 32;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(31, 670);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(188, 24);
            this.label4.TabIndex = 33;
            this.label4.Text = "Runtime GA + WoAC";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(55, 607);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 24);
            this.label5.TabIndex = 34;
            this.label5.Text = " ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(55, 703);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 24);
            this.label6.TabIndex = 35;
            this.label6.Text = " ";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.checkBox1.ForeColor = System.Drawing.Color.White;
            this.checkBox1.Location = new System.Drawing.Point(53, 338);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(141, 24);
            this.checkBox1.TabIndex = 36;
            this.checkBox1.Text = "Perfect Partition";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // Partitioning_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1048, 784);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PP_Chart);
            this.Controls.Add(this.Difference_Box);
            this.Controls.Add(this.Difference_Label);
            this.Controls.Add(this.Sum_Box_Two);
            this.Controls.Add(this.Sum_Label2);
            this.Controls.Add(this.Sum_Box_One);
            this.Controls.Add(this.Sum_Label1);
            this.Controls.Add(this.Sublist_Two_Box);
            this.Controls.Add(this.Sublist_Two_Label);
            this.Controls.Add(this.Sublist_One_Box);
            this.Controls.Add(this.Sublist_One_Label);
            this.Controls.Add(this.Best_Solution_Label);
            this.Controls.Add(this.Generated_List_Box);
            this.Controls.Add(this.Generated_List_Label);
            this.Controls.Add(this.Run_Program);
            this.Controls.Add(this.Population_Size_Box);
            this.Controls.Add(this.Default_Label_3);
            this.Controls.Add(this.Population_Size_Label);
            this.Controls.Add(this.Default_Label_1);
            this.Controls.Add(this.Default_Label_2);
            this.Controls.Add(this.Max_Number_Input);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NumberOfElements);
            this.Controls.Add(this.User_Input_List);
            this.Name = "Partitioning_Form";
            this.Text = "Partitioning Problem";
            ((System.ComponentModel.ISupportInitialize)(this.PP_Chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox User_Input_List;
        private System.Windows.Forms.Label NumberOfElements;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Max_Number_Input;
        private System.Windows.Forms.Label Default_Label_2;
        private System.Windows.Forms.Label Default_Label_1;
        private System.Windows.Forms.Label Population_Size_Label;
        private System.Windows.Forms.Label Default_Label_3;
        private System.Windows.Forms.ComboBox Population_Size_Box;
        private System.Windows.Forms.Button Run_Program;
        private System.Windows.Forms.Label Generated_List_Label;
        private System.Windows.Forms.TextBox Generated_List_Box;
        private System.Windows.Forms.Label Best_Solution_Label;
        private System.Windows.Forms.Label Sublist_One_Label;
        private System.Windows.Forms.TextBox Sublist_One_Box;
        private System.Windows.Forms.Label Sublist_Two_Label;
        private System.Windows.Forms.TextBox Sublist_Two_Box;
        private System.Windows.Forms.Label Sum_Label1;
        private System.Windows.Forms.TextBox Sum_Box_One;
        private System.Windows.Forms.Label Sum_Label2;
        private System.Windows.Forms.TextBox Sum_Box_Two;
        private System.Windows.Forms.Label Difference_Label;
        private System.Windows.Forms.TextBox Difference_Box;
        private System.Windows.Forms.DataVisualization.Charting.Chart PP_Chart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

