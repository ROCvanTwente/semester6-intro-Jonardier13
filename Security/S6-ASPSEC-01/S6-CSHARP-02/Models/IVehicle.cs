namespace S6_CSHARP_02.Models
{
    public interface IVehicle
    {
        int Speed { get; set; }
        int Wheels { get; set; }
        string Color { get; set; }

        void IncreaseSpeed(int amount);
        void DecreaseSpeed(int amount);
        int GetCurrentSpeed();

    }
}
