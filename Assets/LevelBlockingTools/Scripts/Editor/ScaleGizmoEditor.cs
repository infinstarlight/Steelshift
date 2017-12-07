using UnityEngine;
using UnityEditor;
using System.Collections;

namespace LevelBlockingTools
{
    [CustomEditor(typeof(ScaleGizmo))]
    public class ScaleGizmoEditor : Editor
    {
        private ScaleGizmo sg;
        private Transform t;
        private bool handldeActive = false;
        private bool isQuad = false;

        private Vector3 posHandleCurrentPos = Vector3.zero;
        private Vector3 posHandleLastPos = Vector3.zero;
        private Vector3 posHandleDelta = Vector3.zero;
        private Vector3 pos = Vector3.zero;

        void OnEnable()
        {
            sg = (ScaleGizmo)target;
            t = sg.transform;
            isQuad = sg.gameObject.GetComponent<LevelBlockingMesh_Quad>() != null;
        }

        //	public override void OnInspectorGUI()
        //	{
        //    }

        public void OnSceneGUI()
        {

            Matrix4x4 mtx = Matrix4x4.identity;
            mtx.SetTRS(t.position, t.rotation, Vector3.one);
            Handles.matrix = mtx;
            Handles.color = Color.magenta;

            pos = Vector3.one;
            pos.x *= t.localScale.x;
            pos.y *= t.localScale.y;
            if(isQuad)
            {
                pos.z = 0;
            }
            else
            {
                pos.z *= t.localScale.z;
            }
            if(Handles.Button(pos, Quaternion.LookRotation(Vector3.right), HandleUtility.GetHandleSize(pos) * 0.125f, 0.1f, Handles.SphereHandleCap))
            {
                handldeActive = true;
                posHandleLastPos = pos;
                sg.finalScale = t.localScale;
            }

            if(handldeActive)
            {
                EditorGUI.BeginChangeCheck();
                posHandleCurrentPos = Handles.PositionHandle(pos, Quaternion.identity);

                if(EditorGUI.EndChangeCheck())
                {
                    posHandleDelta = posHandleCurrentPos - posHandleLastPos;
                    //Debug.Log("XPos" + posHandleDelta.x);
                    sg.finalScale += posHandleDelta;
                    posHandleLastPos = posHandleCurrentPos;

                    Undo.RecordObject(t, "Move ScaleGizmo");
                    t.localScale = sg.finalScale;
                    EditorUtility.SetDirty(t);
                }
            }
        }
    }
}