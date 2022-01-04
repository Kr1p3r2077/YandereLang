using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ConsoleApp2
{
    public delegate int act(Token[] tokens, ref int i);
    class Parser
    {
        private static Parser instance;

        public List<Token> tokens;

        //List<Func<Token[],  int, int>> actions = new List<Func<Token[], int, int>>(); //= delegate (Token[] tokens, int index){};

        public List<act> actions = new List<act>();

        public static Parser getInstance()
        {
            if (instance == null)
                instance = new Parser();
            return instance;
        }

        public Parser()
        {
            tokens = new List<Token>();
        }

        public Parser(List<Token> t)
        {
            tokens = t;
        }

        public void SetActions(List<act> acts)
        {
            actions = acts;
        }

        public void AddAction(act act)
        {
            actions.Add(act);
        }

        public void AddActions(List<act> acts)
        {
            actions.AddRange(acts);
        }

        public void execute(Token[] ts, int i_start, int i_end, int how_many)
        {
            for (int g = 0; g < how_many - 1; g++) {
                for (int i = i_start; i <= i_end; i++)
                {
                    for (int j = 0; j < actions.Count; j++)
                    {
                        if (actions[j](ts, ref i) == 0) { break; }
                    }
                }
            }
        }

        public void Run()
        {
            Token[] ts = tokens.ToArray();

            for (int i = 0; i < tokens.Count; i++)
            {
                
                for(int j = 0; j < actions.Count; j++)
                {
                    if (actions[j](ts, ref i) == 0) { break; }
                }

            }

        }
    }
}
