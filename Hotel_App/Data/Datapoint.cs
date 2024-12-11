﻿namespace Hotel_App.Data
{
    public class DataPoint
    {
        public string Country { get; set; }
        public double AppleProduction { get; set; }
        public DataPoint(string country, double appleProduction)
        {
            Country = country;
            AppleProduction = appleProduction;
        }
    }
}
