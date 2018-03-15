### TODO
* ~Refactor so that Tests can be in a different project~
* ~Add .gitignore file~


### Time log
* Day 1 (started late in the day):
** 1 hour on getting IDE and initial project setup
* Day 2 (started at commit of .gitignore)
** <commit of .gitignore>-3:45: refactoring project structure, and getting test project hooked up (had some issues with commands, seems to only work well when current directory is the context, see .bat file for details)



### Project language
* I've only spent significant time writing C# for the last couple years, so that is the obvious choice for me.  Any other language I'd likely have a mix of old and new features/syntax.
** I could do it in TypeScript, which I'd be fairly comfortable developing within SublimeText, but still figure I'd risk making choices based on my C# knowledge which may or may not follow TypeScript conventions
** Really debating if using .Net Core is overkill for this exercise, I've only done any initial setup with .Net Core once before, and its changed slightly from that time.  Other languages I could definitely spin up quicker, but for any personal project I would still tend to choose .Net because the initial setup and semi-complex structure that is suggested is a one-time task no matter how painful it might be, and feel it will result in much more maintainable and navigatable code (well at least navigatable for those familiar with .Net)
* I am using .Net Core which I haven't done too much with overall, and using Visual Studio Code which I've previously only used as a text editor not an IDE
** Also in hindsight, either I missed some steps or just VSCode does not work well for actual C# work... :/
** In hindsight this was somewhat a mistake, the "dotnetcore" vs "dotnetstandard" and various libraries only supporting one is still in a good amount of flux, and caused issues for me
* ~Also I'm using a mvc project because that is the way to build an API in the .Net world, likely my Views folder will only contain the defaults that get generated.  Felt like that would be more realistic than a console app taking in a json string or anything else.  Although the initial setup for this requires a number of generated files :/~
* Scratch the MVC API idea, since that should be a pass-through to a library anyway


### Steps to run this:
1. Setup the CLI tools: https://www.microsoft.com/net/learn/get-started
1. Clone this repository
1. Navigate to `\InventoryAllocator\src`
1. Use the command `dotnet test .\InventoryAllocatorTests` when in the `src` folder


### Project structure
* Debated between making sub-folders for "Allocator" and "Datastore"
** Decision: So far I'm expecting only one file in each of those folders so not going to bother
*** Will use the Models folder for the data objects, and a folder for Tests


### Input data
* Going to assume the JSON examples included cannot easily be modified
** That structure is not ideal for deserialization into .Net objects, so will inherit from `Dictionary`

### Libraries
* Newtonsoft Json.net is the defacto standard for JSON serialization/deserialization in .Net
* NUnit3, Having used JUnit in college and previously, and NUnit2 extensively, NUnit3 added support for parallelized test runs which was the only thing I personally find valuable in XUnit.  Although some of the patterns that XUnit uses are more modern, my history causes me to not generally need to utilize those features, and mitigate the issues they are solving in other ways


### Conclusions
* .Net core without having an full up-to-date version of Visual Studio is not a good choice for projects like this
* I have become very reliant on Visual Studio and Resharper, and getting C# to work without them requires lots of minor details that those allow you to gloss over (I should have used Visual Studio Community, not Visual Studio Code)
** As an aside, Microsoft is being horrible with reusing parts of nams of things that actaully end up being very different from each other, I didn't fully realize how bad it was


### Issues with documentation
* `[ { name: owd, inventory: { apple: 5, orange: 10 } }, { name: dm:, inventory: { banana: 5, orange: 10 } } ]` is not valid json
