using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Net;

namespace OBD_II_WiFi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var ip_to_ping = "192.168.0.149";
            /*
            public IPAddress Address { get; }
            public byte[] Buffer { get; }
            public PingOptions Options { get; }
            public long RoundtripTime { get; }
            public IPStatus Status { get; }
            */

            Ping ping = new Ping();

            PingReply result = await ping.SendPingAsync(ip_to_ping);

            display.Text = "Device with IP: " + result.Address.ToString() + " pinged";
            display.Text += "\nTTL: " + result.RoundtripTime.ToString();

            //return result.Status == IPStatus.Success;
        }
    }
}