[![Build status](https://ci.appveyor.com/api/projects/status/ec5obb6k82pwu0ix?svg=true)](https://ci.appveyor.com/project/Spritely/cqrs)

[![NuGet Status](http://nugetstatus.com/Spritely.Cqrs.png)](http://nugetstatus.com/packages/Spritely.Cqrs)

# Spritely.Cqrs
A very simple command/query data access separation library based on blog post(s) at https://www.cuttingedge.it/blogs/steven/pivot/entry.php?id=92

CQRS stands for Command Query Responsibility Segregation. Search around the net and you'll find plenty of great material for understanding what this means, why you do it, etc. It basically just means you separate all your data reads from your data writes. Data reads are referred to as Queries and data writes as Commands. You should not do any state changes during a query and you should avoid returning any data from a command.

## Queries

Using this library you write simple data model classes for your queries such as:
```csharp
    public class Sample
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
```

When needed reference:

```csharp
using Spritely.Cqrs;
```

When you want to query for this data you create a query parameter class for communicating which parameters are required:

```csharp
    public class GetSampleByIdQuery : IQuery<IEnumerable<Sample>>
    {
        public int Id { get; set; }
    }
```

Then you write a query handler class to do the actual SQL query like this:

```csharp
    public class GetSampleByIdQueryHandler : IQueryHandler<GetSampleByIdQuery, IEnumerable<Sample>>
    {
        // This assumes you created a custom type to abstract database connections
        private readonly MyDatabase db;

        // Let your dependency injection framework take care of passing this parameter
        public GetSampleByIdQueryHandler(MyDatabase db)
        {
            this.db = db;
        }

        public IEnumerable<Sample> Handle(GetSampleByIdQuery query)
        {
            // This assumes that your custom database type CreateConnection() returns an open IDbConnection
            using (this.db.CreateConnection())
            {
                // Using Dapper here, but could be ADO.NET or another data access library
                return this.db.Query<Sample>("select * from Sample");
            }
        }
    }
```

Now you can use this in a very straight forward manner in any of your classes as follows:

```csharp
    public class SampleBusinessOperation
    {
        private readonly IQueryHandler<GetSampleByIdQuery, IEnumerable<Sample>> queryHandler;
        
        // Let your dependency injection framework take care of passing this parameter
        public SampleBusinessOperation(IQueryHandler<GetSampleByIdQuery, IEnumerable<Sample>> queryHandler)
        {
            this.queryHandler = queryHandler;
        }

        public IEnumerable<Sample> GetSampleById(int id)
        {
            var getSample = new GetSampleByIdQuery
            {
                Id = 5
            };

            var sample = this.queryHandler.Handle(getSample);

            return sample;
        }
    }
```

If you find yourself injecting many different query handlers, the code can get noisy. First you should think hard about if you might need to apply Single Responsibility to solve the issue. If that doesn't fix it, then try this version of the code instead:

```csharp
    public class SampleBusinessOperation
    {
        private readonly IQueryProcessor queryProcessor;
        
        public SampleBusinessOperation(IQueryProcessor queryProcessor)
        {
            this.queryProcessor = queryProcessor;
        }

        public IEnumerable<Sample> GetSampleById(int id)
        {
            var getSample = new GetSampleByIdQuery
            {
                Id = 5
            };

            // queryProcessor will look up the correct QueryHandler for each query based on the QueryHandler type
            var sample = this.queryProcessor.Process(getSample);

            return sample;
        }
    }
```

## Commands

When you are writing commands you typically should name these around the intent of the operation, but that's a big discussion on its own. For simplicity, lets juse a trivial sample update command parameter object as follows:

```csharp
    public class SampleUpdateCommand : ICommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
```

It looks just like the model object, but following CQRS (remember CQRS is all about separating commands and queries) you should create a different type here to prevent coupling between your command and query sides.

```csharp
    public class SampleUpdateCommandHandler : ICommandHandler<SampleUpdateCommand>
    {
        // This type is just like in the query example - more on this below
        private readonly MyDatabase db;

        public SampleUpdateCommandHandler(MyDatabase db)
        {
            this.db = db;
        }

        public void Handle(SampleUpdateCommand command)
        {
            using (this.db.CreateConnnection())
            {
                // Use dapper do do sql update
                this.db.Execute("update ....");
            }
        }
    }
```

Now you can use it like so:

```csharp
    public class SampleUpdateBusinessOperation
    {
        private readonly ICommandHandler<SampleUpdateCommand> sampleUpdateCommandHandler;

        // Let your dependency injection framework take care of passing this parameter
        public SampleUpdateBusinessOperation(ICommandHandler<SampleUpdateCommand> sampleUpdateCommandHandler)
        {
            this.sampleUpdateCommandHandler = sampleUpdateCommandHandler;
        }

        public void Update(int id, string name, string value)
        {
            var sampleUpdate = new SampleUpdateCommand
            {
                Id = id,
                Name = name,
                Value = value
            };

            // Put it in the DB...
            this.sampleUpdateCommandHandler.Handle(sampleUpdate);
        }
    }
```

There is also an ICommandProcessor if your constructors get too heavy:

```csharp
    public class SampleUpdateBusinessOperation
    {
        private readonly ICommandProcessor commandProcessor;
        
        public SampleUpdateBusinessOperation(ICommandProcessor commandProcessor)
        {
            this.commandProcessor = commandProcessor;
        }

        public void Update(int id, string name, string value)
        {
            var sampleUpdate = new SampleUpdateCommand
            {
                Id = id,
                Name = name,
                Value = value
            };

            this.commandProcessor.Process(sampleUpdate);
        }
    }
```

Commands should not return values, but if you really need this functionality then you can use ICommandWithReturn, ICommandWithReturnHandler, and ICommandWithReturnProcessor to achieve what you want. They are basically a combination of commands and queries. Sorry, no examples because we want to discourage this usage.

## Registering your types

The code for registering your types is simple if you've got a nice dependency injection framework. We recommend SimpleInjector so here's the syntax for that:

```csharp
    // Any type from your assembly that contains your query handlers/command handlers, etc. will do
    container.RegisterManyForOpenGeneric(typeof(IQueryHandler<,>), typeof(GetSampleByIdQueryHandler).Assembly);
```

If your container can't handle generics for you then you will need to register each type manually... something like:

```csharp
    // One line for each type you need to register
    container.Register<IQueryHandler<GetSampleByIdQuery, Sample>>(() => new GetSampleByIdQueryHandler(myDatabase));
```

## Databases

How should one design a MyDatabae class? Spritely.Cqrs has a little bit of helper code for this (if you are working with SQL Server at least), but it's entirey optional and you can customize this anyway you want. The point is you need to get a real database connection into your queries somehow. Here's how you can do this with Spritely.

```csharp
    // IDatabase is just a marker interface (for now at least) so you can use it to find all of these and register them
    // all at once too (see registration code above).
    public class MyDatabase : IDatabase
    {
        // DatabaseConnectionSettings is a simple type that stores the most common properties used
        // when constructing query strings. It is designed to be deserialized from Json.
        // Spritely.Configuration helps a little with that.
        public DatabaseConnectionSettings ConnectionSettings { get; set; }

        public IDbConnection CreateConnection()
        {
            var connection = this.ConnectionSettings.CreateSqlConnection();

            connection.Open();

            return connection;
        }
    }
```

