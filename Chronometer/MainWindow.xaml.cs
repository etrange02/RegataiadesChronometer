using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chronometer
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ((INotifyCollectionChanged)Departures.Items).CollectionChanged += ListBox_CollectionChanged;
            ((INotifyCollectionChanged)Arrivals.Items).CollectionChanged += ListBox_CollectionChanged;
        }

        private void ListBox_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            EnsureLastVisible(Departures);
            EnsureLastVisible(Arrivals);
        }

        private void EnsureLastVisible(ListBox list)
        {
            if (list.Items.Count == 0) return;
            list.ScrollIntoView(list.Items[list.Items.Count - 1]);
        }
    }
}
