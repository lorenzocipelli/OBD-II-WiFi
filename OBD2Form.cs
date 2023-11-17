using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using System.Net;
using System.IO;
using System.Text;
using System.Diagnostics;
using OBD_II_WiFi.classes;

namespace OBD_II_WiFi
{
    public partial class OBD2Form : Form
    {
        TcpClient client = new TcpClient();
        NetworkStream stream;
        StreamReader reader;
        string data;

        delegate void writeDisplayDelegate(string toDisplay);

        bool runEngineMonitoring = true;
        bool stopListening = false;
        bool converting = false;

        public OBD2Form()
        {
            InitializeComponent();
        }

        private async void startListening()
        {
            Regex rgx = new Regex("[^a-zA-Z0-9 -]"); // keep only alphanumerics
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
                        data = rgx.Replace(data, "");
                        if (data.Length > 0)
                        {
                            if (converting)
                            {
                                printPIDs(data);
                            }
                            else
                            {
                                data = data.Replace(" ", string.Empty);
                                var tmp = Convert.FromHexString(data);
                                // non ancora fatto il parsing per una richiesta diversa
                                // da quella dei PIDs
                                if (data.Contains("41 0B"))
                                { // Intake manifold absolute pressure (MAP)
                                    int map = tmp[4]*1000; // il valore arriva in kPa
                                    writeDisplay("MAP: " + map.ToString() + " [Pascal]");
                                } else if (data.Contains("41 0C"))
                                { // Engine speed
                                    int rmp = (tmp[4] * 256 + tmp[5]) / 4; // il valore arriva in rpm * 4
                                    writeDisplay("RPM: " + rmp.ToString() + " [rpm]");
                                } else if (data.Contains("41 0F"))
                                { // Intake air temperature (IAT)
                                    int iat = tmp[4] - 40; // il valore arriva in iat + 40
                                    writeDisplay("IAT: " + iat.ToString() + " [C°]");
                                } else if (data.Contains("41 24"))
                                { // lambda air-fuel ratio
                                    int lambda = (tmp[4] * 256 + tmp[5]) * (2 / 65536);
                                    writeDisplay("lambda: " + lambda + " [ratio V]");
                                }
                                else if (data.Contains("41 66"))
                                { // lambda air-fuel ratio (non dovrebbe andare teoricamente)
                                    int maf = (tmp[4]*256 + tmp[5]) / 100;
                                    writeDisplay("MAF: " + maf.ToString() + " [g/s]");
                                }
                            }
                            //writeDisplay(data); // rimuovere se scommentato il blocco sopra
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
            runEngineMonitoring = false;
            client.Close();
        }

        private void printPIDs(string hex) {
            string to_print = "";
            char[] to_print_array = new char[] { };
            char[] to_print_array_tmp = new char[] { };

            writeDisplay("\n" + hex);

            hex = hex.Replace(" ", string.Empty);
            var test = Convert.FromHexString(hex);
            int responde_index = test[3];
            //writeDisplay("Index: " + responde_index.ToString());

            foreach (byte spaced in test.Skip(4)) // parte di stampa
            {
                to_print = Convert.ToString(spaced, 2).PadLeft(8, '0');
                //writeDisplay(to_print);

                to_print_array_tmp = to_print.ToCharArray();
                foreach (char bit in to_print_array_tmp) {
                    to_print_array = to_print_array.Append(bit).ToArray(); ;
                }
            }

            // to_print_array è una lista di 32 "bit", sono dei char
            string json;
            JsonNode pids;
            int value;
            int j = 0;

            if (responde_index == 0) // se la prima decodifica prendo lo scheletro (tutto nullo)
            {
                using (StreamReader r = new StreamReader("../../../dati/datafile.json"))
                {
                    json = r.ReadToEnd();
                    pids = JsonNode.Parse(json)!;
                }
            }
            else { // altrimenti continuo la modifica del file
                using (StreamReader r = new StreamReader("../../../dati/datafile_mod.json"))
                {
                    json = r.ReadToEnd();
                    pids = JsonNode.Parse(json)!;
                }
            }

            for (int i = responde_index; i < responde_index + to_print_array.Length; i++)
            { // fixed number: 169
                value = int.Parse(to_print_array[j].ToString());
                if (value == 1) {
                    writeDisplay("* " + pids["PIDs"][i]["description"].ToString());
                }
                pids["PIDs"][i]["value"] = value;
                j++;
            }

            //convert to JSON string
            var jsonOptions = new JsonSerializerOptions() { WriteIndented = true };
            var coderJson = pids.ToJsonString(jsonOptions);

            System.IO.File.WriteAllText("../../../dati/datafile_mod.json", coderJson);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // funzione di testing, teoricamente non serve più a nulla
            printPIDs("010041 00 BE 3E A8 13 ");
            printPIDs("012041 20 80 07 B0 11 ");
            printPIDs("014041 40 FE D0 84 01 ");
            printPIDs("016041 60 08 08 00 01 ");
            printPIDs("018041 80 00 00 00 01 ");
            printPIDs("01A041 A0 10 00 00 00 ");
        }

        private async void buttonFuelData_Click(object sender, EventArgs e)
        {
            while (runEngineMonitoring) {
                await Task.Run(() =>
                {
                    send("010B" + "\r"); // Intake manifold absolute pressure (MAP)
                    Task.Delay(2000).Wait();
                    send("010F" + "\r"); // Intake air temperature (IAT)
                    Task.Delay(2000).Wait();
                    send("010C" + "\r"); // Engine speed
                    Task.Delay(2000).Wait();
                    send("0124" + "\r"); // lamda air-fuel ratio
                    Task.Delay(2000).Wait();
                    send("0166" + "\r"); // Mass Air Flow
                });
            }
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

        private async void askPIDButton_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                //send("AT H1" + "\r"); // attivo gli headers
                send("AT D" + "\r"); // impostazioni di fabbrica
                send("AT CRA 7E8" + "\r"); // permettere solamente alla ECU di rispondere
                //send("AT H0" + "\r"); // disattivo gli headers

                Task.Delay(2000).Wait();
                converting = true;
                send("0100" + "\r");
                  
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
    }
}