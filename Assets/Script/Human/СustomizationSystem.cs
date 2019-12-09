using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class СustomizationSystem : MonoBehaviour
{
    [SerializeField]
    private Сustomization[] customization = new Сustomization[] { };
    [SerializeField]
    private GameObject prefab;
    [System.Serializable]
    public class Сustomization
    {
        public string name;
        public Human.HumanType type;
        public List<Mesh> head = new List<Mesh> { };
        public List<Mesh> body = new List<Mesh> { };
        public GameObject[] rightHand = new GameObject[] { };
        public GameObject[] leftHand = new GameObject[] { };
    }
    private void Awake(){
        FillUpСustomizationArray();
    }
    private void FillUpСustomizationArray()
    {
        if (prefab == null)
            return;   
        
        List<Mesh> meshsBody = new List<Mesh>();
        List<Mesh> meshsHead= new List<Mesh>();
        for(int i=0;i<prefab.GetComponentsInChildren<SkinnedMeshRenderer>().Length;i++){
            SkinnedMeshRenderer mesh = prefab.GetComponentsInChildren<SkinnedMeshRenderer>()[i];
            if(mesh.gameObject.name.Contains("Body_01")){
                meshsBody.Add(mesh.sharedMesh);
            }
        }
        for(int i=0;i<prefab.GetComponentsInChildren<MeshFilter>().Length;i++){
            MeshFilter mesh = prefab.GetComponentsInChildren<MeshFilter>()[i];
            if(mesh.gameObject.name.Contains("Head_01")){
                meshsHead.Add(mesh.sharedMesh);
            }
        }
        customization[0].body = meshsBody;     
        customization[0].head = meshsHead;
    }
    private Mesh SetHead_(Human.HumanType type)
    {
        int ArrayIndex = -1;
        for (int i = 0; i < customization.Length; i++)
        {
            if (customization[i].type == type)
            {
                ArrayIndex = i;
            }
        }
        if (ArrayIndex != -1 && customization[0].head.Count >= 1)
        {
            int index = Random.Range(0, customization[ArrayIndex].head.Count);
            for (int i = 0; i < customization[ArrayIndex].head.Count; i++)
            {
                if (i == index)
                {
                    return customization[ArrayIndex].head[i];
                }
            }
        }
        return null;
    }
    private Mesh SetBody_(Human.HumanType type)
    {
        int ArrayIndex = -1;
        for (int i = 0; i < customization.Length; i++)
        {
            if (customization[i].type == type)
            {
                ArrayIndex = i;
            }
        }
        if (ArrayIndex != -1 && customization[0].body.Count >= 1)
        {
            int index = Random.Range(0, customization[ArrayIndex].body.Count);
            for (int i = 0; i < customization[ArrayIndex].body.Count; i++)
            {
                if (i == index)
                {
                    return customization[ArrayIndex].body[i];
                }

            }
        }
        return null;
    }
    private GameObject SetLeftHand_(Human.HumanType type)
    {
        int ArrayIndex = -1;
        for (int i = 0; i < customization.Length; i++)
        {
            if (customization[i].type == type)
            {
                ArrayIndex = i;
            }
        }
        if (ArrayIndex != -1 && customization[0].leftHand.Length >= 1)
        {
            int index = Random.Range(0, customization[ArrayIndex].leftHand.Length);
            for (int i = 0; i < customization[ArrayIndex].leftHand.Length; i++)
            {
                if (i == index)
                {
                    return customization[ArrayIndex].leftHand[i];
                }

            }
        }
        return null;
    }
    private GameObject SetRightHand_(Human.HumanType type)
    {
        int ArrayIndex = -1;
        for (int i = 0; i < customization.Length; i++)
        {
            if (customization[i].type == type)
            {
                ArrayIndex = i;
            }
        }
        if (ArrayIndex != -1 && customization[0].rightHand.Length >= 1)
        {
            int index = Random.Range(0, customization[ArrayIndex].rightHand.Length);
            for (int i = 0; i < customization[ArrayIndex].rightHand.Length; i++)
            {
                if (i == index)
                {
                    return customization[ArrayIndex].rightHand[i];

                }

            }
        }
        return null;
    }


    public Mesh GetHead(Human.HumanType humanType)
    {
        return SetHead_(humanType);
    }
    public Сlothes GetBody(Human.HumanType humanType)
    {
        if(humanType==Human.HumanType.Worker){
            Сlothes clothes = new Сlothes("Worker Cloth",20,Item.PrestigeType.Cheap,SetBody_(humanType));
            return clothes;
        }
        return null;
    }
    public GameObject GetRightHand(Human.HumanType humanType)
    {
        return SetRightHand_(humanType);
    }
    public GameObject GetLeftHand(Human.HumanType humanType)
    {
        return SetLeftHand_(humanType);
    }
}
