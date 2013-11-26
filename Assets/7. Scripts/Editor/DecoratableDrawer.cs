using UnityEditor;
using UnityEngine;
using System.Collections;


[CustomPropertyDrawer(typeof(DecoratableFloat))]
public class DecoratableFloatDrawer : PropertyDrawer {

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		var rawValue = property.FindPropertyRelative("rawValue");
		rawValue.floatValue = EditorGUI.FloatField(position, label, rawValue.floatValue);
	}
}

[CustomPropertyDrawer(typeof(DecoratableVector3))]
public class DecoratableVector3Drawer : PropertyDrawer {
	
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		var rawValue = property.FindPropertyRelative("rawValue");
		rawValue.vector3Value = EditorGUI.Vector3Field(position, label, rawValue.vector3Value);
	}
}