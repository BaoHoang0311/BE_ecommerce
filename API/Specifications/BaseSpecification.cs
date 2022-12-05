﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace API.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : class, new()
    {
        public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T,bool>> _condition )
        {
            Condition = _condition;
        }
        public Expression<Func<T, bool>> Condition { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public void Add_Include(Expression<Func<T, object>> includeExpess)
        {
            Includes.Add(includeExpess);
        }
    }
}