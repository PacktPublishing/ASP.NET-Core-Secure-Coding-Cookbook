using System;
using System.Runtime.Serialization;

namespace OnlineBankingApp.Models
{
    public class LoanDeserializationBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            if (typeName.Equals("OnlineBankingApp.Models.Loan")){
                return typeof(Loan);
            }
            return null;
        }
    }
}