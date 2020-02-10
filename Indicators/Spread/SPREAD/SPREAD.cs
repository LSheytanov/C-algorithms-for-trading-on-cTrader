using System;
using cAlgo.API;
using cAlgo.API.Internals;
using cAlgo.API.Indicators;
using cAlgo.Indicators;

namespace cAlgo
{
    [Indicator(IsOverlay = false, TimeZone = TimeZones.UTC, ScalePrecision = 2)]
    public class SPREAD : Indicator
    {

        [Parameter("Symbol high", DefaultValue = "XBRUSD")]
        public string X_Symbol { get; set; }

        [Parameter("coeff Symbol high", DefaultValue = 1)]
        public int coeffhigh { get; set; }

        [Parameter("Symbol low", DefaultValue = "XTIUSD")]
        public string Y_Symbol { get; set; }

        [Parameter("coeff Symbol low", DefaultValue = 1)]
        public int coefflow { get; set; }

        [Output("MAIN", PlotType = PlotType.Line, LineColor = "White", LineStyle = LineStyle.Solid)]
        public IndicatorDataSeries Result { get; set; }

        private IndicatorDataSeries X_Series;
        private IndicatorDataSeries Y_Series;

        private MarketSeries X_Source;
        private MarketSeries Y_Source;

        protected override void Initialize()
        {
            X_Series = CreateDataSeries();
            Y_Series = CreateDataSeries();

            X_Source = MarketData.GetSeries(X_Symbol, TimeFrame);
            Y_Source = MarketData.GetSeries(Y_Symbol, TimeFrame);
        }

        public override void Calculate(int index)
        {
            X_Series[index] = X_Source.Close[X_Source.OpenTime.GetIndexByTime(MarketSeries.OpenTime[index])];
            Y_Series[index] = Y_Source.Close[Y_Source.OpenTime.GetIndexByTime(MarketSeries.OpenTime[index])];

            Result[index] = coeffhigh * X_Series[index] - coefflow * Y_Series[index];
        }
    }
}
