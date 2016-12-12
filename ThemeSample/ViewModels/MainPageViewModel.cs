using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ThemeSample.Resources;

namespace ThemeSample.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private double _BmiValue;
        public double BmiValue {
            get { return _BmiValue; }
            set { SetProperty(ref _BmiValue, value); }
        }

        private string _IndexText;
        public string IndexText {
            get { return _IndexText; }
            set { SetProperty(ref _IndexText, value); }
        }

        private bool _ShowIndex;
        public bool ShowIndex {
            get { return _ShowIndex; }
            set { SetProperty(ref _ShowIndex, value); }
        }

        private double _Height;
        public double Height {
            get { return _Height; }
            set {
                SetProperty(ref _Height, value);
                RealHeight = value * 190 + 30;
            }
        }

        private double _Weight;
        public double Weight {
            get { return _Weight; }
            set {
                SetProperty(ref _Weight, value);
                RealWeight = value * 145 + 5;
            }
        }

        private double _RealHeight;
        public double RealHeight {
            get { return _RealHeight; }
            set { SetProperty(ref _RealHeight, value); }
        }

        private double _RealWeight;
        public double RealWeight {
            get { return _RealWeight; }
            set { SetProperty(ref _RealWeight, value); }
        }

        public MainPageViewModel()
        {
            Height = 0.5;
            Weight = 0.5;
            BmiValue = 0;
            IndexText = "";
            ShowIndex = true;
        }

        private DelegateCommand _AlterThemeCommand;
        public DelegateCommand AlterThemeCommand {
            get {
                return _AlterThemeCommand = _AlterThemeCommand ?? new DelegateCommand(() => {
                    var app = Xamarin.Forms.Application.Current;
                    var parent = Xamarin.Forms.Application.Current.MainPage.Parent;
                    app.MainPage.Parent = null;
                    if (app.Resources.GetType() == typeof(AppTheme)) {
                        app.Resources = new OtherTheme();
                    }
                    else {
                        app.Resources = new AppTheme();
                    }

                    app.MainPage.Parent = parent;
                });
            }
        }

        private DelegateCommand _CalculateCommand;
        public DelegateCommand CalculateCommand {
            get {
                return _CalculateCommand = _CalculateCommand ?? new DelegateCommand(() => {
                    BmiValue = RealWeight / Math.Pow(RealHeight / 100d, 2);
                    if (BmiValue < 18.5) {
                        IndexText = "瘦せ型";
                    }
                    else if (BmiValue < 25.0) {
                        IndexText = "標準";
                    }
                    else if (BmiValue < 30.0) {
                        IndexText = "肥満（1度）";
                    }
                    else if (BmiValue < 35.0) {
                        IndexText = "肥満（2度）";
                    }
                    else if (BmiValue < 40.0) {
                        IndexText = "肥満（3度）";
                    }
                    else {
                        IndexText = "肥満（4度）";
                    }
                });
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {

        }
    }
}

