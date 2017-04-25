namespace EFDataStorage.Patterns
{
    /// <summary>
    /// Used to define a repository that can Execute an query of TQuery and return a value to the caller 
    /// regarding the number and/or nature of objects affected in response.  The Implementing repository shall
    /// return a value representing the number and/or nature of object updated as required by the TResult parameter.
    /// </summary>
    /// <typeparam name="TQuery">
    /// The type of the object to be inserted into the repository
    /// </typeparam>
    /// <typeparam name="TResult">
    /// The type of the return value or object
    /// </typeparam>
    public interface IExecute<in TQuery, out TResult>
    {
        TResult Execute(TQuery queryParams);
    }
}
