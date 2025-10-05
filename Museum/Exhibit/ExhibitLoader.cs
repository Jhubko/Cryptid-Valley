using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class ExhibitLoader
{
    private static string GetInventoryPath()
    {
        // folder dokumenty użytkownika
        string docs = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        string folder = Path.Combine(docs, "CryptidValley");
        DeleteFile(Path.Combine(folder, "inventory.json"));
        // jeśli folder nie istnieje, utwórz go
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        // pełna ścieżka do pliku JSON
        return Path.Combine(folder, "inventory.json");
    }
    public static void DeleteFile(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
            GD.Print($"Plik {path} został usunięty.");
        }
        else
        {
            GD.Print($"Plik {path} nie istnieje – nic do usunięcia.");
        }
    }
    public static List<Exhibit> LoadFromFile()
    {
        var path = GetInventoryPath();
        var exhibits = new List<Exhibit>();

        if (!File.Exists(path))
        {
            GD.Print("Plik nie istnieje, zostanie utworzony przy zapisie.");
            return exhibits;
        }

        string jsonText = File.ReadAllText(path);
        var items = JsonSerializer.Deserialize<List<ExhibitData>>(jsonText);

        if (items == null) return exhibits;

        foreach (var item in items)
        {
            var icon = GD.Load<Texture2D>(item.IconPath);
            exhibits.Add(new Exhibit(item.Name, item.Description, item.Attractiveness,
                                     Enum.Parse<CryptidType>(item.Cryptid),
                                     Enum.Parse<ExhibitObjectType>(item.ObjectType),
                                     icon));
        }

        return exhibits;
    }

    public static void SaveToFile(List<Exhibit> exhibits)
    {
        var path = GetInventoryPath();
        var dataList = new List<ExhibitData>();

        foreach (var ex in exhibits)
        {
            dataList.Add(new ExhibitData
            {
                Name = ex.Name,
                Description = ex.Description,
                Attractiveness = ex.Attractiveness,
                Cryptid = ex.Cryptid.ToString(),
                ObjectType = ex.ObjectType.ToString(),
                IconPath = "res://icon.svg"
            });
        }

        string jsonText = JsonSerializer.Serialize(dataList, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(path, jsonText);
    }

    private class ExhibitData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Attractiveness { get; set; }
        public string Cryptid { get; set; }
        public string ObjectType { get; set; }
        public string IconPath { get; set; }
    }
}
