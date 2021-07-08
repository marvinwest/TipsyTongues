using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Xamarin.Essentials;

namespace ClientApp
{
    class ElementSizeService
    {
        private Double screenHeight;
        private Double screenWidth;

        

        public ElementSizeService()
        {
            this.screenHeight = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
            this.screenWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
        }

        public Double calculateElementHeight(Double percentage)
        {
            return screenHeight * percentage;
        }

        public Double calculateElementWidth(Double percentage)
        {
            return screenWidth * percentage;
        }

    }

}
