using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Utils
{
    public enum TERRAIN
    {
        STEPPE,
        GRASSLAND,
        FOREST,
        MOUNTAIN,  
        LAKE
    }

    public enum MODE
    {
        MainAction,
        SelectCard,
        SelectPawn,
        SelectCity,
        SelectPath,
        SelectCube,
        SelectCardFromDiscard,
        SelectEquipment,
        Pause,
        Upgrade,
        SelectDisease,
        SelectStructure,
        SelectCharacter,
        SelectCharacterUpgrade,
        SelectUnfundedEvent,
        SelectCityCard
    }

    public enum ROLE
    {
        Colonel,
        Dispatcher,
        Generalist,
        Immunologist,
        Medic,
        OperationsExpert,
        QuarantineSpecialist,
        Researcher,
        Scientist,
        Soldier,
        Virologist
    }

    public enum ELEMENT
    {
        YELLOW,
        RED,
        BLUE,
        BLACK,
        FADED,
        VACCINE,
        QUARANTINE,
        NUKED,
        EVENT,
        EPIDEMIC
    }

    public enum REGION
    {
        NAMERICA,
        SAMERICA,
        AFRICA,
        EUROPE,
        ASIA,
        PACIFIC
    }

    public enum STATUS
    {
        NORMAL,
        UNSTABLE,
        RIOTING2,
        RIOTING3,
        COLLAPSING,
        FALLEN
    }

    public enum STRUCTURE
    {
        STATION,
        BASE,
        FACTORY

    }

    public enum EQUIPTYPE
    {
        MEDICAL,
        MILITARY
    }

    public enum RESOURCE
    {
        CHILI,
        INDIGO,
        PEPPER,
        SAFFRON,
        TEA,
        GOLD
    }

    public enum PHASE
    {
        FILLHOLD,
        OVERALLVALUE,
        LEVELADVANCE,
        LEVELWINNERS,
        LEVELBONUS,
        NEXTDAY,
        POSTGAME
    }

}

