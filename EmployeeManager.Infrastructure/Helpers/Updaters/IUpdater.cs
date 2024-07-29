namespace EmployeeManager.Helpers.Updaters;

public interface IUpdater<T,K>
{
    void Update(T originalEntity, K updateEntity);
}