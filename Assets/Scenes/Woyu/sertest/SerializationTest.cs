#nullable enable
using Newtonsoft.Json;
using UnityEngine;

public class SerializationTest : MonoBehaviour
{
    [SerializeField]
    private SaveablePrefabList prefabList = null!;

    [SerializeField]
    private SaveablePrefab red = null!;
    
    [SerializeField]
    private SaveablePrefab blue = null!;

    [SerializeField]
    [TextArea(10, 20)]
    private string serialized = string.Empty;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            var data = SaveUtility.Save("test");
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.All
            };
            serialized = JsonConvert.SerializeObject(data, Formatting.Indented, settings);
        }
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.All
            };
            var data = JsonConvert.DeserializeObject<SaveDataBase>(serialized, settings);
            SaveUtility.Clear();
            SaveUtility.Load(data!, prefabList);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Instantiate(red.prefab, new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Instantiate(blue.prefab, new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            SaveUtility.Clear();
        }
    }
}
