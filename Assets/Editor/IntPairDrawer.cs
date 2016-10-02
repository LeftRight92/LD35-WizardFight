using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(IntPair))]
public class IntPairDrawer : PropertyDrawer {

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		EditorGUI.BeginProperty(position, label, property);
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
		//GUILayout.BeginHorizontal();
		EditorGUI.LabelField(
			new Rect(position.x, position.y, 12, position.height),
			new GUIContent("A")
			);
		EditorGUI.PropertyField(
			new Rect(position.x + 12, position.y, 68, position.height),
			property.FindPropertyRelative("a"),
			GUIContent.none//new GUIContent("X")
			);
		EditorGUI.LabelField(
			new Rect(position.x + 80, position.y, 15, position.height),
			new GUIContent("B")
			);
		EditorGUI.PropertyField(
			new Rect(position.x + 95, position.y, 68, position.height),
			property.FindPropertyRelative("b"),
			GUIContent.none//new GUIContent("Y")
			);
		//GUILayout.EndHorizontal();
		EditorGUI.EndProperty();
	}
}
