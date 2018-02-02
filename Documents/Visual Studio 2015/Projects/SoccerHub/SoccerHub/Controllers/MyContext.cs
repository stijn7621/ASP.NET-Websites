using System.Data.Entity;
using System.Data.Entity.Validation;

namespace SoccerHub.Controllers
{
    public class MyContext : DbContext
    {
        #region Overriding Methods
        // -- OVERRIDING METHODS --
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var newException = new FormattedDbEntityValidationException(e);
                throw newException;
            }
        }
        #endregion
    }
}