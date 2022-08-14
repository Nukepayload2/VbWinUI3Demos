' To configure or remove Option's included in result, go to Options/Advanced Options...
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading.Tasks
Imports Microsoft.UI.Xaml.Controls

Namespace AppUIBasics
    ''' <summary>
    ''' The OrientedSize structure is used to abstract the growth direction from
    ''' the layout algorithms of WrapPanel.  When the growth direction is
    ''' oriented horizontally (ex: the next element is arranged on the side of
    ''' the previous element), then the Width grows directly with the placement
    ''' of elements and Height grows indirectly with the size of the largest
    ''' element in the row.  When the orientation is reversed, so is the
    ''' directional growth with respect to Width and Height.
    ''' </summary>
    ''' <QualityBand>Mature</QualityBand>
    <StructLayout(LayoutKind.Sequential)>
    Friend Structure OrientedSize
        ''' <summary>
        ''' The orientation of the structure.
        ''' </summary>
        Private _orientation As Orientation
        ''' <summary>
        ''' Gets the orientation of the structure.
        ''' </summary>
        Public ReadOnly Property Orientation As Orientation
            Get
                Return _orientation
            End Get
        End Property

        ''' <summary>
        ''' The size dimension that grows directly with layout placement.
        ''' </summary>
        Private _direct As Double
        ''' <summary>
        ''' Gets or sets the size dimension that grows directly with layout
        ''' placement.
        ''' </summary>
        Public Property Direct As Double
            Get
                Return _direct
            End Get

            Set(value As Double)
                _direct = value
            End Set
        End Property

        ''' <summary>
        ''' The size dimension that grows indirectly with the maximum value of
        ''' the layout row or column.
        ''' </summary>
        Private _indirect As Double
        ''' <summary>
        ''' Gets or sets the size dimension that grows indirectly with the
        ''' maximum value of the layout row or column.
        ''' </summary>
        Public Property Indirect As Double
            Get
                Return _indirect
            End Get

            Set(value As Double)
                _indirect = value
            End Set
        End Property
        ''' <summary>
        ''' Gets or sets the width of the size.
        ''' </summary>
        Public Property Width As Double
            Get
                Return If((Orientation = Orientation.Horizontal),
                    Direct,
                    Indirect)
            End Get

            Set(value As Double)
                If Orientation = Orientation.Horizontal Then
                    Direct = value
                Else
                    Indirect = value
                End If
            End Set
        End Property
        ''' <summary>
        ''' Gets or sets the height of the size.
        ''' </summary>
        Public Property Height As Double
            Get
                Return If((Orientation <> Orientation.Horizontal),
                    Direct,
                    Indirect)
            End Get

            Set(value As Double)
                If Orientation <> Orientation.Horizontal Then
                    Direct = value
                Else
                    Indirect = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' Initializes a new OrientedSize structure.
        ''' </summary>
        ''' <param name="orientation">Orientation of the structure.</param>
        Public Sub New(orientation1 As Orientation)
            Me.New(orientation1, 0.0, 0.0)
        End Sub

        ''' <summary>
        ''' Initializes a new OrientedSize structure.
        ''' </summary>
        ''' <param name="orientation">Orientation of the structure.</param>
        ''' <param name="width">Un-oriented width of the structure.</param>
        ''' <param name="height">Un-oriented height of the structure.</param>
        Public Sub New(orientation1 As Orientation, width1 As Double, height1 As Double)
            _orientation = orientation1

            ' All fields must be initialized before we access the this pointer
            _direct = 0.0
            _indirect = 0.0

            Me.Width = width1
            Me.Height = height1
        End Sub
    End Structure
End Namespace
