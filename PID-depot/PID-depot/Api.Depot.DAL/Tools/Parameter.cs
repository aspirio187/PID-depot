using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.DAL.Tools
{
    internal class Parameter
    {
        private object _parameterValue;
        private readonly Direction _direction;

        public Parameter(object value, Direction direction)
        {
            _parameterValue = value;
            _direction = direction;
        }

        public object ParameterValue
        {
            get
            {
                return _parameterValue;
            }
            internal set
            {
                _parameterValue = value;
            }
        }

        public Direction direction
        {
            get
            {
                return _direction;
            }
        }
    }
}
