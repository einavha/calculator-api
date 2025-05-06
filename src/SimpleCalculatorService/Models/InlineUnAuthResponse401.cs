using System.Text;
using System.Runtime.Serialization;
using System.Text.Json;

namespace SimpleCalculatorService.Models
{
    /// <summary>  
    /// Unauthorized response  
    /// </summary>  
    [DataContract]
    public partial class InlineUnAuthResponse401 : IEquatable<InlineUnAuthResponse401>
    {
        /// <summary>  
        /// Gets or Sets Message  
        /// </summary>  
        [DataMember(Name = "message")]
        public string Message { get; set; } = "Unauthorized";

        /// <summary>  
        /// Returns a string that represents the current object.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>  
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class InlineResponse401 {\n");
            sb.Append("  Message: ").Append(Message).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>  
        /// Converts the object to its JSON string representation.  
        /// </summary>  
        /// <returns>A JSON string representation of the object.</returns>  
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
            return obj is InlineUnAuthResponse401 other && Equals(other);
        }


        /// <summary>  
        /// Determines whether the specified InlineUnAuthResponse401 is equal to the current object.  
        /// </summary>  
        /// <param name="other">The InlineUnAuthResponse401 to compare with the current object.</param>  
        /// <returns>true if the specified InlineUnAuthResponse401 is equal to the current object; otherwise, false.</returns>  
        public bool Equals(InlineUnAuthResponse401? other)
        {
            return other is not null && Message == other.Message;
        }

        /// <summary>    
        /// Serves as the default hash function.    
        /// </summary>    
        /// <returns>A hash code for the current object.</returns>    
        public override int GetHashCode()
        {
            return Message?.GetHashCode() ?? 0;
        }

        /// <summary>  
        /// Determines whether two InlineUnAuthResponse401 instances are equal.  
        /// </summary>  
        /// <param name="left">The first InlineUnAuthResponse401 instance to compare.</param>  
        /// <param name="right">The second InlineUnAuthResponse401 instance to compare.</param>  
        /// <returns>true if the instances are equal; otherwise, false.</returns>  
        public static bool operator ==(InlineUnAuthResponse401 left, InlineUnAuthResponse401 right) => Equals(left, right);

        /// <summary>  
        /// Determines whether two InlineUnAuthResponse401 instances are not equal.  
        /// </summary>  
        /// <param name="left">The first InlineUnAuthResponse401 instance to compare.</param>  
        /// <param name="right">The second InlineUnAuthResponse401 instance to compare.</param>  
        /// <returns>true if the instances are not equal; otherwise, false.</returns>  
        public static bool operator !=(InlineUnAuthResponse401 left, InlineUnAuthResponse401 right) => !Equals(left, right);
    }

}
