using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.CreationalPatterns.Builder.Examples
{
    public class Order
    {
        public string City { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public class Builder: OrderNameBuilder<Builder> { }

        public Builder New () => new Builder();
    }

    public abstract class OrderBuilder
    {
        protected Order order = new Order();
        public Order Build()
        {
            return order;
        }
    }

    public class OrderAddressBuilder<SELF> :OrderBuilder where SELF : OrderAddressBuilder<SELF>
    {
        public SELF Street(string street)
        {
            order.Street = street;
            return (SELF)this;
        }
    }
    public class OrderNameBuilder<SELF>:OrderAddressBuilder<OrderNameBuilder<SELF>> where SELF : OrderNameBuilder<SELF>
    {
        public SELF UserName(string name)
        {
            order.Name = name;
            return (SELF)this;
        }
    }

    /**
     * This is why we need to use type argument
     * var result = ((TestC)((TestC)c.CheckA()).CheckB()).CheckC();
     */
    public class TestA
    {
        public int variable = 5;
        public TestA CheckA()
        {
            return this;
        }
    }

    public class TestB:TestA
    {
        public TestB CheckB()
        {
            variable = 4;
            Console.WriteLine("B has been checked");
            return this;
        }
    }

    public class TestC:TestB 
    {
        public TestC CheckC()
        {
            variable = 3;
            Console.WriteLine("C has been checked");
            return this;
        }
    }
 }
