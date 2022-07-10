' Copyright (c) Microsoft Corporation. All rights reserved.
' Licensed under the MIT License. See LICENSE in the project root for license information.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.Diagnostics
Imports Windows.Foundation
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls

Imports VirtualizingLayout = Microsoft.UI.Xaml.Controls.VirtualizingLayout
Imports VirtualizingLayoutContext = Microsoft.UI.Xaml.Controls.VirtualizingLayoutContext
Imports System.Runtime.InteropServices

Namespace AppUIBasics.Common
    Public Class VariedImageSizeLayout
        Inherits VirtualizingLayout
        Public Property Width As Double = 150
#If Not UNIVERSAL

        Protected Overrides Sub OnItemsChangedCore(context As VirtualizingLayoutContext, source As Object, args As NotifyCollectionChangedEventArgs)
            ' The data collection has changed, so the bounds of all the indices are not valid anymore. 
            ' We need to re-evaluate all the bounds and cache them during the next measure.
            m_cachedBounds.Clear()
            m_firstIndex = __InlineAssignHelper(m_lastIndex, 0)
            cachedBoundsInvalid = True
            InvalidateMeasure()
        End Sub
#End If

        Protected Overrides Function MeasureOverride(context As VirtualizingLayoutContext, availableSize As Size) As Size
            Dim viewport = context.RealizationRect

            If availableSize.Width <> m_lastAvailableWidth OrElse cachedBoundsInvalid Then
                UpdateCachedBounds(availableSize)
                m_lastAvailableWidth = availableSize.Width
            End If

            ' Initialize column offsets
            Dim numColumns As Integer = CInt(CLng(Fix((availableSize.Width / Width))) Mod Integer.MaxValue)
            If m_columnOffsets.Count = 0 Then
                For i As Integer = 0 To numColumns - 1
                    m_columnOffsets.Add(0)
                Next
            End If

            m_firstIndex = GetStartIndex(viewport)
            Dim currentIndex As Integer = m_firstIndex
            Dim nextOffset As Double = -1.0

            ' Measure items from start index to when we hit the end of the viewport.
            While currentIndex < context.ItemCount AndAlso nextOffset < viewport.Bottom
                Dim child = context.GetOrCreateElementAt(currentIndex)
                child.Measure(New Size(Width, availableSize.Height))

                If currentIndex >= m_cachedBounds.Count Then
                    ' We do not have bounds for this index. Lay it out and cache it.
                    Dim columnIndex As Integer = GetIndexOfLowestColumn(m_columnOffsets, nextOffset)
                    m_cachedBounds.Add(New Rect(columnIndex * Width, nextOffset, Width, child.DesiredSize.Height))
                    m_columnOffsets(columnIndex) += child.DesiredSize.Height
                Else
                    If currentIndex + 1 = m_cachedBounds.Count Then
                        ' Last element. Use the next offset.
                        GetIndexOfLowestColumn(m_columnOffsets, nextOffset)
                    Else
                        nextOffset = m_cachedBounds(currentIndex + 1).Top
                    End If
                End If

                m_lastIndex = currentIndex
                currentIndex += 1
            End While

            Dim extent = GetExtentSize(availableSize)
            Return extent
        End Function
        Protected Overrides Function ArrangeOverride(context As VirtualizingLayoutContext, finalSize As Size) As Size
            If m_cachedBounds.Count > 0 Then
                For index As Integer = m_firstIndex To m_lastIndex
                    Dim child = context.GetOrCreateElementAt(index)
                    child.Arrange(m_cachedBounds(index))
                Next
            End If
            Return finalSize
        End Function
        Private Sub UpdateCachedBounds(availableSize As Size)
            Dim numColumns As Integer = CInt(CLng(Fix((availableSize.Width / Width))) Mod Integer.MaxValue)
            m_columnOffsets.Clear()
            For i As Integer = 0 To numColumns - 1
                m_columnOffsets.Add(0)
            Next

            For index As Integer = 0 To m_cachedBounds.Count - 1
                Dim nextOffset As Object = Nothing
                Dim columnIndex As Integer = GetIndexOfLowestColumn(m_columnOffsets, nextOffset)
                Dim oldHeight = m_cachedBounds(index).Height
                m_cachedBounds(index) = New Rect(columnIndex * Width, nextOffset, Width, oldHeight)
                m_columnOffsets(columnIndex) += oldHeight
            Next

            cachedBoundsInvalid = False
        End Sub
        Private Function GetStartIndex(viewport As Rect) As Integer
            Dim startIndex As Integer = 0
            If m_cachedBounds.Count = 0 Then
                startIndex = 0
            Else
                ' find first index that intersects the viewport
                ' perhaps this can be done more efficiently than walking
                ' from the start of the list.
                For i As Integer = 0 To m_cachedBounds.Count - 1
                    Dim currentBounds = m_cachedBounds(i)
                    If currentBounds.Y < viewport.Bottom AndAlso _
                        currentBounds.Bottom > viewport.Top Then
                        startIndex = i
                        Exit For
                    End If
                Next
            End If

            Return startIndex
        End Function
        Private Function GetIndexOfLowestColumn(columnOffsets As List(Of Double), <Out> ByRef lowestOffset As Double) As Integer
            Dim lowestIndex As Integer = 0
            lowestOffset = columnOffsets(lowestIndex)
            For index As Integer = 0 To columnOffsets.Count - 1
                Dim currentOffset As Double = columnOffsets(index)
                If lowestOffset > currentOffset Then
                    lowestOffset = currentOffset
                    lowestIndex = index
                End If
            Next

            Return lowestIndex
        End Function
        Private Function GetExtentSize(availableSize As Size) As Size
            Dim largestColumnOffset As Double = m_columnOffsets(0)
            For index As Integer = 0 To m_columnOffsets.Count - 1
                Dim currentOffset As Double = m_columnOffsets(index)
                If largestColumnOffset < currentOffset Then
                    largestColumnOffset = currentOffset
                End If
            Next

            Return New Size(availableSize.Width, largestColumnOffset)
        End Function
        Private m_firstIndex As Integer = 0
        Private m_lastIndex As Integer = 0
        Private m_lastAvailableWidth As Double = 0.0
        Private m_columnOffsets As New List(Of Double)
        Private m_cachedBounds As New List(Of Rect)
        Private cachedBoundsInvalid As Boolean = False
        <Obsolete("Please refactor code that uses this function, it is a simple work-around to simulate inline assignment in VB!")>
        Private Shared Function __InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
            target = value
            Return value
        End Function
    End Class
End Namespace
