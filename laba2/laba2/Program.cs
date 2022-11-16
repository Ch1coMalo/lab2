using System;
using System.IO;
using System.Collections.Generic;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Specialty
{
    public string Name { get; set; }
    public int Experience { get; set; }
    public Doctor doctor { get; set; }
    public Specialty() { }
    public Specialty(string name, int experience, Doctor doctor)
    {
        Name = name;
        Experience = experience;
        this.doctor = doctor;  
    }
}
public class Doctor
{
    public string FIO { get; set; }

    public int Age { get; set; }
    public Doctor() { }
    public Doctor (string fio, int age )
    {
        FIO = fio;
        Age = age;
     }
}

public class JsonHandler<T> where T : class
{
    private string fileName;
    JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };


    public JsonHandler() { }

    public JsonHandler(string fileName)
    {
        this.fileName = fileName;
    }


    public void SetFileName(string fileName)
    {
        this.fileName = fileName;
    }

    public void Write(List<T> list)
    {
        string jsonString = JsonSerializer.Serialize(list, options);

        if (new FileInfo(fileName).Length == 0)
        {
            File.WriteAllText(fileName, jsonString);
        }
        else
        {
            Console.WriteLine("Specified path file is not empty");
        }
    }

    public void Delete()
    {
        File.WriteAllText(fileName, string.Empty);
    }

    public void Rewrite(List<T> list)
    {
        string jsonString = JsonSerializer.Serialize(list, options);

        File.WriteAllText(fileName, jsonString);
    }

    public void Read(ref List<T> list)
    {
        if (File.Exists(fileName))
        {
            if (new FileInfo(fileName).Length != 0)
            {
                string jsonString = File.ReadAllText(fileName);
                list = JsonSerializer.Deserialize<List<T>>(jsonString);
            }
            else
            {
                Console.WriteLine("Specified path file is empty");
            }
        }
    }

    public void OutputJsonContents()
    {
        string jsonString = File.ReadAllText(fileName);

        Console.WriteLine(jsonString);
    }

    public void OutputSerializedList(List<T> list)
    {
        Console.WriteLine(JsonSerializer.Serialize(list, options));
    }
}



class Program
{
    static void Main(string[] args)
    {
        List<Specialty> partsList = new List<Specialty>();

        JsonHandler<Specialty> partsHandler = new JsonHandler<Specialty>("partsFile.json");

        partsList.Add(new Specialty("Dentist", 5, new Doctor("Ivanov Sergey Ivanovich", 30)));
        partsList.Add(new Specialty("Dermatologist", 10, new Doctor("Seleznev Oleg Viktorovich", 45)));
       
        partsHandler.Rewrite(partsList);
        partsHandler.OutputJsonContents();
    }
}