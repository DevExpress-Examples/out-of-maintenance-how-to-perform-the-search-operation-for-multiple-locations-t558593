<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128571746/17.1.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T558593)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [MainWindow.xaml](./CS/MapSearch/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/MapSearch/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/MapSearch/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/MapSearch/MainWindow.xaml.vb))
<!-- default file list end -->
# How to perform the Search operation for multiple locations


The <a href="https://documentation.devexpress.com/WPF/17463/Controls-and-Libraries/Map-Control/GIS-Data/Search">Search</a> operation is executed in an asynchronous manner and the next Search operation cannot be invoked while waiting for the result from the previous Search request. To resolve this issue, include subsequent Search method calls into the <a href="https://documentation.devexpress.com/WPF/DevExpress.Xpf.Map.BingSearchDataProvider.SearchCompleted.event">SearchCompleted</a> event handler.

<br/>


