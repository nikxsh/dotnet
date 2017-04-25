using EFDataStorage.Helper;

namespace EFDataStorage.Patterns
{
    /// <summary>
    /// Used to defined repositories that any/all items of TResult that match the given query object.  
    /// Implementing classes should use the TQuery class to define all the data needed for paging, filtering, 
    /// sorting, etc. 
    /// </summary>
    /// <typeparam name="TQuery">
    /// Class or Type used to define the sorting, filtering and/or paging criteria 
    /// </typeparam>
    /// <typeparam name="TResult">
    /// Class or Type retuned by the repository.  Can be any data model class, or any collection of any class.
    /// </typeparam>
    public interface ISelect<in TQuery, out TResult> where TQuery : QueryFor<TResult>
    {
        /// <summary>
        /// Returns all items matching the given query.  
        /// </summary>
        /// <param name="query">
        /// A parameter object with all detials neccessary to match the return the required objects from the 
        /// repository
        /// </param>
        /// <returns> All items that match the querySpec.</returns>
        TResult Select(TQuery query);
    }

    public class QueryFor<TResult> : SessionBase
    {
    }
}
