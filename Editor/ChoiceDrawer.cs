using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// [CustomPropertyDrawer(typeof(EventChoice))]
public class ChoiceDrawer : PropertyDrawer {

	    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        // position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        var displayTextRect = new Rect(position.x + 5, position.y, 200, position.height);
        // var defaultValueRect = new Rect(position.x + 75, position.y, 30, position.height);
        // var currentValueRect = new Rect(position.x + 115, position.y, 30, position.height);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(displayTextRect, property.FindPropertyRelative("displayText"), GUIContent.none);
        // EditorGUI.PropertyField(defaultValueRect, property.FindPropertyRelative("defaultValue"), GUIContent.none);
        // EditorGUI.PropertyField(currentValueRect, property.FindPropertyRelative("currentValue"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
