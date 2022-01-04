using System;
using System.Text.RegularExpressions;

namespace ConsoleApp2
{
    /*
        STANDART TOKENS
        
        use_lib,
        variable_name,
        is_equal,
        plus,
        minus,
        multiply,
        divide,
        increment,
        decrement,
        square,
        assign,
        open_bracket,
        close_bracket,
        number,
        class_definition,
        text,
        type,
        library,
        endLine,
        function_call,
        undefinded_token
    */
    public class Token
    {
        public Regex regex;
        public string ttype;
        public dynamic value;
        public Token(string regex_rule, string token_type, RegexOptions options = RegexOptions.None)
        {
            regex = new Regex(regex_rule,options);
            ttype = token_type;
        }

        public string GetValue()
        {
            return value;
        }

        //public static Token operator =
    }
}
