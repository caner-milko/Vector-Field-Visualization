using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

[CustomEditor(typeof(SpriteCreator))]
public class SpriteCreatorEditor: Editor {

	public override void OnInspectorGUI() {
		base.OnInspectorGUI();
		if(GUILayout.Button("Create Empty Sprite")) {
			SpriteCreator sc = (SpriteCreator) target;
			Sprite toSave = sc.CreateEmptySprite();
			File.WriteAllBytes(FileUtil.GetProjectRelativePath(EditorUtility.SaveFilePanel("Sprite To Save", "Assets/", "sprite", "png")), toSave.texture.EncodeToPNG());
			AssetDatabase.Refresh();
			GUIUtility.ExitGUI();
		}
	}

}
