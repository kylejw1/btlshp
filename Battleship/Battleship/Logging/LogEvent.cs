using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Logging
{
    public class LogEvent : DynamicObject
    {
        Dictionary<string, string> Properties = new Dictionary<string, string>();

        public LogEvent(string message=null)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                Properties["Message"] = message;
            }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (Properties.ContainsKey(binder.Name))
            {
                result = Properties[binder.Name];
                return true;
            } else
            {
                result = "Invalid Property";
                return false;
            }
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (string.IsNullOrWhiteSpace(binder.Name))
            {
                return false;
            }

            Properties[binder.Name] = (value ?? "null").ToString();

            return true;
        }

        public override string ToString()
        {
            var cleanedProperties = Properties.Select(kvp =>
            {
                // Enclose key and value in double quotes, replacing all double quotes from their contents with single quotes
                return string.Format("\"{0}\"=\"{1}\"", 
                    kvp.Key.Replace('"', '\''),
                    (kvp.Value ?? "").Replace('"', '\'')
                    );
            });
            return string.Join(", ", cleanedProperties);
        }
    }
}
