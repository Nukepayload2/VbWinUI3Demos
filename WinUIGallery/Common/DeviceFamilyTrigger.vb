' To configure or remove Option's included in result, go to Options/Advanced Options...
Imports Windows.System.Profile
Imports Microsoft.UI.Xaml

Namespace AppUIBasics
    ' https://docs.microsoft.com/windows/apps/design/devices/designing-for-tv#custom-visual-state-trigger-for-xbox
    Class DeviceFamilyTrigger
        Inherits StateTriggerBase
        Private _actualDeviceFamily As String
        Private _triggerDeviceFamily As String
        Public Property DeviceFamily As String
            Get
                Return _triggerDeviceFamily
            End Get

            Set(value As String)
                _triggerDeviceFamily = value
                _actualDeviceFamily = AnalyticsInfo.VersionInfo.DeviceFamily
                SetActive(_triggerDeviceFamily = _actualDeviceFamily)
            End Set
        End Property
    End Class
End Namespace
