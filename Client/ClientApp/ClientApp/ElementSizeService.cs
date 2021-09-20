using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Xamarin.Essentials;

namespace ClientApp
{

    /**
     * ElementSizeService:
     * Calculates Height and Width of shown elements according to the screen size of the users device.
     **/
    class ElementSizeService
    {
        private Double screenHeight;
        private Double screenWidth;

        /**
         * On Initilization:
         * ScreenHeight and ScreenWidth are set from the MainDisplayInfo.
         **/ 
        public ElementSizeService()
        {
            this.screenHeight = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
            this.screenWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
        }

        /**
         * Calculates the Height of an Element as the percentage of the screen the element should use.
         **/
        public Double calculateElementHeight(Double percentage)
        {
            return screenHeight * percentage;
        }

        /**
         * Calculates the Width of an Element as the percentage of the screen the element should use.
         **/
        public Double calculateElementWidth(Double percentage)
        {
            return screenWidth * percentage;
        }

    }

}
