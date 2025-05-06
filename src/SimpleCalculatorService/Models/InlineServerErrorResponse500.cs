using System.Text;
using System.Runtime.Serialization;
using System.Text.Json;

namespace SimpleCalculatorService.Models
{
    /// <summary>  
    /// Internal Server Error response  
    /// </summary>  
    [DataContract]
    public partial class InlineServerErrorResponse500 : IEquatable<InlineServerErrorResponse500>
    {
        /// <summary>  
        /// Gets or sets the error message for the internal server error.  
        /// </summary>  
        [DataMember(Name = "error")]
        public string Error { get; set; } = "Internal server error occurred.";

        /// <summary>  
        /// Returns a string that represents the current object.  
        /// </summary>  
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class InlineServerErrorResponse500 {\n");
            sb.Append("  Error: ").Append(Error).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>  
        /// Converts the current object to its JSON string representation.  
        /// </summary>  
        /// <returns>A JSON string representation of the current object.</returns>  
        public string ToJson()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        }

        /// <summary>  
        /// Determines whether the specified object is equal to the current object.  
        /// </summary>  
        /// <param name="obj">The object to compare with the current object.</param>  
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>  
        public override bool Equals(object? obj)
        {
            return Equals(obj as InlineServerErrorResponse500);
        }

        /// <summary>  
        /// Determines whether the specified InlineServerErrorResponse500 is equal to the current object.  
        /// </summary>  
        /// <param name="other">The InlineServerErrorResponse500 to compare with the current object.</param>  
        /// <returns>true if the specified InlineServerErrorResponse500 is equal to the current object; otherwise, false.</returns>  
        public bool Equals(InlineServerErrorResponse500? other)
        {
            return other is not null && Error == other.Error;
        }

        /// <summary>  
        /// Serves as the default hash function.  
        /// </summary>  
        /// <returns>A hash code for the current object.</returns>  
        public override int GetHashCode()
        {
            return Error?.GetHashCode() ?? 0;
        }
        /// <summary>
        /// Checks if two CalculateBody instances are equal.
        /// </summary>
        /// <param name="left">The first CalculateBody instance.</param>
        /// <param name="right">The second CalculateBody instance.</param>
        /// <returns>True if both instances are equal, otherwise false.</returns>
        public static bool operator ==(InlineServerErrorResponse500 left, InlineServerErrorResponse500 right) => Equals(left, right);
        /// <summary>
        /// Checks if two CalculateBody instances are not equal.
        /// </summary>
        /// <param name="left">The first CalculateBody instance.</param>
        /// <param name="right">The second CalculateBody instance.</param>
        /// <returns>True if both instances are not equal, otherwise false.</returns>
        public static bool operator !=(InlineServerErrorResponse500 left, InlineServerErrorResponse500 right) => !Equals(left, right);
    }

}
