namespace HealthTracker.Server.Core.Exceptions.PhysicalActivity
{
    /// <summary>
    /// Represents an error when a exercise type cannot be found.
    /// </summary>
    public class ExerciseNotFoundException : Exception
    {
        public ExerciseNotFoundException() 
            :base("Exercise Type not found!") { }
        public ExerciseNotFoundException(string message) 
            : base($"Exercise Type not found! {message}") { }
        public ExerciseNotFoundException(int id)
            : base($"Exercise Type not found! Id: {id}") { }
    }
}
