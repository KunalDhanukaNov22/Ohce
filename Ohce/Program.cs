using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddSingleton<IClock, Clock>();
services.AddSingleton<ICurrentHour, CurrentHour>();
services.AddSingleton<IMessage, Message>();
services.AddSingleton<IStringReversal, StringReversal>();
services.AddSingleton<IApplication, Application>();

var provider = services.BuildServiceProvider();

var app = provider.GetRequiredService<IApplication>();

app.Run();