using Engine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Model
{
    public abstract class EventCard : PlayerCard
    {
        public string Image { get; set; }

        internal GameSession Game;
        internal Player Holder;
        internal UIMode ReturnTo;

        public EventCard(string name, string image) : base(name, ELEMENT.EVENT)
        {
            Image = "/PandemicLegacy;component/Images/Events/" + image;
            HasEffect = true;
        }
    }

    public class Airlift : EventCard
    {
        SelectPlayer PlayerListener;
        SelectCity CityListener;
        Player Target;

        public Airlift() : base("Airlift", "Event--AirLift.png")
        {

        }

        public override void Activate(GameSession game, Player holder)
        {
            Game = game;
            // Select Pawn  
            PlayerListener = new SelectPlayer(Game);
            PlayerListener.SendPlayer += Listener_SendPlayer;
            ReturnTo = Game.CurrentMode;
            game.CurrentMode = PlayerListener;
        }

        private void Listener_SendPlayer(SelectPlayer sp, Player player)
        {
            Target = player;
            PlayerListener.SendPlayer -= Listener_SendPlayer;
            // Select City
            CityListener = new SelectCity(Game);
            CityListener.SendCity += Listener_SendCity;
            Game.CurrentMode = CityListener;
        }

        private void Listener_SendCity(SelectCity sp, City location)
        {
            Target.Location = location;
            CityListener.SendCity -= Listener_SendCity;
            Game.CurrentMode.FinishCard(Holder, this);
            Game.CurrentMode = ReturnTo;
            Game.CurrentMode.UpdateFlags();
        }
    }

    public class BorrowedTime : EventCard
    {
        public BorrowedTime() : base("Borrowed Time", "Event--BorrowedTime.png")
        {

        }

        public override void Activate(GameSession Game, Player Holder)
        {
            Game.CurrentPlayer.Actions += 2;
            Game.CurrentMode.FinishCard(Holder, this);
            Game.CurrentMode.UpdateFlags();
        }
    }
    public class Forecast : EventCard
    {

        public Forecast() : base("Forecast", "Event--Forecast.png")
        {

        }

        public override void Activate(GameSession Game, Player Holder)
        {
            throw new NotImplementedException();
        }
    }
    public class GovernmentGrant : EventCard
    {
        SelectCity CityListener;

        public GovernmentGrant() : base("Government Grant", "Event--GovernmentGrant.png")
        {
        }

        public override void Activate(GameSession game, Player holder)
        {
            Game = game;
            Holder = holder;
            // Select City
            CityListener = new SelectCity(Game);
            CityListener.SendCity += Listener_SendCity;
            ReturnTo = Game.CurrentMode;
            Game.CurrentMode = CityListener;
        }

        private void Listener_SendCity(SelectCity sp, City location)
        {
            location.ResearchStation = true;
            CityListener.SendCity -= Listener_SendCity;
            Game.CurrentMode.FinishCard(Holder, this);
            Game.CurrentMode = ReturnTo;
            Game.CurrentMode.UpdateFlags();
        }
    }
    public class LocalInitiative : EventCard
    {

        SelectCity CityListener;

        public LocalInitiative() : base("Local Initiative", "Event--LocalInitiative.png")
        {

        }

        public override void Activate(GameSession game, Player holder)
        {
            Game = game;
            Holder = holder;
            // Select City
            CityListener = new SelectCity(Game);
            CityListener.SendCity += Listener_SendCity;
            ReturnTo = Game.CurrentMode;
            Game.CurrentMode = CityListener;
        }

        private void Listener_SendCity(SelectCity sp, City location)
        {
            location.AddQuarantine();
            CityListener.SendCity -= Listener_SendCity;
            Game.CurrentMode.FinishCard(Holder, this);
            Game.CurrentMode = ReturnTo;
            Game.CurrentMode.UpdateFlags();
        }
    }
    public class NewAssignment : EventCard
    {

        public NewAssignment() : base("New Assignment", "Event--NewAssignment.png")
        {

        }

        public override void Activate(GameSession Game, Player Holder)
        {
            throw new NotImplementedException();
        }
    }
    public class OneQuietNight : EventCard
    {

        public OneQuietNight() : base("One Quiet Night", "Event--OneQuietNight.png")
        {

        }

        public override void Activate(GameSession Game, Player Holder)
        {
            //Add Infect Shield
            Game.InfectShield += 1;
        }
    }
    public class RemoteOutpost : EventCard
    {

        SelectCity CityListener;

        public RemoteOutpost() : base("Remote Outpost", "Event--RemoteOutpost.png")
        {

        }

        public override void Activate(GameSession game, Player holder)
        {
            Game = game;
            Holder = holder;
            // Select City
            CityListener = new SelectCity(Game);
            CityListener.SendCity += Listener_SendCity;
            ReturnTo = Game.CurrentMode;
            Game.CurrentMode = CityListener;
        }

        private void Listener_SendCity(SelectCity sp, City location)
        {
            location.MilitaryBase = true;
            CityListener.SendCity -= Listener_SendCity;
            Game.CurrentMode.FinishCard(Holder, this);
            Game.CurrentMode = ReturnTo;
            Game.CurrentMode.UpdateFlags();
        }
    }

    public class RemoteTreatment : EventCard
    {

        public RemoteTreatment() : base("Remote Treatment", "Event--RemoteTreatment.png")
        {

        }

        public override void Activate(GameSession Game, Player Holder)
        {
            for (int i = 0; i < 2; i += 1)
            {
                //Select City
                //(Select Cube)
            }
            throw new NotImplementedException();
        }
    }
    public class ResilientPopulation : EventCard
    {
        public ResilientPopulation() : base("Resilient Population", "Event--ResilientPopulation.png")
        {

        }

        public override void Activate(GameSession Game, Player Holder)
        {
            //Select Card in Infection Discard pile
            //Delete it
            throw new NotImplementedException();
        }
    }

    public class Vaccine : EventCard
    {

        public Vaccine() : base("Vaccine", "Event--Vaccine.png")
        {

        }

        public override void Activate(GameSession Game, Player Holder)
        {
            throw new NotImplementedException();
        }
    }
}
