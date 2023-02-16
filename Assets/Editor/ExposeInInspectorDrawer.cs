using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ExposeInInspectorAttribute))]
public class ExposeInInspectorDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginChangeCheck();

        var targetObject = property.serializedObject.targetObject;
        var targetType = targetObject.GetType();
        var fieldName = property.propertyPath;

        // Find the field in the object's type hierarchy, including non-public fields
        var field = targetType.GetField(fieldName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);

        // Draw the field in the inspector
        if (field != null)
        {
            var fieldValue = field.GetValue(targetObject);
            var fieldType = field.FieldType;
            var labelContent = new GUIContent(label.text);

            if (fieldType == typeof(bool))
            {
                fieldValue = EditorGUI.Toggle(position, labelContent, (bool)fieldValue);
            }
            else if (fieldType == typeof(int))
            {
                fieldValue = EditorGUI.IntField(position, labelContent, (int)fieldValue);
            }
            else if (fieldType == typeof(float))
            {
                fieldValue = EditorGUI.FloatField(position, labelContent, (float)fieldValue);
            }
            else if (fieldType == typeof(string))
            {
                fieldValue = EditorGUI.TextField(position, labelContent, (string)fieldValue);
            }

            if (EditorGUI.EndChangeCheck())
            {
                field.SetValue(targetObject, fieldValue);
            }
        }
    }
}
