' To configure or remove Option's included in result, go to Options/Advanced Options...
Imports System.Collections.Generic
Imports System.Diagnostics.CodeAnalysis
Imports System.Globalization
Imports System.Linq
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading.Tasks
Imports Windows.Foundation
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports System.Runtime.CompilerServices

Namespace AppUIBasics
    ''' <summary>
    ''' Positions child elements sequentially from left to right or top to
    ''' bottom.  When elements extend beyond the panel edge, elements are
    ''' positioned in the next row or column.
    ''' </summary>
    ''' <QualityBand>Mature</QualityBand>
    Public Class WrapPanel
        Inherits Panel
        ''' <summary>
        ''' A value indicating whether a dependency property change handler
        ''' should ignore the next change notification.  This is used to reset
        ''' the value of properties without performing any of the actions in
        ''' their change handlers.
        ''' </summary>
        Private _ignorePropertyChange As Boolean
#Region "public double ItemHeight"
        ''' <summary>
        ''' Gets or sets the height of the layout area for each item that is
        ''' contained in a <see cref="T:WinRTXamlToolkit.Controls.WrapPanel"/> .
        ''' </summary>
        ''' <value>
        ''' The height applied to the layout area of each item that is contained
        ''' within a <see cref="T:WinRTXamlToolkit.Controls.WrapPanel"/> .  The
        ''' default value is <see cref="F:System.Double.NaN"/> .
        ''' </value>
        '[TypeConverter(typeof(LengthConverter))]
        Public Property ItemHeight As Double
            Get
                Return CDbl(GetValue(ItemHeightProperty))
            End Get

            Set(value As Double)
                SetValue(ItemHeightProperty, value)
            End Set
        End Property
        ''' <summary>
        ''' Identifies the
        ''' <see cref="P:WinRTXamlToolkit.Controls.WrapPanel.ItemHeight"/>
        ''' dependency property.
        ''' </summary>
        ''' <value>
        ''' The identifier for the
        ''' <see cref="P:WinRTXamlToolkit.Controls.WrapPanel.ItemHeight"/>
        ''' dependency property
        ''' </value>
        Public Shared ReadOnly ItemHeightProperty As DependencyProperty = DependencyProperty.Register( _
                "ItemHeight",
GetType(Double),
GetType(WrapPanel),
                New PropertyMetadata(Double.NaN, AddressOf OnItemHeightOrWidthPropertyChanged))
#End Region  ' public double ItemHeight
#Region "public double ItemWidth"
        ''' <summary>
        ''' Gets or sets the width of the layout area for each item that is
        ''' contained in a <see cref="T:WinRTXamlToolkit.Controls.WrapPanel"/> .
        ''' </summary>
        ''' <value>
        ''' The width that applies to the layout area of each item that is
        ''' contained in a <see cref="T:WinRTXamlToolkit.Controls.WrapPanel"/> .
        ''' The default value is <see cref="F:System.Double.NaN"/> .
        ''' </value>
        '[TypeConverter(typeof(LengthConverter))]
        Public Property ItemWidth As Double
            Get
                Return CDbl(GetValue(ItemWidthProperty))
            End Get

            Set(value As Double)
                SetValue(ItemWidthProperty, value)
            End Set
        End Property
        ''' <summary>
        ''' Identifies the
        ''' <see cref="P:WinRTXamlToolkit.Controls.WrapPanel.ItemWidth"/>
        ''' dependency property.
        ''' </summary>
        ''' <value>
        ''' The identifier for the
        ''' <see cref="P:WinRTXamlToolkit.Controls.WrapPanel.ItemWidth"/>
        ''' dependency property.
        ''' </value>
        Public Shared ReadOnly ItemWidthProperty As DependencyProperty = DependencyProperty.Register( _
                "ItemWidth",
GetType(Double),
GetType(WrapPanel),
                New PropertyMetadata(Double.NaN, AddressOf OnItemHeightOrWidthPropertyChanged))
#End Region  ' public double ItemWidth
#Region "public Orientation Orientation"
        ''' <summary>
        ''' Gets or sets the direction in which child elements are arranged.
        ''' </summary>
        ''' <value>
        ''' One of the <see cref="T:Microsoft.UI.Xaml.Controls.Orientation"/> 
        ''' values.  The default is
        ''' <see cref="F:Microsoft.UI.Xaml.Controls.Orientation.Horizontal"/> .
        ''' </value>
        Public Property Orientation As Orientation
            Get
                Return CType(GetValue(OrientationProperty), Orientation)
            End Get

            Set(value As Orientation)
                SetValue(OrientationProperty, value)
            End Set
        End Property
        ''' <summary>
        ''' Identifies the
        ''' <see cref="P:WinRTXamlToolkit.Controls.WrapPanel.Orientation"/>
        ''' dependency property.
        ''' </summary>
        ''' <value>
        ''' The identifier for the
        ''' <see cref="P:WinRTXamlToolkit.Controls.WrapPanel.Orientation"/>
        ''' dependency property.
        ''' </value>
        Public Shared ReadOnly OrientationProperty As DependencyProperty = DependencyProperty.Register( _
                "Orientation",
GetType(Orientation),
GetType(WrapPanel),
                New PropertyMetadata(Orientation.Horizontal, AddressOf OnOrientationPropertyChanged))
        ''' <summary>
        ''' OrientationProperty property changed handler.
        ''' </summary>
        ''' <param name="d">WrapPanel that changed its Orientation.</param>
        ''' <param name="e">Event arguments.</param>
        <SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification:="Almost always set from the CLR property.")>
        Private Shared Sub OnOrientationPropertyChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
            Dim source As WrapPanel = CType(d, WrapPanel)
            Dim value As Orientation = CType(e.NewValue, Orientation)

            ' Ignore the change if requested
            If source._ignorePropertyChange Then
                source._ignorePropertyChange = False
                Return
            End If

            ' Validate the Orientation
            If (value <> Orientation.Horizontal) AndAlso _
(value <> Orientation.Vertical) Then
                ' Reset the property to its original state before throwing
                source._ignorePropertyChange = True
                source.SetValue(OrientationProperty, CType(e.OldValue, Orientation))

                Dim message As String = String.Format( _
                    CultureInfo.InvariantCulture,
                    "Properties.Resources.WrapPanel_OnOrientationPropertyChanged_InvalidValue",
                    value)
                Throw New ArgumentException(message, "value")
            End If

            ' Orientation affects measuring.
            source.InvalidateMeasure()
        End Sub
#End Region
 ' public Orientation Orientation

        ''' <summary>
        ''' Initializes a new instance of the
        ''' <see cref="T:WinRTXamlToolkit.Controls.WrapPanel"/> class.
        ''' </summary>
        Public Sub New()
        End Sub
        ''' <summary>
        ''' Property changed handler for ItemHeight and ItemWidth.
        ''' </summary>
        ''' <param name="d">
        ''' WrapPanel that changed its ItemHeight or ItemWidth.
        ''' </param>
        ''' <param name="e">Event arguments.</param>
        <SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification:="Almost always set from the CLR property.")>
        Private Shared Sub OnItemHeightOrWidthPropertyChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
            Dim source As WrapPanel = CType(d, WrapPanel)
            Dim value As Double = CDbl(e.NewValue)

            ' Ignore the change if requested
            If source._ignorePropertyChange Then
                source._ignorePropertyChange = False
                Return
            End If

            ' Validate the length (which must either be NaN or a positive,
            ' finite number)
            If Not Double.IsNaN(value) AndAlso ((value <= 0.0) OrElse Double.IsPositiveInfinity(value)) Then
                ' Reset the property to its original state before throwing
                source._ignorePropertyChange = True
                source.SetValue(e.[Property], CDbl(e.OldValue))

                Dim message As String = String.Format( _
                    CultureInfo.InvariantCulture,
                    "Properties.Resources.WrapPanel_OnItemHeightOrWidthPropertyChanged_InvalidValue",
                    value)
                Throw New ArgumentException(message, "value")
            End If

            ' The length properties affect measuring.
            source.InvalidateMeasure()
        End Sub
        ''' <summary>
        ''' Measures the child elements of a
        ''' <see cref="T:WinRTXamlToolkit.Controls.WrapPanel"/> in anticipation
        ''' of arranging them during the
        ''' <see cref="Microsoft.UI.Xaml.FrameworkElement.ArrangeOverride(Windows.Foundation.Size)"/>
        ''' pass.
        ''' </summary>
        ''' <param name="constraint">
        ''' The size available to child elements of the wrap panel.
        ''' </param>
        ''' <returns>
        ''' The size required by the
        ''' <see cref="T:WinRTXamlToolkit.Controls.WrapPanel"/> and its 
        ''' elements.
        ''' </returns>
        <SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", MessageId:="0#", Justification:="Compat with WPF.")>
        Protected Overrides Function MeasureOverride(constraint As Size) As Size
            ' Variables tracking the size of the current line, the total size
            ' measured so far, and the maximum size available to fill.  Note
            ' that the line might represent a row or a column depending on the
            ' orientation.
            Dim o As Orientation = Orientation
            Dim lineSize As New OrientedSize(o)
            Dim totalSize As New OrientedSize(o)
            Dim maximumSize As New OrientedSize(o, constraint.Width, constraint.Height)

            ' Determine the constraints for individual items
            Dim itemWidth1 As Double = ItemWidth
            Dim itemHeight1 As Double = ItemHeight
            Dim hasFixedWidth As Boolean = Not Double.IsNaN(itemWidth1)
            Dim hasFixedHeight As Boolean = Not Double.IsNaN(itemHeight1)
            Dim itemSize As New Size( _
                If(hasFixedWidth, itemWidth1, constraint.Width),
                If(hasFixedHeight, itemHeight1, constraint.Height))

            ' Measure each of the Children
            For Each element As UIElement In Children
                ' Determine the size of the element
                element.Measure(itemSize)
                Dim elementSize As New OrientedSize( _
                    o,
                    If(hasFixedWidth, itemWidth1, element.DesiredSize.Width),
                    If(hasFixedHeight, itemHeight1, element.DesiredSize.Height))

                ' If this element falls of the edge of the line
                If NumericExtensions.IsGreaterThan(lineSize.Direct + elementSize.Direct, maximumSize.Direct) Then
                    ' Update the total size with the direct and indirect growth
                    ' for the current line
                    totalSize.Direct = Math.Max(lineSize.Direct, totalSize.Direct)
                    totalSize.Indirect += lineSize.Indirect

                    ' Move the element to a new line
                    lineSize = elementSize

                    ' If the current element is larger than the maximum size,
                    ' place it on a line by itself
                    If NumericExtensions.IsGreaterThan(elementSize.Direct, maximumSize.Direct) Then
                        ' Update the total size for the line occupied by this
                        ' single element
                        totalSize.Direct = Math.Max(elementSize.Direct, totalSize.Direct)
                        totalSize.Indirect += elementSize.Indirect

                        ' Move to a new line
                        lineSize = New OrientedSize(o)
                    End If
                Else
                    ' Otherwise just add the element to the end of the line
                    lineSize.Direct += elementSize.Direct
                    lineSize.Indirect = Math.Max(lineSize.Indirect, elementSize.Indirect)
                End If
            Next

            ' Update the total size with the elements on the last line
            totalSize.Direct = Math.Max(lineSize.Direct, totalSize.Direct)
            totalSize.Indirect += lineSize.Indirect

            ' Return the total size required as an un-oriented quantity
            Return New Size(totalSize.Width, totalSize.Height)
        End Function
        ''' <summary>
        ''' Arranges and sizes the
        ''' <see cref="T:WinRTXamlToolkit.Controls.WrapPanel"/> control and its
        ''' child elements.
        ''' </summary>
        ''' <param name="finalSize">
        ''' The area within the parent that the
        ''' <see cref="T:WinRTXamlToolkit.Controls.WrapPanel"/> should use 
        ''' arrange itself and its children.
        ''' </param>
        ''' <returns>
        ''' The actual size used by the
        ''' <see cref="T:WinRTXamlToolkit.Controls.WrapPanel"/>.
        ''' </returns>
        Protected Overrides Function ArrangeOverride(finalSize As Size) As Size
            ' Variables tracking the size of the current line, and the maximum
            ' size available to fill.  Note that the line might represent a row
            ' or a column depending on the orientation.
            Dim o As Orientation = Orientation
            Dim lineSize As New OrientedSize(o)
            Dim maximumSize As New OrientedSize(o, finalSize.Width, finalSize.Height)

            ' Determine the constraints for individual items
            Dim itemWidth1 As Double = ItemWidth
            Dim itemHeight1 As Double = ItemHeight
            Dim hasFixedWidth As Boolean = Not Double.IsNaN(itemWidth1)
            Dim hasFixedHeight As Boolean = Not Double.IsNaN(itemHeight1)
            Dim indirectOffset As Double = 0
            Dim directDelta As Double? = If((o = Orientation.Horizontal),
                (If(hasFixedWidth, CType(itemWidth1, Double?), Nothing)),
                (If(hasFixedHeight, CType(itemHeight1, Double?), Nothing)))

            ' Measure each of the Children.  We will process the elements one
            ' line at a time, just like during measure, but we will wait until
            ' we've completed an entire line of elements before arranging them.
            ' The lineStart and lineEnd variables track the size of the
            ' currently arranged line.
            Dim children1 As UIElementCollection = Children
            Dim count1 As Integer = children1.Count
            Dim lineStart As Integer = 0
            For lineEnd As Integer = 0 To count1 - 1
                Dim element As UIElement = children1(lineEnd)

                ' Get the size of the element
                Dim elementSize As New OrientedSize( _
                    o,
                    If(hasFixedWidth, itemWidth1, element.DesiredSize.Width),
                    If(hasFixedHeight, itemHeight1, element.DesiredSize.Height))

                ' If this element falls of the edge of the line
                If NumericExtensions.IsGreaterThan(lineSize.Direct + elementSize.Direct, maximumSize.Direct) Then
                    ' Then we just completed a line and we should arrange it
                    ArrangeLine(lineStart, lineEnd, directDelta, indirectOffset, lineSize.Indirect)

                    ' Move the current element to a new line
                    indirectOffset += lineSize.Indirect
                    lineSize = elementSize

                    ' If the current element is larger than the maximum size
                    If NumericExtensions.IsGreaterThan(elementSize.Direct, maximumSize.Direct) Then
                        ' Arrange the element as a single line
                        ArrangeLine(lineEnd, Threading.Interlocked.Increment(lineEnd), directDelta, indirectOffset, elementSize.Indirect)

                        ' Move to a new line
                        indirectOffset += lineSize.Indirect
                        lineSize = New OrientedSize(o)
                    End If

                    ' Advance the start index to a new line after arranging
                    lineStart = lineEnd
                Else
                    ' Otherwise just add the element to the end of the line
                    lineSize.Direct += elementSize.Direct
                    lineSize.Indirect = Math.Max(lineSize.Indirect, elementSize.Indirect)
                End If
            Next

            ' Arrange any elements on the last line
            If lineStart < count1 Then
                ArrangeLine(lineStart, count1, directDelta, indirectOffset, lineSize.Indirect)
            End If

            Return finalSize
        End Function
        ''' <summary>
        ''' Arrange a sequence of elements in a single line.
        ''' </summary>
        ''' <param name="lineStart">
        ''' Index of the first element in the sequence to arrange.
        ''' </param>
        ''' <param name="lineEnd">
        ''' Index of the last element in the sequence to arrange.
        ''' </param>
        ''' <param name="directDelta">
        ''' Optional fixed growth in the primary direction.
        ''' </param>
        ''' <param name="indirectOffset">
        ''' Offset of the line in the indirect direction.
        ''' </param>
        ''' <param name="indirectGrowth">
        ''' Shared indirect growth of the elements on this line.
        ''' </param>
        Private Sub ArrangeLine(lineStart As Integer, lineEnd As Integer, directDelta As Double?, indirectOffset As Double, indirectGrowth As Double)
            Dim directOffset As Double = 0.0

            Dim o As Orientation = Orientation
            Dim isHorizontal As Boolean = o = Orientation.Horizontal

            Dim children1 As UIElementCollection = Children
            For index As Integer = lineStart To lineEnd - 1
                ' Get the size of the element
                Dim element As UIElement = children1(index)
                Dim elementSize As New OrientedSize(o, element.DesiredSize.Width, element.DesiredSize.Height)

                ' Determine if we should use the element's desired size or the
                ' fixed item width or height
                Dim directGrowth As Double = If(directDelta IsNot Nothing,
                    directDelta.Value,
                    elementSize.Direct)

                ' Arrange the element
                Dim bounds As Rect = If(isHorizontal,
                    New Rect(directOffset, indirectOffset, directGrowth, indirectGrowth),
                    New Rect(indirectOffset, directOffset, indirectGrowth, directGrowth))
                element.Arrange(bounds)

                directOffset += directGrowth
            Next
        End Sub
    End Class

    ''' <summary>
    ''' Numeric utility methods used by controls.  These methods are similar in
    ''' scope to the WPF DoubleUtil class.
    ''' </summary>
    Friend Module NumericExtensions
        ''' <summary>
        ''' NanUnion is a C++ style type union used for efficiently converting
        ''' a double into an unsigned long, whose bits can be easily
        ''' manipulated.
        ''' </summary>
        <StructLayout(LayoutKind.Explicit)>
        Private Structure NanUnion
            ''' <summary>
            ''' Floating point representation of the union.
            ''' </summary>
            <SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification:="It is accessed through the other member of the union")>
            <FieldOffset(0)>
            Friend FloatingValue As Double
            ''' <summary>
            ''' Integer representation of the union.
            ''' </summary>
            <FieldOffset(0)>
            Friend IntegerValue As ULong
        End Structure
#If Not WINDOWS_PHONE

        ''' <summary>
        ''' Check if a number is zero.
        ''' </summary>
        ''' <param name="value">The number to check.</param>
        ''' <returns>True if the number is zero, false otherwise.</returns>
        <Extension()>
        Public Function IsZero(_value As Double) As Boolean
            ' We actually consider anything within an order of magnitude of
            ' epsilon to be zero
            Return Math.Abs(_value) < 2.2204460492503131E-15
        End Function
#End If

        ''' <summary>
        ''' Check if a number isn't really a number.
        ''' </summary>
        ''' <param name="value">The number to check.</param>
        ''' <returns>
        ''' True if the number is not a number, false if it is a number.
        ''' </returns>
        <Extension()>
        Public Function IsNaN(_value As Double) As Boolean
            ' Get the double as an unsigned long
            Dim union As New NanUnion With
{
                .FloatingValue = _value}

            ' An IEEE 754 double precision floating point number is NaN if its
            ' exponent equals 2047 and it has a non-zero mantissa.
            Dim exponent As ULong = union.IntegerValue And &Hfff0000000000000UL
            If (exponent <> &H7ff0000000000000) AndAlso (exponent <> &Hfff0000000000000UL) Then
                Return False
            End If
            Dim mantissa As ULong = union.IntegerValue And &H000fffffffffffff
            Return mantissa <> 0L
        End Function
        ''' <summary>
        ''' Determine if one number is greater than another.
        ''' </summary>
        ''' <param name="left">First number.</param>
        ''' <param name="right">Second number.</param>
        ''' <returns>
        ''' True if the first number is greater than the second, false
        ''' otherwise.
        ''' </returns>
        Public Function IsGreaterThan(left As Double, right As Double) As Boolean
            Return (left > right) AndAlso Not AreClose(left, right)
        End Function
        ''' <summary>
        ''' Determine if two numbers are close in value.
        ''' </summary>
        ''' <param name="left">First number.</param>
        ''' <param name="right">Second number.</param>
        ''' <returns>
        ''' True if the first number is close in value to the second, false
        ''' otherwise.
        ''' </returns>
        Public Function AreClose(left As Double, right As Double) As Boolean
            ' ReSharper disable CompareOfFloatsByEqualityOperator
            If left = right Then
                ' ReSharper restore CompareOfFloatsByEqualityOperator
                Return True
            End If

            Dim a As Double = (Math.Abs(left) + Math.Abs(right) + 10.0) * 2.2204460492503131E-16
            Dim b As Double = left - right
            Return (-a < b) AndAlso (a > b)
        End Function
#If Not WINDOWS_PHONE

        ''' <summary>
        ''' Determine if one number is less than or close to another.
        ''' </summary>
        ''' <param name="left">First number.</param>
        ''' <param name="right">Second number.</param>
        ''' <returns>
        ''' True if the first number is less than or close to the second, false
        ''' otherwise.
        ''' </returns>
        Public Function IsLessThanOrClose(left As Double, right As Double) As Boolean
            Return (left < right) OrElse AreClose(left, right)
        End Function
    End Module
#End If

End Namespace
