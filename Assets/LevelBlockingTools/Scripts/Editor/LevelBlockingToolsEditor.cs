using UnityEngine;
using UnityEditor;
using System.Collections;

namespace LevelBlockingTools
{
    public class LevelBlockingToolsEditor : Editor
    {
        static Vector3 IntoViewPos()
        {
            Camera cam = SceneView.lastActiveSceneView.camera;
            Vector3 pos = cam.transform.position;
            Vector3 dir = cam.transform.forward;

            float distance = 10;
            RaycastHit hit;
            if(Physics.Raycast(pos, dir, out hit, 10))
            {
                distance = hit.distance - 0.5f;
            }

            return RoundValues(pos + dir * distance);
        }

        static Vector3 RoundValues(Vector3 v)
        {
            v.x = Mathf.Round(v.x);
            v.y = Mathf.Round(v.y);
            v.z = Mathf.Round(v.z);
            return v;
        }

        [MenuItem("LevelBlockingMeshes/Quad", false, 10)]
        static void CreateQuad()
        {
            GameObject go = new GameObject("LBM_Quad");
            go.AddComponent<MeshCollider>();
            LevelBlockingMesh_Quad lbm = go.AddComponent<LevelBlockingMesh_Quad>();
            lbm.CreateMesh();
            go.AddComponent<ScaleGizmo>();
            go.GetComponent<Renderer>().material = Resources.Load<Material>("UtilityGridMat");
            go.transform.position = IntoViewPos();
        }

        [MenuItem("LevelBlockingMeshes/Cube", false, 10)]
        static void CreateCube()
        {
            GameObject go = new GameObject("LBM_Cube");
            BoxCollider bc = go.AddComponent<BoxCollider>();
            bc.center = Vector3.one * 0.5f;
            LevelBlockingMesh_Cube lbm = go.AddComponent<LevelBlockingMesh_Cube>();
            lbm.CreateMesh();
            go.AddComponent<ScaleGizmo>();
            go.GetComponent<Renderer>().material = Resources.Load<Material>("UtilityGridMat");
            go.transform.position = IntoViewPos();
        }

        [MenuItem("LevelBlockingMeshes/Wedges/Wedge", false, 10)]
        static void CreateWedge()
        {
            GameObject go = new GameObject("LBM_Wedge");
            go.AddComponent<MeshCollider>();
            LevelBlockingMesh_Wedge lbm = go.AddComponent<LevelBlockingMesh_Wedge>();
            lbm.CreateMesh();
            go.AddComponent<ScaleGizmo>();
            go.GetComponent<Renderer>().material = Resources.Load<Material>("UtilityGridMat");
            go.transform.position = IntoViewPos();
        }

        [MenuItem("LevelBlockingMeshes/Wedges/WedgeCorner_Out", false, 10)]
        static void CreateWedgeCorner_Out()
        {
            GameObject go = new GameObject("LBM_WedgeCorner_Out");
            go.AddComponent<MeshCollider>();
            LevelBlockingMesh_WedgeCorner_Out lbm = go.AddComponent<LevelBlockingMesh_WedgeCorner_Out>();
            lbm.CreateMesh();
            go.AddComponent<ScaleGizmo>();
            go.GetComponent<Renderer>().material = Resources.Load<Material>("UtilityGridMat");
            go.transform.position = IntoViewPos();
        }

        [MenuItem("LevelBlockingMeshes/Wedges/WedgeCorner_In", false, 10)]
        static void CreateWedgeCorner_In()
        {
            GameObject go = new GameObject("LBM_WedgeCorner_In");
            go.AddComponent<MeshCollider>();
            LevelBlockingMesh_WedgeCorner_In lbm = go.AddComponent<LevelBlockingMesh_WedgeCorner_In>();
            lbm.CreateMesh();
            go.AddComponent<ScaleGizmo>();
            go.GetComponent<Renderer>().material = Resources.Load<Material>("UtilityGridMat");
            go.transform.position = IntoViewPos();
        }

        [MenuItem("LevelBlockingMeshes/Piramid", false, 10)]
        static void CreatePiramid()
        {
            GameObject go = new GameObject("LBM_Piramid");
            go.AddComponent<MeshCollider>();
            LevelBlockingMesh_Piramid lbm = go.AddComponent<LevelBlockingMesh_Piramid>();
            lbm.CreateMesh();
            go.AddComponent<ScaleGizmo>();
            go.GetComponent<Renderer>().material = Resources.Load<Material>("UtilityGridMat");
            go.transform.position = IntoViewPos();
        }

        [MenuItem("LevelBlockingMeshes/Slope", false, 10)]
        static void CreateSlope()
        {
            GameObject go = new GameObject("LBM_Slope");
            go.AddComponent<MeshCollider>();
            LevelBlockingMesh_Slope lbm = go.AddComponent<LevelBlockingMesh_Slope>();
            lbm.CreateMesh();
            go.GetComponent<Renderer>().material = Resources.Load<Material>("UtilityGridMat");
            go.transform.position = IntoViewPos();
        }

        [MenuItem("LevelBlockingMeshes/Cylinder", false, 10)]
        static void CreateCylinder()
        {
            GameObject go = new GameObject("LBM_Cylinder");
            go.AddComponent<MeshCollider>();
            LevelBlockingMesh_Cylinder lbm = go.AddComponent<LevelBlockingMesh_Cylinder>();
            lbm.CreateMesh();
            go.GetComponent<Renderer>().material = Resources.Load<Material>("UtilityGridMat");
            go.transform.position = IntoViewPos();
        }

        [MenuItem("LevelBlockingMeshes/Arch", false, 10)]
        static void CreateArch()
        {
            GameObject go = new GameObject("LBM_Arch");
            go.AddComponent<MeshCollider>();
            LevelBlockingMesh_Arch lbm = go.AddComponent<LevelBlockingMesh_Arch>();
            lbm.CreateMesh();
            go.GetComponent<Renderer>().material = Resources.Load<Material>("UtilityGridMat");
            go.transform.position = IntoViewPos();
        }

        [MenuItem("LevelBlockingMeshes/Group")]
        static void CreateMeshGroup()
        {
            //int idx = FindObjectsOfType<CombineMeshGroup>().Length;

            GameObject go = new GameObject("MeshGroup_");
            //go.AddComponent<CombineMeshGroup>();
            go.transform.position = Vector3.zero;
            foreach(Transform t in Selection.transforms)
            {
                t.parent = go.transform;
            }
            Selection.activeGameObject = go;
            //go.isStatic = true;
            //EditorApplication.ExecuteMenuItem("GameObject/Center On Children");
        }

        //	[MenuItem("LevelBlockingMeshes/Mesh Combine Group", true)]
        //	static bool ValidateCreateMeshCombineGroup()
        //	{
        //		bool result = false;
        //		foreach(GameObject go in Selection.gameObjects)
        //		{
        //			if( go.GetComponent<LevelBlockingMesh>() == null )
        //			{
        //				result = false;
        //				break;
        //			}
        //			else
        //			{
        //				result = true;
        //			}
        //		}
        //		return result;
        //	}

        [MenuItem("LevelBlockingMeshes/Round Transform Values")]
        static void RoundTransformValues()
        {
            foreach(Transform t in Selection.transforms)
            {
                t.position = RoundValues(t.position);
                t.localScale = RoundValues(t.localScale);
                t.eulerAngles = RoundValues(t.eulerAngles);
            }
        }

		[MenuItem("LevelBlockingMeshes/Round Pos-Rot Values")]
		static void RoundPosRotValues()
		{
			foreach(Transform t in Selection.transforms)
			{
				t.position = RoundValues(t.position);
				t.eulerAngles = RoundValues(t.eulerAngles);
			}
		}

        [MenuItem("LevelBlockingMeshes/Refresh Mesh")]
        static void RefreshMesh()
        {
            foreach(GameObject go in Selection.gameObjects)
            {
                LevelBlockingMesh lbm = go.GetComponent<LevelBlockingMesh>();
                if(lbm)
                {
                    //lbm.UVScale = Vector2.one;
                    //lbm.UVPos = Vector2.zero;
					DestroyImmediate(lbm.meshFilter.sharedMesh);
                    lbm.CreateMesh();
                    lbm.UpdateMesh();
                }
            }
        }
    }
}
