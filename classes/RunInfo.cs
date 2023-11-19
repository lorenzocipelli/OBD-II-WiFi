﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBD_II_WiFi.classes
{
    internal class RunInfo
    {
        private int rpm;
        private int maf;
        private int iat;
        private int accPedal;
        private int throttlePos;
        private int speed;
        private int engineLoad;
        private int runTime;
        private double lambda;
        private int abp;

        public int RMP { get { return rpm; } set { rpm = value; } }
        public int MAF { get { return maf; } set { maf = value; } }
        public int IAT { get { return iat; } set { iat = value; } }
        public int ACCPEDAL { get { return accPedal; } set { accPedal = value; } }
        public int THROTTLEPOS { get { return throttlePos; } set { throttlePos = value; } }
        public int SPEED { get { return speed; } set { speed = value; } }
        public int ENGINELOAD { get { return engineLoad; } set { engineLoad = value; } }
        public int RUNTIME { get { return runTime; } set { runTime = value; } }
        public double LAMBDA { get { return lambda; } set { lambda = value; } }
        public int ABP { get { return abp; } set { abp = value; } }
    }
}