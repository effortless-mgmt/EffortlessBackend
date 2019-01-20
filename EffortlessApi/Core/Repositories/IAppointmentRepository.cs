using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EffortlessApi.Core.Models;

namespace EffortlessApi.Core.Repositories
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        Task UpdateAsync(long id, Appointment newAppointment);
        Task<IEnumerable<Appointment>> FindNoOwnerAsync(Expression<Func<Appointment, bool>> predicate);
        Task<IEnumerable<Appointment>> GetByUserIdAsync(long id);
        Task<IEnumerable<Appointment>> GetByWorkPeriodId(long id);
    }
}