using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LevelBlockingTools
{
    public class CombineMeshGroup : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
            MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();

            List<List<CombineInstance>> combine = new List<List<CombineInstance>>();
            List<Material> usedMaterials = new List<Material>();
            List<Mesh> combinedSubMeshes = new List<Mesh>();

            // find how many different shared materials are used withing the group
            for(int i = 0; i < meshRenderers.Length; i++)
            {
                if(!usedMaterials.Contains(meshRenderers[i].sharedMaterial))
                {
                    //Debug.Log("Found New Material");
                    usedMaterials.Add(meshRenderers[i].sharedMaterial);
                    combine.Add(new List<CombineInstance>());
                }
            }

            // create CombineInstances based on the shared materials found
            for(int m = 0; m < usedMaterials.Count; m++)
            {
                for(int i = 0; i < meshFilters.Length; i++)
                {
                    if(meshFilters[i].gameObject.GetComponent<Renderer>().sharedMaterial == usedMaterials[m])
                    {
                        Matrix4x4 mtx = Matrix4x4.identity;
                        mtx.SetTRS(meshFilters[i].transform.localPosition, meshFilters[i].transform.rotation, meshFilters[i].transform.localScale);

                        CombineInstance c = new CombineInstance();
                        c.mesh = meshFilters[i].sharedMesh;
                        c.transform = mtx;
                        combine[m].Add(c);
                    }
                }
            }


            // pre-combine what will become submeshes
            for(int m = 0; m < usedMaterials.Count; m++)
            {
                Mesh tempMesh = new Mesh();
                tempMesh.CombineMeshes(combine[m].ToArray());
                combinedSubMeshes.Add(tempMesh);
            }

            // the final CombineInstance
            CombineInstance[] finalCombine = new CombineInstance[usedMaterials.Count];
            for(int m = 0; m < usedMaterials.Count; m++)
            {
                finalCombine[m].mesh = combinedSubMeshes[m];
                //finalCombine[m].transform = transform.localToWorldMatrix;
            }

            MeshFilter mf = gameObject.AddComponent<MeshFilter>();
            MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();
            //		MeshCollider mc = gameObject.AddComponent<MeshCollider>();

            mf.mesh = new Mesh();
            mf.mesh.CombineMeshes(finalCombine, false, false);
            //		mc.sharedMesh = mf.mesh;
            mr.sharedMaterials = usedMaterials.ToArray();

            for(int i = 0; i < meshFilters.Length; i++)
            {
                Destroy(meshFilters[i].gameObject.GetComponent<ScaleGizmo>());
                Destroy(meshFilters[i].gameObject.GetComponent<LevelBlockingMesh>());
                Destroy(meshFilters[i].gameObject.GetComponent<Renderer>());
                Destroy(meshFilters[i]);
            }
        }
    }
}
