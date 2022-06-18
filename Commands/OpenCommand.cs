using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using CliWrap;
using Markdig;

namespace Noted.Commands;

[Command("open")]
public class OpenCommand : ICommand
{
    [CommandParameter(0, Description = "Name of note.")]
    public string Name { get; init; }

    public async ValueTask ExecuteAsync(IConsole console)
    {
        var path = $"notes/{Name}.md";

        if (!File.Exists(path))
        {
            console.Output.Write($"{Name} does not exist.");
            return;
        }

        var files = Directory.GetFiles("notes", "*.md", SearchOption.AllDirectories);

        var template = await File.ReadAllTextAsync("template.html");

        foreach (var file in files)
        {
            var text = await File.ReadAllTextAsync(file);
            if (string.IsNullOrEmpty(text)) continue;
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().UseBootstrap().Build();
            var md = Markdown.ToHtml(text, pipeline);
            var html = template.Replace("{{BODY}}", md).Replace("{{TITLE}}", file.Replace("notes/", "").Replace(".md", ""));
            await File.WriteAllTextAsync(file.Replace(".md", ".html"), html);
        }

        await Cli.Wrap("open").WithArguments($"{path.Replace(".md", ".html")}").ExecuteAsync();
    }
}