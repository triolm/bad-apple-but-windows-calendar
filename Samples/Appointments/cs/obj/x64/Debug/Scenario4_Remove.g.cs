﻿#pragma checksum "C:\Users\maris\Documents\Code\mathstuff\badapple-calendar\Samples\Appointments\shared\Scenario4_Remove.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5BC65C50734B89379ACCF857D8C4D4A6D4163070FC1538B67584CAC6CE0986E4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SDKTemplate
{
    partial class Scenario4_Remove : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 0.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Scenario4_Remove.xaml line 22
                {
                    this.RootGrid = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 3: // Scenario4_Remove.xaml line 36
                {
                    this.ApointmentIdLabelTextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 4: // Scenario4_Remove.xaml line 39
                {
                    this.AppointmentIdTextBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 5: // Scenario4_Remove.xaml line 40
                {
                    this.InstanceStartDateLabelTextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 6: // Scenario4_Remove.xaml line 43
                {
                    this.InstanceStartDateCheckBox = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
                }
                break;
            case 7: // Scenario4_Remove.xaml line 44
                {
                    this.InstanceStartDateDatePicker = (global::Windows.UI.Xaml.Controls.DatePicker)(target);
                }
                break;
            case 8: // Scenario4_Remove.xaml line 45
                {
                    this.RemoveAppointmentButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.RemoveAppointmentButton).Click += this.Remove_Click;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 0.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
