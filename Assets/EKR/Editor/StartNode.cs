using UnityEditor.Experimental.GraphView;

namespace EKR.Editor.Graph
{
    public class StartNode:Node
    {
        public StartNode()
        {
            title = "Start";
            
            var outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
            outputPort.portName = "Next";
            outputContainer.Add(outputPort);
        }
    }
}