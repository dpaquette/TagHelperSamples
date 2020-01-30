AppVeyor: [![Build status](https://ci.appveyor.com/api/projects/status/28ml7n6pxd2qc69f/branch/master?svg=true)](https://ci.appveyor.com/project/DavidPaquette/taghelpersamples/branch/master)



# Tag-helper-samples
[TagHelper Samples](http://taghelpersamples.azurewebsites.net/)  running on Azure.
Sample tag helpers that do useful work. 

To use the [Bootstrap](http://getbootstrap.com/) AlertTagHelper  (authored by [Rick Strahl](https://twitter.com/RickStrahl))  add [Font Awesome](https://fortawesome.github.io/Font-Awesome/). The sample project adds FA to the *Views\Shared\_Layout.cshtml* file.

Click the *Summary* menu item to see the tag helpers in action. From the home link, select a tag helper to see a code snipet and the resulting UI.

# Try it in your project

## Bootstrap Tag Helpers
[![Bootstrap Tag Helpers NuGet Badge](https://buildstats.info/nuget/TagHelperSamples.Bootstrap)](https://www.nuget.org/packages/TagHelperSamples.Bootstrap/)

The Bootstrap Tag Helper samples are available on Nuget.

`dotnet add package TagHelperSamples.Bootstrap`

Then add the tag helpers to your _ViewImports.cshtml file:

`@addTagHelper "*, TagHelperSamples.Bootstrap"`

## Markdown Tag Helpers
[![Markdown Tag Helpers NuGet Badge](https://buildstats.info/nuget/TagHelperSamples.Markdown)](https://www.nuget.org/packages/TagHelperSamples.Markdown/)
The Markdown Tag Helper samples are available on Nuget.

`dotnet add package TagHelperSamples.Markdown`

Then add the tag helpers to your _ViewImports.cshtml file:

`@addTagHelper "*, TagHelperSamples.Markdown"`

## Authorize Tag Helper
[![Authorization Tag Helpers NuGet Badge](https://buildstats.info/nuget/TagHelperSamples.Authorization)](https://www.nuget.org/packages/TagHelperSamples.Authorization/)
The Authorize Tag Helper is available on Nuget.

`dotnet add package TagHelperSamples.Authorization`

Then add the tag helpers to your _ViewImports.cshtml file:

`@addTagHelper "*, TagHelperSamples.Authorization"`
