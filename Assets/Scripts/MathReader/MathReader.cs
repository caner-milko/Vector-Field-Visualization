using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MathReader
{
    public class MathReader
    {

        public static Dictionary<string, Func<float, float>> functions = new Dictionary<string, Func<float, float>> {
            {"cos", (param)=>Mathf.Cos(param)},
            {"sin", (param)=>Mathf.Sin(param)},
            {"cot", (param)=>1.0f/Mathf.Tan(param)},
            {"tan", (param)=>Mathf.Tan(param)},
            {"arctan", (param)=>Mathf.Atan(param)},
            {"arccos", (param)=>Mathf.Acos(param)},
            {"arcsin", (param)=>Mathf.Asin(param)},
            {"exp", (param)=>Mathf.Exp(param)},
            {"ln", (param)=>Mathf.Log(param)},
            {"sqrt", (param)=>Mathf.Sqrt(param)},
            {"abs", (param)=>Mathf.Abs(param)},
        };

        public static Dictionary<string, float> constants = new Dictionary<string, float> {

            {"e", Mathf.Epsilon},
            {"E", Mathf.Epsilon},
            {"pi", Mathf.PI},
            {"PI", Mathf.PI},
            {"tau", (Mathf.PI * 2.0f)},
            {"TAU", (Mathf.PI * 2.0f)},
        };

        public static MathFunction ReadFunction(string str, MathEquation eq)
        {
            if (!CheckAllParantheses(str))
                return null;
            string[] array = SearchFor(str, '+');
            if (array != null)
            {
                if (array[0].Length == 0)
                    return ReadFunction(array[1], eq);
                else if (array[1].Length == 0)
                    return null;
                return new MathAddition(ReadFunction(array[0], eq), ReadFunction(array[1], eq), eq);
            }

            array = SearchFor(str, '-');
            if (array != null)
            {
                if (array[1].Length == 0)
                    return null;
                else if (array[0].Length == 0)
                {
                    return new MathMinus(ReadFunction(array[1], eq), eq);
                }
                return new MathSubtraction(ReadFunction(array[0], eq), ReadFunction(array[1], eq), eq);
            }

            array = SearchFor(str, '*');
            if (array != null)
            {
                if (array[0].Length == 0 || array[1].Length == 0)
                    return null;
                return new MathMultiplication(ReadFunction(array[0], eq), ReadFunction(array[1], eq), eq);
            }

            array = SearchFor(str, '/');
            if (array != null)
            {
                if (array[0].Length == 0 || array[1].Length == 0)
                    return null;
                return new MathDivision(ReadFunction(array[0], eq), ReadFunction(array[1], eq), eq);
            }

            array = SearchFor(str, '^');
            if (array != null)
            {
                if (array[0].Length == 0 || array[1].Length == 0)
                    return null;
                return new MathPower(ReadFunction(array[0], eq), ReadFunction(array[1], eq), eq);
            }

            array = SearchFor(str, '&');
            if (array != null)
            {
                if (array[0].Length == 0 || array[1].Length == 0)
                    return null;
                return new MathLog(ReadFunction(array[0], eq), ReadFunction(array[1], eq), eq);
            }

            //check for functions
            foreach (KeyValuePair<string, Func<float, float>> pair in functions)
            {
                if (str.StartsWith(pair.Key))
                {
                    string insideParantheses = TrimOuterParentheses(str.Substring(pair.Key.Length), false);
                    return new MathFunc(ReadFunction(insideParantheses, eq), pair.Value, pair.Key, eq);
                }
            }

            if (str.EndsWith(")"))
            {
                return new MathParentheses(ReadFunction(TrimOuterParentheses(str, false), eq), eq);
            }


            if (str.Equals("x"))
            {
                return new MathVariable((equation) => equation.x, "x", eq);
            }
            if (str.Equals("y"))
            {
                return new MathVariable((equation) => equation.y, "y", eq);
            }
            if (str.Equals("dx"))
            {
                return new MathVariable((equation) => equation.dx, "dx", eq);
            }
            if (str.Equals("dy"))
            {
                return new MathVariable((equation) => equation.dy, "dy", eq);
            }
            if (str.Equals("angle"))
            {
                return new MathVariable((equation) => equation.angle, "angle", eq);
            }
            if (str.Equals("time"))
            {
                return new MathVariable((equation) => equation.time, "time", eq);
            }
            if (str.Equals("r"))
            {
                return new MathVariable((equation) => equation.magnitude, "r", eq);
            }

            foreach (KeyValuePair<string, float> pair in constants)
            {
                if (pair.Key.Equals(str))
                    return new MathConstant(pair.Value, pair.Key, eq);
            }

            float res;
            if (float.TryParse(str, out res))
                return new MathNumber(res, eq);

            return null;
        }

        public static bool CheckAllParantheses(string str)
        {
            int count = 0;
            for (int i = 0; i < str.Length; i++)
            {
                char ch = str[i];
                if (ch == '(')
                    count++;
                if (ch == ')')
                    count--;
            }
            return count == 0;
        }

        public static string TrimOuterParentheses(string str, bool fromEnd)
        {
            if (fromEnd)
            {
                int start = GetStartOfParantheses(str);
                if (start == -1)
                    return null;
                return str.Substring(start + 1, str.Length - start - 1);
            }
            else
            {
                int end = GetEndOfParantheses(str);
                if (end == -1)
                    return null;
                return str.Substring(1, end - 1);
            }

        }

        public static int GetEndOfParantheses(string str)
        {
            int count = 0;
            for (int i = 0; i < str.Length; i++)
            {
                char ch = str[i];
                if (ch == '(')
                    count++;
                if (ch == ')')
                    count--;
                if (count <= 0)
                {
                    return i;
                }
            }
            return -1;
        }

        public static int GetStartOfParantheses(string str)
        {
            int count = 0;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                char ch = str[i];
                if (ch == '(')
                    count++;
                if (ch == ')')
                    count--;
                if (count >= 0)
                {
                    return i;
                }
            }
            return -1;
        }

        public static string[] SearchFor(string str, char search)
        {
            for (int i = str.Length - 1; i >= 0; i--)
            {
                char ch = str[i];
                if (ch == ')')
                {
                    int temp = i;
                    i = GetStartOfParantheses(str.Substring(0, i + 1));
                    if (temp <= i)
                    {
                        Debug.Log("error");
                        return null;
                    }
                    continue;
                }
                if (ch == search)
                {
                    string[] array = new string[2];
                    array[0] = str.Substring(0, i);
                    array[1] = str.Substring(i + 1);
                    return array;
                }
            }
            return null;
        }

    }
}
