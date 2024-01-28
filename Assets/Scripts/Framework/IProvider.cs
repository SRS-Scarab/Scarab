#nullable enable

public interface IProvider<out T>
{
    T Provide();
}
