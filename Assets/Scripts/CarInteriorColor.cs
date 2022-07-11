using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Views;

public class CarInteriorColor : MonoBehaviour, IOptionVisualizer
{
    public List<MaterialMap> IndexMaps;
    
    public List<ReflectionProbe> ReflectionProbes = new ();

    public IEnumerable<string> SupportedOptionCodes =>
        IndexMaps.Select(map => map.ColorSet).SelectMany(opt => opt.Select(t => t.Code)).ToArray();
    public void SetOptionActive(string code)
    {
        SetMaterial(code);
    }

    public void SetOptionInactive(string code)
    {
    }
    
    private void SetMaterial(string code)
    {
        if (IndexMaps.Count == 0)
            return;

        foreach (var map in IndexMaps)
        {
            var set = map.ColorSet.FirstOrDefault(t => t.Code == code);
            if (set == null) continue;
            
            var material = set.Material;

            foreach (var part in map.Parts)
            {
                var sharedMaterials = part.Part.sharedMaterials;
                foreach (var i in part.MaterialIndexes)
                {
                    sharedMaterials[i] = material;
                }

                part.Part.sharedMaterials = sharedMaterials;

            }
        }
        UpdateReflectionProbes();
    }
    
    private void UpdateReflectionProbes()
    {
        foreach (ReflectionProbe refProbe in ReflectionProbes)
        {
            refProbe.gameObject.SetActive(false);
            refProbe.gameObject.SetActive(true);
        }
    }
    
    [Serializable]
    public class MaterialMap
    {
        public List<OptionCodeMaterialMap> ColorSet;
        public List<MaterialPartsMap> Parts;
    }
    
    [Serializable]
    public class MaterialPartsMap
    {
        public MeshRenderer Part;
        public int[] MaterialIndexes;
    }
}