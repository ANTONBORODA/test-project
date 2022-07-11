using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Controllers;
using Model.Options;
using Providers;
using UnityEngine;
using Views;

public class ThreeDView : MonoBehaviour
{
    public CarColor CarColor;
    public CarInteriorColor CarInteriorColor;

    private ConfiguratorController _controller;

    private List<Option> _currentOptions = new ();
    private readonly Dictionary<IOptionVisualizer, List<string>> _visualizers = new ();

    void Start()
    {
        _visualizers.Add(CarColor, CarColor.SupportedOptionCodes.ToList());
        _visualizers.Add(CarInteriorColor, CarInteriorColor.SupportedOptionCodes.ToList());
        _controller = ServiceLocator.Instance.GetService<ConfiguratorController>();
        _controller.PropertyChanged += ControllerOnPropertyChanged;
    }

    private void ControllerOnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "Configuration")
        {
            UpdateCarVisualization();
        }
    }

    public void UpdateCarVisualization()
    {
        var removedOptions = _currentOptions.Except(_controller.CurrentConfiguration.Options);
        foreach (var removedOption in removedOptions)
        {
            foreach (var visualizer in _visualizers.Where(t => t.Value.Contains(removedOption.Code)))
            {
                visualizer.Key.SetOptionInactive(removedOption.Code);
            }
        }

        foreach (var option in _controller.CurrentConfiguration.Options)
        {
            foreach (var visualizer in _visualizers.Where(t => t.Value.Contains(option.Code)))
            {
                visualizer.Key.SetOptionActive(option.Code);
            }
        }

        _currentOptions = _controller.CurrentConfiguration.Options.ToList();
    }
}
