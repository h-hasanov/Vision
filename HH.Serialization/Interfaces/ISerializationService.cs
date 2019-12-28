namespace HH.Serialization.Interfaces
{
    public interface ISerializationService
    {
        /// <summary>
        /// Serializes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="location">The location.</param>
        /// <returns></returns>
        void Serialize<T>(T item, string location);

        /// <summary>
        /// Deserializes the object from the given location.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns></returns>
        T DeSerialize<T>(string location);
    }
}
