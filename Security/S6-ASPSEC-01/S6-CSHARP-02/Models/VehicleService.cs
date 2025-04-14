namespace S6_CSHARP_02.Models
{
    public static class VehicleService
    {
        // Het opslaan van het voertuig in een statische variabele voor eenvoudiger gebruik (dit geldt alleen zolang de applicatie draait)
        private static IVehicle _currentVehicle;

        // Creëer het voertuig op basis van het type
        public static void CreateVehicle(string vehicleType)
        {
            if (vehicleType == "Car")
            {
                _currentVehicle = new Car();  // Maak een Car object aan
            }
            else if (vehicleType == "Bicycle")
            {
                _currentVehicle = new Bicycle();  // Maak een Bicycle object aan
            }
        }

        // Geef het huidige voertuig terug
        public static IVehicle GetVehicle()
        {
            return _currentVehicle;
        }

        // Verhoog de snelheid van het voertuig
        public static void IncreaseSpeed(int amount)
        {
            if (_currentVehicle != null)
            {
                _currentVehicle.IncreaseSpeed(amount);
            }
        }

        // Verlaag de snelheid van het voertuig
        public static void DecreaseSpeed(int amount)
        {
            if (_currentVehicle != null)
            {
                _currentVehicle.DecreaseSpeed(amount);
            }
        }
    }
}