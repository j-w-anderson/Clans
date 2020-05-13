using Engine.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Supply : BaseNotificationClass
    {
        // Storage for all objects not in play
        public ItemQuantity Yellow { get; set; } = new ItemQuantity(ELEMENT.YELLOW, 24);
        public ItemQuantity Red { get; set; } = new ItemQuantity(ELEMENT.RED, 24);
        public ItemQuantity Blue { get; set; } = new ItemQuantity(ELEMENT.BLUE, 24);
        public ItemQuantity Black { get; set; } = new ItemQuantity(ELEMENT.BLACK, 24);


        public ObservableCollection<ItemQuantity> Cubes { get; set; }
        public ObservableCollection<CitySticker> Equipment { get; set; } = new ObservableCollection<CitySticker>();
        public bool HasMilitaryEquipment => Equipment.Any(eq => eq.EqType == EQUIPTYPE.MILITARY);
        public bool HasMedicalEquipment => Equipment.Any(eq => eq.EqType == EQUIPTYPE.MEDICAL);

        public Supply()
        {
            Cubes = new ObservableCollection<ItemQuantity>() { Yellow, Red, Blue, Black };
        }

        public void TakeCube(ELEMENT color)
        {
            Cubes[(int)color].Count -= 1;
        }

        public void ReturnCube(ELEMENT color)
        {
            Cubes[(int)color].Count += 1;
        }

    }
}

