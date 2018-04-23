using DevExpress.Xpf.Map;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MapSearch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int idx = 0;
        delegate void DoSearch();
        DispatcherOperation operation;
        public MainWindow()
        {
            InitializeComponent();
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            idx = 0;
            operation = Application.Current.Dispatcher.BeginInvoke((DoSearch)SearchAsync);
        }


        List<string> addresses = new List<string> {"505 N. Brand Blvd, Glendale CA 91203, USA",
            "1111 N Brand Blvd, Glendale, CA 91202, USA", "300 N Brand Blvd, Glendale, CA 91203, USA" };
        void SearchAsync()
        {
            
            if (idx < addresses.Count)
                searchProvider.Search(addresses[idx++]);
        }

        private void searchProvider_SearchCompleted(object sender, BingSearchCompletedEventArgs e)
        {
            SearchRequestResult result = e.RequestResult;
            if (result.ResultCode == RequestResultCode.Success)
            {
                List<LocationInformation> regions = result.SearchResults;
                foreach (LocationInformation region in regions)
                {
                    AddPushpin(region.Location);
                    if (idx == addresses.Count)
                        map.ZoomToFitLayerItems();
                }

                DisplayResults(e.RequestResult);
                operation = Application.Current.Dispatcher.BeginInvoke((DoSearch)SearchAsync);
            }

            if (result.ResultCode == RequestResultCode.BadRequest)
                tbResults.Text += "The Bing Search service does not work for this location.";

        }

        private void AddPushpin(GeoPoint geoPoint)
        {
            MapPushpin pin = new MapPushpin();
            pin.Location = geoPoint;
            pushpins.Items.Add(pin);
        }

        void NavigateTo(GeoPoint geoPoint)
        {
            map.CenterPoint = geoPoint;
            map.ZoomLevel = 15;
        }

        private void DisplayResults(SearchRequestResult requestResult)
        {
            StringBuilder resultList = new StringBuilder("");

            if (requestResult.ResultCode == RequestResultCode.Success)
            {
                int resCounter = 1;
                foreach (LocationInformation resultInfo in requestResult.SearchResults)
                {
                    resultList.Append(String.Format("\n Result {0}:  \n", resCounter));
                    resultList.Append(String.Format(resultInfo.DisplayName + "\n"));
                    resultList.Append(String.Format("Geographical coordinates:  {0}", resultInfo.Location));
                    resultList.Append(String.Format("\n______________________________\n"));
                    resCounter++;
                }


            }
            tbResults.Text += resultList.ToString();
        }


    }
}
