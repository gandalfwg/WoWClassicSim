using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WoWClassicSim.Models;

namespace WoWClassicSim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var itemId = textBox1.Text;
            WebRequest request = WebRequest.Create("https://classic.wowhead.com/item=" + itemId + "&xml");
            var response = (HttpWebResponse)request.GetResponse();
            using (Stream dataStream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(dataStream))
                {
                    var item = Item.FromStream(reader.ReadToEnd());
                    textBox2.Text = item.toString();
                    pictureBox1.ImageLocation = item.IconFilePath;
                    //textBox2.Text = String.Format("DisplayID: {0}, Int: {1}, Spell Crit: {2}, Spell Power: {3}, Stam: {4}", test.DisplayId, test.Intellect.ToString(), test.SpellCrit.ToString(), test.SpellPower.ToString(), test.Stamina.ToString());
                }
            }
        }
    }
}
