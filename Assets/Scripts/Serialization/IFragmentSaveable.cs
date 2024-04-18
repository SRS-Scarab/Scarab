#nullable enable
public interface IFragmentSaveable
{
    public string GetId();
    
    public SaveFragmentBase Save();

    public void Load(SaveFragmentBase fragment);
}
