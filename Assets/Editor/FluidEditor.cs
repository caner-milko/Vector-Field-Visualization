using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Fluid))]
public class FluidEditor: Editor {
	public override void OnInspectorGUI() {

		DrawDefaultInspector();

		Fluid fluid = (Fluid) target;
		if(GUILayout.Button("Update Equations")) {
			fluid.UpdateEquations();
		}
		EditorGUILayout.LabelField("F(x,y)=(" + fluid.horizontalCalculatedEquation + ")i + (" + fluid.verticalCalculatedEquation + ")j");
	}
}
