' To configure or remove Option's included in result, go to Options/Advanced Options...
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls

#If Not UNIVERSAL

Imports System.Collections.ObjectModel
Imports System.ComponentModel
#Else

using Microsoft.UI.Xaml.Data;

#End If

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class TreeViewPage
        Inherits Page
        Private personalFolder As TreeViewNode
        Private personalFolder2 As TreeViewNode
        Private DataSource As ObservableCollection(Of ExplorerItem)

        Public Sub New()
            Me.InitializeComponent()
            Me.DataContext = Me
            DataSource = GetData()

            InitializeSampleTreeView()
            InitializeSampleTreeView2()
        End Sub
        Private Sub InitializeSampleTreeView()
            Dim workFolder As New TreeViewNode() With
{
                .Content = "Work Documents"}
            workFolder.IsExpanded = True

            workFolder.Children.Add(New TreeViewNode() With
{
                .Content = "XYZ Functional Spec"})
            workFolder.Children.Add(New TreeViewNode() With
{
                .Content = "Feature Schedule"})
            workFolder.Children.Add(New TreeViewNode() With
{
                .Content = "Overall Project Plan"})
            workFolder.Children.Add(New TreeViewNode() With
{
                .Content = "Feature Resources Allocation"})

            Dim remodelFolder As New TreeViewNode() With
{
                .Content = "Home Remodel"}
            remodelFolder.IsExpanded = True

            remodelFolder.Children.Add(New TreeViewNode() With
{
                .Content = "Contractor Contact Info"})
            remodelFolder.Children.Add(New TreeViewNode() With
{
                .Content = "Paint Color Scheme"})
            remodelFolder.Children.Add(New TreeViewNode() With
{
                .Content = "Flooring woodgrain type"})
            remodelFolder.Children.Add(New TreeViewNode() With
{
                .Content = "Kitchen cabinet style"})

            personalFolder = New TreeViewNode() With
{
                .Content = "Personal Documents"}
            personalFolder.IsExpanded = True
            personalFolder.Children.Add(remodelFolder)

            sampleTreeView.RootNodes.Add(workFolder)
            sampleTreeView.RootNodes.Add(personalFolder)
        End Sub
        Private Sub InitializeSampleTreeView2()
            Dim workFolder As New TreeViewNode() With
{
                .Content = "Work Documents"}
            workFolder.IsExpanded = True

            workFolder.Children.Add(New TreeViewNode() With
{
                .Content = "XYZ Functional Spec"})
            workFolder.Children.Add(New TreeViewNode() With
{
                .Content = "Feature Schedule"})
            workFolder.Children.Add(New TreeViewNode() With
{
                .Content = "Overall Project Plan"})
            workFolder.Children.Add(New TreeViewNode() With
{
                .Content = "Feature Resources Allocation"})

            Dim remodelFolder As New TreeViewNode() With
{
                .Content = "Home Remodel"}
            remodelFolder.IsExpanded = True

            remodelFolder.Children.Add(New TreeViewNode() With
{
                .Content = "Contractor Contact Info"})
            remodelFolder.Children.Add(New TreeViewNode() With
{
                .Content = "Paint Color Scheme"})
            remodelFolder.Children.Add(New TreeViewNode() With
{
                .Content = "Flooring woodgrain type"})
            remodelFolder.Children.Add(New TreeViewNode() With
{
                .Content = "Kitchen cabinet style"})

            personalFolder2 = New TreeViewNode() With
{
                .Content = "Personal Documents"}
            personalFolder2.IsExpanded = True
            personalFolder2.Children.Add(remodelFolder)

            sampleTreeView2.RootNodes.Add(workFolder)
            sampleTreeView2.RootNodes.Add(personalFolder2)
        End Sub
        Private Sub sampleTreeView_ItemInvoked(sender As TreeView, args As TreeViewItemInvokedEventArgs)
            Return
        End Sub
        Private Function GetData() As ObservableCollection(Of ExplorerItem)
            Dim list As Collections.ObjectModel.ObservableCollection(Of AppUIBasics.ControlPages.ExplorerItem) = New ObservableCollection(Of ExplorerItem)
            Dim folder1 As New ExplorerItem() With
            { _
            .Name = "Work Documents",
            .Type = ExplorerItem.ExplorerItemType.Folder,
            .Children = New Collections.ObjectModel.ObservableCollection(Of AppUIBasics.ControlPages.ExplorerItem) From { _
                New ExplorerItem() With
                    { _
                .Name = "Functional Specifications",
                .Type = ExplorerItem.ExplorerItemType.Folder,
                .Children = New Collections.ObjectModel.ObservableCollection(Of AppUIBasics.ControlPages.ExplorerItem) From { _
                    New ExplorerItem() With
                            { _
                    .Name = "TreeView spec",
                    .Type = ExplorerItem.ExplorerItemType.File
                              }
                }
                    },
                New ExplorerItem() With
                    { _
                .Name = "Feature Schedule",
                .Type = ExplorerItem.ExplorerItemType.File
                    },
                New ExplorerItem() With
                    { _
                .Name = "Overall Project Plan",
                .Type = ExplorerItem.ExplorerItemType.File
                    },
                New ExplorerItem() With
                    { _
                .Name = "Feature Resources Allocation",
                .Type = ExplorerItem.ExplorerItemType.File
                    }
            }
            }
            Dim folder2 As New ExplorerItem() With
            { _
            .Name = "Personal Folder",
            .Type = ExplorerItem.ExplorerItemType.Folder,
            .Children = New Collections.ObjectModel.ObservableCollection(Of AppUIBasics.ControlPages.ExplorerItem) From { _
                New ExplorerItem() With
                            { _
                .Name = "Home Remodel Folder",
                .Type = ExplorerItem.ExplorerItemType.Folder,
                .Children = New Collections.ObjectModel.ObservableCollection(Of AppUIBasics.ControlPages.ExplorerItem) From { _
                    New ExplorerItem() With
                                    { _
                    .Name = "Contractor Contact Info",
                    .Type = ExplorerItem.ExplorerItemType.File
                                    },
                    New ExplorerItem() With
                                    { _
                    .Name = "Paint Color Scheme",
                    .Type = ExplorerItem.ExplorerItemType.File
                                    },
                    New ExplorerItem() With
                                    { _
                    .Name = "Flooring Woodgrain type",
                    .Type = ExplorerItem.ExplorerItemType.File
                                    },
                    New ExplorerItem() With
                                    { _
                    .Name = "Kitchen Cabinet Style",
                    .Type = ExplorerItem.ExplorerItemType.File
                                    }
                }
                            }
            }
            }

            list.Add(folder1)
            list.Add(folder2)
            Return list
        End Function

    End Class


    Public Class ExplorerItem
        Implements INotifyPropertyChanged
        Public Event PropertyChanged As PropertyChangedEventHandler Implements ComponentModel.INotifyPropertyChanged.PropertyChanged
        Public Enum ExplorerItemType
            Folder
            File
        End Enum
        Public Property Name As String
        Public Property Type As ExplorerItemType
        Private m_children As ObservableCollection(Of ExplorerItem)
        Public Property Children As ObservableCollection(Of ExplorerItem)
            Get
                If m_children Is Nothing Then
                    m_children = New ObservableCollection(Of ExplorerItem)
                End If

                Return m_children
            End Get

            Set(value As ObservableCollection(Of ExplorerItem))
                m_children = value
            End Set
        End Property
        Private m_isExpanded As Boolean
        Public Property IsExpanded As Boolean
            Get
                Return m_isExpanded
            End Get

            Set(value As Boolean)
                If m_isExpanded <> value Then
                    m_isExpanded = value
                    NotifyPropertyChanged("IsExpanded")
                End If
            End Set
        End Property
        Private Sub NotifyPropertyChanged(propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
    End Class

    Class ExplorerItemTemplateSelector
        Inherits DataTemplateSelector
        Public Property FolderTemplate As DataTemplate
        Public Property FileTemplate As DataTemplate
        Protected Overrides Function SelectTemplateCore(item As Object) As DataTemplate
            Dim explorerItem1 As AppUIBasics.ControlPages.ExplorerItem = CType(item, ExplorerItem)
            Return If(explorerItem1.Type = ExplorerItem.ExplorerItemType.Folder, FolderTemplate, FileTemplate)
        End Function
    End Class
End Namespace
