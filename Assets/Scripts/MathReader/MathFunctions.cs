using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MathReader
{

    public class MathAddition : MathFunction
    {
        MathFunction left, right;

        public MathAddition(MathFunction left, MathFunction right, MathEquation equation) : base(equation)
        {
            this.left = left;
            this.right = right;
        }

        public override string asString()
        {
            return left.asString() + " + " + right.asString();
        }

        public override bool Validate()
        {
            return left != null && right != null && left.Validate() && right.Validate();
        }

        public override float GetResult()
        {
            return left.GetResult() + right.GetResult();
        }
    }

    public class MathSubtraction : MathFunction
    {
        MathFunction left, right;

        public MathSubtraction(MathFunction left, MathFunction right, MathEquation equation) : base(equation)
        {
            this.left = left;
            this.right = right;
        }

        public override string asString()
        {
            return left.asString() + " - " + right.asString();
        }

        public override bool Validate()
        {
            return left != null && right != null && left.Validate() && right.Validate();
        }

        public override float GetResult()
        {
            return left.GetResult() - right.GetResult();
        }
    }

    public class MathMultiplication : MathFunction
    {
        MathFunction left, right;

        public MathMultiplication(MathFunction left, MathFunction right, MathEquation equation) : base(equation)
        {
            this.left = left;
            this.right = right;
        }

        public override string asString()
        {
            return left.asString() + " * " + right.asString();
        }

        public override bool Validate()
        {
            return left != null && right != null && left.Validate() && right.Validate();
        }

        public override float GetResult()
        {
            return left.GetResult() * right.GetResult();
        }
    }

    public class MathDivision : MathFunction
    {
        MathFunction left, right;

        public MathDivision(MathFunction left, MathFunction right, MathEquation equation) : base(equation)
        {
            this.left = left;
            this.right = right;
        }

        public override string asString()
        {
            return left.asString() + " / " + right.asString();
        }

        public override bool Validate()
        {
            return left != null && right != null && left.Validate() && right.Validate();
        }

        public override float GetResult()
        {
            float leftRes = left.GetResult();
            if (leftRes == 0)
                return 0;
            float rightRes = right.GetResult();
            if (rightRes == 0)
                //big random constant
                return 100000f * Mathf.PI + Mathf.Epsilon + 1.5f;
            return leftRes / rightRes;
        }
    }

    public class MathPower : MathFunction
    {
        MathFunction left, right;

        public MathPower(MathFunction left, MathFunction right, MathEquation equation) : base(equation)
        {
            this.left = left;
            this.right = right;
        }

        public override string asString()
        {
            return left.asString() + "^" + right.asString();
        }

        public override bool Validate()
        {
            return left != null && right != null && left.Validate() && right.Validate();
        }

        public override float GetResult()
        {
            return Mathf.Pow(left.GetResult(), right.GetResult());
        }
    }

    public class MathLog : MathFunction
    {
        MathFunction left, right;

        public MathLog(MathFunction left, MathFunction right, MathEquation equation) : base(equation)
        {
            this.left = left;
            this.right = right;
        }

        public override string asString()
        {
            return left.asString() + "&" + right.asString();
        }

        public override bool Validate()
        {
            return left != null && right != null && left.Validate() && right.Validate();
        }

        public override float GetResult()
        {
            return Mathf.Log(left.GetResult(), right.GetResult());
        }
    }

    public class MathFunc : MathFunction
    {
        MathFunction param;
        Func<float, float> function;
        string functionName;

        public MathFunc(MathFunction param, Func<float, float> function, string functionName, MathEquation equation) : base(equation)
        {
            this.param = param;
            this.function = function;
            this.functionName = functionName;
        }

        public override string asString()
        {
            return functionName + "(" + param.asString() + ")";
        }

        public override bool Validate()
        {
            return param != null && function != null && param.Validate();
        }

        public override float GetResult()
        {
            return function.Invoke(param.GetResult());
        }
    }

    public class MathVariable : MathFunction
    {
        Func<MathEquation, float> variable;
        string variableName;

        public MathVariable(Func<MathEquation, float> variable, string variableName, MathEquation equation) : base(equation)
        {
            this.variable = variable;
            this.variableName = variableName;
        }

        public override string asString()
        {
            return variableName;
        }

        public override bool Validate()
        {
            return variable != null;
        }

        public override float GetResult()
        {
            return variable.Invoke(equation);
        }
    }

    public class MathNumber : MathFunction
    {
        float number;

        public MathNumber(float number, MathEquation equation) : base(equation)
        {
            this.number = number;
        }

        public override string asString()
        {
            return number + "";
        }

        public override bool Validate()
        {
            return true;
        }

        public override float GetResult()
        {
            return number;
        }
    }

    public class MathConstant : MathFunction
    {
        float constant;
        string constantName;
        public MathConstant(float constant, string constantName, MathEquation equation) : base(equation)
        {
            this.constant = constant;
            this.constantName = constantName;
        }

        public override string asString()
        {
            return constantName;
        }

        public override bool Validate()
        {
            return true;
        }

        public override float GetResult()
        {
            return constant;
        }
    }

    public class MathParentheses : MathFunction
    {
        MathFunction inside;
        public MathParentheses(MathFunction inside, MathEquation equation) : base(equation)
        {
            this.inside = inside;
        }

        public override string asString()
        {
            return "(" + inside.asString() + ")";
        }

        public override bool Validate()
        {
            return inside != null && inside.Validate();
        }

        public override float GetResult()
        {
            return inside.GetResult();
        }

    }

    public class MathMinus : MathFunction
    {
        MathFunction inside;
        public MathMinus(MathFunction inside, MathEquation equation) : base(equation)
        {
            this.inside = inside;
        }

        public override string asString()
        {
            return "-" + inside.asString();
        }

        public override bool Validate()
        {
            return inside != null && inside.Validate();
        }

        public override float GetResult()
        {
            return -inside.GetResult();
        }

    }

}