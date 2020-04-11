using Engine.Utils;
using PandemicLegacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public abstract class Card
    {
        public string Name { get; set; }
        public bool HasEffect { get; set; }

        public Card(string name)
        {
            Name = name;
            HasEffect = false;
        }

        virtual public void Activate(GameSession Game, Player Holder) { }
    }

    public class PlayerCard : Card
    {
        public ELEMENT Color { get; set; }

        public PlayerCard(string name, ELEMENT color) : base(name)
        {
            Color = color;
            Name = name;
        }
    }

    public class CityCard : PlayerCard
    {
        public City City { get; set; }
        public CitySticker Sticker { get; set; } = null;

        public CityCard(City city) : base(city.Name, city.TrueColor)
        {
            City = city;
        }

        public void AddSticker(CitySticker sticker)
        {
            Sticker = sticker;
        }
    }

    public class EpidemicCard : PlayerCard
    {
        public EpidemicCard() : base("Epidemic", ELEMENT.EPIDEMIC)
        {

        }
    }

    public class InfectionCard : Card
    {
        public City City { get; set; }
        public ELEMENT Color { get; set; }

        public InfectionCard(City city) : base(city.Name)
        {
            City = city;
            Color = city.TrueColor;
        }

    }
}
