using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RouteOptimization.WpfApp.View.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для VehicleStatusEditorDialogView.xaml
    /// </summary>
    public partial class VehicleStatusEditorDialogView : Window
    {
        public VehicleStatusEditorDialogView()
        {
            InitializeComponent();
        }

        void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
