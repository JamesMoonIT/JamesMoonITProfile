This is a combination of the majority of my web skills in the form of a Personal Profile. This is intended to be see by employers seeking my skills in Web and Application Development, as well as Full-Stack Development and UX/UI Development.

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

Major Hurdles:
- Deploying Rivescript bot in a C# .Net Core Environment
- Hosting free+ through GitHub Actions and Azure Static Web Apps.
- Developing json database for comments.

Removed features:
- Self-hosting on lcoal machine
    - This caused lots of issues for the server I use for hosting as I use the machine for more than web hosting.
-  Storing comments on sqlserver db
    - This was to also be hosted on local server.
- Implement 3rd party Rivescript C# Library
    - The library caused problems with Azure. The original code my the rivescript bot was based on Java and I configured my site to accept javascript Rivescript inside a .NET Core evironment rather than rebuilding in C#.


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
