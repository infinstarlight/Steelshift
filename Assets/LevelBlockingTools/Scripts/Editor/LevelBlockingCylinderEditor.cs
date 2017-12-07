using UnityEngine;
using UnityEditor;
using System.Collections;

namespace LevelBlockingTools
{
    [CustomEditor(typeof(LevelBlockingMesh_Cylinder))]
    [CanEditMultipleObjects]
    public class LevelBlockingCylinderEditor : LevelBlockingMeshEditor
    {
        //private LevelBlockingMesh_Cylinder lbm;
        protected SerializedProperty sectionsProp;
        protected SerializedProperty radiusProp;
        //protected SerializedProperty bendAngleProp;
        protected SerializedProperty offsetProp;

        public override void OnEnable()
        {
            base.OnEnable();
            //lbm = (LevelBlockingMesh_Cylinder)target;
            sectionsProp = serializedObject.FindProperty("sections");
            radiusProp = serializedObject.FindProperty("radius");
            //bendAngleProp = serializedObject.FindProperty("bendAngle");
            offsetProp = serializedObject.FindProperty("offset");
        }

        public override void OnInspectorGUI()
        {
            // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
            serializedObject.Update();

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.Separator();
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(UVPosProp, new GUIContent("Texture Position"));
            EditorGUILayout.PropertyField(UVScaleProp, new GUIContent("Texture Tiling"));

            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(sectionsProp);
            EditorGUILayout.PropertyField(radiusProp);
            //EditorGUILayout.PropertyField(bendAngleProp);
            EditorGUILayout.PropertyField(offsetProp, new GUIContent("Pivot Offset"));
            EditorGUILayout.Separator();
            EditorGUILayout.EndVertical();

            // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
            serializedObject.ApplyModifiedProperties();

            if(EditorGUI.EndChangeCheck())
            {
                //Debug.Log("------------> Sel");
                Undo.RecordObject(lbm.meshFilter, "Update LBM Cylinder Mesh");
                Undo.RecordObject(lbm.GetComponent<MeshCollider>(), "Update LBM Cylinder ColMesh");
                lbm.UpdateMesh();
            }
        }
    }
}
