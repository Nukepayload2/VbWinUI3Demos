' Copyright (c) Microsoft Corporation. All rights reserved.
' Licensed under the MIT License.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

#Const INDEI = True

Imports System.Numerics
Imports System.Threading.Tasks
Imports Windows.Foundation
Imports Microsoft.UI.Composition
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports System.Runtime.CompilerServices
Imports System.Collections.Generic
Imports System.Linq
Imports System.ComponentModel.DataAnnotations
Imports System.Collections

#If Not UNIVERSAL

Imports System.ComponentModel
#Else

using Microsoft.UI.Xaml.Data;

#End If

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class InputValidationPage
        Inherits Page
        Public Sub New()
            ViewModel = New PurchaseViewModel
            Me.InitializeComponent()
        End Sub
#Region "ViewModelProperty"

        Public Shared ReadOnly ViewModelProperty As DependencyProperty = DependencyProperty.Register("ViewModel", GetType(PurchaseViewModel), GetType(InputValidationPage), New PropertyMetadata(Nothing))
        Public Property ViewModel As PurchaseViewModel
            Get
                Return TryCast(GetValue(ViewModelProperty), PurchaseViewModel)
            End Get

            Set(value As PurchaseViewModel)
                SetValue(ViewModelProperty, value)
            End Set
        End Property
#End Region

        Private Sub UserControl_Loaded(sender As Object, e As RoutedEventArgs)
            'CardHolderNameField.Focus(FocusState.Programmatic);
            'PlayEntranceTransition();
            ' 
            ' Microsoft.UI.Xaml.Controls.InputValidationError err = new Microsoft.UI.Xaml.Controls.InputValidationError("testing");
            ' Windows.Foundation.Collections.IObservableVector<InputValidationError> validationErrors = CardHolderNameField.ValidationErrors;
            ' Windows.Foundation.Collections.IObservableVector<Microsoft.UI.Xaml.Controls.InputValidationError> errorsCasted = Shim.ReinterpretCast<Windows.Foundation.Collections.IObservableVector<Microsoft.UI.Xaml.Controls.InputValidationError>>(validationErrors);
            ' errorsCasted.Clear();
            ' var errCasted = Shim.ReinterpretCast<Microsoft.UI.Xaml.Controls.InputValidationError>(err);
            ' errorsCasted.Add(errCasted);
            ' 
        End Sub

        ' 
        ' private void PlayEntranceTransition()
        ' {
        ' var compositor = XamlRoot.Compositor;
        ' 
        ' var easingFunction = compositor.CreateCubicBezierEasingFunction(new Vector2(0.1f, 0.9f), new Vector2(0.2f, 1f));
        ' var duration = TimeSpan.FromMilliseconds(300);
        ' 
        ' var backdropVisual = ElementCompositionPreview.GetElementVisual(Backdrop);
        ' var purchaseFormVisual = ElementCompositionPreview.GetElementVisual(PurchaseForm);
        ' 
        ' // Initial conditions
        ' backdropVisual.Opacity = 0f;
        ' purchaseFormVisual.Opacity = 0f;
        ' 
        ' // Set up animations
        ' var backdropVisualOpacityAnim = compositor.CreateScalarKeyFrameAnimation();
        ' backdropVisualOpacityAnim.Target = "Opacity";
        ' backdropVisualOpacityAnim.Duration = duration;
        ' backdropVisualOpacityAnim.InsertKeyFrame(1f, 1f, easingFunction);
        ' 
        ' var purchaseFormVisualOpacityAnim = compositor.CreateScalarKeyFrameAnimation();
        ' purchaseFormVisualOpacityAnim.Target = "Opacity";
        ' purchaseFormVisualOpacityAnim.Duration = duration;
        ' purchaseFormVisualOpacityAnim.InsertKeyFrame(1f, 1f, easingFunction);
        ' 
        ' // Start animations
        ' backdropVisual.StartAnimation(backdropVisualOpacityAnim.Target, backdropVisualOpacityAnim);
        ' purchaseFormVisual.StartAnimation(purchaseFormVisualOpacityAnim.Target, purchaseFormVisualOpacityAnim);
        ' }
        ' 
        ' public override Task PlayExitTransition()
        ' {
        ' var compositor = XamlRoot.Compositor;
        ' 
        ' var easingFunction = compositor.CreateCubicBezierEasingFunction(new Vector2(0.7f, 0.0f), new Vector2(1.0f, 0.5f));
        ' var duration = TimeSpan.FromMilliseconds(500);
        ' 
        ' var backdropVisual = ElementCompositionPreview.GetElementVisual(Backdrop);
        ' var purchaseFormVisual = ElementCompositionPreview.GetElementVisual(PurchaseForm);
        ' 
        ' // Set up animations
        ' var backdropVisualOpacityAnim = compositor.CreateScalarKeyFrameAnimation();
        ' backdropVisualOpacityAnim.Target = "Opacity";
        ' backdropVisualOpacityAnim.Duration = duration;
        ' backdropVisualOpacityAnim.InsertKeyFrame(1f, 0f, easingFunction);
        ' 
        ' var purchaseFormVisualOpacityAnim = compositor.CreateScalarKeyFrameAnimation();
        ' purchaseFormVisualOpacityAnim.Target = "Opacity";
        ' purchaseFormVisualOpacityAnim.Duration = duration;
        ' purchaseFormVisualOpacityAnim.InsertKeyFrame(1f, 0f, easingFunction);
        ' 
        ' // Start animations in scoped batch
        ' 
        ' var scopedBatch = compositor.CreateScopedBatch(Windows.UI.Composition.CompositionBatchTypes.Animation);
        ' 
        ' backdropVisual.StartAnimation(backdropVisualOpacityAnim.Target, backdropVisualOpacityAnim);
        ' purchaseFormVisual.StartAnimation(purchaseFormVisualOpacityAnim.Target, purchaseFormVisualOpacityAnim);
        ' 
        ' scopedBatch.End();
        ' 
        ' // Set up task completion
        ' 
        ' TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
        ' TypedEventHandler<object, CompositionBatchCompletedEventArgs> completedHandler = null;
        ' completedHandler = new TypedEventHandler<object, CompositionBatchCompletedEventArgs>((sender, args) =>
        ' {
        ' scopedBatch.Completed -= completedHandler;
        ' tcs.SetResult(null);
        ' });
        ' scopedBatch.Completed += completedHandler;
        ' 
        ' return tcs.Task;
        ' }
        ' 
    End Class


    Public Class PurchaseViewModel
        Implements INotifyPropertyChanged, INotifyDataErrorInfo
        Public Sub New()
        End Sub
        Private _name As String
        <ComponentModel.DefaultValue("")> _
        <MinLength(5, ErrorMessage:="Name must be more than 4 characters")>
        Public Property Name As String
            Get
                Return _name
            End Get

            Set(value As String)
                SetValue(_name, value)
            End Set
        End Property
        Private _cardNumber As String
        <CreditCard>
        Public Property CardNumber As String
            Get
                Return _cardNumber
            End Get

            Set(value As String)
                SetValue(_cardNumber, value)
            End Set
        End Property
        Private _address As String
        Public Property Address As String
            Get
                Return _address
            End Get

            Set(value As String)
                SetValue(_address, value)
            End Set
        End Property
        Private _city As String
        Public Property City As String
            Get
                Return _city
            End Get

            Set(value As String)
                SetValue(_city, value)
            End Set
        End Property
        Private _zip As String
        <CustomValidation(GetType(PurchaseViewModel), "ValidateZip")>
        Public Property Zip As String
            Get
                Return _zip
            End Get

            Set(value As String)
                SetValue(_zip, value)
            End Set
        End Property
        Private _cardExpirationMonth As String
        Public Property CardExpirationMonth As String
            Get
                Return _cardExpirationMonth
            End Get

            Set(value As String)
                SetValue(_cardExpirationMonth, value)
            End Set
        End Property
        Private _cardExpirationYear As String
        <CustomValidation(GetType(PurchaseViewModel), "ValidateYear")>
        Public Property CardExpirationYear As String
            Get
                Return _cardExpirationYear
            End Get

            Set(value As String)
                SetValue(_cardExpirationYear, value)
            End Set
        End Property
        Private _ccv As String
        Public Property CCV As String
            Get
                Return _ccv
            End Get

            Set(value As String)
                SetValue(_ccv, value)
            End Set
        End Property
        Private _billingAddress As String
        Public Property BillingAddress As String
            Get
                Return _billingAddress
            End Get

            Set(value As String)
                SetValue(_billingAddress, value)
            End Set
        End Property
        Private _billingCity As String
        Public Property BillingCity As String
            Get
                Return _billingCity
            End Get

            Set(value As String)
                SetValue(_billingCity, value)
            End Set
        End Property
        Private _billingZip As String
        <CustomValidation(GetType(PurchaseViewModel), "ValidateZip")>
        Public Property BillingZip As String
            Get
                Return _billingZip
            End Get

            Set(value As String)
                SetValue(_billingZip, value)
            End Set
        End Property

        Public Event PropertyChanged As PropertyChangedEventHandler Implements ComponentModel.INotifyPropertyChanged.PropertyChanged
        Private Sub NotifyPropertyChanged(<CallerMemberName> Optional propertyName As String = "")
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
#Region "INPC/INDEI"

        Private Sub SetValue(Of T)(ByRef currentValue As T, newValue As T, <CallerMemberName> Optional propertyName As String = "")
            If Not EqualityComparer(Of T).[Default].Equals(currentValue, newValue) Then
                currentValue = newValue
                NotifyPropertyChanged(propertyName)
                OnPropertyChanged(newValue, propertyName)
            End If
        End Sub
        Private _errors As New Dictionary(Of String, List(Of ComponentModel.DataAnnotations.ValidationResult))
        Public ReadOnly Property HasErrors As Boolean Implements ComponentModel.INotifyDataErrorInfo.HasErrors
            Get
                Return _errors.Any()
            End Get
        End Property

        Public Event ErrorsChanged As EventHandler(Of DataErrorsChangedEventArgs) Implements ComponentModel.INotifyDataErrorInfo.ErrorsChanged
        Private Sub OnPropertyChanged(value As Object, propertyName As String)
            ClearErrors(propertyName)
            Dim results As Collections.Generic.List(Of System.ComponentModel.DataAnnotations.ValidationResult) = New List(Of ComponentModel.DataAnnotations.ValidationResult)
            Dim result As Boolean = Validator.TryValidateProperty( _
                value,
New ValidationContext(Me, Nothing, Nothing) With
                { _
            .MemberName = propertyName
                },
                results _
                )

            If Not result Then
                AddErrors(propertyName, results)
            End If
        End Sub
        Public Shared Function ValidateZip(zip1 As String) As ValidationResult
            If Not zip1.Any(Function(x) Char.IsLetter(x)) Then
                Return ValidationResult.Success
            Else
                Return New ValidationResult( _
        "Zip code must contain numbers only")
            End If
        End Function
        Public Shared Function ValidateYear(year As String) As ValidationResult
            ' This code is valid until the year 10000.
            If year.Length <> 4 Then
                Return New ValidationResult("Year must be in format XXXX")
            End If

            If DateTime.Now.Year <= Integer.Parse(year) Then
                Return ValidationResult.Success
            Else
                Return New ValidationResult( _
        "Year must be on or after the current year.")
            End If
        End Function
        Private Sub AddErrors(propertyName As String, results As IEnumerable(Of ValidationResult))
            Dim errors As List(Of ValidationResult) = Nothing
            If Not _errors.TryGetValue(propertyName, errors) Then
                errors = New List(Of ValidationResult)
                _errors.Add(propertyName, errors)
            End If

            errors.AddRange(results)
            ErrorsChanged?.Invoke(Me, New DataErrorsChangedEventArgs(propertyName))
        End Sub
        Private Sub ClearErrors(propertyName As String)
            Dim errors As Collections.Generic.List(Of System.ComponentModel.DataAnnotations.ValidationResult) = Nothing
            If _errors.TryGetValue(propertyName, errors) Then
                errors.Clear()
                ErrorsChanged?.Invoke(Me, New DataErrorsChangedEventArgs(propertyName))
            End If
        End Sub
#If Not UNIVERSAL

        Public Function GetErrors(propertyName As String) As IEnumerable Implements ComponentModel.INotifyDataErrorInfo.GetErrors
#Else

        public IEnumerable<object> GetErrors(string propertyName)

#End If

            Return _errors(propertyName)
        End Function

#End Region

    End Class
End Namespace
