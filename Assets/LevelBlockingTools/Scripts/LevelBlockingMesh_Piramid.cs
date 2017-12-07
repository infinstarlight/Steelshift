using UnityEngine;
using System.Collections;

namespace LevelBlockingTools
{
    public class LevelBlockingMesh_Piramid : LevelBlockingMesh
    {
        public override void UpdateMesh()
        {
            meshFilter.sharedMesh.uv = origUVs;
            Vector2[] uvs = meshFilter.sharedMesh.uv;

            for(int i = 0; i < 12; i += 3)
            {
                if(i >= 6)
                {
                    uvs[i + 1].y = ((transform.localScale.y + transform.localScale.x) / 2) * UVScale.y;
                    uvs[i + 1].x = transform.localScale.z / 2 * UVScale.x;
                    uvs[i + 2].x = transform.localScale.z * UVScale.x;
                }
                else
                {
                    uvs[i + 1].y = ((transform.localScale.y + transform.localScale.z) / 2) * UVScale.y;
                    uvs[i + 1].x = transform.localScale.x / 2 * UVScale.x;
                    uvs[i + 2].x = transform.localScale.x * UVScale.x;
                }
            }

            // bottom
            uvs[13].y = transform.localScale.z * UVScale.y;
            uvs[14].y = transform.localScale.z * UVScale.y;
            uvs[14].x = transform.localScale.x * UVScale.x;
            uvs[15].x = transform.localScale.x * UVScale.x;

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
            Debug.Log("Creating Piramid");

            // initial hardcoded sizes for a wedge
            Vector3[] verts = new Vector3[16];
            Vector2[] uvs = new Vector2[16];
            int[] tris = new int[18];

            // set vert positions per face
            //		1				1		2
            //	
            //	0		2			0		3
            //
            // front
            verts[0] = new Vector3(0, 0, 0);
            verts[1] = new Vector3(0.5f, 1, 0.5f);
            verts[2] = new Vector3(1, 0, 0);
            // back
            verts[3] = new Vector3(1, 0, 1);
            verts[4] = new Vector3(0.5f, 1, 0.5f);
            verts[5] = new Vector3(0, 0, 1);
            // left
            verts[6] = new Vector3(1, 0, 0);
            verts[7] = new Vector3(0.5f, 1, 0.5f);
            verts[8] = new Vector3(1, 0, 1);
            // right
            verts[9] = new Vector3(0, 0, 1);
            verts[10] = new Vector3(0.5f, 1, 0.5f);
            verts[11] = new Vector3(0, 0, 0);
            // bottom
            verts[12] = new Vector3(0, 0, 1);
            verts[13] = new Vector3(0, 0, 0);
            verts[14] = new Vector3(1, 0, 0);
            verts[15] = new Vector3(1, 0, 1);

            // set uv positions per face
            //		1				1		2
            //	
            //	0		2			0		3
            //
            // first 12 form 4 tris
            for(int i = 0; i < 12; i += 3)
            {
                uvs[i] = new Vector2(0, 0);
                uvs[i + 1] = new Vector2(0.5f, 1);
                uvs[i + 2] = new Vector2(1, 0);
            }
            // last 4 form 1 quad
            uvs[12] = new Vector2(0, 0);
            uvs[13] = new Vector2(0, 1);
            uvs[14] = new Vector2(1, 1);
            uvs[15] = new Vector2(1, 0);


            // set triangle array per face
            // first 12 form 4 tris
            int anchorIndex = 0;
            for(int i = 0; i < 12; i += 3)
            {
                tris[i] = anchorIndex;
                tris[i + 1] = anchorIndex + 1;
                tris[i + 2] = anchorIndex + 2;
                anchorIndex += 3;
            }
            // last 6 form 1 quad
            tris[12] = 12;
            tris[13] = 13;
            tris[14] = 14;
            tris[15] = 12;
            tris[16] = 14;
            tris[17] = 15;

            // assing data to the mesh
            meshFilter.sharedMesh = new Mesh();
            meshFilter.sharedMesh.vertices = verts;
            meshFilter.sharedMesh.uv = uvs;
            meshFilter.sharedMesh.triangles = tris;
            meshFilter.sharedMesh.RecalculateNormals();
            meshFilter.sharedMesh.RecalculateBounds();
            meshFilter.sharedMesh.name = "LBM_Piramid_" + transform.GetInstanceID();
#if UNITY_EDITOR
            SecondaryUV(meshFilter.sharedMesh);
            TangentSolver.Solve(meshFilter.sharedMesh);
#endif
            origUVs = meshFilter.sharedMesh.uv;
            GetComponent<MeshCollider>().sharedMesh = meshFilter.sharedMesh;
        }
    }
}
