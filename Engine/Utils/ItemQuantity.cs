using System;
using Engine.Utils;

namespace Engine
{
    public class ItemQuantity : BaseNotificationClass
    {
        private int _count;

        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                OnPropertyChanged(nameof(Count));
                OnPropertyChanged(nameof(Any));
            }
        }

        public ELEMENT Element { get; set; }

        public bool Selectable { get; set; } = false;

        public bool Any => Count != 0;

        public ItemQuantity(ELEMENT item, int count = 1)
        {
            Element = item;
            Count = count;
        }

        public void Add(int count)
        {
            Count += count;
        }
    }
}