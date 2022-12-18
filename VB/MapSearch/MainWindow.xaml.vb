Imports DevExpress.Xpf.Map
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Threading

Namespace MapSearch

    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Public Partial Class MainWindow
        Inherits Window

        Private idx As Integer = 0

        Friend Delegate Sub DoSearch()

        Private operation As DispatcherOperation

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            idx = 0
            operation = Application.Current.Dispatcher.BeginInvoke(CType(AddressOf SearchAsync, DoSearch))
        End Sub

        Private addresses As List(Of String) = New List(Of String) From {"505 N. Brand Blvd, Glendale CA 91203, USA", "1111 N Brand Blvd, Glendale, CA 91202, USA", "300 N Brand Blvd, Glendale, CA 91203, USA"}

        Private Sub SearchAsync()
            If idx < addresses.Count Then Me.searchProvider.Search(addresses(Math.Min(Threading.Interlocked.Increment(idx), idx - 1)))
        End Sub

        Private Sub searchProvider_SearchCompleted(ByVal sender As Object, ByVal e As BingSearchCompletedEventArgs)
            Dim result As SearchRequestResult = e.RequestResult
            If result.ResultCode = RequestResultCode.Success Then
                Dim regions As List(Of LocationInformation) = result.SearchResults
                For Each region As LocationInformation In regions
                    AddPushpin(region.Location)
                    If idx = addresses.Count Then Me.map.ZoomToFitLayerItems()
                Next

                DisplayResults(e.RequestResult)
                operation = Application.Current.Dispatcher.BeginInvoke(CType(AddressOf SearchAsync, DoSearch))
            End If

            If result.ResultCode = RequestResultCode.BadRequest Then Me.tbResults.Text += "The Bing Search service does not work for this location."
        End Sub

        Private Sub AddPushpin(ByVal geoPoint As GeoPoint)
            Dim pin As MapPushpin = New MapPushpin()
            pin.Location = geoPoint
            Me.pushpins.Items.Add(pin)
        End Sub

        Private Sub NavigateTo(ByVal geoPoint As GeoPoint)
            Me.map.CenterPoint = geoPoint
            Me.map.ZoomLevel = 15
        End Sub

        Private Sub DisplayResults(ByVal requestResult As SearchRequestResult)
            Dim resultList As StringBuilder = New StringBuilder("")
            If requestResult.ResultCode = RequestResultCode.Success Then
                Dim resCounter As Integer = 1
                For Each resultInfo As LocationInformation In requestResult.SearchResults
                    resultList.Append(String.Format(Microsoft.VisualBasic.Constants.vbLf & " Result {0}:  " & Microsoft.VisualBasic.Constants.vbLf, resCounter))
                    resultList.Append(String.Format(resultInfo.DisplayName & Microsoft.VisualBasic.Constants.vbLf))
                    resultList.Append(String.Format("Geographical coordinates:  {0}", resultInfo.Location))
                    resultList.Append(String.Format(Microsoft.VisualBasic.Constants.vbLf & "______________________________" & Microsoft.VisualBasic.Constants.vbLf))
                    resCounter += 1
                Next
            End If

            Me.tbResults.Text += resultList.ToString()
        End Sub
    End Class
End Namespace
