using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoWClassicSim.Models
{
    enum ItemSet
    {
        Magister = 181,
        [Description("Arcanist Regalia")]
        Arcanist = 201,
        [Description("Netherwind Regalia")]
        Netherwind = 210,
        HordeEpicMage = 387,
        AllianceEpicMage = 388,
        Highlander = 473,
        Illusionist = 482,
        Defiler = 485,
        Enigma = 503,
        VaultedSecrets = 504,
        Ironweave = 520,
        [Description("Frostfire Regalia")]
        Frostfire = 526,
        HordeRareMage = 542,
        AllianceRareMage = 546
    }

    public class ItemSetBonus
    {
        public int Armor { get; set; }
        public int SpellPower { get; set; }
        public int SpellPen { get; set; }
        public int AllResist { get; set; }
        public bool Netherwind8Piece { get; set; }
        public int EvocationCDReduction { get; set; }
        //Fairly useless for the individual - public bool FrostFire6Piece { get; set; }
        //20% chance on spell hit to make target take 200 more damage from the next spell
    }
}
