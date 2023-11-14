using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Net;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace OBD_II_WiFi
{
    public partial class OBD2Form : Form
    {
        TcpClient client = new TcpClient();
        NetworkStream stream;
        StreamReader reader;
        string data;

        delegate void writeDisplayDelegate(string toDisplay);
        bool stopListening = false;
        bool converting = false;

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
                        n_try = 6;
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

            if (n_try == 6) startListening(); // apertura thread di ascolto della stream
            else { display.Text += "\n" + "IP address is unreachable at the moment, try again later..."; }
        }

        private async void startListening()
        {
            display.Text = "Connection established!";
            await Task.Run(() =>
            {
                while (!stopListening)
                {
                    try
                    {
                        byte[] buffer = new byte[client.ReceiveBufferSize];
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        byte[] mesajj = new byte[bytesRead];

                        for (int i = 0; i < bytesRead; i++)
                        {
                            mesajj[i] = buffer[i];
                        }

                        data += Encoding.Default.GetString(mesajj, 0, bytesRead).Replace("\r", "");
                        if (data.Length > 0)
                        {
                            /*if (converting)
                            {
                                printHexToBin("0100 41 00 BE 3E A8 13");
                            }
                            else
                            {
                                writeDisplay(data);
                            }*/
                            writeDisplay(data);

                        }
                    }
                    catch (IOException error)
                    {
                        stopListening = true;
                        writeDisplay(error.Message);
                    }

                }
            });
        }

        private void writeDisplay(string text)
        {
            if (display.InvokeRequired)
            {
                writeDisplayDelegate d = new writeDisplayDelegate(writeDisplay);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                display.Text += "\n" + text.TrimEnd('\r', '\n');
            }
        }

        private void askPIDButton_Click(object sender, EventArgs e)
        {
            findOutPIDs();
        }

        private async void findOutPIDs()
        {
            converting = true;
            await Task.Run(() =>
            {
                send("AT H1"); // per mostrare gli headers, per capire quale componente sta inviando indietro
                /* dopodiché bisognerà mandare solamente AT CRA 7E8, se per esempio il motore è 7E8
                   successivamente si può anche togliere la possibilità di vedere gli header con AT H0 */
                send("0100" + "\r");
                // 0100 41 00 BE 3E A8 13  
                // 0100 41 00 BE 3E A8 13
                /*Task.Delay(2000).Wait();
                send("0120" + "\r");

                Task.Delay(2000).Wait();
                send("0140" + "\r");

                Task.Delay(2000).Wait();
                send("0160" + "\r");

                Task.Delay(2000).Wait();
                send("0180" + "\r");

                Task.Delay(2000).Wait();
                send("01A0" + "\r"); // ultimo

                Task.Delay(2000).Wait();*/
            });
            converting = false;
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
                display.Text += "\nSent: " + msg;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void buttonStopListening_Click(object sender, EventArgs e)
        {
            stopListening = true;
            client.Close();
        }

        private void printHexToBin(string hex) {
            string to_print = "";
            hex = hex.Replace(" ", string.Empty);
            writeDisplay(hex);
            var test = Convert.FromHexString(hex);
            foreach (byte spaced in test.Skip(4))
            {
                to_print = Convert.ToString(spaced, 2).PadLeft(8, '0');
                writeDisplay(to_print);
            }
        }
    }
}