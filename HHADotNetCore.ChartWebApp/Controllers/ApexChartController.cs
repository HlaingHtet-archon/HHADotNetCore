using HHADotNetCore.ChartWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace HHADotNetCore.ChartWebApp.Controllers
{
    public class ApexChartController : Controller
    {
        public IActionResult PieChart()
        {
            ApexChartPieChartModel model = new ApexChartPieChartModel();
            model.Series = new int[] { 44, 55, 13, 43, 22 };
            model.Labels = new string[] { "Team A", "Team B", "Team C", "Team D", "Team E" };
            return View(model);
        }

        public IActionResult CombinedChart()
        {
            ApexChartCombinedChartModel model = new ApexChartCombinedChartModel
            {
                LineSeries = new List<LineDataPoint>
        {
            new LineDataPoint { X = new DateTime(1538778600000), Y = 6604 },
            new LineDataPoint { X = new DateTime(1538782200000), Y = 6602 },
            new LineDataPoint { X = new DateTime(1538814600000), Y = 6607 },
            new LineDataPoint { X = new DateTime(1538884800000), Y = 6620 }
        },
                CandleSeries = new List<CandleDataPoint>
        {
            new CandleDataPoint { X = new DateTime(1538778600000), Y = new decimal[] { 6629.81m, 6650.5m, 6623.04m, 6633.33m } },
            new CandleDataPoint { X = new DateTime(1538780400000), Y = new decimal[] { 6632.01m, 6643.59m, 6620m, 6630.11m } },
            new CandleDataPoint { X = new DateTime(1538782200000), Y = new decimal[] { 6630.71m, 6648.95m, 6623.34m, 6635.65m } },
            new CandleDataPoint { X = new DateTime(1538784000000), Y = new decimal[] { 6635.65m, 6651m, 6629.67m, 6638.24m } }
        }
            };

            return View(model);
        }

        public IActionResult StackedAreaChart()
        {
            ApexChartStackedAreaModel model = new ApexChartStackedAreaModel
            {
                DataSet = new List<List<TimeSeriesDataPoint>>
        {
            new List<TimeSeriesDataPoint>
            {
                new TimeSeriesDataPoint { X = new DateTime(2014, 1, 1), Y = 300000 },
                new TimeSeriesDataPoint { X = new DateTime(2014, 1, 5), Y = 400000 },
                new TimeSeriesDataPoint { X = new DateTime(2014, 1, 10), Y = 500000 },
                new TimeSeriesDataPoint { X = new DateTime(2014, 1, 15), Y = 450000 },
                new TimeSeriesDataPoint { X = new DateTime(2014, 1, 20), Y = 600000 }
            },
            new List<TimeSeriesDataPoint>
            {
                new TimeSeriesDataPoint { X = new DateTime(2014, 1, 1), Y = 200000 },
                new TimeSeriesDataPoint { X = new DateTime(2014, 1, 5), Y = 300000 },
                new TimeSeriesDataPoint { X = new DateTime(2014, 1, 10), Y = 350000 },
                new TimeSeriesDataPoint { X = new DateTime(2014, 1, 15), Y = 400000 },
                new TimeSeriesDataPoint { X = new DateTime(2014, 1, 20), Y = 500000 }
            },
            new List<TimeSeriesDataPoint>
            {
                new TimeSeriesDataPoint { X = new DateTime(2014, 1, 1), Y = 100000 },
                new TimeSeriesDataPoint { X = new DateTime(2014, 1, 5), Y = 200000 },
                new TimeSeriesDataPoint { X = new DateTime(2014, 1, 10), Y = 250000 },
                new TimeSeriesDataPoint { X = new DateTime(2014, 1, 15), Y = 300000 },
                new TimeSeriesDataPoint { X = new DateTime(2014, 1, 20), Y = 400000 }
            }
        }
            };

            return View(model);
        }

        public IActionResult StackedBarChart()
        {
            ApexChartStackedBarModel model = new ApexChartStackedBarModel
            {
                Series = new List<SeriesData>
        {
            new SeriesData { Name = "Marine Sprite", Data = new List<int> { 44, 55, 41, 37, 22, 43, 21 } },
            new SeriesData { Name = "Striking Calf", Data = new List<int> { 53, 32, 33, 52, 13, 43, 32 } },
            new SeriesData { Name = "Tank Picture", Data = new List<int> { 12, 17, 11, 9, 15, 11, 20 } },
            new SeriesData { Name = "Bucket Slope", Data = new List<int> { 9, 7, 5, 8, 6, 9, 4 } },
            new SeriesData { Name = "Reborn Kid", Data = new List<int> { 25, 12, 19, 32, 25, 24, 10 } }
        },
                Categories = new List<string> { "2008", "2009", "2010", "2011", "2012", "2013", "2014" }
            };

            return View(model);
        }

        public IActionResult ScatterChart()
        {
            ApexChartScatterModel model = new ApexChartScatterModel
            {
                Series = new List<ScatterSeries>
        {
            new ScatterSeries
            {
                Name = "DIAMOND",
                Data = GenerateDayWiseTimeSeries(new DateTime(2017, 2, 1), 10, 5, 60)
            },
            new ScatterSeries
            {
                Name = "TRIANGLE",
                Data = GenerateDayWiseTimeSeries(new DateTime(2017, 2, 11), 10, 54, 90)
            },
            new ScatterSeries
            {
                Name = "CROSS",
                Data = GenerateDayWiseTimeSeries(new DateTime(2017, 2, 20), 8, 10, 50)
            },
            new ScatterSeries
            {
                Name = "PLUS",
                Data = GenerateDayWiseTimeSeries(new DateTime(2017, 2, 28), 16, 30, 99)
            },
            new ScatterSeries
            {
                Name = "SQUARE",
                Data = GenerateDayWiseTimeSeries(new DateTime(2017, 3, 20), 10, 0, 59)
            },
            new ScatterSeries
            {
                Name = "LINE",
                Data = GenerateDayWiseTimeSeries(new DateTime(2017, 3, 29), 10, 0, 90)
            },
            new ScatterSeries
            {
                Name = "CIRCLE",
                Data = GenerateDayWiseTimeSeries(new DateTime(2017, 4, 10), 10, 5, 35)
            },
            new ScatterSeries
            {
                Name = "STAR",
                Data = GenerateDayWiseTimeSeries(new DateTime(2017, 4, 20), 10, 15, 60)
            },
            new ScatterSeries
            {
                Name = "SPARKLE",
                Data = GenerateDayWiseTimeSeries(new DateTime(2017, 4, 29), 10, 45, 99)
            }
        }
            };

            return View(model);
        }

        private List<ScatterDataPoint> GenerateDayWiseTimeSeries(DateTime startDate, int count, int min, int max)
        {
            var random = new Random();
            var data = new List<ScatterDataPoint>();

            for (int i = 0; i < count; i++)
            {
                data.Add(new ScatterDataPoint
                {
                    X = startDate.AddDays(i),
                    Y = random.Next(min, max)
                });
            }

            return data;
        }
    }
}
