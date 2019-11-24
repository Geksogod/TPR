﻿using UnityEngine;

public class СustomizationSystem : MonoBehaviour
{
    [SerializeField]
    private Сustomization[] customization = new Сustomization[] { };
    [System.Serializable]
    public class Сustomization
    {
        public string name;
        public Human.HumanType type;
        public GameObject[] head = new GameObject[] { };
        public GameObject[] body = new GameObject[] { };
        public GameObject[] rightHand = new GameObject[] { };
        public GameObject[] leftHand = new GameObject[] { };
    }

    private Mesh SetHead(Human.HumanType type)
    {
        int ArrayIndex = -1;
        for (int i = 0; i < customization.Length; i++)
        {
            if (customization[i].type == type)
            {
                ArrayIndex = i;
            }
        }
        if (ArrayIndex != -1 && customization[0].head.Length >= 1)
        {
            int index = Random.Range(0, customization[ArrayIndex].head.Length);
            for (int i = 0; i < customization[ArrayIndex].head.Length; i++)
            {
                if (i == index)
                {
                    if (customization[ArrayIndex].head[i].GetComponent<SkinnedMeshRenderer>() != null)
                        return customization[ArrayIndex].head[i].GetComponent<SkinnedMeshRenderer>().sharedMesh;
                    else
                        return customization[ArrayIndex].head[i].GetComponent<MeshFilter>().sharedMesh;
                }

            }
        }
        return null;
    }
    private GameObject SetHead_(Human.HumanType type)
    {
        int ArrayIndex = -1;
        for (int i = 0; i < customization.Length; i++)
        {
            if (customization[i].type == type)
            {
                ArrayIndex = i;
            }
        }
        if (ArrayIndex != -1 && customization[0].head.Length >= 1)
        {
            int index = Random.Range(0, customization[ArrayIndex].head.Length);
            for (int i = 0; i < customization[ArrayIndex].head.Length; i++)
            {
                if (i == index)
                {
                    return customization[ArrayIndex].head[i];
                }
            }
        }
        return null;
    }

    private Mesh SetBody(Human.HumanType type)
    {
        int ArrayIndex = -1;
        for (int i = 0; i < customization.Length; i++)
        {
            if (customization[i].type == type)
            {
                ArrayIndex = i;
            }
        }
        if (ArrayIndex != -1 && customization[0].body.Length >= 1)
        {
            int index = Random.Range(0, customization[ArrayIndex].body.Length);
            for (int i = 0; i < customization[ArrayIndex].body.Length; i++)
            {
                if (i == index)
                {
                    if (customization[ArrayIndex].body[i].GetComponent<SkinnedMeshRenderer>() != null)
                        return customization[ArrayIndex].body[i].GetComponent<SkinnedMeshRenderer>().sharedMesh;
                    else
                        return customization[ArrayIndex].body[i].GetComponent<MeshFilter>().sharedMesh;
                }

            }
        }
        return null;
    }
    private GameObject SetBody_(Human.HumanType type)
    {
        int ArrayIndex = -1;
        for (int i = 0; i < customization.Length; i++)
        {
            if (customization[i].type == type)
            {
                ArrayIndex = i;
            }
        }
        if (ArrayIndex != -1 && customization[0].body.Length >= 1)
        {
            int index = Random.Range(0, customization[ArrayIndex].body.Length);
            for (int i = 0; i < customization[ArrayIndex].body.Length; i++)
            {
                if (i == index)
                {
                    return customization[ArrayIndex].body[i];
                }

            }
        }
        return null;
    }
    private Mesh SetLeftHand(Human.HumanType type)
    {
        int ArrayIndex = -1;
        Mesh leftHandMesh = null;
        Material leftHandMaterial = null;
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
                    if (customization[ArrayIndex].leftHand[i].GetComponent<SkinnedMeshRenderer>() != null)
                    {
                        leftHandMesh = customization[ArrayIndex].leftHand[i].GetComponent<SkinnedMeshRenderer>().sharedMesh;
                        leftHandMaterial = customization[ArrayIndex].leftHand[i].GetComponent<SkinnedMeshRenderer>().material;
                    }
                    else if (customization[ArrayIndex].leftHand[i].GetComponent<MeshFilter>() != null && customization[ArrayIndex].leftHand[i].GetComponent<MeshRenderer>())
                    {
                        leftHandMesh = customization[ArrayIndex].leftHand[i].GetComponent<MeshFilter>().sharedMesh;
                        leftHandMaterial = customization[ArrayIndex].leftHand[i].GetComponent<MeshRenderer>().material;
                    }

                    if (leftHandMesh != null)
                        return leftHandMesh;
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
    private Mesh SetRightHand(Human.HumanType type)
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
                    if (customization[ArrayIndex].rightHand[i].GetComponent<SkinnedMeshRenderer>() != null)
                        return customization[ArrayIndex].rightHand[i].GetComponent<SkinnedMeshRenderer>().sharedMesh;
                    else
                        return customization[ArrayIndex].rightHand[i].GetComponent<MeshFilter>().sharedMesh;

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


    /// <summary>
    /// Customization you charecter
    /// </summary>
    public void Customization(ref GameObject head, ref GameObject body, ref GameObject leftHand, ref GameObject rightHand, Human.HumanType humanType)
    {
        if (head != null)
        {
            if (head.GetComponent<SkinnedMeshRenderer>() != null)
                head.GetComponent<SkinnedMeshRenderer>().sharedMesh = GameObject.Find("СustomizationSystem").GetComponent<СustomizationSystem>().SetHead(humanType);
            else if (head.GetComponent<MeshFilter>() != null)
                head.GetComponent<MeshFilter>().sharedMesh = GameObject.Find("СustomizationSystem").GetComponent<СustomizationSystem>().SetHead(humanType);
        }
        if (body != null)
        {
            if (body.GetComponent<SkinnedMeshRenderer>() != null)
                body.GetComponent<SkinnedMeshRenderer>().sharedMesh = GameObject.Find("СustomizationSystem").GetComponent<СustomizationSystem>().SetBody(humanType);
            else if (body.GetComponent<MeshFilter>() != null)
                body.GetComponent<MeshFilter>().sharedMesh = GameObject.Find("СustomizationSystem").GetComponent<СustomizationSystem>().SetBody(humanType);
        }
        if (leftHand != null)
        {
            if (leftHand.GetComponent<SkinnedMeshRenderer>() != null)
                leftHand.GetComponent<SkinnedMeshRenderer>().sharedMesh = GameObject.Find("СustomizationSystem").GetComponent<СustomizationSystem>().SetLeftHand(humanType);
            else if (leftHand.GetComponent<MeshFilter>() != null)
            {
                leftHand.GetComponent<MeshFilter>().sharedMesh = GameObject.Find("СustomizationSystem").GetComponent<СustomizationSystem>().SetLeftHand(humanType);
            }
            Debug.Log("Left Hand");
        }
        if (rightHand != null)
        {
            if (rightHand.GetComponent<SkinnedMeshRenderer>() != null)
                rightHand.GetComponent<SkinnedMeshRenderer>().sharedMesh = GameObject.Find("СustomizationSystem").GetComponent<СustomizationSystem>().SetRightHand(humanType);
            else if (rightHand.GetComponent<MeshFilter>() != null)
                rightHand.GetComponent<MeshFilter>().sharedMesh = GameObject.Find("СustomizationSystem").GetComponent<СustomizationSystem>().SetRightHand(humanType);
        }
    }
    /// <summary>
    /// Customization you charecter with one hand
    /// </summary>
    public void Customization(ref GameObject head, ref GameObject body, ref GameObject Hand, Human.HumanType humanType)
    {
        if (head != null)
        {
            if (head.GetComponent<SkinnedMeshRenderer>() != null)
                head.GetComponent<SkinnedMeshRenderer>().sharedMesh = GameObject.Find("СustomizationSystem").GetComponent<СustomizationSystem>().SetHead(humanType);
            else if (head.GetComponent<MeshFilter>() != null)
                head.GetComponent<MeshFilter>().sharedMesh = GameObject.Find("СustomizationSystem").GetComponent<СustomizationSystem>().SetHead(humanType);
        }
        if (body != null)
        {
            if (body.GetComponent<SkinnedMeshRenderer>() != null)
                body.GetComponent<SkinnedMeshRenderer>().sharedMesh = GameObject.Find("СustomizationSystem").GetComponent<СustomizationSystem>().SetBody(humanType);
            else if (body.GetComponent<MeshFilter>() != null)
                body.GetComponent<MeshFilter>().sharedMesh = GameObject.Find("СustomizationSystem").GetComponent<СustomizationSystem>().SetBody(humanType);
        }
        if (Hand != null)
        {
            if (Hand.GetComponent<SkinnedMeshRenderer>() != null)
                Hand.GetComponent<SkinnedMeshRenderer>().sharedMesh = GameObject.Find("СustomizationSystem").GetComponent<СustomizationSystem>().SetRightHand(humanType);
            else if (Hand.GetComponent<MeshFilter>() != null)
                Hand.GetComponent<MeshFilter>().sharedMesh = GameObject.Find("СustomizationSystem").GetComponent<СustomizationSystem>().SetRightHand(humanType);
        }
    }
    public GameObject GetHead(Human.HumanType humanType)
    {
        return SetHead_(humanType);
    }
    public GameObject GetBody(Human.HumanType humanType)
    {
        return SetBody_(humanType);
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
