using UnityEditor;
namespace EKR.Editor.Graph
{
    public class SpellWordGraphEditor: EditorWindow
    {
        [MenuItem("Window/ExampleGraphEditorWindow")]
        public static void Open()
        {
            GetWindow<SpellWordGraphEditor>(ObjectNames.NicifyVariableName(nameof(SpellWordGraphEditor)));
        }

        void OnEnable()
        {
            var graphView = new SpellWordGraphView(this);
            rootVisualElement.Add(graphView);
        }
        
    }
}