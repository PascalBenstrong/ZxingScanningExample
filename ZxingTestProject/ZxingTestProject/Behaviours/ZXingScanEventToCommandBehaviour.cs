using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace ZxingTestProject.Behaviours
{
    public class ZXingScanEventToCommandBehaviour : Behavior<ZXingScannerView>
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.CreateAttached(
            "Command",
            typeof(ICommand),
            typeof(ZXingScannerView),
            default(ICommand),
            BindingMode.OneWay);

        public static ICommand GetCommand(BindableObject target)
        {
            return (ICommand)target.GetValue(CommandProperty);
        }

        public static void SetCommand(BindableObject target, ICommand value)
        {
            target.SetValue(CommandProperty, value);
        }


        private ZXingScannerView target;
        private void OnScanResult(ZXing.Result result)
        {
            var command = GetCommand(target);
            if (command is null || !command.CanExecute(result))
                return;

            command.Execute(result);
        }

        protected override void OnAttachedTo(ZXingScannerView bindable)
        {
            target = bindable;
            target.OnScanResult += OnScanResult;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(ZXingScannerView bindable)
        {
            target.OnScanResult -= OnScanResult;
            base.OnDetachingFrom(bindable);
        }
    }
}
