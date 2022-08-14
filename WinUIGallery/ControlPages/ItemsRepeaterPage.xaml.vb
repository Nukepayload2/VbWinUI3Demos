' To configure or remove Option's included in result, go to Options/Advanced Options...
Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.Linq
Imports AppUIBasics.Common
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Automation
Imports Microsoft.UI.Xaml.Automation.Peers
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Hosting
Imports Microsoft.UI.Xaml.Input

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class ItemsRepeaterPage
        Inherits ItemsPageBase
        Private random1 As New Random
        Private MaxLength As Integer = 425
        Public BarItems As ObservableCollection(Of Bar)
        Public filteredRecipeData As New MyItemsSource(Nothing)
        Public staticRecipeData As List(Of Recipe)
        Private IsSortDescending As Boolean = False
        Private AnimatedBtnHeight As Double
        Private AnimatedBtnMargin As Thickness
        Private LastSelectedColorButton As Button
        Private PreviouslyFocusedAnimatedScrollRepeaterIndex As Integer = -1

        Public Sub New()
            Me.InitializeComponent()
            InitializeData()
            repeater2.ItemsSource = Enumerable.Range(0, 500)
        End Sub
        Public ColorList As New List(Of [String]) From { _
                "Blue",
                "BlueViolet",
                "Crimson",
                "DarkCyan",
                "DarkGoldenrod",
                "DarkMagenta",
                "DarkOliveGreen",
                "DarkRed",
                "DarkSlateBlue",
                "DeepPink",
                "IndianRed",
                "MediumSlateBlue",
                "Maroon",
                "MidnightBlue",
                "Peru",
                "SaddleBrown",
                "SteelBlue",
                "OrangeRed",
                "Firebrick",
                "DarkKhaki"
        }
        Private Sub InitializeData()
            If BarItems Is Nothing Then
                BarItems = New ObservableCollection(Of Bar)
            End If
            BarItems.Add(New Bar(300, Me.MaxLength))
            BarItems.Add(New Bar(25, Me.MaxLength))
            BarItems.Add(New Bar(175, Me.MaxLength))

            Dim basicData As New List(Of Object) From { _
                64,
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                128,
                "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                256,
                "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
                512,
                "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                1024
            }
            MixedTypeRepeater.ItemsSource = basicData

            Dim nestedCategories As New List(Of NestedCategory) From { _
                New NestedCategory("Fruits", GetFruits()),
                New NestedCategory("Vegetables", GetVegetables()),
                New NestedCategory("Grains", GetGrains()),
                New NestedCategory("Proteins", GetProteins())
            }


            outerRepeater.ItemsSource = nestedCategories

            ' Set sample code to display on page's initial load
            SampleCodeLayout.Value = "<StackLayout x:Name=""VerticalStackLayout"" Orientation=""Vertical"" Spacing=""8""/>"

            SampleCodeDT.Value = "<DataTemplate x:Key=""HorizontalBarTemplate"" x:DataType=""l:Bar"">
    <Border Background=""{ThemeResource SystemChromeLowColor}"" Width=""{x:Bind MaxLength}"" >
        <Rectangle Fill=""{ThemeResource SystemAccentColor}"" Width=""{x:Bind Length}"" 
                   Height=""24"" HorizontalAlignment=""Left""/> 
    </Border>
</DataTemplate>"

            SampleCodeLayout2.Value = "<common:ActivityFeedLayout x:Key=""MyFeedLayout"" ColumnSpacing=""12""
                          RowSpacing=""12"" MinItemSize=""80, 108""/>"


            animatedScrollRepeater.ItemsSource = ColorList
            AddHandler animatedScrollRepeater.ElementPrepared, AddressOf OnElementPrepared

            ' Initialize custom MyItemsSource object with new recipe data
            Dim RecipeList As List(Of Recipe) = GetRecipeList()
            filteredRecipeData.InitializeCollection(RecipeList)
            ' Save a static copy to compare to while filtering
            staticRecipeData = RecipeList
            VariedImageSizeRepeater.ItemsSource = filteredRecipeData
        End Sub
        Private Function GetFruits() As ObservableCollection(Of String)
            Return New ObservableCollection(Of String) From {
                "Apricots",
                "Bananas",
                "Grapes",
                "Strawberries",
                "Watermelon",
                "Plums",
                "Blueberries"}
        End Function
        Private Function GetVegetables() As ObservableCollection(Of String)
            Return New ObservableCollection(Of String) From {
                "Broccoli",
                "Spinach",
                "Sweet potato",
                "Cauliflower",
                "Onion",
                "Brussels sprouts",
                "Carrots"}
        End Function
        Private Function GetGrains() As ObservableCollection(Of String)
            Return New ObservableCollection(Of String) From {
                "Rice",
                "Quinoa",
                "Pasta",
                "Bread",
                "Farro",
                "Oats",
                "Barley"}
        End Function
        Private Function GetProteins() As ObservableCollection(Of String)
            Return New ObservableCollection(Of String) From {
                "Steak",
                "Chicken",
                "Tofu",
                "Salmon",
                "Pork",
                "Chickpeas",
                "Eggs"}
        End Function
        ' ==========================================================================
        ' Basic, non-interactive ItemsRepeater
        ' ==========================================================================
        Private Sub AddBtn_Click(sender As Object, e As RoutedEventArgs)
            BarItems.Add(New Bar(random1.[Next](Me.MaxLength), Me.MaxLength))
            DeleteBtn.IsEnabled = True
        End Sub
        Private Sub DeleteBtn_Click(sender As Object, e As RoutedEventArgs)
            If BarItems.Count > 0 Then
                BarItems.RemoveAt(0)
                If BarItems.Count = 0 Then
                    DeleteBtn.IsEnabled = False
                End If
            End If
        End Sub
        Private Sub RadioBtn_Click(sender As Object, e As SelectionChangedEventArgs)
            Dim itemTemplateKey As String = String.Empty
            Dim selected = TryCast(sender, Microsoft.UI.Xaml.Controls.RadioButtons).SelectedItem
            If selected Is Nothing Then
                ' No point in continuing if selected element is null
                Return
            End If
            Dim layoutKey As String = TryCast(CType(selected, FrameworkElement).Tag, String)

            If layoutKey.Equals(NameOf(Me.VerticalStackLayout)) Then
                ' we used x:Name in the resources which both acts as the x:Key value and creates a member field by the same name
                layout.Value = layoutKey
                itemTemplateKey = "HorizontalBarTemplate"

                repeater.MaxWidth = MaxLength + 12

                SampleCodeLayout.Value = "<StackLayout x:Name=""VerticalStackLayout"" Orientation=""Vertical"" Spacing=""8""/>"
                SampleCodeDT.Value = "<DataTemplate x:Key=""HorizontalBarTemplate"" x:DataType=""l:Bar"">
    <Border Background=""{ThemeResource SystemChromeLowColor}"" Width=""{x:Bind MaxLength}"" >
        <Rectangle Fill=""{ThemeResource SystemAccentColor}"" Width=""{x:Bind Length}""
                   Height=""24"" HorizontalAlignment=""Left""/> 
    </Border>
</DataTemplate>"
            ElseIf layoutKey.Equals(NameOf(Me.HorizontalStackLayout)) Then
                layout.Value = layoutKey
                itemTemplateKey = "VerticalBarTemplate"

                repeater.MaxWidth = 6000

                SampleCodeLayout.Value = "<StackLayout x:Name=""HorizontalStackLayout"" Orientation=""Horizontal"" Spacing=""8""/> "
                SampleCodeDT.Value = "<DataTemplate x:Key=""VerticalBarTemplate"" x:DataType=""l:Bar"">
    <Border Background=""{ThemeResource SystemChromeLowColor}"" Height=""{x:Bind MaxHeight}"">
        <Rectangle Fill=""{ThemeResource SystemAccentColor}"" Height=""{x:Bind Height}"" 
                   Width=""48"" VerticalAlignment=""Top""/>
    </Border>
</DataTemplate>"
            ElseIf layoutKey.Equals(NameOf(Me.UniformGridLayout)) Then
                layout.Value = layoutKey
                itemTemplateKey = "CircularTemplate"

                repeater.MaxWidth = 540

                SampleCodeLayout.Value = "<UniformGridLayout x:Name=""UniformGridLayout"" MinRowSpacing=""8"" MinColumnSpacing=""8""/>"
                SampleCodeDT.Value = "<DataTemplate x:Key=""CircularTemplate"" x:DataType=""l:Bar"">
    <Grid>
        <Ellipse Fill=""{ThemeResource SystemChromeLowColor}"" Height=""{x:Bind MaxDiameter}"" 
                 Width=""{x:Bind MaxDiameter}"" VerticalAlignment=""Center"" HorizontalAlignment=""Center""/>
        <Ellipse Fill=""{ThemeResource SystemAccentColor}"" Height=""{x:Bind Diameter}"" 
                 Width=""{x:Bind Diameter}"" VerticalAlignment=""Center"" HorizontalAlignment=""Center""/>
    </Grid>
</DataTemplate>"
            End If
            repeater.Layout = TryCast(Resources(layoutKey), Microsoft.UI.Xaml.Controls.VirtualizingLayout)
            repeater.ItemTemplate = TryCast(Resources(itemTemplateKey), DataTemplate)
            repeater.ItemsSource = BarItems

            elementGenerator.Value = itemTemplateKey
        End Sub
        ' ==========================================================================
        ' Virtualizing, scrollable list of items laid out by ItemsRepeater
        ' ==========================================================================
        Private Sub LayoutBtn_Click(sender As Object, e As RoutedEventArgs)
            Dim layoutKey As String = TryCast(CType(sender, FrameworkElement).Tag, String)

            repeater2.Layout = TryCast(Resources(layoutKey), Microsoft.UI.Xaml.Controls.VirtualizingLayout)

            layout2.Value = layoutKey

            If layoutKey = "UniformGridLayout2" Then
                SampleCodeLayout2.Value = "<UniformGridLayout x:Key=""UniformGridLayout2"" MinItemWidth=""108"" MinItemHeight=""108""
                   MinRowSpacing=""12"" MinColumnSpacing=""12""/>"
            ElseIf layoutKey = "MyFeedLayout" Then
                SampleCodeLayout2.Value = "<common:ActivityFeedLayout x:Key=""MyFeedLayout"" ColumnSpacing=""12""
                          RowSpacing=""12"" MinItemSize=""80, 108""/>"
            End If
        End Sub
        ' ==========================================================================
        ' Animated Scrolling ItemsRepeater with Content Sample
        ' ==========================================================================
        Private Sub OnAnimatedItemGotFocus(sender As Object, e As RoutedEventArgs)
            Dim item = TryCast(sender, FrameworkElement)

            ' Store the last focused Index so we can land back on it when focus leaves
            ' and comes back to the repeater.
            PreviouslyFocusedAnimatedScrollRepeaterIndex = animatedScrollRepeater.GetElementIndex(TryCast(sender, UIElement))

            item.StartBringIntoView(New BringIntoViewOptions() With
            { _
            .VerticalAlignmentRatio = 0.5,
            .AnimationDesired = True
            })
        End Sub
        Private Sub OnAnimatedScrollRepeaterGettingFocus(sender As UIElement, args As GettingFocusEventArgs)
            ' If we have a previously focused index and focus moving from outside the repeater to inside,
            ' then we can pick the previously focused index and land on that item again.
            Dim lastFocus = TryCast(args.OldFocusedElement, UIElement)
            If PreviouslyFocusedAnimatedScrollRepeaterIndex <> -1 AndAlso _
 lastFocus IsNot Nothing AndAlso animatedScrollRepeater.GetElementIndex(lastFocus) = -1 Then
                Dim item = animatedScrollRepeater.TryGetElement(PreviouslyFocusedAnimatedScrollRepeaterIndex)
                args.NewFocusedElement = item
            End If
        End Sub
        Private Sub OnAnimatedItemClicked(sender As Object, e As RoutedEventArgs)
            ' Update corresponding rectangle with selected color
            Dim senderBtn As Button = TryCast(sender, Button)
            colorRectangle.Fill = senderBtn.Background

            SetUIANamesForSelectedEntry(senderBtn)
        End Sub
        ' This function occurs each time an element is made ready for use.
        '  This is necessary for virtualization. 
        Private Sub OnElementPrepared(sender As Microsoft.UI.Xaml.Controls.ItemsRepeater, args As Microsoft.UI.Xaml.Controls.ItemsRepeaterElementPreparedEventArgs)
            Dim item = ElementCompositionPreview.GetElementVisual(args.Element)
            Dim svVisual = ElementCompositionPreview.GetElementVisual(Animated_ScrollViewer)
            Dim scrollProperties = ElementCompositionPreview.GetScrollViewerManipulationPropertySet(Animated_ScrollViewer)

            Dim scaleExpresion = scrollProperties.Compositor.CreateExpressionAnimation()
            scaleExpresion.SetReferenceParameter("svVisual", svVisual)
            scaleExpresion.SetReferenceParameter("scrollProperties", scrollProperties)
            scaleExpresion.SetReferenceParameter("item", item)

            ' Scale the item based on the distance of the item relative to the center of the viewport.
            scaleExpresion.Expression = "1 - abs((svVisual.Size.Y/2 - scrollProperties.Translation.Y) - (item.Offset.Y + item.Size.Y/2))*(.25/(svVisual.Size.Y/2))"

            ' Animate the item to change size based on distance from center of viewpoint
            item.StartAnimation("Scale.X", scaleExpresion)
            item.StartAnimation("Scale.Y", scaleExpresion)
            Dim centerPointExpression = scrollProperties.Compositor.CreateExpressionAnimation()
            centerPointExpression.SetReferenceParameter("item", item)
            centerPointExpression.Expression = "Vector3(item.Size.X/2, item.Size.Y/2, 0)"
            item.StartAnimation("CenterPoint", centerPointExpression)
        End Sub
        Private Sub GetButtonSize(sender As Object, e As RoutedEventArgs)
            Dim AnimatedBtn As Button = TryCast(sender, Button)
            AnimatedBtnHeight = AnimatedBtn.ActualHeight
            AnimatedBtnMargin = AnimatedBtn.Margin
        End Sub
        Private Sub SetUIANamesForSelectedEntry(selectedItem1 As Button)

            If LastSelectedColorButton IsNot Nothing AndAlso
                TypeOf LastSelectedColorButton.Content Is String Then

                Dim content1 As String = LastSelectedColorButton.Content.ToString
                AutomationProperties.SetName(LastSelectedColorButton, content1)
            End If

            AutomationProperties.SetName(selectedItem1, CStr(selectedItem1.Content) & " , selected")
            LastSelectedColorButton = selectedItem1
        End Sub
        ' Find centerpoint of ScrollViewer
        Private Function CenterPointOfViewportInExtent() As Double
            Return Animated_ScrollViewer.VerticalOffset + Animated_ScrollViewer.ViewportHeight / 2
        End Function
        ' Find index of the item that's at the center of the viewport
        Private Function GetSelectedIndexFromViewport() As Integer
            Dim selectedItemIndex As Integer = CInt(CLng(Fix(Math.Floor(CenterPointOfViewportInExtent() / (CDbl(AnimatedBtnMargin.Top) + AnimatedBtnHeight)))) Mod Integer.MaxValue)
            selectedItemIndex = selectedItemIndex Mod animatedScrollRepeater.ItemsSourceView.Count
            Return selectedItemIndex
        End Function
        ' Return item that's currently in center of viewport
        Private Function GetSelectedItemFromViewport() As Object
            Dim selectedIndex As Integer = GetSelectedIndexFromViewport()
            Dim selectedElement = TryCast(animatedScrollRepeater.TryGetElement(selectedIndex), Button)
            Return selectedElement
        End Function
        ' ==========================================================================
        ' VariedImageSize Layout with Filtering/Sorting
        ' ==========================================================================
        Private Function GetRecipeList() As List(Of Recipe)
            ' Initialize list of recipes for varied image size layout sample
            Dim rnd As System.Random = New Random
            Dim tempList As New List(Of Recipe)( _
                                        Enumerable.Range(0, 1000).[Select](Function(k) As AppUIBasics.ControlPages.Recipe
                                                                               Return New Recipe With
                                                                                                                           { _
                                                                               .Num = k,
                                                                               .Name = "Recipe " & k.ToString(),
                                                                               .Color = ColorList(rnd.[Next](0, 19))
                                                                                                                           }
                                                                           End Function))

            For Each rec As Recipe In tempList
                ' Add one food from each option into the recipe's ingredient list and ingredient string
                Dim fruitOption As String = GetFruits()(rnd.[Next](0, 6))
                Dim vegOption As String = GetVegetables()(rnd.[Next](0, 6))
                Dim grainOption As String = GetGrains()(rnd.[Next](0, 6))
                Dim proteinOption As String = GetProteins()(rnd.[Next](0, 6))
                rec.Ingredients = Microsoft.VisualBasic.vbLf & fruitOption & Microsoft.VisualBasic.vbLf & vegOption & Microsoft.VisualBasic.vbLf & grainOption & Microsoft.VisualBasic.vbLf & proteinOption
                rec.IngList = New List(Of String) From {
                    fruitOption,
                    vegOption,
                    grainOption,
                    proteinOption}

                ' Add extra ingredients so items have varied heights in the layout
                rec.RandomizeIngredients()
            Next

            Return tempList
        End Function
        Private Sub OnEnableAnimationsChanged(sender As Object, e As RoutedEventArgs)
#If WINUI_PRERELEASE

            VariedImageSizeRepeater.Animator = EnableAnimations.IsChecked.GetValueOrDefault() ? new DefaultElementAnimator() : null;

#End If

        End Sub
        Public Sub FilterRecipes_FilterChanged(sender As Object, e As RoutedEventArgs)
            UpdateSortAndFilter()
        End Sub
        Private Sub OnSortAscClick(sender As Object, e As RoutedEventArgs)
            If IsSortDescending = True Then
                IsSortDescending = False
                UpdateSortAndFilter()
            End If
        End Sub
        Private Sub OnSortDesClick(sender As Object, e As RoutedEventArgs)
            If Not IsSortDescending = True Then
                IsSortDescending = True
                UpdateSortAndFilter()
            End If
        End Sub
        Private Sub UpdateSortAndFilter()
            ' Find all recipes that ingredients include what was typed into the filtering text box
            Dim filteredTypes = staticRecipeData.Where(Function(i) i.Ingredients.Contains(FilterRecipes.Text, StringComparison.InvariantCultureIgnoreCase))
            ' Sort the recipes by whichever sorting mode was last selected (least to most ingredients by default)
            Dim sortedFilteredTypes = If(IsSortDescending,
                filteredTypes.OrderByDescending(Function(i) i.IngList.Count()),
                filteredTypes.OrderBy(Function(i) i.IngList.Count()))
            ' Re-initialize MyItemsSource object with this newly filtered data
            filteredRecipeData.InitializeCollection(sortedFilteredTypes)

            Dim peer = FrameworkElementAutomationPeer.FromElement(VariedImageSizeRepeater)

            peer.RaiseNotificationEvent(AutomationNotificationKind.Other, AutomationNotificationProcessing.ImportantMostRecent, $"Filtered recipes, {sortedFilteredTypes.Count()} results.", "RecipesFilteredNotificationActivityId")
        End Sub
        Private Sub OnAnimatedScrollRepeaterKeyDown(sender As Object, e As KeyRoutedEventArgs)
            If e.Handled <> True Then
                Dim targetIndex As Integer = -1
                If e.Key = Windows.System.VirtualKey.Home Then
                    targetIndex = If(PreviouslyFocusedAnimatedScrollRepeaterIndex <> 0, 0, -1)
                ElseIf e.Key = Windows.System.VirtualKey.[End] Then
                    targetIndex = If(PreviouslyFocusedAnimatedScrollRepeaterIndex <> animatedScrollRepeater.ItemsSourceView.Count - 1, animatedScrollRepeater.ItemsSourceView.Count - 1, -1)
                End If

                If targetIndex <> -1 Then
                    Dim element1 = animatedScrollRepeater.GetOrCreateElement(targetIndex)
                    element1.StartBringIntoView()
                    TryCast(element1, Control).Focus(FocusState.Programmatic)
                    e.Handled = True
                End If
            End If
        End Sub
    End Class


    Public Class NestedCategory
        Public Property CategoryName As String
        Public Property CategoryItems As ObservableCollection(Of String)
        Public Sub New(catName As String, catItems As ObservableCollection(Of String))
            CategoryName = catName
            CategoryItems = catItems
        End Sub
    End Class
    Public Class MyDataTemplateSelector
        Inherits DataTemplateSelector
        Public Property Normal As DataTemplate
        Public Property Accent As DataTemplate
        Protected Overrides Function SelectTemplateCore(item As Object) As DataTemplate
            If CInt(CLng(Fix(item)) Mod Integer.MaxValue) Mod 2 = 0 Then
                Return Normal
            Else
                Return Accent
            End If
        End Function
    End Class


    Public Class StringOrIntTemplateSelector
        Inherits DataTemplateSelector
        ' Define the (currently empty) data templates to return
        ' These will be "filled-in" in the XAML code.
        Public Property StringTemplate As DataTemplate

        Public Property IntTemplate As DataTemplate
        Protected Overrides Function SelectTemplateCore(item As Object) As DataTemplate
            ' Return the correct data template based on the item's type.
            If item.[GetType]() Is GetType(String) Then
                Return StringTemplate
            ElseIf item.[GetType]() Is GetType(Integer) Then
                Return IntTemplate
            Else
                Return Nothing
            End If
        End Function
    End Class


    Public Class Bar
        Public Sub New(length As Double, max As Integer)
            Me.Length = length
            MaxLength = max

            Height = length / 4
            MaxHeight = max / 4

            Diameter = length / 6
            MaxDiameter = max / 6
        End Sub
        Public Property Length As Double
        Public Property MaxLength As Integer

        Public Property Height As Double
        Public Property MaxHeight As Double

        Public Property Diameter As Double
        Public Property MaxDiameter As Double
    End Class


    Public Class Recipe
        Public Property Num As Integer
        Public Property Ingredients As String
        Public Property IngList As List(Of String)
        Public Property Name As String
        Public Property Color As String
        Public ReadOnly Property NumIngredients As Integer
            Get
                Return IngList.Count()
            End Get
        End Property
        Public Sub RandomizeIngredients()
            ' To give the items different heights, give recipes random numbers of random ingredients
            Dim rndNum As New Random
            Dim rndIng As New Random

            Dim extras As New ObservableCollection(Of String) From { _
                                                         "Garlic",
                                                         "Lemon",
                                                         "Butter",
                                                         "Lime",
                                                         "Feta Cheese",
                                                         "Parmesan Cheese",
                                                         "Breadcrumbs"}
            For i As Integer = 0 To rndNum.[Next](0, 4) - 1
                Dim newIng As String = extras(rndIng.[Next](0, 6))
                If Not IngList.Contains(newIng) Then
                    Ingredients &= Microsoft.VisualBasic.vbLf & newIng
                    IngList.Add(newIng)
                End If
            Next
        End Sub
    End Class

    ' Custom data source class that assigns elements unique IDs, making filtering easier
    Public Class MyItemsSource
        Implements IList, Microsoft.UI.Xaml.Controls.IKeyIndexMapping, INotifyCollectionChanged
        Private inner As New List(Of Recipe)

        Public Sub New(collection As IEnumerable(Of Recipe))
            InitializeCollection(collection)
        End Sub
        Public Sub InitializeCollection(collection As IEnumerable(Of Recipe))
            inner.Clear()
            If collection IsNot Nothing Then
                inner.AddRange(collection)
            End If

            RaiseEvent CollectionChanged(Me, New NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset))
        End Sub
#Region "IReadOnlyList<T>"
        Public ReadOnly Property Count As Integer Implements Collections.ICollection.Count
            Get
                Return If(Me.inner IsNot Nothing, Me.inner.Count, 0)
            End Get
        End Property
        Default Public Property item(index As Integer) As Object Implements Collections.IList.item
            Get
                Return TryCast(inner(index), Recipe)
            End Get

            Set(value As Object)
                inner(index) = CType(value, Recipe)
            End Set
        End Property
        Public Function GetEnumerator() As IEnumerator(Of Recipe)
            Return Me.inner.GetEnumerator()
        End Function

#End Region

#Region "INotifyCollectionChanged"

        Public Event CollectionChanged As NotifyCollectionChangedEventHandler Implements Collections.Specialized.INotifyCollectionChanged.CollectionChanged
#End Region

#Region "IKeyIndexMapping"

        Public Function KeyFromIndex(index As Integer) As String Implements IKeyIndexMapping.KeyFromIndex
            Return inner(index).Num.ToString()
        End Function
        Public Function IndexFromKey(key As String) As Integer Implements IKeyIndexMapping.IndexFromKey
            For Each item As Recipe In inner
                If item.Num.ToString() = key Then
                    Return inner.IndexOf(item)
                End If
            Next
            Return -1
        End Function
#End Region

#Region "Unused List methods"

        Private Function GetEnumerator1() As IEnumerator Implements Collections.IEnumerable.GetEnumerator
            Throw New NotImplementedException
        End Function
        Public Function Add(value As Object) As Integer Implements Collections.IList.Add
            Throw New NotImplementedException
        End Function
        Public Sub Clear() Implements Collections.IList.Clear
            Throw New NotImplementedException
        End Sub
        Public Function Contains(value As Object) As Boolean Implements Collections.IList.Contains
            Throw New NotImplementedException
        End Function
        Public Function IndexOf(value As Object) As Integer Implements Collections.IList.IndexOf
            Throw New NotImplementedException
        End Function
        Public Sub Insert(index As Integer, value As Object) Implements Collections.IList.Insert
            Throw New NotImplementedException
        End Sub
        Public Sub Remove(value As Object) Implements Collections.IList.Remove
            Throw New NotImplementedException
        End Sub
        Public Sub RemoveAt(index As Integer) Implements Collections.IList.RemoveAt
            Throw New NotImplementedException
        End Sub
        Public Sub CopyTo(array1 As Array, index As Integer) Implements Collections.ICollection.CopyTo
            Throw New NotImplementedException
        End Sub
        Public ReadOnly Property IsFixedSize As Boolean Implements Collections.IList.IsFixedSize
            Get
                Throw New NotImplementedException
            End Get
        End Property
        Public ReadOnly Property IsReadOnly As Boolean Implements Collections.IList.IsReadOnly
            Get
                Throw New NotImplementedException
            End Get
        End Property
        Public ReadOnly Property IsSynchronized As Boolean Implements Collections.ICollection.IsSynchronized
            Get
                Throw New NotImplementedException
            End Get
        End Property
        Public ReadOnly Property SyncRoot As Object Implements Collections.ICollection.SyncRoot
            Get
                Throw New NotImplementedException
            End Get
        End Property

#End Region

    End Class
End Namespace
