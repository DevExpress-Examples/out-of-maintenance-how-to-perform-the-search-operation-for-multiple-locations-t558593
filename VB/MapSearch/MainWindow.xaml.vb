Imports DevExpress.Xpf.Map
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Threading

Namespace MapSearch
    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Partial Public Class MainWindow
        Inherits Window

        Private idx As Integer = 0
        Private Delegate Sub DoSearch()
        Private operation As DispatcherOperation
        Public Sub New()
            InitializeComponent()
        End Sub


        Private Sub button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            idx = 0
            operation = Application.Current.Dispatcher.BeginInvoke(CType(AddressOf SearchAsync, DoSearch))
        End Sub


        Private addresses As New List(Of String) From {"505 N. Brand Blvd, Glendale CA 91203, USA", "1111 N Brand Blvd, Glendale, CA 91202, USA", "300 N Brand Blvd, Glendale, CA 91203, USA"}
        Private Sub SearchAsync()

            If idx < addresses.Count Then
                searchProvider.Search(addresses(idx))
                idx += 1
            End If
        End Sub

        Private Sub searchProvider_SearchCompleted(ByVal sender As Object, ByVal e As BingSearchCompletedEventArgs)
            Dim result As SearchRequestResult = e.RequestResult
            If result.ResultCode = RequestResultCode.Success Then
                Dim regions As List(Of LocationInformation) = result.SearchResults
                For Each region As LocationInformation In regions
                    AddPushpin(region.Location)
                    If idx = addresses.Count Then
                        map.ZoomToFitLayerItems()
                    End If
                Next region

                DisplayResults(e.RequestResult)
                operation = Application.Current.Dispatcher.BeginInvoke(CType(AddressOf SearchAsync, DoSearch))
            End If

            If result.ResultCode = RequestResultCode.BadRequest Then
                tbResults.Text &= "The Bing Search service does not work for this location."
            End If

        End Sub

        Private Sub AddPushpin(ByVal geoPoint As GeoPoint)
            Dim pin As New MapPushpin()
            pin.Location = geoPoint
            pushpins.Items.Add(pin)
        End Sub

        Private Sub NavigateTo(ByVal geoPoint As GeoPoint)
            map.CenterPoint = geoPoint
            map.ZoomLevel = 15
        End Sub

        Private Sub DisplayResults(ByVal requestResult As SearchRequestResult)
            Dim resultList As New StringBuilder("")

            If requestResult.ResultCode = RequestResultCode.Success Then
                Dim resCounter As Integer = 1
                For Each resultInfo As LocationInformation In requestResult.SearchResults
                    resultList.Append(String.Format(vbLf & " Result {0}:  " & vbLf, resCounter))
                    resultList.Append(String.Format(resultInfo.DisplayName & vbLf))
                    resultList.Append(String.Format("Geographical coordinates:  {0}", resultInfo.Location))
                    resultList.Append(String.Format(vbLf & "______________________________" & vbLf))
                    resCounter += 1
                Next resultInfo


            End If
            tbResults.Text += resultList.ToString()
        End Sub


    End Class
End Namespace
