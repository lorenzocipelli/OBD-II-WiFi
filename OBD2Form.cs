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
                                printHexToBin(data); // ex: "0100 41 00 BE 3E A8 13"
                            }
                            else
                            {
                                writeDisplay(data);
                            }*/
                            writeDisplay(data); // rimuovere se scommentato il blocco sopra
                            data = "";
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
                display.Text += "\n" + text;
            }
        }

        private void askPIDButton_Click(object sender, EventArgs e)
        {
            findOutPIDs();
        }

        private async void findOutPIDs()
        {
            await Task.Run(() =>
            {
                //send("AT H1" + "\r"); // attivo gli headers
                send("AT D" + "\r"); // impostazioni di fabbrica
                send("AT CRA 7E8" + "\r"); // permettere solamente alla ECU di rispondere
                //send("AT H0" + "\r"); // disattivo gli headers
                converting = true;

                Task.Delay(2000).Wait();
                send("0100" + "\r");
                // 0100 41 00 BE 3E A8 13  
                Task.Delay(2000).Wait();
                send("0120" + "\r");

                Task.Delay(2000).Wait();
                send("0140" + "\r");

                Task.Delay(2000).Wait();
                send("0160" + "\r");

                Task.Delay(2000).Wait();
                send("0180" + "\r");

                Task.Delay(2000).Wait();
                send("01A0" + "\r"); // ultimo

                Task.Delay(2000).Wait();
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
            char[] to_print_array = new char[] { };
            char[] to_print_array_tmp = new char[] { };
            hex = hex.Replace(" ", string.Empty);
            writeDisplay(hex);
            var test = Convert.FromHexString(hex);
            foreach (byte spaced in test.Skip(4)) // parte di stampa
            {
                to_print = Convert.ToString(spaced, 2).PadLeft(8, '0');
                writeDisplay(to_print);

                to_print_array_tmp = to_print.ToCharArray();
                foreach (char bit in to_print_array_tmp) {
                    to_print_array = to_print_array.Append(bit).ToArray(); ;
                }
            }

            foreach (char bit in to_print_array) {
                writeDisplay(bit.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            printHexToBin("010041 00 BE 3E A8 13 ");
            /*printHexToBin("012041 20 80 07 B0 11 ");
            printHexToBin("014041 40 FE D0 84 01 ");
            printHexToBin("016041 60 08 08 00 01 ");
            printHexToBin("018041 80 00 00 00 01 ");
            printHexToBin("01A041 A0 00 00 00 00 ");*/
        }
    }
}