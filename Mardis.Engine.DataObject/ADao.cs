using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataObject.MardisCommon;
using Mardis.Engine.Framework;
using Mardis.Engine.Web.ViewModel.Filter;
using Microsoft.EntityFrameworkCore;

namespace Mardis.Engine.DataObject
{
    public abstract class ADao
    {
        protected CoreFilterDetailDao CoreFilterDetailDao;
        protected CoreFilterDao CoreFilterDao;

        protected ADao(MardisContext mardisContext)
        {
            Context = mardisContext;
        }
    

        public MardisContext Context { get; }

        /// <summary>
        /// Insert or Update 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public T InsertOrUpdate<T>(T entity) where T : class, IEntity
        {
            try
            {
                var stateRegister = Guid.Empty == entity.Id ? EntityState.Added : EntityState.Modified;

                if (Context.Entry(entity).State == EntityState.Detached && stateRegister == EntityState.Added)
                {
                    Context.Set<T>().Add(entity);
                }

                Context.Entry(entity).State = stateRegister;

                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ExceptionMardis(ex.Message, ex);
            }

            return entity;
        }
        public T InsertOrUpdateID<T>(T entity) where T : class, IEntityId
        {
            try
            {
                var stateRegister = 0 == entity.Id ? EntityState.Added : EntityState.Modified;

                if (Context.Entry(entity).State == EntityState.Detached && stateRegister == EntityState.Added)
                {
                    Context.Set<T>().Add(entity);
                }

                Context.Entry(entity).State = stateRegister;

                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ExceptionMardis(ex.Message, ex);
            }

            return entity;
        }
        public List<T> GetPaginatedList<T>(int pageIndex, int pageSize) where T : class, IEntity
        {
            var sortedList = Context.Set<T>()
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return sortedList;
        }

        public void Update<T>(T entity) where T : class
        {
            Context.Update(entity);
        }

        public void PhysicalDelete<T>(T entity) where T : class
        {
            try
            {

                if (Context.Entry(entity).State == EntityState.Detached)
                {
                    Context.Set<T>().Add(entity);
                }

                Context.Set<T>().Remove(entity);

                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

        protected string GetFilterPredicate(List<FilterValue> filterValues, string operatorFilter = "AND")
        {
            var op = operatorFilter == "OR" ? "|| " : "&& ";
            var builderPredicate = new StringBuilder();

            if (filterValues != null)
            {
                foreach (var filterValue in filterValues)
                {
                    var filter = CoreFilterDetailDao.GetCoreFilterDetail(filterValue.IdFilter);

                    var filterTable = string.IsNullOrEmpty(filter.Table) ? "" : filter.Table + ".";

                    switch (filterValue.Criteria)
                    {
                        case "Contains":
                            builderPredicate.Append(op + filterTable + filter.Property + ".");
                            builderPredicate.Append(filterValue.Criteria + "(\"");
                            builderPredicate.Append(filterValue.Value + "\")");
                            break;
                        case "NotContains":
                            builderPredicate.Append(op + "!" + filterTable + filter.Property + ".");
                            builderPredicate.Append("Contains(\"");
                            builderPredicate.Append(filterValue.Value + "\")");
                            break;
                        default:
                            if (filterValue.NameFilter.IndexOf("Id", StringComparison.Ordinal) >= 0)
                            {
                                var fs = $" == \"{filterValue.Value}\" ";
                                builderPredicate.Append(op + filterTable + filter.Property + fs);
                            }
                            else
                            {
                                builderPredicate.Append(op + filterTable + filter.Property + " ");
                                builderPredicate.Append(filterValue.Criteria + " \"");
                                builderPredicate.Append(filterValue.Value + "\"");
                            }
                            break;
                    }
                }
            }
            return builderPredicate.ToString();
        }

    }
}
