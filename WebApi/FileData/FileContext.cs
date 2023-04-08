using System.Text.Json;
using Shared.Models;

namespace FileData; 

public class FileContext {
    private const string filePath = "data.json";
    private DataContainer? dataContainer;

    public ICollection<User> Users {
        get {
            LoadData();
            return dataContainer!.Users;
        }
    }

    private void LoadData() {
        if (dataContainer != null) {
            return;
        }

        if (!File.Exists(filePath)) {
            dataContainer = new DataContainer() {
                Users = new List<User>()
            };
            return;
        }

        string content = File.ReadAllText(filePath);
        dataContainer = JsonSerializer.Deserialize<DataContainer>(content);
    }

    //The purpose of this method is to take the content of the DataContainer field, and put into the file.
    public void SaveChanges() {
        string serialized = JsonSerializer.Serialize(dataContainer, new JsonSerializerOptions{
            WriteIndented = true
        });
        
        File.WriteAllText(filePath,serialized);
        dataContainer = null;
    }
}