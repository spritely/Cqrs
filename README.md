[![Build status](https://ci.appveyor.com/api/projects/status/github/Spritely/Cqrs?branch=master&svg=true)](https://ci.appveyor.com/project/VictorRobinson/cqrs/)

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

When you want to query for this data you create a query parameter class for communicating which parameters are required:

```csharp
    public class GetSampleByIdQuery : Spritely.Cqrs.IQuery<IEnumerable<Sample>>
    {
        public int Id { get; set; }
    }
```

Then you write a query handler class to do the actual SQL query like this:

```csharp
    public class GetSampleByIdQueryHandler : IQueryHandler<GetSampleByIdQuery, IEnumerable<Sample>>
    {
        private readonly IDbConnection db;

        // Let your dependency injection framework take care of passing this parameter
        public GetSampleByIdQueryHandler(IDbConnection db)
        {
            this.db = db;
        }

        public IEnumerable<Sample> Handle(GetSampleByIdQuery query)
        {
            using (this.db)
            {
                this.db.Open();

                // Using Dapper here, but could be data access library or just ADO.NET
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

If you find yourself injecting a bunch of different query handlers, the code can get noisy. First you should think hard about if you might need to apply Single Responsibility to solve the issue. If that doesn't fix it, then try this version of the code instead:

```csharp
    public class SampleBusinessOperation
    {
        private readonly IQueryProcessor queryProcessor;
        
        // Let your dependency injection framework take care of passing this parameter
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

            var sample = this.queryProcessor.Process(getSample);

            return sample;
        }
    }
```

## Commands

When you are writing commands you typically should name these around the intent of the operation, but that's a big discussion on its own. For simplicity, lets juse a trivial sample update command parameter object as follows:

```csharp
    public class SampleUpdateCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
```

It looks just like the model object, but following CQRS you should create a different type here to prevent coupling between your command and query sides.

```csharp
    public class SampleUpdateCommandHandler : ICommandHandler<SampleUpdateCommand>
    {
        private readonly IDbConnection db;

        // Let your dependency injection framework take care of passing this parameter
        public SampleUpdateCommandHandler(IDbConnection db)
        {
            this.db = db;
        }

        public void Handle(SampleUpdateCommand command)
        {
            using (this.db)
            {
                this.db.Open();

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
