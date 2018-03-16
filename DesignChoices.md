### Assumptions/should ask for clarification on
* The problem asks for the cheapest shipment, is splitting it across warehouses cheaper, or is one shipment cheaper?  Is there some cut-off in there?
  * Assumption: 1 shipment is always cheaper than 2, no matter what warehouse it must come from
    * BUT I'm going to start with being greedy about orders, meaning I'm NOT optmising the number of shipments (I should have a test case illustrating this)
* Instead of returning an empty object when no allocation is possible, I am returning null
* I would normally write fewer doc comments and you should be able to see whats going on with ignoring all the comments, but for exercises like this feel including my thought process would never be a negative
  * Comments starting with "// <comment for reviewers only:> " would be not be included at all normally, are only for the purpose of explaining some conventions that are part of C# that most might not be familiar with
* Going to assume I do not need to validate the input objects too much.  I have what I consider standard protections, but not going to have unit tests for completely mangled json input or anything.  The rational being the API layer should contain logic to return status code 400 in all of those cases
  * Also because I'm using a typed langauge and a mature json library, an order of `{ apple: null }` or `{ apple: "hello" }` will either cause an exception during deserialization or just default to 0



### Project structure
* Debated between making sub-folders for "Allocator" and "Datastore"
  * Decision: So far I'm expecting only one file in each of those folders so not going to bother
    * Will use a DTOs folder for the data objects, and a separate project for Tests
* Based on the project description I did not see a need for a front-end at all in this, so there is only a library class and unit tests
  * But I was imagining an internal library, if it were a public one I'd ideally go through and make certain all public members had enough doc comments (in C# this is ones with 3 '/'s above signatures) to be consumable (which I may try to do before submitting this, or shortly after)


### Program design choices
* I tend to use DTO (aka POJO/POCO) over having the object itself owning much business logic, preferring that logic should live in an Application layer, not a Model layer
* I've come to prefer a pattern for unit test of naming the test "When_..." and the `Assert` statements are the "Should" statements


### Steps to run this:
1. This exact code may or may not work on Linux/Mac, I would hope yes, but I didn't specifically test that, my belief is that "dotnetstandard" is functional on other operating systems these days, but it might still be a work in progress
1. Setup the .Net Core CLI tools: https://www.microsoft.com/net/learn/get-started
1. Clone this repository
1. Navigate to `\InventoryAllocator\src`
1. Use the command `dotnet test .\InventoryAllocatorTests` when in the `src` folder



### Rejected unit test ideas
* Assuming the inputs are from trusted sources.  Had the idea that using a mocking framework could generate warehouse inventory with 1000 warehouses, and throw an exception if more than are needed to fulfill the order are enumerated. Which could be performance and/or DOS type attacks if the code did not stop enumerating once the order is fulfilled;
* Other input validation tests (there are better ways to enforce that, jsonschema or something)


### Time log
* Day 1 (started late in the day):
  * 1 hour on getting IDE and initial project setup
* Day 2 (started at commit of .gitignore)
  * 3:00-3:45: refactoring project structure, and getting test project hooked up (had some issues with commands, seems to only work well when current directory is the context, see .bat file for details)
  * 4:20: Actually starting the implementation of the allocator
  * 6:10: All supplied test cases working (distracted by other things going on, probably at best 80% focused on working on this)
  * 6:40: Trying to think of test cases that are not covered, seems like good coverage for me, and excessive/redundant unit tests are sometimes worse than too few in my opinion
* Day 3, revised this document to be more readable and accurate



### Project language
* I've only spent significant time writing C# for the last couple years, so that is the obvious choice for me.  Any other language I'd likely have a mix of old and new features/syntax.
  * I could do it in TypeScript, which I'd be fairly comfortable developing within SublimeText for example, but still figure I'd risk making choices based on my C# knowledge which may or may not follow TypeScript conventions
  * Really debating if using .Net Core is overkill for this exercise, I've only done any initial setup with .Net Core once before, and its changed slightly from that time.  Other languages I could definitely spin up quicker, but for any personal project I would still tend to choose .Net because the initial setup is a one-time task, and feel it will result in much more maintainable and navigatable code (well at least navigatable for those familiar with .Net)
* I am using .Net Core which I haven't done too much with overall, and using Visual Studio Code which I've previously only used as a text editor not an IDE
  * In hindsight, either I missed some steps in the setup of my environment or just VSCode does not work as well as I would have hoped for actual C# work... :/
  * In further hindsight this was somewhat a mistake, the "dotnetcore" vs "dotnetstandard" and various libraries only supporting one is still in a good amount of flux, and caused getting everything up and running to have some issues
* I started with thinking I'd build an MVC API, but I would want the API layer to be basically pass-through anyway so just created it as a class library and unit tests, since that should be a pass-through to a library anyway


### Input data
* Going to assume the JSON structure described in the examples cannot easily be modified
  * That structure is not ideal for deserialization into .Net objects, so will inherit from `Dictionary`


### Libraries
* Newtonsoft Json.net is the defacto standard for JSON serialization/deserialization in .Net
* NUnit3, is the newest version of the long unchanged NUnit2, in that time XUnit or MSTest has grown in abilities and popularity, and in certain circumstances are much easier to work with than NUnit, but for basic tests porting between the differnet frameworks has been a simple task
  * I default to NUnit because I used JUnit in college, and NUnit2 extensively, NUnit3 added support for parallelized test runs which is the biggest general thing I personally find valuable in XUnit.  Although some of the patterns that XUnit uses are more modern, my history causes me to not generally need to utilize those features, and mitigate the issues they are solving in other ways


### Conclusions
* I didn't have a development environment for C# setup on my newest personal computer, and getting that fully functional and up-to-date took more time, as well as different tools than what I've come to expect slowed me down some as well
* .Net core without having an full up-to-date version of Visual Studio is not a good choice for projects like this
  * I have become more reliant than I realized on Visual Studio and Resharper, and getting C# to work without them requires lots of minor details that using those allow you to gloss over (aka I should have at least used Visual Studio Community, not Visual Studio Code)


### Issues with documentation
* `[ { name: owd, inventory: { apple: 5, orange: 10 } }, { name: dm:, inventory: { banana: 5, orange: 10 } } ]` is not valid json, other examples given also have similar issues, I presume you wouldn't want a pull request cleaning that up
