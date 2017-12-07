using UnityEngine;
using System.Collections;

namespace LevelBlockingTools
{
    public class LevelBlockingMesh_WedgeCorner_Out : LevelBlockingMesh
    {
        public override void UpdateMesh()
        {
            meshFilter.sharedMesh.uv = origUVs;
            Vector2[] uvs = meshFilter.sharedMesh.uv;

            // slope
            uvs[1].y = ((transform.localScale.y + transform.localScale.x + transform.localScale.z) / 3) * UVScale.y;
            uvs[1].x = ((transform.localScale.z + transform.localScale.x) / 4) * UVScale.x;
            uvs[2].x = ((transform.localScale.z + transform.localScale.x) / 2) * UVScale.x;
            // back
            uvs[3].x = transform.localScale.x * UVScale.x * -1;
            uvs[4].y = transform.localScale.y * UVScale.y;
            // right
            uvs[7].y = transform.localScale.y * UVScale.y;
            uvs[8].x = transform.localScale.z * UVScale.x;
            // bottom
            uvs[10].y = transform.localScale.z * UVScale.y;
            uvs[11].x = transform.localScale.x * UVScale.x;

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
            Debug.Log("Creating WedgeCorner_Out");

            // initial hardcoded sizes for a wedge
            Vector3[] verts = new Vector3[12];
            Vector2[] uvs = new Vector2[12];
            int[] tris = new int[12];

            // set vert positions per face
            //		1		
            //	
            //	0		2
            //
            // slope
            verts[0] = new Vector3(0, 0, 0);
            verts[1] = new Vector3(0, 1, 1);
            verts[2] = new Vector3(1, 0, 1);
            // back
            verts[3] = new Vector3(1, 0, 1);
            verts[4] = new Vector3(0, 1, 1);
            verts[5] = new Vector3(0, 0, 1);
            // right
            verts[6] = new Vector3(0, 0, 1);
            verts[7] = new Vector3(0, 1, 1);
            verts[8] = new Vector3(0, 0, 0);
            // bottom
            verts[9] = new Vector3(0, 0, 1);
            verts[10] = new Vector3(0, 0, 0);
            verts[11] = new Vector3(1, 0, 1);

            // set uv positions per face
            //		1				
            //	
            //	0		2	
            //
            // slope
            uvs[0] = new Vector2(0, 0);
            uvs[1] = new Vector2(0.5f, 1);
            uvs[2] = new Vector2(1, 0);
            //back
            uvs[3] = new Vector2(-1, 0);
            uvs[4] = new Vector2(0, 1);
            uvs[5] = new Vector2(0, 0);
            //right
            uvs[6] = new Vector2(0, 0);
            uvs[7] = new Vector2(0, 1);
            uvs[8] = new Vector2(1, 0);
            //bottom
            uvs[9] = new Vector2(0, 0);
            uvs[10] = new Vector2(0, 1);
            uvs[11] = new Vector2(1, 0);


            // set triangle array per face
            int anchorIndex = 0;
            for(int i = 0; i < 12; i += 3)
            {
                tris[i] = anchorIndex;
                tris[i + 1] = anchorIndex + 1;
                tris[i + 2] = anchorIndex + 2;
                anchorIndex += 3;
            }

            // assing data to the mesh
            meshFilter.sharedMesh = new Mesh();
            meshFilter.sharedMesh.vertices = verts;
            meshFilter.sharedMesh.uv = uvs;
            meshFilter.sharedMesh.triangles = tris;
            meshFilter.sharedMesh.RecalculateNormals();
            meshFilter.sharedMesh.RecalculateBounds();
            meshFilter.sharedMesh.name = "LBM_WedgeCorner_Out_" + transform.GetInstanceID();
#if UNITY_EDITOR
            SecondaryUV(meshFilter.sharedMesh);
            TangentSolver.Solve(meshFilter.sharedMesh);
#endif
            origUVs = meshFilter.sharedMesh.uv;
            GetComponent<MeshCollider>().sharedMesh = meshFilter.sharedMesh;
        }
    }
}