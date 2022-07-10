using System;
using System.Collections.Generic;
using Model.Options;
using TMPro;
using UI.OptionDrawers;
using UnityEngine;

namespace UI.GroupDrawers
{
    public class ToggleGroupWithPreviewDrawer : MonoBehaviour, IGroupDrawer
    {
        public PreviewToggleDrawer TogglePrefab;
        public TMP_Text Title;

        public Transform ToggleGroupParent;
        
        public IEnumerable<IOptionDrawer> CreateOptionDrawers(OptionGroup group)
        {
            if (group.PreviewType != PreviewType.Image) throw new InvalidOperationException();

            Title.text = group.Name;
            foreach (var groupOption in group.Options)
            {
                var instance = Instantiate(TogglePrefab, ToggleGroupParent.transform);
                instance.Init(groupOption);
                yield return instance;
            }
        }
    }
}