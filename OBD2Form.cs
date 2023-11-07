using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Net;
using System.IO;
using System.Text;

namespace OBD_II_WiFi
{
    public partial class OBD2Form : Form
    {
        TcpClient client = new TcpClient();
        NetworkStream stream;
        StreamReader reader;
        byte[] data;

        public OBD2Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ip_address = IPAddress.Parse(ipTextBox.Text); // parsing dell'indirizzo IP
            var port = Int32.Parse(portTextBox.Text); // parsing della porta, per ODB2 sempre 35000

            try
            {
                client.Connect(ip_address, port);

                stream = client.GetStream();
                reader = new StreamReader(stream, Encoding.ASCII);
            }
            catch (SocketException error)
            {
                display.Text = "Connection is NOT successfull, try again later! -> " + error.Message;
            }
            catch (Exception error)
            {
                display.Text = error.Message;
            }

            data = new byte[client.ReceiveBufferSize];

            /*Ping ping = new Ping();

            PingReply result = await ping.SendPingAsync(ip_to_ping);

            display.Text = "Device with IP: " + result.Address.ToString() + " pinged";
            display.Text += "\nTTL: " + result.RoundtripTime.ToString();*/

            //return result.Status == IPStatus.Success;
        }

        void configDevice()
        {

            send("\r");

            send("AT SP 0" + "\r");

            send("AT D" + "\r");

            send("AT DPN" + "\r");

            send("AT I" + "\r");

            send("0100" + "\r");

            send("AT H0" + "\r");

            send("AT AT2" + "\r");

            send("AT SH 7FF" + "\r");

            Console.WriteLine("Battery Voltage:");
            send("AT RV" + "\r");

            send("0902" + "\r");
        }

        public void send(string msg)
        {
            if (stream == null)
            {
                Console.WriteLine("No stream yet");
            }
            var msgByte = Encoding.ASCII.GetBytes(msg);
            try
            {
                stream.Write(msgByte, 0, msgByte.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}