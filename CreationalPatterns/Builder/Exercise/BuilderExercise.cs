using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns.CreationalPatterns.Builder.Exercise
{
    //Create order by using facade
    //1.Order class


    class BuilderExercise
    {
        //public static void Main(string[] args)
        //{
        //    var order = new OrderBuilder()
        //        .Dish
        //            .DishName("Pizza")
        //            .AddIngredient(new Mushrooms())
        //            .AddIngredient(new Cheese())
        //            .ConfirmDish()
        //        .Dish
        //            .DishName("Burger")
        //            .DishName("Pizza")
        //            .AddIngredient(new Cheese())
        //            .AddIngredient(new Burger())
        //            .ConfirmDish()
        //        .Pay
        //            .By("Card")
        //            .Currency("BitCoin")
        //            .ConfirmPaymentType()
        //        .Address
        //            .Street("81 Broadfield Road")
        //            .City("Bristol")
        //            .PostCode("BS42UH")
        //            .ConfirmAddress()
        //        .ConfirmOrder();


        //    order.PrintCheck();
        //}
    }

    public class OrderBuilder
    {
        protected Order Order = new Order();

        public DishBuilder Dish => new DishBuilder(Order);

        public AddressBuilder Address => new AddressBuilder(Order);

        public PaymentBuilder Pay => new PaymentBuilder(Order);

        public Order ConfirmOrder()
        {
            return Order;
        }

    }
    public class Order
    {
        public OrderInfo OrderInfo = new OrderInfo();

  

        public void PrintCheck()
        {
            OrderInfo.DishInfo();
            OrderInfo.AddressInfo();
            OrderInfo.PaymentInfo();
            OrderInfo.Total();
        }
    }

    public class OrderInfo
    {
        public List<Dish> Items = new List<Dish>();
        public Address OrderAddress;
        public PaymentType Payment;

        public void DishInfo()
        {
            StringBuilder info = new StringBuilder()
                .Append($"Ordered positions: \n");
            foreach (var i in Items)
            {
                info.Append(i.Name + "\n");
            }
            Console.WriteLine(info.ToString());
        }

        public void AddressInfo()
        {
            StringBuilder info = new StringBuilder()
                .Append($"Address: \n")
                .Append(OrderAddress.Street + "\n")
                .Append(OrderAddress.City + "\n")
                .Append(OrderAddress.Postcode + "\n");
            Console.WriteLine(info.ToString());
        }

        public void PaymentInfo()
        {
            var info = new StringBuilder()
                .Append($"Payment by: \n")
                .Append(Payment.Type + "\n")
                .Append(Payment.Currency + "\n");
            Console.Write(info.ToString());

        }

        public void Total()
        {
            var total = Items.Sum(i => i.Price());
            Console.WriteLine("Total: " + total);
        }
    }

    public class DishBuilder:OrderBuilder
    {
        public DishBuilder(Order order)
        {
            this.Order = order;
        }
        private Dish Dish = new Dish();

        public DishBuilder DishName(string name)
        {
            Dish.Name = name;
            return this;
        }

        public DishBuilder AddIngredient(Ingredient ingredient)
        {
            Dish.Ingredients.Add(ingredient);
            return this;
        }

        public DishBuilder ConfirmDish()
        {
            Order.OrderInfo.Items.Add(Dish);
            Dish = new Dish();
            return this;
        }

    }


    public class Dish
    {
        public string Name { get; set; }

        public List<Ingredient> Ingredients = new List<Ingredient>();

        public double CalculatePrice()
        {
            return Math.Round(Ingredients.Sum(x => x.Price),2);
        }

        public double Price()
        {
            return CalculatePrice();
        }

        public override string ToString()
        {
            var sb = new StringBuilder()
                .Append($"Name of the item: {Name}\n");
            foreach(var i in Ingredients)
            {
                sb.Append(i.Name+ " ");
            }
            sb.Append("\nPrice is: " + Price());

            return sb.ToString();
        }

    }

    public abstract class Ingredient
    {
        public abstract string Name { get; set; }
        public abstract double Price { get; set; }

        public override string ToString()
        {
            return $"Name of Ingredient{Name} \nPrice of ingredient: {Price} \n";
        }
    }

    public class Cheese : Ingredient
    {
        public override string Name { get; set; } = "Cheese";
        public override double Price { get; set; } = 2.55;
    }

    public class Mushrooms : Ingredient
    {
        public override string Name { get; set; } = "Mushrooms";
        public override double Price { get; set; } = 1.80;
    }

    public class Burger : Ingredient
    {
        public override string Name { get; set; } = "Hamburger";
        public override double Price { get; set; } = 3.60;
    }



    public class Address:OrderBuilder
    {

        public string Street { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }

    }

    public class AddressBuilder:OrderBuilder
    {
        private Address Address = new Address();

        public AddressBuilder(Order order)
        {
            this.Order = order;
        }
        public AddressBuilder Street(string streetName)
        {
            Address.Street = streetName;
            return this;
        }

        public AddressBuilder City(string cityName)
        {
            Address.City = cityName;
            return this;
        }

        public AddressBuilder PostCode(string postcode)
        {
            Address.Postcode = postcode;
            return this;
        }

        public AddressBuilder ConfirmAddress()
        {
            Order.OrderInfo.OrderAddress = Address;
            return this;
        }

    }

    public class PaymentType
    {
        public string Type { get; set; }

        public string Currency { get; set; }
    }

    public class PaymentBuilder:OrderBuilder
    {
        private PaymentType PaymentType = new PaymentType();

        public PaymentBuilder(Order order)
        {
            this.Order = order;
        }
        public PaymentBuilder By(string type)
        {
            PaymentType.Type = type;
            return this;
        }

        public PaymentBuilder Currency(string currency)
        {
            PaymentType.Currency = currency;
            return this;
        }

        public PaymentBuilder ConfirmPaymentType()
        {
            this.Order.OrderInfo.Payment = PaymentType;
            return this;
        }

    }

    
}
