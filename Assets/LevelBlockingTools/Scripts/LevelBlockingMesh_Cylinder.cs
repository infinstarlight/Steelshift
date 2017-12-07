using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LevelBlockingTools
{
    [RequireComponent(typeof(MeshCollider))]
    public class LevelBlockingMesh_Cylinder : LevelBlockingMesh
    {
#if UNITY_EDITOR
        [Range(3, 512)]
        public int sections = 12;
        [Range(0.001f, 512)]
        public float radius = 0.5f;
        public Vector3 offset = new Vector3(0, 0, 0);

        private List<Vector3> vertList = new List<Vector3>();
        private List<Vector2> uvList = new List<Vector2>();
        private List<int> triList = new List<int>();

        public override void CreateMesh()
        {
            Debug.Log("Creating Cylinder");
            UpdateMesh();
        }

        public override void UpdateMesh()
        {
            vertList.Clear();
            triList.Clear();

            Vector3 centrePos = Vector3.zero;
            float angle = 0f;
            for(int i = 0; i <= sections; i++)
            {
                //unit position along the edge of the bend circle:
                centrePos = Vector3.zero;
                centrePos.x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
                centrePos.z = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;

                centrePos += offset;

                vertList.Add(new Vector3(centrePos.x, offset.y, centrePos.z));
                vertList.Add(new Vector3(centrePos.x, 1 + offset.y, centrePos.z));

                angle += (360.0f / sections);
            }

            int vertZero = vertList.Count - 2;
            int vertOne = vertList.Count - 1;

            // set triangle array per face
            int indexUtil = sections * 2 - 2;
            for(int i = 0; i <= indexUtil; i += 2)
            {
                triList.Add(i);
                triList.Add(i + 1);
                triList.Add(i + 3);
                triList.Add(i);
                triList.Add(i + 3);
                triList.Add(i + 2);
            }

            // Caps
            // top cap		
            for(int i = 1; i < sections * 2; i += 2)
            {
                vertList.Add(vertList[i]);
            }

            vertList.Add(new Vector3(offset.x, 1 + offset.y, offset.z));
            int capCenterVert = vertList.Count - 1;

            indexUtil = sections * 2 + 2;
            for(int i = indexUtil; i < indexUtil + sections; i++)
            {
                if(i < indexUtil + sections - 1)
                {
                    triList.Add(i);
                    triList.Add(capCenterVert);
                    triList.Add(i + 1);
                }
                else
                {
                    triList.Add(i);
                    triList.Add(capCenterVert);
                    triList.Add(indexUtil);
                }
            }

            // bottom cap	
            for(int i = 0; i < sections * 2; i += 2)
            {
                vertList.Add(vertList[i]);
            }

            vertList.Add(new Vector3(offset.x, offset.y, offset.z));
            capCenterVert = vertList.Count - 1;

            indexUtil = sections * 2 + sections + 3;
            for(int i = indexUtil; i < indexUtil + sections; i++)
            {
                if(i < indexUtil + sections - 1)
                {
                    triList.Add(capCenterVert);
                    triList.Add(i);
                    triList.Add(i + 1);
                }
                else
                {
                    triList.Add(capCenterVert);
                    triList.Add(i);
                    triList.Add(indexUtil);
                }
            }

            // assing data to the mesh
			DestroyImmediate(meshFilter.sharedMesh);
            meshFilter.sharedMesh = new Mesh();
            meshFilter.sharedMesh.name = "LBM_Cylinder_" + transform.GetInstanceID();
            meshFilter.sharedMesh.vertices = vertList.ToArray();
            meshFilter.sharedMesh.triangles = triList.ToArray();
            meshFilter.sharedMesh.uv = UpdateUVs();

            meshFilter.sharedMesh.RecalculateNormals();
            Vector3[] normals = meshFilter.sharedMesh.normals;
            normals[0] = normals[vertZero] = (normals[0] + normals[vertZero]).normalized;
            normals[1] = normals[vertOne] = (normals[1] + normals[vertOne]).normalized;
            meshFilter.sharedMesh.normals = normals;
            meshFilter.sharedMesh.RecalculateBounds();
#if UNITY_EDITOR
            SecondaryUV(meshFilter.sharedMesh);
            TangentSolver.Solve(meshFilter.sharedMesh);
            lastScale = transform.localScale;
            lastUVScale = UVScale;
            lastUVPos = UVPos;
#endif
            GetComponent<MeshCollider>().sharedMesh = meshFilter.sharedMesh;
        }

        public Vector2[] UpdateUVs()
        {
            uvList.Clear();

            Vector3 tls = transform.localScale;

            float Xinc = 1.0f / sections;
            float halfXZ = (tls.x * 0.5f) + (tls.z * 0.5f);
            //float bendAngleScale = bendAngle / 360;
            for(int i = 0; i <= sections; i++)
            {
                uvList.Add(new Vector2(Xinc * halfXZ * i * UVScale.x * 4 * radius, 0));
                uvList.Add(new Vector2(Xinc * halfXZ * i * UVScale.x * 4 * radius, tls.y * UVScale.y));
            }

            // top cap
            Vector2 centrePos = Vector2.zero;
            float angle = 0f;
            for(int i = 0; i < sections; i++)
            {
                //unit position along the edge of the bend circle:
                centrePos = Vector2.zero;
                centrePos.x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius * transform.localScale.x;
                centrePos.y = Mathf.Sin(Mathf.Deg2Rad * angle) * radius * transform.localScale.z;

                //centrePos += new Vector2(0.5f,0.5f);

                uvList.Add(new Vector2(centrePos.x, centrePos.y));

                angle += (360.0f / sections);
            }

            uvList.Add(Vector2.zero);

            // bottom cap
            angle = 0f;
            for(int i = 0; i < sections; i++)
            {
                //unit position along the edge of the bend circle:
                centrePos = Vector2.zero;
                centrePos.x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius * transform.localScale.x;
                centrePos.y = Mathf.Sin(Mathf.Deg2Rad * angle) * radius * transform.localScale.z;

                //centrePos += new Vector2(0.5f,0.5f);

                uvList.Add(new Vector2(centrePos.x, centrePos.y));

                angle += (360.0f / sections);
            }

            uvList.Add(Vector2.zero);

            // apply pos
            for(int i = 0; i < uvList.Count; i++)
            {
                uvList[i] += UVPos;
            }

            //		Debug.Log("v = " + sharedMesh.vertices.Length + " uv = " + uvList.Count);

            return uvList.ToArray();
        }

        //void OnDrawGizmos()
        //{
        //    //for(int i = 0; i < sharedMesh.normals.Length; i++)
        //    //{
        //    //    Debug.DrawRay(transform.position + sharedMesh.vertices[i], sharedMesh.normals[i], Color.red);
        //    //}

        //    Gizmos.color = Color.magenta;
        //    for(int i = 0; i < meshFilter.sharedMesh.vertexCount; i++)
        //    {
        //        Gizmos.DrawSphere(meshFilter.sharedMesh.vertices[i], 0.025f);
        //    }
        //}
#endif
    }
}
