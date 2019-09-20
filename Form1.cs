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
        private Item CurrentItem;

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
                    CurrentItem = Item.FromStream(reader.ReadToEnd());
                    //textBox2.Text = CurrentItem.toString();
                    pictureBox1.ImageLocation = CurrentItem.IconFilePath;
                    ToolTip toolTip = new ToolTip();
                    // Set up the delays for the ToolTip.
                    toolTip.AutoPopDelay = 5000;
                    toolTip.InitialDelay = 1000;
                    toolTip.ReshowDelay = 500;
                    // Force the ToolTip text to be displayed whether or not the form is active.
                    toolTip.ShowAlways = true;

                    toolTip.SetToolTip(this.pictureBox1, CurrentItem.toString());

                    //textBox2.Text = String.Format("DisplayID: {0}, Int: {1}, Spell Crit: {2}, Spell Power: {3}, Stam: {4}", test.DisplayId, test.Intellect.ToString(), test.SpellCrit.ToString(), test.SpellPower.ToString(), test.Stamina.ToString());
                }
            }
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            if(CurrentItem != null)
                textBox2.Text = CurrentItem.toString();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }
    }
}
