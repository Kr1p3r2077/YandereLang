using System;
using System.Collections.Generic;
using System.Text;
using ConsoleApp2;

namespace yandere
{
    public static class io_library
    {
        public static Token[] TOKENS = {
            new Token("^print:$", "Print"),
            new Token("^printL:$", "PrintL"),
            new Token("^printS:$", "PrintS"),
            new Token("^read:$", "Read"),
            new Token("^[:]{1}n$", "newLine")
        };

        public static int Read(Token[] tokens, ref int i)
        {
            if (tokens[i].ttype == "Read" && tokens[i + 1].ttype == "variable_name")
            {
                while (tokens[i + 1].ttype != "endLine")
                {

                    string data = Console.ReadLine();

                    if (Globals.GetVariableType(tokens[i + 1].value) == "int")
                    {
                        int a;
                        if (int.TryParse(data, out a))
                        {
                            Globals.SetVariableValue((tokens[i + 1].value).ToString(), a);
                        }
                    }
                    else if (Globals.GetVariableType(tokens[i + 1].value) == "text")
                    {
                        Globals.SetVariableValue((tokens[i + 1].value).ToString(), data);

                    }
                    i++;
                }
                return 0;
            }
            return -1;
        }

        public static int Print(Token[] tokens, ref int i)
        {
            if (tokens[i].ttype == "Print")
            {
                while (tokens[i + 1].ttype != "endLine")
                {

                    if (tokens[i + 1].ttype == "text" || tokens[i + 1].ttype == "undefinded_token")
                    {
                        Console.Write((string)tokens[i + 1].value);
                        i++;
                    }
                    if (tokens[i + 1].ttype == "variable_name")
                    {
                        if (Globals.GetVariableType(tokens[i + 1].value) == "bool")
                        {
                            if (Globals.GetVariableValue(tokens[i + 1].value) == 0)
                            {
                                Console.Write("true");
                            }
                            else
                            {
                                Console.Write("false");
                            }
                        }
                        else
                        {
                            Console.Write(Globals.GetVariableValue(tokens[i + 1].value));
                        }
                        i++;
                    }
                    if (tokens[i + 1].ttype == "number")
                    {
                        Console.Write(int.Parse(tokens[i + 1].value));
                        i++;
                    }
                    if (tokens[i + 1].ttype == "newLine")
                    {
                        Console.Write('\n');
                        i++;
                    }

                }
                return 0;
                i++;
            }

            return -1;
        }

        public static int PrintL(Token[] tokens, ref int i)
        {
            if (tokens[i].ttype == "PrintL")
            {
                while (tokens[i + 1].ttype != "endLine")
                {

                    if (tokens[i + 1].ttype == "text" || tokens[i + 1].ttype == "undefinded_token")
                    {
                        Console.WriteLine((string)tokens[i + 1].value);
                        i++;
                    }
                    if (tokens[i + 1].ttype == "variable_name")
                    {
                        if (Globals.GetVariableType(tokens[i + 1].value) == "bool")
                        {
                            if (Globals.GetVariableValue(tokens[i + 1].value) == 0)
                            {
                                Console.WriteLine("true");
                            }
                            else
                            {
                                Console.WriteLine("false");
                            }
                        }
                        else
                        {
                            Console.WriteLine(Globals.GetVariableValue(tokens[i + 1].value));
                        }
                        i++;
                    }
                    if (tokens[i + 1].ttype == "number")
                    {
                        Console.WriteLine(int.Parse(tokens[i + 1].value));
                        i++;
                    }
                    if (tokens[i + 1].ttype == "newLine")
                    {
                        Console.WriteLine('\n');
                        i++;
                    }

                }
                return 0;
                i++;
            }

            return -1;
        }

        public static int PrintS(Token[] tokens, ref int i)
        {
            if (tokens[i].ttype == "PrintS")
            {
                while (tokens[i + 1].ttype != "endLine")
                {

                    if (tokens[i + 1].ttype == "text" || tokens[i + 1].ttype == "undefinded_token")
                    {
                        Console.Write((string)tokens[i + 1].value);
                        i++;
                    }
                    if (tokens[i + 1].ttype == "variable_name")
                    {
                        if (Globals.GetVariableType(tokens[i + 1].value) == "bool")
                        {
                            if (Globals.GetVariableValue(tokens[i + 1].value) == 0)
                            {
                                Console.Write("true");
                            }
                            else
                            {
                                Console.Write("false");
                            }
                        }
                        else
                        {
                            Console.Write(Globals.GetVariableValue(tokens[i + 1].value));
                        }
                        i++;
                    }
                    if (tokens[i + 1].ttype == "number")
                    {
                        Console.Write(int.Parse(tokens[i + 1].value));
                        i++;
                    }
                    if (tokens[i + 1].ttype == "newLine")
                    {
                        Console.Write('\n');
                        i++;
                    }
                    Console.Write(" ");
                }
                return 0;
                i++;
            }

            return -1;
        }

    }
}
