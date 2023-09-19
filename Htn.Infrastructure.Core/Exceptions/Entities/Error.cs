using System.Runtime.Serialization;

namespace Htn.Infrastructure.Core.Exceptions.Entities
{
    [DataContract]
    public class Error
    {
        [DataMember]
        public string Codigo { get; set; }

        [DataMember]
        public string Descripcion { get; set; }
    }
}