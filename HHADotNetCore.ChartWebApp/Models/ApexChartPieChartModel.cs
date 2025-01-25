namespace HHADotNetCore.ChartWebApp.Models
{
    public class ApexChartPieChartModel
    {
        public int[] Series { get; set; }
        public string[] Labels { get; set; }
    }

    public class ApexChartCombinedChartModel
    {
        public List<LineDataPoint> LineSeries { get; set; }
        public List<CandleDataPoint> CandleSeries { get; set; }
    }

    public class LineDataPoint
    {
        public DateTime X { get; set; }
        public decimal Y { get; set; }
    }

    public class CandleDataPoint
    {
        public DateTime X { get; set; }
        public decimal[] Y { get; set; }
    }

    public class ApexChartStackedAreaModel
    {
        public List<List<TimeSeriesDataPoint>> DataSet { get; set; }
    }

    public class TimeSeriesDataPoint
    {
        public DateTime X { get; set; }
        public decimal Y { get; set; }
    }

    public class ApexChartStackedBarModel
    {
        public List<SeriesData> Series { get; set; }
        public List<string> Categories { get; set; }
    }

    public class SeriesData
    {
        public string Name { get; set; }
        public List<int> Data { get; set; }
    }

    public class ApexChartScatterModel
    {
        public List<ScatterSeries> Series { get; set; }
    }

    public class ScatterSeries
    {
        public string Name { get; set; }
        public List<ScatterDataPoint> Data { get; set; }
    }

    public class ScatterDataPoint
    {
        public DateTime X { get; set; }
        public int Y { get; set; }
    }
}
