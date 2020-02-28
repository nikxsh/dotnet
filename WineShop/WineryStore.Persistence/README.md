Why?
- https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/ef/overview
- https://www.tutorialspoint.com/entity_framework/entity_framework_overview.htm
- Entity Framework is an object-relational mapper. This means that it can return the data in your database as an object (e.g.: a Person object with the properties Id, Name, etc.) 
  or a collection of objects.
- Why is this useful? Well, it's really easy. Most of the time you don't have to write any SQL yourself, and iterating is very easy using your language built in functions. 
  When you make any changes to the object, the ORM will usually detect this, and mark the object as 'modified'. When you save all the changes in your ORM to the database, 
  the ORM will automatically generate insert/update/delete statements, based on what you did with the objects.
- For large domain project i suggest to go with the Entity Framework
- With the Entity Framework, developers can work at a higher level of abstraction when they deal with data, and can create and maintain data-oriented applications 
  with less code than in  traditional applications. Because the Entity Framework is a component of the .NET Framework, Entity Framework applications can run on any 
  computer on which the .NET Framework starting with version 3.5 SP1 is installed.
- The Entity Framework gives life to models by enabling developers to query entities and relationships in the domain model (called a conceptual model in the Entity Framework) 
  while relying on the Entity Framework to translate those operations to data source–specific commands. This frees applications from hard-coded dependencies on a particular 
  data source.