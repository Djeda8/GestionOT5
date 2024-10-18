using GestionOT5.MVVM.ViewModels.AppShell;
using System.Diagnostics;

namespace GestionOT5
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
        protected async override void OnNavigated(ShellNavigatedEventArgs args)
        {
            base.OnNavigated(args);
            string source = args.Source.ToString();
            string previous = args.Previous != null ? args.Previous.Location.ToString() : "";
            string current = args.Current != null ? args.Current.Location.ToString() : "";

            Debug.WriteLine($"--- A OnNavigated was performed: {source}, from {previous} to {current}");
        }

        protected async override void OnNavigating(ShellNavigatingEventArgs args)
        {
            base.OnNavigating(args);
            string source = args.Source.ToString();
            string current = args.Current != null ? args.Current.Location.ToString(): "";  //.Split('/').LastOrDefault() 
            string target = args.Target != null ? args.Target.Location.ToString() : "";

            Debug.WriteLine($"--- A OnNavigating was performed: {source}, from {current} to {target}");
        }

    }
}
