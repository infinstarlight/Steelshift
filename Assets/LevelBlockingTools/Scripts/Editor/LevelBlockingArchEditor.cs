using UnityEngine;
using UnityEditor;
using System.Collections;

namespace LevelBlockingTools
{
    [CustomEditor(typeof(LevelBlockingMesh_Arch))]
    [CanEditMultipleObjects]
    public class LevelBlockingArchEditor : LevelBlockingMeshEditor
    {
        //private LevelBlockingMesh_Arch lbm;
        protected SerializedProperty sectionsProp;
        protected SerializedProperty radiusProp;
        protected SerializedProperty bendAngleProp;
        protected SerializedProperty startAngleProp;
        protected SerializedProperty offsetProp;
        protected SerializedProperty widthProp;
        protected SerializedProperty squaredProp;

        public override void OnEnable()
        {
            base.OnEnable();
            //lbm = (LevelBlockingMesh_Arch)target;
            sectionsProp = serializedObject.FindProperty("sections");
            radiusProp = serializedObject.FindProperty("radius");
            startAngleProp = serializedObject.FindProperty("startAngle");
            bendAngleProp = serializedObject.FindProperty("bendAngle");
            widthProp = serializedObject.FindProperty("width");
            offsetProp = serializedObject.FindProperty("offset");
            squaredProp = serializedObject.FindProperty("squared");
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

            EditorGUILayout.PropertyField(squaredProp);
            EditorGUILayout.PropertyField(sectionsProp);
            EditorGUILayout.PropertyField(radiusProp);
            EditorGUILayout.PropertyField(startAngleProp);
            EditorGUILayout.PropertyField(bendAngleProp);
            EditorGUILayout.PropertyField(widthProp);
            EditorGUILayout.PropertyField(offsetProp, new GUIContent("Pivot Offset"));
            EditorGUILayout.Separator();
            EditorGUILayout.EndVertical();

            // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
            serializedObject.ApplyModifiedProperties();

            if(EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(lbm.meshFilter, "Update LBM Arch Mesh");
                Undo.RecordObject(lbm.GetComponent<MeshCollider>(), "Update LBM Arch ColMesh");
                lbm.UpdateMesh();
            }
        }
    }
}
