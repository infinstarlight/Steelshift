using UnityEngine;
using UnityEditor;
using System.Collections;

namespace LevelBlockingTools
{
    [CustomEditor(typeof(LevelBlockingMesh_Slope))]
    [CanEditMultipleObjects]
    public class LevelBlockingSlopeEditor : LevelBlockingMeshEditor
    {
        protected SerializedProperty widthProp;

        private Vector3 posHandleCurrentPos = Vector3.zero;
        private Vector3 posHandleLastPos = Vector3.zero;
        private Vector3 posHandleDelta = Vector3.zero;
        private Vector3 pos = Vector3.zero;

        private LevelBlockingMesh_Slope lbm_s;
        private bool handldeActive = false;

        public override void OnEnable()
        {
            lbm_s = (LevelBlockingMesh_Slope)target;
            t = lbm_s.transform;
            UVPosProp = serializedObject.FindProperty("UVPos");
            UVScaleProp = serializedObject.FindProperty("UVScale");
            widthProp = serializedObject.FindProperty("width");
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
            EditorGUI.BeginChangeCheck();
            lbm_s.slopePos.y = EditorGUILayout.FloatField("Slope Height", lbm_s.slopePos.y);
            EditorGUILayout.PropertyField(widthProp);
            EditorGUILayout.Separator();
            EditorGUILayout.EndVertical();

            // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
            serializedObject.ApplyModifiedProperties();

            if(EditorGUI.EndChangeCheck())
            {
                lbm_s.UpdateMesh();
            }
        }

        public override void OnSceneGUI()
        {
            Event e = Event.current;
            if(e != null && e.type == EventType.ValidateCommand && (e.commandName == "Paste" || e.commandName == "Duplicate"))
            {
                LevelBlockingMesh.duplicate = true;
            }

            Matrix4x4 mtx = Matrix4x4.identity;
            mtx.SetTRS(t.position, t.rotation, Vector3.one);
            Handles.matrix = mtx;
            Handles.color = Color.magenta;

            pos = Vector3.one;
            pos.x *= t.localScale.x;
            pos.y = lbm_s.slopePos.y;
            pos.z *= t.localScale.z;

            if(Handles.Button(pos, Quaternion.LookRotation(Vector3.right), HandleUtility.GetHandleSize(pos) * 0.125f, 0.1f, Handles.SphereHandleCap))
            {
                handldeActive = true;
                posHandleLastPos = pos;
                lbm_s.slopePos = new Vector3(t.localScale.x, lbm_s.slopePos.y, t.localScale.z);
            }

            if(handldeActive)
            {
                EditorGUI.BeginChangeCheck();
                posHandleCurrentPos = Handles.PositionHandle(pos, Quaternion.identity);

                if(EditorGUI.EndChangeCheck())
                {
                    posHandleDelta = posHandleCurrentPos - posHandleLastPos;
                    //Debug.Log("XPos" + posHandleDelta.x);
                    Undo.RecordObject(lbm_s, "Move Slope Y");
                    lbm_s.slopePos += posHandleDelta;
                    posHandleLastPos = posHandleCurrentPos;

                    Undo.RecordObject(t, "Scale Slope");
                    t.localScale = new Vector3(lbm_s.slopePos.x, 1, lbm_s.slopePos.z);
                    EditorUtility.SetDirty(t);

                    lbm_s.UpdateMesh();
                }
            }
        }
    }
}