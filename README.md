AppVeyor: [![Build status](https://ci.appveyor.com/api/projects/status/28ml7n6pxd2qc69f/branch/master?svg=true)](https://ci.appveyor.com/project/DavidPaquette/taghelpersamples/branch/master)



# Tag-helper-samples
[TagHelper Samples](http://taghelpersamples.azurewebsites.net/)  running on Azure.
Sample tag helpers that do useful work. 

To use the [Bootstrap](http://getbootstrap.com/) AlertTagHelper  (authored by [Rick Strahl](https://twitter.com/RickStrahl))  add [Font Awesome](https://fortawesome.github.io/Font-Awesome/). The sample project adds FA to the *Views\Shared\_Layout.cshtml* file.

Click the *Summary* menu item to see the tag helpers in action. From the home link, select a tag helper to see a code snipet and the resulting UI.

#Try it in your project
The Bootstrap Tag Helper samples are available on [Nuget](https://www.nuget.org/packages/TagHelperSamples.Bootstrap).

`Install-Package TagHelperSamples.Bootstrap`

Then add the tag helpers to your _ViewImports.cshtml file:

`@addTagHelper "*, TagHelperSamples.Bootstrap"`


The Markdown Tag Helper samples are available on [Nuget](https://www.nuget.org/packages/TagHelperSamples.Markdown).

`Install-Package TagHelperSamples.Markdown`

Then add the tag helpers to your _ViewImports.cshtml file:

`@addTagHelper "*, TagHelperSamples.Markdown"`


