using UnityEngine;
using UnityEditor;
using System.Collections;

namespace LevelBlockingTools
{
    public class LevelBlockingMeshEditor : Editor
    {
        protected LevelBlockingMesh lbm;
        protected Transform t;
        protected SerializedProperty UVPosProp;
        protected SerializedProperty UVScaleProp;

        public virtual void OnEnable()
        {
            lbm = (LevelBlockingMesh)target;
            t = lbm.transform;
            UVPosProp = serializedObject.FindProperty("UVPos");
            UVScaleProp = serializedObject.FindProperty("UVScale");
        }

        public override void OnInspectorGUI()
        {
            // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
            serializedObject.Update();

            EditorGUILayout.Separator();
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(UVPosProp, new GUIContent("Texture Position"));
            EditorGUILayout.PropertyField(UVScaleProp, new GUIContent("Texture Tiling"));
            EditorGUILayout.Separator();
            EditorGUILayout.EndVertical();

            // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
            serializedObject.ApplyModifiedProperties();
        }

        public virtual void OnSceneGUI()
        {
            Event e = Event.current;
            if(e != null && e.type == EventType.ValidateCommand && (e.commandName == "Paste" || e.commandName == "Duplicate"))
            {
                LevelBlockingMesh.duplicate = true;
                LevelBlockingMesh.dupAmount++;
                //Debug.Log("LBM Dup Amount : " + LevelBlockingMesh.dupAmount);
            }

            if(lbm.lastScale != t.localScale || lbm.UVScale != lbm.lastUVScale || lbm.UVPos != lbm.lastUVPos)
            {
                lbm.UpdateMesh();
            }
        }
    }
}