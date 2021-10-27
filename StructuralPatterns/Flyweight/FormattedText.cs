using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.StructuralPatterns.Flyweight
{
    public class BetterFormatter
    {
        private string _plainText;
        private List<TextRange> formatting = new List<TextRange>();
        public BetterFormatter(string plainText)
        {
            _plainText = plainText;
        }

        public TextRange GetRange(int start, int end)
        {
            var range = new TextRange { Start = start, End = end };
            formatting.Add(range);
            return range;
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var index = 0; index<_plainText.Length; index++)
            {
                var c = _plainText[index];
                foreach(var range in formatting)
                {
                    if(range.Covers(index) && range.Capitalize)
                    {
                        c= char.ToUpper(c);
                    }
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
    public class TextRange
    {
        public int Start, End;
        public bool Capitalize, Bold, Italic;

        public bool Covers(int position)
        {
            return position >= Start && position <= End;
        }
    }
    public class FormattedText
    {
        //public static void Main()
        //{
        //    var ft = new FormattedText("This is a new brave world");
        //    ft.Capitalized(10, 15);
        //    Console.WriteLine(ft);

        //    var ft1 = new BetterFormatter("This is a new brave world");
        //    ft1.GetRange(10, 15).Capitalize = true;
        //    Console.WriteLine(ft1);
        //}
        private readonly string plainText;
        private bool[] capitalize;
        public FormattedText(string plainText)
        {
            this.plainText = plainText;
            capitalize = new bool[plainText.Length];
        }

        public void Capitalized(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                capitalize[i] = true;
            }
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < plainText.Length; i++)
            {
                var c = plainText[i];
                sb.Append(capitalize[i] ? char.ToUpper(c) : c);
            }
            return sb.ToString();
        }
    }
}

