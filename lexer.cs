using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp2
{
    public class Lexer
    {
        private static Lexer instance;

        public List<Token> tokens = new List<Token>();

        string str;
        public string[] tokens_in_strings;
        public List<Token> result_tokens = new List<Token>();

        public static Lexer getInstance()
        {
            if (instance == null)
                instance = new Lexer();
            return instance;
        }

        public Lexer()
        {
            tokens.Add(new Token(@"^#invite$", "use_lib"));
            tokens.Add(new Token(@"^false$", "false"));
            tokens.Add(new Token(@"^true$", "true"));
            tokens.Add(new Token(@"^[a-z]+[0-9]?$", "variable_name", RegexOptions.IgnoreCase));
            tokens.Add(new Token(@"^[=]{2}$", "is_equal"));
            tokens.Add(new Token(@"^[+]$", "plus"));
            tokens.Add(new Token(@"^[-]$", "minus"));
            tokens.Add(new Token(@"^[*]$", "multiply"));
            tokens.Add(new Token(@"^[/]$", "divide"));
            tokens.Add(new Token(@"^[+]{2}$", "increment"));
            tokens.Add(new Token(@"^[-]{2}$", "decrement"));
            tokens.Add(new Token(@"^[*]{2}$", "square"));
            tokens.Add(new Token(@"^[=]$", "assign"));
            tokens.Add(new Token(@"^[(]$", "open_bracket"));
            tokens.Add(new Token(@"^[)]$", "close_bracket"));
            tokens.Add(new Token(@"^[{]$", "open_f_bracket"));
            tokens.Add(new Token(@"^[}]$", "close_f_bracket"));
            tokens.Add(new Token(@"^[0-9]+$", "number"));
            tokens.Add(new Token(@"^[0-9]+$", "class_definition"));
            tokens.Add(new Token(@"^"".+""$", "text"));
            tokens.Add(new Token(@"^/.+/$", "library"));
            tokens.Add(new Token(@"^[;]$", "endLine"));
            tokens.Add(new Token(@"^[a-z]+[0-9]?[(]{1}[)]{1}$", "function_call", RegexOptions.IgnoreCase));
        }

        public void SetStr(string _s)
        {
            str = _s;

            tokens_in_strings = SuperSplit(str);
            

            foreach (string s in tokens_in_strings)
            {
                bool matched = false;
                string token_value = s;

                //if (token_value[0] == '\t') token_value = token_value.Remove(0, 1);
                token_value = s.Replace("\t", String.Empty);

                for (int ii=0;ii<tokens.Count;ii++)
                {
                    Token t = tokens[ii];
                    Match match = t.regex.Match(token_value);
                    if (match.Success)
                    {
                        Token newToken = new Token(t.regex.ToString(),t.ttype);

                        if (t.ttype == "text") token_value = token_value.Trim('"');
                        if (t.ttype == "library") token_value = token_value.Trim('/');

                        newToken.value = token_value;
                        result_tokens.Add(newToken);
                        matched = true;

                        if (t.ttype == "library")
                        {
                            Parser.getInstance().tokens = result_tokens;
                            Parser.getInstance().Run();
                        }
                        break;
                    }
                }

                if (!matched)
                {
                    Token newToken = new Token("", "undefinded_token");
                    newToken.value = s;

                    //Console.WriteLine("UNDEFINDED TOKEN: " + s);

                    result_tokens.Add(newToken);
                }
            }
        }

        public void PrintTokens()
        {
            for (int i = 0; i < result_tokens.Count; i++)
            {
                Console.WriteLine(result_tokens[i].value + " - " + result_tokens[i].ttype);
            }
        }

        public bool isThisTypeName(string s)
        {
            for(int i = 0; i < Globals.types.Count; i++)
            {
                //Console.WriteLine("Checking " + i + " " + Globals.types[i]);
                if(Globals.types[i] == s) { return true; }
            }
            return false;
        }

        public static string[] SuperSplit(string s)
        {
            var str = s.Split('"');

            List<string> lst = new List<string>();

            for (int i = 0; i < str.Length; i++)
            {
                if (i % 2 == 0)
                {
                    lst.AddRange(str[i].Split(' ',' '));
                    lst.Remove("");
                }
                else
                {
                    string str2 = '"' + str[i] + '"';
                    lst.Add(str2);
                }
            }

            return lst.ToArray();
        }
    }
}
