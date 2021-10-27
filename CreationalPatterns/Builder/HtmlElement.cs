using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.CreationalPatterns.Builder
{
    public class HtmlElement
    {
        public string Name, Text;
        public List<HtmlElement> Elements = new List<HtmlElement>();
        private const int indentSize = 2;
        public HtmlElement()
        {
                
        }
        public HtmlElement(string name, string text)
        {
            Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            Text = text ?? throw new ArgumentNullException(paramName: nameof(text));
        }

        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', indentSize * indent);
            sb.AppendLine($"{i}<{Name}>");

            if (!string.IsNullOrWhiteSpace(Text))
            {
                sb.Append(new string(' ', indentSize * (indent + 1)));
                sb.AppendLine(Text);
            }
            foreach (var e in Elements)
            {
                sb.Append(e.ToStringImpl(indent + 1));
            }
            sb.AppendLine($"{i}</{Name}>");

            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }

        public class HtmlBuilder
        {
            private readonly string rootName;
            HtmlElement root = new HtmlElement();
            public HtmlBuilder(string rootName)
            {
                this.rootName = rootName;
                root.Name = rootName;
            }

            //This behavior is called "Fluent interface". This allows you to chain several calls
            public HtmlBuilder AddChild(string childName, string childText)
            {
                var e = new HtmlElement(childName, childText);
                root.Elements.Add(e);
                return this;
            }

            public HtmlBuilder AddChild(HtmlBuilder builder)
            {
                root.Elements.Add(builder.root);
                return this;
            }

            //Added by me
            public HtmlBuilder AddChilds(HtmlElement elem)
            {
                root.Elements.Add(elem);
                return this;
            }

            public override string ToString()
            {
                return root.ToString();
            }

            public void Clear()
            {
                root = new HtmlElement { Name = rootName };
            }
        }
    }
}
