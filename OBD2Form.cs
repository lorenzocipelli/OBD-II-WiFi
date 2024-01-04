using System.Net.Sockets;
using System.Globalization;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Net;
using System.Text;
using System.Diagnostics;
using OBD_II_WiFi.classes;
using CsvHelper;
using CsvHelper.Configuration;
using ScottPlot;

namespace OBD_II_WiFi
{
    public partial class OBD2Form : Form
    {
        static HttpClient httpClient = new HttpClient();
        const string URL = "http://localhost:8000/predict";
        string messageFromAPI;

        TcpClient client = new TcpClient();
        RunInfo currentInfo = new RunInfo();
        NetworkStream stream;
        StreamReader reader;
        int mode;
        string data;

        delegate void writeDisplayDelegate(string toDisplay);
        delegate void updateChartDelegate();

        bool runEngineMonitoring = true;
        bool stopListening = false;
        bool converting = false;
        bool append = true;

        readonly ScottPlot.Plottable.DataLogger Logger_speed;
        readonly ScottPlot.Plottable.DataLogger Logger_rpm;
        readonly ScottPlot.Plottable.DataLogger Logger_load;

        public OBD2Form()
        {
            InitializeComponent();

            httpClient.BaseAddress = new Uri(URL);

            Logger_speed = chart_speed.Plot.AddDataLogger(label: "speed", color: Color.LightGoldenrodYellow, lineWidth: 2);
            Logger_rpm = chart_rpm.Plot.AddDataLogger(label: "rpm", color: Color.LightSalmon, lineWidth: 2);
            Logger_load = chart_load.Plot.AddDataLogger(label: "load", color: Color.LightGreen, lineWidth: 2);

            initializeCharts();
        }

        private async void startListening()
        {
            Regex rgx = new Regex("[^a-zA-Z0-9 -]"); // keep only alphanumerics
            display.Text = "Connection established!";

            JsonNode responseAsJson;
            string responseContent;
            string responseState;

            await Task.Run(async () =>
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
                        if (data.Length > 0) // limitare la lettura anche a valori minori di un tot
                        // tanto non ha senso mandare in lettura ">" oppure gli errori in risposta
                        {
                            if (converting)
                            {
                                printPIDs(data);
                            }
                            else
                            {
                                Debug.WriteLine(data);
                                data = data.Replace(" ", string.Empty);
                                try
                                {
                                    var tmp = Convert.FromHexString(data);

                                    if (data.Contains("410C"))
                                    { // Engine speed
                                        int rpm = (tmp[4] * 256 + tmp[5]) / 4; // il valore arriva in rpm * 4
                                        currentInfo.RPM = rpm;
                                        writeDisplay("RPM: " + rpm.ToString() + " [rpm]");
                                    }
                                    else if (data.Contains("4110"))
                                    { // Mass Air Flow
                                        int maf = (tmp[4] * 256 + tmp[5]) / 4; // il valore arriva in rpm * 4
                                        currentInfo.MAF = maf;
                                        writeDisplay("MAF: " + maf.ToString() + " [g/s]");
                                    }
                                    else if (data.Contains("410F"))
                                    { // Intake air temperature (IAT)
                                        int iat = tmp[4] - 40; // il valore arriva in iat + 40
                                        currentInfo.IAT = iat;
                                        writeDisplay("IAT: " + iat.ToString() + " [C°]");
                                    }
                                    else if (data.Contains("4149"))
                                    { // Acc. Pedal Pos. D
                                        int accD = Convert.ToInt32(tmp[4] / 2.55);
                                        currentInfo.ACCPEDAL = accD;
                                        writeDisplay("Acc. Pedal Pos. D: " + accD.ToString() + " [%]");
                                    }
                                    else if (data.Contains("4111"))
                                    { // Throttle Position
                                        int thPos = Convert.ToInt32(tmp[4] / 2.55);
                                        currentInfo.THROTTLEPOS = thPos;
                                        writeDisplay("Throttle Position: " + thPos.ToString() + " [%]");
                                    }
                                    else if (data.Contains("410D"))
                                    { // Vehicle Speed
                                        int speed = tmp[4];
                                        currentInfo.SPEED = speed;
                                        writeDisplay("Vehicle Speed: " + speed.ToString() + " [km/h]");
                                    }
                                    else if (data.Contains("4104"))
                                    { // Engine Load
                                        int load = Convert.ToInt32(tmp[4] / 2.55);
                                        currentInfo.ENGINELOAD = load;
                                        writeDisplay("Engine Load: " + load.ToString() + " [%]");
                                    }
                                    else if (data.Contains("411F"))
                                    { // Run Time Since Engine Start
                                        double runtime = tmp[4] * 256 + tmp[5];
                                        currentInfo.RUNTIME = runtime;
                                        writeDisplay("Run Time: " + runtime.ToString() + " [s]");
                                    }
                                    else if (data.Contains("4133"))
                                    { // Absolute Barometric Pressure
                                        int abp = tmp[4];
                                        currentInfo.ABP = abp;
                                        writeDisplay("ABP: " + abp.ToString() + " [kPa]");
                                    }
                                    else if (data.Contains("411C")) // PID di appoggio per la scrittura su CSV a fine ciclo
                                    {
                                        if (mode == 0) // scrittura su CSV
                                        {
                                            var config = new CsvConfiguration(CultureInfo.InvariantCulture);
                                            config.HasHeaderRecord = !append;

                                            using (var writer = new StreamWriter("../../../dati/dataset.csv", append))
                                            {
                                                using (var csv = new CsvWriter(writer, config))
                                                {
                                                    csv.WriteRecord(currentInfo);
                                                    csv.NextRecord();
                                                }
                                            }

                                            writeDisplay("Info Added To CSV");
                                        }
                                        else if (mode == 1) // utilizzo dell'API di ML
                                        {
                                            try
                                            {
                                                // aggiornamento dei grafici
                                                Logger_rpm.Add(currentInfo.RUNTIME, currentInfo.RPM);
                                                Logger_speed.Add(currentInfo.RUNTIME, currentInfo.SPEED);
                                                Logger_load.Add(currentInfo.RUNTIME, currentInfo.ENGINELOAD);

                                                refreshCharts(); // metodo con delegate

                                                // Serialize our concrete class into a JSON String
                                                var stringPayload = JsonConvert.SerializeObject(currentInfo);
                                                //writeDisplay(stringPayload);
                                                // dentro al body ci devo mettere il json con i parametri per la predizione
                                                var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                                                HttpResponseMessage response = await httpClient.PostAsync(URL, httpContent);
                                                if (response.IsSuccessStatusCode)
                                                {
                                                    messageFromAPI = await response.Content.ReadAsStringAsync();
                                                    responseAsJson = JsonNode.Parse(messageFromAPI)!;
                                                    // parsing del messaggio come un json e andare a prendere il campo 'prediction'
                                                    // sse lo stato della risposta: 'state' vale OK
                                                    responseState = responseAsJson["state"].ToString();
                                                    if (responseState == "OK")
                                                    {
                                                        responseContent = responseAsJson["prediction"].ToString();
                                                        writeDisplay(responseContent);
                                                        if (responseContent == "SPORT") {
                                                            displayDriverStyle.Text = "SPORT";
                                                            displayDriverStyle.ForeColor = Color.LightCoral;
                                                        } else if (responseContent == "ECO") {
                                                            displayDriverStyle.Text = "ECO";
                                                            displayDriverStyle.ForeColor = Color.LightGreen;
                                                        }
                                                    }
                                                    else if (responseState == "BUFFERING" || responseState == "ERROR")
                                                    {
                                                        responseContent = responseAsJson["message"].ToString();
                                                        writeDisplay(responseContent);
                                                        displayDriverStyle.Text = "BUFFERING";
                                                        displayDriverStyle.ForeColor = Color.LightSteelBlue;
                                                    }
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                writeDisplay(e.Message);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        writeDisplay(data);
                                    }
                                }
                                catch (FormatException fe)
                                {
                                    writeDisplay("Reading error!");
                                }
                            }
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

        private void refreshCharts()
        {
            if (chart_speed.InvokeRequired)
            {
                updateChartDelegate d = new updateChartDelegate(refreshCharts);
                this.Invoke(d, new object[] { });
            }
            else
            {
                chart_speed.Refresh();
                chart_rpm.Refresh();
                chart_load.Refresh();
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

        private void printPIDs(string hex)
        {
            string to_print = "";
            char[] to_print_array = new char[] { };
            char[] to_print_array_tmp = new char[] { };

            Debug.WriteLine(hex);
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
                foreach (char bit in to_print_array_tmp)
                {
                    to_print_array = to_print_array.Append(bit).ToArray();
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
            else
            { // altrimenti continuo la modifica del file
                using (StreamReader r = new StreamReader("../../../dati/datafile_mod.json"))
                {
                    json = r.ReadToEnd();
                    pids = JsonNode.Parse(json)!;
                }
            }

            for (int i = responde_index; i < responde_index + to_print_array.Length; i++)
            { // fixed number: 169
                value = int.Parse(to_print_array[j].ToString());
                if (value == 1)
                {
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

        private async void button1_Click(object sender, EventArgs e)
        {
            const string URL = "http://localhost:8000/";
            string messageFromAPI;
            string urlParameters = "";

            HttpResponseMessage response = await httpClient.GetAsync(URL);
            if (response.IsSuccessStatusCode)
            {
                messageFromAPI = await response.Content.ReadAsStringAsync();
                writeDisplay(messageFromAPI);
            }
        }

        private async void buttonFuelData_Click(object sender, EventArgs e)
        {
            mode = 0; // per permettere la scrittura su CSV
            askEngineData();
        }

        private async void askEngineData()
        {
            while (runEngineMonitoring)
            {
                await Task.Run(() =>
                {
                    send("010F" + "\r"); // Intake air temperature (IAT)
                    Task.Delay(0200).Wait();
                    send("010C" + "\r"); // Engine speed
                    Task.Delay(0200).Wait();
                    send("0110" + "\r"); // MAF
                    Task.Delay(0200).Wait();
                    send("0149" + "\r"); // Acc. Pedal Pos. D
                    Task.Delay(0200).Wait();
                    send("0111" + "\r"); // Throttle Position
                    Task.Delay(0200).Wait();
                    send("010D" + "\r"); // Vehicle Speed
                    Task.Delay(0200).Wait();
                    send("0104" + "\r"); // Engine Load
                    Task.Delay(0200).Wait();
                    send("011F" + "\r"); // Run Time
                    Task.Delay(0200).Wait();
                    send("0133" + "\r"); // Absolute Barometric Pressure
                    Task.Delay(0200).Wait();
                    send("011C" + "\r"); // Update CSV or Predict Drivestyle
                    Task.Delay(0200).Wait();
                    writeDisplay("");
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

            if (n_try == 6)
            {
                currentInfo.DRIVESTYLE = "eco";
                currentInfo.ROADTYPE = "urban";
                startListening();
            } // apertura thread di ascolto della stream
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

                //Task.Delay(2000).Wait();
                //send("01A0" + "\r"); // ultimo

                Task.Delay(2000).Wait();
                converting = false;
            });
        }

        private void ecoButton_Click(object sender, EventArgs e) { currentInfo.DRIVESTYLE = "eco"; }

        private void normalButton_Click(object sender, EventArgs e) { currentInfo.DRIVESTYLE = "normal"; }

        private void sportButton_Click(object sender, EventArgs e) { currentInfo.DRIVESTYLE = "sport"; }

        private void urbanButton_Click(object sender, EventArgs e) { currentInfo.ROADTYPE = "urban"; }

        private void extraButton_Click(object sender, EventArgs e) { currentInfo.ROADTYPE = "suburban"; }

        private void highwayButton_Click(object sender, EventArgs e) { currentInfo.ROADTYPE = "highway"; }

        private void initializeCharts()
        {
            Logger_speed.ViewSlide();
            Logger_rpm.ViewSlide();
            Logger_load.ViewSlide();

            // 75 secondi come finestra, mostrati fino a 200 khm
            chart_speed.Plot.SetAxisLimits(-2, 500, -20, 200);
            chart_speed.Plot.Style(dataBackground: Color.FromArgb(7, 102, 173));
            chart_speed.Plot.YAxis.Label("Speed [Km/h]", Color.White, size: 12, fontName: "Courier New");
            chart_speed.Plot.Grid(lineStyle: LineStyle.Dot, color: Color.FromArgb(155, 176, 176));
            chart_speed.Plot.XAxis2.Line(false);
            chart_speed.Plot.YAxis2.Line(false);
            chart_speed.Plot.XAxis.Color(Color.White);
            chart_speed.Plot.YAxis.Color(Color.White);

            // 75 secondi come finestra, mostrati fino a 3000 rpm
            chart_rpm.Plot.SetAxisLimits(-2, 500, 900, 3000);
            chart_rpm.Plot.Style(dataBackground: Color.FromArgb(7, 102, 173));
            chart_rpm.Plot.YAxis.Label("Engine Speed [RPM]", Color.White, size: 12, fontName: "Courier New");
            chart_rpm.Plot.Grid(lineStyle: LineStyle.Dot, color: Color.FromArgb(155, 176, 176));
            chart_rpm.Plot.XAxis2.Line(false);
            chart_rpm.Plot.YAxis2.Line(false);
            chart_rpm.Plot.XAxis.Color(Color.White);
            chart_rpm.Plot.YAxis.Color(Color.White);

            // 75 secondi come finestra, mostrati fino a 100 %
            chart_load.Plot.SetAxisLimits(-2, 500, -20, 120);
            chart_load.Plot.Style(dataBackground: Color.FromArgb(7, 102, 173));
            chart_load.Plot.YAxis.Label("Engine Load [%]", Color.White, size: 12, fontName: "Courier New");
            chart_load.Plot.Grid(lineStyle: LineStyle.Dot, color: Color.FromArgb(155, 176, 176));
            chart_load.Plot.XAxis2.Line(false);
            chart_load.Plot.YAxis2.Line(false);
            chart_load.Plot.XAxis.Color(Color.White);
            chart_load.Plot.YAxis.Color(Color.White);

            chart_speed.Refresh();
            chart_rpm.Refresh();
            chart_load.Refresh();
        }

        private void buttonStopListening_Click(object sender, EventArgs e)
        {
            stopListening = true;
            runEngineMonitoring = false;
            client.Close();
        }

        private void evalDriveButton_Click(object sender, EventArgs e)
        {
            mode = 1;
            askEngineData();
            writeDisplay("Mode Changed");
        }

        private void videoSyncButton_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            videoSyncTimer.Tick += new EventHandler(TimerEventProcessor);
            syncPanel.BackColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            videoSyncTimer.Enabled = true;
        }

        private void TimerEventProcessor(object myObject, EventArgs e)
        {
            syncPanel.BackColor = Color.FromArgb(7, 102, 173);
            videoSyncTimer.Enabled = false;
        }
    }
}