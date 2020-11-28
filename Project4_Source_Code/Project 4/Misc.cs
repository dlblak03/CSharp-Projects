using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Project_4
{
    class Misc
    {
        private static Random random = new Random();

        //Initialize chart with the correct settings
        public static void Initialize_Chart(int Length, Chart TSP_Chart, Chart Graph_Data) 
        {
            for(int i = 1; i <= Length; i++)
            {
                TSP_Chart.Series.Add(Convert.ToString(i));
                TSP_Chart.Series[i].IsVisibleInLegend = false;
                TSP_Chart.Series[i].ChartType = SeriesChartType.Line;
                TSP_Chart.Series[i].Color = Color.White;
            }

            //Configure the chart
            TSP_Chart.ChartAreas[0].AxisX.Interval = 10;
            TSP_Chart.ChartAreas[0].AxisY.Interval = 10;
            TSP_Chart.ChartAreas[0].AxisX.Maximum = 100;
            TSP_Chart.ChartAreas[0].AxisY.Maximum = 100;
            TSP_Chart.ChartAreas[0].AxisX.Minimum = 0;
            TSP_Chart.ChartAreas[0].AxisY.Minimum = 0;

            Graph_Data.ChartAreas[0].AxisY.Title = "Cost";
            Graph_Data.ChartAreas[0].AxisX.Title = "Generations";
            Graph_Data.ChartAreas[0].AxisX.TitleForeColor = Color.White;
            Graph_Data.ChartAreas[0].AxisY.TitleForeColor = Color.White;
        }

    }
}
