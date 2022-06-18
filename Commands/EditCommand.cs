using System.Diagnostics;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using CliWrap;

namespace Noted.Commands;

[Command("edit")]
public class EditCommand : ICommand
{
    [CommandParameter(0, Description = "Name of note.")]
    public string Name { get; init; }

    public async ValueTask ExecuteAsync(IConsole console)
    {
        var result = await Cli.Wrap("code")
            .WithArguments($"notes/{Name}.md")
            .ExecuteAsync();
    }
}