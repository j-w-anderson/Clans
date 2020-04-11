using Engine.Utils;

namespace Engine
{
    public abstract class CitySticker
    {
        public string Name { get; set; }
        public EQUIPTYPE EqType { get; set; }

        public CitySticker(string name)
        {
            Name = name;
        }
    }

    public class UnfundedEvent : CitySticker
    {
        public UnfundedEvent(string name) : base(name) { }
    }

    public class Equipment : CitySticker
    {
        public EQUIPTYPE EType { get; set; }

        public Equipment(string name, EQUIPTYPE etype) : base(name)
        {
            EType = etype;
        }
    }
}