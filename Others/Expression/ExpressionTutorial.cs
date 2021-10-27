using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DesignPatterns.Others 
{
    class ExpressionTutorial
    {
        public Expression<Func<User, object>> exp = user => user.Name;
        public Expression<Func<User, object>> anotherExp = user => user.Age;

        public string CreateUrl(string url,params string[] fields)
        {
            var selectedFields = string.Join(',', fields);
            Console.WriteLine("Fields after joining: "+selectedFields);

            Console.WriteLine("Fields after concat: "+ string.Concat(url, "?fields=", selectedFields));
            return string.Concat(url, "?fields=", selectedFields);
        }


        //We can call it as following
        //Console.WriteLine("By using expression: "+exp.CreateUrlWithExpression("www.google.com/",u=>u.Name,u=>u.Age));

        public string CreateUrlWithExpression(string url,params Expression<Func<User,object>>[] fieldSelectors)
        {
            var fields = new List<string>();
            foreach(var selector in fieldSelectors)
            {
                var body = selector.Body;
                if(body is MemberExpression me)
                {
                    fields.Add(me.Member.Name.ToLower());
                }
                else if(body is UnaryExpression ue)
                {
                    fields.Add(((MemberExpression)ue.Operand).Member.Name.ToLower());
                }
            }
            var selectedFields = string.Join(',', fields);
            return string.Concat(url, "?fields=", selectedFields);
        }
        public void CheckExpression()
        {
            var exp = new ExpressionTutorial();
            var body = exp.exp.Body;

            Console.WriteLine("Body is " + body);
            if (body is MemberExpression me)
            {
                Console.WriteLine(me.Member.Name);
            }
        }

        public void BuildExpression(string propName, SomeClass obj)
        {
            var parameter = Expression.Parameter(typeof(SomeClass));
            var accessor = Expression.PropertyOrField(parameter, propName);

            var lambda = Expression.Lambda(accessor, false, parameter);

            Console.WriteLine(lambda.Compile().DynamicInvoke(obj));
        }
    }
    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }

    }

    public class SomeClass
    {
        public int Number { get; set; }
        public string SomeText { get; set; }
    }
}
