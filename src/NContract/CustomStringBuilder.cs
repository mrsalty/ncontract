using System.Collections.Generic;
using System.Linq;

namespace NContract
{
    public class CustomStringBuilder
    {
        private readonly IList<string> _lines;
         
        public CustomStringBuilder()
        {
            _lines = new List<string>();        
        }

        public void AppendLine(string line)
        {
            _lines.Add(line);
        }

        public void Append(string value)
        {
            if (_lines.Any())
                _lines[_lines.Count-1] += value;
            else
                _lines.Add(value);
        }

        public string ToHtmlString()
        {
            var html = "<table>";
            _lines.ToList().ForEach(x => html += $"<tr><td>{x}</td></tr>");
            html += "</table>";
            return html;
        }
    }
}