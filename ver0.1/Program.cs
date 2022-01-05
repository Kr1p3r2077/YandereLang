using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ConsoleApp2
{
    class Field
    {
        public dynamic value;
        public string name;
        public string type;
        public Field(string _name, string _type)
        {
            name = _name;
            type = _type;
        }
    }

    class CustomClass
    {
        public List<Field> fields;
        public string name;
        public CustomClass()
        {
            fields = new List<Field>();
        }

        public void AddField(Field f)
        {
            fields.Add(f);
        }

        public void SetFieldValue(string name, dynamic value)
        {
            for(int i = 0; i < fields.Count; i++)
            {
                if (fields[i].name == name)
                {
                    fields[i].value = value;
                }
            }
        }

        public dynamic GetFieldValue(string name)
        {
            foreach(Field f in fields)
            {
                if(f.name == name)
                {
                    return f.value;
                    break;
                }
            }

            return null;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] programm = File.ReadAllLines("test.ydr");

            Lexer lexer = Lexer.getInstance();

            //

            Globals.AddTokensFromLibrary(yandere.system_library.TOKENS);

            Parser parser = Parser.getInstance();

            AddAllMethodsFrom(typeof(yandere.system_library));

            //

            for (int i = 0; i < programm.Length; i++)
            {
                lexer.SetStr(programm[i]);
                lexer.result_tokens.Add(new Token("", "endLine"));
                lexer.result_tokens[lexer.result_tokens.Count - 1].value = ";";

                if (Preferences.getInstance().log)
                {
                    Console.WriteLine(programm[i]);
                }
            }
            if (Preferences.getInstance().log)
            {
                Console.WriteLine('\n');
            }

            if (Preferences.getInstance().log)
            {
                lexer.PrintTokens();
            }

            /*
            CustomClass c = new CustomClass();
            c.AddField(new Field("age", "int"));
            c.AddField(new Field("name", "string"));

            c.SetFieldValue("age", 56);
            c.SetFieldValue("name", "Makar");
            */

            //var sw = new Stopwatch();

            //sw.Start();

            parser.Run();

            //sw.Stop();
            //Console.WriteLine(sw.ElapsedMilliseconds);
            //Console.Read();
        }

        public static void AddAllMethodsFrom(Type t)
        {

            MethodInfo[] inf = t.GetMethods();

            List<act> methods = new List<act>();

            for (int i = 0; i < inf.Length - 4; i++)
            {
                methods.Add((act)inf[i].CreateDelegate(typeof(act)));
            }

            Parser.getInstance().AddActions(methods);
        }
    }


}