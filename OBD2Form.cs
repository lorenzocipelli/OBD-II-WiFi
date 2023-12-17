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
        string urlParameters = "";

        TcpClient client = new TcpClient();
        RunInfo currentInfo = new RunInfo();
        NetworkStream stream;
        StreamReader reader;
        int mode;
        string data;

        delegate void writeDisplayDelegate(string toDisplay);

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
                                try {
                                    var tmp = Convert.FromHexString(data);

                                    if (data.Contains("410C"))
                                    { // Engine speed
                                        int rpm = (tmp[4] * 256 + tmp[5]) / 4; // il valore arriva in rpm * 4
                                        currentInfo.RMP = rpm;
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
                                                // UNCOMMENT Logger_rpm.Add(currentInfo.RUNTIME, currentInfo.RMP);
                                                // UNCOMMENT Logger_speed.Add(currentInfo.RUNTIME, currentInfo.SPEED);
                                                // UNCOMMENT Logger_load.Add(currentInfo.RUNTIME, currentInfo.ENGINELOAD);

                                                // UNCOMMENT chart_speed.Refresh();
                                                // UNCOMMENT chart_rpm.Refresh();
                                                // UNCOMMENT chart_load.Refresh();

                                                // Serialize our concrete class into a JSON String
                                                var stringPayload = JsonConvert.SerializeObject(currentInfo);
                                                // dentro al body ci devo mettere il json con i parametri per la predizione
                                                var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                                                HttpResponseMessage response = await httpClient.PostAsync(URL, httpContent);
                                                if (response.IsSuccessStatusCode)
                                                {
                                                    messageFromAPI = await response.Content.ReadAsStringAsync();
                                                    responseAsJson = JsonNode.Parse(messageFromAPI)!;
                                                    // parsing del messaggio come un json e andare a prendere il campo 'prediction'
                                                    // sse lo stato della risposta: 'state' vale OK
                                                    if (responseAsJson["state"].ToString() == "OK")
                                                    {
                                                        responseContent = responseAsJson["prediction"].ToString();
                                                        writeDisplay(responseContent);
                                                    }
                                                    else if (responseAsJson["state"].ToString() == "ERROR")
                                                    {
                                                        writeDisplay("Can't Read Prediction!");
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
                                catch(FormatException fe)
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

        private void printPIDs(string hex) {
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
                foreach (char bit in to_print_array_tmp) {
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
                    send("011C" + "\r"); // Update CSV
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

            if (n_try == 6) {
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

                Task.Delay(2000).Wait();
                //send("01A0" + "\r"); // ultimo

                Task.Delay(2000).Wait();
                converting = false;
            });
        }

        private void buttonStopListening_Click_1(object sender, EventArgs e)
        {
            stopListening = true;
            runEngineMonitoring = false;
            client.Close();
        }

        private void ecoButton_Click(object sender, EventArgs e) { currentInfo.DRIVESTYLE = "eco"; }

        private void normalButton_Click(object sender, EventArgs e) { currentInfo.DRIVESTYLE = "normal"; }

        private void sportButton_Click(object sender, EventArgs e) { currentInfo.DRIVESTYLE = "sport"; }

        private void urbanButton_Click(object sender, EventArgs e) { currentInfo.ROADTYPE = "urban"; }

        private void extraButton_Click(object sender, EventArgs e) { currentInfo.ROADTYPE = "suburban"; }

        private void highwayButton_Click(object sender, EventArgs e) { currentInfo.ROADTYPE = "highway"; }

        private void initializeCharts() {
            Logger_speed.ViewSlide();
            Logger_rpm.ViewSlide();
            Logger_load.ViewSlide();

            // 75 secondi come finestra, mostrati fino a 200 khm
            chart_speed.Plot.SetAxisLimits(-2, 75, -20, 200);
            chart_speed.Plot.Style(dataBackground: Color.FromArgb(7, 102, 173));
            chart_speed.Plot.YAxis.Label("Speed [Km/h]", Color.White, size: 12, fontName: "Courier New");
            chart_speed.Plot.Grid(lineStyle: LineStyle.Dot, color: Color.FromArgb(155, 176, 176));
            chart_speed.Plot.XAxis2.Line(false);
            chart_speed.Plot.YAxis2.Line(false);
            chart_speed.Plot.XAxis.Color(Color.White);
            chart_speed.Plot.YAxis.Color(Color.White);

            // 75 secondi come finestra, mostrati fino a 3000 rpm
            chart_rpm.Plot.SetAxisLimits(-2, 75, 900, 3000);
            chart_rpm.Plot.Style(dataBackground: Color.FromArgb(7, 102, 173));
            chart_rpm.Plot.YAxis.Label("Engine Speed [RPM]", Color.White, size: 12, fontName: "Courier New");
            chart_rpm.Plot.Grid(lineStyle: LineStyle.Dot, color: Color.FromArgb(155, 176, 176));
            chart_rpm.Plot.XAxis2.Line(false);
            chart_rpm.Plot.YAxis2.Line(false);
            chart_rpm.Plot.XAxis.Color(Color.White);
            chart_rpm.Plot.YAxis.Color(Color.White);
            
            // 75 secondi come finestra, mostrati fino a 100 %
            chart_load.Plot.SetAxisLimits(-2, 75, -20, 120);
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

        int to_remove = 0;
        int to_remove1 = 0;
        int[] speed_fake = new int[] { 10, 14, 24, 40, 17, 22, 59, 22, 0, 22, 25, 27, 32, 36, 40, 60, 50, 51, 33 };
        int[] rpm_fake = new int[] { 950, 1000, 1120, 1156, 1350, 1350, 1260, 1388, 2000, 2120, 2350, 27, 32, 36, 40, 60, 50, 51, 33 };
        int[] runtime_fake = new int[] { 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110, 115, 120, 125, 130, 135, 140 };

        private void button1_Click_1(object sender, EventArgs e)
        {
            /*Logger_speed.Add(runtime_fake[to_remove1], speed_fake[to_remove]);
            Logger_rpm.Add(runtime_fake[to_remove1], rpm_fake[to_remove]);
            Logger_load.Add(runtime_fake[to_remove1], speed_fake[to_remove]);

            to_remove++;
            to_remove1++;

            if (to_remove == (speed_fake.Length-1)) { to_remove = 0; } */
        }

        private void updatePlotTimer_Tick(object sender, EventArgs e)
        {
            if (Logger_speed.Count == Logger_speed.CountOnLastRender)
                return;

            chart_speed.Refresh();
            chart_rpm.Refresh();
            chart_load.Refresh();
        }

        private void evalDriveButton_Click_1(object sender, EventArgs e)
        {
            mode = 1;
            updatePlotTimer.Enabled = true;
        }

        private async void button1_Click_2(object sender, EventArgs e)
        {
            JsonNode responseAsJson;
            string responseContent;

            await Task.Run(async () => {
                try
                {
                    // Serialize our concrete class into a JSON String
                    // UNCOMMENT var stringPayload = JsonConvert.SerializeObject(currentInfo);
                    // dentro al body ci devo mettere il json con i parametri per la predizione
                    // UNCOMMENT var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                    var httpContent = new StringContent("{ \"rpm\": 2022, \"maf\": 968, \"iat\": 9, \"speed\": 106, \"engineload\": 72 }", Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.PostAsync(URL, httpContent);
                    if (response.IsSuccessStatusCode)
                    {
                        messageFromAPI = await response.Content.ReadAsStringAsync();
                        responseAsJson = JsonNode.Parse(messageFromAPI)!;
                        // parsing del messaggio come un json e andare a prendere il campo 'prediction'
                        // sse lo stato della risposta: 'state' vale OK
                        if (responseAsJson["state"].ToString() == "OK") {
                            responseContent = responseAsJson["prediction"].ToString();
                            writeDisplay(responseContent);
                        } else if (responseAsJson["state"].ToString() == "ERROR") {
                            writeDisplay("Can't Read Prediction!");
                        }
                    }
                }
                catch (Exception e)
                {
                    writeDisplay(e.Message);
                }
            });
        }
    }
}

/*
    rpm,maf,iat,accpedal,throttlepos,speed,engineload,runtime,abp
    828,182,36,12,202,0,38,53,100
    972,296,35,36,202,14,35,58,100
    1076,264,35,61,202,24,76,63,100
    947,187,35,0,202,21,40,68,100
    1088,163,35,35,202,17,43,74,100
    1326,252,35,76,202,22,43,79,100
    893,161,35,0,202,0,59,84,100
    1826,235,34,71,202,22,32,89,100
    903,242,34,0,202,25,88,95,100
    1017,205,34,7,202,27,100,100,100
    1178,282,34,7,202,32,36,105,100
    997,130,34,0,202,25,68,111,100
    943,207,34,107,203,25,98,116,100
    1286,676,33,148,202,47,98,121,100
    1886,460,31,0,202,52,77,126,100
    2111,1205,30,0,202,60,98,132,100
    1602,800,29,125,202,68,100,137,100
    1930,357,26,0,203,73,0,142,100
    923,170,28,0,202,58,0,147,100
    2048,391,27,0,202,46,0,153,100
    1517,1075,27,159,202,54,83,158,100
     */