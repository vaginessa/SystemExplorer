﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using SystemExplorer.Core;
using SystemExplorer.Modules.Computer.ViewModels;
using SystemExplorer.Modules.Computer.Views;

namespace SystemExplorer.Modules.Computer {
    [Module("Computer", Author = "Pavel Yosifovich")]
    [Export(typeof(IModule))]
    public sealed class Module : IModule {
        [Import]
        ISystemExplorer Explorer;

        public bool Init() {
            var root = Explorer.Container.GetExportedValue<ComputerTreeViewItem>();
            var envVar = Explorer.Container.GetExportedValue<EnvironmentVariablesTreeViewItem>();
            root.Items.Add(envVar);
            root.IsExpanded = true;

            Explorer.AddTreeViewItem(root, this);
            var resources = new ResourceDictionary { Source = Helpers.ToPackUri(Assembly.GetExecutingAssembly(), "/resources.xaml") };
            Explorer.AddResourceDictionary(resources, this);

            return true;
        }
    }
}
