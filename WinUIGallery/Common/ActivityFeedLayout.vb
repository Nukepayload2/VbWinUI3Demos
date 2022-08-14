' To configure or remove Option's included in result, go to Options/Advanced Options...
Imports System.Collections.Generic
Imports Windows.Foundation
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls

Imports LayoutContext = Microsoft.UI.Xaml.Controls.LayoutContext
Imports VirtualizingLayout = Microsoft.UI.Xaml.Controls.VirtualizingLayout
Imports VirtualizingLayoutContext = Microsoft.UI.Xaml.Controls.VirtualizingLayoutContext

Namespace AppUIBasics.Common
    Class ActivityFeedLayout
        Inherits VirtualizingLayout
#Region "Layout parameters"

        ' We'll cache copies of the dependency properties to avoid calling GetValue during layout since that
        ' can be quite expensive due to the number of times we'd end up calling these.
        Private _rowSpacing As Double
        Private _colSpacing As Double
        Private _minItemSize As Size = Size.Empty
        ''' <summary>
        ''' Gets or sets the size of the whitespace gutter to include between rows
        ''' </summary>
        Public Property RowSpacing As Double
            Get
                Return _rowSpacing
            End Get

            Set(value As Double)
                SetValue(RowSpacingProperty, value)
            End Set
        End Property
        Public Shared ReadOnly RowSpacingProperty As DependencyProperty = DependencyProperty.Register( _
                "RowSpacing",
GetType(Double),
GetType(ActivityFeedLayout),
                New PropertyMetadata(0, AddressOf OnPropertyChanged))
        ''' <summary>
        ''' Gets or sets the size of the whitespace gutter to include between items on the same row
        ''' </summary>
        Public Property ColumnSpacing As Double
            Get
                Return _colSpacing
            End Get

            Set(value As Double)
                SetValue(ColumnSpacingProperty, value)
            End Set
        End Property
        Public Shared ReadOnly ColumnSpacingProperty As DependencyProperty = DependencyProperty.Register( _
                "ColumnSpacing",
GetType(Double),
GetType(ActivityFeedLayout),
                New PropertyMetadata(0, AddressOf OnPropertyChanged))
        Public Property MinItemSize As Size
            Get
                Return _minItemSize
            End Get

            Set(value As Size)
                SetValue(MinItemSizeProperty, value)
            End Set
        End Property
        Public Shared ReadOnly MinItemSizeProperty As DependencyProperty = DependencyProperty.Register( _
                "MinItemSize",
GetType(Size),
GetType(ActivityFeedLayout),
                New PropertyMetadata(Size.Empty, AddressOf OnPropertyChanged))
        Private Shared Sub OnPropertyChanged(obj As DependencyObject, args As DependencyPropertyChangedEventArgs)
            Dim layout As AppUIBasics.Common.ActivityFeedLayout = TryCast(obj, ActivityFeedLayout)
            If args.[Property] = RowSpacingProperty Then
                layout._rowSpacing = CDbl(args.NewValue)
            ElseIf args.[Property] = ColumnSpacingProperty Then
                layout._colSpacing = CDbl(args.NewValue)
            ElseIf args.[Property] = MinItemSizeProperty Then
                layout._minItemSize = CType(args.NewValue, Size)
            Else
                Throw New InvalidOperationException("Don't know what you are talking about!")
            End If

            layout.InvalidateMeasure()
        End Sub
#End Region

#Region "Setup / teardown"

        Protected Overrides Sub InitializeForContextCore(context As VirtualizingLayoutContext)
            MyBase.InitializeForContextCore(context)
            Dim TempVar As Boolean = TypeOf context.LayoutState Is ActivityFeedLayoutState
            Dim state As ActivityFeedLayoutState = context.LayoutState
            If Not TempVar Then
                ' Store any state we might need since (in theory) the layout could be in use by multiple
                ' elements simultaneously
                ' In reality for the Xbox Activity Feed there's probably only a single instance.
                context.LayoutState = New ActivityFeedLayoutState
            End If
        End Sub
        Protected Overrides Sub UninitializeForContextCore(context As VirtualizingLayoutContext)
            MyBase.UninitializeForContextCore(context)

            ' clear any state
            context.LayoutState = Nothing
        End Sub
#End Region

#Region "Layout"

        Protected Overrides Function MeasureOverride(context As VirtualizingLayoutContext, availableSize As Size) As Size
            If Me.MinItemSize = Size.Empty Then
                Dim firstElement = context.GetOrCreateElementAt(0)
#If Not UNIVERSAL

                firstElement.Measure(New Size(Single.PositiveInfinity, Single.PositiveInfinity))
#Else

                firstElement.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

#End If

                ' setting the member value directly to skip invalidating layout
                Me._minItemSize = firstElement.DesiredSize
            End If

            ' Determine which rows need to be realized.  We know every row will have the same height and
            ' only contain 3 items.  Use that to determine the index for the first and last item that
            ' will be within that realization rect.
            Dim firstRowIndex = Math.Max( _
                CInt(CLng(Fix((context.RealizationRect.Y / (Me.MinItemSize.Height + Me.RowSpacing)))) Mod Integer.MaxValue) - 1,
                0)
            Dim lastRowIndex = Math.Min( _
                CInt(CLng(Fix((context.RealizationRect.Bottom / (Me.MinItemSize.Height + Me.RowSpacing)))) Mod Integer.MaxValue) + 1,
                CInt(CLng(Fix((context.ItemCount / 3))) Mod Integer.MaxValue))

            ' Determine which items will appear on those rows and what the rect will be for each item
            Dim state As AppUIBasics.Common.ActivityFeedLayoutState = TryCast(context.LayoutState, ActivityFeedLayoutState)
            state.LayoutRects.Clear()

            ' Save the index of the first realized item.  We'll use it as a starting point during arrange.
            state.FirstRealizedIndex = firstRowIndex * 3

            ' ideal item width that will expand/shrink to fill available space
            Dim desiredItemWidth As Double = Math.Max(Me.MinItemSize.Width, (availableSize.Width - Me.ColumnSpacing * 3) / 4)

            ' Foreach item between the first and last index,
            '     Call GetElementOrCreateElementAt which causes an element to either be realized or retrieved
            '       from a recycle pool
            '     Measure the element using an appropriate size
            '
            ' Any element that was previously realized which we don't retrieve in this pass (via a call to
            ' GetElementOrCreateAt) will be automatically cleared and set aside for later re-use.
            ' Note: While this work fine, it does mean that more elements than are required may be
            ' created because it isn't until after our MeasureOverride completes that the unused elements
            ' will be recycled and available to use.  We could avoid this by choosing to track the first/last
            ' index from the previous layout pass.  The diff between the previous range and current range
            ' would represent the elements that we can pre-emptively make available for re-use by calling
            ' context.RecycleElement(element).
            For rowIndex As Integer = firstRowIndex To lastRowIndex - 1
                Dim firstItemIndex As Integer = rowIndex * 3
                Dim boundsForCurrentRow As Rect() = CalculateLayoutBoundsForRow(rowIndex, desiredItemWidth)

                For columnIndex As Integer = 0 To 3 - 1
                    Dim index As Integer = firstItemIndex + columnIndex
                    Dim rect = boundsForCurrentRow(index Mod 3)
                    Dim container = context.GetOrCreateElementAt(index)

                    container.Measure( _
                        New Size(boundsForCurrentRow(columnIndex).Width, boundsForCurrentRow(columnIndex).Height))

                    state.LayoutRects.Add(boundsForCurrentRow(columnIndex))
                Next
            Next

            ' Calculate and return the size of all the content (realized or not) by figuring out
            ' what the bottom/right position of the last item would be.
            Dim extentHeight = (CInt(CLng(Fix((context.ItemCount / 3))) Mod Integer.MaxValue) - 1) * (Me.MinItemSize.Height + Me.RowSpacing) + Me.MinItemSize.Height

            ' Report this as the desired size for the layout
            Return New Size(desiredItemWidth * 4 + Me.ColumnSpacing * 2, extentHeight)
        End Function
        Protected Overrides Function ArrangeOverride(context As VirtualizingLayoutContext, finalSize As Size) As Size
            ' walk through the cache of containers and arrange
            Dim state As AppUIBasics.Common.ActivityFeedLayoutState = TryCast(context.LayoutState, ActivityFeedLayoutState)
            Dim virtualContext = TryCast(context, VirtualizingLayoutContext)
            Dim currentIndex As Integer = state.FirstRealizedIndex

            For Each arrangeRect As Rect In state.LayoutRects
                Dim container = virtualContext.GetOrCreateElementAt(currentIndex)
                container.Arrange(arrangeRect)
                currentIndex += 1
            Next

            Return finalSize
        End Function
#End Region

#Region "Helper methods"

        Private Function CalculateLayoutBoundsForRow(rowIndex As Integer, desiredItemWidth As Double) As Rect()
            Dim boundsForRow As Rect() = New Rect(2) {}

            Dim yoffset = rowIndex * (Me.MinItemSize.Height + Me.RowSpacing)
            boundsForRow(0).Y = __InlineAssignHelper(boundsForRow(1).Y, __InlineAssignHelper(boundsForRow(2).Y, yoffset))
            boundsForRow(0).Height = __InlineAssignHelper(boundsForRow(1).Height, __InlineAssignHelper(boundsForRow(2).Height, Me.MinItemSize.Height))

            If rowIndex Mod 2 = 0 Then
                ' Left tile (narrow)
                boundsForRow(0).X = 0
                boundsForRow(0).Width = desiredItemWidth
                ' Middle tile (narrow)
                boundsForRow(1).X = boundsForRow(0).Right + Me.ColumnSpacing
                boundsForRow(1).Width = desiredItemWidth
                ' Right tile (wide)
                boundsForRow(2).X = boundsForRow(1).Right + Me.ColumnSpacing
                boundsForRow(2).Width = desiredItemWidth * 2 + Me.ColumnSpacing
            Else
                ' Left tile (wide)
                boundsForRow(0).X = 0
                boundsForRow(0).Width = (desiredItemWidth * 2 + Me.ColumnSpacing)
                ' Middle tile (narrow)
                boundsForRow(1).X = boundsForRow(0).Right + Me.ColumnSpacing
                boundsForRow(1).Width = desiredItemWidth
                ' Right tile (narrow)
                boundsForRow(2).X = boundsForRow(1).Right + Me.ColumnSpacing
                boundsForRow(2).Width = desiredItemWidth
            End If

            Return boundsForRow
        End Function
        <Obsolete("Please refactor code that uses this function, it is a simple work-around to simulate inline assignment in VB!")>
        Private Shared Function __InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
            target = value
            Return value
        End Function

#End Region

    End Class


    Friend Class ActivityFeedLayoutState
        Public Property FirstRealizedIndex As Integer
        ''' <summary>
        ''' List of layout bounds for items starting with the
        ''' FirstRealizedIndex.
        ''' </summary>
        Public ReadOnly Property LayoutRects As List(Of Rect)
            Get
                If _layoutRects Is Nothing Then
                    _layoutRects = New List(Of Rect)
                End If

                Return _layoutRects
            End Get
        End Property
        Private _layoutRects As List(Of Rect)
    End Class
End Namespace
