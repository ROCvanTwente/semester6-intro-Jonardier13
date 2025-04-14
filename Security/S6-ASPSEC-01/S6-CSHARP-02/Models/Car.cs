namespace S6_CSHARP_02.Models
{
    public class Car : IVehicle
    {
        public int Speed { get; set; }
        public int Wheels { get; set; }
        public string Color { get; set; }

        public Car()
        {
            Speed = 0;
            Wheels = 4;
            Color = "Red";
        }

        public void IncreaseSpeed(int amount)
        {
            Speed += amount;
        }

        public void DecreaseSpeed(int amount)
        {
            Speed = (Speed - amount < 0) ? 0 : Speed - amount;
        }

        public int GetCurrentSpeed()
        {
            return Speed;
        }
    }

}
