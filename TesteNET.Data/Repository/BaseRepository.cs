using ITSingular.TesteNET.Data.Context;
using ITSingular.TesteNET.DataTransfer.Entityes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Linq.Expressions;

namespace ITSingular.TesteNET.Data.Repository
{
    public abstract class BaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected TesteNETContext Db;
        private DbSet<TEntity> DbSet;

        public BaseRepository()
        {
            Db = new TesteNETContext();
            DbSet = Db.Set<TEntity>();
        }

        public virtual TEntity DeatachAndFind(TEntity obj, Guid id)
        {
            this.Deatach(obj);
            return DbSet.Find(id);
        }

        public void Deatach(TEntity obj)
        {
            Db.Entry(obj).State = EntityState.Detached;
        }

        public virtual void Add(TEntity obj)
        {
            try
            {
                DbSet.Add(obj);
                SaveChanges();
            }
            catch (Exception ex)    
            {
                throw ex;
            }
        }

        public virtual TEntity GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        public virtual IEnumerable<TEntity> GetAllPaged(int skip, int take)
        {
            return DbSet.ToList().Skip(skip).Take(take);
        }

        public virtual void Update(TEntity obj)
        {
            try
            {
                var entry = Db.Entry(obj);
                DbSet.Attach(obj);
                entry.State = EntityState.Modified;
                SaveChanges();
                Deatach(obj);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                    throw dbEx;
            }
        }

        public virtual void Remove(Guid id)
        {
            DbSet.Remove(GetById(id));
            SaveChanges();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void AddWithTransaction(IList<TEntity> lstModel)
        {
            Context.TesteNETContext context = null;
            try
            {
                context = new Context.TesteNETContext();
                context.Configuration.AutoDetectChangesEnabled = false;

                int count = 0;
                foreach (var entityToInsert in lstModel)
                {
                    ++count;
                    context = AddToContext(context, entityToInsert, count, 100, true);
                }

                context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);

                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (context != null)
                    context.Dispose();
            }

        }

        public void UpdateWithTransaction(List<TEntity> lstUpdate)
        {
            TesteNETContext context = null;
            try
            {
                context = new TesteNETContext();
                context.Configuration.AutoDetectChangesEnabled = false;

                var lstDistinct = lstUpdate.Distinct().ToList();

                int count = 0;
                foreach (var entityToUpdate in lstDistinct)
                {
                    ++count;
                    context = AddToContextUpdate(context, entityToUpdate, count, 200, false);
                }

                context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);

                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            catch (Exception sex)
            {
                throw sex;
            }
            finally
            {
                if (context != null)
                    context.Dispose();
            }
        }

        public void AddWithTransactionPlus(List<TEntity> lstModel)
        {
            try
            {
                Db.Configuration.AutoDetectChangesEnabled = false;

                int count = 0;
                foreach (var entityToInsert in lstModel)
                {
                    ++count;
                    Db = AddToContext(Db, entityToInsert, count, 100, false);
                }

                Db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);

                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Db.Configuration.AutoDetectChangesEnabled = true;
            }
        }


        public void UpdateWithTransactionPlus(List<TEntity> lstUpdate)
        {
            try
            {
                Db.Configuration.AutoDetectChangesEnabled = false;

                var lstDistinct = lstUpdate.Distinct().ToList();

                int count = 0;
                foreach (var entityToUpdate in lstDistinct)
                {
                    ++count;
                    Db = AddToContextUpdate(Db, entityToUpdate, count, 200, false);
                }

                Db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            catch (Exception sex)
            {
                throw sex;
            }
            finally
            {
                Db.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        public TesteNETContext AddToContextUpdate(TesteNETContext context, TEntity entity, int count, int commitCount, bool recreateContext)
        {
            try
            {
                var entry = context.Entry(entity);

                context.Set<TEntity>().Attach(entity);
                entry.State = EntityState.Modified;

                if (count % commitCount == 0)
                {
                    context.SaveChanges();
                    if (recreateContext)
                    {
                        context.Dispose();
                        context = new Context.TesteNETContext();
                        context.Configuration.AutoDetectChangesEnabled = true;
                    }
                }
            }
            catch (OptimisticConcurrencyException)
            {
                context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);

                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return context;
        }

        public TesteNETContext AddToContext(TesteNETContext context, TEntity entity, int count, int commitCount, bool recreateContext)
        {
            try
            {
                context.Set<TEntity>().Add(entity);

                if (count % commitCount == 0)
                {
                    context.SaveChanges();
                    if (recreateContext)
                    {
                        context.Dispose();
                        context = new Context.TesteNETContext();
                        context.Configuration.AutoDetectChangesEnabled = false;
                    }
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);

                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return context;
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
