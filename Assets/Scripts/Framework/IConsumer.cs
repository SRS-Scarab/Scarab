#nullable enable

public interface IConsumer<in T>
{
    void Consume(T value);
}
