using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using PiCamCV;
using PiCamCV.ConsoleApp.Runners.PanTilt;
using PiCamCV.ConsoleApp.Runners.PanTilt.MoveStrategies;
using PiCamCV.UnitTests;

namespace UnitTests
{
    public class CameraModifierStrategyFixture : Fixture
    {
        [Test]
        public void CalculateNewSettingCalibratedResolution()
        {
            var target = new Point(160, 120);
            var objective = new Point(128, 120);
            var config = new CaptureConfig{Width=320, Height=240};
            var moqScreen = new Mock<IScreen>();
            var strategy = new LinearRegressionModifierStrategy(config, target);

            strategy.Objective = objective;
            
            var setting = new PanTiltSetting(50,50);

            var newSetting = strategy.CalculateNewSetting(setting);

            // AssertBuilder.Generate(newSetting, "newSetting"); // The following assertions were generated on 07-Mar-2015
            #region CodeGen Assertions
            Assert.AreEqual(53.0720m, newSetting.PanPercent);
            Assert.AreEqual(50m, newSetting.TiltPercent);
            #endregion
        }

        //[Test]
        //public void CalculateNewSettingSmallerResolution()
        //{
        //    var config = new CaptureConfig { Width = 96, Height = 48 };
        //    var target = config.;
        //    var objective = new Point(128, 120);
        //    var moqScreen = new Mock<IScreen>();
        //    var strategy = new CameraModifierStrategy(config, moqScreen.Object, objective, target);

        //    var setting = new PanTiltSetting(50, 50);

        //    var newSetting = strategy.CalculateNewSetting(setting);

        //     AssertBuilder.Generate(newSetting, "newSetting"); // The following assertions were generated on 07-Mar-2015
        //    #region CodeGen Assertions
        //    Assert.AreEqual(53.0720m, newSetting.PanPercent);
        //    Assert.AreEqual(50m, newSetting.TiltPercent);
        //    #endregion
        //}
    }

}
