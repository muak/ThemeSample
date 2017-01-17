using System;
using NUnit.Framework;
using Xamarin.UITest;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Linq;
using Xamarin.UITest.Configuration;

namespace ThemeSample.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class MainPageFixture
    {
        IApp app;
        Platform platform;

        public MainPageFixture(Platform platform)
        {
            this.platform = platform;
        }

        [TestFixtureSetUp]
        public void AppStart()
        {
            app = AppInitializer.StartApp(platform);
            //最初のページが表示されるまで待つ
            app.WaitForElement("MainPage", "Timeout", TimeSpan.FromSeconds(10));
        }

        [Test]
        public void T01_DefaultBmi()
        {
            var ret = app.Query("BmiLabel")[0];
            Assert.AreEqual("0.0",ret.Text);
        }

        [Test]
        public void T02_DefaultIndex()
        {
            var ret = app.Query("IndexLabel")[0];
            Assert.IsNullOrEmpty(ret.Text);
        }

        [Test]
        public async Task T03_HeightSliderTest()
        {
            app.ScrollDownTo("HeightSlider");
            await Task.Delay(250);
            app.SetSliderValue("HeightSlider",0d);
            Assert.AreEqual("30cm",app.Query("HeightText")[0].Text);
            app.SetSliderValue("HeightSlider", OnPlatform(1.0d,1000d));
            Assert.AreEqual("220cm", app.Query("HeightText")[0].Text);
        }

        [Test]
        public void T04_WeightSliderTest()
        {
            app.ScrollDownTo("WeightSlider");
            app.SetSliderValue("WeightSlider", 0d);
            Assert.AreEqual("5kg", app.Query("WeightText")[0].Text);
            app.SetSliderValue("WeightSlider", OnPlatform(1.0d, 1000d));
            Assert.AreEqual("150kg", app.Query("WeightText")[0].Text);
        }

        [Test]
        public async Task T05_IndexSwitchIsOn()
        {
            //ON->OFF
            app.ScrollDownTo("IndexToggleSwitch");
            app.Tap("IndexToggleSwitch");
            app.ScrollUpTo("BmiLabel");
            await Task.Delay(250);
            Assert.AreEqual(0,app.Query("IndexLabel").Count());

            //OFF->ON
            app.ScrollDownTo("IndexToggleSwitch");
            app.Tap("IndexToggleSwitch");
            app.ScrollUpTo("BmiLabel");
            await Task.Delay(250);
            Assert.AreEqual(1, app.Query("IndexLabel").Count());
        }

        [Test]
        public async Task T06_CalculateTest()
        {
            app.ScrollDownTo("CalculateButton");
            app.Tap("CalculateButton");
            await Task.Delay(250);

            Assert.AreNotEqual("0.0",app.Query("BmiLabel")[0].Text);
            Assert.IsNotNullOrEmpty(app.Query("IndexLabel")[0].Text);
        }

        [Test]
        public async Task T07_ThemeChangeTest()
        {
            app.Screenshot("Before");
            app.Tap(OnPlatform("AlterThemeButton","切替"));
            await Task.Delay(250);
            app.Screenshot("After");
        }

        T OnPlatform<T>(T iOS, T android)
        {
            if (platform == Platform.iOS) {
                return iOS;
            }
            else {
                return android;
            }
        }

    }
}
