using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

namespace EKR.Editor.Graph
{
    public class SpellWordGraphView: GraphView
    {
        public SpellWordGraphView(EditorWindow editorWindow)
        {
            AddElement(new StartNode());
            this.StretchToParentSize();
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
        }
    }
}