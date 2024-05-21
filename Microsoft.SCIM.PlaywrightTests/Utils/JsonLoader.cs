namespace PlaywrightTests.Utils;

public class JsonLoader
{
    public static async Task<string> LoadJsonDataAsync(string filePath, string variable, string value)
    {
        // Read the content of the JSON file
        string jsonContent = await File.ReadAllTextAsync(filePath);

        // Replace the placeholder with the actual value
        jsonContent = jsonContent.Replace("{{" + variable + "}}", value);

        // Parse the JSON content into a dynamic object
        return jsonContent;
    }

    public static async Task<string> LoadJsonDataAsync(string filePath)
    {
        // Read the content of the JSON file
        return await File.ReadAllTextAsync(filePath);
    }
}
