using Multitrans.Repositories;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Multitrans
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers    
            container.RegisterType<IAuthentificationRepository, AuthentificationRepositoryImpl>();
            container.RegisterType<ICAllApi, CallApiRepositoryImpl>();
            container.RegisterType<IUserRepository, UserRepositoryImpl>();
			container.RegisterType<IAgenceRepository, AgenceRepositoryImpl>();
            container.RegisterType<IOperationRepository, OperationRepositoryImpl>();
            container.RegisterType<IOperateurRepository, OperateurRepositoryImpl>();
            container.RegisterType<ISoldeDebuterJourneeRepository, SoldeDebuterJourneeRepositoryImpl>();
            container.RegisterType<ISoldeCloturerJourneeRepository, SoldeCloturerJourneeRepositoryImpl>();
			container.RegisterType<ITransactionRepository, TransactionRepositoryImpl>();
			container.RegisterType<ISoldeReelRepository, SoldeReelRepositoryImpl>();
			container.RegisterType<IDepenseExtratRepository, DepenseExtratRepositoryImpl>();
			container.RegisterType<IOperateurRepository, OperateurRepositoryImpl>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}