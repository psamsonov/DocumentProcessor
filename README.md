# DocumentProcessor

# Requirements

.NET Framework 3.1.400-preview-015178 or greater

# Running the solution

Build solution:
`dotnet build`

Run the following commands to start the document processor (preferably in different consoles):

`dotnet run -p DocumentProcessorApi `

`dotnet run -p DocumentProcessor --urls=http://localhost:5002/`

Navigate your browser to http://localhost:5002/

## Known limitations

* The parser does not support documents that are more than one page long
* There is no dependency injection, which is why the code has to be slightly unusual to be unit testable
* No security is implemented.
* Storage is only in memory, there is no database.
