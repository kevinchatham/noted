using CliFx;

if (!Directory.Exists("notes"))
    Directory.CreateDirectory("notes");

await new CliApplicationBuilder()
            .AddCommandsFromThisAssembly()
            .Build()
            .RunAsync();
