#nullable enable
public class TransformFragmentSource : SerializationFragmentSource
{
    public override SerializationFragment Generate()
    {
        return new TransformFragment(transform);
    }
}