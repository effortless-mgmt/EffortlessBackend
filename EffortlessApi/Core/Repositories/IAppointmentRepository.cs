using System.Threading.Tasks;
using EffortlessApi.Core.Models;

namespace EffortlessApi.Core.Repositories
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        Task UpdateAsync(long appointmentID, Appointment newAppointment);
    }
}