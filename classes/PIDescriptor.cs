using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBD_II_WiFi.classes
{
    internal class PIDescrptior
    {
        private string description;
        private int available;

        public string Description { get { return description; } set { description = value; } }
        public int Available { get { return available; } set { available = value; } }
        public override string ToString()
        {
            return "Description: " + description + " - available: " + available;
        }
    }
}
