namespace S6_CSHARP_02.Models
{
    public class Bicycle : IVehicle
    {
        public int Speed { get; set; }
        public int Wheels { get; set; }
        public string Color { get; set; }

        public Bicycle()
        {
            Speed = 0;
            Wheels = 2;
            Color = "Blue";
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
