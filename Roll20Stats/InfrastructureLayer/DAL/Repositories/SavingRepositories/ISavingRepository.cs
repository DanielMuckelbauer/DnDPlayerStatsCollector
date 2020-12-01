namespace Roll20Stats.InfrastructureLayer.DAL.Repositories.SavingRepositories
{
    public interface ISavingRepository<TModel>
    {
        void Save();
        TModel GetById(int id);
        void Add(TModel model);
        void Update(TModel model);
        void Remove(TModel model);
    }
}
