using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

[CustomPropertyDrawer(typeof(GameModeID))]
public class GameModeIDDrawer : PropertyDrawer {

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		property.intValue = (int)(GameModeID)EditorGUI.EnumMaskField(position, label, (GameModeID)property.intValue);
	}

}
