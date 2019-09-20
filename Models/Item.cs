using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WoWClassicSim.Helpers;

namespace WoWClassicSim.Models
{
    public enum Slot
    {
        Helmet = 1,
        Necklace = 2,
        Shoulders = 3,
        Waist = 6,
        Legs = 7,
        Feet = 8,
        Wrists = 9,
        Hands = 10,
        Ring = 11,
        Trinket = 12,
        OneHanded = 13,
        Shield = 14,
        Back = 16,
        TwoHanded = 17,
        Bag = 18,
        Chest = 20,
        MainHand = 21,
        OffHand = 22,
        HeldOffHand = 23,
        Thrown = 25,
        Ranged = 26,
        Relic = 28
    }

    public enum Class
    {
        Warrior = 1,
        Priest = 16,
        Mage = 128,
        Warlock = 256
    }

    public enum ItemQuality
    {
        Common = 1,
        Uncommon = 2,
        Rare = 3,
        Epic = 4,
        Legendary = 5
    }

    public enum ArmorType
    {
        Cloth = 1,
        Leather = 2,
        Mail = 3,
        Plate = 4
    }

    class Item
    {
        [JsonProperty("displayid")]
        public string DisplayId { get; set; }
        [JsonIgnore]
        public string IconFilePath { get; set; }
        [JsonIgnore]
        public string Name { get; set; }
        [JsonIgnore]
        public int ItemLevel { get; set; }
        [JsonIgnore]
        public ItemQuality Quality { get; set; }
        [JsonProperty("slotbak")]
        public Slot Slot { get; set; }
        [JsonProperty("subclass")]
        public ArmorType ArmorType { get; set; }
        [JsonProperty("armor")]
        public int Armor { get; set; }
        [JsonProperty("str")]
        public int Strength { get; set; }
        [JsonProperty("agi")]
        public int Agility { get; set; }
        [JsonProperty("int")]
        public int Intellect { get; set; }
        [JsonProperty("sta")]
        public int Stamina { get; set; }
        [JsonProperty("spi")]
        public int Spirit { get; set; }
        [JsonProperty("arcres")]
        public int ArcaneResist { get; set; }
        [JsonProperty("firres")]
        public int FireResist { get; set; }
        [JsonProperty("natres")]
        public int NatureResist { get; set; }
        [JsonProperty("frores")]
        public int FrostResist { get; set; }
        [JsonProperty("shares")]
        public int ShadowResist { get; set; }
        [JsonProperty("splcritstrkpct")]
        public int SpellCrit { get; set; }
        [JsonProperty("splhitpct")]
        public int SpellHit { get; set; }
        [JsonProperty("splpwr")]
        public int SpellPower { get; set; }
        [JsonProperty("splpen")]
        public int SpellPen { get; set; }
        [JsonProperty("manargn")]
        public int ManaRegen { get; set; }
        [JsonProperty("classes")]
        public int Classes { get; set; }
        [JsonProperty("itemset")]
        public ItemSet ItemSet { get; set; }

        public static Item FromStream(string ItemStream)
        {
            var xDoc = XDocument.Parse(ItemStream);
            var xItem = xDoc.Root.Element("item");
            var statsText = xItem.Element("jsonEquip").Value;
            statsText = statsText.Insert(0, "{");
            statsText += "}";
            var newItem = JsonConvert.DeserializeObject<Item>(statsText);
            if (newItem.Armor > 0)
            {
                var jsonText = xItem.Element("json").Value;
                jsonText = jsonText.Insert(0, "{");
                jsonText += "}";
                var armorTypeItem = JsonConvert.DeserializeObject<Item>(jsonText);
                newItem.ArmorType = armorTypeItem.ArmorType;
            }

            newItem.Name = xItem.Element("name").Value;
            newItem.ItemLevel = Convert.ToInt32(xItem.Element("level").Value);
            newItem.Quality = (ItemQuality)Convert.ToInt32(xItem.Element("quality").Attributes().FirstOrDefault().Value);

            newItem.IconFilePath = "https://wow.zamimg.com/images/wow/icons/large/" + xItem.Element("icon").Value + ".jpg";

            return newItem;
        }

        public string toString()
        {
            var toString = "";

            if (!String.IsNullOrEmpty(Name))
                toString += Name + "\r\n";
            if (Quality != 0)
                toString += Quality.ToString() + "\r\n";
            if (ItemLevel != 0)
                toString += "Item Level " + ItemLevel + "\r\n";
            if (Armor > 0)
            {
                toString += Slot.ToString() + ":" + ArmorType.ToString() + "\r\n";
                toString += Armor + " Armor\r\n";
            }
            else
                toString += Slot.ToString() + "\r\n";

            if (Strength > 0)
                toString += "+" + Strength.ToString() + " Strength" + "\r\n";
            if (Agility > 0)
                toString += "+" + Agility.ToString() + " Agility" + "\r\n";
            if (Intellect > 0)
                toString += "+" + Intellect.ToString() + " Intellect" + "\r\n";
            if (Stamina > 0)
                toString += "+" + Stamina.ToString() + " Stamina" + "\r\n";
            if (Spirit > 0)
                toString += "+" + Spirit.ToString() + " Spirit" + "\r\n";

            if (ArcaneResist > 0)
                toString += "+" + ArcaneResist.ToString() + " Arcane Resistance" + "\r\n";
            if (FireResist > 0)
                toString += "+" + FireResist.ToString() + " Fire Resistance" + "\r\n";
            if (NatureResist > 0)
                toString += "+" + NatureResist.ToString() + " Nature Resistance" + "\r\n";
            if (FrostResist > 0)
                toString += "+" + FrostResist.ToString() + " Frost Resistance" + "\r\n";
            if (ShadowResist > 0)
                toString += "+" + ShadowResist.ToString() + " Shadow Resistance" + "\r\n";

            if (Classes != 0)
                toString += ((Class)Classes).ToString() + "\r\n";

            if (SpellCrit > 0)
                toString += "Improves your chance to get a critical strike with spells by " + SpellCrit + "%.\r\n";
            if (SpellPower > 0)
                toString += "Increases damage and healing done by magical spells and effects by up to " + SpellPower + ".\r\n";
            if (SpellHit > 0)
                toString += "Improves your chance to hit with spells by " + SpellHit + "%.\r\n";
            if (SpellPen > 0)
                toString += "Decreases the magical resistances of your spell targets by " + SpellPen + ".\r\n";
            if (ManaRegen > 0)
                toString += "Restores " + ManaRegen + " mana per 5 sec.\r\n";

            if (ItemSet != 0)
                toString += ModelHelper.GetEnumDescription(ItemSet);

            return toString;
        }

        public List<int> CalcClasses()
        {
            var classes = new List<int>();

            return classes;
        }
    }
}