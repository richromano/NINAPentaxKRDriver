using NINA.Core.Utility.Extensions;
using NINA.Equipment.Equipment.MyCamera;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Rtg.NINA.NinaPentaxKRDriver.NinaPentaxKRDriverDockables {
    [Export(typeof(ResourceDictionary))]
    public partial class MyPluginDockableTemplates : ResourceDictionary {
        public MyPluginDockableTemplates() {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            // Handle selection change
            var listBox = sender as ListBox;
            string selectedItem = listBox?.SelectedItem.ToString();
            Rtg.NINA.NinaPentaxKRDriver.NinaPentaxKRDriverDockables.NinaPentaxKRDriverDockable.SelectedItem=selectedItem;
//            MessageBox.Show($"Selected Item: {selectedItem}");
        }
        private void ZoomBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            // Handle selection change
            var listBox = sender as ListBox;
            string selectedItem = listBox?.SelectedItem.ToString();
            Rtg.NINA.NinaPentaxKRDriver.NinaPentaxKRDriverDockables.NinaPentaxKRDriverDockable.SelectedZoomItem = selectedItem;
            //            MessageBox.Show($"Selected Item: {selectedItem}");
        }
    }
}