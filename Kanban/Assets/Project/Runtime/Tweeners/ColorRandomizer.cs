// Solution: Opting for Unity's less-than-ideal serialization system instead of reflection.
// This choice is driven by the fact that color input fields visible in the inspector can be
// intricately nested by cunning developers and concealed by a script's editor, making them appear
// as if they are[SerializeField] - marked fields within the current MonoBehaviour instance.

using UnityEditor;
using UnityEngine;

public class ColorRandomizer : MonoBehaviour
{
    [SerializeField] private Color _seedColor;
    private SerializedProperty _targetColorProperty;

    private SerializedProperty TargetColorProperty
    {
        get
        {
            _targetColorProperty ??= GetPropertyByCachedName();
            return _targetColorProperty;
        }
    }

    [field: SerializeField] internal UnityEngine.Object TargetObject { get; set; }

    [field: SerializeField, HideInInspector] internal string TargetPropertyName { get; set; }

    private SerializedProperty GetPropertyByCachedName()
    {
        if(string.IsNullOrEmpty(TargetPropertyName))
            return null;
        else
            return new SerializedObject(TargetObject).FindProperty(TargetPropertyName);
    }

    // Update is called once per frame
    private void Update()
    {
        if(TargetObject != null && TargetColorProperty != null)
        {
            Color.RGBToHSV(_seedColor, out float h, out float s, out float v);
            //s -= Time.deltaTime;
            //s = Mathf.Repeat(s, 1.0f);
            s = Mathf.PingPong(Time.timeSinceLevelLoad, 1.0f);
            Color nextColor = Color.HSVToRGB(h, s, v);

            TargetColorProperty.colorValue = nextColor;
            TargetColorProperty.serializedObject.ApplyModifiedProperties();
        }
    }
}

[CustomEditor(typeof(ColorRandomizer))]
public class ColorRandomizerEditor : Editor
{
    // Target script whitch being edited.
    private ColorRandomizer Target => (ColorRandomizer)target;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if(EditorGUILayout.DropdownButton(new GUIContent($"{Target.TargetPropertyName}"), FocusType.Passive))
        {
            GeneratePropertiesMenu().ShowAsContext();
        }
    }

    private void AddMenuItem(GenericMenu menu, string propertyName)
    {
        var itemName = new GUIContent($"{propertyName}");
        bool IsItemChecked = false;
        menu.AddItem(itemName, IsItemChecked, () => Target.TargetPropertyName = propertyName);
    }

    private GenericMenu GeneratePropertiesMenu()
    {
        GenericMenu menu = new GenericMenu();

        if(Target.TargetObject == null)
            return menu;

        var serializedTargetObject = new SerializedObject(Target.TargetObject);
        var currentProperty = serializedTargetObject.GetIterator();

        while(currentProperty.Next(enterChildren: true))
        {
            if(currentProperty.propertyType == SerializedPropertyType.Color)
                AddMenuItem(menu, currentProperty.name);
        }

        return menu;
    }
}
