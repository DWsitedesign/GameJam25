using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor (typeof(PlacementGenerator))]
public class PlacementGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PlacementGenerator treeGen = (PlacementGenerator) target;
        if (DrawDefaultInspector() && treeGen.autoUpdate)
        {
                treeGen.Generate();
        }

        if(GUILayout.Button("Generate")){
            treeGen.Generate();
        }

        if(GUILayout.Button("Clear")){
            treeGen.Clear();
        }
    }
}
#endif

