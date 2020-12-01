namespace Roll20Stats.InfrastructureLayer.DAL.Context
{
    public interface IApplicationContext
    {
        int SaveChanges();
    }
}