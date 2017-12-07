using UnityEngine;
using System.Collections;

namespace LevelBlockingTools
{
    public class LevelBlockingMesh_Wedge : LevelBlockingMesh
    {
        public override void UpdateMesh()
        {
            meshFilter.sharedMesh.uv = origUVs;
            Vector2[] uvs = meshFilter.sharedMesh.uv;

            // apply scale
            // slope
            uvs[1].y = ((transform.localScale.y + transform.localScale.z) / 2) * UVScale.y;
            uvs[2].y = ((transform.localScale.y + transform.localScale.z) / 2) * UVScale.y;
            uvs[2].x = transform.localScale.x * UVScale.x;
            uvs[3].x = transform.localScale.x * UVScale.x;
            // back
            uvs[5].y = transform.localScale.y * UVScale.y;
            uvs[6].y = transform.localScale.y * UVScale.y;
            uvs[6].x = transform.localScale.x * UVScale.x;
            uvs[7].x = transform.localScale.x * UVScale.x;
            // bottom
            uvs[9].y = transform.localScale.z * UVScale.y;
            uvs[10].y = transform.localScale.z * UVScale.y;
            uvs[10].x = transform.localScale.x * UVScale.x;
            uvs[11].x = transform.localScale.x * UVScale.x;

            // left/right
            uvs[12].x = transform.localScale.z * UVScale.x * -1;
            uvs[13].y = transform.localScale.y * UVScale.y;
            uvs[16].y = transform.localScale.y * UVScale.y;
            uvs[17].x = transform.localScale.z * UVScale.x;

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
            Debug.Log("Creating Wedge");

            // initial hardcoded sizes for a wedge
            Vector3[] verts = new Vector3[18];
            Vector2[] uvs = new Vector2[18];
            int[] tris = new int[24];

            // set vert positions per face
            //	1		2
            //	
            //	0		3
            //
            // slope
            verts[0] = new Vector3(0, 0, 0);
            verts[1] = new Vector3(0, 1, 1);
            verts[2] = new Vector3(1, 1, 1);
            verts[3] = new Vector3(1, 0, 0);
            // back
            verts[4] = new Vector3(1, 0, 1);
            verts[5] = new Vector3(1, 1, 1);
            verts[6] = new Vector3(0, 1, 1);
            verts[7] = new Vector3(0, 0, 1);
            // bottom
            verts[8] = new Vector3(0, 0, 1);
            verts[9] = new Vector3(0, 0, 0);
            verts[10] = new Vector3(1, 0, 0);
            verts[11] = new Vector3(1, 0, 1);
            // left
            verts[12] = new Vector3(1, 0, 0);
            verts[13] = new Vector3(1, 1, 1);
            verts[14] = new Vector3(1, 0, 1);
            // right
            verts[15] = new Vector3(0, 0, 1);
            verts[16] = new Vector3(0, 1, 1);
            verts[17] = new Vector3(0, 0, 0);

            // set uv positions per face
            //	1		2
            //
            //	0		3
            //
            // first 12 form 3 quads
            for(int i = 0; i < 12; i += 4)
            {
                uvs[i] = new Vector2(0, 0);
                uvs[i + 1] = new Vector2(0, 1);
                uvs[i + 2] = new Vector2(1, 1);
                uvs[i + 3] = new Vector2(1, 0);
            }
            // last 6 form 2 tris
            uvs[12] = new Vector2(-1, 0);
            uvs[13] = new Vector2(0, 1);
            uvs[14] = new Vector2(0, 0);
            uvs[15] = new Vector2(0, 0);
            uvs[16] = new Vector2(0, 1);
            uvs[17] = new Vector2(1, 0);


            // set triangle array per face
            // first 18 form 3 quads
            int anchorIndex = 0;
            for(int i = 0; i < 18; i += 6)
            {
                tris[i] = anchorIndex;
                tris[i + 1] = anchorIndex + 1;
                tris[i + 2] = anchorIndex + 2;
                tris[i + 3] = anchorIndex;
                tris[i + 4] = anchorIndex + 2;
                tris[i + 5] = anchorIndex + 3;

                anchorIndex += 4;
            }
            // last 6 form 2 tris
            tris[18] = 12;
            tris[19] = 13;
            tris[20] = 14;
            tris[21] = 15;
            tris[22] = 16;
            tris[23] = 17;

            // assing data to the mesh
            meshFilter.sharedMesh = new Mesh();
            meshFilter.sharedMesh.vertices = verts;
            meshFilter.sharedMesh.uv = uvs;
            meshFilter.sharedMesh.triangles = tris;
            meshFilter.sharedMesh.RecalculateNormals();
            meshFilter.sharedMesh.RecalculateBounds();
            meshFilter.sharedMesh.name = "LBM_Wedge_" + transform.GetInstanceID();
#if UNITY_EDITOR
            SecondaryUV(meshFilter.sharedMesh);
            TangentSolver.Solve(meshFilter.sharedMesh);
#endif
            origUVs = meshFilter.sharedMesh.uv;
            GetComponent<MeshCollider>().sharedMesh = meshFilter.sharedMesh;
        }
    }
}
