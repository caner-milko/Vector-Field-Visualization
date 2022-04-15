using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MathReader
{
    public class MathEquation
    {

        //public Dictionary<string, float> inputs;

        public float x, y, dx, dy, time, angle, magnitude;

        public MathFunction startFunction;

        public string from;

        public MathEquation(string from)
        {
            this.from = from.Replace(" ", "");
            startFunction = MathReader.ReadFunction(this.from, this);
        }

        public string AsString()
        {
            return startFunction.asString();
        }

        public bool Validate()
        {
            return startFunction != null && startFunction.Validate();
        }

        public float GetResult()
        {
            return startFunction.GetResult();
        }

    }
}