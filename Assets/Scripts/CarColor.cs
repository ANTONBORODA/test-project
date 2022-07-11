using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Views;

public class CarColor : MonoBehaviour, IOptionVisualizer {

    public List<GameObject> colouredBodyWorkParts = new ();

    public List<OptionCodeMaterialMap> mColourSet = new();

    public List<ReflectionProbe> reflectionProbes = new ();

    private int currentColourIndex = 0;

    public IEnumerable<string> SupportedOptionCodes => mColourSet.Select(t => t.Code);
    
    public void SetOptionActive(string code)
    {
        var map = mColourSet.FirstOrDefault(t => t.Code == code);
        if (map == null)
        {
            Debug.LogError($"Color with code {code} not found");
            return;
        }
        SetPartMaterial(map.Material);
        currentColourIndex = mColourSet.IndexOf(map);
    }

    public void SetOptionInactive(string code)
    {
    }

    public void SetColourByIndex(int index) {
        if (mColourSet.Count == 0) {
            Debug.LogWarning("Empty body Colour sets.");
            return;
        }

        if (index >= mColourSet.Count) {
            Debug.LogError("Out of index.");
            return;
        }

        SetPartMaterial(mColourSet[index].Material);
    }

    [ContextMenu("Next")]
    public void SetNextColour()
    {
        if (mColourSet.Count == 0)
            return;

        if (currentColourIndex < mColourSet.Count - 1)
        {
            currentColourIndex++;
        }
        else if (currentColourIndex == mColourSet.Count - 1)
        {
            currentColourIndex = 0;
        }

        SetPartMaterial(mColourSet[currentColourIndex].Material);
    }


    bool IsCarPaint(Material mat)
    {
        if (mColourSet.Any(t => t.Material == mat))
        {
            return true;
        }
        return false;
    }


    private void SetPartColour(Color lColour)
    {
        if (colouredBodyWorkParts.Count == 0)
            return;

        foreach (GameObject bodyPart in colouredBodyWorkParts)
        {
            MeshRenderer[] lMeshRenderers = bodyPart.GetComponentsInChildren<MeshRenderer>();

            foreach (MeshRenderer lMeshRenderer in lMeshRenderers)
            {
                if (lMeshRenderer != null)
                {
                    foreach (Material mat in lMeshRenderer.sharedMaterials)
                    {
                        if (IsCarPaint(mat))
                            mat.SetColor("_BaseColor", lColour);
                    }
                }
            }
        }
    }


    private void UpdateReflectionProbes()
    {
        foreach (ReflectionProbe refProbe in reflectionProbes)
        {
            refProbe.gameObject.SetActive(false);
            refProbe.gameObject.SetActive(true);
        }
    }


    private void SetPartMaterial(Material lMaterial)
    {
        if (colouredBodyWorkParts.Count == 0)
            return;

        foreach (GameObject bodyPart in colouredBodyWorkParts)
        {
            MeshRenderer[] lMeshRenderers = bodyPart.GetComponentsInChildren<MeshRenderer>();

            foreach (MeshRenderer lMeshRenderer in lMeshRenderers)
            {
                if (lMeshRenderer != null)
                {
                    Material[] lSharedMaterials = lMeshRenderer.sharedMaterials;
                    bool update = false;
                    for (int i = 0; i < lSharedMaterials.Length; i++)
                    {
                        if (IsCarPaint(lSharedMaterials[i]))
                        {
                            lSharedMaterials[i] = lMaterial;
                            update = true;
                        }
                    }
                    if (update) lMeshRenderer.sharedMaterials = lSharedMaterials;
                }
            }
        }
        UpdateReflectionProbes();
    }
    
}