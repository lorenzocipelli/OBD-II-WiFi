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
        string data;

        delegate void writeDisplayDelegate(string toDisplay);

        public OBD2Form()
        {
            InitializeComponent();
        }

        private async void initConnButton_Click(object sender, EventArgs e)
        {
            int n_try = 0;
            var ip_address = IPAddress.Parse(ipTextBox.Text); // parsing dell'indirizzo IP
            var port = Int32.Parse(portTextBox.Text); // parsing della porta, per ODB2 sempre 35000          

            await Task.Run(() =>
            {
                while (n_try < 5)
                {
                    try
                    {
                        writeDisplay("Trying to connect...");
                        client.Connect(ip_address, port);

                        stream = client.GetStream();
                        reader = new StreamReader(stream, Encoding.ASCII);
                    }
                    catch (SocketException error)
                    {
                        n_try++;
                        writeDisplay("Attempt #" + n_try + " " + error.Message);
                    }
                }
            });

            if (n_try < 5) startListening(); // apertura thread di ascolto della stream
            else { display.Text += "\n" + "IP address is uncreachable at the moment, try again later..."; }
        }

        private async void startListening() {
            await Task.Run(() =>
            {
                byte[] buffer = new byte[client.ReceiveBufferSize];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                byte[] mesajj = new byte[bytesRead];

                for (int i = 0; i < bytesRead; i++)
                {
                    mesajj[i] = buffer[i];
                }

                data += Encoding.Default.GetString(mesajj, 0, bytesRead).Replace("\r", " ");
                writeDisplay(data);
            });
        }

        private void writeDisplay(string text) {
            if (display.InvokeRequired)
            {
                writeDisplayDelegate d = new writeDisplayDelegate(writeDisplay);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                display.Text += "\n" + text;
            }
        }

        private void askPIDButton_Click(object sender, EventArgs e)
        {
            findOutPIDs();
        }

        void findOutPIDs() {
            send("0100" + "\r");
            //send("0120" + "\r");
            //send("0140" + "\r");
            //send("0160" + "\r");
            //send("0180" + "\r");
            //send("01A0" + "\r"); // ultimo, non ritorna tutto come gli altri
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
                //stream.Write(msgByte, 0, msgByte.Length);
                display.Text += "\nSent: " + msg;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        private async Task pingFunctionAsync() {
            var ip_address = IPAddress.Parse(ipTextBox.Text);
            Ping ping = new Ping();

            PingReply result = await ping.SendPingAsync(ip_address);

            display.Text = "Device with IP: " + result.Address.ToString() + " pinged";
            display.Text += "\nTTL: " + result.RoundtripTime.ToString();

            //return result.Status == IPStatus.Success;
        }
    }
}