<!-- default file list -->
*Files to look at*:

* [MainWindow.xaml](./CS/MapSearch/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/MapSearch/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/MapSearch/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/MapSearch/MainWindow.xaml.vb))
<!-- default file list end -->
# How to perform the Search operation for multiple locations


The <a href="https://documentation.devexpress.com/WPF/17463/Controls-and-Libraries/Map-Control/GIS-Data/Search">Search</a> operation is executed in an asynchronous manner and the next Search operation cannot be invoked while waiting for the result from the previous Search request. To resolve this issue, include subsequent Search method calls into the <a href="https://documentation.devexpress.com/WPF/DevExpress.Xpf.Map.BingSearchDataProvider.SearchCompleted.event">SearchCompleted</a> event handler.

<br/>


