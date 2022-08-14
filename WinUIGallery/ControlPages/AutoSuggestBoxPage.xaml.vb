' To configure or remove Option's included in result, go to Options/Advanced Options...
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Linq
Imports System.Runtime.InteropServices.WindowsRuntime
Imports Windows.Foundation
Imports Windows.Foundation.Collections
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Controls.Primitives
Imports Microsoft.UI.Xaml.Data
Imports Microsoft.UI.Xaml.Input
Imports Microsoft.UI.Xaml.Media
Imports Microsoft.UI.Xaml.Navigation
Imports AppUIBasics.Data
Imports System.Threading.Tasks
Imports Microsoft.UI.Xaml.Media.Imaging

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class AutoSuggestBoxPage
        Inherits Page
        Private Cats As New List(Of String) From { _
            "Abyssinian",
            "Aegean",
            "American Bobtail",
            "American Curl",
            "American Ringtail",
            "American Shorthair",
            "American Wirehair",
            "Aphrodite Giant",
            "Arabian Mau",
            "Asian cat",
            "Asian Semi-longhair",
            "Australian Mist",
            "Balinese",
            "Bambino",
            "Bengal",
            "Birman",
            "Bombay",
            "Brazilian Shorthair",
            "British Longhair",
            "British Shorthair",
            "Burmese",
            "Burmilla",
            "California Spangled",
            "Chantilly-Tiffany",
            "Chartreux",
            "Chausie",
            "Colorpoint Shorthair",
            "Cornish Rex",
            "Cymric",
            "Cyprus",
            "Devon Rex",
            "Donskoy",
            "Dragon Li",
            "Dwelf",
            "Egyptian Mau",
            "European Shorthair",
            "Exotic Shorthair",
            "Foldex",
            "German Rex",
            "Havana Brown",
            "Highlander",
            "Himalayan",
            "Japanese Bobtail",
            "Javanese",
            "Kanaani",
            "Khao Manee",
            "Kinkalow",
            "Korat",
            "Korean Bobtail",
            "Korn Ja",
            "Kurilian Bobtail",
            "Lambkin",
            "LaPerm",
            "Lykoi",
            "Maine Coon",
            "Manx",
            "Mekong Bobtail",
            "Minskin",
            "Napoleon",
            "Munchkin",
            "Nebelung",
            "Norwegian Forest Cat",
            "Ocicat",
            "Ojos Azules",
            "Oregon Rex",
            "Oriental Bicolor",
            "Oriental Longhair",
            "Oriental Shorthair",
            "Persian (modern)",
            "Persian (traditional)",
            "Peterbald",
            "Pixie-bob",
            "Ragamuffin",
            "Ragdoll",
            "Raas",
            "Russian Blue",
            "Russian White",
            "Sam Sawet",
            "Savannah",
            "Scottish Fold",
            "Selkirk Rex",
            "Serengeti",
            "Serrade Petit",
            "Siamese",
            "Siberian or´Siberian Forest Cat",
            "Singapura",
            "Snowshoe",
            "Sokoke",
            "Somali",
            "Sphynx",
            "Suphalak",
            "Thai",
            "Thai Lilac",
            "Tonkinese",
            "Toyger",
            "Turkish Angora",
            "Turkish Van",
            "Turkish Vankedisi",
            "Ukrainian Levkoy",
            "Wila Krungthep",
            "York Chocolate"
        }

        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub AutoSuggestBox_TextChanged(sender As AutoSuggestBox, args As AutoSuggestBoxTextChangedEventArgs)
            ' Since selecting an item will also change the text,
            ' only listen to changes caused by user entering text.
            If args.Reason = AutoSuggestionBoxTextChangeReason.UserInput Then
                Dim suitableItems As List(Of String) = New List(Of String)
                Dim splitText = sender.Text.ToLower().Split(" ")
                For Each cat As String In Cats
                    Dim found As Boolean = splitText.All(Function(key) As Boolean
                                                             Return cat.ToLower().Contains(key)
                                                         End Function)
                    If found Then
                        suitableItems.Add(cat)
                    End If
                Next
                If suitableItems.Count = 0 Then
                    suitableItems.Add("No results found")
                End If
                sender.ItemsSource = suitableItems
            End If
        End Sub
        Private Sub AutoSuggestBox_SuggestionChosen(sender As AutoSuggestBox, args As AutoSuggestBoxSuggestionChosenEventArgs)
            SuggestionOutput.Text = args.SelectedItem.ToString()
        End Sub
        ''' <summary>
        ''' This event gets fired anytime the text in the TextBox gets updated.
        ''' It is recommended to check the reason for the text changing by checking against args.Reason
        ''' </summary>
        ''' <param name="sender">The AutoSuggestBox whose text got changed.</param>
        ''' <param name="args">The event arguments.</param>
        Private Sub Control2_TextChanged(sender As AutoSuggestBox, args As AutoSuggestBoxTextChangedEventArgs)
            'We only want to get results when it was a user typing,
            'otherwise we assume the value got filled in by TextMemberPath
            'or the handler for SuggestionChosen
            If args.Reason = AutoSuggestionBoxTextChangeReason.UserInput Then
                Dim suggestions As List(Of ControlInfoDataItem) = SearchControls(sender.Text)

                If suggestions.Count > 0 Then
                    sender.ItemsSource = suggestions
                Else sender.ItemsSource = New String() {"No results found"}
                End If
            End If
        End Sub
        ''' <summary>
        ''' This event gets fired when:
        '''     * a user presses Enter while focus is in the TextBox
        '''     * a user clicks or tabs to and invokes the query button (defined using the QueryIcon API)
        '''     * a user presses selects (clicks/taps/presses Enter) a suggestion
        ''' </summary>
        ''' <param name="sender">The AutoSuggestBox that fired the event.</param>
        ''' <param name="args">The args contain the QueryText, which is the text in the TextBox,
        ''' and also ChosenSuggestion, which is only non-null when a user selects an item in the list.</param>
        Private Sub Control2_QuerySubmitted(sender As AutoSuggestBox, args As AutoSuggestBoxQuerySubmittedEventArgs)
            If args.ChosenSuggestion IsNot Nothing AndAlso TypeOf args.ChosenSuggestion Is ControlInfoDataItem Then
                'User selected an item, take an action
                SelectControl(TryCast(args.ChosenSuggestion, ControlInfoDataItem))
            ElseIf Not String.IsNullOrEmpty(args.QueryText) Then
                'Do a fuzzy search based on the text
                Dim suggestions As List(Of ControlInfoDataItem) = SearchControls(sender.Text)
                If suggestions.Count > 0 Then
                    SelectControl(suggestions.FirstOrDefault())
                End If
            End If
        End Sub
        ''' <summary>
        ''' This event gets fired as the user keys through the list, or taps on a suggestion.
        ''' This allows you to change the text in the TextBox to reflect the item in the list.
        ''' Alternatively you can use TextMemberPath.
        ''' </summary>
        ''' <param name="sender">The AutoSuggestBox that fired the event.</param>
        ''' <param name="args">The args contain SelectedItem, which contains the data item of the item that is currently highlighted.</param>
        Private Sub Control2_SuggestionChosen(sender As AutoSuggestBox, args As AutoSuggestBoxSuggestionChosenEventArgs)
            'Don't autocomplete the TextBox when we are showing "no results"
            Dim TempVar As Boolean = TypeOf args.SelectedItem Is ControlInfoDataItem
            Dim control As ControlInfoDataItem = args.SelectedItem
            If TempVar Then
                sender.Text = control.Title
            End If
        End Sub
        ''' <summary>
        ''' This
        ''' </summary>
        ''' <param name="contact"></param>
        Private Sub SelectControl(control As ControlInfoDataItem)
            If control IsNot Nothing Then
                ControlDetails.Visibility = Visibility.Visible

                Dim image As New BitmapImage(New Uri(control.ImageIconPath))
                ControlImage.Source = image

                ControlTitle.Text = control.Title
                ControlSubtitle.Text = control.Subtitle
            End If
        End Sub
        Private Function SearchControls(query As String) As List(Of ControlInfoDataItem)
            Dim suggestions As List(Of ControlInfoDataItem) = New List(Of ControlInfoDataItem)

            Dim querySplit As String() = query.Split(" ")
            For Each group In ControlInfoDataSource.Instance.Groups
                Dim matchingItems = group.Items.Where( _
        Function(item) As Boolean
                        ' Idea: check for every word entered (separated by space) if it is in the name,  
                        ' e.g. for query "split button" the only result should "SplitButton" since its the only query to contain "split" and "button" 
                        ' If any of the sub tokens is not in the string, we ignore the item. So the search gets more precise with more words 
                        Dim flag As Boolean = item.IncludedInBuild
            For Each queryToken As String In querySplit
                            ' Check if token is not in string 
                            If item.Title.IndexOf(queryToken, StringComparison.CurrentCultureIgnoreCase) < 0 Then
                                ' Token is not in string, so we ignore this item. 
                                flag = False
                End If
            Next
            Return flag
        End Function)
                For Each item In matchingItems
                    suggestions.Add(item)
                Next
            Next
            Return suggestions.OrderByDescending(Function(i) i.Title.StartsWith(query, StringComparison.CurrentCultureIgnoreCase)).ThenBy(Function(i) i.Title).ToList()
        End Function
    End Class
End Namespace
