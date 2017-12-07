using UnityEngine;
using System.Collections;

namespace LevelBlockingTools
{
    public class LevelBlockingMesh_Quad : LevelBlockingMesh
    {
        public override void UpdateMesh()
        {
            meshFilter.sharedMesh.uv = origUVs;
            Vector2[] uvs = meshFilter.sharedMesh.uv;

            // apply scale
            uvs[1].y = transform.localScale.y * UVScale.y;
            uvs[2].y = transform.localScale.y * UVScale.y;
            uvs[2].x = transform.localScale.x * UVScale.x;
            uvs[3].x = transform.localScale.x * UVScale.x;

            // apply pos
            for(int i = 0; i < uvs.Length; i++)
            {
                uvs[i] += UVPos;
            }

            meshFilter.sharedMesh.uv = uvs;
            lastScale = transform.localScale;
            lastUVScale = UVScale;
            lastUVPos = UVPos;
        }

        public override void CreateMesh()
        {
            Debug.Log("Creating Quad");

            // initial hardcoded sizes for a quad
            Vector3[] verts = new Vector3[4];
            Vector2[] uvs = new Vector2[4];
            int[] tris = new int[6];

            // set vert positions
            //	1		2
            //	
            //	0		3
            verts[0] = new Vector3(0, 0, 0);
            verts[1] = new Vector3(0, 1, 0);
            verts[2] = new Vector3(1, 1, 0);
            verts[3] = new Vector3(1, 0, 0);

            // set uv positions
            //	1		2
            //
            //	0		3
            uvs[0] = new Vector2(0, 0);
            uvs[1] = new Vector2(0, 1);
            uvs[2] = new Vector2(1, 1);
            uvs[3] = new Vector2(1, 0);

            // set triangle array
            tris[0] = 0;
            tris[1] = 1;
            tris[2] = 2;
            tris[3] = 0;
            tris[4] = 2;
            tris[5] = 3;

            // assing data to the mesh
            meshFilter.sharedMesh = new Mesh();
            meshFilter.sharedMesh.vertices = verts;
            meshFilter.sharedMesh.uv = uvs;
            meshFilter.sharedMesh.triangles = tris;
            meshFilter.sharedMesh.RecalculateNormals();
            meshFilter.sharedMesh.RecalculateBounds();
            meshFilter.sharedMesh.name = "LBM_Quad_" + transform.GetInstanceID();
#if UNITY_EDITOR
            SecondaryUV(meshFilter.sharedMesh);
            TangentSolver.Solve(meshFilter.sharedMesh);
#endif
            origUVs = meshFilter.sharedMesh.uv;
            GetComponent<MeshCollider>().sharedMesh = meshFilter.sharedMesh;
        }
    }
}
