using System.Windows;
using System.Windows.Controls;

namespace FirstMenuCommand
{
    /// <summary>
    /// Interaction logic for ViewPreview.xaml
    /// </summary>
    public partial class ViewPreview : Window
    {
        public ViewPreview()
        {
            InitializeComponent();
        }

        private void Browser_OnLoaded(object sender, RoutedEventArgs e)
        {
            var browser = (WebBrowser) sender;
            browser.Navigate(
                "file:///C:/Dev/GitHub/QuantDoc/Docs/build/html/writing-docs.html#equation-math-example-formula");
        }
    }
}
