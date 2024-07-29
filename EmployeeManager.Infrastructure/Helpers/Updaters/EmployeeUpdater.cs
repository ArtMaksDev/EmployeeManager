
using Common;
using EmployeeManager.Domain.Models;
using EmployeeManager.Helpers.Updaters;
using Newtonsoft.Json.Linq;

namespace EmployeeManager.Infrastructure.Helpers.Updaters;

public class EmployeeUpdater : IUpdater<JToken, Employee>
{
    public void Update(JToken originalEntity, Employee updateEntity)
    {
        Argument.IsNotNull(originalEntity, nameof(originalEntity));
        Argument.IsNotNull(updateEntity, nameof(updateEntity));

        if (!string.IsNullOrEmpty(updateEntity.FirstName))
        {
            originalEntity[nameof(updateEntity.FirstName)] = updateEntity.FirstName;
        }

        if (!string.IsNullOrEmpty(updateEntity.LastName))
        {
            originalEntity[nameof(updateEntity.LastName)] = updateEntity.LastName;
        }

        if (updateEntity.SalaryPerHour is > 0)
        {
            originalEntity[nameof(updateEntity.SalaryPerHour)] = updateEntity.SalaryPerHour;
        }
    }
}