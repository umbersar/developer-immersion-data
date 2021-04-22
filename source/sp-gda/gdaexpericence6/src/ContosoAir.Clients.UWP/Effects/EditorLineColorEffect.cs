﻿using ContosoAir.Clients.Effects;
using ContosoAir.Clients.UWP.Effects;
using System;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using UI = Windows.UI;
using Xaml = Windows.UI.Xaml;
using Media = Windows.UI.Xaml.Media;
using System.ComponentModel;

[assembly: ExportEffect(typeof(EditorLineColorEffect), "EditorLineColorEffect")]
namespace ContosoAir.Clients.UWP.Effects
{
    public class EditorLineColorEffect : PlatformEffect
    {
        TextBox control;

        protected override void OnAttached()
        {
            try
            {
                control = Control as TextBox;
                UpdateLineColor();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
            control = null;
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName == LineColorEffect.LineColorProperty.PropertyName)
            {
                UpdateLineColor();
            }
        }

        private void UpdateLineColor()
        {
            if (control != null)
            {
                control.BorderThickness = new Xaml.Thickness(0, 0, 0, 1);
                var lineColor = XamarinFormColorToWindowsColor(LineColorEffect.GetLineColor(Element));
                control.BorderBrush = new Media.SolidColorBrush(lineColor);

                var style = lineColor.ToString() == "#FFFFFFFF" ? App.Current.Resources["LoginTextBoxStyle"] as Xaml.Style : App.Current.Resources["FormTextBoxStyle"] as Xaml.Style;
                control.Style = style;
            }
        }

        private UI.Color XamarinFormColorToWindowsColor(Color xamarinColor)
        {
            return UI.Color.FromArgb((byte)(xamarinColor.A * 255),
                                     (byte)(xamarinColor.R * 255),
                                     (byte)(xamarinColor.G * 255),
                                     (byte)(xamarinColor.B * 255));
        }
    }
}