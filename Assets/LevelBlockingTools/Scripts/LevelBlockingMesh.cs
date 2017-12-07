using UnityEngine;
using System.Collections;

namespace LevelBlockingTools
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    [ExecuteInEditMode]
    public class LevelBlockingMesh : MonoBehaviour
    {
        public static bool duplicate = false;
        public static int dupAmount = 0;

        //#if UNITY_EDITOR
        public LayerMask drawLayer;
        public Vector3 lastScale = Vector3.one;
        public Vector2 lastUVPos = Vector2.zero;
        public Vector2 lastUVScale = Vector2.one;
//#endif
        public Vector2 UVPos = Vector2.zero;
        public Vector2 UVScale = Vector2.one;
        public Vector2[] origUVs;

        private MeshFilter mf;
        public MeshFilter meshFilter
        {
            get
            {
                if(mf == null) { mf = GetComponent<MeshFilter>(); }
                return mf;
            }
            set
            {
                mf = value;
            }
        }

        void OnEnable()
        {
            //if(meshFilter.sharedMesh == null)
            //{
            //    CreateMesh();
            //}

            //if(meshFilter.sharedMesh.name == "")
            //{
            //    meshFilter.sharedMesh.name = "lbm_mesh_" + transform.GetInstanceID();
            //}

            if(duplicate)
            {
                LBMDup();
            }
        }

        void LBMDup()
        {
            //Debug.Log("Duplicating LBM - " + transform.GetInstanceID());
            Mesh oldMesh = GetComponent<MeshFilter>().sharedMesh;
            Mesh newMesh = new Mesh();
            string newName = this.GetType().ToString().Replace("LevelBlockingTools.LevelBlockingMesh", "LBM");
            //Debug.Log(newName);
            newMesh.name = newName + "_" + transform.GetInstanceID();
            newMesh.vertices = oldMesh.vertices;
            newMesh.uv = oldMesh.uv;
            newMesh.uv2 = oldMesh.uv2;
            newMesh.triangles = oldMesh.triangles;
            newMesh.normals = oldMesh.normals;
            newMesh.RecalculateBounds();
            //newMesh.Optimize();
            meshFilter.sharedMesh = newMesh;

            LevelBlockingMesh.dupAmount--;
            if(LevelBlockingMesh.dupAmount <= 0)
            {
                Debug.Log("Duplication Complete");
                duplicate = false;
            }
        }

#if UNITY_EDITOR
        protected void SecondaryUV(Mesh m)
        {
            UnityEditor.UnwrapParam param = new UnityEditor.UnwrapParam();
            UnityEditor.UnwrapParam.SetDefaults(out param);
            UnityEditor.Unwrapping.GenerateSecondaryUVSet(m, param);
        }
#endif

        public virtual void CreateMesh() { }
        public virtual void UpdateMesh() { }
    }
}