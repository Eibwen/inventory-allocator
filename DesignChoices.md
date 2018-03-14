### Project language
* I've only spent significant time writing C# for the last couple years, so that is the obvious choice for me.  Any other language I'd likely have a mix of old and new features/syntax.
** I could do it in TypeScript, which I'd be fairly comfortable developing within SublimeText, but still figure I'd risk making choices based on my C# knowledge which may or may not follow TypeScript conventions
* I am using .Net Core which I haven't done too much with overall, and using Visual Studio Code which I've previously only used as a text editor not an IDE
* Also I'm using a mvc project because that is the way to build an API in the .Net world, likely my Views folder will only contain the defaults that get generated.  Felt like that would be more realistic than a console app taking in a json string or anything else.  Although the initial setup for this requires a number of generated files :/


### Steps to run this:
1. Clone this repository
1. Setup the CLI tools: https://www.microsoft.com/net/learn/get-started
1. Use the command `dotnet run` when in the root of this repo


### Project structure
* Debated between making sub-folders for "Allocator" and "Datastore"
** Decision: So far I'm expecting only one file in each of those folders so not going to bother
*** Will create a DTO or Entities folder for the data objects, and a folder for Tests


### Input data
* Going to assume the JSON examples included cannot easily be modified
** That structure is not ideal for deserialization into .Net objects, so will inherit from `Dictionary`

### Libraries
* Newtonsoft Json.net is the defacto standard for JSON serialization/deserialization in .Net
* NUnit3, Having used JUnit in college and previously, and NUnit2 extensively, NUnit3 added support for parallelized test runs which was the only thing I personally find valuable in XUnit.  Although some of the patterns that XUnit uses are more modern, my history causes me to not generally need to utilize those features, and mitigate the issues they are solving in other ways

