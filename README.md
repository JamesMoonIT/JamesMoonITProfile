This is a combination of the majority of my web skills in the form of a Personal Profile. This is intended to be see by employers seeking my skills in Web and Application Development, as well as Full-Stack Development and UX/UI Development. The plan was to keep this private, but as Rivescript doesn't have any support for MVC or Core architecture, I thought to make this public and searchable under the "rivescript" tag for any developers.

Languages used:
- HTML
- CSS
- Javascript
- jQuery
- Rivescript
- C#
- Json5

Skills used:
- Azure Development and Deployment
- .NET Core Development
- MVC Web Development
- Web Design
- HCI Analysis
- Photoshop

Major Hurdles:
- Deploying Rivescript bot in a C# .Net Core Environment
- Hosting for as little as possible through GitHub Actions and Azure Web App Service.
- Developing API (private) for storing comments, along with local backup incase for any reason it goes down.

Removed features:
- Self-hosting on local machine
    - This caused lots of issues for the server I use for hosting as I use the machine for more than web hosting.
-  Storing comments on sqlserver db
    - This was to also be hosted on the same local server.
- Implement 3rd party Rivescript C# Library
    - The library caused problems with Azure. The original code my the rivescript bot was based on Java and I configured my site to accept javascript Rivescript inside a .NET Core evironment rather than rebuilding in C#.
- Hosting an Azure Static Web App for free
    - On top of having a crazy long domain that is hard to share, Static Web Apps cannot handle .Net MVC Core architecture. Despite attempts to learn Blazor and migrate the code, it proved to be to much of a task in comparison to paying a very small amount to deploy to azure and host files on GitHub.

P.S. A profanity filter has been manually added to the bottom of bigbrain.rive. Should you wish to not read the profain words entered into the file, I suggest not reading beyond line 354. I cannot hide this one section in github so YOU HAVE BEEN WARNED!


If anyone is curious about setting up Rivescript in a similar environment, I would recommend this approach as the Javascript method is better supported. In terms of how its done, you need to tell your 'Startup.cs' how to read the file as by default, it will not be able to interpret it and ignore it in the Build process. You can fix this by adding this to your startup file:
```
var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".rive"] = "text/rive";
app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider
});
```
You can also view this in my own startup.cs file. Hope this helps because I was not aware of how to fix this and there is no documentation for this method that I could find.
