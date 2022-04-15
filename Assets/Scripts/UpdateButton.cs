using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateButton: MonoBehaviour {

	public InputField horizontal, vertical;
	public Fluid fluid;
	public Image image;

	void Start() {
		horizontal.text = fluid.horizontalCalculatedEquation;
		vertical.text = fluid.verticalCalculatedEquation;
	}

	public void UpdateEquation() {
		fluid.horizontalEquation = horizontal.text;
		fluid.verticalEquation = vertical.text;
		if(fluid.UpdateEquations()) {
			horizontal.text = fluid.horizontalCalculatedEquation;
			vertical.text = fluid.verticalCalculatedEquation;
			image.color = Color.green;
		} else {
			image.color = Color.red;
		}
	}
}
