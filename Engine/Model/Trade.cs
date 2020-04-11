using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Model
{

    public class Trade
    {
        public Player FromPlayer { get; set; }
        public Player ToPlayer { get; set; }
        public PlayerCard TradeCard { get; set; }

        public Trade(Player fromPlayer, Player toPlayer, PlayerCard tradeCard)
        {
            FromPlayer = fromPlayer;
            ToPlayer = toPlayer;
            TradeCard = tradeCard;
        }

        public void Execute()
        {
            FromPlayer.Hand.Remove(TradeCard);
            ToPlayer.Hand.Add(TradeCard);
        }
    }
}
