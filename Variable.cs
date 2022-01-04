using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public class Variable
    {
        public string name;
        public dynamic value;
        public string type;
        public Variable(string _name, dynamic _value, string _type)
        {
            name = _name;
            value = _value;
            type = _type;
        }
    }
}
