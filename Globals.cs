using System;
using System.Collections.Generic;
using System.Text;
using ConsoleApp2;

public class Globals
{
    public static List<string> types = new List<string>();

    public static List<Variable> variables = new List<Variable>();

    public static List<string> invited_libraries = new List<string>();

    public static void AddLibraryToInvited(string name)
    {
        invited_libraries.Add(name);
    }

    public static bool IsLibraryInvited(string name)
    {
        foreach(string s in invited_libraries)
        {
            if (name == s) return true;
        }
        return false;
    }

    public static void AddTokensFromLibrary(dynamic tokens)
    {
        for(int i = 0; i < tokens.Length; i++)
        {
            Lexer.getInstance().tokens.Insert(0, tokens[i]);
        }
    }

    public static void AddVariable(string name, string type, dynamic value = null)
    {
        if (Preferences.getInstance().log)
        {
            Console.WriteLine("TRYING TO ADD VARIABLE: " + name + " = " + value);
        }

        if (!IsVariableExist(name))
        {
            variables.Add(new Variable(name, value, type));

            if (Preferences.getInstance().log)
            {
                Console.WriteLine("ADDED VARIABLE: " + name + " = " + value);
            }
        }
    }

    public static bool IsVariableExist(string name)
    {
        foreach(Variable v in variables)
        {
            if(v.name == name) { return true; }
        }

        return false;
    }

    public static dynamic GetVariableValue(string name)
    {
        foreach (Variable v in variables)
        {
            if (v.name == name) return v.value;
        }

        return null;
    }

    public static dynamic GetVariableType(string name)
    {
        foreach (Variable v in variables)
        {
            if (v.name == name) return v.type;
        }

        return null;
    }

    public static void SetVariableValue(string name, dynamic value)
    {
        foreach (Variable v in variables)
        {
            if (v.name == name)
            {
                v.value = value;
                break;
            }
        }
    }
}
