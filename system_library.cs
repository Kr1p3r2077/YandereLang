using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using ConsoleApp2;

namespace yandere {

    public static class system_library
    {
        public static Token[] TOKENS = {
            new Token("^int$","integer_def"),
            new Token("^text$", "text_def"),
            new Token("^bool$", "bool_def"),
            new Token("^any$", "any_def"),
            new Token("^bool$", "bool_def"),
            new Token("^loop[(]{1}.+[)]{1}$", "loop"),
        };

        public static int integer_definition(Token[] tokens, ref int i)
        {

            if (tokens[i].ttype == "integer_def" && tokens[i + 1].ttype == "variable_name")
            {
                if (tokens[i + 2].ttype == "endLine")
                {
                    Globals.AddVariable(tokens[i + 1].value, "int");


                    string l = "Created variable: " + tokens[i].value + " " + tokens[i + 1].value + " = 0\n";
                    File.AppendAllText("log.txt",l);

                    i += 2;
                    return 0;
                }
                if (tokens[i + 2].ttype == "assign")
                {
                    Globals.AddVariable(tokens[i + 1].value, "int", tokens[i + 3].value.ToString());


                    string l = "Created variable: " + tokens[i].value + " " + tokens[i + 1].value + " = " + tokens[i+3].value + '\n';
                    File.AppendAllText("log.txt", l);

                    i += 4;
                    return 0;
                }
            }

            return -1;
        }

        public static int text_definition(Token[] tokens, ref int i)
        {

            if (tokens[i].ttype == "text_def" && tokens[i + 1].ttype == "variable_name")
            {
                if (tokens[i + 2].ttype == "endLine")
                {
                    Globals.AddVariable(tokens[i + 1].value,"text");
                    i += 2;
                    return 0;
                }
                if (tokens[i + 2].ttype == "assign")
                {
                    Globals.AddVariable(tokens[i + 1].value, "text", (tokens[i + 3].value));
                    i += 4;
                    return 0;
                }
            }

            return -1;
        }

        public static int bool_definition(Token[] tokens, ref int i)
        {

            if (tokens[i].ttype == "bool_def" && tokens[i + 1].ttype == "variable_name")
            {
                if (tokens[i + 2].ttype == "endLine")
                {
                    Globals.AddVariable(tokens[i + 1].value, "bool", 0);
                    i += 2;
                    return 0;
                }
                if (tokens[i + 2].ttype == "assign")
                {
                    if (tokens[i + 3].ttype == "true")
                    {
                        Globals.AddVariable(tokens[i + 1].value, "bool", 1);
                    }
                    else if (tokens[i + 3].ttype == "false")
                    {
                        Globals.AddVariable(tokens[i + 1].value, "bool", 0);
                    }
                    i += 4;
                    return 0;
                }
            }

            return -1;
        }

        public static int any_definition(Token[] tokens, ref int i)
        {

            if (tokens[i].ttype == "any_def" && tokens[i + 1].ttype == "variable_name")
            {
                if (tokens[i + 2].ttype == "endLine")
                {
                    Globals.AddVariable(tokens[i + 1].value,"any");
                    i += 2;
                    return 0;
                }
                if (tokens[i + 2].ttype == "assign")
                {
                    Globals.AddVariable(tokens[i + 1].value,"any", (tokens[i + 3].value));
                    i += 4;
                    return 0;
                }
            }

            return -1;
        }

        public static int IO_library_adding(Token[] tokens, ref int i)
        {
            try
            {
                if (Globals.IsLibraryInvited(tokens[i + 1].value)) return -1;
            }
            catch { return - 1; }

            if (tokens[i].ttype == "use_lib" && tokens[i + 1].ttype == "library" && tokens[i + 1].value == "IO")
            {
                Globals.AddTokensFromLibrary(yandere.io_library.TOKENS);

                /*
                Parser.getInstance().AddAction(yandere.io_library.Print);
                Parser.getInstance().AddAction(yandere.io_library.Read);
                */

                Program.AddAllMethodsFrom(typeof(yandere.io_library));

                if (Preferences.getInstance().log)
                {
                    Console.WriteLine("INVITED /IO/ LIB");
                }

                Globals.AddLibraryToInvited(tokens[i + 1].value);

                if (Preferences.getInstance().log)
                {
                    Console.WriteLine("IO INVITED");
                }

                i += 3;

                return 0;
            }

            return -1;
        }

        public static int ChangeVariableValue(Token[] tokens, ref int i)
        {
            if (tokens[i].ttype == "variable_name" && tokens[i + 1].ttype == "assign")
            {
                if (tokens[i + 2].ttype == "variable_name")
                {
                    Globals.SetVariableValue(tokens[i].value, Globals.GetVariableValue(tokens[i + 2].value));

                    i += 3;
                    return 0;
                }
                else
                {
                    Globals.SetVariableValue(tokens[i].value, tokens[i + 2].value);

                    i += 3;
                    return 0;
                }
            }
            return -1;
        }

        public static int increment(Token[] tokens, ref int i)
        {
            if (tokens[i].ttype == "variable_name" && tokens[i + 1].ttype == "increment")
            {
                Globals.SetVariableValue(tokens[i].value, int.Parse(Globals.GetVariableValue(tokens[i].value).ToString()) + 1);

                i += 2;
                return 0;
            }

            return -1;
        }

        public static int decrement(Token[] tokens, ref int i)
        {
            if (tokens[i].ttype == "variable_name" && tokens[i + 1].ttype == "decrement")
            {
                Globals.SetVariableValue(tokens[i].value, int.Parse(Globals.GetVariableValue(tokens[i].value).ToString()) - 1);

                i += 2;
                return 0;
            }

            return -1;
        }

        public static int square(Token[] tokens, ref int i)
        {
            if (tokens[i].ttype == "variable_name" && tokens[i + 1].ttype == "square")
            {
                int v = int.Parse(Globals.GetVariableValue(tokens[i].value).ToString());
                Globals.SetVariableValue(tokens[i].value, v * v);

                i += 2;
                return 0;
            }

            return -1;
        }

        public static int loop(Token[] tokens, ref int i)
        {
            int brackets_count = 0;
            int ind = i;
            if (tokens[ind].ttype == "loop" && tokens[ind + 1].ttype == "open_f_bracket")
            {
                int index_count = 0;
                string str = tokens[ind].value.ToString();
                str = str.Remove(0, 5);
                str = str.Remove(str.Length - 1,1);

                int repeat_count = 0;

                if (!(int.TryParse(str, out repeat_count)))
                {
                    repeat_count = int.Parse(Globals.GetVariableValue(str).ToString());
                }

                while (tokens[ind + 1].ttype != "close_f_bracket" || brackets_count > 0)
                {
                    if(tokens[ind + 1].ttype == "open_f_bracket")
                    {
                        brackets_count++;
                    }
                    if (tokens[ind + 1].ttype == "close_f_bracket")
                    {
                        if (brackets_count > 1)
                        {
                            brackets_count--;
                        }
                        else
                        {
                            break;
                        }
                    }

                    ind++;
                    index_count++;
                }
                //Console.WriteLine(index_count);

                Parser.getInstance().execute(tokens, i + 3, i + index_count - 2, repeat_count);
                i++;
                return 0;
            }
            return -1;
        }
    }
}
