#nullable enable
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var data = SaveUtility.Save("save");
        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            PreserveReferencesHandling = PreserveReferencesHandling.All
        };
        var serialized = JsonConvert.SerializeObject(data, Formatting.Indented, settings);

        var fileInfo = new FileInfo(Application.persistentDataPath + "/save.txt");
        if (!fileInfo.Exists)
        {
            var stream = fileInfo.Create();
            stream.Close();
        }
        
        File.WriteAllText(fileInfo.FullName, serialized);
    }
}
