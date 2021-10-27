using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DesignPatterns.EnterprisePatterns.Specification
{
    /**
     * There are 3 main use cases for the Specification pattern:

        Looking up data in the database. That is finding records that match the specification we have in hand.
        Validating objects in the memory. In other words, checking that an object we retrieved or created fits the spec.
        Creating a new instance that matches the criteria. This is useful in scenarios where you don’t care about the actual content of the instances, but still need it to have certain attributes.
     */

    public abstract class Specification<T>
    {
        public abstract Expression<Func<T, bool>> ToExpression();

        public bool IsSatisfied(T item)
        {
            Func<T, bool> predicate = ToExpression().Compile();
            return predicate(item);
        }
    }

    public class MpaaRatingAtTheMostSpecification : Specification<Movie>
    {
        private readonly MpaaRating _rating;
        public MpaaRatingAtTheMostSpecification(MpaaRating rating)
        {
            _rating = rating;
        }
        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => movie.MpaaRating == _rating;
        }
    }

    public class MovieRatingSpecification : Specification<Movie>
    {
        private double _rating;
        public MovieRatingSpecification(double rating)
        {
            _rating = rating;
        }
        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => movie.Rating > _rating;
        }
    }


    public class AndSpecification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;
        public AndSpecification(Specification<T> left, Specification<T> right)
        {
            _left = left;
            _right = right;
        }
        public Expression<Func<T,T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> leftExpression = _left.ToExpression();
            Expression<Func<T, bool>> rightExpression = _right.ToExpression();

            BinaryExpression andExpression = Expression.AndAlso(leftExpression.Body, rightExpression.Body);
            Console.WriteLine(leftExpression.Parameters.Single());
            Console.WriteLine(rightExpression.Parameters.Single());
            return Expression.Lambda<Func<T, T, bool>>(andExpression, new[]{
                leftExpression.Parameters[0],
                rightExpression.Parameters[0]
            });
            ;
            
        }

        public bool IsSatisfied(T item)
        {
            Func<T,T, bool> predicate = ToExpression().Compile();
            return predicate(item,item);
        }
    }
 
}
