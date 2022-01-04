using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    class Preferences
    {
        private static Preferences instance;

        public static Preferences getInstance()
        {
            if (instance == null)
                instance = new Preferences();
            return instance;
        }


        public bool log = false; //LOGGING
    }
}
