using Microsoft.AspNet.Identity.EntityFramework;

namespace Farm_Central.Controllers
{
    internal class RoleManager<T1, T2, T3>
    {
        private RoleStore<IdentityRole> roleStore;

        public RoleManager(RoleStore<IdentityRole> roleStore)
        {
            this.roleStore = roleStore;
        }
    }
}