using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.BehavioralPatterns.Strategy
{
    public class StaticStrategy
    {
        //public static void Main(string[] args)
        //{
        //    //cb.Register<MarkdownListStrategy>().As<IListStrategy>();
        //    var tp = new TextProcessStatic<MarkdownListStrategy>();
        //    tp.AppendList(new[] { "foo", "bazz", "barr" });
        //    Console.WriteLine(tp);

        //    var tp2 = new TextProcessStatic<HtmlListStrategy>();
        //    tp2.AppendList(new[] { "foo", "bazz", "barr" });
        //    Console.WriteLine(tp2);

        //}
    }

    public class TextProcessStatic<LS> where LS:IListStrategy, new ()
    {
        private StringBuilder sb = new StringBuilder();
        IListStrategy listStrategy = new LS();

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
            foreach (var item in items)
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
