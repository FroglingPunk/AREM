public interface IInitializationRequirable
{
    bool IsInit { get; set; }

    void Init();
}