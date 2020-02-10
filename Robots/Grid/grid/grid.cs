using System;
using System.Linq;
using cAlgo.API;
using cAlgo.API.Indicators;
using cAlgo.API.Internals;
using cAlgo.Indicators;

namespace cAlgo
{
    [Robot(TimeZone = TimeZones.UTC, AccessRights = AccessRights.None)]
    public class xGRIDoil : Robot
    {
        [Parameter("Volume", DefaultValue = 1)]
        public int VOLUME { get; set; }

        [Parameter("SL", DefaultValue = 0)]
        public double SL { get; set; }

        [Parameter("TP", DefaultValue = 10)]
        public double TP { get; set; }

        protected override void OnStart()
        {

            Positions.Opened += OnPositionsOpened;
            Positions.Closed += OnPositionsClosed;
        }

        protected override void OnTick()
        {
            var labelASK = Math.Round(Symbol.Ask).ToString();
            var labelBID = Math.Round(Symbol.Bid).ToString();

            //    var labelASK = Math.Round(Symbol.Ask).ToString();
            //     var labelBID = Math.Round(Symbol.Bid).ToString();
            var label = Math.Round(Symbol.Bid).ToString();

            //  var ASKPositions = Positions.FindAll(labelASK);
            //  var BIDPositions = Positions.FindAll(labelBID);
            var labelPositions = Positions.FindAll(label);


            if (labelPositions.Length == 0)
            {
                ExecuteMarketOrder(TradeType.Buy, Symbol, VOLUME, label, SL, TP);
                ExecuteMarketOrder(TradeType.Sell, Symbol, VOLUME, label, SL, TP);
            }

        }
            /*     if (labelPositions.Length == 0)
            {
                ExecuteMarketOrder(TradeType.Sell, Symbol, VOLUME, label, SL, TP);
            }*/


                private void OnPositionsOpened(PositionOpenedEventArgs args)
        {

        }

        private void OnPositionsClosed(PositionClosedEventArgs args)
        {

        }

        protected override void OnStop()
        {
            Print("#", Positions.Count);
            foreach (var position in Positions)
            {
                ClosePositionAsync(position);
            }
            foreach (var pendingOrder in PendingOrders)
            {
                CancelPendingOrderAsync(pendingOrder);
            }
        }


    }
}
