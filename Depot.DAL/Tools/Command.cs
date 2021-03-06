using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depot.DAL.Tools
{
    public class Command
    {
        internal string Query { get; private set; }
        internal bool IsStoredProcedure { get; private set; }
        internal IDictionary<string, Parameter> Parameters { get; private set; }

        public object this[string parameterName]
        {
            get
            {
                if (!Parameters.ContainsKey(parameterName))
                    throw new InvalidOperationException($"The parameter \"{ parameterName }\" does not exist!");

                return Parameters[parameterName].ParameterValue;
            }
        }

        public Command(string query, bool isStoredProcedure = false)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentException("Query is not valid!");

            Query = query;
            IsStoredProcedure = isStoredProcedure;
            Parameters = new Dictionary<string, Parameter>();
        }

        public void AddParameter(string parameterName, object value)
        {
            AddParameter(parameterName, value, Direction.Input);
        }

        public void AddParameter(string parameterName, object value, Direction direction)
        {
            if (string.IsNullOrWhiteSpace(parameterName))
                throw new ArgumentException($"Parameter name \"{ parameterName }\" is not valid!");

            if (direction == Direction.Output && !IsStoredProcedure)
                throw new ArgumentException("The direction 'Output' can only be used with Stored Procedure!");

            Parameters.Add(parameterName, new Parameter(value ?? DBNull.Value, direction));
        }
    }
}
