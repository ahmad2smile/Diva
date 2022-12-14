using System.Text;

namespace SourceGenerators.Utils;

public class TemplatesUtils
{
    public static string TemplateFileToString<T>()
    {
        // NOTE:
        // Assumption 1: Templates are in directory valid namespace
        // Assumption 2: Templates filenames are same as Type name
        // var resource = $"{typeof(T).Namespace}.{nameof(T)}.cs";
        var resource = $"SourceGenerators.Templates.RestApi.{nameof(T)}.cs";

        using var stream = typeof(TemplatesUtils).Assembly.GetManifestResourceStream(resource);

        if (stream is null)
        {
            throw new InvalidOperationException($"Template file {resource} not found");
        }

        using var reader = new StreamReader(stream, Encoding.UTF8);

        var fileContent = reader.ReadToEnd();

        if (string.IsNullOrWhiteSpace(fileContent))
        {
            throw new InvalidOperationException($"Template file {resource} is empty");
        }

        return fileContent;
    }
    
    public static string TemplateFileToString(string fileName, string fileNamespace)
    {
        // NOTE:
        // Assumption 1: Templates are in directory valid namespace
        // Assumption 2: Templates filenames are same as Type name
        // var resource = $"{typeof(T).Namespace}.{nameof(T)}.cs";
        var resource = $"SourceGenerators.Templates.RestApi.{fileName}.cs";

        using var stream = typeof(TemplatesUtils).Assembly.GetManifestResourceStream(resource);

        if (stream is null)
        {
            throw new InvalidOperationException($"Template file {resource} not found");
        }

        using var reader = new StreamReader(stream, Encoding.UTF8);

        var fileContent = reader.ReadToEnd();

        if (string.IsNullOrWhiteSpace(fileContent))
        {
            throw new InvalidOperationException($"Template file {resource} is empty");
        }

        return fileContent;
    }
}