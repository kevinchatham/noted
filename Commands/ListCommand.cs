using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;

namespace Noted.Commands;

[Command("list")]
public class ListCommand : ICommand
{
    public ValueTask ExecuteAsync(IConsole console)
    {
        var files = Directory.GetFiles("notes", "*.md", SearchOption.AllDirectories)
                .Select(x =>
                    x.ToLower()
                        .Replace("notes/", "")
                        .Replace(".md", ""))
                .ToList();

        if (files.Count == 0)
            console.Output.WriteLine("No notes.");

        files.ForEach(x => console.Output.WriteLine(x));

        return default;
    }
}