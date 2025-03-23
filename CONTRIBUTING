Want to contribute some code to this project? That's great, but please read these points first.

# Create an issue first
If you spot a bug or you have a feature request, please create an issue in the GitHub issue tracker, even if you're willing to write all the code yourself. This will allow us to discuss the issue before you start coding. I don't want you to waste your time on a pull request that we won't merge, and I'd much rather we have a discussion first to make sure that we're on the same page. Creating an issue is the best way to start that discussion.

# No warnings
Please make sure that your code compiles without warnings. A codebase with lots of warnings, even if they're not particularly important, can hide new warnings that might be important.

# Write unit tests
Please write unit tests for your code, using XUnit. This project has a good test coverage and I want to keep it that way. Pull requests where any method has less than 80% code coverage will be blocked by the GitHub actions workflow, but you can do better than 80%, can't you? Aim for 100% coverage where possible, and if something really doesn't need testing or can't be tested, decorate it with an `[ExcludeFromCodeCoverage]` attribute with a justification, and be prepared to argue the case for your justification (in a civilised manner of course).

# Respect the analyzers
This project has a number of analyzers enabled, and I'd like to keep it that way. Please make sure that your code does not generate any warnings from the analyzers. If you think that the analyzer is wrong (and sometimes they are), you can suppress the violation with a `[SuppressMessage]` attribute with a justification on a case-by-case basis, and be prepared to argue the case for your justification (in a civilised manner of course).