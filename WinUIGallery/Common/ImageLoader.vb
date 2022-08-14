' To configure or remove Option's included in result, go to Options/Advanced Options...
Imports AppUIBasics.Data
Imports System
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Media.Imaging

Namespace AppUIBasics.Common
    Public NotInheritable Class ImageLoader
        Public Shared Function GetSource(obj As DependencyObject) As String
            Return CStr(obj.GetValue(SourceProperty))
        End Function
        Public Shared Sub SetSource(obj As DependencyObject, _value As String)
            obj.SetValue(SourceProperty, _value)
        End Sub
        ' Using a DependencyProperty as the backing store for Path.  This enables animation, styling, binding, etc...
        Public Shared ReadOnly SourceProperty As DependencyProperty = DependencyProperty.RegisterAttached("Source", GetType(String), GetType(ImageLoader), New PropertyMetadata(String.Empty, AddressOf OnPropertyChanged))
        Private Async Shared Sub OnPropertyChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
            Dim TempVar As Boolean = TypeOf d Is Image
            Dim image As Image = d
            If TempVar Then
                Dim item = Await ControlInfoDataSource.Instance.GetItemAsync(e.NewValue?.ToString())
                If item?.ImageIconPath IsNot Nothing Then
                    Dim imageUri As New Uri(item.ImageIconPath, UriKind.Absolute)
                    Dim imageBitmap As New BitmapImage(imageUri)
                    image.Source = imageBitmap
                End If
            End If
        End Sub
    End Class
End Namespace
