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

namespace OpenVoice
{
    /// <summary>
    /// Interaction logic for TriggersWindow.xaml
    /// </summary>
    public partial class TriggersWindow : Window
    {
        public TriggersWindow()
        {
            InitializeComponent();


            TriggersDataGrid.DataContext = VoiceEngine.getTriggers();
        }
    }
}
