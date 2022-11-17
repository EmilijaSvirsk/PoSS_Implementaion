namespace PSP_Komanda32_API.Services.Interfaces
{
    public interface IRandomizer
    {
        T GenerateRandomData<T>(int? id = null) where T : class, new();
    }
}
