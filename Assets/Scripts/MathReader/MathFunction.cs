using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MathReader {
	public abstract class MathFunction {
		protected readonly MathEquation equation;

		public MathFunction(MathEquation equation) {
			this.equation = equation;
		}

		public abstract float GetResult();

		public abstract string asString();

		public abstract bool Validate();

		public override string ToString() {
			return asString();
		}
	}
}