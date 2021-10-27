using JetBrains.Annotations;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DesignPatterns.BehavioralPatterns.Observer
{
    public class BidirectionalObserverDemo
    {
        //public static void Main(string[] args)
        //{
        //    var product = new Product { Name = "Product" };
        //    var window = new Display { ProductName = "Product" };

            //product.PropertyChanged += (sender, eventArgs) =>
            //{
            //    if (eventArgs.PropertyName == "Name")
            //    {
            //        Console.WriteLine("Name changed in Product");
            //        window.ProductName = product.Name;
            //    }
            //};

            //window.PropertyChanged += (sender, eventArgs) =>
            //{
            //    if (eventArgs.PropertyName == "ProductName")
            //    {
            //        Console.WriteLine("Name changed in Product");
            //        product.Name = window.ProductName;
            //    }
            //};

        //    Console.WriteLine(product);
        //    Console.WriteLine(window);


        //    using var binding = new BidirectionalBinding(
        //        product,
        //        () => product.Name,
        //        window,
        //        () => window.ProductName);


        //    //Console.WriteLine(product);
        //    //Console.WriteLine(window);

        //    product.Name = "Product name changed";
        //    window.ProductName = "This name has been changed from window object";

        //    Console.WriteLine(product);
        //    Console.WriteLine(window);
        //}
    }

    public class Product : INotifyPropertyChanged
    {
        private string name;
        public string Name {
            get => name;
            set
            {
                if (value == name) return;
                name = value;
                OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return $"Product name is: {Name}";
        }
    }
    public class Display : INotifyPropertyChanged
    {
        private string productName;
        public string ProductName
        {
            get => productName;
            set
            {
                if (value == productName) return;
                productName = value;
                OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return $"Product name is: {ProductName}";
        }
    }

    public sealed class BidirectionalBinding : IDisposable
    {
        private bool disposed;

        public BidirectionalBinding(
            INotifyPropertyChanged first,
            Expression<Func<object>> firstProperty, // ()=>x.Foo;
            INotifyPropertyChanged second,
            Expression<Func<object>> secondProperty)
        {
            //xxxProperty is MemberExpression
            //Member of the above is PropertyInfo
            if(firstProperty.Body is MemberExpression firstExpr
                && secondProperty.Body is MemberExpression secondExpr)
            {
                if(firstExpr.Member is PropertyInfo firstProp &&
                    secondExpr.Member is PropertyInfo secondProp)
                {
                    first.PropertyChanged += (sender, args) =>
                    {
                        if (!disposed)
                        {
                            secondProp.SetValue(second,
                                firstProp.GetValue(first));
                        }
                    };

                    second.PropertyChanged += (sender, args) =>
                    {
                        if (!disposed)
                        {
                            firstProp.SetValue(first,
                                secondProp.GetValue(second));
                        }
                    };
                }
            }
        }
        public void Dispose()
        {
            disposed = true;
        }
    }
}
