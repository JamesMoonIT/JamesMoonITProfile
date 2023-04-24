I can't be certain, but if you are here looking for how to run RiveScript in C# .NET Core, my fix was adding this to Startup.cs

app.UseStaticFiles(new StaticFileOptions
{
    ServeUnknownFileTypes = true,
    DefaultContentType = "text/rive"
});

It seems .NET does not appreciate foreign file types, so you need to tell it the format and how to read it. With this, you can use the standard javascript libraries and file directory under wwwroot without problems with finding the .rive files.
