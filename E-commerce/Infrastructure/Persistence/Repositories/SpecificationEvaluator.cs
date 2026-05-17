using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    internal static class SpecificationEvaluator
    {

        public static IQueryable<T> GetQuery <T>(IQueryable<T> inputQuery,Specifications<T> specifications) where T :class
        {
            var query = inputQuery;

            if(specifications.Criteria != null)
            {
                query.Where(specifications.Criteria);
            }
            //foreach (var item in specifications.IncludeExpression)
            //{
            //    query = query.Include(item);
            //}

            query = specifications.IncludeExpression.
                Aggregate(query, (curr, includeExpression) => curr.Include(includeExpression));


            if (specifications.OrderBy != null)
            {
                query.OrderBy(specifications.OrderBy);
            }

            else if(specifications.OrderByDesc != null)
            {
                query.OrderByDescending(specifications.OrderByDesc);
            }

            if (specifications.IsPaginated)
            {
                query = query.Skip(specifications.Skip).Take(specifications.Take);
            }


            return query;

                

        }  
    }
}
