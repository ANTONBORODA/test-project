using System;
using System.Collections.Generic;
using System.IO;
using Model.Options.Extensions;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace Model.Options.Editor
{
    /// <summary>
    /// This is simple code-based generator for data. This can be extended to full featured GUI configuration builder or pulled
    /// from database if required
    /// </summary>
    public class ConfigurationGenerator : EditorWindow
    {
        private List<Option> _options = new();
        private List<OptionGroup> _groups = new();
        private List<OptionRelation> _relations = new();


        [MenuItem("Window/Configuration Generator")]
        public static void Init()
        {
            var window = (ConfigurationGenerator)EditorWindow.GetWindow(typeof(ConfigurationGenerator));
            window.CreateConfiguration();
            window.Show();
        }
        
        private void OnGUI()
        {
            if (GUILayout.Button("Save configuration"))
            {
                var path = EditorUtility.SaveFilePanel("Save serialized options asset", "Assets", "configuration",
                    "json");
                try
                {
                    var configuration = JsonConvert.SerializeObject(
                        new ConfigurationData(_options.ToArray(), _groups.ToArray(), _relations.ToArray()),
                        Formatting.Indented,
                        new JsonSerializerSettings {PreserveReferencesHandling = PreserveReferencesHandling.Objects});
                    File.WriteAllText(path, configuration);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                    throw;
                }
            }
        }
        
        private void CreateConfiguration()
        {
            CreateOptions();
            CreateGroups();
            CreateRelations();
        }

        private void CreateRelations()
        {
            var momentumRelations = new List<OptionRelation>()
            {
                // Engines
                new()
                {
                    RelationType = RelationType.Stock,
                    This = _options.GetOption("EDMOM"),
                    To = _options.GetOption("PTT3MF"),
                },
                new()
                {
                    RelationType = RelationType.Stock,
                    This = _options.GetOption("EDMOM"),
                    To = _options.GetOption("PTT3AF"),
                },

                // Colors
                new()
                {
                    RelationType = RelationType.Stock,
                    This = _options.GetOption("EDMOM"),
                    To = _options.GetOption("ECBS"),
                },
                new()
                {
                    RelationType = RelationType.Stock,
                    This = _options.GetOption("EDMOM"),
                    To = _options.GetOption("ECIW"),
                },
                new()
                {
                    RelationType = RelationType.Optional,
                    This = _options.GetOption("EDMOM"),
                    To = _options.GetOption("ECGS"),
                },
                
                // Upholstery
                new()
                {
                    RelationType = RelationType.Stock,
                    This = _options.GetOption("EDMOM"),
                    To = _options.GetOption("ICBL"),
                },
                new()
                {
                    RelationType = RelationType.Stock,
                    This = _options.GetOption("EDMOM"),
                    To = _options.GetOption("ICWH"),
                },
                
                // Packages
                new()
                {
                    RelationType = RelationType.Optional,
                    This = _options.GetOption("EDMOM"),
                    To = _options.GetOption("APW"),
                },
                new()
                {
                    RelationType = RelationType.Optional,
                    This = _options.GetOption("EDMOM"),
                    To = _options.GetOption("APP"),
                },
            };
            
            var inscriptionRelations = new List<OptionRelation>()
            {
                // Engines
                new ()
                {
                    RelationType = RelationType.Stock,
                    This = _options.GetOption("EDINS"),
                    To = _options.GetOption("PTT3AF"),
                },
                new ()
                {
                    RelationType = RelationType.Stock,
                    This = _options.GetOption("EDINS"),
                    To = _options.GetOption("PTB4AF"),
                },
                new ()
                {
                    RelationType = RelationType.Stock,
                    This = _options.GetOption("EDINS"),
                    To = _options.GetOption("PTB4AA"),
                },
                new ()
                {
                    RelationType = RelationType.Stock,
                    This = _options.GetOption("EDINS"),
                    To = _options.GetOption("PTB5AA"),
                },

                // Colors
                new ()
                {
                    RelationType = RelationType.Stock,
                    This = _options.GetOption("EDINS"),
                    To = _options.GetOption("ECBS"),
                },
                new ()
                {
                    RelationType = RelationType.Stock,
                    This = _options.GetOption("EDINS"),
                    To = _options.GetOption("ECIW"),
                },
                new ()
                {
                    RelationType = RelationType.Optional,
                    This = _options.GetOption("EDINS"),
                    To = _options.GetOption("ECGS"),
                },
                new ()
                {
                    RelationType = RelationType.Optional,
                    This = _options.GetOption("EDINS"),
                    To = _options.GetOption("ECDB"),
                },
                new ()
                {
                    RelationType = RelationType.Optional,
                    This = _options.GetOption("EDINS"),
                    To = _options.GetOption("ECFR"),
                },
                
                // Upholstery
                new()
                {
                    RelationType = RelationType.Stock,
                    This = _options.GetOption("EDINS"),
                    To = _options.GetOption("ICBL"),
                },
                new()
                {
                    RelationType = RelationType.Stock,
                    This = _options.GetOption("EDINS"),
                    To = _options.GetOption("ICWH"),
                },
                
                // Packages
                new()
                {
                    RelationType = RelationType.Optional,
                    This = _options.GetOption("EDINS"),
                    To = _options.GetOption("APW"),
                },
                new()
                {
                    RelationType = RelationType.Optional,
                    This = _options.GetOption("EDINS"),
                    To = _options.GetOption("APP"),
                },
                new ()
                {
                    RelationType = RelationType.Optional,
                    This = _options.GetOption("EDINS"),
                    To = _options.GetOption("APT"),
                },
                new ()
                {
                    RelationType = RelationType.Optional,
                    This = _options.GetOption("EDINS"),
                    To = _options.GetOption("APL"),
                },
            };
            
            
            var rdesignRelations = new List<OptionRelation>()
            {
                // Engines
                new ()
                {
                    RelationType = RelationType.Stock,
                    This = _options.GetOption("EDRDE"),
                    To = _options.GetOption("PTT3AF"),
                },
                new ()
                {
                    RelationType = RelationType.Stock,
                    This = _options.GetOption("EDRDE"),
                    To = _options.GetOption("PTB4AF"),
                },
                new ()
                {
                    RelationType = RelationType.Stock,
                    This = _options.GetOption("EDRDE"),
                    To = _options.GetOption("PTB4AA"),
                },
                new ()
                {
                    RelationType = RelationType.Stock,
                    This = _options.GetOption("EDRDE"),
                    To = _options.GetOption("PTB5AA"),
                },

                // Colors
                new ()
                {
                    RelationType = RelationType.Stock,
                    This = _options.GetOption("EDRDE"),
                    To = _options.GetOption("ECBS"),
                },
                new ()
                {
                    RelationType = RelationType.Stock,
                    This = _options.GetOption("EDRDE"),
                    To = _options.GetOption("ECIW"),
                },
                new ()
                {
                    RelationType = RelationType.Optional,
                    This = _options.GetOption("EDRDE"),
                    To = _options.GetOption("ECGS"),
                },
                new ()
                {
                    RelationType = RelationType.Optional,
                    This = _options.GetOption("EDRDE"),
                    To = _options.GetOption("ECDB"),
                },
                new ()
                {
                    RelationType = RelationType.Optional,
                    This = _options.GetOption("EDRDE"),
                    To = _options.GetOption("ECFR"),
                },
                new ()
                {
                    RelationType = RelationType.Optional,
                    This = _options.GetOption("EDRDE"),
                    To = _options.GetOption("ECIG"),
                },
                
                // Upholstery
                new()
                {
                    RelationType = RelationType.Stock,
                    This = _options.GetOption("EDRDE"),
                    To = _options.GetOption("ICBL"),
                },
                new()
                {
                    RelationType = RelationType.Stock,
                    This = _options.GetOption("EDRDE"),
                    To = _options.GetOption("ICWH"),
                },
                
                // Packages
                new()
                {
                    RelationType = RelationType.Optional,
                    This = _options.GetOption("EDRDE"),
                    To = _options.GetOption("APW"),
                },
                new()
                {
                    RelationType = RelationType.Optional,
                    This = _options.GetOption("EDRDE"),
                    To = _options.GetOption("APP"),
                },
                new ()
                {
                    RelationType = RelationType.Optional,
                    This = _options.GetOption("EDRDE"),
                    To = _options.GetOption("APT"),
                },
                new ()
                {
                    RelationType = RelationType.Optional,
                    This = _options.GetOption("EDRDE"),
                    To = _options.GetOption("APL"),
                },
            };
            _relations.AddRange(momentumRelations);
            _relations.AddRange(inscriptionRelations);
            _relations.AddRange(rdesignRelations);
        }

        private void CreateGroups()
        {
            _groups.Add(new OptionGroup()
            {
                Name = "Package",
                PreviewType = PreviewType.NameWithDescription,
                GroupType = GroupType.SingleSelect,
                Options = new []
                {
                    _options.GetOption("EDMOM"), 
                    _options.GetOption("EDINS"), 
                    _options.GetOption("EDRDE"),
                }
            });
            _groups.Add(new OptionGroup()
            {
                Name = "Engine",
                PreviewType = PreviewType.Name,
                GroupType = GroupType.SingleSelect,
                Options = new [] 
                { 
                    _options.GetOption("PTT3MF"), 
                    _options.GetOption("PTT3AF"), 
                    _options.GetOption("PTB4AF"),
                    _options.GetOption("PTB4AA"),
                    _options.GetOption("PTB5AA"),
                }
            });
            _groups.Add(new OptionGroup()
            {
                Name = "Colors",
                PreviewType = PreviewType.Image,
                GroupType = GroupType.SingleSelect,
                Options = new [] 
                { 
                    _options.GetOption("ECBS"), 
                    _options.GetOption("ECIW"),
                    _options.GetOption("ECGS"),
                    _options.GetOption("ECDB"),
                    _options.GetOption("ECFR"),
                    _options.GetOption("ECIG"),
                }
            });
            _groups.Add(new OptionGroup()
            {
                Name = "Upholstery",
                PreviewType = PreviewType.Name,
                GroupType = GroupType.SingleSelect,
                Options = new [] 
                { 
                    _options.GetOption("ICBL"), 
                    _options.GetOption("ICWH"),
                }
            });
            _groups.Add(new OptionGroup()
            {
                Name = "Additional packages",
                PreviewType = PreviewType.Name,
                GroupType = GroupType.MultiSelect,
                Options = new [] 
                { 
                    _options.GetOption("APL"), 
                    _options.GetOption("APP"),
                    _options.GetOption("APT"),
                    _options.GetOption("APW"),
                }
            });
        }

        private void CreateOptions()
        {
            // Models
            _options.Add(new Option()
            {
                Code = "EDMOM",
                Name = "Momentum",
                Description = "Comfort that is the hallmark of an inspiring urban SUV. Contrasting exterior details and interior with premium textile upholstery."
            });
            _options.Add(new Option()
            {
                Code = "EDINS",
                Name = "Inscription",
                Description = "The most sophisticated XC40 model. Chrome exterior details, unique wheels and exclusive leather seats create absolute Scandinavian luxury."
            });
            _options.Add(new Option()
            {
                Code = "EDRDE",
                Name = "R-Design",
                Description = "Built for active driving. With gloss black details, unique wheels and a stylishly finished interior."
            });
            
            // Engines
            _options.Add(new Option()
            {
                Code = "PTT3MF",
                Name = "T3 manual (160 HP)",
            });
            _options.Add(new Option()
            {
                Code = "PTT3AF",
                Name = "T3 automatic (160 HP)",
            });
            _options.Add(new Option()
            {
                Code = "PTB4AF",
                Name = "B4 mild hybrid (190 HP)",
            });
            _options.Add(new Option()
            {
                Code = "PTB4AA",
                Name = "B4 AWD mild hybrid (190 HP)",
            });
            _options.Add(new Option()
            {
                Code = "PTB5AA",
                Name = "B5 AWD mild hybrid (250 HP)",
            });
            
            // Colors
            _options.Add(new Option()
            {
                Code = "ECBS",
                Name = "Black Stone",
            });
            _options.Add(new Option()
            {
                Code = "ECIW",
                Name = "Ice White",
            });
            _options.Add(new Option()
            {
                Code = "ECGS",
                Name = "Glacier Silver",
            });
            _options.Add(new Option()
            {
                Code = "ECDB",
                Name = "Denim Blue",
            });
            _options.Add(new Option()
            {
                Code = "ECFR",
                Name = "Fusion Red",
            });
            _options.Add(new Option()
            {
                Code = "ECIG",
                Name = "Its Green",
            });


            // Upholstery
            _options.Add(new Option()
            {
                Code = "ICBL",
                Name = "Black",
            });
            _options.Add(new Option()
            {
                Code = "ICWH",
                Name = "White",
            });
            
            // Packages
            _options.Add(new Option()
            {
                Code = "APL",
                Name = "Lighting",
            });
            _options.Add(new Option()
            {
                Code = "APP",
                Name = "Parking",
            });
            _options.Add(new Option()
            {
                Code = "APT",
                Name = "Technology",
            });
            _options.Add(new Option()
            {
                Code = "APW",
                Name = "Winter",
            });
        }
        
    }
}