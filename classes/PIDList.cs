using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OBD_II_WiFi.classes;

namespace OBD_II_WiFi.classes
{
    internal class PIDList
    {
        private string key;
        private PIDescriptor[] pid_list;

        public string Key { get { return key; } set { key = value; } }
        public PIDescriptor[] Pidlist { get { return pid_list; } set { pid_list = value; } }
    }
}
