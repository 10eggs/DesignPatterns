using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.BehavioralPatterns.Strategy
{
    //Partialy specify behavior of the system
    //Called also as a POLICY
    //Enables exact behavior of the system to be seleceted either at the run-time
    //or compile-time
    public class Demo
    {
        //public static void Main(string[] args)
        //{
        //    var textProcessor = new TextProcess();
        //    textProcessor.SetOutputFormat(OutputFormat.Markdown);
        //    textProcessor.AppendList(new[] { "foo", "bar", "baz" });
        //    Console.WriteLine(textProcessor);

        //    textProcessor.Clear();

        //    textProcessor.SetOutputFormat(OutputFormat.Html);
        //    textProcessor.AppendList(new[] { "foo", "bar", "baz" });
        //    Console.WriteLine(textProcessor);


        //}
    }

    public enum OutputFormat
    {
        Markdown,
        Html
    }

    //<ul><li>foo</li></ul>
    public interface IListStrategy
    {
        void Start(StringBuilder sb);
        void End(StringBuilder sb);
        void AddListItem(StringBuilder sb, string item);
    }

    public class HtmlListStrategy : IListStrategy
    {
        public void AddListItem(StringBuilder sb, string item)
        {
            sb.AppendLine($"    <li>{item}</li>");
        }

        public void End(StringBuilder sb)
        {
            sb.AppendLine("</ul>");
        }

        public void Start(StringBuilder sb)
        {
            sb.AppendLine("<ul>");
        }
    }

    public class MarkdownListStrategy : IListStrategy
    {
        public void AddListItem(StringBuilder sb, string item)
        {
            sb.AppendLine($"*   {item}");
        }

        public void End(StringBuilder sb)
        {

        }

        public void Start(StringBuilder sb)
        {

        }
    }

    public class TextProcess
    {
        private StringBuilder sb = new StringBuilder();
        IListStrategy listStrategy;
        
        public void SetOutputFormat(OutputFormat format)
        {
            switch (format)
            {
                case OutputFormat.Html:
                    listStrategy = new HtmlListStrategy();
                    break;
                case OutputFormat.Markdown:
                    listStrategy = new MarkdownListStrategy();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(format), format, null);

            }
        }

        public void AppendList(IEnumerable<string> items)
        {
            listStrategy.Start(sb);
            foreach(var item in items)
                listStrategy.AddListItem(sb, item);
            listStrategy.End(sb);
        }

        public StringBuilder Clear()
        {
            return sb.Clear();
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }

}
