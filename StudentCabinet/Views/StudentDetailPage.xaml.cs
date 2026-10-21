using Java.Lang;
using StudentCabinet.ViewModels;
using static Android.Icu.Text.CaseMap;
using static System.Net.Mime.MediaTypeNames;

<? xml version = "1.0" encoding = "utf-8" ?>
< ContentPage xmlns = "http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns: x = "http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns: vm = "clr-namespace:StudentCabinet.ViewModels"
             x: Class = "StudentCabinet.Views.StudentDetailPage"
             Title = "Деталі студента" >

    < ContentPage.BindingContext >
        < vm:StudentDetailViewModel />
    </ ContentPage.BindingContext >

    < VerticalStackLayout Padding = "20" Spacing = "12" >
        < Label Text = "{Binding FullName, StringFormat='ПІБ: {0}'}" FontSize = "20" FontAttributes = "Bold" />
        < Label Text = "{Binding Group, StringFormat='Група: {0}'}" FontSize = "18" />

        < Label Text = "{Binding AverageScore, StringFormat='Середній бал: {0:F1}'}" FontSize = "18" />
        < Stepper Minimum = "0" Maximum = "5" Increment = "0.1"
                 Value = "{Binding AverageScore, Mode=TwoWay}" />

        < Button Text = "Зберегти" Command = "{Binding SaveCommand}" />
        < Button Text = "Назад" Command = "{Binding GoBackCommand}" />
    </ VerticalStackLayout >
</ ContentPage >