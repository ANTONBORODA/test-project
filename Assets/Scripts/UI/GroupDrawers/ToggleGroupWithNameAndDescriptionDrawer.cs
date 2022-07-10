using System;
using System.Collections.Generic;
using Model.Options;
using TMPro;
using UI.OptionDrawers;
using UnityEngine;

namespace UI.GroupDrawers
{
    public class ToggleGroupWithNameAndDescriptionDrawer : MonoBehaviour, IGroupDrawer
    {
        public TextToggleDrawer TogglePrafab;
        public TMP_Text Title;
        public TMP_Text DescriptionText;

        public Transform ToggleGroupParent;
        

        public IEnumerable<IOptionDrawer> CreateOptionDrawers(OptionGroup group)
        {
            if (group.GroupType != GroupType.SingleSelect) throw new InvalidOperationException();
            if (group.PreviewType != PreviewType.NameWithDescription) throw new InvalidOperationException();

            Title.text = group.Name;
            foreach (var groupOption in group.Options)
            {
                var instance = Instantiate(TogglePrafab, ToggleGroupParent.transform);
                instance.Init(groupOption);
                instance.OptionChanged += OnOptionChanged;
                yield return instance;
            }
            
        }

        private void OnOptionChanged(Option opt, bool active)
        {
            if (active)
                DescriptionText.text = opt.Description;
        }
    }
}