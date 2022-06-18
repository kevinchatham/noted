using System.Collections;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using CliWrap;

namespace Noted.Commands;

[Command("add")]
public class AddCommand : ICommand
{
    [CommandParameter(0, Description = "Name of note.")]
    public string Name { get; init; }

    public async ValueTask ExecuteAsync(IConsole console)
    {
        if (File.Exists($"notes/{Name}.md"))
        {
            console.Output.Write($"{Name} already exists.");
            return;
        }

        File.Create($"notes/{Name.ToLower()}.md");

        await Cli.Wrap("code")
                    .WithArguments($"notes/{Name}.md")
                    .ExecuteAsync();
    }
}