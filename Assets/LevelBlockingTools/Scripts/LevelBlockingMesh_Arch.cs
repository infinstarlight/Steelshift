using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LevelBlockingTools
{
    public class LevelBlockingMesh_Arch : LevelBlockingMesh
    {
#if UNITY_EDITOR
        public bool squared = true;
        [Range(2, 256)]
        public int sections = 6;
        [Range(0.001f, 512)]
        public float radius = 0.5f;
        [Range(0, 360)]
        public float startAngle = 0.0f;
        [Range(0, 360)]
        public float bendAngle = 180.0f;
        [Range(0.001f, 100)]
        public float width = 0.25f;
        public Vector3 offset = Vector3.zero;

        private List<Vector3> vertList = new List<Vector3>();
        private List<Vector2> uvList = new List<Vector2>();
        private List<int> triList = new List<int>();

        private Vector3[] squaringPos;
        private Vector2[] squaringUVPos;

        public override void CreateMesh()
        {
            Debug.Log("Creating Arch");
            UpdateMesh();
        }

        public override void UpdateMesh()
        {

            squaringPos = new Vector3[8];
            squaringPos[0] = new Vector3(-radius + offset.x, offset.y, offset.z);
            squaringPos[1] = new Vector3(-radius + offset.x, offset.y, radius + offset.z);
            squaringPos[2] = new Vector3(offset.x, offset.y, radius + offset.z);
            squaringPos[3] = new Vector3(radius + offset.x, offset.y, radius + offset.z);
            squaringPos[4] = new Vector3(radius + offset.x, offset.y, offset.z);
            squaringPos[5] = new Vector3(radius + offset.x, offset.y, -radius + offset.z);
            squaringPos[6] = new Vector3(offset.x, offset.y, -radius + offset.z);
            squaringPos[7] = new Vector3(-radius + offset.x, offset.y, -radius + offset.z);

            vertList.Clear();
            triList.Clear();

            //Debug.Log(sections);
            bool startCap = false;
            float angle = 0f;
            angle += startAngle;
            //float sectionSpacing = 1/sections;
            for(int i = 0; i <= sections; i++)
            {
                //unit position along the edge of the bend circle:
                Vector3 centrePos = Vector3.zero;
                centrePos.x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
                centrePos.z = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;

                Vector3 inner = -centrePos;
                inner.y = 0;
                inner.Normalize();

                centrePos += offset;

                if(squared)
                {
                    // test squaring ////////////////////////////////////////////////////
                    Vector3 testVec = new Vector3(centrePos.x, offset.y, centrePos.z);
                    float testDist = float.PositiveInfinity;
                    int sqIndex = 0;
                    for(int j = 0; j < squaringPos.Length; j++)
                    {
                        float thisDist = (testVec - squaringPos[j]).magnitude;
                        if(j == 1 || j == 3 || j == 5 || j == 7)
                        {
                            thisDist -= 0.5f * radius;
                        }

                        if(thisDist < testDist)
                        {
                            testDist = thisDist;
                            sqIndex = j;
                        }
                    }

                    // start cap
                    if(!startCap)
                    {
                        vertList.Add(new Vector3((centrePos + (inner * width)).x, offset.y, (centrePos + (inner * width)).z));
                        vertList.Add(new Vector3((centrePos + (inner * width)).x, 1 + offset.y, (centrePos + (inner * width)).z));
                        vertList.Add(new Vector3(squaringPos[sqIndex].x, 1 + squaringPos[sqIndex].y, squaringPos[sqIndex].z));
                        vertList.Add(squaringPos[sqIndex]);

                        startCap = true;
                    }

                    vertList.Add(squaringPos[sqIndex]);
                    vertList.Add(new Vector3(squaringPos[sqIndex].x, 1 + squaringPos[sqIndex].y, squaringPos[sqIndex].z));

                    vertList.Add(new Vector3(squaringPos[sqIndex].x, 1 + squaringPos[sqIndex].y, squaringPos[sqIndex].z));
                    vertList.Add(new Vector3((centrePos + (inner * width)).x, 1 + offset.y, (centrePos + (inner * width)).z));

                    vertList.Add(new Vector3((centrePos + (inner * width)).x, 1 + offset.y, (centrePos + (inner * width)).z));
                    vertList.Add(new Vector3((centrePos + (inner * width)).x, offset.y, (centrePos + (inner * width)).z));

                    vertList.Add(new Vector3((centrePos + (inner * width)).x, offset.y, (centrePos + (inner * width)).z));
                    vertList.Add(squaringPos[sqIndex]);

                    // end cap
                    if(i == sections)
                    {
                        vertList.Add(squaringPos[sqIndex]);
                        vertList.Add(new Vector3(squaringPos[sqIndex].x, 1 + squaringPos[sqIndex].y, squaringPos[sqIndex].z));
                        vertList.Add(new Vector3((centrePos + (inner * width)).x, 1 + offset.y, (centrePos + (inner * width)).z));
                        vertList.Add(new Vector3((centrePos + (inner * width)).x, offset.y, (centrePos + (inner * width)).z));
                    }
                }
                else
                {
                    // start cap
                    if(!startCap)
                    {
                        vertList.Add(new Vector3((centrePos + (inner * width)).x, offset.y, (centrePos + (inner * width)).z));
                        vertList.Add(new Vector3((centrePos + (inner * width)).x, 1 + offset.y, (centrePos + (inner * width)).z));
                        vertList.Add(new Vector3(centrePos.x, 1 + offset.y, centrePos.z));
                        vertList.Add(new Vector3(centrePos.x, offset.y, centrePos.z));
                        startCap = true;
                    }

                    vertList.Add(new Vector3(centrePos.x, offset.y, centrePos.z));
                    vertList.Add(new Vector3(centrePos.x, 1 + offset.y, centrePos.z));

                    vertList.Add(new Vector3(centrePos.x, 1 + offset.y, centrePos.z));
                    vertList.Add(new Vector3((centrePos + (inner * width)).x, 1 + offset.y, (centrePos + (inner * width)).z));

                    vertList.Add(new Vector3((centrePos + (inner * width)).x, 1 + offset.y, (centrePos + (inner * width)).z));
                    vertList.Add(new Vector3((centrePos + (inner * width)).x, offset.y, (centrePos + (inner * width)).z));

                    vertList.Add(new Vector3((centrePos + (inner * width)).x, offset.y, (centrePos + (inner * width)).z));
                    vertList.Add(new Vector3(centrePos.x, offset.y, centrePos.z));

                    // end cap
                    if(i == sections)
                    {
                        vertList.Add(new Vector3(centrePos.x, offset.y, centrePos.z));
                        vertList.Add(new Vector3(centrePos.x, 1 + offset.y, centrePos.z));
                        vertList.Add(new Vector3((centrePos + (inner * width)).x, 1 + offset.y, (centrePos + (inner * width)).z));
                        vertList.Add(new Vector3((centrePos + (inner * width)).x, offset.y, (centrePos + (inner * width)).z));
                    }
                }

                angle += (bendAngle / sections);
            }

            // set triangle array per face
            int indexUtil = sections * 8;
            for(int i = 4; i < indexUtil + 4; i += 2)
            {
                triList.Add(i);
                triList.Add(i + 1);
                triList.Add(i + 9);
                triList.Add(i);
                triList.Add(i + 9);
                triList.Add(i + 8);
            }

            // Caps
            triList.Add(0);
            triList.Add(1);
            triList.Add(2);
            triList.Add(0);
            triList.Add(2);
            triList.Add(3);
            indexUtil += 12;
            triList.Add(indexUtil);
            triList.Add(indexUtil + 1);
            triList.Add(indexUtil + 2);
            triList.Add(indexUtil);
            triList.Add(indexUtil + 2);
            triList.Add(indexUtil + 3);

            // assing data to the mesh
			DestroyImmediate(meshFilter.sharedMesh);
            meshFilter.sharedMesh = new Mesh();
            meshFilter.sharedMesh.name = "LBM_Arch_" + transform.GetInstanceID();
            meshFilter.sharedMesh.vertices = vertList.ToArray();
            meshFilter.sharedMesh.triangles = triList.ToArray();
            meshFilter.sharedMesh.uv = UpdateUVs();
            meshFilter.sharedMesh.RecalculateNormals();
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

            squaringUVPos = new Vector2[8];
            squaringUVPos[0] = new Vector2(-radius + UVPos.x, UVPos.y);
            squaringUVPos[1] = new Vector2(-radius + UVPos.x, radius + UVPos.y);
            squaringUVPos[2] = new Vector2(UVPos.x, radius + UVPos.y);
            squaringUVPos[3] = new Vector2(radius + UVPos.x, radius + UVPos.y);
            squaringUVPos[4] = new Vector2(radius + UVPos.x, UVPos.y);
            squaringUVPos[5] = new Vector2(radius + UVPos.x, -radius + UVPos.y);
            squaringUVPos[6] = new Vector2(UVPos.x, -radius + UVPos.y);
            squaringUVPos[7] = new Vector2(-radius + UVPos.x, -radius + UVPos.y);

            uvList.Clear();

            Vector3 tls = transform.localScale;
            // start cap 
            uvList.Add(new Vector2(0, 0));
            uvList.Add(new Vector2(0, tls.y * UVScale.y));
            uvList.Add(new Vector2(width * tls.x, tls.y * UVScale.y));
            uvList.Add(new Vector2(width * tls.x, 0));

            float Xinc = 1.0f / sections;
            float halfXZ = (tls.x * 0.5f) + (tls.z * 0.5f);
            //float bendAngleScale = bendAngle / 360;
            float angle = 0f;
            float bendDelta = Mathf.Lerp(0, 4, bendAngle / 360);

            for(int i = 0; i <= sections; i++)
            {
                if(squared)
                {                   
                    int sqIndex = 0;

                    //unit position along the edge of the bend circle:
                    Vector2 centrePos = Vector2.zero;
                    centrePos.x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
                    centrePos.y = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;

                    Vector2 inner = -centrePos;
                    inner.Normalize();

                    centrePos += UVPos;

                    // test squaring ////////////////////////////////////////////////////
                    float testDist = float.PositiveInfinity;
                    
                    for(int j = 0; j < squaringPos.Length; j++)
                    {
                        float thisDist = (centrePos - squaringUVPos[j]).magnitude;
                        if(j == 1 || j == 3 || j == 5 || j == 7)
                        {
                            thisDist -= 0.5f * radius;
                        }

                        if(thisDist < testDist)
                        {
                            testDist = thisDist;
                            sqIndex = j;
                        }
                    }

                    uvList.Add(new Vector2(1 * i * UVScale.x * radius, 0));
                    uvList.Add(new Vector2(1 * i * UVScale.x * radius, tls.y * UVScale.y));

                    //Debug.Log(new Vector2(vertList[i * 8 + 2].x, vertList[i * 8 + 2].z));
                    uvList.Add(new Vector2(squaringUVPos[sqIndex].x * UVScale.x, squaringUVPos[sqIndex].y * UVScale.y));
                    Vector2 secondPos = centrePos + inner * width;
                    uvList.Add(new Vector2(secondPos.x * UVScale.x, secondPos.y * UVScale.y));

                    uvList.Add(new Vector2(-Xinc * halfXZ * i * UVScale.x * bendDelta * radius, tls.y * UVScale.y));
                    uvList.Add(new Vector2(-Xinc * halfXZ * i * UVScale.x * bendDelta * radius, 0));

                    uvList.Add(new Vector2(secondPos.x * UVScale.x, secondPos.y * UVScale.y));
                    uvList.Add(new Vector2(squaringUVPos[sqIndex].x * UVScale.x, squaringUVPos[sqIndex].y * UVScale.y));

                    angle += (bendAngle / sections);
                }
                else
                {
                    uvList.Add(new Vector2(Xinc * halfXZ * i * UVScale.x * bendDelta * radius, 0));
                    uvList.Add(new Vector2(Xinc * halfXZ * i * UVScale.x * bendDelta * radius, tls.y * UVScale.y));

                    uvList.Add(new Vector2(Xinc * halfXZ * i * UVScale.x * bendDelta * radius, 0));
                    uvList.Add(new Vector2(Xinc * halfXZ * i * UVScale.x * bendDelta * radius, width * tls.x * UVScale.y));

                    uvList.Add(new Vector2(-Xinc * halfXZ * i * UVScale.x * bendDelta * radius, tls.y * UVScale.y));
                    uvList.Add(new Vector2(-Xinc * halfXZ * i * UVScale.x * bendDelta * radius, 0));

                    uvList.Add(new Vector2(-Xinc * halfXZ * i * UVScale.x * bendDelta * radius, width * tls.x * UVScale.y));
                    uvList.Add(new Vector2(-Xinc * halfXZ * i * UVScale.x * bendDelta * radius, 0));
                }
            }

            // end cap
            uvList.Add(new Vector2(0, 0));
            uvList.Add(new Vector2(0, tls.y * UVScale.y));
            uvList.Add(new Vector2(width * tls.x, tls.y * UVScale.y));
            uvList.Add(new Vector2(width * tls.x, 0));

            // apply pos
            for(int i = 0; i < uvList.Count; i++)
            {
                uvList[i] += UVPos;
            }

            return uvList.ToArray();
        }

        private void AddToVertList(Vector3 vec)
        {
            if(!vertList.Contains(vec))
            {
                vertList.Add(vec);
            }
        }

        //void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.magenta;
        //    if(squaringPos.Length > 0)
        //    {
        //        for(int i = 0; i < squaringPos.Length; i++)
        //        {
        //            Gizmos.DrawSphere(squaringPos[i], 0.05f);
        //        }
        //    }
        //}
#endif
    }
}
